﻿using CarcassSpark.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CarcassSpark.ObjectViewers
{
    public enum ElementType
    {
        Element,
        Aspect,
        Generator
    }
    public partial class ElementViewer : Form, IGameObjectViewer
    {
        private readonly Dictionary<string, Slot> slots = new Dictionary<string, Slot>();
        public Element DisplayedElement;
        private bool editing;

        private event EventHandler<Element> SuccessCallback;
        public ListViewItem associatedListViewItem;

        public ListViewItem AssociatedListViewItem { get => associatedListViewItem; set => associatedListViewItem=value; }

        public ElementViewer(Element element, EventHandler<Element> successCallback, ListViewItem item)
        {
            InitializeComponent();
            DisplayedElement = element;
            associatedListViewItem = item;
            if (successCallback != null)
            {
                SetEditingMode(true);
                this.SuccessCallback += successCallback;
            }
            else
            {
                SetEditingMode(false);
            }
        }

        public ElementViewer(Element element, EventHandler<Element> successCallback, ElementType elementType, ListViewItem item)
        {
            InitializeComponent();
            DisplayedElement = element;
            associatedListViewItem = item;
            if (successCallback != null)
            {
                SetEditingMode(true);
                this.SuccessCallback += successCallback;
            }
            else
            {
                SetEditingMode(false);
            }
            SetType(elementType);
        }

        private void SetEditingMode(bool editing)
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
            verbIconTextBox.ReadOnly = !editing;
        }

        private void SetType(ElementType elementType)
        {
            switch (elementType)
            {
                case ElementType.Element:
                    break;
                case ElementType.Generator:
                    idLabel.ForeColor = Color.Red;
                    labelLabel.ForeColor = Color.Red;
                    descriptionLabel.ForeColor = Color.Red;
                    decayToLabel.ForeColor = Color.Red;
                    aspectsLabel.ForeColor = Color.Red;
                    break;
                case ElementType.Aspect:
                    break;
            }
        }

        private void FillValues(Element element)
        {
            if (element.ID != null)
            {
                idTextBox.Text = element.ID;
            }

            if (element.label != null)
            {
                labelTextBox.Text = element.label;
            }

            if (element.icon != null)
            {
                iconTextBox.Text = element.icon;
                // if (Utilities.ElementImageExists(element.icon)) pictureBox1.Image = Utilities.GetElementImage(element.icon);
            }
            else if (Utilities.ElementImageExists(element.ID))
            {
                pictureBox1.Image = Utilities.GetElementImage(element.ID);
            }
            if (element.lifetime.HasValue)
            {
                lifetimeNumericUpDown.Value = element.lifetime.Value;
            }

            if (element.decayTo != null)
            {
                decayToTextBox.Text = element.decayTo;
            }

            if (element.unique.HasValue)
            {
                uniqueCheckBox.Checked = element.unique.Value;
            }

            if (element.resaturate.HasValue)
            {
                resaturateCheckBox.Checked = element.resaturate.Value;
            }

            if (element.uniquenessgroup != null)
            {
                uniquenessgroupTextBox.Text = element.uniquenessgroup;
            }

            if (element.description != null)
            {
                descriptionTextBox.Text = element.description;
            }

            if (element.comments != null)
            {
                commentsTextBox.Text = element.comments;
            }

            if (element.inherits != null)
            {
                inheritsTextBox.Text = element.inherits;
            }

            if (element.deleted.HasValue)
            {
                deletedCheckBox.Checked = element.deleted.Value;
            }

            if (element.verbicon != null)
            {
                verbIconTextBox.Text = element.verbicon;
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
                    slotsListView.Items.Add(item);
                }
            }
            if (element.slots != null)
            {
                foreach (Slot slot in element.slots)
                {
                    slots[slot.id] = slot;
                    ListViewItem item = new ListViewItem(slot.id);
                    slotsListView.Items.Add(item);
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
                    row.CreateCells(aspectsDataGridView, removeId);
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
            if (slotsListView.SelectedItems.Count == 0)
            {
                return;
            }

            string slotId = slotsListView.SelectedItems[0].Text;
            SlotViewer sv = new SlotViewer(slots[slotId], editing, SlotType.Element);
            sv.Show();
        }

        private void AspectsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!(aspectsDataGridView.Rows[e.RowIndex].Cells[0].Value is string aspectId))
            {
                return;
            }
            AspectViewer av = new AspectViewer(Utilities.GetAspect(aspectId), null, null);
            av.Show();
        }

        private void XtriggersListView_DoubleClick(object sender, EventArgs e)
        {
            // multiselect is false, so SelectedItems.Count can only ever be 0 or 1
            if (xtriggersListView.SelectedItems.Count == 0)
            {
                return;
            }

            string id = xtriggersListView.SelectedItems[0].Text;
            Color backColor = xtriggersListView.SelectedItems[0].BackColor;
            XTriggerViewer xtv;
            if (backColor == Utilities.DictionaryExtendStyle.BackColor)
            {
                xtv = new XTriggerViewer(id, DisplayedElement.xtriggers_extend[id], editing, backColor == Utilities.DictionaryRemoveStyle.BackColor);
                if (xtv.ShowDialog() == DialogResult.OK)
                {
                    // if it returns null, we're deleting it from the xtrigger viewer
                    if (xtv.DisplayedXTriggers != null)
                    {
                        // otherwise, check to see if the catalyst id has been changed
                        if (xtv.Catalyst != xtriggersListView.SelectedItems[0].Text)
                        {
                            // if so, remove the copy under the old name
                            DisplayedElement.xtriggers_extend.Remove(id);
                            // change the displayed name in the listview
                            xtriggersListView.SelectedItems[0].Text = xtv.Catalyst;
                            // and add the xtrigger entry under the new id
                            DisplayedElement.xtriggers_extend[xtv.Catalyst] = xtv.DisplayedXTriggers.ToList();
                        }
                        else
                        {
                            // otherwise just swap out the entry
                            DisplayedElement.xtriggers_extend[xtv.Catalyst] = xtv.DisplayedXTriggers.ToList();
                        }
                    }
                    else
                    {
                        // just remove everything, C# will clean up after us
                        DisplayedElement.xtriggers_extend.Remove(id);
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
                xtv = new XTriggerViewer(id, DisplayedElement.xtriggers[id].ToList(), editing, false);
                if (xtv.ShowDialog() == DialogResult.OK)
                {
                    if (xtv.DisplayedXTriggers != null)
                    {
                        if (xtv.Catalyst != xtriggersListView.SelectedItems[0].Text)
                        {
                            DisplayedElement.xtriggers.Remove(xtriggersListView.SelectedItems[0].Text);
                            xtriggersListView.SelectedItems[0].Text = xtv.Catalyst;
                            DisplayedElement.xtriggers[xtv.Catalyst] = xtv.DisplayedXTriggers.ToList();
                        }
                        else
                        {
                            DisplayedElement.xtriggers[xtv.Catalyst] = xtv.DisplayedXTriggers.ToList();
                        }
                    }
                    else
                    {
                        DisplayedElement.xtriggers_extend.Remove(id);
                        xtriggersListView.Items.Remove(xtriggersListView.SelectedItems[0]);
                    }
                }
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(idTextBox.Text))
            {
                MessageBox.Show("All Elements must have an ID");
                return;
            }
            if (aspectsDataGridView.Rows.Count > 1)
            {
                DisplayedElement.aspects = null;
                DisplayedElement.aspects_extend = null;
                DisplayedElement.aspects_remove = null;
                foreach (DataGridViewRow row in aspectsDataGridView.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        string key = row.Cells[0].Value.ToString();
                        int? value = row.Cells[1].Value != null ? Convert.ToInt32(row.Cells[1].Value) : (int?)null;
                        if (Equals(row.DefaultCellStyle, Utilities.DictionaryExtendStyle))
                        {
                            if (DisplayedElement.aspects_extend == null)
                            {
                                DisplayedElement.aspects_extend = new Dictionary<string, int>();
                            }

                            if (!DisplayedElement.aspects_extend.ContainsKey(key))
                            {
                                DisplayedElement.aspects_extend.Add(key, value.Value);
                            }
                        }
                        else if (Equals(row.DefaultCellStyle, Utilities.DictionaryRemoveStyle))
                        {
                            if (DisplayedElement.aspects_remove == null)
                            {
                                DisplayedElement.aspects_remove = new List<string>();
                            }

                            if (!DisplayedElement.aspects_remove.Contains(key))
                            {
                                DisplayedElement.aspects_remove.Add(key);
                            }
                        }
                        else
                        {
                            if (DisplayedElement.aspects == null)
                            {
                                DisplayedElement.aspects = new Dictionary<string, int>();
                            }

                            if (!DisplayedElement.aspects.ContainsKey(key))
                            {
                                DisplayedElement.aspects.Add(key, value.Value);
                            }
                        }
                    }
                    //if (row.Cells[0].Value != null && row.Cells[1].Value != null) displayedElement.aspects.Add(row.Cells[0].Value.ToString(), Convert.ToInt32(row.Cells[1].Value));
                }
            }
            Close();
            SuccessCallback?.Invoke(this, DisplayedElement);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void IdTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedElement.ID = idTextBox.Text;
            if (DisplayedElement.ID == "")
            {
                DisplayedElement.ID = null;
            }
        }

        private void LabelTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedElement.label = labelTextBox.Text;
            if (DisplayedElement.label == "")
            {
                DisplayedElement.label = null;
            }
        }

        private void IconTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedElement.icon = iconTextBox.Text;
            if (Utilities.ElementImageExists(iconTextBox.Text))
            {
                pictureBox1.Image = Utilities.GetElementImage(iconTextBox.Text);
            }
            if (DisplayedElement.icon == "")
            {
                DisplayedElement.icon = null;
            }
        }

        private void UniquenessgroupTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedElement.uniquenessgroup = uniquenessgroupTextBox.Text;
            if (DisplayedElement.uniquenessgroup == "")
            {
                DisplayedElement.uniquenessgroup = null;
            }
        }

        private void DecayToTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedElement.decayTo = decayToTextBox.Text;
            if (DisplayedElement.decayTo == "")
            {
                DisplayedElement.decayTo = null;
            }
        }

        private void LifetimeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            DisplayedElement.lifetime = Convert.ToInt32(lifetimeNumericUpDown.Value);
            if (DisplayedElement.lifetime == 0)
            {
                DisplayedElement.lifetime = null;
            }
        }

        private void DescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedElement.description = descriptionTextBox.Text;
            if (DisplayedElement.description == "")
            {
                DisplayedElement.description = null;
            }
        }

        private void AddSlotButton_Click(object sender, EventArgs e)
        {
            SlotViewer sv = new SlotViewer(new Slot(), true, SlotType.Element);
            DialogResult dr = sv.ShowDialog();
            if (dr == DialogResult.OK)
            {
                slots.Add(sv.DisplayedSlot.id, sv.DisplayedSlot);
                slotsListView.Items.Add(sv.DisplayedSlot.id);
                if (DisplayedElement.slots == null)
                {
                    DisplayedElement.slots = new List<Slot>();
                }

                DisplayedElement.slots.Add(sv.DisplayedSlot);
            }
        }

        private void AspectsDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            string key = e.Row.Cells[1].Value != null ? e.Row.Cells[0].Value.ToString() : null;
            if (Equals(e.Row.DefaultCellStyle, Utilities.DictionaryExtendStyle))
            {

                if (DisplayedElement.aspects_extend == null)
                {
                    return;
                }

                if (DisplayedElement.aspects_extend.ContainsKey(key))
                {
                    DisplayedElement.aspects_extend.Remove(key);
                }

                if (DisplayedElement.aspects_extend.Count == 0)
                {
                    DisplayedElement.aspects_extend = null;
                }
            }
            else if (Equals(e.Row.DefaultCellStyle, Utilities.DictionaryRemoveStyle))
            {

                if (DisplayedElement.aspects_remove == null)
                {
                    return;
                }

                if (DisplayedElement.aspects_remove.Contains(key))
                {
                    DisplayedElement.aspects_remove.Remove(key);
                }

                if (DisplayedElement.aspects_remove.Count == 0)
                {
                    DisplayedElement.aspects_remove = null;
                }
            }
            else
            {
                if (DisplayedElement.aspects == null)
                {
                    return;
                }

                if (DisplayedElement.aspects.ContainsKey(key))
                {
                    DisplayedElement.aspects.Remove(key);
                }

                if (DisplayedElement.aspects.Count == 0)
                {
                    DisplayedElement.aspects = null;
                }
            }
        }

        private void NewXTriggerButton_Click(object sender, EventArgs e)
        {
            XTriggerViewer xtv = new XTriggerViewer();
            if (xtv.ShowDialog() == DialogResult.OK)
            {
                if (DisplayedElement.xtriggers == null)
                {
                    DisplayedElement.xtriggers = new Dictionary<string, List<XTrigger>>();
                }

                xtriggersListView.Items.Add(xtv.Catalyst);
                DisplayedElement.xtriggers[xtv.Catalyst] = xtv.DisplayedXTriggers;
            }
        }

        private void DeleteXTriggerButton_Click(object sender, EventArgs e)
        {
            if (xtriggersListView.SelectedItems.Count == 0)
            {
                return;
            }

            ListViewItem item = xtriggersListView.SelectedItems[0];
            string selectedId = item.Text;
            if (item.BackColor == Utilities.DictionaryExtendStyle.BackColor)
            {
                if (DisplayedElement.xtriggers_extend.ContainsKey(selectedId))
                {
                    DisplayedElement.xtriggers_extend.Remove(selectedId);
                    xtriggersListView.Items.Remove(item);
                }
                if (DisplayedElement.xtriggers_extend.Count == 0)
                {
                    DisplayedElement.xtriggers_extend = null;
                }
            }
            else if (item.BackColor == Utilities.DictionaryRemoveStyle.BackColor)
            {
                if (DisplayedElement.xtriggers_remove.Contains(selectedId))
                {
                    DisplayedElement.xtriggers_remove.Remove(selectedId);
                    xtriggersListView.Items.Remove(item);
                }
                if (DisplayedElement.xtriggers_remove.Count == 0)
                {
                    DisplayedElement.xtriggers_remove = null;
                }
            }
            else
            {
                if (DisplayedElement.xtriggers.ContainsKey(selectedId))
                {
                    DisplayedElement.xtriggers.Remove(selectedId);
                    xtriggersListView.Items.Remove(item);
                }
                if (DisplayedElement.xtriggers.Count == 0)
                {
                    DisplayedElement.xtriggers = null;
                }
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
            if (uniqueCheckBox.CheckState == CheckState.Checked)
            {
                DisplayedElement.unique = true;
            }

            if (uniqueCheckBox.CheckState == CheckState.Unchecked)
            {
                DisplayedElement.unique = false;
            }

            if (uniqueCheckBox.CheckState == CheckState.Indeterminate)
            {
                DisplayedElement.unique = null;
            }
        }

        private void ResaturateCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (resaturateCheckBox.CheckState == CheckState.Checked)
            {
                DisplayedElement.resaturate = true;
            }

            if (resaturateCheckBox.CheckState == CheckState.Unchecked)
            {
                DisplayedElement.resaturate = false;
            }

            if (resaturateCheckBox.CheckState == CheckState.Indeterminate)
            {
                DisplayedElement.resaturate = null;
            }
        }

        private void CommentsTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedElement.comments = commentsTextBox.Text;
            if (DisplayedElement.comments == "")
            {
                DisplayedElement.comments = null;
            }
        }

        private void InheritsTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedElement.inherits = inheritsTextBox.Text;
            if (DisplayedElement.inherits == "")
            {
                DisplayedElement.inherits = null;
            }
        }

        private void DeletedCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (deletedCheckBox.CheckState == CheckState.Checked)
            {
                DisplayedElement.deleted = true;
            }

            if (deletedCheckBox.CheckState == CheckState.Unchecked)
            {
                DisplayedElement.deleted = false;
            }

            if (deletedCheckBox.CheckState == CheckState.Indeterminate)
            {
                DisplayedElement.deleted = null;
            }
        }

        private void RemoveSlotButton_Click(object sender, EventArgs e)
        {
            if (slotsListView.SelectedItems.Count == 1)
            {
                ListViewItem selectedItem = slotsListView.SelectedItems[0];
                if (Equals(selectedItem.BackColor, Utilities.ListAppendColor))
                {
                    DisplayedElement.slots_append.Remove(slots[selectedItem.Text]);
                }
                else if (Equals(selectedItem.BackColor, Utilities.ListPrependColor))
                {
                    DisplayedElement.slots_prepend.Remove(slots[selectedItem.Text]);
                }
                else if (Equals(selectedItem.BackColor, Utilities.ListRemoveColor))
                {
                    DisplayedElement.slots_remove.Remove(selectedItem.Text);
                }
                else
                {
                    DisplayedElement.slots.Remove(slots[selectedItem.Text]);
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
                DisplayedElement.extends = extendsTextBox.Text.Split(',').ToList();
            }
            else
            {
                DisplayedElement.extends = extendsTextBox.Text != "" ? new List<string> { extendsTextBox.Text } : null;
            }
        }

        private void ExtendXTriggerButton_Click(object sender, EventArgs e)
        {
            XTriggerViewer xtv = new XTriggerViewer();
            if (xtv.ShowDialog() == DialogResult.OK)
            {
                if (DisplayedElement.xtriggers_extend == null)
                {
                    DisplayedElement.xtriggers_extend = new Dictionary<string, List<XTrigger>>();
                }

                xtriggersListView.Items.Add(new ListViewItem(xtv.Catalyst) { BackColor = Utilities.DictionaryExtendStyle.BackColor });
                DisplayedElement.xtriggers_extend[xtv.Catalyst] = xtv.DisplayedXTriggers;
            }
        }

        private void VerbIconTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedElement.verbicon = !string.IsNullOrEmpty(verbIconTextBox.Text) ? verbIconTextBox.Text : null;
        }

        private void ElementViewer_Shown(object sender, EventArgs e)
        {
            FillValues(DisplayedElement);
        }
    }
}
