using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CarcassSpark.ObjectTypes
{
    public class Culture : IHasGuidAndID
    {
        [JsonIgnore]
        public Guid guid = Guid.NewGuid();
        // id, endonym, exonym, and fontscript are all required
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id, endonym, exonym, fontscript;
        // both should be true when using normal font scripts
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? boldallowed, released;
        // and so is this, where the actual localization happens
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> uilabels;

        public Guid Guid { get => this.guid; set => this.guid = value; }
        public string ID { get => this.id; set => this.id = value; }

        [JsonConstructor]
        public Culture(string id, string endonym, string exonym, string fontscript, bool? boldallowed, bool? released, Dictionary<string, string> uilabels)
        {
            this.id = id;
            this.endonym = endonym;
            this.exonym = exonym;
            this.fontscript = fontscript;
            this.boldallowed = boldallowed;
            this.released = released;
            this.uilabels = uilabels;
        }

        public Culture()
        {

        }

        public Culture Copy()
        {
            string serializedObject = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<Culture>(serializedObject);
        }

    }
}
