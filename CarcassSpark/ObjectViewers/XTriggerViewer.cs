using CarcassSpark.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CarcassSpark.ObjectViewers
{
    public partial class XTriggerViewer : Form
    {
        public List<XTrigger> DisplayedXTriggers;
        public string Catalyst;

        public XTriggerViewer(string catalyst, List<XTrigger> xTriggers, bool editing)
        {
            InitializeComponent();
            DisplayedXTriggers = xTriggers;
            this.Catalyst = catalyst;
            FillValues();
            SetEditingMode(editing);
        }

        public XTriggerViewer(string catalyst, List<XTrigger> xTriggers, bool editing, bool remove)
        {
            InitializeComponent();
            DisplayedXTriggers = xTriggers;
            this.Catalyst = catalyst;
            SetEditingMode(editing);
            xtriggersDataGridView.ReadOnly = remove;
        }

        public XTriggerViewer(string catalyst, List<XTrigger> xTriggers)
        {
            InitializeComponent();
            DisplayedXTriggers = xTriggers;
            this.Catalyst = catalyst;
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
            catalystTextBox.Text = Catalyst;
            if (DisplayedXTriggers != null && DisplayedXTriggers.Count > 0)
            {
                foreach (XTrigger xTrigger in DisplayedXTriggers)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(xtriggersDataGridView, xTrigger.id, xTrigger.chance?.ToString(), xTrigger.level?.ToString(), xTrigger.morpheffect?.ToLower());
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
                DisplayedXTriggers = new List<XTrigger>();
                foreach (DataGridViewRow row in xtriggersDataGridView.Rows)
                {
                    if (string.IsNullOrEmpty(row.Cells[0].Value as string))
                    {
                        continue;
                    }

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
                    DisplayedXTriggers.Add(xtrigger);
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
            Catalyst = catalystTextBox.Text;
            if (Catalyst == "")
            {
                Catalyst = null;
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("This will delete this entire entry, are you sure you want to do this?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                DisplayedXTriggers = null;
                Close();
            }
        }

        private void XTriggerViewer_Shown(object sender, EventArgs e)
        {
            FillValues();
        }
    }
}
