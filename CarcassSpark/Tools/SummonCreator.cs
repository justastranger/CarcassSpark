using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CarcassSpark.ObjectViewers;
using CarcassSpark.ObjectTypes;

namespace CarcassSpark.Tools
{
    public partial class SummonCreator : Form
    {

        public Element baseSummon, preSummon;
        public Recipe startSummon, succeedSummon;

        public SummonCreator()
        {
            InitializeComponent();
        }

        private void InspectBaseButton_Click(object sender, EventArgs e)
        {
            ElementViewer ev = new ElementViewer(baseSummon, null);
            ev.Show();
        }

        private void InspectPreButton_Click(object sender, EventArgs e)
        {
            ElementViewer ev = new ElementViewer(preSummon, null);
            ev.Show();
        }

        private void BaseRecipe_Assign(object sender, Recipe result)
        {
            startSummon = result;
            startSummon.effects = new Dictionary<string, string>
            {
                { baseSummon.id, "1" }
            };
            startSummon.linked = new List<RecipeLink>
            {
                new RecipeLink("summoninglosingcontrol", 30, false, (Dictionary<string, string>)null, null)
            };
            startSummon.actionId = "work";
            startSummon.warmup = 180;
            startSummon.requirements["desire"] = "-1";
            startSummon.requirements["ritual"] = "1";
            succeedSummon = new Recipe
            {
                id = startSummon.id + "_success",
                label = startSummon.label,
                actionId = startSummon.actionId,
                description = startSummon.description
            };
            startSummon.linked.Add(new RecipeLink(succeedSummon.id, 100, false, (Dictionary<string, string>)null, null));
            baseSummonIdTextBox.Text = baseSummon.id;
            successSummonTextBox.Text = succeedSummon.id;
            inspectBaseButton.Enabled = true;
            inspectSuccessRecipeButton.Enabled = true;
        }

        private void CreateRecipeButton_Click(object sender, EventArgs e)
        {
            RecipeViewer rv = new RecipeViewer(new Recipe(), BaseRecipe_Assign);
            MessageBox.Show("You do not have to fill out the Warm Up, Verb ID, Linked Recipes or effects portion, just the: ID, Label, Start Description, Description, and Requirements. You do not need to include desire: -1 or ritual: 1 in Requirements.");
            rv.Show();
        }

        private void InspectBaseRecipeButton_Click(object sender, EventArgs e)
        {
            RecipeViewer rv = new RecipeViewer(startSummon, null);
            rv.Show();
        }

        private void InspectSuccessRecipeButton_Click(object sender, EventArgs e)
        {
            RecipeViewer rv = new RecipeViewer(succeedSummon, null);
            rv.Show();
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

        private void BaseElement_Assign(object sender, Element result)
        {
            baseSummon = result;
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
            Dictionary<string, List<XTrigger>> tempXTriggers = baseSummon.xtriggers;
            tempXTriggers.Add("killmanifesting", new List<XTrigger> { new XTrigger(baseSummon.decayTo) });
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

        private void CreateBaseElementButton_click(object sender, EventArgs e)
        {
            ElementViewer ev = new ElementViewer(new Element(), BaseElement_Assign);
            ev.Show();
        }
    }
}
