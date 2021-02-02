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
    public partial class DeckViewer : Form
    {
        public Deck displayedDeck;
        bool editing;
        event EventHandler<Deck> SuccessCallback;
        bool internalDeck;
        public ListViewItem associatedListViewItem;

        public DeckViewer(Deck deck)
        {
            InitializeComponent();
            this.displayedDeck = deck;
            FillValues(deck);
            SetEditingMode(false);
            SetInternal(false);
        }

        public DeckViewer(Deck deck, EventHandler<Deck> SuccessCallback, ListViewItem item)
        {
            InitializeComponent();
            this.displayedDeck = deck;
            FillValues(deck);
            associatedListViewItem = item;
            if (SuccessCallback != null)
            {
                SetEditingMode(true);
                this.SuccessCallback += SuccessCallback;
            }
            else SetEditingMode(false);
            SetInternal(false);
        }

        public DeckViewer(Deck deck, EventHandler<Deck> SuccessCallback, bool? internalDeck, ListViewItem item)
        {
            InitializeComponent();
            this.displayedDeck = deck;
            FillValues(deck);
            associatedListViewItem = item;
            if (SuccessCallback != null)
            {
                SetEditingMode(true);
                this.SuccessCallback += SuccessCallback;
            }
            else SetEditingMode(false);
            if (internalDeck.HasValue) SetInternal(internalDeck.Value);
            else SetInternal(false);
        }

        void SetInternal(bool internalDeck)
        {
            idTextBox.Visible = !internalDeck;
            idLabel.Visible = !internalDeck;
            drawsNumericUpDown.Visible = internalDeck;
            drawsLabel.Visible = internalDeck;
            drawmessagesDataGridView.Visible = !internalDeck;
            drawmessagesLlabel.Visible = !internalDeck;
            this.internalDeck = internalDeck;
        }

        void FillValues(Deck deck)
        {
            if (deck.id != null) idTextBox.Text = deck.id;
            if (deck.label != null) labelTextBox.Text = deck.label;
            if (deck.comments != null) commentsTextBox.Text = deck.comments;
            if (deck.description != null) descriptionTextBox.Text = deck.description;
            if (deck.resetonexhaustion.HasValue) resetOnExhaustionCheckBox.Checked = deck.resetonexhaustion.Value;
            if (deck.defaultcard != null) defaultCardTextBox.Text = deck.defaultcard;
            if (deck.draws.HasValue) drawsNumericUpDown.Value = deck.draws.Value;
            if (deck.deleted.HasValue) deletedCheckBox.Checked = deck.deleted.Value;
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

        void SetEditingMode(bool editing)
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
            okButton.Visible = editing;
            cancelButton.Text = editing ? "Cancel" : "Close";
            deletedCheckBox.Enabled = editing;
        }
        
        private void NewCardButton_Click(object sender, EventArgs e)
        {
            if(newCardTextBox.Text != "" && newCardTextBox.Text != null)
            {
                specListView.Items.Add(newCardTextBox.Text);
                if (displayedDeck.spec == null) displayedDeck.spec = new List<string>();
                displayedDeck.spec.Add(newCardTextBox.Text);
                newCardTextBox.Text = "";
                newCardTextBox.Focus();
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!internalDeck && (idTextBox.Text == null || idTextBox.Text == ""))
            {
                MessageBox.Show("All Standalone Decks must have an ID");
                return;
            }
            if (drawmessagesDataGridView.Rows.Count > 1)
            {
                displayedDeck.drawmessages = new Dictionary<string, string>();
                foreach (DataGridViewRow row in drawmessagesDataGridView.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                    {
                        if (row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
                        {
                            displayedDeck.drawmessages.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString());
                        }
                        else if (row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
                        {
                            displayedDeck.drawmessages.Remove(row.Cells[0].Value.ToString());
                        }
                        else
                        {
                            displayedDeck.drawmessages.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString());
                        }
                    }
                }
            }
            Close();
            SuccessCallback?.Invoke(this, displayedDeck);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void IdTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedDeck.id = idTextBox.Text;
            if (displayedDeck.id == "")
            {
                displayedDeck.id = null;
            }
        }

        private void LabelTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedDeck.label = labelTextBox.Text;
            if (displayedDeck.label == "")
            {
                displayedDeck.label = null;
            }
        }

        private void DescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedDeck.description = descriptionTextBox.Text;
            if (displayedDeck.description == "")
            {
                displayedDeck.description = null;
            }
        }

        private void CommentsTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedDeck.comments = commentsTextBox.Text;
            if (displayedDeck.comments == "")
            {
                displayedDeck.comments = null;
            }
        }

        private void NewCardTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (newCardTextBox.Text != "" && newCardTextBox.Text != null)
                {
                    specListView.Items.Add(newCardTextBox.Text);
                    if (displayedDeck.spec == null) displayedDeck.spec = new List<string>();
                    displayedDeck.spec.Add(newCardTextBox.Text);
                    newCardTextBox.Text = "";
                    newCardTextBox.Focus();
                }
            }
        }

        private void DefaultCardTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedDeck.defaultcard = defaultCardTextBox.Text;
            if (displayedDeck.defaultcard == "")
            {
                displayedDeck.defaultcard = null;
            }
        }
        
        private void SpecListView_DoubleClick(object sender, EventArgs e)
        {
            if (specListView.SelectedItems == null) return;
            string id = specListView.SelectedItems[0].Text.ToString();
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
            if (e.Row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
            {

                if (displayedDeck.drawmessages_extend == null) return;
                if (displayedDeck.drawmessages_extend.ContainsKey(key)) displayedDeck.drawmessages_extend.Remove(key);
                if (displayedDeck.drawmessages_extend.Count == 0) displayedDeck.drawmessages_extend = null;
            }
            else if (e.Row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
            {

                if (displayedDeck.drawmessages_remove == null) return;
                if (displayedDeck.drawmessages_remove.Contains(key)) displayedDeck.drawmessages_remove.Remove(key);
                if (displayedDeck.drawmessages_remove.Count == 0) displayedDeck.drawmessages_remove = null;
            }
            else
            {
                if (displayedDeck.drawmessages == null) return;
                if (displayedDeck.drawmessages.ContainsKey(key)) displayedDeck.drawmessages.Remove(key);
                if (displayedDeck.drawmessages.Count == 0) displayedDeck.drawmessages = null;
            }
        }

        private void SpecPrependButton_Click(object sender, EventArgs e)
        {
            if (newCardTextBox.Text != "" && newCardTextBox.Text != null)
            {
                ListViewItem item = new ListViewItem(newCardTextBox.Text)
                {
                    BackColor = Utilities.ListPrependColor
                };
                specListView.Items.Add(item);
                if (displayedDeck.spec_prepend == null) displayedDeck.spec_prepend = new List<string>();
                displayedDeck.spec_prepend.Add(newCardTextBox.Text);
                newCardTextBox.Text = "";
                newCardTextBox.Focus();
            }
        }

        private void SpecAppendButton_Click(object sender, EventArgs e)
        {
            if (newCardTextBox.Text != "" && newCardTextBox.Text != null)
            {
                ListViewItem item = new ListViewItem(newCardTextBox.Text)
                {
                    BackColor = Utilities.ListAppendColor
                };
                specListView.Items.Add(item);
                if (displayedDeck.spec_append == null) displayedDeck.spec_append = new List<string>();
                displayedDeck.spec_append.Add(newCardTextBox.Text);
                newCardTextBox.Text = "";
                newCardTextBox.Focus();
            }
        }

        private void SpecRemoveButton_Click(object sender, EventArgs e)
        {
            if (newCardTextBox.Text != "" && newCardTextBox.Text != null)
            {
                ListViewItem item = new ListViewItem(newCardTextBox.Text)
                {
                    BackColor = Utilities.ListRemoveColor
                };
                specListView.Items.Add(item);
                if (displayedDeck.spec_remove == null) displayedDeck.spec_remove = new List<string>();
                displayedDeck.spec_remove.Add(newCardTextBox.Text);
                newCardTextBox.Text = "";
                newCardTextBox.Focus();
            }
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem item = specListView.SelectedItems[0];
            specListView.Items.Remove(item);
            if (item.BackColor == Utilities.ListAppendColor)
            {
                if (displayedDeck.spec_append.Contains(item.Text)) displayedDeck.spec_append.Remove(item.Text);
                if (displayedDeck.spec_append.Count == 0) displayedDeck.spec_append = null;
            }
            else if (item.BackColor == Utilities.ListPrependColor)
            {
                if (displayedDeck.spec_prepend.Contains(item.Text)) displayedDeck.spec_prepend.Remove(item.Text);
                if (displayedDeck.spec_prepend.Count == 0) displayedDeck.spec_prepend = null;
            }
            else if (item.BackColor == Utilities.ListRemoveColor)
            {
                if (displayedDeck.spec_remove.Contains(item.Text)) displayedDeck.spec_remove.Remove(item.Text);
                if (displayedDeck.spec_remove.Count == 0) displayedDeck.spec_remove = null;
            }
            else
            {
                if (displayedDeck.spec.Contains(item.Text)) displayedDeck.spec.Remove(item.Text);
                if (displayedDeck.spec.Count == 0) displayedDeck.spec = null;
            }
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
            if (resetOnExhaustionCheckBox.CheckState == CheckState.Checked) displayedDeck.resetonexhaustion = true;
            if (resetOnExhaustionCheckBox.CheckState == CheckState.Unchecked) displayedDeck.resetonexhaustion = false;
            if (resetOnExhaustionCheckBox.CheckState == CheckState.Indeterminate) displayedDeck.resetonexhaustion = null;
        }

        private void DeletedCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (deletedCheckBox.CheckState == CheckState.Checked) displayedDeck.resetonexhaustion = true;
            if (deletedCheckBox.CheckState == CheckState.Unchecked) displayedDeck.resetonexhaustion = false;
            if (deletedCheckBox.CheckState == CheckState.Indeterminate) displayedDeck.resetonexhaustion = null;
        }

        private void ExtendsTextBox_TextChanged(object sender, EventArgs e)
        {
            if (extendsTextBox.Text.Contains(","))
            {
                displayedDeck.extends = extendsTextBox.Text.Split(',').ToList();
            }
            else
            {
                if (extendsTextBox.Text != "") displayedDeck.extends = new List<string> { extendsTextBox.Text };
                else displayedDeck.extends = null;
            }
        }

        private void DrawsNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (drawsNumericUpDown.Value == 0)
            {
                displayedDeck.draws = null;
                return;
            }
            displayedDeck.draws = Convert.ToInt32(drawsNumericUpDown.Value);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (specListView.SelectedItems.Count < 1) return;
            ListViewItem item = specListView.SelectedItems[0];
            specListView.Items.Remove(item);
            if (item.BackColor == Utilities.ListAppendColor)
            {
                if (displayedDeck.spec_append.Contains(item.Text)) displayedDeck.spec_append.Remove(item.Text);
                if (displayedDeck.spec_append.Count == 0) displayedDeck.spec_append = null;
            }
            else if (item.BackColor == Utilities.ListPrependColor)
            {
                if (displayedDeck.spec_prepend.Contains(item.Text)) displayedDeck.spec_prepend.Remove(item.Text);
                if (displayedDeck.spec_prepend.Count == 0) displayedDeck.spec_prepend = null;
            }
            else if (item.BackColor == Utilities.ListRemoveColor)
            {
                if (displayedDeck.spec_remove.Contains(item.Text)) displayedDeck.spec_remove.Remove(item.Text);
                if (displayedDeck.spec_remove.Count == 0) displayedDeck.spec_remove = null;
            }
            else
            {
                if (displayedDeck.spec.Contains(item.Text)) displayedDeck.spec.Remove(item.Text);
                if (displayedDeck.spec.Count == 0) displayedDeck.spec = null;
            }
        }
    }
}
