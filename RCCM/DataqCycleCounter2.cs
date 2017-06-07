using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dataq.Misc;
using Dataq.Devices;
using Dataq.Devices.DI1100;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace RCCM
{
    /// <summary>
    /// Cycle counter using analog pressure input to get pressure and cycle
    /// </summary>
    public class DataqCycleCounter2 : ICycleCounter
    {
        /// <summary>
        /// A boolean indicating whether or not the counter is active
        /// </summary>
        public bool Active { get; protected set; }
        /// <summary>
        /// Current cycle number
        /// </summary>
        public int Cycle { get; set; }
        /// <summary>
        /// Accumulator for ticks elapsed in test
        /// </summary>
        protected int elapsed;
        /// <summary>
        /// Environment tick count when cycle counting started
        /// </summary>
        protected int startTick;
        /// <summary>
        /// Handle to DATAQ DI-1100 data acquisition device
        /// </summary>
        protected Dataq.Devices.DI1100.Device di_1100;
        /// <summary>
        /// Conversion from volts to pressure. Represented as rows of (voltage, pressure) pairs
        /// that are linearly interpolated to get pressure from a given voltage reading
        /// </summary>
        protected double[,] calibration;
        /// <summary>
        /// Stores last pressure reading
        /// </summary>
        protected double pressure;
        /// <summary>
        /// Channel for pressure reading
        /// </summary>
        protected AnalogVoltageIn pressureChannel;
        /// <summary>
        /// Background task for reading data
        /// </summary>
        protected Task readDataTask;
        /// <summary>
        /// Token for signalling cancellation to background task
        /// </summary>
        protected CancellationTokenSource cancelRead;
        /// <summary>
        /// Threshold voltage that must be crossed to interpret as a cycle signal
        /// </summary>
        public static double THRESHOLD = (double)Program.Settings.json["cycle counter"]["threshold"];
        /// <summary>
        /// Input channel for pressure reading - a number from 1 to 4
        /// </summary>
        public static int IN_PRESSURE = (int)Program.Settings.json["cycle counter"]["pressure input"];

        /// <summary>
        /// Create dataq device and counter. Note: initialization is asynchronous
        /// </summary>
        public DataqCycleCounter2()
        {
            // Load calibration from setttings
            int i = 0;
            int calibrationRows = Program.Settings.json["cycle counter"]["calibration"].Count();
            this.calibration = new double[calibrationRows, 2];
            foreach (JArray row in Program.Settings.json["cycle counter"]["calibration"].Children())
            {
                this.calibration[i, 0] = (double)row[0];
                this.calibration[i, 1] = (double)row[1];
                i++;
            }

            // Initialize timing variables
            this.elapsed = 0;
            this.startTick = Environment.TickCount;

            this.cancelRead = new CancellationTokenSource();
            this.initialize();
        }

        /// <summary>
        /// Asynchronous function that initializes DATAQ device and starts data acquisition
        /// </summary>
        protected async void initialize()
        {
            // Get device, preferably by serial number
            IDevice device;
            try
            {
                device = await Discovery.BySerial((string)Program.Settings.json["cycle counter"]["serial"]);
            }
            catch (Exception ex)
            {
                IReadOnlyList<IDevice> devices = await Discovery.AllDevices();
                if (devices.Count > 0)
                {
                    device = devices[0];
                }
                else
                {
                    device = null;
                }
            }

            if (device == null)
            {
                MessageBox.Show("DATAQ device not found.");
                return;
            }
            this.di_1100 = (Dataq.Devices.DI1100.Device)device;

            // Re-configure device
            await this.di_1100.AcquisitionStopAsync();
            this.di_1100.Channels.Clear();

            this.pressureChannel = (Dataq.Devices.DI1100.AnalogVoltageIn)this.di_1100.ChannelFactory(DataqCycleCounter2.IN_PRESSURE, typeof(AnalogVoltageIn));
            this.di_1100.SampleRate = 2 * this.di_1100.SupportedSampleRateRange.Minimum;

            // Start device
            await this.di_1100.InitializeAsync();
            await this.di_1100.AcquisitionStartAsync();

            // Start background task for reading data
            this.readDataTask = new Task(new Action(this.readDataLoop));
            this.readDataTask.Start();
        }

        /// <summary>
        /// Activates the counter without resetting the cycle number
        /// </summary>
        public void Start()
        {
            this.Active = true;
            this.startTick = Environment.TickCount;
        }

        /// <summary>
        /// Activates the counter without and sets the current cycle to the specified value
        /// </summary>
        /// <param name="cycle">Cycle number to start at</param>
        public void Start(int cycle)
        {
            this.Cycle = cycle;
            this.Active = true;
            this.startTick = Environment.TickCount;
        }

        /// <summary>
        /// Stops the cycle counting. Cycle counting can be resumed
        /// </summary>
        public void Stop()
        {
            this.Active = false;
            this.elapsed += Environment.TickCount - this.startTick;
        }

        /// <summary>
        /// Get most recent analog pressure reading
        /// </summary>
        /// <returns>Last pressure reading recorded</returns>
        public double GetPressure()
        {
            return pressure;
        }

        /// <summary>
        /// Get time elasped in test in milliseconds
        /// </summary>
        /// <returns>Cumulative milliseconds elapsed, accounting for pauses</returns>
        public int GetElapsed()
        {
            if (this.Active)
            {
                return this.elapsed + Environment.TickCount - this.startTick;
            }
            else
            {
                return this.elapsed;
            }
        }

        /// <summary>
        /// Worker function for background task that reads data from DATAQ device.
        /// </summary>
        protected async void readDataLoop()
        {
            while (this.di_1100.IsAcquiring)
            {
                try
                {
                    // Get all data
                    await this.di_1100.ReadDataAsync(cancelRead.Token);

                    // Average pressure readings received since last iteration
                    double lastCyclePressure = this.pressure;
                    this.pressure = DataqCycleCounter2.PwlInterp(this.calibration,
                        this.pressureChannel.DataIn.Average());
                    // Only increment cycle counters when cycle counting is active
                    if (this.Active && this.pressure >= DataqCycleCounter2.THRESHOLD &&
                                       lastCyclePressure < DataqCycleCounter2.THRESHOLD)
                    {
                        this.Cycle++;
                    }
                    // Clear data so it is not read twice
                    this.pressureChannel.DataIn.Clear();
                }
                // Handle cancellation by Terminate function
                catch (OperationCanceledException ex)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Stop data acquisition and release DATAQ device
        /// </summary>
        public async Task Terminate()
        {
            if (this.di_1100 != null)
            {
                this.cancelRead.Cancel();
                await this.readDataTask;
            }
        }

        /// <summary>
        /// Helper function for piecewise linear interpolation
        /// </summary>
        /// <param name="data">2D array of (x, y) value pairs</param>
        /// <param name="val">X value to be interpolated</param>
        /// <returns>Interpolated Y value corresponding to input X</returns>
        public static double PwlInterp(double[,] data, double val)
        {
            int i = 0;
            int n = data.GetLength(0);

            while (i < n && val > data[i, 0])
            {
                i++;
            }
            if (i == n)
            {
                return data[n - 1, 1];
            }
            if (i == 0)
            {
                return data[0, 1];
            }

            double m = (data[i, 1] - data[i - 1, 1])
                       / (data[i, 0] - data[i - 1, 0]);
            double x = val - data[i - 1, 0];
            double b = data[i - 1, 1];
            return m * x + b;
        }
    }
}
