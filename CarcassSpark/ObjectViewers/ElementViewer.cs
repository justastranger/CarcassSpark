using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CarcassSpark.ObjectTypes;

namespace CarcassSpark.ObjectViewers
{
    public enum ElementType
    {
        ELEMENT,
        ASPECT,
        GENERATOR
    }
    public partial class ElementViewer : Form
    {
        readonly Dictionary<string, Slot> slots = new Dictionary<string, Slot>();
        public Element displayedElement;
        bool editing;
        event EventHandler<Element> SuccessCallback;
        public ListViewItem associatedListViewItem;

        public ElementViewer(Element element, EventHandler<Element> SuccessCallback, ListViewItem item)
        {
            InitializeComponent();
            displayedElement = element;
            FillValues(element);
            associatedListViewItem = item;
            if (SuccessCallback != null)
            {
                SetEditingMode(true);
                this.SuccessCallback += SuccessCallback;
            }
            else
            {
                SetEditingMode(false);
            }
        }

        public ElementViewer(Element element, EventHandler<Element> SuccessCallback, ElementType elementType, ListViewItem item)
        {
            InitializeComponent();
            displayedElement = element;
            FillValues(element);
            associatedListViewItem = item;
            if (SuccessCallback != null)
            {
                SetEditingMode(true);
                this.SuccessCallback += SuccessCallback;
            }
            else
            {
                SetEditingMode(false);
            }
            SetType(elementType);
        }

        void SetEditingMode(bool editing)
        {
            this.editing = editing;
            idTextBox.ReadOnly = !editing;
            labelTextBox.ReadOnly = !editing;
            iconTextBox.ReadOnly = !editing;
            lifetimeNumericUpDown.Enabled = editing;
            decayToTextBox.ReadOnly = !editing;
            uniqueCheckBox.Enabled = editing;
            resaturateCheckBox.Enabled = editing;
            uniquenessgroupTextBox.ReadOnly = !editing;
            descriptionTextBox.ReadOnly = !editing;
            commentsTextBox.ReadOnly = !editing;
            inheritsTextBox.ReadOnly = !editing;
            // xtriggersDataGridView.AllowUserToAddRows = editing;
            // xtriggersDataGridView.AllowUserToDeleteRows = editing;
            // xtriggersDataGridView.ReadOnly = !editing;
            newXTriggerButton.Visible = editing;
            deleteXTriggerButton.Visible = editing;
            aspectsDataGridView.AllowUserToAddRows = editing;
            aspectsDataGridView.AllowUserToDeleteRows = editing;
            aspectsDataGridView.ReadOnly = !editing;
            okButton.Visible = editing;
            addSlotButton.Visible = editing;
            removeSlotButton.Visible = editing;
            extendXTriggerButton.Visible = editing;
            setAsExtendToolStripMenuItem.Visible = editing;
            setAsRemoveToolStripMenuItem.Visible = editing;
            cancelButton.Text = editing ? "Cancel" : "Close";
            deletedCheckBox.Enabled = editing;
        }

        void SetType(ElementType elementType)
        {
            switch (elementType)
            {
                case ElementType.ELEMENT:
                    break;
                case ElementType.GENERATOR:
                    idLabel.ForeColor = Color.Red;
                    labelLabel.ForeColor = Color.Red;
                    descriptionLabel.ForeColor = Color.Red;
                    decayToLabel.ForeColor = Color.Red;
                    aspectsLabel.ForeColor = Color.Red;
                    break;
                case ElementType.ASPECT:
                    break;
            }
        }

