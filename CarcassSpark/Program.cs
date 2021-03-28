using CarcassSpark.ObjectViewers;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace CarcassSpark
{
    internal static class Program
    {
        private static readonly string CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly string CsDllDirectory = "\\cultistsimulator_Data\\Managed\\";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
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
            if (File.Exists(CurrentDirectory + "csmt.settings.json"))
            {
                Settings.LoadSettings(CurrentDirectory + "csmt.settings.json");
            }
            Application.Run(new TabbedModViewer());
        }
    }
}
