using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cultist_Simulator_Modding_Toolkit
{
    static class Program
    {

        static string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        static string dllDirectory = currentDirectory + "cultistsimulator_Data\\Managed\\";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Before we initialize, check to see if we're in the game folder at ./CSMT/
            if (File.Exists("./cultistsimulator.exe")) {

                //Environment.SetEnvironmentVariable("PATH", Environment.GetEnvironmentVariable("PATH") + ";" + dllDirectory + ";");

                AppDomain.CurrentDomain.AssemblyResolve += resolveCultistSimulatorAssemblies;

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            } else {
                MessageBox.Show("Please install me your Cultist Simulator installation folder.", "I'm lost :(", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private static string[] allowedAssemblies = new string[] { "Assembly-CSharp" };

        private static Assembly resolveCultistSimulatorAssemblies(object sender, ResolveEventArgs args)
        {
            string assemblyFile = (args.Name.Contains(',')) ? args.Name.Substring(0, args.Name.IndexOf(',')) : args.Name;

            if (!allowedAssemblies.Contains(assemblyFile))
            {
                return null;
            }

            try
            {
                return Assembly.LoadFile(dllDirectory + assemblyFile+".dll");
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
