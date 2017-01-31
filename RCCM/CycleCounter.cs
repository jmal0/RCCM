using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RCCM
{
    /// <summary>
    /// Tracks the cycle number of the test. Can be paused and restarted. Currently a simplified implementation
    /// </summary>
    public class CycleCounter
    {
        protected Timer countTimer;

        /// <summary>
        /// A boolean indicating whether or not the counter is active
        /// </summary>
        public bool Active { get; private set; }
        
        /// <summary>
        /// Current cycle number
        /// </summary>
        public int Cycle { get; set; }

        public CycleCounter(int period)
        {
            // Create timer to call countLoop periodically
            this.countTimer = new Timer();
            this.countTimer.Enabled = true;
            this.countTimer.Interval = period;
            this.countTimer.Tick += new EventHandler(countLoop);

            // Intialize properties to indicate inactive status
            this.Active = false;
            this.Cycle = 0;
        }

        public void setPeriod(int period)
        {
            this.countTimer.Interval = period;
        }

        /// <summary>
        /// Activates the counter without resetting the cycle number
        /// </summary>
        public void start()
        {
            this.countTimer.Start();
            this.Active = true;
        }

        /// <summary>
        /// Activates the counter without and sets the current cycle to the specified value
        /// </summary>
        /// <param name="cycle">Cycle number to start at</param>
        public void start(int cycle)
        {
            this.Cycle = cycle;
            this.countTimer.Start();
            this.Active = true;
        }
        
        /// <summary>
        /// Stops the cycle counting. Cycle counting can be resumed
        /// </summary>
        public void stop()
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
        }
    }
}
