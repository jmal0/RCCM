using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AxTrioPCLib;
using System.Windows.Forms;
using System.Threading;

namespace RCCM
{
    /// <summary>
    /// Class representing Trio stepper motor controller
    /// </summary>
    public class TrioController
    {
        /// <summary>
        /// Enum constant identifying controller port as an ethernet connection
        /// </summary>
        public static short PORT_TYPE = 2;
        /// <summary>
        /// Port id used to connect to controller
        /// </summary>
        public static short PORT_ID = 3240;
        /// <summary>
        /// Static IP address of controller
        /// </summary>
        public static string IP = "192.168.0.250";
        /// <summary>
        /// Number of axes on controller
        /// </summary>
        public static short NUMBER_AXES = 8;
        /// <summary>
        /// Enum value identifying a step-direction stepper driver type
        /// </summary>
        public static short ATYPE = 43;
        /// <summary>
        /// Property names accessible for each motor
        /// </summary>
        public static string[] AX_PROPERTIES = { "ATYPE", "P_GAIN", "I_GAIN", "D_GAIN", "OV_GAIN", "VFF_GAIN", "UNITS", "SPEED", "ACCEL", "DECEL", "CREEP", "JOGSPEED", "FE_LIMIT", "DAC", "SERVO", "REP_DIST", "FWD_IN", "REV_IN", "DATUM_IN", "FS_LIMIT", "RS_LIMIT", "MTYPE", "NTYPE", "MPOS", "DPOS", "FE", "AXISSTATUS" };
        /// <summary>
        /// ActiveX control for Trio controller
        /// </summary>
        private AxTrioPC triopc;
        /// <summary>
        /// Indicates whether or not controller is connected and port is opened
        /// </summary>
        public bool Open { get; private set; }

        /// <summary>
        /// Connect to and initialize Trio controller
        /// </summary>
        /// <param name="axTrioPC">Trio ActviveX control</param>
        public TrioController(AxTrioPC axTrioPC)
        {
            this.triopc = axTrioPC;

            this.triopc.HostAddress = TrioController.IP;
            this.Open = this.triopc.Open(TrioController.PORT_TYPE, TrioController.PORT_ID);

            for (short ax = 0; ax < TrioController.NUMBER_AXES; ax++)
            {
                this.triopc.SetAxisVariable("ATYPE", ax, TrioController.ATYPE);
                this.triopc.SetAxisVariable("SERVO", ax, 0);
            }

            if (this.Open)
            {
                this.triopc.SetVariable("WDOG", 1);
            }
            else
            {
                MessageBox.Show("Could not connect to motion controler. Non-virtual axes are disabled");
            }
        }
        
        /// <summary>
        /// Check if an axis is currently moving
        /// </summary>
        /// <param name="nAxis">Number (0-7) of port where axis is connected to trio controller</param>
        /// <returns>True if axis is performing a motion</returns>
        public bool isMoving(short nAxis)
        {
            double distRemaining;
            this.triopc.GetAxisVariable("REMAIN", nAxis, out distRemaining);
            return Math.Abs(distRemaining) > 0.001;
        }

        /// <summary>
        /// Get all axis property values
        /// </summary>
        /// <param name="nAxis">Number (0-7) of port where axis is connected to trio controller</param>
        /// <returns>Array of property values with indices corresponding to TrioController.AX_PROPERTIES</returns>
        public double[] GetAllAxisProperties(short nAxis)
        {
            // Double which will store read property value
            double dReadVar;
            //
            double[] properties = new double[TrioController.AX_PROPERTIES.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                // Read property into dReadVar
                if (this.triopc.IsOpen(TrioController.PORT_ID) &&
                    this.triopc.GetAxisVariable(TrioController.AX_PROPERTIES[i], nAxis, out dReadVar))
                {
                    properties[i] = dReadVar;
                }
            }
            return properties;
        }

        /// <summary>
        /// Get a specified property of a motor
        /// </summary>
        /// <param name="property">Name of property to check</param>
        /// <param name="nAxis">Number (0-7) of port where axis is connected to trio controller</param>
        /// <returns>Specified property value</returns>
        public double GetAxisProperty(string property, short nAxis)
        {
            double dReadVar;
            if (this.triopc.IsOpen(TrioController.PORT_ID) && this.triopc.GetAxisVariable(property, nAxis, out dReadVar))
            {
                return dReadVar;
            }
            throw new Exception(string.Format("Invalid property: {0}", property));
        }

