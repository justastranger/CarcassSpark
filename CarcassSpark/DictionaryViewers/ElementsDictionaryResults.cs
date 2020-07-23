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
    public partial class ElementsDictionaryResults : Form
    {
        private readonly Dictionary<string, Element> results;

        public ElementsDictionaryResults(Dictionary<string, Element> results)
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
            if (Utilities.ElementExists(resultsListBox.SelectedItem.ToString()))
            {
                ElementViewer ev = new ElementViewer(results[resultsListBox.SelectedItem.ToString()], null);
                ev.Show();
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
    }
}
