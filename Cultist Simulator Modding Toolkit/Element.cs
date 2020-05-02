using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultist_Simulator_Modding_Toolkit
{
    class Element
    {
        public string icon;
        public string id;
        public string label;
        public string description;
        public bool isAspect;
        public Induces[] induces;
        public bool isHidden;
        public bool noartneeded;
        public string comments;

        [JsonConstructor]
        public Element(string id = null, string label = null, string description = null,
                       bool isAspect = false, string icon = null, JArray induces = null,
                       bool isHidden = false, bool noartneeded = false,
                       string comments = null)
        {
            this.id = id;
            this.label = label;
            this.description = description;
            this.isAspect = isAspect;
            if (icon != null)
            {
                this.icon = icon;
            }
            else
            {
                this.icon = id;
            }
            if (isHidden == true)
            {
                this.isHidden = true;
            }
            if (induces != null)
            {
                this.induces = induces[0].ToObject<Induces[]>();
            }
            if (noartneeded == true)
            {
                this.noartneeded = true;
            }
            this.comments = comments;
        }
    }

    class Induces
    {
        public string id;
        public int chance;

        [JsonConstructor]
        public Induces(string id, int chance)
        {
            this.id = id;
            this.chance = chance;
        }
    }
}
