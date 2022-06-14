using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CarcassSpark.ObjectTypes
{
    public class Ending : IGameObject
    {
        [JsonIgnore]
        public string filename;
        [JsonIgnore]
        public Guid guid = Guid.NewGuid();
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id, label, description, image, flavour, anim, achievement, comments;
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
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "description$postfix")]
        public string description_postfix;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "description$replace")]
        public string description_replace;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "description$replacelast")]
        public string description_replace_last;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? deleted;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> extends;

        [JsonIgnore]
        public string Filename { get => this.filename; set => this.filename = value; }
        [JsonIgnore]
        public Guid Guid { get => this.guid; set => this.guid = value; }
        [JsonIgnore]
        public string ID { get => this.id; set => this.id = value; }

        [JsonConstructor]
        public Ending(string id, string label, string label_prefix, string label_postfix, string label_replace, string label_replace_last,
                      string description, string description_prefix, string description_postfix, string description_replace, string description_replace_last,
                      string image, string flavour, string anim, string achievement, string comments, bool? deleted, List<string> extends)
        {
            this.id = id;
            // necessary
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
            this.image = image;
            this.flavour = flavour;
            this.anim = anim;
            this.achievement = achievement;
            this.comments = comments;
            this.deleted = deleted;
            this.extends = extends;
        }

        public Ending()
        {

        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public Ending Copy()
        {
            string serializedObject = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<Ending>(serializedObject);
        }

        Ending IGameObject.Copy<Ending>()
        {
            string serializedObject = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<Ending>(serializedObject);
        }
    }
}
