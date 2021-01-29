using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassSpark.ObjectTypes
{
    public class RecipeLink
    {
        [JsonIgnore]
        public Guid guid = Guid.NewGuid();
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? chance;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? additional;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> challenges; // string aspect ID, string challenge type: "base", "advanced". Default if null: "base"
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Expulsion expulsion;


        [JsonConstructor]
        public RecipeLink(string id, int? chance, bool? additional, object challenges, Expulsion expulsion)
        {
            this.id = id;
            this.chance = chance;
            this.additional = additional;
            if (challenges != null)
            {
                Dictionary<string, string> dict = ((JObject)challenges).ToObject<Dictionary<string, string>>();
                if (dict != null && dict.Count > 0)
                {
                    this.challenges = dict;
                }
                else if (challenges is string) this.challenges = new Dictionary<string, string>() { [challenges as string] = "base" };
            }
            this.expulsion = expulsion;
        }

        public RecipeLink(string id, int? chance, bool? additional, Dictionary<string, string> challenges, Expulsion expulsion)
        {
            this.id = id;
            this.chance = chance;
            this.additional = additional;
            this.challenges = challenges;
            this.expulsion = expulsion;
        }

        public RecipeLink(string id)
        {
            this.id = id;
        }

        public RecipeLink()
        {

        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public RecipeLink Copy()
        {
            string serializedObject = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<RecipeLink>(serializedObject);
        }
    }
}
