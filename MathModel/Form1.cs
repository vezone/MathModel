using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using MathModel.src.Models;

namespace MathModel
{
    public partial class Form1 : Form
    {
        double[] x;
        double[] y;
        double[] a1;
        double[] b1;
        double[] a2;
        double[] b2;
        double[] a3;
        double[] b3;
        BoundaryProblem bp;

        public Form1()
        {
            InitializeComponent();

            bp = new BoundaryProblem();
            bp.Model(bp.function);
            
            ChartInit();

            int index = 0;
            a1 = new double[3001];
            b1 = new double[3001];
            b2 = new double[3001];
            for (double a = 0.0; a < 3.0; a+=0.001, index++)
            {
                a1[index] = a;
                b1[index] = bp.function(a) - 0.5;
                b2[index] = bp.kfunction(a);
            }
            chart1.Series["Series2"].Points.DataBindXY(a1, b1);
        }

        private void ChartInit()
        {
            chart1.Visible = true;

            chart1.ChartAreas[0].Name = "ChartArea0";
            chart1.ChartAreas[0].Visible = true;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

            chart1.ChartAreas[0].AxisX.Minimum = bp.MinX;
            chart1.ChartAreas[0].AxisX.Maximum = bp.MaxX;
            chart1.ChartAreas[0].AxisX.Interval = 0.5;

            chart1.ChartAreas[0].AxisY.Minimum = (int)(bp.MinY - 1);//-0.5;
            chart1.ChartAreas[0].AxisY.Maximum = (int)(bp.MaxY + 1);
            chart1.ChartAreas[0].AxisY.Interval = 0.5;
            
            chart1.Series.Add(new Series("Series2"));

            chart1.Series["Series1"].ChartType = SeriesChartType.Line;
            chart1.Series["Series2"].ChartType = SeriesChartType.Line;
            chart1.Series["Series1"].Color = System.Drawing.Color.Blue;
            chart1.Series["Series2"].Color = System.Drawing.Color.Indigo;
            chart1.Series["Series1"].ChartArea = "ChartArea0";
            chart1.Series["Series2"].ChartArea = "ChartArea0";
            chart1.Series["Series1"].Points.DataBindXY(bp.X, bp.Y);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (chart1.ChartAreas[0].AxisX.MajorGrid.Enabled)
            {
                chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            }
            else
            {
                chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
                chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
            }
        }

        //F5
        private void button2_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            if (text.Length > 0)
            {
                Int32 int32 = new Int32();
                Int32.TryParse(text, out int32);
                if (int32 > 0)
                {
                    bp = new BoundaryProblem(int32);
                    bp.Model(bp.function);

                    chart1.ChartAreas[0].AxisX.Minimum = bp.MinX;
                    chart1.ChartAreas[0].AxisX.Maximum = bp.MaxX;
                    chart1.ChartAreas[0].AxisX.Interval = 0.5;
                    
                    chart1.ChartAreas[0].AxisY.Minimum = (int)(bp.MinY - 1);
                    chart1.ChartAreas[0].AxisY.Maximum = (int)(bp.MaxY + 1);
                    chart1.ChartAreas[0].AxisY.Interval = 0.5;
                
                    chart1.Series["Series1"].Points.DataBindXY(bp.X, bp.Y);
                    chart1.Series["Series2"].Points.DataBindXY(a1, b1);
                    chart1.Update();
                }
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            //chart click event
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
