using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CarcassSpark.ObjectTypes
{
    public class Deck : IGameObject
    {
        [JsonIgnore]
        public string filename;
        [JsonIgnore]
        public Guid guid = Guid.NewGuid();
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id, label, description, comments, defaultcard;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "label$prefix")]
        public string label_prefix;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "label$postfix")]
        public string label_postfix;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "label$replace")]
        public string label_replace;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "label$replacelast")]
        public string label_replace_last;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "description$prefix")]
        public string description_prefix;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "description$prefix")]
        public string description_postfix;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "description$replace")]
        public string description_replace;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "description$replacelast")]
        public string description_replace_last;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> spec; // the actual internal deck
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "spec$append")]
        public List<string> spec_append;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "spec$prepend")]
        public List<string> spec_prepend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "spec$remove")]
        public List<string> spec_remove;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? resetonexhaustion, deleted;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? draws, defaultdraws;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> drawmessages, defaultdrawmessages;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "drawmessages$add")]
        public Dictionary<string, string> drawmessages_extend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "drawmessages$remove")]
        public List<string> drawmessages_remove;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "defaultdrawmessages$add")]
        public Dictionary<string, string> defaultdrawmessages_extend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "defaultdrawmessages$remove")]
        public List<string> defaultdrawmessages_remove;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> extends;

        [JsonIgnore]
        public string Filename { get => this.filename; set => this.filename = value; }
        [JsonIgnore]
        public Guid Guid { get => this.guid; set => this.guid = value; }
        [JsonIgnore]
        public string ID { get => this.id; set => this.id = value; }

        [JsonConstructor]
        public Deck(List<string> spec, int? defaultdraws, int? draws, bool? resetonexhaustion, bool? deleted, string id, string label, string label_prefix, string label_postfix, string label_replace, string label_replace_last,
                    string description, string description_prefix, string description_postfix, string description_replace, string description_replace_last, string comments,
                    string defaultcard, Dictionary<string, string> drawmessages,
                    Dictionary<string, string> defaultdrawmessages, List<string> spec_append, List<string> specPrepend, List<string> specRemove,
                    Dictionary<string, string> drawmessagesExtend, List<String> drawmessagesRemove, Dictionary<string, string> defaultdrawmessagesExtend, List<String> defaultdrawmessagesRemove, List<string> extends)
        {
            this.id = id;// necessary
            this.label = label;
            this.label_prefix = label_prefix;
            this.label_postfix = label_postfix;
            this.label_replace = label_replace;
            this.label_replace_last = label_replace_last;
            // necessary
            this.description = description;
            this.description_prefix = description_prefix;
            this.description_postfix = description_postfix;
            this.description_replace = description_replace;
            this.description_replace_last = description_replace_last;
            this.spec = spec;
            this.spec_append = spec_append;
            this.spec_prepend = specPrepend;
            this.spec_remove = specRemove;
            this.comments = comments;
            this.defaultcard = defaultcard;
            this.resetonexhaustion = resetonexhaustion;
            this.deleted = deleted;
            this.defaultdraws = defaultdraws;
            this.draws = draws;
            this.drawmessages = drawmessages;
            this.drawmessages_extend = drawmessagesExtend;
            this.drawmessages_remove = drawmessagesRemove;
            this.defaultdrawmessages = defaultdrawmessages;
            this.defaultdrawmessages_extend = defaultdrawmessagesExtend;
            this.defaultdrawmessages_remove = defaultdrawmessagesRemove;
            this.extends = extends;
        }

        public Deck()
        {

        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public Deck Copy()
        {
            string serializedObject = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<Deck>(serializedObject);
        }

        Deck IGameObject.Copy<Deck>()
        {
            string serializedObject = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<Deck>(serializedObject);
        }
    }

}