        private void FillValues(Element element)
        {
            if (element.id != null) idTextBox.Text = element.id;
            if (element.label != null) labelTextBox.Text = element.label;
            if (element.icon != null)
            {
                iconTextBox.Text = element.icon;
                // if (Utilities.ElementImageExists(element.icon)) pictureBox1.Image = Utilities.GetElementImage(element.icon);
            }
            else if (Utilities.ElementImageExists(element.id))
            {
                pictureBox1.Image = Utilities.GetElementImage(element.id);
            }
            if (element.lifetime.HasValue) lifetimeNumericUpDown.Value = element.lifetime.Value;
            if (element.decayTo != null) decayToTextBox.Text = element.decayTo;
            if (element.unique.HasValue) uniqueCheckBox.Checked = element.unique.Value;
            if (element.resaturate.HasValue) resaturateCheckBox.Checked = element.resaturate.Value;
            if (element.uniquenessgroup != null) uniquenessgroupTextBox.Text = element.uniquenessgroup;
            if (element.description != null) descriptionTextBox.Text = element.description;
            if (element.comments != null) commentsTextBox.Text = element.comments;
            if (element.inherits != null) inheritsTextBox.Text = element.inherits;
            if (element.deleted.HasValue) deletedCheckBox.Checked = element.deleted.Value;
            if (element.slots != null)
            {
                foreach (Slot slot in element.slots)
                {
                    slots[slot.id] = slot;
                    ListViewItem item = new ListViewItem(slot.id);
                    slotsListView.Items.Add(item);
                }
            }
            if (element.slots_prepend != null)
            {
                foreach (Slot slot in element.slots_prepend)
                {
                    slots[slot.id] = slot;
                    ListViewItem item = new ListViewItem(slot.id)
                    {
                        BackColor = Utilities.ListPrependColor
                    };
                    slotsListView.Items.Insert(0, item);
                }
            }
            if (element.slots_append != null)
            {
                foreach (Slot slot in element.slots_append)
                {
                    slots[slot.id] = slot;
                    ListViewItem item = new ListViewItem(slot.id)
                    {
                        BackColor = Utilities.ListAppendColor
                    };
                    slotsListView.Items.Add(item);
                }
            }
            if (element.slots_remove != null)
            {
                foreach (string slot in element.slots_remove)
                {
                    ListViewItem item = new ListViewItem(slot)
                    {
                        BackColor = Utilities.ListRemoveColor
                    };
                    slotsListView.Items.Add(item);
                }
            }
            if (element.xtriggers != null)
            {
                foreach (KeyValuePair<string, List<XTrigger>> kvp in element.xtriggers)
                {
                    ListViewItem item = new ListViewItem(kvp.Key);
                    xtriggersListView.Items.Add(item);
                }
            }
            if (element.xtriggers_extend != null)
            {
                foreach (KeyValuePair<string, List<XTrigger>> kvp in element.xtriggers_extend)
                {
                    ListViewItem item = new ListViewItem(kvp.Key)
                    {
                        BackColor = Utilities.DictionaryExtendStyle.BackColor
                    };
                    xtriggersListView.Items.Add(item);
                }
            }
            if (element.xtriggers_remove != null)
            {
                foreach (string removeId in element.xtriggers_remove)
                {
                    ListViewItem item = new ListViewItem(removeId)
                    {
                        BackColor = Utilities.DictionaryRemoveStyle.BackColor
                    };
                    xtriggersListView.Items.Add(item);
                }
            }
            if (element.aspects != null)
            {
                foreach (KeyValuePair<string, int> kvp in element.aspects)
                {
                    aspectsDataGridView.Rows.Add(kvp.Key, Convert.ToString(kvp.Value));
                }
            }
            if (element.aspects_extend != null)
            {
                foreach (KeyValuePair<string, int> kvp in element.aspects_extend)
                {
                    DataGridViewRow row = new DataGridViewRow
                    {
                        DefaultCellStyle = Utilities.DictionaryExtendStyle
                    };
                    row.CreateCells(aspectsDataGridView, kvp.Key, Convert.ToString(kvp.Value));
                    aspectsDataGridView.Rows.Add(row);
                    //aspectsDataGridView.Rows.Add(kvp.Key, Convert.ToString(kvp.Value));
                }
            }
            if (element.aspects_remove != null)
            {
                foreach (string removeId in element.aspects_remove)
                {
                    DataGridViewRow row = new DataGridViewRow
                    {
                        DefaultCellStyle = Utilities.DictionaryRemoveStyle
                    };
                    row.CreateCells(aspectsDataGridView, removeId );
                    aspectsDataGridView.Rows.Add(row);
                }
            }
            if (element.extends?.Count > 1)
            {
                extendsTextBox.Text = string.Join(",", element.extends);
            }
            else if (element.extends?.Count == 1)
            {
                extendsTextBox.Text = element.extends[0];
            }
        }

