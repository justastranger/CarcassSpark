using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Cultist_Simulator_Modding_Toolkit.Element;

namespace Cultist_Simulator_Modding_Toolkit
{
    public class Recipe
    {
        public string id, label, actionId, startdescription, description, ending, burnimage;
        // craftable has to be true in order for the player to initiate the recipe
        // false means the recipe is linked to by another recipe somehow
        public bool craftable, hintonly;
        public int maxexecutions, warmup;
        public ElementDictionary effects, requirements, extantreqs, tablereqs;
        public AspectDictionary aspects;
        public RecipeLink[] linked, alternativerecipes;
        public Slot[] slots;
        public Mutation[] mutations;
        public Deck internalDeck;
        public Dictionary<string, int> deckeffect;

        [JsonConstructor]
        public Recipe(string id, string label, string actionId, string startdescription, string description,
                      bool craftable, bool hintonly = false, JObject requirements = null, int warmup = 0, int maxexecutions = 0, JObject effects = null,
                      JArray linked = null, JArray slots = null, JArray alternativerecipes = null, JObject deckeffect = null, JObject internaldeck = null,
                      JArray mutations = null, JObject aspects = null, JObject tablereqs = null, JObject extantreqs = null, string ending = null, string burnimage = null)
        {
            this.id = id;
            this.label = label;
            this.actionId = actionId;
            this.startdescription = startdescription;
            this.description = description;
            this.craftable = craftable;
            this.ending = ending;
            this.burnimage = burnimage;
            if (warmup > 0) this.warmup = warmup;
            if (requirements != null) this.requirements = requirements.ToObject<ElementDictionary>();
            if (extantreqs != null) this.extantreqs = extantreqs.ToObject<ElementDictionary>();
            if (tablereqs != null) this.tablereqs = tablereqs.ToObject<ElementDictionary>();
            if (maxexecutions > 0) this.maxexecutions = maxexecutions;
            if (effects != null)
            {
                this.effects = new ElementDictionary(effects);
            }
            if (linked != null)
            {
                this.linked = linked.ToObject<RecipeLink[]>();
            }
            if (slots != null)
            {
                this.slots = slots.ToObject<Slot[]>();
            }
            if (alternativerecipes != null)
            {
                this.alternativerecipes = alternativerecipes.ToObject<RecipeLink[]>();
            }
            if (mutations != null)
            {
                this.mutations = mutations.ToObject<Mutation[]>();
            }
            if (aspects != null)
            {
                this.aspects = new AspectDictionary(aspects);
            }
            if (deckeffect != null)
            {
                this.deckeffect = deckeffect.ToObject<Dictionary<string, int>>();
            }
            if (hintonly)
            {
                this.hintonly = hintonly;
            }
            if (internaldeck != null)
            {
                this.internalDeck = internaldeck.ToObject<Deck>();
            }

        }

        public class RecipeLink
        {
            public string id;
            public int chance;
            public bool additional;
            public Dictionary<string, string> challenges;

            [JsonConstructor]
            public RecipeLink(string id, int chance = 100, bool additional = false, JObject challenges = null)
            {
                this.id = id;
                this.chance = chance;
                if (additional) this.additional = additional;
                if (challenges != null)
                {
                    this.challenges = challenges.ToObject<Dictionary<string, string>>();
                }
            }
        }

        public class Mutation
        {
            public string filter; // element ID to use to select a card, can filter based on aspect or card itself
            public string mutateAspectId; // Aspect on filtered card to modify
            public int level; // how much to modify the aspect by

            public Mutation(string filter, int level, string mutateAspectId = null, string mutate = null)
            {
                this.filter = filter;
                this.mutateAspectId = mutateAspectId != null ? mutateAspectId : mutate;
                this.level = level;
            }
        }
    }
}
