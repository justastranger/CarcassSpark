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
    public partial class LegaciesDictionaryResults : Form
    {
        Dictionary<string, Legacy> results;

        public LegaciesDictionaryResults(Dictionary<string, Legacy> results)
        {
            InitializeComponent();

            this.results = results;
            foreach (string key in results.Keys)
            {
                resultsListBox.Items.Add(key);
            }
        }

        private void ResultsListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (resultsListBox.SelectedItem == null) return;
            if (Utilities.LegacyExists(resultsListBox.SelectedItem.ToString()))
            {
                LegacyViewer ev = new LegacyViewer(results[resultsListBox.SelectedItem.ToString()], null);
                ev.Show();
            }
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

        private void OkButton_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void CancelButton_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void ResultsListBox_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            if (resultsListBox.SelectedItem == null) return;
            LegacyViewer lv = new LegacyViewer(results[resultsListBox.SelectedItem.ToString()], null);
            lv.Show();
        }
    }
}
