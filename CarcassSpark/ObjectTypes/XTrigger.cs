using Newtonsoft.Json;
using System;

namespace CarcassSpark.ObjectTypes
{
    public class XTrigger : IHasGuidAndID
    {
        [JsonIgnore]
        public Guid guid = Guid.NewGuid();
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string morpheffect, id;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? chance, level;
        
        [JsonIgnore]
        public Guid Guid { get => this.guid; set => this.guid = value; }
        [JsonIgnore]
        public string ID { get => this.id; set => this.id = value; }

        [JsonConstructor]
        public XTrigger(string id, int? chance, string morpheffect, int? level)
        {
            this.id = id;
            this.chance = chance;
            this.morpheffect = morpheffect;
            this.level = level;
        }

        public XTrigger(string id)
        {
            this.id = id;
        }

        public XTrigger()
        {

        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public XTrigger Copy()
        {
            string serializedObject = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<XTrigger>(serializedObject);
        }

        public enum MorphEffectType
        {
            Transform,
            Spawn,
            Mutate
        }
    }
}
