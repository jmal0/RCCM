using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RCCM
{
    /// <summary>
    /// Tracks the cycle number of the test. Can be paused and restarted. This is a virtual implementation of ICycleCounter
    /// </summary>
    public class CycleCounter : ICycleCounter
    {
        /// <summary>
        /// Timer for automatically incrementing cycle number
        /// </summary>
        protected Timer countTimer;
        /// <summary>
        /// A boolean indicating whether or not the counter is active
        /// </summary>
        public bool Active { get; private set; }        
        /// <summary>
        /// Current cycle number
        /// </summary>
        public int Cycle { get; set; }
        /// <summary>
        /// Fixed cycle period in milliseconds
        /// </summary>
        protected int period;
        /// <summary>
        /// Cycle period in milliseconds
        /// </summary>
        public int Period
        {
            get { return this.period; }
            set { this.period = value; this.countTimer.Interval = value; }
        }
        /// <summary>
        /// Environment tick count of last cycle increment
        /// </summary>
        protected int lastTick;
        /// <summary>
        /// Amplitude of pressure signal 
        /// </summary>
        public double Amplitude { get; set; }

        /// <summary>
        /// Create a cycle counter with a fixed period
        /// </summary>
        /// <param name="period">Cycle period in milliseconds</param>
        public CycleCounter(int period)
        {
            // Create timer to call countLoop periodically
            this.countTimer = new Timer();
            this.countTimer.Enabled = false;
            this.period = period;
            this.countTimer.Interval = this.period;
            this.countTimer.Tick += new EventHandler(countLoop);

            // Intialize properties to indicate inactive status
            this.Active = false;
            this.Cycle = 0;
            this.Amplitude = 1;
            this.lastTick = Environment.TickCount;
        }

        /// <summary>
        /// Activates the counter without resetting the cycle number
        /// </summary>
        public void Start()
        {
            this.countTimer.Start();
            this.Active = true;
            this.lastTick = Environment.TickCount;
        }

        /// <summary>
        /// Activates the counter without and sets the current cycle to the specified value
        /// </summary>
        /// <param name="cycle">Cycle number to start at</param>
        public void Start(int cycle)
        {
            this.Cycle = cycle;
            this.countTimer.Start();
            this.Active = true;
            this.lastTick = Environment.TickCount;
        }
        
        /// <summary>
        /// Stops the cycle counting. Cycle counting can be resumed
        /// </summary>
        public void Stop()
        {
            this.countTimer.Stop();
            this.Active = false;
        }
        
        /// <summary>
        /// Function called periodically to update the cycle number. For now this just increments the cycle periodically
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void countLoop(object sender, EventArgs e)
        {
            this.Cycle += 1;
            this.lastTick = Environment.TickCount;
        }

        /// <summary>
        /// Get current pressure reading (simulated with sine curve)
        /// <returns>current pressure reading</returns>
        /// </summary>
        public double GetPressure()
        {
            return this.Amplitude * Math.Sin(2 * Math.PI * ((double)Environment.TickCount / this.Period));
        }

        /// <summary>
        /// Elapsed time in test from current cycle and elapsed time since last tick
        /// </summary>
        public int GetElapsed()
        {
            if (this.Active)
            {
                return this.Cycle * this.Period + Environment.TickCount - this.lastTick;
            }
            else
            {
                return this.Cycle * this.Period;
            }
        }

        /// <summary>
        /// Implementing a required function that serves no purpose for virtual implementation
        /// </summary>
        /// <returns>Nothing</returns>
        public async Task Terminate()
        {
            // Does nothing
        }
    }
}
