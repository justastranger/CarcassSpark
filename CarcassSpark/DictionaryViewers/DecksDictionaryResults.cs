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
using CarcassSpark.ObjectViewers;

namespace CarcassSpark.DictionaryViewers
{
    public partial class DecksDictionaryResults : Form
    {
        private Dictionary<Deck, Guid> results = new Dictionary<Deck, Guid>();
        private Dictionary<string, Deck> resultsWithId = new Dictionary<string, Deck>();

        public DecksDictionaryResults(Dictionary<Guid, Deck> results)
        {
            InitializeComponent();

            // this.results = results;
            foreach (KeyValuePair<Guid, Deck> kvp in results)
            {
                resultsListBox.Items.Add(kvp.Value.id);
                resultsWithId.Add(kvp.Value.id, kvp.Value);
                this.results.Add(kvp.Value, kvp.Key);
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ResultsListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (resultsListBox.SelectedItem == null) return;
            Deck selectedDeck = resultsWithId[resultsListBox.SelectedItem.ToString()];
            DeckViewer ev = new DeckViewer(selectedDeck.Copy(), null, null);
            ev.Show();
        }
    }
}
