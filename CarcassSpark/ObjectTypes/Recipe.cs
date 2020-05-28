using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassSpark.ObjectTypes
{
    public class Recipe
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id, label, actionId, startdescription, description, ending, burnimage, portaleffect, signalendingflavour;
        // craftable has to be true in order for the player to initiate the recipe
        // false means the recipe is linked to by another recipe somehow
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? craftable, hintonly, signalimportantloop;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Deck internalDeck;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> extends;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? maxexecutions, warmup;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "maxexecutions$add")]
        public int? maxexecutions_add;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "maxexecutions$minus")]
        public int? maxexecutions_minus;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "warmup$add")]
        public int? warmup_add;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "warmup$minus")]
        public int? warmup_minus;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> effects;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "effects$extend")]
        public Dictionary<string, string> effects_extend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "effects$remove")]
        public List<string> effects_remove;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, int> aspects;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "aspects$extend")]
        public Dictionary<string, int> aspects_extend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "aspects$remove")]
        public List<string> aspects_remove;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, int> deckeffect;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "deckeffect$extend")]
        public Dictionary<string, int> deckeffect_extend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "deckeffect$remove")]
        public List<string> deckeffect_remove;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> requirements;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "requirements$extend")]
        public Dictionary<string, string> requirements_extend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "requirements$remove")]
        public List<string> requirements_remove;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> extantreqs;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "extantreqs$extend")]
        public Dictionary<string, string> extantreqs_extend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "extantreqs$remove")]
        public List<string> extantreqs_remove;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> tablereqs;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "tablereqs$extend")]
        public Dictionary<string, string> tablereqs_extend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "tablereqs$remove")]
        public List<string> tablereqs_remove;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<RecipeLink> linked;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "linked$append")]
        public List<RecipeLink> linked_append;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "linked$prepend")]
        public List<RecipeLink> linked_prepend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "linked$remove")]
        public List<string> linked_remove;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<RecipeLink> alternativerecipes;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "alternativerecipes$append")]
        public List<RecipeLink> alternativerecipes_append;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "alternativerecipes$prepend")]
        public List<RecipeLink> alternativerecipes_prepend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "alternativerecipes$remove")]
        public List<string> alternativerecipes_remove;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Slot> slots;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Mutation> mutations;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "mutations$append")]
        public List<Mutation> mutations_append;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "mutations$prepend")]
        public List<Mutation> mutations_prepend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "mutations$remove")]
        public List<string> mutations_remove;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, int> purge;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, int> haltverb;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, int> deleteverb;


        [JsonConstructor]
        public Recipe(bool? craftable, bool? hintonly, int? warmup, int? warmup_add, int? warmup_minus, int? maxexecutions, int? maxexecutions_add, int? maxexecutions_minus,
                      string actionId, string startdescription, string description, string id, string label,
                      List<string> extends, Deck internaldeck, string ending, string burnimage,
                      Dictionary<string, string> requirements, Dictionary<string, string> requirements_extend, List<string> requirements_remove,
                      Dictionary<string, string> effects, Dictionary<string, string> effects_extend, List<string> effects_remove,
                      List<RecipeLink> linked, List<RecipeLink> linked_prepend, List<RecipeLink> linked_append, List<string> linked_remove, List<Slot> slots,
                      List<RecipeLink> alternativerecipes, List<RecipeLink> alternativerecipes_prepend, List<RecipeLink> alternativerecipes_append, List<string> alternativerecipes_remove,
                      Dictionary<string, int> deckeffect, Dictionary<string, int> deckeffect_extend, List<string> deckeffect_remove,
                      List<Mutation> mutations, List<Mutation> mutations_prepend, List<Mutation> mutations_append, List<string> mutations_remove,
                      Dictionary<string, int> aspects, Dictionary<string, int> aspects_extend, List<string> aspects_remove,
                      Dictionary<string, string> tablereqs, Dictionary<string, string> tablereqs_extend, List<string> tablereqs_remove,
                      Dictionary<string, string> extantreqs, Dictionary<string, string> extantreqs_extend, List<string> extantreqs_remove)
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
            if (extends != null) this.extends = extends;
            if (warmup.HasValue) this.warmup = warmup;
            if (warmup_add.HasValue) this.warmup_add = warmup_add;
            if (warmup_minus.HasValue) this.warmup_minus = warmup_minus;
            if (requirements != null) this.requirements = requirements;
            if (requirements_extend != null) this.requirements_extend = requirements_extend;
            if (requirements_remove != null) this.requirements_remove = requirements_remove;
            if (extantreqs != null) this.extantreqs = extantreqs;
            if (extantreqs_extend != null) this.extantreqs_extend = extantreqs_extend;
            if (extantreqs_remove != null) this.extantreqs_remove = extantreqs_remove;
            if (tablereqs != null) this.tablereqs = tablereqs;
            if (tablereqs_extend != null) this.tablereqs_extend = tablereqs_extend;
            if (tablereqs_remove != null) this.tablereqs_remove = tablereqs_remove;
            if (maxexecutions.HasValue) this.maxexecutions = maxexecutions;
            if (maxexecutions_add.HasValue) this.maxexecutions_add = maxexecutions_add;
            if (maxexecutions_minus.HasValue) this.maxexecutions_minus = maxexecutions_minus;
            if (effects != null) this.effects = effects;
            if (effects_extend != null) this.effects_extend = effects_extend;
            if (effects_remove != null) this.effects_remove = effects_remove;
            if (linked != null) this.linked = linked;
            if (linked_prepend != null) this.linked_prepend = linked_prepend;
            if (linked_append != null) this.linked_append = linked_append;
            if (linked_remove != null) this.linked_remove = linked_remove;
            if (slots != null) this.slots = slots;
            if (alternativerecipes != null) this.alternativerecipes = alternativerecipes;
            if (alternativerecipes_prepend != null) this.alternativerecipes_prepend = alternativerecipes_prepend;
            if (alternativerecipes_append != null) this.alternativerecipes_append = alternativerecipes_append;
            if (alternativerecipes_remove != null) this.alternativerecipes_remove = alternativerecipes_remove;
            if (mutations != null) this.mutations = mutations;
            if (mutations_prepend != null) this.mutations_prepend = mutations_prepend;
            if (mutations_append != null) this.mutations_append = mutations_append;
            if (mutations_remove != null) this.mutations_remove = mutations_remove;
            if (aspects != null) this.aspects = aspects;
            if (aspects_extend != null) this.aspects_extend = aspects_extend;
            if (aspects_remove != null) this.aspects_remove = aspects_remove;
            if (deckeffect != null) this.deckeffect = deckeffect;
            if (deckeffect_extend != null) this.deckeffect_extend = deckeffect_extend;
            if (deckeffect_remove != null) this.deckeffect_remove = deckeffect_remove;
            if (hintonly.HasValue) this.hintonly = hintonly.Value;
            if (internaldeck != null) this.internalDeck = internaldeck;

        }

        public Recipe()
        {

        }
    }
}
