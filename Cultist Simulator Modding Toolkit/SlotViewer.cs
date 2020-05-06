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
        ModViewer currentMod;

        public SlotViewer(Slot slot, ModViewer currentMod)
        {
            InitializeComponent();
            this.displayedSlot = slot;
            this.currentMod = currentMod;
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

        }

        private void requiredDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = requiredDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            if (Utilities.elementExists(id))
            {
                ElementViewer ev = new ElementViewer(Utilities.getElement(id), currentMod);
                ev.ShowDialog();
            }
            else if(Utilities.aspectExists(id))
            {
                AspectViewer av = new AspectViewer(Utilities.getAspect(id), currentMod);
                av.ShowDialog();
            }
        }

        private void forbiddenDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = forbiddenDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            if (Utilities.elementExists(id))
            {
                ElementViewer ev = new ElementViewer(Utilities.getElement(id), currentMod);
                ev.ShowDialog();
            }
            else if (Utilities.aspectExists(id))
            {
                AspectViewer av = new AspectViewer(Utilities.getAspect(id), currentMod);
                av.ShowDialog();
            }
        }
    }
}
