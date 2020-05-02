using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cultist_Simulator_Modding_Toolkit
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Before we initialize, check to see if we're in the game folder at ./CSMT/
            if (File.Exists("./cultistsimulator.exe")) {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            } else {
                MessageBox.Show("Please install me to ./CSMT/ inside your Cultist Simulator installation folder.", "I'm lost :(", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    }
}
