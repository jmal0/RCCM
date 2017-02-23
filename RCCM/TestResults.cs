using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace RCCM
{
    public class TestResults
    {
        protected Chart crackChart;
        protected Chart cycleChart;
        protected TextBox cycleIndicator;
        protected TextBox pressureIndicator;
        protected CycleCounter counter;
        protected Timer updateControlsTimer;
        protected int savedReadings;

        public TestResults(RCCMSystem rccm, Settings settings, Chart cracks, Chart cycles, TextBox cycleIndicator, TextBox pressureIndicator)
        {
            // Initialize Paris law chart
            this.crackChart = cracks;
            Title crackChartTitle = new Title("Crack Length vs Cycle Number");
            this.crackChart.Titles.Add(crackChartTitle);
            this.crackChart.ChartAreas[0].AxisX.Title = "da/dN";
            this.crackChart.ChartAreas[0].AxisY.Title = "Crack Length (mm)";
            // Initialize pressure vs time chart
            this.cycleChart = cycles;
            Title cycleChartTitle = new Title("Crack Length vs Cycle Number");
            this.cycleChart.Titles.Add(cycleChartTitle);
            this.cycleChart.ChartAreas[0].AxisX.Title = "Time (s)";
            this.cycleChart.ChartAreas[0].AxisY.Title = "Pressure";
            this.cycleChart.Series[0].Name = "Cycle";
            this.cycleChart.Series[0].ChartType = SeriesChartType.Line;
            // Save cycle/pressure indicators for displaying test status
            this.cycleIndicator = cycleIndicator;
            this.pressureIndicator = pressureIndicator;
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

        public double[] getCycles(MeasurementSequence crack)
        {
            double[] cycles = new double[crack.CountPoints];
            for (int i = 1; i < crack.CountPoints; i++)
            {
                cycles[i] = crack.getPoint(i).Cycle;
            }
            return cycles;
        }

        public double[] calculateCrackLength(MeasurementSequence crack)
        {
            double[] lengths = new double[crack.CountPoints];
            if (crack.CountPoints == 0)
            {
                return lengths;
            }

            double originX = crack.getPoint(0).X;
            double originY = crack.getPoint(0).Y;
            for (int i = 0; i<crack.CountPoints;i++)
            {
                Measurement point = crack.getPoint(i);
                lengths[i] = Math.Sqrt(Math.Pow((point.X - originX), 2) + Math.Pow((point.Y - originY), 2));
            }
            return lengths; 
        }

        public void updateTestControls(object sender, EventArgs e)
        {
            // Update cycle & pressure textbox indicators
            this.cycleIndicator.Text = this.counter.Cycle.ToString();
            this.pressureIndicator.Text = string.Format("{0:0.00}", this.counter.GetPressure());
            // Update pressure history chart
            if (this.counter.Active)
            {
                this.cycleChart.Series[0].Points.AddXY(this.counter.GetElapsed() / 1000.0, this.counter.GetPressure());
                if (this.cycleChart.Series[0].Points.Count > this.savedReadings)
                {
                    this.cycleChart.Series[0].Points.RemoveAt(0);
                    this.cycleChart.ChartAreas[0].AxisX.Minimum = this.cycleChart.Series[0].Points[0].XValue;
                    this.cycleChart.ChartAreas[0].AxisX.Maximum = this.cycleChart.Series[0].Points[this.savedReadings - 1].XValue;
                }
            }
        }

        public void plotCracks(List<MeasurementSequence> cracks)
        {
            this.crackChart.Series.Clear();
            foreach (var crack in cracks)
            {
                Series crackSeries = new Series(crack.Name);
                crackSeries.ChartType = SeriesChartType.Line;

                double[] cycles = this.getCycles(crack);
                double[] lengths = this.calculateCrackLength(crack);
                crackSeries.Points.DataBindXY(cycles, lengths);

                this.crackChart.Series.Add(crackSeries);
            }
        }
    }
}
