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
    public partial class Edge_Detection : Form
    {
        Image<Bgr, byte> original_image;
        Image<Gray, byte> gray_image, canny_image;
        Image<Gray, float> converted_gray_image, sobel_image, laplace_image;

        public Edge_Detection()
        {
            InitializeComponent();
            groupBox2.Visible = false;
            comboBox1.SelectedIndex = 0;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                original_image = new Image<Bgr, byte>(openFileDialog1.FileName);
                gray_image = new Image<Gray, byte>(original_image.Size.Width, original_image.Size.Height);
                canny_image = gray_image.Clone();

                CvInvoke.cvCvtColor(original_image, gray_image, COLOR_CONVERSION.CV_BGR2GRAY);

                converted_gray_image = gray_image.Convert<Gray, float>();

                CvInvoke.cvCanny(gray_image, canny_image, 60, 150, 3);
                sobel_image = converted_gray_image.Sobel(1, 0, 3);
                laplace_image = converted_gray_image.Laplace(7);

                pictureBox1.Image = original_image.ToBitmap();
                pictureBox3.Image = canny_image.ToBitmap();
                pictureBox4.Image = laplace_image.ToBitmap();
                pictureBox2.Image = sobel_image.ToBitmap();
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
                pictureBox1.Image = pictureBox3.Image;
                label1.ForeColor = Color.AliceBlue;
                label2.ForeColor = Color.Black;
                label3.ForeColor = Color.Black;
                groupBox2.Visible = true;
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
                pictureBox1.Image = pictureBox4.Image;
                label2.ForeColor = Color.AliceBlue;
                label3.ForeColor = Color.Black;
                label1.ForeColor = Color.Black;
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
                pictureBox1.Image = pictureBox2.Image;
                label3.ForeColor = Color.AliceBlue;
                label2.ForeColor = Color.Black;
                label1.ForeColor = Color.Black;
                groupBox2.Visible = false;
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            int value = 0;
            if (comboBox1.SelectedIndex == 0)
            {
                value = 3;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                value = 5;
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                value = 7;
            }
            CvInvoke.cvCanny(gray_image, canny_image, (double)trackBar2.Value, (double)trackBar3.Value, value);
            pictureBox1.Image = canny_image.ToBitmap();
            pictureBox3.Image = pictureBox1.Image;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            int value = 0;
            if (comboBox1.SelectedIndex == 0)
            {
                value = 3;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                value = 5;
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                value = 7;
            }
            CvInvoke.cvCanny(gray_image, canny_image, (double)trackBar2.Value, (double)trackBar3.Value, value);
            pictureBox1.Image = canny_image.ToBitmap();
            pictureBox3.Image = pictureBox1.Image;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Home Home_load = new Home();
            Home_load.Show();
        }
    }
}
