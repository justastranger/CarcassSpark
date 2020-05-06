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
        Deck displayedDeck;
        ModViewer currentMod;

        public DeckViewer(Deck deck, ModViewer currentMod)
        {
            InitializeComponent();
            this.displayedDeck = deck;
            this.currentMod = currentMod;
            idTextBox.Text = deck.id;
            labelTextBox.Text = deck.label;
            commentsTextBox.Text = deck.comments;
            descriptionTextBox.Text = deck.description;
            resetOnExhaustionCheckBox.Checked = deck.resetonexhaustion;
            defaultCardTextBox.Text = deck.defaultcard;
            drawsTextBox.Text = Convert.ToString(deck.draws);
            foreach (string id in deck.spec)
            {
                specListBox.Items.Add(id);
            }
            if (deck.drawmessages != null && !deck.drawmessages.isNull())
            {
                foreach (KeyValuePair<string, string> kvp in deck.drawmessages.toDictionary())
                {
                    drawmessagesDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
        }

        private void specListBox_DoubleClick(object sender, EventArgs e)
        {
            if (specListBox.SelectedItem == null) return;
            string id = specListBox.SelectedItem.ToString();
            if (id.Contains("deck:") && Utilities.deckExists(id.Substring(id.IndexOf(":"))))
            {
                DeckViewer dv = new DeckViewer(Utilities.getDeck(id.Substring(id.IndexOf(":"))), currentMod);
                dv.ShowDialog();
            }
            else if (Utilities.elementExists(id))
            {
                ElementViewer ev = new ElementViewer(Utilities.getElement(id), currentMod);
                ev.ShowDialog();
            }
        }
    }
}
