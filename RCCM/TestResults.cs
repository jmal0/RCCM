using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace RCCM
{
    public class TestResults
    {
        protected ObservableCollection<MeasurementSequence> cracks;
        protected Chart crackChart;
        protected Chart cycleChart;
        protected TextBox cycleIndicator;
        protected TextBox pressureIndicator;
        protected ListBox crackSelection;
        protected CycleCounter counter;
        protected Timer updateControlsTimer;
        protected int savedReadings;

        public TestResults(RCCMSystem rccm, Settings settings, ObservableCollection<MeasurementSequence> cracks, Chart crackChart, Chart cycleChart, TextBox cycleIndicator, TextBox pressureIndicator, ListBox crackSelection)
        {
            // Initialize Paris law chart
            this.cracks = cracks;
            this.cracks.CollectionChanged += cracksChangedHandler;

            this.crackChart = crackChart;
            Title crackChartTitle = new Title("Crack Length vs Cycle Number");
            this.crackChart.Titles.Add(crackChartTitle);
            this.crackChart.ChartAreas[0].AxisX.Title = "Cycle";
            this.crackChart.ChartAreas[0].AxisY.Title = "da/dN";
            // Initialize pressure vs time chart
            this.cycleChart = cycleChart;
            Title cycleChartTitle = new Title("Crack Length vs Cycle Number");
            this.cycleChart.Titles.Add(cycleChartTitle);
            this.cycleChart.ChartAreas[0].AxisX.Title = "Time (s)";
            this.cycleChart.ChartAreas[0].AxisY.Title = "Pressure";
            this.cycleChart.Series[0].Name = "Cycle";
            this.cycleChart.Series[0].ChartType = SeriesChartType.Line;
            // Save cycle/pressure indicators for displaying test status & crack selection list
            this.cycleIndicator = cycleIndicator;
            this.pressureIndicator = pressureIndicator;
            this.crackSelection = crackSelection;
            // Save counter
            this.counter = rccm.Counter;
            // Create timer for updating test controls
            this.updateControlsTimer = new Timer();
            this.updateControlsTimer.Enabled = false;
            this.updateControlsTimer.Interval = (int) settings.json["repaint period"];
            this.updateControlsTimer.Tick += new EventHandler(updateTestControls);
            this.updateControlsTimer.Start();
            // Create list for holding past pressure readings
            this.savedReadings = (int) settings.json["pressure readings saved"];
        }

        private void cracksChangedHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (MeasurementSequence crack in e.OldItems)
                {
                    this.crackSelection.Items.Remove(crack);
                }
            }
            if (e.NewItems != null)
            {
                foreach (MeasurementSequence crack in e.NewItems)
                {
                    int ind = this.crackSelection.Items.Add(crack);
                    this.crackSelection.SetSelected(ind, true);
                }
            }
        }

        public Tuple<double[], double[]> calculateParisLaw(MeasurementSequence crack)
        {
            double[] cycles = new double[crack.CountPoints];
            double[] slopes = new double[crack.CountPoints];
            if (crack.CountPoints == 0)
            {
                return new Tuple<double[], double[]>(cycles, slopes);
            }

            double originX = crack.getPoint(0).X;
            double originY = crack.getPoint(0).Y;
            double lastA = 0;
            double lastN = 0;
            for (int i = 0; i<crack.CountPoints;i++)
            {
                Measurement point = crack.getPoint(i);
                // Calculate length of crack from origin
                double a = Math.Sqrt(Math.Pow((point.X - originX), 2) + Math.Pow((point.Y - originY), 2));
                cycles[i] = point.Cycle;
                // Protect against divide by 0
                if (cycles[i] - lastN == 0)
                {
                    slopes[i] = (a - lastA); // Assume 1 cycle passed
                }
                else
                {
                    slopes[i] = (a - lastA) / (cycles[i] - lastN);
                }                
                // Save crack length and cycle for calculation of next slope
                lastA = a;
                lastN = cycles[i];
            }
            return new Tuple<double[], double[]>(cycles, slopes); 
        }

        public void updateTestControls(object sender, EventArgs e)
        {
            // Update cycle indicator
            this.cycleIndicator.Text = this.counter.Cycle.ToString();
            // Update pressure history chart
            if (this.counter.Active)
            {
                // Update pressure textbox indicator
                this.pressureIndicator.Text = string.Format("{0:0.00}", this.counter.GetPressure());
                this.cycleChart.Series[0].Points.AddXY(this.counter.GetElapsed() / 1000.0, this.counter.GetPressure());
                if (this.cycleChart.Series[0].Points.Count > this.savedReadings)
                {
                    this.cycleChart.Series[0].Points.RemoveAt(0);
                    this.cycleChart.ChartAreas[0].AxisX.Minimum = this.cycleChart.Series[0].Points[0].XValue;
                    this.cycleChart.ChartAreas[0].AxisX.Maximum = this.cycleChart.Series[0].Points[this.savedReadings - 1].XValue;
                }
            }
        }

        public void plotCracks()
        {
            this.crackChart.Series.Clear();
            foreach (MeasurementSequence crack in this.crackSelection.SelectedItems)
            {
                Series crackSeries = new Series(crack.Name);
                crackSeries.ChartType = SeriesChartType.Line;

                Tuple<double[], double[]> data = calculateParisLaw(crack);
                crackSeries.Points.DataBindXY(data.Item1, data.Item2);

                this.crackChart.Series.Add(crackSeries);
            }
        }
    }
}
