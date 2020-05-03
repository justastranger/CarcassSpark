using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultist_Simulator_Modding_Toolkit
{
    class Recipe
    {
        public string id, label, actionId, startdescription, description;
        // craftable has to be true in order for the player to initiate the recipe
        // false means the recipe is linked to by another recipe somehow
        public bool craftable;
        public int maxececutions, warmup;
        public EffectsDictionary effects;
        public RequirementsDictionary requirements;

        public class EffectsDictionary
        {
            Dictionary<string, int> effectsDictionary;

            public EffectsDictionary(JToken effects)
            {
                this.effectsDictionary = JsonConvert.DeserializeObject<Dictionary<string, int>>(JsonConvert.SerializeObject(effects));
            }
        }

        public class RequirementsDictionary : Dictionary<string,int>
        {
            Dictionary<string, int> internalDictionary;

            new public int this[string key]
            {
                get
                {
                    return internalDictionary[key];
                }
                set
                {
                    internalDictionary[key] = value;
                }
            }

            public RequirementsDictionary(string id, int amount)
            {
                this.internalDictionary = new Dictionary<string, int>();
                this.internalDictionary[id] = amount;
            }

            public RequirementsDictionary(JToken requirements)
            {
                this.internalDictionary = JsonConvert.DeserializeObject<Dictionary<string, int>>(JsonConvert.SerializeObject(requirements));
            }
        }
    }
}
