﻿using System;
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
    public partial class ElementViewer : Form
    {

        Dictionary<string, Slot> slots = new Dictionary<string, Slot>();
        public Element displayedElement;
        bool editing;

        public ElementViewer(Element element, bool? editing)
        {
            InitializeComponent();
            displayedElement = element;
            fillValues(element);
            if (editing.HasValue)
            {
                setEditingMode(editing.Value);
            }
            else
            {
                setEditingMode(false);
            }
        }

        void setEditingMode(bool editing)
        {
            this.editing = editing;
            idTextBox.ReadOnly = !editing;
            labelTextBox.ReadOnly = !editing;
            iconTextBox.ReadOnly = !editing;
            animFramesNumericUpDown.Enabled = editing;
            lifetimeNumericUpDown.Enabled = editing;
            decayToTextBox.ReadOnly = !editing;
            uniqueCheckBox.Enabled = editing;
            resaturateCheckBox.Enabled = editing;
            uniquenessgroupTextBox.ReadOnly = !editing;
            descriptionTextBox.ReadOnly = !editing;
            extendsTextBox.ReadOnly = !editing;
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
            cancelButton.Text = editing ? "Cancel" : "Close";
        }

        private void fillValues(Element element)
        {
            if (element.extends != null)
            {
                extendsTextBox.Text = element.extends[0]; // afaik extends should only ever be an array of a single string
                //Element extendedElement = Utilities.getElement(element.extends[0]);
                //fillValues(extendedElement);
            }
            if (element.id != null)
            {
                idTextBox.Text = element.id;
                pictureBox1.Image = Utilities.getElementImage(element.id);
            }
            if (element.label != null) labelTextBox.Text = element.label;
            if (element.icon != null)
            {
                // iconTextBox.Text = element.icon;
                pictureBox1.Image = Utilities.getElementImage(element.icon);
            }
            if (element.animframes.HasValue) animFramesNumericUpDown.Value = element.animframes.Value;
            if (element.lifetime.HasValue) lifetimeNumericUpDown.Value = element.lifetime.Value;
            if (element.decayTo != null) decayToTextBox.Text = element.decayTo;
            if (element.unique.HasValue) uniqueCheckBox.Checked = element.unique.Value;
            if (element.uniquenessgroup != null) uniquenessgroupTextBox.Text = element.uniquenessgroup;
            if (element.description != null) descriptionTextBox.Text = element.description;
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
                    ListViewItem item = new ListViewItem(slot.id);
                    item.BackColor = Utilities.ListPrependColor;
                    slotsListView.Items.Insert(0, item);
                }
            }
            if (element.slots_append != null)
            {
                foreach (Slot slot in element.slots_append)
                {
                    slots[slot.id] = slot;
                    ListViewItem item = new ListViewItem(slot.id);
                    item.BackColor = Utilities.ListAppendColor;
                    slotsListView.Items.Add(item);
                }
            }
            if (element.slots_remove != null)
            {
                foreach (string slot in element.slots_remove)
                {
                    ListViewItem item = new ListViewItem(slot);
                    item.BackColor = Utilities.ListRemoveColor;
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
                    ListViewItem item = new ListViewItem(kvp.Key);
                    item.BackColor = Utilities.DictionaryExtendStyle.BackColor;
                    xtriggersListView.Items.Add(item);
                }
            }
            if (element.xtriggers_remove != null)
            {
                foreach (string removeId in element.xtriggers_remove)
                {
                    ListViewItem item = new ListViewItem(removeId);
                    item.BackColor = Utilities.DictionaryRemoveStyle.BackColor;
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
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(aspectsDataGridView, kvp.Key, Convert.ToString(kvp.Value));
                    row.DefaultCellStyle = Utilities.DictionaryExtendStyle;
                    aspectsDataGridView.Rows.Add(row);
                    //aspectsDataGridView.Rows.Add(kvp.Key, Convert.ToString(kvp.Value));
                }
            }
            if (element.aspects_remove != null)
            {
                foreach (string removeId in element.aspects_remove)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(aspectsDataGridView, removeId );
                    row.DefaultCellStyle = Utilities.DictionaryRemoveStyle;
                    aspectsDataGridView.Rows.Add(row);
                }
            }
        }

        private void slotsListView_DoubleClick(object sender, EventArgs e)
        {
            if (slotsListView.SelectedItems == null) return;
            string slotId = slotsListView.SelectedItems[0].Text.ToString();
            SlotViewer sv = new SlotViewer(slots[slotId], editing, SlotViewer.SlotType.ELEMENT);
            sv.ShowDialog();
        }

        private void aspectsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string aspectID = aspectsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            AspectViewer av = new AspectViewer(Utilities.getAspect(aspectID), editing);
            av.ShowDialog();
        }

        private void xtriggersListView_DoubleClick(object sender, EventArgs e)
        {
            if (xtriggersListView.SelectedItems.Count != 1) return;
            string id = xtriggersListView.SelectedItems[0].Text;
            List<XTrigger> xTriggers = new List<XTrigger>();
            if (xtriggersListView.SelectedItems[0].BackColor == Utilities.DictionaryExtendStyle.BackColor)
            {
                xTriggers = displayedElement.xtriggers_extend[id];
            }
            else if (xtriggersListView.SelectedItems[0].BackColor == Utilities.DictionaryRemoveStyle.BackColor)
            {
                // There isn't a List<XTrigger> here, so display nothing
            }
            else
            {
                xTriggers = displayedElement.xtriggers[id];
            }
            XTriggerViewer xtv = new XTriggerViewer(id, xTriggers, editing, xtriggersListView.SelectedItems[0].BackColor == Utilities.DictionaryRemoveStyle.BackColor);
            if (xtv.ShowDialog() == DialogResult.OK)
            {
                if (xtv.catalyst != xtriggersListView.SelectedItems[0].Text)
                {
                    displayedElement.xtriggers.Remove(xtriggersListView.SelectedItems[0].Text);
                    xtriggersListView.SelectedItems[0].Text = xtv.catalyst;
                    displayedElement.xtriggers[xtv.catalyst] = xtv.displayedXTriggers;
                }
                else
                {
                    displayedElement.xtriggers[xtv.catalyst] = xtv.displayedXTriggers;
                }
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (idTextBox.Text == null || idTextBox.Text == "")
            {
                MessageBox.Show("All Elements must have an ID");
                return;
            }
            // if (xtriggersDataGridView.Rows.Count > 1) {
            // displayedElement.xtriggers = new Dictionary<string, Element.XTrigger>();
            // foreach (ListViewItem item in xtriggersListView.Items)
            // {
            // if (item.Text != null)
            // {
            // if (item.BackColor == Utilities.DictionaryExtendStyle.BackColor)
            // {
            // if (displayedElement.aspects_extend == null) displayedElement.xtriggers_extend = new Dictionary<string, Element.XTrigger>();
            // displayedElement.aspects_extend.Add(item.Text, Convert.ToInt32(item.Cells[1].Value));
            // }
            // else if (item.BackColor == Utilities.DictionaryRemoveStyle.BackColor)
            // {
            // if (displayedElement.xtriggers_remove == null) displayedElement.xtriggers_remove = new List<string>();
            // displayedElement.xtriggers_remove.Add(item.Text.ToString());
            // }
            // else
            // {
            // if (displayedElement.xtriggers == null) displayedElement.xtriggers = new Dictionary<string, Element.XTrigger>();
            // displayedElement.xtriggers.Add(item.Text, item.Cells[1].Value.ToString());
            // }
            // }
            // }
            // }
            if (aspectsDataGridView.Rows.Count > 1)
            {
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
            DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void idTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedElement.id = idTextBox.Text;
        }

        private void extendsTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedElement.extends = new List<string> { extendsTextBox.Text };
        }

        private void labelTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedElement.label = labelTextBox.Text;
        }

        private void iconTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedElement.icon = iconTextBox.Text;
            if (Utilities.getElementImage(iconTextBox.Text) != null)
            {
                pictureBox1.Image = Utilities.getElementImage(iconTextBox.Text);
            }
        }

        private void uniquenessgroupTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedElement.uniquenessgroup = uniquenessgroupTextBox.Text;
        }

        private void decayToTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedElement.decayTo = decayToTextBox.Text;
        }

        private void lifetimeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            displayedElement.lifetime = Convert.ToInt32(lifetimeNumericUpDown.Value);
        }

        private void animFramesNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            displayedElement.animframes = Convert.ToInt32(animFramesNumericUpDown.Value);
        }

        private void descriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedElement.description = descriptionTextBox.Text;
        }

        private void addSlotButton_Click(object sender, EventArgs e)
        {
            SlotViewer sv = new SlotViewer(new Slot(), true, SlotViewer.SlotType.ELEMENT);
            DialogResult dr = sv.ShowDialog();
            if (dr == DialogResult.OK)
            {
                slots.Add(sv.displayedSlot.id, sv.displayedSlot);
                slotsListView.Items.Add(sv.displayedSlot.id);
                if (displayedElement.slots == null) displayedElement.slots = new List<Slot>();
                displayedElement.slots.Add(sv.displayedSlot);
            }
        }

        private void resaturateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            displayedElement.resaturate = resaturateCheckBox.Checked;
        }

        private void uniqueCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            displayedElement.unique = uniqueCheckBox.Checked;
        }

        private void aspectsDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
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

        private void xtriggersDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
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

        private void newXTriggerButton_Click(object sender, EventArgs e)
        {
            XTriggerViewer xtv = new XTriggerViewer();
            if (xtv.ShowDialog() == DialogResult.OK)
            {
                if (displayedElement.xtriggers == null) displayedElement.xtriggers = new Dictionary<string, List<XTrigger>>();
                xtriggersListView.Items.Add(xtv.catalyst);
                displayedElement.xtriggers[xtv.catalyst] = xtv.displayedXTriggers;
            }
        }

        private void deleteXTriggerButton_Click(object sender, EventArgs e)
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
    }
}
