using CarcassSpark.ObjectTypes;
using CarcassSpark.ObjectViewers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CarcassSpark.DictionaryViewers
{
    public partial class RecipesDictionaryResults : Form
    {
        private readonly Dictionary<Recipe, Guid> results = new Dictionary<Recipe, Guid>();
        private readonly Dictionary<string, Recipe> resultsWithId = new Dictionary<string, Recipe>();

        public RecipesDictionaryResults(Dictionary<Guid, Recipe> results)
        {
            InitializeComponent();

            // this.results = results;
            foreach (KeyValuePair<Guid, Recipe> kvp in results)
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
