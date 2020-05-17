using CultistSimulatorModdingToolkit.ObjectTypes;
using MindFusion.Diagramming;
using MindFusion.Diagramming.Layout;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CultistSimulatorModdingToolkit
{
    public partial class RecipeFlowchartViewer : Form
    {
        Dictionary<string, RecipeNode> recipeNodes = new Dictionary<string, RecipeNode>();

        List<RecipeNode> recipeNodesThatNeedToBeProcessed = new List<RecipeNode>();
        TreeViewNode RootNode;
        
        public RecipeFlowchartViewer(Recipe recipe)
        {
            InitializeComponent();
            int x = 5;
            int y = 5;
            RecipeNode RootRecipeNode = createRecipeNode(recipe, x, y);
            recipeNodes.Add(recipe.id, RootRecipeNode);
            recipeNodesThatNeedToBeProcessed.Add(RootRecipeNode);
            RootNode = RootRecipeNode.Node;
            while(recipeNodesThatNeedToBeProcessed.Count > 0)
            {
                RecipeNode currentNode = recipeNodesThatNeedToBeProcessed[0];

                foreach (string link in currentNode.linkedRecipes)
                {
                    // if a node for this recipe doesn't already exist, create it and link to it
                    if (!recipeNodes.ContainsKey(link))
                    {
                        RecipeNode tmpNode = createRecipeNode(Utilities.getRecipe(link), x, y);
                        recipeNodes[link] = tmpNode;
                        createLink(currentNode.Node, tmpNode.Node);
                        recipeNodesThatNeedToBeProcessed.Add(tmpNode);
                        //y += 200;
                    }
                    else // otherwise if it exists already, just link to it
                    {
                        createLink(currentNode.Node, recipeNodes[link].Node);
                    }
                }
                recipeNodesThatNeedToBeProcessed.Remove(currentNode);
            }
            treeLayout1.Arrange(diagram1, diagram1.Items);
        }

        void createLink(TreeViewNode outgoing, TreeViewNode incoming)
        {
            DiagramLink tmp = diagram1.Factory.CreateDiagramLink(outgoing, incoming);
            tmp.AutoRoute = true;
        }

        RecipeNode createRecipeNode(Recipe recipe, float x, float y)
        {
            //if (recipeNodes.ContainsKey(recipe.id))
            //{
            //    MessageBox.Show("createRecipeNode called on a recipe that's already been created.");
            //    throw new Exception();
            //}
            //Dictionary<string, TreeViewItem> tmp = new Dictionary<string, TreeViewItem>();
            List<string> outgoingLinks = new List<string>();
            TreeViewNode tempTVN = diagram1.Factory.CreateTreeViewNode(x, y, 150, 150);
            tempTVN.Id = recipe.id;
            if (recipe.label != null) tempTVN.Text = recipe.label;
            else tempTVN.Text = recipe.id;
            tempTVN.RootItems.Add(new TreeViewItem("Recipe ID: " + recipe.id));
            if (recipe.actionId != null) tempTVN.RootItems.Add(new TreeViewItem("Action ID: " + recipe.actionId));
            if (recipe.startdescription != null)
            {
                tempTVN.RootItems.Add(new TreeViewItem("Recipe Start Description: "));
                tempTVN.RootItems.Last().Children.Add(new TreeViewItem(recipe.startdescription));
            }
            if (recipe.description != null)
            {
                tempTVN.RootItems.Add(new TreeViewItem("Recipe Description: "));
                tempTVN.RootItems.Last().Children.Add(new TreeViewItem(recipe.description));
            }
            if (recipe.ending != null) tempTVN.RootItems.Add(new TreeViewItem("Action ID: " + recipe.ending));
            if (recipe.burnimage != null) tempTVN.RootItems.Add(new TreeViewItem("Burn Image: " + recipe.burnimage));
            if (recipe.extends != null) tempTVN.RootItems.Add(new TreeViewItem("Extends: " + recipe.extends));
            if (recipe.craftable.HasValue) tempTVN.RootItems.Add(new TreeViewItem("Craftable: " + recipe.craftable.Value.ToString()));
            if (recipe.hintonly.HasValue) tempTVN.RootItems.Add(new TreeViewItem("Hint Only: " + recipe.hintonly.Value.ToString()));
            if (recipe.maxexecutions.HasValue) tempTVN.RootItems.Add(new TreeViewItem("Max Executions: " + recipe.maxexecutions.Value.ToString()));
            if (recipe.warmup.HasValue) tempTVN.RootItems.Add(new TreeViewItem("Warmup: " + recipe.warmup.Value.ToString()));
            if (recipe.internalDeck != null)
            {
                tempTVN.RootItems.Add(new TreeViewItem("Internal Deck: "));
                if (recipe.internalDeck.label != null) tempTVN.RootItems.Last().Children.Add(new TreeViewItem("Label: " + recipe.internalDeck.label));
                if (recipe.internalDeck.description != null)
                {
                    tempTVN.RootItems.Last().Children.Add(new TreeViewItem("Description: "));
                    tempTVN.RootItems.Last().Children.Last().Children.Add(new TreeViewItem(recipe.internalDeck.description));
                }
                if (recipe.internalDeck.draws.HasValue) tempTVN.RootItems.Last().Children.Add(new TreeViewItem("Draws: " + recipe.internalDeck.draws.Value));
                if (recipe.internalDeck.resetonexhaustion.HasValue) tempTVN.RootItems.Last().Children.Add(new TreeViewItem("Reset on Exhaustion: " + recipe.internalDeck.resetonexhaustion.Value));
                if (recipe.internalDeck.defaultcard != null) tempTVN.RootItems.Last().Children.Add(new TreeViewItem("Default Card: " + recipe.internalDeck.defaultcard));
                if (recipe.internalDeck.spec != null)
                {
                    tempTVN.RootItems.Last().Children.Add(new TreeViewItem("Cards: "));
                    foreach (string card in recipe.internalDeck.spec)
                    {
                        tempTVN.RootItems.Last().Children.Last().Children.Add(new TreeViewItem(card));
                    }
                }
            }
            if (recipe.slots != null)
            {
                Slot slot = recipe.slots[0];
                TreeViewItem slotItem = new TreeViewItem("Recipe Slot");
                tempTVN.RootItems.Add(slotItem);
                // string id
                if (slot.id != null) slotItem.Children.Add(new TreeViewItem("ID: " + slot.id));
                // string label
                if (slot.label != null) slotItem.Children.Add(new TreeViewItem("Label: " + slot.label));
                // string description
                if (slot.description != null)
                {
                    slotItem.Children.Add(new TreeViewItem("Description: " + slot.id));
                    slotItem.Children.Last().Children.Add(new TreeViewItem(slot.description));
                }
                // string actionId
                if (slot.actionId != null) slotItem.Children.Add(new TreeViewItem("Action ID: " + slot.actionId));
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
                if (slot.greedy.HasValue) slotItem.Children.Add(new TreeViewItem("Greedy: " + slot.greedy.Value.ToString()));
                // bool? consumes
                if (slot.consumes.HasValue) slotItem.Children.Add(new TreeViewItem("Consumes: " + slot.consumes.Value.ToString()));
            }
            if (recipe.requirements != null)
            {
                TreeViewItem requirements = new TreeViewItem("Requirements:");
                tempTVN.RootItems.Add(requirements);
                foreach (KeyValuePair<string, int> kvp in recipe.requirements)
                {
                    requirements.Children.Add(new TreeViewItem(kvp.Key + ": " + kvp.Value.ToString()));
                }
            }
            if (recipe.tablereqs != null)
            {
                TreeViewItem tablereqs = new TreeViewItem("Table Requirements:");
                tempTVN.RootItems.Add(tablereqs);
                foreach (KeyValuePair<string, int> kvp in recipe.tablereqs)
                {
                    tablereqs.Children.Add(new TreeViewItem(kvp.Key + ": " + kvp.Value.ToString()));
                }
            }
            if (recipe.extantreqs != null)
            {
                TreeViewItem extantreqs = new TreeViewItem("ExtantRequirements:");
                tempTVN.RootItems.Add(extantreqs);
                foreach (KeyValuePair<string, int> kvp in recipe.extantreqs)
                {
                    extantreqs.Children.Add(new TreeViewItem(kvp.Key + ": " + kvp.Value.ToString()));
                }
            }
            if (recipe.effects != null)
            {
                TreeViewItem effects = new TreeViewItem("Card Effects:");
                tempTVN.RootItems.Add(effects);
                foreach (KeyValuePair<string, int> kvp in recipe.effects)
                {
                    effects.Children.Add(new TreeViewItem(kvp.Key + ": " + kvp.Value.ToString()));
                }
            }
            if (recipe.aspects != null)
            {
                TreeViewItem aspects = new TreeViewItem("Aspect Effects:");
                tempTVN.RootItems.Add(aspects);
                foreach (KeyValuePair<string, int> kvp in recipe.aspects)
                {
                    aspects.Children.Add(new TreeViewItem(kvp.Key + ": " + kvp.Value.ToString()));
                }
            }
            if (recipe.deckeffect != null)
            {
                TreeViewItem deckeffect = new TreeViewItem("Deck Effects:");
                tempTVN.RootItems.Add(deckeffect);
                foreach (KeyValuePair<string, int> kvp in recipe.deckeffect)
                {
                    deckeffect.Children.Add(new TreeViewItem(kvp.Key + ": " + kvp.Value.ToString()));
                }
            }
            if (recipe.linked != null)
            {
                TreeViewItem linked = new TreeViewItem("Linked Recipes:");
                tempTVN.RootItems.Add(linked);
                foreach (RecipeLink link in recipe.linked)
                {
                    TreeViewItem recipeTree = new TreeViewItem("Linked Recipe: ");
                    linked.Children.Add(recipeTree);
                    //linkedNodes.Add(link.id, tempTVN);
                    outgoingLinks.Add(link.id);
                    if (link.id != null) recipeTree.Children.Add(new TreeViewItem("Recipe ID: " + link.id));
                    if (link.chance.HasValue) recipeTree.Children.Add(new TreeViewItem("Chance: " + link.chance.Value.ToString()));
                    if (link.additional.HasValue) recipeTree.Children.Add(new TreeViewItem("Additional: " + link.additional.Value.ToString()));
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
            if (recipe.alternativerecipes != null)
            {
                TreeViewItem linked = new TreeViewItem("Alternative Recipes:");
                tempTVN.RootItems.Add(linked);
                foreach (RecipeLink link in recipe.alternativerecipes)
                {
                    TreeViewItem recipeTree = new TreeViewItem("Linked Recipe ID: " + link.id);
                    linked.Children.Add(recipeTree);
                    //linkedNodes.Add(link.id, tempTVN);
                    outgoingLinks.Add(link.id);
                    if (link.chance.HasValue) recipeTree.Children.Add(new TreeViewItem("Chance: " + link.chance.Value.ToString()));
                    if (link.additional.HasValue) recipeTree.Children.Add(new TreeViewItem("Additional: " + link.additional.Value.ToString()));
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
                tempTVN.RootItems.Add(mutations);
                foreach (Mutation mutation in recipe.mutations)
                {
                    TreeViewItem mutationItem = new TreeViewItem("Mutation: ");
                    mutationItem.Children.Add(mutations);
                    mutationItem.Children.Add(new TreeViewItem("Aspect Filter: " + mutation.filterOnAspectId));
                    mutationItem.Children.Add(new TreeViewItem("Aspect Affected: " + mutation.mutateAspectId));
                    mutationItem.Children.Add(new TreeViewItem("Mutation Level: " + mutation.mutationLevel.ToString()));
                    mutationItem.Children.Add(new TreeViewItem("Additive Mutation: " + mutation.additive.ToString()));
                }
            }
            tempTVN.ResizeToFitText();
            return new RecipeNode(tempTVN, outgoingLinks);
        }
        
        private void RecipeFlowchartViewer_Load(object sender, EventArgs e)
        {
        }

        public class RecipeNode
        {
            public TreeViewNode Node;
            public List<string> linkedRecipes;

            public RecipeNode(TreeViewNode Node, List<string> linkedRecipes)
            {
                this.Node = Node;
                this.linkedRecipes = linkedRecipes;
            }
        }
    }
}
