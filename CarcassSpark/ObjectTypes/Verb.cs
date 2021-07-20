using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CarcassSpark.ObjectTypes
{
    public class Verb : IGameObject
    {
        [JsonIgnore]
        public Guid guid = Guid.NewGuid();
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id, label, description, comments;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? deleted;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Slot slot;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> extends;

        [JsonIgnore]
        public string Filename { get; set; }
        [JsonIgnore]
        public string Filepath { get; set; }

        [JsonIgnore]
        public Guid Guid { get => this.guid; set => this.guid = value; }
        [JsonIgnore]
        public string ID { get => this.id; set => this.id = value; }

        [JsonConstructor]
        public Verb(string id, string label, string description, string comments, bool? deleted, Slot slot, List<string> extends)
        {
            this.id = id;
            this.label = label;
            this.description = description;
            this.comments = comments;
            this.slot = slot;
            this.deleted = deleted;
            this.extends = extends;
        }

        public Verb()
        {

        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public Verb Copy()
        {
            string serializedObject = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<Verb>(serializedObject);
        }

        Verb IGameObject.Copy<Verb>()
        {
            string serializedObject = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<Verb>(serializedObject);
        }
    }
}
