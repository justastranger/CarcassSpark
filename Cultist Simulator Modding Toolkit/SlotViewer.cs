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
        public SlotViewer(Slot slot)
        {
            InitializeComponent();

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
    }
}
