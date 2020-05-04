using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultist_Simulator_Modding_Toolkit
{
    public class Ending
    {
        public string id, label, description, image, flavor, anim, achievement;

        public Ending(string id, string label, string description, string image, string flavor, string anim, string achievement)
        {
            this.id = id;
            this.label = label;
            this.description = description;
            this.image = image;
            this.flavor = flavor;
            this.anim = anim;
            this.achievement = achievement;
        }
    }
}
