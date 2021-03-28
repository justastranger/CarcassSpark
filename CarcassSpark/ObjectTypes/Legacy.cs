using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CarcassSpark.ObjectTypes
{
    public class Legacy : IGameObject
    {
        [JsonIgnore]
        public string filename;
        [JsonIgnore]
        public Guid guid = Guid.NewGuid();
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id, label, description, startdescription, image, fromEnding, startingVerbId, comments, tablecoverimage, tablesurfaceimage, tableedgeimage;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? availableWithoutEndingMatch, deleted, newstart;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> excludesOnEnding;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "excludesOnEnding$append")]
        public List<string> excludesOnEnding_append;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "excludesOnEnding$prepend")]
        public List<string> excludesOnEnding_prepend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "excludesOnEnding$remove")]
        public List<string> excludesOnEnding_remove;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, int> effects;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "effects$add")]
        public Dictionary<string, int> effects_extend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "effects$remove")]
        public List<string> effects_remove;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> statusbarelements;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "statusbarelements&prepend")]
        public List<string> statusbarelements_prepend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "statusbarelements&append")]
        public List<string> statusbarelements_append;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "statusbarelements&remove")]
        public List<string> statusbarelements_remove;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> extends;

        [JsonIgnore]
        public string Filename { get => this.filename; set => this.filename = value; }
        [JsonIgnore]
        public Guid Guid { get => this.guid; set => this.guid = value; }
        [JsonIgnore]
        public string ID { get => this.id; set => this.id = value; }

        [JsonConstructor]
        public Legacy(string id, string label, string description, string startdescription, string comments, string tablecoverimage, string tablesurfaceimage, string tableedgeimage,
                      Dictionary<string, int> effects, string image, string fromEnding, bool? availableWithoutEndingMatch, bool? newstart,
                      string startingVerbId, List<string> excludesOnEnding,
                      List<string> excludesOnEnding_prepend, List<string> excludesOnEndingAppend, List<string> excludesOnEndingRemove,
                      Dictionary<string, int> effectsExtend, List<string> effectsRemove, List<string> statusbarelements,
                      List<string> statusbarelementsPrepend, List<string> statusbarelementsAppend, List<string> statusbarelementsRemove,
                      bool? deleted, List<string> extends)
        {
            this.id = id;
            this.label = label;
            this.description = description;
            this.startdescription = startdescription;
            this.effects = effects;
            this.effects_extend = effectsExtend;
            this.effects_remove = effectsRemove;
            this.comments = comments;
            this.tablecoverimage = tablecoverimage;
            this.tableedgeimage = tableedgeimage;
            this.tablesurfaceimage = tablesurfaceimage;
            this.image = image;
            this.fromEnding = fromEnding;
            this.availableWithoutEndingMatch = availableWithoutEndingMatch;
            this.startingVerbId = startingVerbId;
            this.excludesOnEnding = excludesOnEnding;
            this.excludesOnEnding_prepend = excludesOnEnding_prepend;
            this.excludesOnEnding_append = excludesOnEndingAppend;
            this.excludesOnEnding_remove = excludesOnEndingRemove;
            this.statusbarelements = statusbarelements;
            this.statusbarelements_prepend = statusbarelementsPrepend;
            this.statusbarelements_append = statusbarelementsAppend;
            this.statusbarelements_remove = statusbarelementsRemove;
            this.deleted = deleted;
            this.newstart = newstart;
            this.extends = extends;
        }

        public Legacy()
        {

        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public Legacy Copy()
        {
            string serializedObject = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<Legacy>(serializedObject);
        }

        Legacy IGameObject.Copy<Legacy>()
        {
            string serializedObject = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<Legacy>(serializedObject);
        }
    }
}
