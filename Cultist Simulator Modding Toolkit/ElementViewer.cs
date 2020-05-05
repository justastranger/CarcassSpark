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

        public ElementViewer(Element element)
        {
            InitializeComponent();
            displayedElement = element;
            idTextBox.Text = element.id;
            labelTextBox.Text = element.label;
            iconTextBox.Text = element.icon;
            animFramesTextBox.Text = Convert.ToString(element.animFrames);
            lifetimeTextBox.Text = Convert.ToString(element.lifeTime);
            decayToTextBox.Text = element.decayTo;
            uniqueCheckBox.Checked = element.unique;
            uniquenessgroupTextBox.Text = element.uniquenessgroup;
            if(element.slots != null)
            {
                foreach (Slot slot in element.slots)
                {
                    slots[slot.id] = slot;
                    slotsListBox.Items.Add(slot.id);
                }
            }
            if (element.xtriggers != null)
            {
                Dictionary<string, string> xtriggers = element.xtriggers.toDictionary();
                foreach (KeyValuePair<string, string> kvp in xtriggers)
                {
                    xtriggersDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (element.aspects.toDictionary() != null)
            {
                Dictionary<string, int> aspects = element.aspects.toDictionary();
                foreach (KeyValuePair<string, int> kvp in aspects)
                {
                    aspectsDataGridView.Rows.Add(kvp.Key, Convert.ToString(kvp.Value));
                } 
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (slotsListBox.SelectedItem == null) return;
            SlotViewer sv = new SlotViewer(slots[slotsListBox.SelectedItem.ToString()]);
            sv.ShowDialog();
        }

        private void aspectsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string aspectID = aspectsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            AspectViewer av = new AspectViewer(Aspect.getAspect(aspectID));
            av.ShowDialog();
        }

        private void xtriggersDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = xtriggersDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (Element.elementExists(id))
            {
                ElementViewer ev = new ElementViewer(Element.getElement(id));
                ev.Show();
            }
            else if (Aspect.aspectExists(id))
            {
                AspectViewer av = new AspectViewer(Aspect.getAspect(id));
                av.ShowDialog();
            }
            else
            {
                MessageBox.Show("XTrigger catalyst and result must both be either aspects or elements.", "What the hell is this?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
