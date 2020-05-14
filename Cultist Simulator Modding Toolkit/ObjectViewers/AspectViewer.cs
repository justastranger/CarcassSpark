using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CultistSimulatorModdingToolkit.ObjectTypes;
using CultistSimulatorModdingToolkit.ObjectTypes;

namespace CultistSimulatorModdingToolkit.ObjectViewers
{
    public partial class AspectViewer : Form
    {
        public Aspect displayedAspect;
        Dictionary<string, Induces> inducesDictionary;
        bool editing;

        public AspectViewer(Aspect aspect, bool? editing)
        {
            InitializeComponent();
            this.displayedAspect = aspect;
            if(aspect.extends != null)
            {
                Aspect extendedAspect = Utilities.getAspect(aspect.extends[0]);
                extendsTextBox.Text = aspect.extends[0];
                fillValues(extendedAspect);
            }
            fillValues(aspect);
            if (editing.HasValue)
            {
                setEditingMode(editing.Value);
                this.editing = editing.Value;
            }
            else
            {
                setEditingMode(false);
                this.editing = false;
            }
        }

        void setEditingMode(bool editing)
        {
            idTextBox.ReadOnly = !editing;
            labelTextBox.ReadOnly = !editing;
            iconTextBox.ReadOnly = !editing;
            descriptionTextBox.ReadOnly = !editing;
            extendsTextBox.ReadOnly = !editing;
            isHiddenCheckBox.Enabled = editing;
            noartworkneededCheckBox.Enabled = editing;
            inducesDataGridView.AllowUserToAddRows = editing;
            inducesDataGridView.AllowUserToDeleteRows = editing;
            inducesDataGridView.ReadOnly = !editing;
            cancelButton.Text = editing ? "Cancel" : "Close";
            okButton.Visible = editing;
        }

        public void fillValues(Aspect aspect)
        {
            if (aspect.id != null) idTextBox.Text = aspect.id;
            if (aspect.label != null) labelTextBox.Text = aspect.label;
            if (!aspect.noartneeded.HasValue || aspect.noartneeded.Value == false)
            {
                if (aspect.icon != null)
                {
                    iconTextBox.Text = aspect.icon;
                    pictureBox1.Image = Utilities.getAspectImage(aspect.icon);
                }
                else
                {
                    pictureBox1.Image = Utilities.getAspectImage(aspect.id);
                }
            }
            if (aspect.description != null) descriptionTextBox.Text = aspect.description;
            if (aspect.isHidden.HasValue) isHiddenCheckBox.Checked = aspect.isHidden.Value;
            if (aspect.induces != null)
            {
                inducesDictionary = new Dictionary<string, Induces>();
                foreach (Induces induces in aspect.induces)
                {
                    inducesDictionary.Add(induces.id, induces);
                    inducesDataGridView.Rows.Add(induces.id, induces.chance, induces.additional.HasValue ? induces.additional.Value : false);
                }
            }
        }

        private void inducesDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = inducesDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            RecipeViewer rv = new RecipeViewer(Utilities.getRecipe(id), editing);
            rv.ShowDialog();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (idTextBox.Text == null || idTextBox.Text == "")
            {
                MessageBox.Show("All Aspects must have an ID.");
                return;
            }
            if (inducesDataGridView.Rows.Count > 1) {
                displayedAspect.induces = new List<Induces>();
                foreach (DataGridViewRow row in inducesDataGridView.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null) displayedAspect.induces.Add(new Induces(row.Cells[0].Value.ToString(), Convert.ToInt32(row.Cells[1].Value), Convert.ToBoolean(row.Cells[2].Value)));
                }
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void idTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedAspect.id = idTextBox.Text;
        }

        private void labelTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedAspect.label = labelTextBox.Text;
        }

        private void iconTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedAspect.icon = iconTextBox.Text;
            if(Utilities.getAspectImage(iconTextBox.Text) != null)
            {
                pictureBox1.Image = Utilities.getAspectImage(iconTextBox.Text);
            }
        }

        private void extendsTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedAspect.extends = new string[] { extendsTextBox.Text };
        }

        private void descriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedAspect.description = descriptionTextBox.Text;
        }

        private void isHiddenCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            displayedAspect.isHidden = isHiddenCheckBox.Checked;
        }

        private void noartworkneededCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            displayedAspect.noartneeded = noartworkneededCheckBox.Checked;
        }

        private void inducesDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (displayedAspect.induces.Contains(inducesDictionary[e.Row.Cells[0].Value.ToString()])) displayedAspect.induces.Remove(inducesDictionary[e.Row.Cells[0].Value.ToString()]);
            if (displayedAspect.induces.Count == 0) displayedAspect.induces = null;
        }
    }
}
