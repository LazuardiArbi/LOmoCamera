using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

namespace Project_Akhir_COMVIS
{
    public partial class Pattern_Recognition_and_Classification : Form
    {
        bool detectFace, detectEyes, canny_edge_status, video_play;
        int fps;

        Image<Bgr, byte> ori, copy;
        Image<Gray, byte> gray;

        Capture cap;

        HaarCascade eyesclassifier, faceclassifier, cascade;

        public Pattern_Recognition_and_Classification()
        {
            InitializeComponent();
            fps = 30;
            timer1.Interval = 1000 / fps;
            detectEyes = detectFace = video_play = canny_edge_status = false;

            faceclassifier = new HaarCascade("haarcascade_frontalface_alt.xml");
            eyesclassifier = new HaarCascade("haarcascade_eye.xml");
            cascade = new HaarCascade("haarcascade_frontalface_alt.xml");
            button4.Enabled = false;
            button5.Enabled = false;
            groupBox1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                timer1.Stop();
                ori = new Image<Bgr, byte>(openFileDialog1.FileName);
                copy = new Image<Bgr, byte>(ori.Size.Width, ori.Size.Height);
                gray = new Image<Gray, byte>(ori.Size.Width, ori.Size.Height);

                groupBox1.Enabled = true;
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                button4.Enabled = false;
                button5.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                cap = new Capture(openFileDialog1.FileName);
                timer1.Start();
                video_play = true;
                button4.Enabled = true;
                button5.Enabled = true;
                groupBox1.Enabled = true;
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            video_play = true;
            ori = cap.QueryFrame();

            if (ori != null)
            {
                gray = new Image<Gray, byte>(ori.Width, ori.Height);
                CvInvoke.cvCvtColor(ori, gray, COLOR_CONVERSION.CV_BGR2GRAY);

                if (detectFace)
                {
                    var faces = faceclassifier.Detect(gray);

                    foreach (var face in faces)
                    {
                        ori.Draw(face.rect, new Bgr(Color.Blue), 3);
                    }
                }
                if (detectEyes)
                {
                    var eyes = eyesclassifier.Detect(gray);

                    foreach (var eye in eyes)
                    {
                        ori.Draw(eye.rect, new Bgr(Color.Orange), 3);
                    }
                }
                if (canny_edge_status)
                {
                    var faces_canny = cascade.Detect(gray, 1.1, 2, HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(ori.Width, ori.Height));

                    foreach (var face in faces_canny)
                    {
                        ori.Draw(face.rect, new Bgr(Color.Red), 3);
                    }
                }
                pictureBox1.Image = ori.ToBitmap();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox2.Checked = false;
                checkBox3.Checked = false;

                if (video_play == true)
                {
                    detectFace = true;
                }
                else
                {
                    CvInvoke.cvCvtColor(ori, gray, COLOR_CONVERSION.CV_BGR2GRAY);
                    var faces = faceclassifier.Detect(gray);

                    copy = ori.Clone();

                    foreach (var face in faces)
                    {
                        copy.Draw(face.rect, new Bgr(Color.Blue), 3);
                    }

                    pictureBox1.Image = copy.ToBitmap();
                }
            }
            else
            {
                detectFace = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                checkBox1.Checked = false;
                checkBox3.Checked = false;

                if (video_play == true) detectEyes = true;
                else
                {
                    CvInvoke.cvCvtColor(ori, gray, COLOR_CONVERSION.CV_BGR2GRAY);
                    var eyes = eyesclassifier.Detect(gray);

                    copy = ori.Clone();

                    foreach (var eye in eyes)
                    {
                        copy.Draw(eye.rect, new Bgr(Color.Orange), 3);
                    }

                    pictureBox1.Image = copy.ToBitmap();
                }
            }
            else
            {
                detectEyes = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;

                if (video_play == true) canny_edge_status = true;
                else
                {
                    CvInvoke.cvCvtColor(ori, gray, COLOR_CONVERSION.CV_BGR2GRAY);
                    var faces = cascade.Detect(gray, 1.1, 2, HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(ori.Width, ori.Height));

                    copy = ori.Clone();

                    foreach (var face in faces)
                    {
                        copy.Draw(face.rect, new Bgr(Color.Red), 3);
                    }

                    pictureBox1.Image = copy.ToBitmap();
                }
            }
            else
            {
                canny_edge_status = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Home Home_load = new Home();
            Home_load.Show();
        }
    }
}
