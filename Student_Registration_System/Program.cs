using System;
using System.Windows.Forms;

namespace Student_Registration_System
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Ensure splashForm is defined correctly
            Application.Run(new splashForm());
        }
    }
}