        private void SlotsListView_DoubleClick(object sender, EventArgs e)
        {
            if (slotsListView.SelectedItems == null) return;
            string slotId = slotsListView.SelectedItems[0].Text.ToString();
            SlotViewer sv = new SlotViewer(slots[slotId], editing, SlotType.ELEMENT);
            sv.Show();
        }

        private void AspectsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!(aspectsDataGridView.Rows[e.RowIndex].Cells[0].Value is string aspectID))
            {
                return;
            }
            AspectViewer av = new AspectViewer(Utilities.GetAspect(aspectID), null, null);
            av.Show();
        }

        private void XtriggersListView_DoubleClick(object sender, EventArgs e)
        {
            // multiselect is false, so SelectedItems.Count can only ever be 0 or 1
            if (xtriggersListView.SelectedItems.Count == 0) return;
            string id = xtriggersListView.SelectedItems[0].Text;
            Color backColor = xtriggersListView.SelectedItems[0].BackColor;
            XTriggerViewer xtv;
            if (backColor == Utilities.DictionaryExtendStyle.BackColor)
            {
                xtv = new XTriggerViewer(id, displayedElement.xtriggers_extend[id], editing, backColor == Utilities.DictionaryRemoveStyle.BackColor);
                if (xtv.ShowDialog() == DialogResult.OK)
                {
                    // if it returns null, we're deleting it from the xtrigger viewer
                    if (xtv.displayedXTriggers != null)
                    {
                        // otherwise, check to see if the catalyst id has been changed
                        if (xtv.catalyst != xtriggersListView.SelectedItems[0].Text)
                        {
                            // if so, remove the copy under the old name
                            displayedElement.xtriggers_extend.Remove(id);
                            // change the displayed name in the listview
                            xtriggersListView.SelectedItems[0].Text = xtv.catalyst;
                            // and add the xtrigger entry under the new id
                            displayedElement.xtriggers_extend[xtv.catalyst] = xtv.displayedXTriggers.ToList();
                        }
                        else
                        {
                            // otherwise just swap out the entry
                            displayedElement.xtriggers_extend[xtv.catalyst] = xtv.displayedXTriggers.ToList();
                        }
                    }
                    else
                    {
                        // just remove everything, C# will clean up after us
                        displayedElement.xtriggers_extend.Remove(id);
                        xtriggersListView.Items.Remove(xtriggersListView.SelectedItems[0]);
                    }
                }
            }
            else if (backColor == Utilities.DictionaryRemoveStyle.BackColor)
            {
                // There isn't a List<XTrigger> here, so display nothing
                // Until $remove is fixed, this won't even be supported anyways
            }
            else
            {
                xtv = new XTriggerViewer(id, displayedElement.xtriggers[id].ToList(), editing, false);
                if (xtv.ShowDialog() == DialogResult.OK)
                {
                    if (xtv != null)
                    {
                        if (xtv.catalyst != xtriggersListView.SelectedItems[0].Text)
                        {
                            displayedElement.xtriggers.Remove(xtriggersListView.SelectedItems[0].Text);
                            xtriggersListView.SelectedItems[0].Text = xtv.catalyst;
                            displayedElement.xtriggers[xtv.catalyst] = xtv.displayedXTriggers.ToList();
                        }
                        else
                        {
                            displayedElement.xtriggers[xtv.catalyst] = xtv.displayedXTriggers.ToList();
                        }
                    }
                    else
                    {
                        displayedElement.xtriggers_extend.Remove(id);
                        xtriggersListView.Items.Remove(xtriggersListView.SelectedItems[0]);
                    }
                }
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (idTextBox.Text == null || idTextBox.Text == "")
            {
                MessageBox.Show("All Elements must have an ID");
                return;
            }
            if (aspectsDataGridView.Rows.Count > 1)
            {
                displayedElement.aspects = null;
                displayedElement.aspects_extend = null;
                displayedElement.aspects_remove = null;
                foreach (DataGridViewRow row in aspectsDataGridView.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        string key = row.Cells[0].Value.ToString();
                        int? value = row.Cells[1].Value != null ? Convert.ToInt32(row.Cells[1].Value) : (int?)null;
                        if (row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
                        {
                            if (displayedElement.aspects_extend == null) displayedElement.aspects_extend = new Dictionary<string, int>();
                            if (!displayedElement.aspects_extend.ContainsKey(key)) displayedElement.aspects_extend.Add(key, value.Value);
                        }
                        else if (row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
                        {
                            if (displayedElement.aspects_remove == null) displayedElement.aspects_remove = new List<string>();
                            if (!displayedElement.aspects_remove.Contains(key)) displayedElement.aspects_remove.Add(key);
                        }
                        else
                        {
                            if (displayedElement.aspects == null) displayedElement.aspects = new Dictionary<string, int>();
                            if (!displayedElement.aspects.ContainsKey(key)) displayedElement.aspects.Add(key, value.Value);
                        }
                    }
                    //if (row.Cells[0].Value != null && row.Cells[1].Value != null) displayedElement.aspects.Add(row.Cells[0].Value.ToString(), Convert.ToInt32(row.Cells[1].Value));
                }
            }
            Close();
            SuccessCallback?.Invoke(this, displayedElement);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void IdTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedElement.id = idTextBox.Text;
            if (displayedElement.id == "")
            {
                displayedElement.id = null;
            }
        }

        private void LabelTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedElement.label = labelTextBox.Text;
            if (displayedElement.label == "")
            {
                displayedElement.label = null;
            }
        }

        private void IconTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedElement.icon = iconTextBox.Text;
            if (Utilities.ElementImageExists(iconTextBox.Text))
            {
                pictureBox1.Image = Utilities.GetElementImage(iconTextBox.Text);
            }
            if (displayedElement.icon == "")
            {
                displayedElement.icon = null;
            }
        }

        private void UniquenessgroupTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedElement.uniquenessgroup = uniquenessgroupTextBox.Text;
            if (displayedElement.uniquenessgroup == "")
            {
                displayedElement.uniquenessgroup = null;
            }
        }

        private void DecayToTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedElement.decayTo = decayToTextBox.Text;
            if (displayedElement.decayTo == "")
            {
                displayedElement.decayTo = null;
            }
        }

        private void LifetimeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            displayedElement.lifetime = Convert.ToInt32(lifetimeNumericUpDown.Value);
            if (displayedElement.lifetime == 0)
            {
                displayedElement.lifetime = null;
            }
        }

        private void DescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedElement.description = descriptionTextBox.Text;
            if (displayedElement.description == "")
            {
                displayedElement.description = null;
            }
        }

        private void AddSlotButton_Click(object sender, EventArgs e)
        {
            SlotViewer sv = new SlotViewer(new Slot(), true, SlotType.ELEMENT);
            DialogResult dr = sv.ShowDialog();
            if (dr == DialogResult.OK)
            {
                slots.Add(sv.displayedSlot.id, sv.displayedSlot);
                slotsListView.Items.Add(sv.displayedSlot.id);
                if (displayedElement.slots == null) displayedElement.slots = new List<Slot>();
                displayedElement.slots.Add(sv.displayedSlot);
            }
        }

        private void AspectsDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            string key = e.Row.Cells[1].Value != null ? e.Row.Cells[0].Value.ToString() : null;
            if (e.Row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
            {

                if (displayedElement.aspects_extend == null) return;
                if (displayedElement.aspects_extend.ContainsKey(key)) displayedElement.aspects_extend.Remove(key);
                if (displayedElement.aspects_extend.Count == 0) displayedElement.aspects_extend = null;
            }
            else if (e.Row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
            {

                if (displayedElement.aspects_remove == null) return;
                if (displayedElement.aspects_remove.Contains(key)) displayedElement.aspects_remove.Remove(key);
                if (displayedElement.aspects_remove.Count == 0) displayedElement.aspects_remove = null;
            }
            else
            {
                if (displayedElement.aspects == null) return;
                if (displayedElement.aspects.ContainsKey(key)) displayedElement.aspects.Remove(key);
                if (displayedElement.aspects.Count == 0) displayedElement.aspects = null;
            }
        }

        private void XtriggersDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            string key = e.Row.Cells[1].Value != null ? e.Row.Cells[0].Value.ToString() : null;
            if (e.Row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
            {

                if (displayedElement.xtriggers_extend == null) return;
                if (displayedElement.xtriggers_extend.ContainsKey(key)) displayedElement.xtriggers_extend.Remove(key);
                if (displayedElement.xtriggers_extend.Count == 0) displayedElement.xtriggers_extend = null;
            }
            else if (e.Row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
            {

                if (displayedElement.xtriggers_remove == null) return;
                if (displayedElement.xtriggers_remove.Contains(key)) displayedElement.xtriggers_remove.Remove(key);
                if (displayedElement.xtriggers_remove.Count == 0) displayedElement.xtriggers_remove = null;
            }
            else
            {
                if (displayedElement.xtriggers == null) return;
                if (displayedElement.xtriggers.ContainsKey(key)) displayedElement.xtriggers.Remove(key);
                if (displayedElement.xtriggers.Count == 0) displayedElement.xtriggers = null;
            }
        }

        private void NewXTriggerButton_Click(object sender, EventArgs e)
        {
            XTriggerViewer xtv = new XTriggerViewer();
            if (xtv.ShowDialog() == DialogResult.OK)
            {
                if (displayedElement.xtriggers == null) displayedElement.xtriggers = new Dictionary<string, List<XTrigger>>();
                xtriggersListView.Items.Add(xtv.catalyst);
                displayedElement.xtriggers[xtv.catalyst] = xtv.displayedXTriggers;
            }
        }

        private void DeleteXTriggerButton_Click(object sender, EventArgs e)
        {
            if (xtriggersListView.SelectedItems.Count == 0) return;
            ListViewItem item = xtriggersListView.SelectedItems[0];
            string selectedId = item.Text;
            if (item.BackColor == Utilities.DictionaryExtendStyle.BackColor)
            {
                if (displayedElement.xtriggers_extend.ContainsKey(selectedId))
                {
                    displayedElement.xtriggers_extend.Remove(selectedId);
                    xtriggersListView.Items.Remove(item);
                }
                if (displayedElement.xtriggers_extend.Count == 0) displayedElement.xtriggers_extend = null;
            }
            else if (item.BackColor == Utilities.DictionaryRemoveStyle.BackColor)
            {
                if (displayedElement.xtriggers_remove.Contains(selectedId))
                {
                    displayedElement.xtriggers_remove.Remove(selectedId);
                    xtriggersListView.Items.Remove(item);
                }
                if (displayedElement.xtriggers_remove.Count == 0) displayedElement.xtriggers_remove = null;
            }
            else
            {
                if (displayedElement.xtriggers.ContainsKey(selectedId))
                {
                    displayedElement.xtriggers.Remove(selectedId);
                    xtriggersListView.Items.Remove(item);
                }
                if (displayedElement.xtriggers.Count == 0) displayedElement.xtriggers = null;
            }
        }

        private void SetAsExtendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView affectedDataGridView = (DataGridView)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl;
            if (affectedDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow row = affectedDataGridView.SelectedRows[0];
                row.DefaultCellStyle = Utilities.DictionaryExtendStyle;
            }
            else if (affectedDataGridView.SelectedCells.Count > 0)
            {
                DataGridViewRow row = affectedDataGridView.Rows[affectedDataGridView.SelectedCells[0].RowIndex];
                row.DefaultCellStyle = Utilities.DictionaryExtendStyle;
            }
        }

        private void SetAsRemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView affectedDataGridView = (DataGridView)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl;
            if (affectedDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow row = affectedDataGridView.SelectedRows[0];
                row.DefaultCellStyle = Utilities.DictionaryRemoveStyle;
            }
            else if (affectedDataGridView.SelectedCells.Count > 0)
            {
                DataGridViewRow row = affectedDataGridView.Rows[affectedDataGridView.SelectedCells[0].RowIndex];
                row.DefaultCellStyle = Utilities.DictionaryRemoveStyle;
            }
        }

        private void UniqueCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (uniqueCheckBox.CheckState == CheckState.Checked) displayedElement.unique = true;
            if (uniqueCheckBox.CheckState == CheckState.Unchecked) displayedElement.unique = false;
            if (uniqueCheckBox.CheckState == CheckState.Indeterminate) displayedElement.unique = null;
        }

        private void ResaturateCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (resaturateCheckBox.CheckState == CheckState.Checked) displayedElement.resaturate = true;
            if (resaturateCheckBox.CheckState == CheckState.Unchecked) displayedElement.resaturate = false;
            if (resaturateCheckBox.CheckState == CheckState.Indeterminate) displayedElement.resaturate = null;
        }

        private void CommentsTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedElement.comments = commentsTextBox.Text;
            if (displayedElement.comments == "") displayedElement.comments = null;
        }

        private void InheritsTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedElement.inherits = inheritsTextBox.Text;
            if (displayedElement.inherits == "") displayedElement.inherits = null;
        }

        private void DeletedCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (deletedCheckBox.CheckState == CheckState.Checked) displayedElement.deleted = true;
            if (deletedCheckBox.CheckState == CheckState.Unchecked) displayedElement.deleted = false;
            if (deletedCheckBox.CheckState == CheckState.Indeterminate) displayedElement.deleted = null;
        }

        private void RemoveSlotButton_Click(object sender, EventArgs e)
        {
            if (slotsListView.SelectedItems.Count == 1)
            {
                ListViewItem selectedItem = slotsListView.SelectedItems[0];
                if (selectedItem.BackColor == Utilities.ListAppendColor)
                {
                    displayedElement.slots_append.Remove(slots[selectedItem.Text]);
                }
                else if (selectedItem.BackColor == Utilities.ListPrependColor)
                {
                    displayedElement.slots_prepend.Remove(slots[selectedItem.Text]);
                }
                else if (selectedItem.BackColor == Utilities.ListRemoveColor)
                {
                    displayedElement.slots_remove.Remove(selectedItem.Text);
                }
                else
                {
                    displayedElement.slots.Remove(slots[selectedItem.Text]);
                }
                slots.Remove(selectedItem.Text);
                slotsListView.Items.Remove(selectedItem);
            }
            else
            {
                MessageBox.Show("Please select a slot to remove.");
            }
        }

        private void ExtendsTextBox_TextChanged(object sender, EventArgs e)
        {
            if (extendsTextBox.Text.Contains(","))
            {
                displayedElement.extends = extendsTextBox.Text.Split(',').ToList();
            }
            else 
            {
                if (extendsTextBox.Text != "") displayedElement.extends = new List<string> { extendsTextBox.Text };
                else displayedElement.extends = null;
            }
        }

        private void ExtendXTriggerButton_Click(object sender, EventArgs e)
        {
            XTriggerViewer xtv = new XTriggerViewer();
            if (xtv.ShowDialog() == DialogResult.OK)
            {
                if (displayedElement.xtriggers_extend == null) displayedElement.xtriggers_extend = new Dictionary<string, List<XTrigger>>();
                xtriggersListView.Items.Add(new ListViewItem(xtv.catalyst) { BackColor = Utilities.DictionaryExtendStyle.BackColor });
                displayedElement.xtriggers_extend[xtv.catalyst] = xtv.displayedXTriggers;
            }
        }
    }
}
