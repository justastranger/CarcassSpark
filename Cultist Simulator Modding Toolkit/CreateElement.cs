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
            isHiddenCheckBox.Visible = isAspectCheckbox.Checked;
            this.Text = isAspectCheckbox.Checked ? "Create Aspect" : "Create Element";
        }

        private Aspect generateAspect()
        {
            /*
                     (string id, string label, string description,
                      string icon = null, Induces[] induces = null,
                      bool isHidden = false, bool noartneeded = false,
                      bool isAspect = true, string comments = null)
             */
            Aspect temp = new Aspect(idTextBox.Text, labelTextBox.Text, descriptionTextBox.Text,
                                    idTextBox.Text, (Aspect.Induces[]) null, isHiddenCheckBox.Checked,
                                    noartneededCheckBox.Checked, true, commentsTextBox.Text);
            return temp;
        }

        private Element generateElement()
        {
            /*
                  (string id = null, string label = null, string description = null,
                   string icon = null, string comments = null, AspectDictionary aspects = null,
                   Slot[] slots = null, XTriggers xtriggers = null)
             */
            Element temp = new Element(idTextBox.Text, labelTextBox.Text, descriptionTextBox.Text,
                                       idTextBox.Text, commentsTextBox.Text, elementAspects, null, null);
            return temp;
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
        
        private void addAspectContextMenuItem_Click(object sender, EventArgs e)
        {
            using (var frm = new AddAspectForm())
            {
                var result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string aspectID = frm.aspectID;
                    int amount = frm.amount;

                }
            }
        }

        private void removeAspectContextMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void changeQuantityContextMenuItem_Click(object sender, EventArgs e)
        {
            using (var frm = new ChangeAspectQuantityForm(elementAspects[selectedAspect]))
            {
                var result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    int amount = frm.amount;
                }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        // aspectsListBox section

        string selectedAspect;
        AspectDictionary elementAspects = new AspectDictionary();
        
        private void actuallyAddAspect(string aspectID, int amount)
        {
            elementAspects[aspectID] = amount;
            dataGridView1.Rows.Add(aspectID, amount);
        }
        
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            changeQuantityContextMenuItem_Click(sender, e);
        }

        private void addAspectButton_Click(object sender, EventArgs e)
        {
            using (var frm = new AddAspectForm())
            {
                var value = frm.ShowDialog();
                if (value == DialogResult.OK)
                {
                    string aspectId = frm.aspectID;
                    int amount = frm.amount;
                    actuallyAddAspect(aspectId, amount);
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            selectedAspect = dataGridView1.SelectedCells[0].Value.ToString();
        }
    }
}
