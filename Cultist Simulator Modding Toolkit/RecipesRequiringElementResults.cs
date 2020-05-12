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
        
        private void resultsListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (resultsListBox.SelectedItem == null) return;
            if (Utilities.recipeExists(resultsListBox.SelectedItem.ToString()))
            {
                RecipeViewer ev = new RecipeViewer(Utilities.getRecipe(resultsListBox.SelectedItem.ToString()), false);
                ev.ShowDialog();
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
    }
}
