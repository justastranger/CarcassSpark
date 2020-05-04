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

        public DeckViewer(Deck deck)
        {
            InitializeComponent();
            displayedDeck = deck;
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
    }
}
