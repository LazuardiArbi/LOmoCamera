using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Project_Akhir_COMVIS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Home home_load = new Home();
            home_load.Show();

            Application.Run();
        }
    }
}
