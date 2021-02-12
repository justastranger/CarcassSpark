using CarcassSpark.ObjectViewers;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace CarcassSpark
{
    static class Program
    {

        static readonly string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        static readonly string csDllDirectory = "\\cultistsimulator_Data\\Managed\\";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Mutex mutex = new Mutex(true, "CarcassSpark");
            if (!mutex.WaitOne(0, false))
            {
                MessageBox.Show("Only one instance of Carcass Spark may be open at a time.");
                return;
            }

            // AppDomain.CurrentDomain.AssemblyResolve += ResolveCultistSimulatorAssemblies;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (File.Exists(currentDirectory + "csmt.settings.json"))
            {
                Settings.LoadSettings(currentDirectory + "csmt.settings.json");
            }
            Application.Run(new TabbedModViewer());
        }
    }
}
