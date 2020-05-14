using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultistSimulatorModdingToolkit.ObjectTypes
{
    public class Recipe
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id, label, actionId, startdescription, description, ending, burnimage;
        // craftable has to be true in order for the player to initiate the recipe
        // false means the recipe is linked to by another recipe somehow
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? craftable, hintonly;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? maxexecutions, warmup;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, int> effects, requirements, extantreqs, tablereqs;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, int> aspects;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<RecipeLink> linked, alternativerecipes;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Slot> slots;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Mutation> mutations;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Deck internalDeck;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, int> deckeffect;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> extends;

        [JsonConstructor]
        public Recipe(bool? craftable, bool? hintonly, int? warmup, int? maxexecutions,
                      string id, string label = null, string actionId = null, string startdescription = null,
                      string description = null, JArray extends = null, JObject requirements = null,
                      JObject effects = null, JArray linked = null, JArray slots = null, JArray alternativerecipes = null,
                      JObject deckeffect = null, JObject internaldeck = null, JArray mutations = null,
                      JObject aspects = null, JObject tablereqs = null, JObject extantreqs = null, string ending = null,
                      string burnimage = null)
        {
            this.id = id;
            this.label = label;
            this.actionId = actionId;
            this.startdescription = startdescription;
            this.description = description;
            if (craftable.HasValue) this.craftable = craftable;
            if (hintonly.HasValue) this.hintonly = hintonly;
            if (ending != null) this.ending = ending;
            if (burnimage != null) this.burnimage = burnimage;
            if (extends != null) this.extends = extends.ToObject<List<string>>();
            if (warmup.HasValue) this.warmup = warmup;
            if (requirements != null) this.requirements = requirements.ToObject<Dictionary<string, int>>();
            if (extantreqs != null) this.extantreqs = extantreqs.ToObject<Dictionary<string, int>>();
            if (tablereqs != null) this.tablereqs = tablereqs.ToObject<Dictionary<string, int>>();
            if (maxexecutions.HasValue) this.maxexecutions = maxexecutions;
            if (effects != null)
            {
                this.effects = effects.ToObject<Dictionary<string, int>>();
            }
            if (linked != null)
            {
                this.linked = linked.ToObject<List<RecipeLink>>();
            }
            if (slots != null)
            {
                this.slots = slots.ToObject<List<Slot>>();
            }
            if (alternativerecipes != null)
            {
                this.alternativerecipes = alternativerecipes.ToObject<List<RecipeLink>>();
            }
            if (mutations != null)
            {
                this.mutations = mutations.ToObject<List<Mutation>>();
            }
            if (aspects != null)
            {
                this.aspects = aspects.ToObject<Dictionary<string, int>>();
            }
            if (deckeffect != null)
            {
                this.deckeffect = deckeffect.ToObject<Dictionary<string, int>>();
            }
            if (hintonly.HasValue)
            {
                this.hintonly = hintonly.Value;
            }
            if (internaldeck != null)
            {
                this.internalDeck = internaldeck.ToObject<Deck>();
            }

        }

        public Recipe()
        {

        }
    }
}
