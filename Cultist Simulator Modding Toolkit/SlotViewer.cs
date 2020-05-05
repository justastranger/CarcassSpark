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
            greedyCheckBox.Checked = slot.greedy;
            if (slot.required != null)
            {
                foreach (Required.Requirement req in slot.required.requirements)
                {
                    requiredDataGridView.Rows.Add(req.id, req.amount);
                } 
            }
            if (slot.forbidden != null)
            {
                foreach (Required.Requirement req in slot.forbidden.requirements)
                {
                    forbiddenDataGridView.Rows.Add(req.id, req.amount);
                } 
            }

        }

        private void requiredDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = requiredDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            if (currentMod.elementExists(id))
            {
                ElementViewer ev = new ElementViewer(currentMod.getElement(id), currentMod);
                ev.ShowDialog();
            }
            else if(currentMod.aspectExists(id))
            {
                AspectViewer av = new AspectViewer(currentMod.getAspect(id), currentMod);
                av.ShowDialog();
            }
        }

        private void forbiddenDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = forbiddenDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            if (currentMod.elementExists(id))
            {
                ElementViewer ev = new ElementViewer(currentMod.getElement(id), currentMod);
                ev.ShowDialog();
            }
            else if (currentMod.aspectExists(id))
            {
                AspectViewer av = new AspectViewer(currentMod.getAspect(id), currentMod);
                av.ShowDialog();
            }
        }
    }
}
