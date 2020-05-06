using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultist_Simulator_Modding_Toolkit
{
    public class Ending
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id, label, description, image, flavour, anim, achievement;

        public Ending(string id, string label, string description, string image, string flavour, string anim, string achievement)
        {
            this.id = id;
            this.label = label;
            this.description = description;
            this.image = image;
            this.flavour = flavour;
            this.anim = anim;
            this.achievement = achievement;
        }
    }
}
