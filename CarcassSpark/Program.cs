using CarcassSpark.ObjectViewers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarcassSpark
{
    static class Program
    {

        static readonly string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        static readonly string csDllDirectory = currentDirectory + "cultistsimulator_Data\\Managed\\";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Before we initialize, check to see if we're in the game folder at ./CSMT/
            if (File.Exists("./cultistsimulator.exe")) {

                Mutex mutex = new Mutex(true, "CarcassSpark", out bool newInstance);
                if (!newInstance)
                {
                    MessageBox.Show("Only one instance of Carcass Spark may be open at a time.");
                    return;
                }

                //Environment.SetEnvironmentVariable("PATH", Environment.GetEnvironmentVariable("PATH") + ";" + dllDirectory + ";");

                AppDomain.CurrentDomain.AssemblyResolve += ResolveCultistSimulatorAssemblies;

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                if (File.Exists(currentDirectory + "csmt.settings.json"))
                {
                    Settings.LoadSettings(currentDirectory + "csmt.settings.json");
                }
                Application.Run(new TabbedModViewer());
            } else {
                MessageBox.Show("Please install me your Cultist Simulator installation folder.", "I'm lost :(", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private static readonly string[] allowedAssemblies = new string[] { "Assembly-CSharp" };

        private static Assembly ResolveCultistSimulatorAssemblies(object sender, ResolveEventArgs args)
        {
            string assemblyFile = (args.Name.Contains(',')) ? args.Name.Substring(0, args.Name.IndexOf(',')) : args.Name;

            if (!allowedAssemblies.Contains(assemblyFile))
            {
                return null;
            }

            try
            {
                return Assembly.LoadFile(csDllDirectory + assemblyFile+".dll");
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
