using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Project_Akhir_COMVIS
{
    public partial class Shape_Detection : Form
    {
        Image<Bgr, byte> ori, edit;
        Image<Gray, byte> gray;

        public Shape_Detection()
        {
            InitializeComponent();
            button3.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                ori = new Image<Bgr, byte>(openFileDialog1.FileName);
                edit = new Image<Bgr, byte>(ori.Size.Width, ori.Size.Height);
                gray = new Image<Gray, byte>(ori.Size.Width, ori.Size.Height);

                pictureBox1.Image = ori.ToBitmap();

                CvInvoke.cvCvtColor(ori, gray, COLOR_CONVERSION.CV_BGR2GRAY);
                CvInvoke.cvCanny(gray, gray, 150, 60, 3);

                button3.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Please upload image first");
            }
            else if (checkBox1.Checked == false && checkBox2.Checked == false && checkBox3.Checked == false)
            {
                MessageBox.Show("Choose Shape Detection Mode");
            }
            else
            {
                if (checkBox1.Checked == true)
                {
                    edit = ori.Clone();
                    LineSegment2D[] lines = gray.HoughLinesBinary(1, Math.PI / 45, 1, 25, 8)[0];

                    foreach (var line in lines)
                    {
                        edit.Draw(line, new Bgr(Color.Green), 5);
                    }

                    pictureBox1.Image = edit.ToBitmap();
                }
                else if (checkBox2.Checked == true)
                {
                    edit = ori.Clone();
                    Contour<Point> contours = gray.FindContours();

                    while (contours != null)
                    {
                        Contour<Point> contour = contours.ApproxPoly(contours.Perimeter * 0.01);

                        if (contour.Total == 4)
                        {
                            Point[] points = contour.ToArray();
                            LineSegment2D[] lines = PointCollection.PolyLine(points, true);

                            for (var i = 0; i < lines.Length; i++)
                            {
                                var angle = lines[i].GetExteriorAngleDegree(lines[(i + 1) % lines.Length]);

                                if (angle >= 80 && angle <= 100)
                                {
                                    edit.Draw(contour.GetMinAreaRect(), new Bgr(Color.Teal), 3);
                                }
                            }
                        }

                        contours = contours.HNext;
                    }

                    pictureBox1.Image = edit.ToBitmap();
                }
                else if (checkBox3.Checked == true)
                {
                    edit = ori.Clone();

                    CircleF[] circles = gray.HoughCircles(new Gray(127), new Gray(127), 5, 400, 50, 0)[0];

                    foreach (var circle in circles)
                    {
                        edit.Draw(circle, new Bgr(Color.Turquoise), 3);
                    }

                    pictureBox1.Image = edit.ToBitmap();
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox2.Checked = false;
                checkBox3.Checked = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                checkBox1.Checked = false;
                checkBox3.Checked = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Home Home_load = new Home();
            Home_load.Show();
        }
    }
}
