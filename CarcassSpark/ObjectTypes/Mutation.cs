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
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)] //, PropertyName = "filter")] // PropertyName = filter because the check for "filterOnAspectId" is broken but "filter" still works
        public string filterOnAspectId; // element ID to use to select a card, can filter based on aspect or card itself
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string mutateAspectId; // Aspect on filtered card to modify
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? mutationLevel; // how much to modify the aspect by
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? additive;

        [JsonConstructor]
        public Mutation(string filterOnAspectId, string filter, string mutateAspectId, string mutate, int? mutationLevel, int? level, bool? additive)
        {
            this.filterOnAspectId = filterOnAspectId ?? filter;
            this.mutateAspectId = mutateAspectId ?? mutate;
            this.mutationLevel = mutationLevel ?? level;
            this.additive = additive;
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
            return new Mutation(filterOnAspectId, null, mutateAspectId, null, mutationLevel, null, additive);
        }
    }
}
