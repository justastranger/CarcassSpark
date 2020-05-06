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
    public partial class SlotViewer : Form
    {
        Slot displayedSlot;

        public SlotViewer(Slot slot, bool? editing)
        {
            InitializeComponent();
            this.displayedSlot = slot;
            idTextBox.Text = slot.id;
            labelTextBox.Text = slot.label;
            descriptionTextBox.Text = slot.description;
            actionIdTextBox.Text = slot.actionId;
            if (slot.greedy.HasValue) greedyCheckBox.Checked = slot.greedy.Value;
            if (slot.required != null)
            {
                foreach (KeyValuePair<string, int> req in slot.required)
                {
                    requiredDataGridView.Rows.Add(req.Key, req.Value);
                } 
            }
            if (slot.forbidden != null)
            {
                foreach (KeyValuePair<string, int> req in slot.forbidden)
                {
                    forbiddenDataGridView.Rows.Add(req.Key, req.Value);
                } 
            }
            if (editing.HasValue) setEditingMode(editing.Value);
            else setEditingMode(false);
        }

        void setEditingMode(bool editing)
        {
            idTextBox.Enabled = editing;
            labelTextBox.Enabled = editing;
            descriptionTextBox.Enabled = editing;
            actionIdTextBox.Enabled = editing;
            greedyCheckBox.Enabled = editing;
            requiredDataGridView.ReadOnly = !editing;
            forbiddenDataGridView.ReadOnly = !editing;
        }

        private void requiredDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = requiredDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            if (Utilities.elementExists(id))
            {
                ElementViewer ev = new ElementViewer(Utilities.getElement(id), false);
                ev.ShowDialog();
            }
            else if(Utilities.aspectExists(id))
            {
                AspectViewer av = new AspectViewer(Utilities.getAspect(id), false);
                av.ShowDialog();
            }
        }

        private void forbiddenDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = forbiddenDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            if (Utilities.elementExists(id))
            {
                ElementViewer ev = new ElementViewer(Utilities.getElement(id), false);
                ev.ShowDialog();
            }
            else if (Utilities.aspectExists(id))
            {
                AspectViewer av = new AspectViewer(Utilities.getAspect(id), false);
                av.ShowDialog();
            }
        }
    }
}
