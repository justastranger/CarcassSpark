using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassSpark.ObjectTypes
{
    public class XTrigger
    {
        [JsonIgnore]
        public Guid guid = Guid.NewGuid();
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string morpheffect, id;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? chance, level;

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
