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

            t = 0;
            x = 0;
            y = y0;
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
                x = v0 * Math.Cos(a * Math.PI / 180) * t;
                y = y0 + v0 * Math.Sin(a * Math.PI / 180) * t - g * t * t / 2;
                chart1.Series[0].Points.AddXY(x, y);
                showTimeSpent.Text = Math.Round(t, 5).ToString();
                if (y <= 0)
                {
                    timer1.Stop();
                    finished = true;
                }
            }

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
