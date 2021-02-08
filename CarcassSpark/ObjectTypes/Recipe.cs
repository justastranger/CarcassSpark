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
        [JsonIgnore]
        public string filename;
        [JsonIgnore]
        public Guid guid = Guid.NewGuid();
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id, label, actionId, startdescription, description, ending, burnimage, portaleffect, signalendingflavour, comments;
        // craftable has to be true in order for the player to initiate the recipe
        // false means the recipe is linked to by another recipe somehow
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? craftable, hintonly, signalimportantloop, deleted;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Deck internaldeck;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? maxexecutions, warmup;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> effects;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "effects$add")]
        public Dictionary<string, string> effects_extend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "effects$remove")]
        public List<string> effects_remove;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, int> aspects;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "aspects$add")]
        public Dictionary<string, int> aspects_extend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "aspects$remove")]
        public List<string> aspects_remove;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, int> deckeffects;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "deckeffects$add")]
        public Dictionary<string, int> deckeffects_extend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "deckeffects$remove")]
        public List<string> deckeffects_remove;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> requirements;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "requirements$add")]
        public Dictionary<string, string> requirements_extend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "requirements$remove")]
        public List<string> requirements_remove;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> extantreqs;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "extantreqs$add")]
        public Dictionary<string, string> extantreqs_extend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "extantreqs$remove")]
        public List<string> extantreqs_remove;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> tablereqs;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "tablereqs$add")]
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
        public List<RecipeLink> alt;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "alt$append")]
        public List<RecipeLink> alt_append;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "alt$prepend")]
        public List<RecipeLink> alt_prepend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "alt$remove")]
        public List<string> alt_remove;

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
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "purge$add")]
        public Dictionary<string, int> purge_extend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "purge$remove")]
        public List<string> purge_remove;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, int> haltverb;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "haltverb$add")]
        public Dictionary<string, int> haltverb_extend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "haltverb$remove")]
        public List<string> haltverb_remove;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, int> deleteverb;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "deleteverb$add")]
        public Dictionary<string, int> deleteverb_extend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "deleteverb$remove")]
        public List<string> deleteverb_remove;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> extends;

        [JsonConstructor]
        public Recipe(bool? craftable, bool? hintonly, bool? deleted, int? warmup, int? maxexecutions,
                      string actionId, string startdescription, string description, string id, string label, string signalendingflavour, string portaleffect, bool? signalimportantloop,
                      Deck internaldeck, string ending, string burnimage, string comments,
                      Dictionary<string, string> requirements, Dictionary<string, string> requirements_extend, List<string> requirements_remove,
                      Dictionary<string, string> effects, Dictionary<string, string> effects_extend, List<string> effects_remove,
                      List<RecipeLink> linked, List<RecipeLink> linked_prepend, List<RecipeLink> linked_append, List<string> linked_remove, List<Slot> slots,
                      List<RecipeLink> alternativerecipes, List<RecipeLink> alt, List<RecipeLink> alt_prepend, List<RecipeLink> alt_append, List<string> alt_remove,
                      Dictionary<string, int> deckeffects, Dictionary<string, int> deckeffects_extend, List<string> deckeffects_remove,
                      List<Mutation> mutations, List<Mutation> mutations_prepend, List<Mutation> mutations_append, List<string> mutations_remove,
                      Dictionary<string, int> aspects, Dictionary<string, int> aspects_extend, List<string> aspects_remove,
                      Dictionary<string, string> tablereqs, Dictionary<string, string> tablereqs_extend, List<string> tablereqs_remove,
                      Dictionary<string, string> extantreqs, Dictionary<string, string> extantreqs_extend, List<string> extantreqs_remove,
                      Dictionary<string, int> purge, Dictionary<string, int> purge_extend, List<string> purge_remove,
                      Dictionary<string, int> haltverb, Dictionary<string, int> haltverb_extend, List<string> haltverb_remove,
                      Dictionary<string, int> deleteverb, Dictionary<string, int> deleteverb_extend, List<string> deleteverb_remove, List<string> extends)
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
            this.deleted = deleted;
            this.warmup = warmup;
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
            this.effects = effects;
            this.effects_extend = effects_extend;
            this.effects_remove = effects_remove;
            this.linked = linked;
            this.linked_prepend = linked_prepend;
            this.linked_append = linked_append;
            this.linked_remove = linked_remove;
            this.slots = slots;
            this.alt = alt ?? alternativerecipes;
            this.alt_prepend = alt_prepend;
            this.alt_append = alt_append;
            this.alt_remove = alt_remove;
            this.mutations = mutations;
            this.mutations_prepend = mutations_prepend;
            this.mutations_append = mutations_append;
            this.mutations_remove = mutations_remove;
            this.aspects = aspects;
            this.aspects_extend = aspects_extend;
            this.aspects_remove = aspects_remove;
            this.deckeffects = deckeffects;
            this.deckeffects_extend = deckeffects_extend;
            this.deckeffects_remove = deckeffects_remove;
            this.hintonly = hintonly;
            this.internaldeck = internaldeck;
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
            this.extends = extends;
        }

        public Recipe()
        {

        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public Recipe Copy()
        {
            // that's a big ol' yikes from me, but it works perfectly. I just wish I knew how to simplify it.
            string serializedObject = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<Recipe>(serializedObject);
        }
    }
}
