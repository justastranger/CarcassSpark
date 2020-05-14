using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CultistSimulatorModdingToolkit
{
    public partial class ChangeAspectQuantityForm : Form
    {
        public int amount;

        public ChangeAspectQuantityForm(int amount)
        {
            InitializeComponent();
            this.amount = amount;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.amount = Convert.ToInt32(numericUpDown1.Value);
            this.DialogResult = DialogResult.OK;
            this.Close(); 
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.amount = 0;

        }
    }
}
