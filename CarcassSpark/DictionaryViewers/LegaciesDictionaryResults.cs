using CarcassSpark.ObjectTypes;
using CarcassSpark.ObjectViewers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CarcassSpark.DictionaryViewers
{
    public partial class LegaciesDictionaryResults : Form
    {
        private readonly Dictionary<Legacy, Guid> results = new Dictionary<Legacy, Guid>();
        private readonly Dictionary<string, Legacy> resultsWithId = new Dictionary<string, Legacy>();

        public LegaciesDictionaryResults(Dictionary<Guid, Legacy> results)
        {
            InitializeComponent();

            // this.resultsWithIds = results;
            foreach (KeyValuePair<Guid, Legacy> kvp in results)
            {
                resultsListBox.Items.Add(kvp.Value.ID);
                resultsWithId.Add(kvp.Value.ID, kvp.Value);
                this.results.Add(kvp.Value, kvp.Key);
            }
        }

        private void ResultsListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (resultsListBox.SelectedItem == null)
            {
                return;
            }

            Legacy selectedLegacy = resultsWithId[resultsListBox.SelectedItem.ToString()];
            LegacyViewer ev = new LegacyViewer(selectedLegacy.Copy(), null, null);
            ev.Show();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
