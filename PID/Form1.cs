using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PID
{
    public partial class Form1 : Form
    {
        int P, I, D, PID, Kp, Ki, Kd;
        int Error, Error_old, Et, EEo, time, X_need, X_real = 5, step, KpP, KiI, KdD, step_old, X_real_old, i, Delta;
        int shag;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            X_need = int.Parse(textBox1.Text);
            time = int.Parse(textBox3.Text);
            Kp = int.Parse(textBox2.Text);
            Ki = int.Parse(textBox4.Text);
            Kd = int.Parse(textBox5.Text);
            Delta = int.Parse(textBox6.Text);
            shag = int.Parse(textBox7.Text);

            if (1 != 0)
            {
                Error = X_need - X_real;

                Et = Error * time;

                EEo = Error - Error_old;

                P = Error;
                I = I + Et;
                D = EEo / time;

                KpP = Kp * P;
                KiI = Ki * I;
                KdD = Kd * D;

                PID = KpP + KiI + KdD;

                Error_old = Error;

                X_real = (int)(Delta * Math.Sin((i - PID) / 40));

                label1.Text = PID.ToString();
                label2.Text = X_real.ToString();

                label10.Text = KpP.ToString();
                label11.Text = KiI.ToString();
                label12.Text = KdD.ToString();

                step = step + shag;

                Graphics graph = pictureBox1.CreateGraphics();

                Pen pen_black = new Pen(Brushes.Black, 2);
                Pen pen_red = new Pen(Brushes.Red, 1);

                graph.DrawEllipse(pen_black, (float)step, (float)(536 - X_real * shag*5), 1, 1);
                graph.DrawLine(pen_red, 0, (float)(536 - X_need * shag*5), 750, (float)(536 - X_need * shag*5));

                if (step >= 750)
                {
                    pictureBox1.Image = null;
                    step = 0;
                }

                graph.DrawLine(pen_black, (float)step_old, (float)(536 - X_real_old * shag*5), (float)step, (float)(536 - X_real * shag*5));

                step_old = step;
                X_real_old = X_real;
            }
            else
            {
                timer1.Stop();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            X_real = 0;
            Error = 0;
            Error_old = 0;
            PID = 0;
        }

    }
}
