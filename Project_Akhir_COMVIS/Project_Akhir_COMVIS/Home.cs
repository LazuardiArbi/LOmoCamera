using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project_Akhir_COMVIS
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Image_Processing image_processing_load = new Image_Processing();
            image_processing_load.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Shape_Detection Shape_Detection_load = new Shape_Detection();
            Shape_Detection_load.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Pattern_Recognition_and_Classification Pattern_Recognition_and_Classification_load = new Pattern_Recognition_and_Classification();
            Pattern_Recognition_and_Classification_load.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Edge_Detection Edge_Detection_load = new Edge_Detection();
            Edge_Detection_load.Show();
        }
    }
}
