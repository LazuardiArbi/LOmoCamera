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
    public partial class Image_Processing : Form
    {
        Image<Bgr, byte> original_image, smooth_image;
        Image<Gray, byte> gray_image, thr_image;

        public Image_Processing()
        {
            InitializeComponent();
            groupBox2.Visible = false;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                original_image = new Image<Bgr, byte>(openFileDialog1.FileName);
                smooth_image = new Image<Bgr, byte>(original_image.Size.Width, original_image.Size.Height);
                gray_image = new Image<Gray, byte>(original_image.Size.Width, original_image.Size.Height);
                thr_image = new Image<Gray, byte>(original_image.Size.Width, original_image.Size.Height);

                CvInvoke.cvSmooth(original_image, smooth_image, SMOOTH_TYPE.CV_BLUR, 5, 5, 0, 0);
                CvInvoke.cvCvtColor(original_image, gray_image, COLOR_CONVERSION.CV_BGR2GRAY);
                CvInvoke.cvThreshold(gray_image, thr_image, (double)100, (double)128, THRESH.CV_THRESH_BINARY);

                pictureBox1.Image = original_image.ToBitmap();
                pictureBox2.Image = thr_image.ToBitmap();
                pictureBox3.Image = smooth_image.ToBitmap();
                pictureBox4.Image = gray_image.ToBitmap();

            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (pictureBox3.Image == null)
            {
                MessageBox.Show("Please upload image first");
            }
            else
            {
                label1.ForeColor = Color.AliceBlue;
                label2.ForeColor = Color.Black;
                label3.ForeColor = Color.Black;
                pictureBox1.Image = pictureBox3.Image;
                groupBox2.Visible = true;
                trackBar1.Visible = true;
                label4.Visible = true;
                trackBar2.Visible = false;
                trackBar3.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (pictureBox4.Image == null)
            {
                MessageBox.Show("Please upload image first");
            }
            else
            {
                label2.ForeColor = Color.AliceBlue;
                label1.ForeColor = Color.Black;
                label3.ForeColor = Color.Black;
                pictureBox1.Image = pictureBox4.Image;
                groupBox2.Visible = false;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image == null)
            {
                MessageBox.Show("Please upload image first");
            }
            else
            {
                label3.ForeColor = Color.AliceBlue;
                label1.ForeColor = Color.Black;
                label2.ForeColor = Color.Black;
                pictureBox1.Image = pictureBox2.Image;
                groupBox2.Visible = true;
                trackBar2.Visible = true;
                trackBar3.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                trackBar1.Visible = false;
                label4.Visible = false;
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            CvInvoke.cvCvtColor(original_image, gray_image, COLOR_CONVERSION.CV_BGR2GRAY);
            CvInvoke.cvThreshold(gray_image, thr_image, (double)trackBar2.Value, (double)trackBar3.Value, THRESH.CV_THRESH_BINARY);
            pictureBox2.Image = thr_image.ToBitmap();
            pictureBox1.Image = pictureBox2.Image;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int value = 1;
            if (trackBar1.Value % 2 == 0)
            {
                value = trackBar1.Value + 1;
            }
            else
            {
                value = trackBar1.Value;
            }

            CvInvoke.cvSmooth(original_image, smooth_image, SMOOTH_TYPE.CV_BLUR, value, value, 0, 0);
            pictureBox3.Image = smooth_image.ToBitmap();
            pictureBox1.Image = pictureBox3.Image;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            CvInvoke.cvCvtColor(original_image, gray_image, COLOR_CONVERSION.CV_BGR2GRAY);
            CvInvoke.cvThreshold(gray_image, thr_image, (double)trackBar2.Value, (double)trackBar3.Value, THRESH.CV_THRESH_BINARY);
            pictureBox2.Image = thr_image.ToBitmap();
            pictureBox1.Image = pictureBox2.Image;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Home Home_load = new Home();
            Home_load.Show();
        }
    }
}
