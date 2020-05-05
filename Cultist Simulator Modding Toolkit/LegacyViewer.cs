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
    public partial class LegacyViewer : Form
    {
        Legacy displayedLegacy;

        public LegacyViewer(Legacy legacy)
        {
            InitializeComponent();
            displayedLegacy = legacy;
            idTextBox.Text = legacy.id;
            labelTextBox.Text = legacy.label;
            descriptionTextBox.Text = legacy.description;
            startdescriptionTextBox.Text = legacy.startdescription;
            imageTextBox.Text = legacy.image;
            fromEndingTextBox.Text = legacy.fromEnding;
            checkBox1.Checked = legacy.availableWithoutEndingMatch;
            startingVerbIdTextBox.Text = legacy.startingVerbId;
            if (legacy.effects != null && !legacy.effects.isNull())
            {
                foreach (KeyValuePair<string, int> kvp in legacy.effects.toDictionary())
                {
                    effectsDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (legacy.excludesOnEnding != null)
            {
                foreach (string ending in legacy.excludesOnEnding)
                {
                    excludesOnEndingListBox.Items.Add(ending);
                }
            }
        }

        private void effectsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = effectsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            ElementViewer ev = new ElementViewer(Element.getElement(id));
            ev.ShowDialog();
        }

        private void excludesOnEndingListBox_DoubleClick(object sender, EventArgs e)
        {
            string id = excludesOnEndingListBox.SelectedItem.ToString();
            LegacyViewer lv = new LegacyViewer(Legacy.getLegacy(id));
            lv.ShowDialog();
        }
    }
}
