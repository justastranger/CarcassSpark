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
        public Effects effects;
        public Requirements requirements;

        public class Effects
        {
            Dictionary<string, int> effectsDictionary;

            public Effects(JToken effects)
            {
                this.effectsDictionary = JsonConvert.DeserializeObject<Dictionary<string, int>>(JsonConvert.SerializeObject(effects));
            }
        }

        public class Requirements
        {
            Dictionary<string, int> requirementsDictionary;

            public Requirements(JToken requirements)
            {
                this.requirementsDictionary = JsonConvert.DeserializeObject<Dictionary<string, int>>(JsonConvert.SerializeObject(requirements));
            }
        }
    }
}
