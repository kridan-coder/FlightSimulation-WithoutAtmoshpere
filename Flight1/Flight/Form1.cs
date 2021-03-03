using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flight
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool stopped = false;
        bool finished = false;

        const double dt = 0.01;
        const double g = 9.81;

        double sinA;
        double cosA;

        double tMax;

        double a;
        double v0;
        double y0;

        double t;
        double x;
        double y;
        private void btStart_Click(object sender, EventArgs e)
        {

            finished = false;

            a = (double)edAngle.Value;
            v0 = (double)edSpeed.Value;
            y0 = (double)edHeight.Value;

            sinA = Math.Sin(a * Math.PI / 180);
            cosA = Math.Cos(a * Math.PI / 180);

            t = 0;
            x = 0;
            y = y0;

            tMax = ((v0*sinA + Math.Sqrt(v0*v0 * sinA*sinA + 2*g*y0))/g);
                //2 * (((2*v0*sinA) + (Math.Sqrt(4*(v0*v0 * sinA*sinA + 16*y0*y0)))) / g);
                //(((v0 * sinA * t) + Math.Sqrt(v0*v0 * sinA*sinA + 2*g*y0)) / g);
                //(v0 * sinA + Math.Sqrt(Math.Pow(v0, 2) * Math.Pow(sinA, 2) + 2 * g * y0) / g);

            chart1.ChartAreas[0].AxisX.Maximum = calculateMaxX(v0);
            chart1.ChartAreas[0].AxisY.Maximum = calculateMaxY(v0, y0);

            chart1.Series[0].Points.Clear();
            chart1.Series[0].Points.AddXY(x, y);
            btPause.Visible = true;

            showTimeSpent.Text = Math.Round(t, 5).ToString();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (!finished)
            {
                t += dt;
                x = v0 * cosA * t;
                y = y0 + v0 * sinA * t - g * t * t / 2;
                chart1.Series[0].Points.AddXY(x, y);
                showTimeSpent.Text = Math.Round(t, 5).ToString();
                if (y <= 0)
                {
                    timer1.Stop();
                    finished = true;
                }
            }

        }

        private double calculateMaxX(double v0)
        {
            double x = v0 * cosA * tMax;
            return x;
        }

        private double calculateMaxY(double v0, double y0)
        {
            double tMaxDiv2 = tMax / 2;
            double y = y0 + v0 * sinA * tMaxDiv2 - g * tMaxDiv2 * tMaxDiv2 / 2;
            return y;
        }

        private void btPause_Click(object sender, EventArgs e)
        {
            if (stopped)
            {
                timer1.Start();
                stopped = false;
                btPause.Text = "Pause";
            }
            else
            {
                timer1.Stop();
                stopped = true;
                btPause.Text = "Resume";
            }
        }
    }
}
