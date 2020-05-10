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
    public partial class SummonCreator : Form
    {

        public Element baseSummon, preSummon;
        public Recipe startSummon, succeedSummon;

        private void inspectBaseButton_Click(object sender, EventArgs e)
        {
            ElementViewer ev = new ElementViewer(baseSummon, false);
            ev.ShowDialog();
        }

        private void inspectPreButton_Click(object sender, EventArgs e)
        {
            ElementViewer ev = new ElementViewer(preSummon, false);
            ev.ShowDialog();
        }

        private void createRecipeButton_Click(object sender, EventArgs e)
        {
            RecipeViewer rv = new RecipeViewer(new Recipe(), true);
            MessageBox.Show("You do not have to fill out the Warm Up, Verb ID, Linked Recipes or effects portion, just the: ID, Label, Start Description, Description, and Requirements. You do not need to include desire: -1 or ritual: 1 in Requirements.");
            rv.ShowDialog();
            if (rv.DialogResult == DialogResult.OK)
            {
                startSummon = rv.displayedRecipe;
                startSummon.effects = new Dictionary<string, int>();
                startSummon.effects.Add(baseSummon.id, 1);
                startSummon.linked = new List<Recipe.RecipeLink>();
                startSummon.linked.Add(new Recipe.RecipeLink("summoninglosingcontrol", 30, false));
                startSummon.actionId = "work";
                startSummon.warmup = 180;
                startSummon.requirements["desire"] = -1;
                startSummon.requirements["ritual"] = 1;
                succeedSummon = new Recipe();
                succeedSummon.id = startSummon.id + "_success";
                succeedSummon.label = startSummon.label;
                succeedSummon.actionId = startSummon.actionId;
                succeedSummon.description = startSummon.description;
                startSummon.linked.Add(new Recipe.RecipeLink(succeedSummon.id, 100, false));
            }
        }

        private void inspectBaseRecipeButton_Click(object sender, EventArgs e)
        {
            RecipeViewer rv = new RecipeViewer(startSummon, false);
            rv.ShowDialog();
        }

        private void inspectSuccessRecipeButton_Click(object sender, EventArgs e)
        {
            RecipeViewer rv = new RecipeViewer(succeedSummon, false);
            rv.ShowDialog();
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

        public SummonCreator()
        {
            InitializeComponent();
        }

        private void createBaseElementButton_click(object sender, EventArgs e)
        {
            ElementViewer ev = new ElementViewer(new Element(), true);
            ev.ShowDialog();
            if (ev.DialogResult == DialogResult.OK)
            {
                baseSummon = ev.displayedElement;
                baseIdTextBox.Text = baseSummon.id;
                Dictionary<string, int> tmpAspects = baseSummon.aspects;
                Dictionary<string, int> newAspects = new Dictionary<string, int>();
                foreach (KeyValuePair<string, int> kvp in tmpAspects)
                {
                    switch (kvp.Key)
                    {
                        case "summoned":
                            //tmpAspects.Remove(kvp.Key);
                            newAspects.Add("manifesting", 1);
                            break;

                        case "follower":
                            //tmpAspects.Remove(kvp.Key);
                            break;

                        default:
                            newAspects[kvp.Key] = kvp.Value;
                            break;
                    }
                }
                Dictionary<string, string> tempXTriggers = baseSummon.xtriggers;
                tempXTriggers.Add("killmanifesting", baseSummon.decayTo);
                preSummon = new Element();
                preSummon.id = "pre." + baseSummon.id;
                preSummon.label = baseSummon.label;
                preSummon.description = baseSummon.description;
                preSummon.unique = baseSummon.unique;
                preSummon.icon = baseSummon.icon;
                preSummon.comments = baseSummon.comments;
                preSummon.aspects = tmpAspects;
                preSummon.xtriggers = tempXTriggers;
                preSummon.decayTo = baseSummon.id;
                preSummon.lifetime = 1;
                preSummonIdTextBox.Text = preSummon.id;

                createRecipeButton.Enabled = true;
                inspectBaseButton.Enabled = true;
                inspectPreButton.Enabled = true;
            }
        }
    }
}
