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
            ElementViewer ev = new ElementViewer(baseSummon, null, null);
            ev.Show();
        }

        private void InspectPreButton_Click(object sender, EventArgs e)
        {
            ElementViewer ev = new ElementViewer(preSummon, null, null);
            ev.Show();
        }

        private void BaseRecipe_Assign(object sender, Recipe result)
        {
            startSummon = result.Copy();
            succeedSummon = new Recipe
            {
                id = startSummon.id + ".success",
                label = startSummon.label,
                actionId = startSummon.actionId,
                description = startSummon.description
            };
            startSummon.linked.Add(new RecipeLink(succeedSummon.id));
            baseSummonIdTextBox.Text = baseSummon.id;
            successSummonTextBox.Text = succeedSummon.id;
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
                    { baseSummon.id, "1" }
                },
                linked = new List<RecipeLink>
                {
                    new RecipeLink("summoninglosingcontrol", 30, false, (Dictionary<string, string>)null, null)
                },
                craftable = true
            };

            RecipeViewer rv = new RecipeViewer(startSummonRecipe, BaseRecipe_Assign, RecipeType.GENERATOR, null);
            MessageBox.Show("The fields with red labels are required.", "Required Values");
            rv.Show();
        }

        private void InspectBaseRecipeButton_Click(object sender, EventArgs e)
        {
            RecipeViewer rv = new RecipeViewer(startSummon, null, null);
            rv.Show();
        }

        private void InspectSuccessRecipeButton_Click(object sender, EventArgs e)
        {
            RecipeViewer rv = new RecipeViewer(succeedSummon, null, null);
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
            baseSummon = result.Copy();
            baseIdTextBox.Text = baseSummon.id;
            baseSummon.xtriggers = new Dictionary<string, List<XTrigger>>()
            {
                { "killsummoned", new List<XTrigger>()
                    {
                        new XTrigger(baseSummon.decayTo)
                    }
                }
            };
            Dictionary<string, int> preAspects = new Dictionary<string, int>();
            foreach (KeyValuePair<string, int> kvp in baseSummon.aspects)
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
            preSummon = new Element
            {
                id = "pre." + baseSummon.id,
                label = baseSummon.label,
                description = baseSummon.description,
                unique = baseSummon.unique,
                icon = baseSummon.icon,
                comments = baseSummon.comments,
                aspects = preAspects,
                xtriggers = new Dictionary<string, List<XTrigger>>
                {
                    { "killmanifesting", new List<XTrigger>
                        {
                            new XTrigger(baseSummon.decayTo)
                        }
                    },
                    { "killsummoned", new List<XTrigger>
                        {
                            new XTrigger(baseSummon.decayTo)
                        }
                    }
                },
                decayTo = baseSummon.id,
                lifetime = 1
            };
            preSummonIdTextBox.Text = preSummon.id;

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
            ElementViewer ev = new ElementViewer(baseElement, BaseElement_Assign, ElementType.GENERATOR, null);
            ev.Show();
        }
    }
}
