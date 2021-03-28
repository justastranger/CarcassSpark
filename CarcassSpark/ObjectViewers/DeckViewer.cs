using CarcassSpark.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CarcassSpark.ObjectViewers
{
    public partial class DeckViewer : Form, IGameObjectViewer
    {
        public Deck DisplayedDeck;
        private bool editing;

        private event EventHandler<Deck> SuccessCallback;

        private bool internalDeck;
        public ListViewItem associatedListViewItem;

        public ListViewItem AssociatedListViewItem { get => associatedListViewItem; set => associatedListViewItem=value; }

        public DeckViewer(Deck deck)
        {
            InitializeComponent();
            DisplayedDeck = deck;
            SetEditingMode(false);
            SetInternal(false);
        }

        public DeckViewer(Deck deck, EventHandler<Deck> successCallback, ListViewItem item)
        {
            InitializeComponent();
            DisplayedDeck = deck;
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

            SetInternal(false);
        }

        public DeckViewer(Deck deck, EventHandler<Deck> successCallback, bool? internalDeck, ListViewItem item)
        {
            InitializeComponent();
            DisplayedDeck = deck;
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

            SetInternal(internalDeck.HasValue && internalDeck.Value);
        }

        private void SetInternal(bool internalDeck)
        {
            idTextBox.Visible = !internalDeck;
            idLabel.Visible = !internalDeck;
            drawsNumericUpDown.Visible = internalDeck;
            drawsLabel.Visible = internalDeck;
            drawmessagesDataGridView.Visible = !internalDeck;
            drawmessagesLlabel.Visible = !internalDeck;
            this.internalDeck = internalDeck;
        }

        private void FillValues(Deck deck)
        {
            if (deck.ID != null)
            {
                idTextBox.Text = deck.ID;
            }

            if (deck.label != null)
            {
                labelTextBox.Text = deck.label;
            }

            if (deck.comments != null)
            {
                commentsTextBox.Text = deck.comments;
            }

            if (deck.description != null)
            {
                descriptionTextBox.Text = deck.description;
            }

            if (deck.resetonexhaustion.HasValue)
            {
                resetOnExhaustionCheckBox.Checked = deck.resetonexhaustion.Value;
            }

            if (deck.defaultcard != null)
            {
                defaultCardTextBox.Text = deck.defaultcard;
            }

            if (deck.draws.HasValue)
            {
                drawsNumericUpDown.Value = deck.draws.Value;
            }

            if (deck.deleted.HasValue)
            {
                deletedCheckBox.Checked = deck.deleted.Value;
            }

            if (deck.spec_prepend != null)
            {
                foreach (string id in deck.spec_prepend)
                {
                    ListViewItem lvi = new ListViewItem(id)
                    {
                        BackColor = Utilities.ListPrependColor
                    };
                    specListView.Items.Add(lvi);
                }
            }
            if (deck.spec != null)
            {
                foreach (string id in deck.spec)
                {
                    ListViewItem lvi = new ListViewItem(id);
                    specListView.Items.Add(lvi);
                }
            }
            if (deck.spec_append != null)
            {
                foreach (string id in deck.spec_append)
                {
                    ListViewItem lvi = new ListViewItem(id)
                    {
                        BackColor = Utilities.ListAppendColor
                    };
                    specListView.Items.Add(lvi);
                }
            }
            if (deck.spec_remove != null)
            {
                foreach (string id in deck.spec_remove)
                {
                    ListViewItem lvi = new ListViewItem(id)
                    {
                        BackColor = Utilities.ListRemoveColor
                    };
                    specListView.Items.Add(lvi);
                }
            }
            if (deck.drawmessages != null)
            {
                foreach (KeyValuePair<string, string> kvp in deck.drawmessages)
                {
                    drawmessagesDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (deck.drawmessages_extend != null)
            {
                foreach (KeyValuePair<string, string> kvp in deck.drawmessages_extend)
                {
                    DataGridViewRow row = new DataGridViewRow
                    {
                        DefaultCellStyle = Utilities.DictionaryExtendStyle
                    };
                    row.CreateCells(drawmessagesDataGridView, kvp.Key, kvp.Value);
                    drawmessagesDataGridView.Rows.Add(row);
                }
            }
            if (deck.drawmessages_remove != null)
            {
                foreach (string removeId in deck.drawmessages_remove)
                {
                    DataGridViewRow row = new DataGridViewRow
                    {
                        DefaultCellStyle = Utilities.DictionaryRemoveStyle
                    };
                    row.CreateCells(drawmessagesDataGridView, removeId);
                    drawmessagesDataGridView.Rows.Add(row);
                }
            }
            if (deck.extends?.Count > 1)
            {
                extendsTextBox.Text = string.Join(",", deck.extends);
            }
            else if (deck.extends?.Count == 1)
            {
                extendsTextBox.Text = deck.extends[0];
            }
        }

        private void SetEditingMode(bool editing)
        {
            this.editing = editing;
            idTextBox.ReadOnly = !editing;
            labelTextBox.ReadOnly = !editing;
            commentsTextBox.ReadOnly = !editing;
            descriptionTextBox.ReadOnly = !editing;
            commentsTextBox.ReadOnly = !editing;
            resetOnExhaustionCheckBox.Enabled = editing;
            defaultCardTextBox.ReadOnly = !editing;
            drawsNumericUpDown.Enabled = editing;
            drawmessagesDataGridView.ReadOnly = !editing;
            drawmessagesDataGridView.AllowUserToAddRows = editing;
            drawmessagesDataGridView.AllowUserToDeleteRows = editing;
            newCardTextBox.Visible = editing;
            newCardButton.Visible = editing;
            specAppendButton.Visible = editing;
            specPrependButton.Visible = editing;
            specRemoveButton.Visible = editing;
            deleteButton.Visible = editing;
            okButton.Visible = editing;
            cancelButton.Text = editing ? "Cancel" : "Close";
            deletedCheckBox.Enabled = editing;
        }

        private void NewCardButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(newCardTextBox.Text))
            {
                specListView.Items.Add(newCardTextBox.Text);
                if (DisplayedDeck.spec == null)
                {
                    DisplayedDeck.spec = new List<string>();
                }
                DisplayedDeck.spec.Add(newCardTextBox.Text);
                newCardTextBox.Text = "";
                newCardTextBox.Focus();
            }
            else if (specListView.SelectedItems.Count > 0)
            {
                ListViewItem item = specListView.SelectedItems[0];
                if (DisplayedDeck.spec == null)
                {
                    DisplayedDeck.spec = new List<string>();
                }
                DeleteItemFromCurrentGroup(item);
                item.BackColor = new Color();
                DisplayedDeck.spec.Add(item.Text);
                newCardTextBox.Text = "";
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!internalDeck && string.IsNullOrEmpty(idTextBox.Text))
            {
                MessageBox.Show("All Standalone Decks must have an ID");
                return;
            }
            if (drawmessagesDataGridView.Rows.Count > 1)
            {
                DisplayedDeck.drawmessages = new Dictionary<string, string>();
                foreach (DataGridViewRow row in drawmessagesDataGridView.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                    {
                        if (Equals(row.DefaultCellStyle, Utilities.DictionaryExtendStyle))
                        {
                            DisplayedDeck.drawmessages.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString());
                        }
                        else if (Equals(row.DefaultCellStyle, Utilities.DictionaryRemoveStyle))
                        {
                            DisplayedDeck.drawmessages.Remove(row.Cells[0].Value.ToString());
                        }
                        else
                        {
                            DisplayedDeck.drawmessages.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString());
                        }
                    }
                }
            }
            Close();
            SuccessCallback?.Invoke(this, DisplayedDeck);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void IdTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedDeck.ID = idTextBox.Text;
            if (DisplayedDeck.ID == "")
            {
                DisplayedDeck.ID = null;
            }
        }

        private void LabelTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedDeck.label = labelTextBox.Text;
            if (DisplayedDeck.label == "")
            {
                DisplayedDeck.label = null;
            }
        }

        private void DescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedDeck.description = descriptionTextBox.Text;
            if (DisplayedDeck.description == "")
            {
                DisplayedDeck.description = null;
            }
        }

        private void CommentsTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedDeck.comments = commentsTextBox.Text;
            if (DisplayedDeck.comments == "")
            {
                DisplayedDeck.comments = null;
            }
        }

        private void NewCardTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(newCardTextBox.Text))
                {
                    specListView.Items.Add(newCardTextBox.Text);
                    if (DisplayedDeck.spec == null)
                    {
                        DisplayedDeck.spec = new List<string>();
                    }

                    DisplayedDeck.spec.Add(newCardTextBox.Text);
                    newCardTextBox.Text = "";
                    newCardTextBox.Focus();
                }
            }
        }

        private void DefaultCardTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedDeck.defaultcard = defaultCardTextBox.Text;
            if (DisplayedDeck.defaultcard == "")
            {
                DisplayedDeck.defaultcard = null;
            }
        }

        private void SpecListView_DoubleClick(object sender, EventArgs e)
        {
            if (specListView.SelectedItems.Count == 0)
            {
                return;
            }

            string id = specListView.SelectedItems[0].Text;
            if (id.Contains("deck:") && Utilities.DeckExists(id.Substring(id.IndexOf(":"))))
            {
                DeckViewer dv = new DeckViewer(Utilities.GetDeck(id.Substring(id.IndexOf(":"))), null, null);
                dv.Show();
            }
            else if (Utilities.ElementExists(id))
            {
                ElementViewer ev = new ElementViewer(Utilities.GetElement(id), null, null);
                ev.Show();
            }
        }

        private void DrawmessagesDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            string key = e.Row.Cells[1].Value != null ? e.Row.Cells[0].Value.ToString() : null;
            if (Equals(e.Row.DefaultCellStyle, Utilities.DictionaryExtendStyle))
            {

                if (DisplayedDeck.drawmessages_extend == null)
                {
                    return;
                }

                if (DisplayedDeck.drawmessages_extend.ContainsKey(key))
                {
                    DisplayedDeck.drawmessages_extend.Remove(key);
                }

                if (DisplayedDeck.drawmessages_extend.Count == 0)
                {
                    DisplayedDeck.drawmessages_extend = null;
                }
            }
            else if (Equals(e.Row.DefaultCellStyle, Utilities.DictionaryRemoveStyle))
            {

                if (DisplayedDeck.drawmessages_remove == null)
                {
                    return;
                }

                if (DisplayedDeck.drawmessages_remove.Contains(key))
                {
                    DisplayedDeck.drawmessages_remove.Remove(key);
                }

                if (DisplayedDeck.drawmessages_remove.Count == 0)
                {
                    DisplayedDeck.drawmessages_remove = null;
                }
            }
            else
            {
                if (DisplayedDeck.drawmessages == null)
                {
                    return;
                }

                if (DisplayedDeck.drawmessages.ContainsKey(key))
                {
                    DisplayedDeck.drawmessages.Remove(key);
                }

                if (DisplayedDeck.drawmessages.Count == 0)
                {
                    DisplayedDeck.drawmessages = null;
                }
            }
        }

        private void SpecPrependButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(newCardTextBox.Text))
            {
                ListViewItem item = new ListViewItem(newCardTextBox.Text)
                {
                    BackColor = Utilities.ListPrependColor
                };
                specListView.Items.Add(item);
                if (DisplayedDeck.spec_prepend == null)
                {
                    DisplayedDeck.spec_prepend = new List<string>();
                }

                DisplayedDeck.spec_prepend.Add(newCardTextBox.Text);
                newCardTextBox.Text = "";
                newCardTextBox.Focus();
            }
            else if (specListView.SelectedItems.Count > 0)
            {
                ListViewItem item = specListView.SelectedItems[0];
                if (DisplayedDeck.spec_prepend == null)
                {
                    DisplayedDeck.spec_prepend = new List<string>();
                }
                DeleteItemFromCurrentGroup(item);
                item.BackColor = Utilities.ListPrependColor;
                DisplayedDeck.spec_prepend.Add(item.Text);
                newCardTextBox.Text = "";
            }
        }

        private void SpecAppendButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(newCardTextBox.Text))
            {
                ListViewItem item = new ListViewItem(newCardTextBox.Text)
                {
                    BackColor = Utilities.ListAppendColor
                };
                specListView.Items.Add(item);
                if (DisplayedDeck.spec_append == null)
                {
                    DisplayedDeck.spec_append = new List<string>();
                }

                DisplayedDeck.spec_append.Add(newCardTextBox.Text);
                newCardTextBox.Text = "";
                newCardTextBox.Focus();
            }
            else if (specListView.SelectedItems.Count > 0)
            {
                ListViewItem item = specListView.SelectedItems[0];
                if (DisplayedDeck.spec_append == null)
                {
                    DisplayedDeck.spec_append = new List<string>();
                }
                DeleteItemFromCurrentGroup(item);
                item.BackColor = Utilities.ListAppendColor;
                DisplayedDeck.spec_append.Add(item.Text);
                newCardTextBox.Text = "";
            }
        }

        private void SpecRemoveButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(newCardTextBox.Text))
            {
                ListViewItem item = new ListViewItem(newCardTextBox.Text)
                {
                    BackColor = Utilities.ListRemoveColor
                };
                specListView.Items.Add(item);
                if (DisplayedDeck.spec_remove == null)
                {
                    DisplayedDeck.spec_remove = new List<string>();
                }

                DisplayedDeck.spec_remove.Add(newCardTextBox.Text);
                newCardTextBox.Text = "";
                newCardTextBox.Focus();
            }
            else if (specListView.SelectedItems.Count > 0)
            {
                ListViewItem item = specListView.SelectedItems[0];
                if (DisplayedDeck.spec_remove == null)
                {
                    DisplayedDeck.spec_remove = new List<string>();
                }
                DeleteItemFromCurrentGroup(item);
                item.BackColor = Utilities.ListRemoveColor;
                DisplayedDeck.spec_remove.Add(item.Text);
                newCardTextBox.Text = "";
            }
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem item = specListView.SelectedItems[0];
            specListView.Items.Remove(item);
            DeleteItemFromCurrentGroup(item);
        }

        private void SpecListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (specListView.HitTest(e.Location).Item != null)
                {
                    specListView.HitTest(e.Location).Item.Selected = true;
                    //contextMenuStrip1.Show(e.Location);
                }
            }
        }

        private void ResetOnExhaustionCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (resetOnExhaustionCheckBox.CheckState == CheckState.Checked)
            {
                DisplayedDeck.resetonexhaustion = true;
            }

            if (resetOnExhaustionCheckBox.CheckState == CheckState.Unchecked)
            {
                DisplayedDeck.resetonexhaustion = false;
            }

            if (resetOnExhaustionCheckBox.CheckState == CheckState.Indeterminate)
            {
                DisplayedDeck.resetonexhaustion = null;
            }
        }

        private void DeletedCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (deletedCheckBox.CheckState == CheckState.Checked)
            {
                DisplayedDeck.resetonexhaustion = true;
            }

            if (deletedCheckBox.CheckState == CheckState.Unchecked)
            {
                DisplayedDeck.resetonexhaustion = false;
            }

            if (deletedCheckBox.CheckState == CheckState.Indeterminate)
            {
                DisplayedDeck.resetonexhaustion = null;
            }
        }

        private void ExtendsTextBox_TextChanged(object sender, EventArgs e)
        {
            if (extendsTextBox.Text.Contains(","))
            {
                DisplayedDeck.extends = extendsTextBox.Text.Split(',').ToList();
            }
            else
            {
                DisplayedDeck.extends = extendsTextBox.Text != "" ? new List<string> { extendsTextBox.Text } : null;
            }
        }

        private void DrawsNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (drawsNumericUpDown.Value == 0)
            {
                DisplayedDeck.draws = null;
                return;
            }
            DisplayedDeck.draws = Convert.ToInt32(drawsNumericUpDown.Value);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (specListView.SelectedItems.Count < 1)
            {
                return;
            }

            ListViewItem item = specListView.SelectedItems[0];
            specListView.Items.Remove(item);
            DeleteItemFromCurrentGroup(item);
        }

        private void DeleteItemFromCurrentGroup(ListViewItem item)
        {
            if (item.BackColor == Utilities.ListAppendColor && DisplayedDeck.spec_append != null)
            {
                DisplayedDeck.spec_append.Remove(item.Text);
                if (DisplayedDeck.spec_append.Count == 0)
                {
                    DisplayedDeck.spec_append = null;
                }
            }
            else if (item.BackColor == Utilities.ListPrependColor && DisplayedDeck.spec_append != null)
            {
                DisplayedDeck.spec_prepend.Remove(item.Text);
                if (DisplayedDeck.spec_prepend.Count == 0)
                {
                    DisplayedDeck.spec_prepend = null;
                }
            }
            else if (item.BackColor == Utilities.ListRemoveColor && DisplayedDeck.spec_append != null)
            {
                DisplayedDeck.spec_remove.Remove(item.Text);
                if (DisplayedDeck.spec_remove.Count == 0)
                {
                    DisplayedDeck.spec_remove = null;
                }
            }
            else if (DisplayedDeck.spec != null)
            {
                DisplayedDeck.spec.Remove(item.Text);
                if (DisplayedDeck.spec.Count == 0)
                {
                    DisplayedDeck.spec = null;
                }
            }
        }

        private void DeckViewer_Shown(object sender, EventArgs e)
        {
            FillValues(DisplayedDeck);
        }
    }
}
