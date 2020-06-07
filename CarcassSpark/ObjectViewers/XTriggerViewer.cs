using CarcassSpark.ObjectTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarcassSpark.ObjectViewers
{
    public partial class XTriggerViewer : Form
    {
        public List<XTrigger> displayedXTriggers;
        public string catalyst;
        bool editing = false;

        public XTriggerViewer(string catalyst, List<XTrigger> xTriggers, bool editing)
        {
            InitializeComponent();
            displayedXTriggers = xTriggers;
            this.catalyst = catalyst;
            fillValues();
            this.editing = editing;
            setEditingMode(editing);
        }

        public XTriggerViewer(string catalyst, List<XTrigger> xTriggers, bool editing, bool remove)
        {
            InitializeComponent();
            displayedXTriggers = xTriggers;
            this.catalyst = catalyst;
            fillValues();
            this.editing = editing;
            setEditingMode(editing);
            xtriggersDataGridView.ReadOnly = remove;
        }

        public XTriggerViewer(string catalyst, List<XTrigger> xTriggers)
        {
            InitializeComponent();
            displayedXTriggers = xTriggers;
            this.catalyst = catalyst;
            fillValues();
            setEditingMode(false);
        }

        public XTriggerViewer()
        {
            InitializeComponent();
            setEditingMode(true);
        }

        public void fillValues()
        {
            catalystTextBox.Text = catalyst;
            if (displayedXTriggers != null && displayedXTriggers.Count > 0)
            {
                foreach (XTrigger xTrigger in displayedXTriggers)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(xtriggersDataGridView, xTrigger.id, xTrigger.chance.HasValue ? xTrigger.chance.Value.ToString() : null, xTrigger.level.HasValue ? xTrigger.level.Value.ToString() : null, xTrigger.morphEffect != null ? xTrigger.morphEffect.ToLower() : null);
                    xtriggersDataGridView.Rows.Add(row);
                }
            }
        }

        public void setEditingMode(bool editing)
        {
            catalystTextBox.ReadOnly = !editing;
            xtriggersDataGridView.AllowUserToAddRows = editing;
            xtriggersDataGridView.AllowUserToDeleteRows = editing;
            xtriggersDataGridView.ReadOnly = !editing;
            okButton.Visible = editing;
            cancelButton.Text = editing ? "Cancel" : "Close";
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (xtriggersDataGridView.Rows.Count > 1)
            {
                displayedXTriggers = new List<XTrigger>();
                foreach (DataGridViewRow row in xtriggersDataGridView.Rows)
                {
                    if (row.Cells[0].Value as string == null || row.Cells[0].Value as string == "") continue;
                    XTrigger xtrigger = new XTrigger()
                    {
                        id = row.Cells[0].Value as string,
                        chance = row.Cells[1].Value as int?,
                        level = row.Cells[2].Value as int?,
                        morphEffect = row.Cells[3].Value as string
                    };
                    // row.Cells[0] -> id
                    // row.Cells[1] -> chance
                    // row.Cells[2] -> level
                    // row.Cells[3] -> morphEffect
                    displayedXTriggers.Add(xtrigger);
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

        private void catalystTextBox_TextChanged(object sender, EventArgs e)
        {
            catalyst = catalystTextBox.Text;
            if (catalyst == "")
            {
                catalyst = null;
            }
        }
    }
}
