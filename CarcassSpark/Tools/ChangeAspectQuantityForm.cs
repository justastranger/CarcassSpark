using System;
using System.Windows.Forms;

namespace CarcassSpark.Tools
{
    public partial class ChangeAspectQuantityForm : Form
    {
        public int Amount;

        public ChangeAspectQuantityForm(int amount)
        {
            InitializeComponent();
            this.Amount = amount;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Amount = Convert.ToInt32(numericUpDown1.Value);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Amount = 0;

        }
    }
}
