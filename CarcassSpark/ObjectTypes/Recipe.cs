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
        public string id, label, actionId, startdescription, description, ending, burnimage, portaleffect, signalendingflavour, comments;
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
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "purge$extend")]
        public Dictionary<string, int> purge_extend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "purge$remove")]
        public List<string> purge_remove;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, int> haltverb;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "haltverb$extend")]
        public Dictionary<string, int> haltverb_extend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "haltverb$remove")]
        public List<string> haltverb_remove;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, int> deleteverb;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "deleteverb$extend")]
        public Dictionary<string, int> deleteverb_extend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "deleteverb$remove")]
        public List<string> deleteverb_remove;


        [JsonConstructor]
        public Recipe(bool? craftable, bool? hintonly, int? warmup, int? warmup_add, int? warmup_minus, int? maxexecutions, int? maxexecutions_add, int? maxexecutions_minus,
                      string actionId, string startdescription, string description, string id, string label, string signalendingflavour, string portaleffect, bool? signalimportantloop,
                      List<string> extends, Deck internalDeck, string ending, string burnimage, string comments,
                      Dictionary<string, string> requirements, Dictionary<string, string> requirements_extend, List<string> requirements_remove,
                      Dictionary<string, string> effects, Dictionary<string, string> effects_extend, List<string> effects_remove,
                      List<RecipeLink> linked, List<RecipeLink> linked_prepend, List<RecipeLink> linked_append, List<string> linked_remove, List<Slot> slots,
                      List<RecipeLink> alternativerecipes, List<RecipeLink> alternativerecipes_prepend, List<RecipeLink> alternativerecipes_append, List<string> alternativerecipes_remove,
                      Dictionary<string, int> deckeffect, Dictionary<string, int> deckeffect_extend, List<string> deckeffect_remove,
                      List<Mutation> mutations, List<Mutation> mutations_prepend, List<Mutation> mutations_append, List<string> mutations_remove,
                      Dictionary<string, int> aspects, Dictionary<string, int> aspects_extend, List<string> aspects_remove,
                      Dictionary<string, string> tablereqs, Dictionary<string, string> tablereqs_extend, List<string> tablereqs_remove,
                      Dictionary<string, string> extantreqs, Dictionary<string, string> extantreqs_extend, List<string> extantreqs_remove,
                      Dictionary<string, int> purge, Dictionary<string, int> purge_extend, List<string> purge_remove,
                      Dictionary<string, int> haltverb, Dictionary<string, int> haltverb_extend, List<string> haltverb_remove,
                      Dictionary<string, int> deleteverb, Dictionary<string, int> deleteverb_extend, List<string> deleteverb_remove)
        {
            this.id = id;
            this.label = label;
            this.actionId = actionId;
            this.startdescription = startdescription;
            this.description = description;
            this.comments = comments;
            this.craftable = craftable;
            this.hintonly = hintonly;
            this.ending = ending;
            this.burnimage = burnimage;
            this.extends = extends;
            this.warmup = warmup;
            this.warmup_add = warmup_add;
            this.warmup_minus = warmup_minus;
            this.requirements = requirements;
            this.requirements_extend = requirements_extend;
            this.requirements_remove = requirements_remove;
            this.extantreqs = extantreqs;
            this.extantreqs_extend = extantreqs_extend;
            this.extantreqs_remove = extantreqs_remove;
            this.tablereqs = tablereqs;
            this.tablereqs_extend = tablereqs_extend;
            this.tablereqs_remove = tablereqs_remove;
            this.maxexecutions = maxexecutions;
            this.maxexecutions_add = maxexecutions_add;
            this.maxexecutions_minus = maxexecutions_minus;
            this.effects = effects;
            this.effects_extend = effects_extend;
            this.effects_remove = effects_remove;
            this.linked = linked;
            this.linked_prepend = linked_prepend;
            this.linked_append = linked_append;
            this.linked_remove = linked_remove;
            this.slots = slots;
            this.alternativerecipes = alternativerecipes;
            this.alternativerecipes_prepend = alternativerecipes_prepend;
            this.alternativerecipes_append = alternativerecipes_append;
            this.alternativerecipes_remove = alternativerecipes_remove;
            this.mutations = mutations;
            this.mutations_prepend = mutations_prepend;
            this.mutations_append = mutations_append;
            this.mutations_remove = mutations_remove;
            this.aspects = aspects;
            this.aspects_extend = aspects_extend;
            this.aspects_remove = aspects_remove;
            this.deckeffect = deckeffect;
            this.deckeffect_extend = deckeffect_extend;
            this.deckeffect_remove = deckeffect_remove;
            this.hintonly = hintonly;
            this.internalDeck = internalDeck;
            this.purge = purge;
            this.purge_extend = purge_extend;
            this.purge_remove = purge_remove;
            this.haltverb = haltverb;
            this.haltverb_extend = haltverb_extend;
            this.haltverb_remove = haltverb_remove;
            this.deleteverb = deleteverb;
            this.deleteverb_extend = deleteverb_extend;
            this.deleteverb_remove = deleteverb_remove;
            this.portaleffect = portaleffect;
            this.signalendingflavour = signalendingflavour;
            this.signalimportantloop = signalimportantloop;
        }

        public Recipe()
        {

        }

        public Recipe Copy()
        {
            Recipe tmp = new Recipe();
            tmp.id = id;
            tmp.label = label;
            tmp.actionId = actionId;
            tmp.startdescription = startdescription;
            tmp.description = description;
            tmp.comments = comments;
            tmp.craftable = craftable;
            tmp.hintonly = hintonly;
            tmp.ending = ending;
            tmp.burnimage = burnimage;
            tmp.extends = extends;
            tmp.warmup = warmup;
            tmp.warmup_add = warmup_add;
            tmp.warmup_minus = warmup_minus;
            tmp.requirements = requirements;
            tmp.requirements_extend = requirements_extend;
            tmp.requirements_remove = requirements_remove;
            tmp.extantreqs = extantreqs;
            tmp.extantreqs_extend = extantreqs_extend;
            tmp.extantreqs_remove = extantreqs_remove;
            tmp.tablereqs = tablereqs;
            tmp.tablereqs_extend = tablereqs_extend;
            tmp.tablereqs_remove = tablereqs_remove;
            tmp.maxexecutions = maxexecutions;
            tmp.maxexecutions_add = maxexecutions_add;
            tmp.maxexecutions_minus = maxexecutions_minus;
            tmp.effects = effects;
            tmp.effects_extend = effects_extend;
            tmp.effects_remove = effects_remove;
            tmp.linked = linked;
            tmp.linked_prepend = linked_prepend;
            tmp.linked_append = linked_append;
            tmp.linked_remove = linked_remove;
            tmp.slots = slots;
            tmp.alternativerecipes = alternativerecipes;
            tmp.alternativerecipes_prepend = alternativerecipes_prepend;
            tmp.alternativerecipes_append = alternativerecipes_append;
            tmp.alternativerecipes_remove = alternativerecipes_remove;
            tmp.mutations = mutations;
            tmp.mutations_prepend = mutations_prepend;
            tmp.mutations_append = mutations_append;
            tmp.mutations_remove = mutations_remove;
            tmp.aspects = aspects;
            tmp.aspects_extend = aspects_extend;
            tmp.aspects_remove = aspects_remove;
            tmp.deckeffect = deckeffect;
            tmp.deckeffect_extend = deckeffect_extend;
            tmp.deckeffect_remove = deckeffect_remove;
            tmp.hintonly = hintonly;
            tmp.internalDeck = internalDeck;
            tmp.purge = purge;
            tmp.purge_extend = purge_extend;
            tmp.purge_remove = purge_remove;
            tmp.haltverb = haltverb;
            tmp.haltverb_extend = haltverb_extend;
            tmp.haltverb_remove = haltverb_remove;
            tmp.deleteverb = deleteverb;
            tmp.deleteverb_extend = deleteverb_extend;
            tmp.deleteverb_remove = deleteverb_remove;
            tmp.portaleffect = portaleffect;
            tmp.signalendingflavour = signalendingflavour;
            tmp.signalimportantloop = signalimportantloop;
            return tmp;
        }
    }
}
