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

        [JsonConstructor]
        public Ending(string id, string label, string description, string image, string flavour, string anim, string achievement, string comments, bool? deleted)
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
        }

        public Ending()
        {

        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public Ending Copy()
        {
            return new Ending(id, label, description, image, flavour, anim, achievement, comments, deleted);
        }
    }
}
