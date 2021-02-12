using System;
using System.Windows.Forms;

namespace CarcassSpark.Tools
{
    public partial class ChangeAspectQuantityForm : Form
    {
        public int amount;

        public ChangeAspectQuantityForm(int amount)
        {
            InitializeComponent();
            this.amount = amount;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            amount = Convert.ToInt32(numericUpDown1.Value);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            amount = 0;

        }
    }
}
