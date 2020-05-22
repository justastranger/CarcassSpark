using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using CarcassSpark.ObjectTypes;

namespace CarcassSpark.ObjectTypes
{
    public class Aspect : Element
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? isAspect, isHidden, noartneeded;
        
        [JsonConstructor]
        public Aspect(string id, string label, string description,
                      bool? isHidden, bool? noartneeded, string icon, List<Induces> induces,
                      List<Induces> induces_prepend, List<Induces> induces_append, List<string> induces_remove,
                      bool? isAspect, string comments, Dictionary<string, int> aspects)
        {
            // necessary
            this.id = id;
            // necessary
            this.label = label;
            // necessary
            this.description = description;
            // optional
            this.isAspect = isAspect;
            // optional
            if (icon != null) this.icon = icon;
            else this.icon = id;
            // isHidden is true iff isAspect is true
            // optional
            if (isHidden == true) this.isHidden = true;
            // optional
            if (induces != null) this.induces = induces;
            if (induces_prepend != null) this.induces_prepend = induces_prepend;
            if (induces_append != null) this.induces_append = induces_append;
            if (induces_remove != null) this.induces_remove = induces_remove;
            // optional
            this.noartneeded = noartneeded;
            // optional
            this.comments = comments;
            // optional, didn't even know it was possible tbqh
            if (aspects != null) this.aspects = aspects;
        }

        public Aspect()
        {
            isAspect = true;
        }

        public string toString()
        {
            return JsonConvert.SerializeObject(this);
        }
        
    }
}