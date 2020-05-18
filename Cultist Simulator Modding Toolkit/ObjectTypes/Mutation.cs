using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultistSimulatorModdingToolkit.ObjectTypes
{
    public class Mutation
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "filter")] // PropertyName = filter because the check for "filterOnAspectId" is broken but "filter" still works
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
            this.filterOnAspectId = filterOnAspectId != null ? filterOnAspectId : filter;
            this.mutateAspectId = mutateAspectId != null ? mutateAspectId : mutate;
            if (mutationLevel.HasValue) this.mutationLevel = mutationLevel;
            else if (level.HasValue) this.mutationLevel = level;
            if (additive.HasValue) this.additive = additive;
        }

        public Mutation()
        {

        }
    }
}
