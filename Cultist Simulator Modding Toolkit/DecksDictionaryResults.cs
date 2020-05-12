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
    public partial class DecksDictionaryResults : Form
    {
        Dictionary<string, Deck> results;

        public DecksDictionaryResults(Dictionary<string, Deck> results)
        {
            InitializeComponent();

            this.results = results;
            foreach (string key in results.Keys)
            {
                resultsListBox.Items.Add(key);
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void resultsListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (resultsListBox.SelectedItem == null) return;
            DeckViewer dv = new DeckViewer(results[resultsListBox.SelectedItem.ToString()], false);
            dv.ShowDialog();
        }
    }
}
