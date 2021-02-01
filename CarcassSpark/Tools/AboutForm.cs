using CarcassSpark.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarcassSpark.Tools
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            versionLabel.Text = "Version: " + Application.ProductVersion;
            pictureBox1.Image = Resources.toolforgef.ToBitmap();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SourceLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = @"https://github.com/justastranger/CarcassSpark/"
            };
            Process.Start(startInfo);
        }
    }
}
