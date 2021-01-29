using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassSpark.ObjectTypes
{
    public class Mutation
    {
        [JsonIgnore]
        public Guid guid = Guid.NewGuid();
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string filter; // element ID to use to select a card, can filter based on aspect or card itself
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string mutate; // Aspect on filtered card to modify
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? level; // how much to modify the aspect by
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? additive;

        [JsonConstructor]
        public Mutation(string filter, string mutate, int? level, bool? additive)
        {
            this.filter = filter;
            this.mutate = mutate;
            this.level = level;
            this.additive = additive;
        }

        public Mutation(string mutate)
        {
            this.mutate = mutate;
        }

        public Mutation()
        {

        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public Mutation Copy()
        {
            string serializedObject = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<Mutation>(serializedObject);
        }
    }
}
