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
    public partial class RecipesDictionaryResults : Form
    {
        private Dictionary<Recipe, Guid> results = new Dictionary<Recipe, Guid>();
        private Dictionary<string, Recipe> resultsWithId = new Dictionary<string, Recipe>();

        public RecipesDictionaryResults(Dictionary<Guid, Recipe> results)
        {
            InitializeComponent();

            // this.results = results;
            foreach (KeyValuePair<Guid, Recipe> kvp in results)
            {
                resultsListBox.Items.Add(kvp.Value.id);
                resultsWithId.Add(kvp.Value.id, kvp.Value);
                this.results.Add(kvp.Value, kvp.Key);
            }
        }
        
        private void ResultsListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (resultsListBox.SelectedItem == null) return;
            Recipe selectedRecipe = resultsWithId[resultsListBox.SelectedItem.ToString()];
            RecipeViewer ev = new RecipeViewer(selectedRecipe.Copy(), null, null);
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