        /// <summary>
        /// Set a specified property of a motor
        /// </summary>
        /// <param name="property">Name of property to check</param>
        /// <param name="value">Newe value of property</param>
        /// <param name="nAxis">Number (0-7) of port where axis is connected to trio controller</param>
        /// <returns>True if value was set successfully</returns>
        public bool SetAxisProperty(string property, double value, short nAxis)
        {
            if (this.triopc.IsOpen(TrioController.PORT_ID))
            {
                return this.triopc.SetAxisVariable(property, nAxis, value);
            }
            throw new Exception(string.Format("Invalid property: {0}", property));
        }

        /// <summary>
        /// Get property value (could be axis or controller property)
        /// </summary>
        /// <param name="property">Property name</param>
        /// <returns>Current property value</returns>
        public double GetProperty(string property)
        {
            double dReadVar;
            if (this.triopc.IsOpen(TrioController.PORT_ID) && this.triopc.GetVariable(property, out dReadVar))
            {
                return dReadVar;
            }
            throw new Exception("Invalid property");
        }

        /// <summary>
        /// Move a specified actuator to a new coordinate
        /// </summary>
        /// <param name="nAxis">Number (0-7) of port where axis is connected to trio controller</param>
        /// <param name="pos">New position of axis</param>
        /// <returns>True if command was sent successfully</returns>
        public bool MoveAbs(short nAxis, double pos)
        {
            double movesBuffered = 0;
            this.triopc.GetAxisVariable("MOVE_COUNT", nAxis, out movesBuffered);
            // If moves are buffered, cancel them and wait for them to end
            if (movesBuffered > 0.01)
            {
                Console.WriteLine("buffered moves?");
                this.triopc.Cancel(0, nAxis); // Cancel current move
                this.triopc.Cancel(1, nAxis); // Cancel buffered move
                this.WaitForEndOfMove(nAxis); // Wait for axis to stop
            }
            return this.triopc.MoveAbs(1, pos, nAxis); // Send new move command
        }

        /// <summary>
        /// Move a specified distance from current actuator position
        /// </summary>
        /// <param name="nAxis">Number (0-7) of port where axis is connected to trio controller</param>
        /// <param name="pos">Distance to move</param>
        /// <returns>True if command was sent successfully</returns>
        public bool MoveRel(short nAxis, double pos)
        {
            return this.triopc.MoveRel(1, pos, nAxis);
        }

        /// <summary>
        /// Begin moving an actuator continuously
        /// </summary>
        /// <param name="fwd">Flag to indicate if actuator should move forward or backward</param>
        /// <param name="nAxis">Number (0-7) of port where axis is connected to trio controller</param>
        /// <returns>True if command was sent successfully</returns>
        public bool Jog(bool fwd, short nAxis)
        {
            if (fwd)
            {
                return this.triopc.Forward(nAxis);
            }
            return this.triopc.Reverse(nAxis);
        }

        /// <summary>
        /// Stop continuous actuator motion
        /// </summary>
        /// <param name="nAxis">Number (0-7) of port where axis is connected to trio controller</param>
        /// <returns>True if command was sent successfully</returns>
        public bool JogStop(short nAxis)
        {
            bool status1 = this.triopc.Cancel(0, nAxis); // Cancel current move
            bool status2 = this.triopc.Cancel(1, nAxis); // Cancel buffered moves
            return status1 && status2;
        }

        /// <summary>
        /// Set current actuator position as 0 and clear errors
        /// </summary>
        /// <param name="nAxis">Number (0-7) of port where axis is connected to trio controller</param>
        public void Zero(short nAxis)
        {
            this.triopc.Datum(0, nAxis);
        }

        /// <summary>
        /// Stop all moving actuators
        /// </summary>
        /// <returns>True if command was sent successfully</returns>
        public bool Stop()
        {
            return this.triopc.RapidStop();
        }

        /// <summary>
        /// Blocking function that completes once current actuator motion completes
        /// </summary>
        /// <param name="nAxis">Number (0-7) of port where axis is connected to trio controller</param>
        public void WaitForEndOfMove(short nAxis)
        {
            double distRemaining = 0;
            bool bWaiting;

            this.triopc.GetAxisVariable("REMAIN", nAxis, out distRemaining);
            bWaiting = Math.Abs(distRemaining) > 0.001;
            while (bWaiting)
            {
                Console.WriteLine("waiting");
                Thread.Sleep(100);
                this.triopc.GetAxisVariable("REMAIN", nAxis, out distRemaining);
                bWaiting = Math.Abs(distRemaining) > 0.001;
            }
        }
    }
}
