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
    public partial class DeckViewer : Form
    {
        public Deck displayedDeck;
        bool editing;

        public DeckViewer(Deck deck)
        {
            InitializeComponent();
            this.displayedDeck = deck;
            fillValues(deck);
            setEditingMode(false);
            setInternal(false);
        }

        public DeckViewer(Deck deck, bool? editing)
        {
            InitializeComponent();
            this.displayedDeck = deck;
            fillValues(deck);
            if (editing.HasValue) setEditingMode(editing.Value);
            else setEditingMode(false);
            setInternal(false);
        }

        public DeckViewer(Deck deck, bool? editing, bool? internalDeck)
        {
            InitializeComponent();
            this.displayedDeck = deck;
            fillValues(deck);
            if (editing.HasValue) setEditingMode(editing.Value);
            else setEditingMode(false);
            if (internalDeck.HasValue) setInternal(internalDeck.Value);
            else setInternal(false);
        }

        void setInternal(bool internalDeck)
        {
            idTextBox.Visible = !internalDeck;
            idLabel.Visible = !internalDeck;
            drawsNumericUpDown.Visible = internalDeck;
            drawsLabel.Visible = internalDeck;
            drawmessagesDataGridView.Visible = !internalDeck;
            drawmessagesLlabel.Visible = !internalDeck;
        }

        void fillValues(Deck deck)
        {
            idTextBox.Text = deck.id;
            labelTextBox.Text = deck.label;
            commentsTextBox.Text = deck.comments;
            descriptionTextBox.Text = deck.description;
            if (deck.resetonexhaustion.HasValue) resetOnExhaustionCheckBox.Checked = deck.resetonexhaustion.Value;
            defaultCardTextBox.Text = deck.defaultcard;
            if (deck.draws.HasValue) drawsNumericUpDown.Value = deck.draws.Value;
            foreach (string id in deck.spec)
            {
                specListBox.Items.Add(id);
            }
            if (deck.drawmessages != null)
            {
                foreach (KeyValuePair<string, string> kvp in deck.drawmessages)
                {
                    drawmessagesDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
        }

        void setEditingMode(bool editing)
        {
            this.editing = editing;
            idTextBox.ReadOnly = !editing;
            labelTextBox.ReadOnly = !editing;
            commentsTextBox.ReadOnly = !editing;
            descriptionTextBox.ReadOnly = !editing;
            resetOnExhaustionCheckBox.Enabled = editing;
            defaultCardTextBox.ReadOnly = !editing;
            drawsNumericUpDown.Enabled = editing;
            drawmessagesDataGridView.ReadOnly = !editing;
            drawmessagesDataGridView.AllowUserToAddRows = editing;
            drawmessagesDataGridView.AllowUserToDeleteRows = editing;
            newCardTextBox.Visible = editing;
            newCardButton.Visible = editing;
            okButton.Visible = editing;
            cancelButton.Text = editing ? "Cancel" : "Close";
        }

        private void specListBox_DoubleClick(object sender, EventArgs e)
        {
            if (specListBox.SelectedItem == null || editing) return;
            string id = specListBox.SelectedItem.ToString();
            if (id.Contains("deck:") && Utilities.deckExists(id.Substring(id.IndexOf(":"))))
            {
                DeckViewer dv = new DeckViewer(Utilities.getDeck(id.Substring(id.IndexOf(":"))), false);
                dv.ShowDialog();
            }
            else if (Utilities.elementExists(id))
            {
                ElementViewer ev = new ElementViewer(Utilities.getElement(id), false);
                ev.ShowDialog();
            }
        }

        private void newCardButton_Click(object sender, EventArgs e)
        {
            if(newCardTextBox.Text != "" && newCardTextBox.Text != null)
            {
                specListBox.Items.Add(newCardTextBox.Text);
                if (displayedDeck.spec == null) displayedDeck.spec = new List<string>();
                displayedDeck.spec.Add(newCardTextBox.Text);
                newCardTextBox.Text = "";
                newCardTextBox.Focus();
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (drawmessagesDataGridView.Rows.Count > 1)
            {
                displayedDeck.drawmessages = new Dictionary<string, string>();
                foreach (DataGridViewRow row in drawmessagesDataGridView.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null) displayedDeck.drawmessages.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString());
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

        private void idTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedDeck.id = idTextBox.Text;
        }

        private void labelTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedDeck.label = labelTextBox.Text;
        }

        private void descriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedDeck.description = descriptionTextBox.Text;
        }

        private void commentsTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedDeck.comments = commentsTextBox.Text;
        }

        private void newCardTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (newCardTextBox.Text != "" && newCardTextBox.Text != null)
                {
                    specListBox.Items.Add(newCardTextBox.Text);
                    if (displayedDeck.spec == null) displayedDeck.spec = new List<string>();
                    displayedDeck.spec.Add(newCardTextBox.Text);
                    newCardTextBox.Text = "";
                    newCardTextBox.Focus();
                }
            }
        }

        private void defaultCardTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedDeck.defaultcard = defaultCardTextBox.Text;
        }

        private void resetOnExhaustionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            displayedDeck.resetonexhaustion = resetOnExhaustionCheckBox.Checked;
        }
    }
}
