using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassSpark.ObjectTypes
{
    public class Induces
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? chance;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? additional;

        [JsonConstructor]
        public Induces(string id, int? chance, bool? additional)
        {
            this.id = id;
            this.chance = chance;
            this.additional = additional;
        }
    }
}
