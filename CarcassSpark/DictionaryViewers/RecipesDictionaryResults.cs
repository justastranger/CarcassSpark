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
        Dictionary<string, Recipe> results;

        public RecipesDictionaryResults(Dictionary<string, Recipe> results)
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
            if (Utilities.RecipeExists(resultsListBox.SelectedItem.ToString()))
            {
                RecipeViewer ev = new RecipeViewer(Utilities.GetRecipe(resultsListBox.SelectedItem.ToString()), null);
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
