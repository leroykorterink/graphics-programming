using System;
using System.Windows.Forms;

namespace graphics_programming
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

            // Starts application that renders 2D shapes
            //Application.Run(new Form2D());

            // Starts application that renders 3D shapes
            Application.Run(new Form3D());
        }
    }
}
