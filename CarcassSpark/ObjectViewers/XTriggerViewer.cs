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

        public XTriggerViewer(string catalyst, List<XTrigger> xTriggers, bool editing)
        {
            InitializeComponent();
            displayedXTriggers = xTriggers;
            this.catalyst = catalyst;
            FillValues();
            SetEditingMode(editing);
        }

        public XTriggerViewer(string catalyst, List<XTrigger> xTriggers, bool editing, bool remove)
        {
            InitializeComponent();
            displayedXTriggers = xTriggers;
            this.catalyst = catalyst;
            FillValues();
            SetEditingMode(editing);
            xtriggersDataGridView.ReadOnly = remove;
        }

        public XTriggerViewer(string catalyst, List<XTrigger> xTriggers)
        {
            InitializeComponent();
            displayedXTriggers = xTriggers;
            this.catalyst = catalyst;
            FillValues();
            SetEditingMode(false);
        }

        public XTriggerViewer()
        {
            InitializeComponent();
            SetEditingMode(true);
        }

        public void FillValues()
        {
            catalystTextBox.Text = catalyst;
            if (displayedXTriggers != null && displayedXTriggers.Count > 0)
            {
                foreach (XTrigger xTrigger in displayedXTriggers)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(xtriggersDataGridView, xTrigger.id, xTrigger.chance.HasValue ? xTrigger.chance.Value.ToString() : null, xTrigger.level.HasValue ? xTrigger.level.Value.ToString() : null, xTrigger.morpheffect?.ToLower());
                    xtriggersDataGridView.Rows.Add(row);
                }
            }
        }

        public void SetEditingMode(bool editing)
        {
            catalystTextBox.ReadOnly = !editing;
            xtriggersDataGridView.AllowUserToAddRows = editing;
            xtriggersDataGridView.AllowUserToDeleteRows = editing;
            xtriggersDataGridView.ReadOnly = !editing;
            okButton.Visible = editing;
            cancelButton.Text = editing ? "Cancel" : "Close";
        }

        private void OkButton_Click(object sender, EventArgs e)
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
                        chance = Convert.ToInt32(row.Cells[1].Value) > 0 ? Convert.ToInt32(row.Cells[1].Value) : (int?)null,
                        level = Convert.ToInt32(row.Cells[2].Value) > 0 ? Convert.ToInt32(row.Cells[2].Value) : (int?)null,
                        morpheffect = row.Cells[3].Value as string
                    };
                    // row.Cells[0] -> id
                    // row.Cells[1] -> chance
                    // row.Cells[2] -> level
                    // row.Cells[3] -> morphEffect
                    displayedXTriggers.Add(xtrigger);
                }
            }
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CatalystTextBox_TextChanged(object sender, EventArgs e)
        {
            catalyst = catalystTextBox.Text;
            if (catalyst == "")
            {
                catalyst = null;
            }
        }
    }
}
