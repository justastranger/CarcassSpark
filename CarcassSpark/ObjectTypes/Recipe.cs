using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CarcassSpark.ObjectTypes
{
    public class Recipe : IGameObject
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

        [JsonIgnore]
        public string Filename { get => this.filename; set => this.filename = value; }
        [JsonIgnore]
        public Guid Guid { get => this.guid; set => this.guid = value; }
        [JsonIgnore]
        public string ID { get => this.id; set => this.id = value; }

        [JsonConstructor]
        public Recipe(bool? craftable, bool? hintonly, bool? deleted, int? warmup, int? maxexecutions,
                      string actionId, string startdescription, string description, string id, string label, string signalendingflavour, string portaleffect, bool? signalimportantloop,
                      Deck internaldeck, string ending, string burnimage, string comments,
                      Dictionary<string, string> requirements, Dictionary<string, string> requirements_extend, List<string> requirementsRemove,
                      Dictionary<string, string> effects, Dictionary<string, string> effectsExtend, List<string> effectsRemove,
                      List<RecipeLink> linked, List<RecipeLink> linkedPrepend, List<RecipeLink> linkedAppend, List<string> linkedRemove, List<Slot> slots,
                      List<RecipeLink> alternativerecipes, List<RecipeLink> alt, List<RecipeLink> altPrepend, List<RecipeLink> altAppend, List<string> altRemove,
                      Dictionary<string, int> deckeffects, Dictionary<string, int> deckeffectsExtend, List<string> deckeffectsRemove,
                      List<Mutation> mutations, List<Mutation> mutationsPrepend, List<Mutation> mutationsAppend, List<string> mutationsRemove,
                      Dictionary<string, int> aspects, Dictionary<string, int> aspectsExtend, List<string> aspectsRemove,
                      Dictionary<string, string> tablereqs, Dictionary<string, string> tablereqsExtend, List<string> tablereqsRemove,
                      Dictionary<string, string> extantreqs, Dictionary<string, string> extantreqsExtend, List<string> extantreqsRemove,
                      Dictionary<string, int> purge, Dictionary<string, int> purgeExtend, List<string> purgeRemove,
                      Dictionary<string, int> haltverb, Dictionary<string, int> haltverbExtend, List<string> haltverbRemove,
                      Dictionary<string, int> deleteverb, Dictionary<string, int> deleteverbExtend, List<string> deleteverbRemove, List<string> extends)
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
            this.requirements_remove = requirementsRemove;
            this.extantreqs = extantreqs;
            this.extantreqs_extend = extantreqsExtend;
            this.extantreqs_remove = extantreqsRemove;
            this.tablereqs = tablereqs;
            this.tablereqs_extend = tablereqsExtend;
            this.tablereqs_remove = tablereqsRemove;
            this.maxexecutions = maxexecutions;
            this.effects = effects;
            this.effects_extend = effectsExtend;
            this.effects_remove = effectsRemove;
            this.linked = linked;
            this.linked_prepend = linkedPrepend;
            this.linked_append = linkedAppend;
            this.linked_remove = linkedRemove;
            this.slots = slots;
            this.alt = alt ?? alternativerecipes;
            this.alt_prepend = altPrepend;
            this.alt_append = altAppend;
            this.alt_remove = altRemove;
            this.mutations = mutations;
            this.mutations_prepend = mutationsPrepend;
            this.mutations_append = mutationsAppend;
            this.mutations_remove = mutationsRemove;
            this.aspects = aspects;
            this.aspects_extend = aspectsExtend;
            this.aspects_remove = aspectsRemove;
            this.deckeffects = deckeffects;
            this.deckeffects_extend = deckeffectsExtend;
            this.deckeffects_remove = deckeffectsRemove;
            this.hintonly = hintonly;
            this.internaldeck = internaldeck;
            this.purge = purge;
            this.purge_extend = purgeExtend;
            this.purge_remove = purgeRemove;
            this.haltverb = haltverb;
            this.haltverb_extend = haltverbExtend;
            this.haltverb_remove = haltverbRemove;
            this.deleteverb = deleteverb;
            this.deleteverb_extend = deleteverbExtend;
            this.deleteverb_remove = deleteverbRemove;
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
            string serializedObject = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<Recipe>(serializedObject);
        }

        Recipe IGameObject.Copy<Recipe>()
        {
            // that's a big ol' yikes from me, but it works perfectly. I just wish I knew how to simplify it.
            string serializedObject = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<Recipe>(serializedObject);
        }
    }
}
