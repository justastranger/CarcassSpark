using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassSpark.ObjectTypes
{
    public class Verb
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id, label, description, comments;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? atStart, deleted;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Slot slot;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> extends;

        [JsonConstructor]
        public Verb(string id, string label, string description, string comments, bool? atStart, bool? deleted, Slot slot, List<string> extends)
        {
            this.id = id;
            this.label = label;
            this.description = description;
            this.comments = comments;
            this.atStart = atStart;
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
            return new Verb(id, label, description, comments, atStart, deleted, slot, extends);
        }
    }
}
