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

            //Environment.SetEnvironmentVariable("PATH", Environment.GetEnvironmentVariable("PATH") + ";" + dllDirectory + ";");

            AppDomain.CurrentDomain.AssemblyResolve += ResolveCultistSimulatorAssemblies;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (File.Exists(currentDirectory + "csmt.settings.json"))
            {
                Settings.LoadSettings(currentDirectory + "csmt.settings.json");
            }
            Application.Run(new TabbedModViewer());
        }

        private static readonly string[] allowedAssemblies = new string[] { "Assembly-CSharp" };

        private static readonly FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
        {
            SelectedPath = currentDirectory
        };

        private static Assembly ResolveCultistSimulatorAssemblies(object sender, ResolveEventArgs args)
        {
            string assemblyFile = (args.Name.Contains(',')) ? args.Name.Substring(0, args.Name.IndexOf(',')) : args.Name;

            if (!allowedAssemblies.Contains(assemblyFile))
            {
                return null;
            }
            // If we want to select a specific DLL so that CarcSpark can be portable
            if (Settings.settings["portable"] != null && Settings.settings["portable"].ToObject<bool>())
            {
                // if we'd already chosen a DLL, try to load it
                if (Settings.settings["GamePath"] != null && Directory.Exists(Settings.settings["GamePath"].ToString()))
                {
                    try
                    {
                        return Assembly.LoadFile(Settings.settings["GamePath"].ToString() + csDllDirectory + "Assembly-CSharp.dll");
                    }
                    catch (Exception)
                    {
                        // if the DLL can't be loaded automatically, force the user to select one.
                        MessageBox.Show("Previously selected Assembly-CSharp.dll could not be loaded, please select a new one.");
                        return SelectAndLoadAssembly();
                    }
                }
                else
                {
                    // If no Assembly-CSharp.dll is recorded in the settings
                    MessageBox.Show("Please select your Cultist Simulator's Assembly-CSharp.dll file, located in your game's installation folder at\nCultist Simulator/cultistsimulator_Data/Managed");
                    return SelectAndLoadAssembly();
                }
            }
            else
            {
                if (File.Exists("./cultistsimulator.exe"))
                {
                    // Assume we're with cultistsimulator.exe
                    try
                    {
                        Settings.settings["portable"] = false;
                        return Assembly.LoadFile(currentDirectory + csDllDirectory + assemblyFile + ".dll");
                    }
                    catch (Exception)
                    {
                        // DLL was no good, resort to selecting DLL
                        Settings.settings["portable"] = true;
                        return SelectAndLoadAssembly();
                    }
                }
                else
                {
                    MessageBox.Show("Please install me in your Cultist Simulator installation folder.", "I'm lost :(", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Application.Exit();
                    return null;
                }
            }
        }

        private static Assembly SelectAndLoadAssembly()
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Settings.settings["GamePath"] = folderBrowserDialog.SelectedPath;
                    Settings.SaveSettings();
                    return Assembly.LoadFile(folderBrowserDialog.SelectedPath + csDllDirectory + "Assembly-CSharp.dll");
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }
    }
}
