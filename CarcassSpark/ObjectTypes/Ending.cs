using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassSpark.ObjectTypes
{
    public class Ending
    {
        // [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id, label, description, image, flavour, anim, achievement;

        [JsonConstructor]
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

        public Ending()
        {

        }

        public Ending Copy()
        {
            Ending tmp = new Ending();
            tmp.id = id;
            tmp.label = label;
            tmp.description = description;
            tmp.image = image;
            tmp.flavour = flavour;
            tmp.anim = anim;
            tmp.achievement = achievement;
            return tmp;
        }
    }
}
