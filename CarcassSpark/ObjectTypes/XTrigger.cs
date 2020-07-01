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
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string morphEffect, id;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? chance, level;

        [JsonConstructor]
        public XTrigger(string id, int? chance, string morphEffect, int? level)
        {
            this.id = id;
            this.chance = chance;
            this.morphEffect = morphEffect;
            this.level = level;
        }

        public XTrigger(string id)
        {
            this.id = id;
        }

        public XTrigger()
        {

        }

        public string toString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public XTrigger Copy()
        {
            return new XTrigger(id, chance, morphEffect, level);
        }

        public enum MorphEffectType
        {
            Transform,
            Spawn,
            Mutate
        }
    }
}
