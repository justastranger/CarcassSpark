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
        private Dictionary<Element, Guid> results = new Dictionary<Element, Guid>();
        private Dictionary<string, Element> resultsWithId = new Dictionary<string, Element>();

        public ElementsDictionaryResults(Dictionary<Guid, Element> results)
        {
            InitializeComponent();

            // this.results = results;
            foreach (KeyValuePair<Guid, Element> kvp in results)
            {
                resultsListBox.Items.Add(kvp.Value.id);
                resultsWithId.Add(kvp.Value.id, kvp.Value);
                this.results.Add(kvp.Value, kvp.Key);
            }
        }

        private void ResultsListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (resultsListBox.SelectedItem == null) return;
            Element selectedElement = resultsWithId[resultsListBox.SelectedItem.ToString()];
            ElementViewer ev = new ElementViewer(selectedElement.Copy(), null, null);
            ev.Show();
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
