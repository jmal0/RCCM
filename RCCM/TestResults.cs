using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace RCCM
{
    public class TestResults
    {
        Chart crackChart;
        Chart cycleChart;

        public TestResults(Chart cracks, Chart cycles)
        {
            this.crackChart = cracks;
            Title crackChartTitle = new Title("Crack Length vs Cycle Number");
            this.crackChart.Titles.Add(crackChartTitle);
            this.crackChart.ChartAreas[0].AxisX.Title = "Cycle";
            this.crackChart.ChartAreas[0].AxisY.Title = "Crack Length (mm)";
            this.cycleChart = cycles;
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
