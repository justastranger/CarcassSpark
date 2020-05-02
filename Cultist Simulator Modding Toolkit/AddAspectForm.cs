using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cultist_Simulator_Modding_Toolkit
{
    public partial class AddAspectForm : Form
    {
        public AddAspectForm()
        {
            InitializeComponent();
            aspectListBox.Items.Clear();
            foreach (string key in Aspect.aspectsList.Keys.ToArray())
            {
                aspectListBox.Items.Add(key);
            }
        }

        private void addAspectAcceptButton_Click(object sender, EventArgs e)
        {
            // this should be a string anyways, but just in case, I guess.
            string aspectID = aspectListBox.SelectedItem.ToString();
            int amount = Convert.ToInt32(aspectAmountUpDown.Value);
        }

        private void addAspectCancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
