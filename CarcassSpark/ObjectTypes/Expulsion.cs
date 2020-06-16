using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassSpark.ObjectTypes
{
    public class Expulsion
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, int> filter;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? limit;

        [JsonConstructor]
        public Expulsion(Dictionary<string, int> filter, int? limit)
        {
            this.filter = filter;
            this.limit = limit;
        }

        public Expulsion(int limit)
        {
            filter = new Dictionary<string, int>();
            this.limit = limit;
        }
    }
}
