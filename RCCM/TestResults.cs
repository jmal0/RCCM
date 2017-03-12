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

        public TestResults(RCCMSystem rccm, ObservableCollection<MeasurementSequence> cracks, Chart crackChart, Chart cycleChart, TextBox cycleIndicator, TextBox pressureIndicator, ListBox crackSelection)
        {
            // Initialize Paris law chart
            this.cracks = cracks;
            this.cracks.CollectionChanged += cracksChangedHandler;

            this.crackChart = crackChart;
            Title crackChartTitle = new Title("Crack Length vs Cycle Number");
            this.crackChart.Titles.Add(crackChartTitle);
            this.crackChart.ChartAreas[0].AxisX.Title = "Cycle";
            this.crackChart.ChartAreas[0].AxisY.Title = "Crack Length";
            // Initialize pressure vs time chart
            this.cycleChart = cycleChart;
            Title cycleChartTitle = new Title("Pressure Reading history");
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
            this.updateControlsTimer.Interval = (int)Program.Settings.json["repaint period"];
            this.updateControlsTimer.Tick += new EventHandler(updateTestControls);
            this.updateControlsTimer.Start();
            // Create list for holding past pressure readings
            this.savedReadings = (int)Program.Settings.json["pressure readings saved"];
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
                    (crack as INotifyCollectionChanged).CollectionChanged += delegate (object sender2, NotifyCollectionChangedEventArgs e2) { this.PlotCracks(); };
                }
            }
        }

        private void updateTestControls(object sender, EventArgs e)
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

        public void PlotCracks()
        {
            // Refresh crack selection list (names may have changed)
            this.crackSelection.DisplayMember = "";
            this.crackSelection.DisplayMember = "Name";
            // Plot cracks
            this.crackChart.Series.Clear();
            foreach (MeasurementSequence crack in this.crackSelection.SelectedItems)
            {
                Series crackSeries = new Series(crack.Name);
                crackSeries.ChartType = SeriesChartType.Line;

                double[] cycles = new double[crack.CountPoints];
                double[] lengths = new double[crack.CountPoints];
                for (int i = 0; i < crack.CountPoints; i++)
                {
                    cycles[i] = crack.GetPoint(i).Cycle;
                    lengths[i] = crack.GetPoint(i).CrackLength;
                }
                crackSeries.Points.DataBindXY(cycles, lengths);

                this.crackChart.Series.Add(crackSeries);
            }
        }
    }
}
