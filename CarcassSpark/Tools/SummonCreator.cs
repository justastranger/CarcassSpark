using CarcassSpark.ObjectTypes;
using CarcassSpark.ObjectViewers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CarcassSpark.Tools
{
    public partial class SummonCreator : Form
    {

        public Element BaseSummon, PreSummon;
        public Recipe StartSummon, SucceedSummon;

        public SummonCreator()
        {
            InitializeComponent();
        }

        private void InspectBaseButton_Click(object sender, EventArgs e)
        {
            ElementViewer ev = new ElementViewer(BaseSummon, null, null);
            ev.Show();
        }

        private void InspectPreButton_Click(object sender, EventArgs e)
        {
            ElementViewer ev = new ElementViewer(PreSummon, null, null);
            ev.Show();
        }

        private void BaseRecipe_Assign(object sender, Recipe result)
        {
            StartSummon = result.Copy();
            SucceedSummon = new Recipe
            {
                ID = StartSummon.ID + ".success",
                label = StartSummon.label,
                actionId = StartSummon.actionId,
                description = StartSummon.description
            };
            StartSummon.linked.Add(new RecipeLink(SucceedSummon.ID));
            baseSummonIdTextBox.Text = BaseSummon.ID;
            successSummonTextBox.Text = SucceedSummon.ID;
            inspectBaseButton.Enabled = true;
            inspectSuccessRecipeButton.Enabled = true;
        }

        private void CreateRecipeButton_Click(object sender, EventArgs e)
        {
            Recipe startSummonRecipe = new Recipe()
            {
                actionId = "work",
                requirements = new Dictionary<string, string>() {
                    { "desire", "-1" },
                    { "ritual", "1" }
                },
                warmup = 60,
                effects = new Dictionary<string, string>
                {
                    { BaseSummon.ID, "1" }
                },
                linked = new List<RecipeLink>
                {
                    new RecipeLink("summoninglosingcontrol", 30, false, null, null)
                },
                craftable = true
            };

            RecipeViewer rv = new RecipeViewer(startSummonRecipe, BaseRecipe_Assign, RecipeType.Generator, null);
            MessageBox.Show("The fields with red labels are required.", "Required Values");
            rv.Show();
        }

        private void InspectBaseRecipeButton_Click(object sender, EventArgs e)
        {
            RecipeViewer rv = new RecipeViewer(StartSummon, null, null);
            rv.Show();
        }

        private void InspectSuccessRecipeButton_Click(object sender, EventArgs e)
        {
            RecipeViewer rv = new RecipeViewer(SucceedSummon, null, null);
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
            BaseSummon = result.Copy();
            baseIdTextBox.Text = BaseSummon.ID;
            BaseSummon.xtriggers = new Dictionary<string, List<XTrigger>>()
            {
                { "killsummoned", new List<XTrigger>()
                    {
                        new XTrigger(BaseSummon.decayTo)
                    }
                }
            };
            Dictionary<string, int> preAspects = new Dictionary<string, int>();
            foreach (KeyValuePair<string, int> kvp in BaseSummon.aspects)
            {
                switch (kvp.Key)
                {
                    case "summoned":
                        preAspects.Add("manifesting", 1);
                        break;

                    case "follower":
                        break;

                    default:
                        preAspects[kvp.Key] = kvp.Value;
                        break;
                }
            }
            PreSummon = new Element
            {
                id = "pre." + BaseSummon.ID,
                label = BaseSummon.label,
                description = BaseSummon.description,
                unique = BaseSummon.unique,
                icon = BaseSummon.icon,
                comments = BaseSummon.comments,
                aspects = preAspects,
                xtriggers = new Dictionary<string, List<XTrigger>>
                {
                    { "killmanifesting", new List<XTrigger>
                        {
                            new XTrigger(BaseSummon.decayTo)
                        }
                    },
                    { "killsummoned", new List<XTrigger>
                        {
                            new XTrigger(BaseSummon.decayTo)
                        }
                    }
                },
                decayTo = BaseSummon.ID,
                lifetime = 1
            };
            preSummonIdTextBox.Text = PreSummon.ID;

            createRecipeButton.Enabled = true;
            inspectBaseButton.Enabled = true;
            inspectPreButton.Enabled = true;
        }

        private void CreateBaseElementButton_click(object sender, EventArgs e)
        {
            Element baseElement = new Element()
            {
                aspects = new Dictionary<string, int>()
                {
                    { "summoned", 1 },
                    { "follower", 1 }
                },
                lifetime = 60
            };
            MessageBox.Show("The fields with red labels are required.", "Required Values");
            ElementViewer ev = new ElementViewer(baseElement, BaseElement_Assign, ElementType.Generator, null);
            ev.Show();
        }
    }
}
