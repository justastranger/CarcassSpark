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
    public partial class ElementViewer : Form
    {

        Dictionary<string, Slot> slots = new Dictionary<string, Slot>();
        Element displayedElement;
        ModViewer currentMod;

        public ElementViewer(Element element, ModViewer currentMod)
        {
            InitializeComponent();
            displayedElement = element;
            this.currentMod = currentMod;
            if (element.extends != null)
            {
                extendsTextBox.Text = element.extends[0]; // afaik extends should only ever be an array of a single string
                Element extendedElement = Utilities.getElement(element.extends[0]);
                fillValues(extendedElement);
            }
            fillValues(element);
        }

        private void fillValues(Element element)
        {
            if (element.id != null) idTextBox.Text = element.id;
            if (element.label != null) labelTextBox.Text = element.label;
            if (element.icon != null) iconTextBox.Text = element.icon;
            if (element.animFrames.HasValue) animFramesTextBox.Text = Convert.ToString(element.animFrames.Value);
            if (element.lifeTime.HasValue) lifetimeTextBox.Text = Convert.ToString(element.lifeTime.Value);
            if (element.decayTo != null) decayToTextBox.Text = element.decayTo;
            if (element.unique.HasValue) uniqueCheckBox.Checked = element.unique.Value;
            if (element.uniquenessgroup != null) uniquenessgroupTextBox.Text = element.uniquenessgroup;
            if (element.slots != null)
            {
                foreach (Slot slot in element.slots)
                {
                    slots[slot.id] = slot;
                    slotsListBox.Items.Add(slot.id);
                }
            }
            if (element.xtriggers != null)
            {
                Dictionary<string, string> xtriggers = element.xtriggers;
                foreach (KeyValuePair<string, string> kvp in xtriggers)
                {
                    xtriggersDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (element.aspects != null)
            {
                Dictionary<string, int> aspects = element.aspects;
                foreach (KeyValuePair<string, int> kvp in aspects)
                {
                    aspectsDataGridView.Rows.Add(kvp.Key, Convert.ToString(kvp.Value));
                }
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (slotsListBox.SelectedItem == null) return;
            SlotViewer sv = new SlotViewer(slots[slotsListBox.SelectedItem.ToString()], currentMod);
            sv.ShowDialog();
        }

        private void aspectsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string aspectID = aspectsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            AspectViewer av = new AspectViewer(Utilities.getAspect(aspectID), currentMod);
            av.ShowDialog();
        }

        private void xtriggersDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = xtriggersDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (Utilities.elementExists(id))
            {
                ElementViewer ev = new ElementViewer(Utilities.getElement(id), currentMod);
                ev.Show();
            }
            else if (Utilities.aspectExists(id))
            {
                AspectViewer av = new AspectViewer(Utilities.getAspect(id), currentMod);
                av.ShowDialog();
            }
            else
            {
                MessageBox.Show("XTrigger catalyst and result must both be either aspects or elements.", "What the hell is this?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
