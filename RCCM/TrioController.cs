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
        public static string IP = "192.168.0.250";
        public static short NUMBER_AXES = 1;
        public static short ATYPE = 43;

        public static string[] AX_PROPERTIES = { "ATYPE", "P_GAIN", "I_GAIN", "D_GAIN", "OV_GAIN", "VFF_GAIN", "UNITS", "SPEED", "ACCEL", "DECEL", "CREEP", "JOGSPEED", "FE_LIMIT", "DAC", "SERVO", "REP_DIST", "FWD_IN", "REV_IN", "DATUM_IN", "FS_LIMIT", "RS_LIMIT", "MTYPE", "NTYPE", "MPOS", "DPOS", "FE", "AXISSTATUS" };

        private AxTrioPC triopc;
        /// <summary>
        /// Indicates whether or not controller is connected and port is opened
        /// </summary>
        public bool Open { get; private set; }

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
        
        public bool isMoving(short nAxis)
        {
            double distRemaining;
            this.triopc.GetAxisVariable("REMAIN", nAxis, out distRemaining);
            return Math.Abs(distRemaining) > 0.001;
        }

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

        public double GetAxisProperty(string property, short nAxis)
        {
            double dReadVar;
            if (this.triopc.IsOpen(TrioController.PORT_ID) && this.triopc.GetAxisVariable(property, nAxis, out dReadVar))
            {
                return dReadVar;
            }
            throw new Exception(string.Format("Invalid property: {0}", property));
        }

        public bool SetAxisProperty(string property, double value, short nAxis)
        {
            if (this.triopc.IsOpen(TrioController.PORT_ID))
            {
                return this.triopc.SetAxisVariable(property, nAxis, value);
            }
            throw new Exception(string.Format("Invalid property: {0}", property));
        }

        public double GetProperty(string property)
        {
            double dReadVar;
            if (this.triopc.IsOpen(TrioController.PORT_ID) && this.triopc.GetVariable(property, out dReadVar))
            {
                return dReadVar;
            }
            throw new Exception("Invalid property");
        }

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

        public bool MoveRel(short nAxis, double pos)
        {
            return this.triopc.MoveRel(1, pos, nAxis);
        }

        // Gotta look into this
        public bool Jog(bool fwd, short nAxis)
        {
            if (fwd)
            {
                return this.triopc.Forward(nAxis);
            }
            return this.triopc.Reverse(nAxis);
        }

        public bool JogStop(short nAxis)
        {
            bool status1 = this.triopc.Cancel(0, nAxis); // Cancel current move
            bool status2 = this.triopc.Cancel(1, nAxis); // Cancel buffered moves
            return status1 && status2;
        }

        public bool Stop()
        {
            return this.triopc.RapidStop();
        }

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
