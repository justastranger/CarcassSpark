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
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id, label, description, image, flavour, anim, achievement, comments;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? deleted;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> extends;

        [JsonConstructor]
        public Ending(string id, string label, string description, string image, string flavour, string anim, string achievement, string comments, bool? deleted, List<string> extends)
        {
            this.id = id;
            this.label = label;
            this.description = description;
            this.image = image;
            this.flavour = flavour;
            this.anim = anim;
            this.achievement = achievement;
            this.comments = comments;
            this.deleted = deleted;
            this.extends = extends;
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
            tmp.comments = comments;
            tmp.deleted = deleted;
            tmp.extends = extends;
            return tmp;
        }
    }
}
