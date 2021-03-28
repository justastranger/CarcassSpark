using CarcassSpark.ObjectTypes;
using MindFusion.Diagramming;
using MindFusion.Diagramming.Layout;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace CarcassSpark.Flowchart
{
    public partial class RecipeFlowchartViewer : Form
    {
        private bool skipYesNo = false;
        private readonly Dictionary<string, RecipeNode> recipeNodes = new Dictionary<string, RecipeNode>();
        private readonly List<RecipeNode> recipeNodesThatNeedToBeProcessed = new List<RecipeNode>();
        private readonly CascadeLayout layout = new CascadeLayout();

        public RecipeFlowchartViewer(Recipe recipe)
        {
            InitializeComponent();
            RecipeNode rootRecipeNode = CreateRecipeNode(recipe, 25, 25);
            recipeNodes.Add(recipe.ID, rootRecipeNode);
            recipeNodesThatNeedToBeProcessed.Add(rootRecipeNode);
            //tl.KeepGroupLayout = true;
            //layout.Orientation = MindFusion.Diagramming.Layout.Orientation.Vertical;
            layout.EnableParallelism = true;
            if (Settings.settings["loadAllFlowchartNodes"] != null && Settings.settings["loadAllFlowchartNodes"].ToObject<bool>())
            {
                loadLinkedRecipesButton.Visible = false;
                ProcessAllRecipes();
            }
        }

        private void ArrangeNodes()
        {
            layout.Arrange(diagram1);
        }

        private void ProcessRecipes()
        {
            List<RecipeNode> nodesInProgress = new List<RecipeNode>(recipeNodesThatNeedToBeProcessed);
            recipeNodesThatNeedToBeProcessed.Clear();
            progressBar1.Visible = true;
            progressBar1.Value = 0;
            progressBar1.Maximum = nodesInProgress.Count;
            progressBar1.Step = 1;
            foreach (RecipeNode currentNode in nodesInProgress)
            {
                foreach (string link in currentNode.LinkedRecipes)
                {
                    // if a node for this recipe doesn't already exist, create it and link to it
                    if (!recipeNodes.ContainsKey(link))
                    {
                        RecipeNode tmpNode = CreateRecipeNode(Utilities.GetRecipe(link), 25, 25);
                        recipeNodes[link] = tmpNode;

                        tmpNode.Node.AttachTo(currentNode.Node, AttachToNode.MiddleRight);
                        recipeNodesThatNeedToBeProcessed.Add(tmpNode);
                    }

                }
                foreach (TreeViewItem rootItem in currentNode.Node.RootItems)
                {
                    if (rootItem.Label.Contains("Recipes:"))
                    {
                        foreach (TreeViewItem child in rootItem.Children)
                        {
                            // child.Label -> "Linked Recipe:"
                            foreach (TreeViewItem recipeChild in child.Children)
                            {
                                if (recipeChild.Label.Contains("Recipe ID: "))
                                {
                                    CreateLink(currentNode.Node, child, (TreeViewNode)diagram1.FindNodeById(recipeChild.Label.Substring(recipeChild.Label.LastIndexOf(' ') + 1)));
                                }
                            }
                        }
                    }
                }
                progressBar1.PerformStep();
            }
            ArrangeNodes();
            diagram1.RouteAllLinks();
            progressBar1.Visible = false;
        }

        private void ProcessAllRecipes()
        {
            progressBar1.Visible = true;
            progressBar1.Value = 0;
            progressBar1.Maximum = recipeNodesThatNeedToBeProcessed.Count;
            progressBar1.Step = 1;
            while (recipeNodesThatNeedToBeProcessed.Count > 0)
            {
                RecipeNode currentNode = recipeNodesThatNeedToBeProcessed[0];
                foreach (string link in currentNode.LinkedRecipes)
                {
                    // if a node for this recipe doesn't already exist, create it and link to it
                    if (!recipeNodes.ContainsKey(link))
                    {
                        RecipeNode tmpNode = CreateRecipeNode(Utilities.GetRecipe(link), 25, 25);
                        recipeNodes[link] = tmpNode;

                        tmpNode.Node.AttachTo(currentNode.Node, AttachToNode.MiddleRight);
                        recipeNodesThatNeedToBeProcessed.Add(tmpNode);
                    }

                }
                foreach (TreeViewItem rootItem in currentNode.Node.RootItems)
                {
                    if (rootItem.Label.Contains("Recipes:"))
                    {
                        foreach (TreeViewItem child in rootItem.Children)
                        {
                            // child.Label -> "Linked Recipe:"
                            foreach (TreeViewItem recipeChild in child.Children)
                            {
                                if (recipeChild.Label.Contains("Recipe ID: "))
                                {
                                    CreateLink(currentNode.Node, child, (TreeViewNode)diagram1.FindNodeById(recipeChild.Label.Substring(recipeChild.Label.LastIndexOf(' ') + 1)));
                                }
                            }
                        }
                    }
                }
                recipeNodesThatNeedToBeProcessed.Remove(currentNode);
                progressBar1.PerformStep();
            }
            ArrangeNodes();
            diagram1.RouteAllLinks();
            progressBar1.Visible = false;
        }

        private void CreateLink(TreeViewNode outgoing, TreeViewNode incoming)
        {
            DiagramLink tmp = diagram1.Factory.CreateDiagramLink(outgoing, incoming);
            tmp.AddLabel((string)incoming.Id);
            tmp.Id = (string)outgoing.Id + (string)incoming.Id;
            //incoming.AttachTo(outgoing, AttachToNode.BottomCenter);
            tmp.AutoRoute = true;
        }

        private void CreateLink(TreeViewNode outgoingNode, TreeViewItem outgoingItem, TreeViewNode incoming)
        {
            //DiagramLink tmp = diagram1.Factory.CreateDiagramLink(outgoingNode, incoming);
            DiagramLink tmp = new DiagramLink(diagram1);
            tmp.OriginConnection = new TreeViewConnectionPoint(outgoingNode, tmp, false, outgoingItem);
            outgoingItem.OutgoingLinks.Add(tmp);
            tmp.DestinationConnection = new ConnectionPoint(incoming, tmp, true);
            incoming.IncomingLinks.Add(tmp);
            tmp.AddLabel((string)incoming.Id);
            tmp.Id = outgoingItem.Label.Substring(11) + (string)incoming.Id;
            tmp.AutoRoute = true;
            diagram1.Links.Add(tmp);
        }

        private RecipeNode CreateRecipeNode(Recipe recipe, float x, float y)
        {
            //if (recipeNodes.ContainsKey(recipe.id))
            //{
            //    MessageBox.Show("createRecipeNode called on a recipe that's already been created.");
            //    throw new Exception();
            //}
            //Dictionary<string, TreeViewItem> tmp = new Dictionary<string, TreeViewItem>();
            List<string> outgoingLinks = new List<string>();
            TreeViewNode tempTvn = diagram1.Factory.CreateTreeViewNode(x, y, 150, 150);
            tempTvn.Id = recipe.ID;
            tempTvn.IgnoreLayout = false;
            tempTvn.ConnectionStyle = TreeViewConnectionStyle.Items;
            tempTvn.Text = recipe.label ?? recipe.ID;

            tempTvn.RootItems.Add(new TreeViewItem("Recipe ID: " + recipe.ID));
            if (recipe.actionId != null)
            {
                tempTvn.RootItems.Add(new TreeViewItem("Action ID: " + recipe.actionId));
            }

            if (recipe.startdescription != null)
            {
                tempTvn.RootItems.Add(new TreeViewItem("Recipe Start Description: "));
                tempTvn.RootItems.Last().Children.Add(new TreeViewItem(recipe.startdescription));
            }
            if (recipe.description != null)
            {
                tempTvn.RootItems.Add(new TreeViewItem("Recipe Description: "));
                tempTvn.RootItems.Last().Children.Add(new TreeViewItem(recipe.description));
            }
            if (recipe.comments != null)
            {
                tempTvn.RootItems.Add(new TreeViewItem("Recipe Comments: "));
                tempTvn.RootItems.Last().Children.Add(new TreeViewItem(recipe.comments));
            }
            if (recipe.ending != null)
            {
                tempTvn.RootItems.Add(new TreeViewItem("Ending: " + recipe.ending));
            }

            if (recipe.signalendingflavour != null)
            {
                tempTvn.RootItems.Add(new TreeViewItem("Signal Ending Flavour: " + recipe.signalendingflavour));
            }

            if (recipe.signalimportantloop.HasValue)
            {
                tempTvn.RootItems.Add(new TreeViewItem("Signal Important Loop: " + recipe.signalimportantloop.Value));
            }

            if (recipe.portaleffect != null)
            {
                tempTvn.RootItems.Add(new TreeViewItem("Portal Effect: " + recipe.portaleffect));
            }

            if (recipe.burnimage != null)
            {
                tempTvn.RootItems.Add(new TreeViewItem("Burn Image: " + recipe.burnimage));
            }

            if (recipe.deleted.HasValue)
            {
                tempTvn.RootItems.Add(new TreeViewItem("Deleted: " + recipe.deleted.Value));
            }

            if (recipe.craftable.HasValue)
            {
                tempTvn.RootItems.Add(new TreeViewItem("Craftable: " + recipe.craftable.Value.ToString()));
            }

            if (recipe.hintonly.HasValue)
            {
                tempTvn.RootItems.Add(new TreeViewItem("Hint Only: " + recipe.hintonly.Value.ToString()));
            }

            if (recipe.maxexecutions.HasValue)
            {
                tempTvn.RootItems.Add(new TreeViewItem("Max Executions: " + recipe.maxexecutions.Value.ToString()));
            }

            if (recipe.warmup.HasValue)
            {
                tempTvn.RootItems.Add(new TreeViewItem("Warmup: " + recipe.warmup.Value.ToString()));
            }

            if (recipe.internaldeck != null)
            {
                tempTvn.RootItems.Add(new TreeViewItem("Internal Deck: "));
                if (recipe.internaldeck.label != null)
                {
                    tempTvn.RootItems.Last().Children.Add(new TreeViewItem("Label: " + recipe.internaldeck.label));
                }

                if (recipe.internaldeck.description != null)
                {
                    tempTvn.RootItems.Last().Children.Add(new TreeViewItem("Description: "));
                    tempTvn.RootItems.Last().Children.Last().Children.Add(new TreeViewItem(recipe.internaldeck.description));
                }
                if (recipe.internaldeck.draws.HasValue)
                {
                    tempTvn.RootItems.Last().Children.Add(new TreeViewItem("Draws: " + recipe.internaldeck.draws.Value));
                }

                if (recipe.internaldeck.resetonexhaustion.HasValue)
                {
                    tempTvn.RootItems.Last().Children.Add(new TreeViewItem("Reset on Exhaustion: " + recipe.internaldeck.resetonexhaustion.Value));
                }

                if (recipe.internaldeck.defaultcard != null)
                {
                    tempTvn.RootItems.Last().Children.Add(new TreeViewItem("Default Card: " + recipe.internaldeck.defaultcard));
                }

                if (recipe.internaldeck.spec != null)
                {
                    tempTvn.RootItems.Last().Children.Add(new TreeViewItem("Cards: "));
                    foreach (string card in recipe.internaldeck.spec)
                    {
                        tempTvn.RootItems.Last().Children.Last().Children.Add(new TreeViewItem(card));
                    }
                }
            }
            if (recipe.slots != null)
            {
                Slot slot = recipe.slots[0];
                TreeViewItem slotItem = new TreeViewItem("Recipe Slot");
                tempTvn.RootItems.Add(slotItem);
                // string id
                if (slot.id != null)
                {
                    slotItem.Children.Add(new TreeViewItem("ID: " + slot.id));
                }
                // string label
                if (slot.label != null)
                {
                    slotItem.Children.Add(new TreeViewItem("Label: " + slot.label));
                }
                // string description
                if (slot.description != null)
                {
                    slotItem.Children.Add(new TreeViewItem("Description: " + slot.id));
                    slotItem.Children.Last().Children.Add(new TreeViewItem(slot.description));
                }
                // string actionId
                if (slot.actionId != null)
                {
                    slotItem.Children.Add(new TreeViewItem("Action ID: " + slot.actionId));
                }
                // Dictionary<string, int> required
                if (slot.required != null)
                {
                    slotItem.Children.Add(new TreeViewItem("Required: "));
                    foreach (KeyValuePair<string, int> required in slot.required)
                    {
                        slotItem.Children.Last().Children.Add(new TreeViewItem(required.Key + ": " + required.Value));
                    }
                }
                // Dictionary<string, int> forbidden
                if (slot.forbidden != null)
                {
                    slotItem.Children.Add(new TreeViewItem("Forbidden: "));
                    foreach (KeyValuePair<string, int> forbidden in slot.forbidden)
                    {
                        slotItem.Children.Last().Children.Add(new TreeViewItem(forbidden.Key + ": " + forbidden.Value));
                    }
                }
                // bool? greedy
                if (slot.greedy.HasValue)
                {
                    slotItem.Children.Add(new TreeViewItem("Greedy: " + slot.greedy.Value.ToString()));
                }
                // bool? consumes
                if (slot.consumes.HasValue)
                {
                    slotItem.Children.Add(new TreeViewItem("Consumes: " + slot.consumes.Value.ToString()));
                }
            }
            if (recipe.requirements != null)
            {
                TreeViewItem requirements = new TreeViewItem("Requirements:");
                tempTvn.RootItems.Add(requirements);
                foreach (KeyValuePair<string, string> kvp in recipe.requirements)
                {
                    requirements.Children.Add(new TreeViewItem(kvp.Key + ": " + kvp.Value));
                }
            }
            if (recipe.tablereqs != null)
            {
                TreeViewItem tablereqs = new TreeViewItem("Table Requirements:");
                tempTvn.RootItems.Add(tablereqs);
                foreach (KeyValuePair<string, string> kvp in recipe.tablereqs)
                {
                    tablereqs.Children.Add(new TreeViewItem(kvp.Key + ": " + kvp.Value));
                }
            }
            if (recipe.extantreqs != null)
            {
                TreeViewItem extantreqs = new TreeViewItem("ExtantRequirements:");
                tempTvn.RootItems.Add(extantreqs);
                foreach (KeyValuePair<string, string> kvp in recipe.extantreqs)
                {
                    extantreqs.Children.Add(new TreeViewItem(kvp.Key + ": " + kvp.Value));
                }
            }
            if (recipe.effects != null)
            {
                TreeViewItem effects = new TreeViewItem("Card Effects:");
                tempTvn.RootItems.Add(effects);
                foreach (KeyValuePair<string, string> kvp in recipe.effects)
                {
                    effects.Children.Add(new TreeViewItem(kvp.Key + ": " + kvp.Value));
                }
            }
            if (recipe.aspects != null)
            {
                TreeViewItem aspects = new TreeViewItem("Aspect Effects:");
                tempTvn.RootItems.Add(aspects);
                foreach (KeyValuePair<string, int> kvp in recipe.aspects)
                {
                    aspects.Children.Add(new TreeViewItem(kvp.Key + ": " + kvp.Value.ToString()));
                }
            }
            if (recipe.deckeffects != null)
            {
                TreeViewItem deckeffect = new TreeViewItem("Deck Effects:");
                tempTvn.RootItems.Add(deckeffect);
                foreach (KeyValuePair<string, int> kvp in recipe.deckeffects)
                {
                    deckeffect.Children.Add(new TreeViewItem(kvp.Key + ": " + kvp.Value.ToString()));
                }
            }
            if (recipe.haltverb != null)
            {
                TreeViewItem effects = new TreeViewItem("Halt Verb:");
                tempTvn.RootItems.Add(effects);
                foreach (KeyValuePair<string, string> kvp in recipe.effects)
                {
                    effects.Children.Add(new TreeViewItem(kvp.Key + ": " + kvp.Value));
                }
            }
            if (recipe.deleteverb != null)
            {
                TreeViewItem effects = new TreeViewItem("Delete Verb:");
                tempTvn.RootItems.Add(effects);
                foreach (KeyValuePair<string, string> kvp in recipe.effects)
                {
                    effects.Children.Add(new TreeViewItem(kvp.Key + ": " + kvp.Value));
                }
            }
            if (recipe.purge != null)
            {
                TreeViewItem effects = new TreeViewItem("Purge Cards:");
                tempTvn.RootItems.Add(effects);
                foreach (KeyValuePair<string, string> kvp in recipe.effects)
                {
                    effects.Children.Add(new TreeViewItem(kvp.Key + ": " + kvp.Value));
                }
            }
            if (recipe.linked != null)
            {
                TreeViewItem linked = new TreeViewItem("Linked Recipes:");
                tempTvn.RootItems.Add(linked);
                foreach (RecipeLink link in recipe.linked)
                {
                    TreeViewItem recipeTree = new TreeViewItem("Linked Recipe:");
                    linked.Children.Add(recipeTree);
                    //linkedNodes.Add(link.id, tempTVN);
                    outgoingLinks.Add(link.id);
                    if (link.id != null)
                    {
                        recipeTree.Children.Add(new TreeViewItem("Recipe ID: " + link.id));
                    }

                    if (link.chance.HasValue)
                    {
                        recipeTree.Children.Add(new TreeViewItem("Chance: " + link.chance.Value.ToString()));
                    }

                    if (link.additional.HasValue)
                    {
                        recipeTree.Children.Add(new TreeViewItem("Additional: " + link.additional.Value.ToString()));
                    }

                    if (link.expulsion != null)
                    {
                        TreeViewItem expulsion = new TreeViewItem("Expulsions: ");
                        recipeTree.Children.Add(expulsion);
                        expulsion.Children.Add(new TreeViewItem("Limit: " + link.expulsion.limit.ToString()));
                        foreach (KeyValuePair<string, int> kvp in link.expulsion.filter)
                        {
                            expulsion.Children.Add(new TreeViewItem(kvp.Key + ": " + kvp.Value));
                        }
                    }
                    if (link.challenges != null)
                    {
                        TreeViewItem challenges = new TreeViewItem("Challenges: ");
                        recipeTree.Children.Add(challenges);
                        foreach (KeyValuePair<string, string> kvp in link.challenges)
                        {
                            challenges.Children.Add(new TreeViewItem(kvp.Key + ": " + kvp.Value));
                        }
                    }
                }
            }
            if (recipe.alt != null)
            {
                TreeViewItem linked = new TreeViewItem("Alternative Recipes:");
                tempTvn.RootItems.Add(linked);
                foreach (RecipeLink link in recipe.alt)
                {
                    TreeViewItem recipeTree = new TreeViewItem("Linked Recipe:");
                    linked.Children.Add(recipeTree);
                    //linkedNodes.Add(link.id, tempTVN);
                    outgoingLinks.Add(link.id);
                    if (link.id != null)
                    {
                        recipeTree.Children.Add(new TreeViewItem("Recipe ID: " + link.id));
                    }

                    if (link.chance.HasValue)
                    {
                        recipeTree.Children.Add(new TreeViewItem("Chance: " + link.chance.Value.ToString()));
                    }

                    if (link.additional.HasValue)
                    {
                        recipeTree.Children.Add(new TreeViewItem("Additional: " + link.additional.Value.ToString()));
                    }

                    if (link.expulsion != null)
                    {
                        TreeViewItem expulsion = new TreeViewItem("Expulsions: ");
                        recipeTree.Children.Add(expulsion);
                        expulsion.Children.Add(new TreeViewItem("Limit: " + link.expulsion.limit.ToString()));
                        foreach (KeyValuePair<string, int> kvp in link.expulsion.filter)
                        {
                            expulsion.Children.Add(new TreeViewItem(kvp.Key + ": " + kvp.Value));
                        }
                    }
                    if (link.challenges != null)
                    {
                        TreeViewItem challenges = new TreeViewItem("Challenges: ");
                        recipeTree.Children.Add(challenges);
                        foreach (KeyValuePair<string, string> kvp in link.challenges)
                        {
                            challenges.Children.Add(new TreeViewItem(kvp.Key + ": " + kvp.Value));
                        }
                    }
                }
            }
            if (recipe.mutations != null)
            {
                TreeViewItem mutations = new TreeViewItem("Mutations: ");
                tempTvn.RootItems.Add(mutations);
                foreach (Mutation mutation in recipe.mutations)
                {
                    TreeViewItem mutationItem = new TreeViewItem("Mutation: ");
                    mutationItem.Children.Add(new TreeViewItem("Aspect Filter: " + mutation.filter));
                    mutationItem.Children.Add(new TreeViewItem("Aspect Affected: " + mutation.mutate));
                    mutationItem.Children.Add(new TreeViewItem("Mutation Level: " + mutation.level.ToString()));
                    mutationItem.Children.Add(new TreeViewItem("Additive Mutation: " + mutation.additive.ToString()));
                    mutations.Children.Add(mutationItem);
                }
            }
            tempTvn.ResizeToFitText();
            return new RecipeNode(tempTvn, outgoingLinks);
        }

        private void RecipeFlowchartViewer_Load(object sender, EventArgs e)
        {

        }

        private void LoadLinkedRecipesButton_Click(object sender, EventArgs e)
        {
            if (recipeNodesThatNeedToBeProcessed.Count == 0 || recipeNodesThatNeedToBeProcessed.Sum(item => item.LinkedRecipes.Count) == 0)
            {
                return;
            }

            if (!skipYesNo)
            {
                switch (MessageBox.Show("There are currently " + recipeNodes.Count.ToString() + " Recipe Nodes currently loaded and " + recipeNodesThatNeedToBeProcessed.Sum(item => item.LinkedRecipes.Count) + " more that you are attempting to load. As you load more Recipe Nodes, loading becomes slower and slower. Are you sure you want to continue? Pressing Cancel will supprress this warning. There is a config setting that can be enabled to load all nodes at once when this form is opened, completely bypassing this.", "Loading More Recipes", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Cancel:
                        skipYesNo = true;
                        ProcessRecipes();
                        break;
                    case DialogResult.Yes:
                        ProcessRecipes();
                        break;
                }
            }
            else
            {
                ProcessRecipes();
            }
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = Utilities.BaseDirectory;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Bitmap exportedImage = diagram1.CreateImage(diagram1.Bounds, 100f);
                exportedImage.Save(saveFileDialog1.FileName, ImageFormat.Png);
            }
        }
    }

    public class RecipeNode
    {
        public TreeViewNode Node;
        public List<string> LinkedRecipes;

        public RecipeNode(TreeViewNode node, List<string> linkedRecipes)
        {
            this.Node = node;
            this.LinkedRecipes = linkedRecipes;
        }
    }
}
