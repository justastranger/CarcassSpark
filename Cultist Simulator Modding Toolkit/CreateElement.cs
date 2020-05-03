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
    public partial class CreateElement : Form
    {
        public CreateElement()
        {
            InitializeComponent();
        }

        private void isAspectCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            aspectsListView.Visible = !isAspectCheckbox.Checked;
            isHiddenCheckBox.Visible = isAspectCheckbox.Checked;
            this.Text = isAspectCheckbox.Checked ? "Create Aspect" : "Create Element";
        }

        private Aspect generateAspect()
        {

        }

        private Element generateElement()
        {

        }

        private void iconSelectButton_Click(object sender, EventArgs e)
        {
            openIconDialog.InitialDirectory = MainForm.currentModDirectory;
            openIconDialog.ShowDialog();
        }

        private void openIconDialog_FileOk(object sender, CancelEventArgs e)
        {
            iconPictureBox.Image = new Bitmap(openIconDialog.OpenFile());
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (isAspectCheckbox.Checked) {
                generateAspect();
            } else {
                generateElement();
            }
        }
    }
}
