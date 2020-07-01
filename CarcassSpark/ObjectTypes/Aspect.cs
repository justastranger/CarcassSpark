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
        public Aspect(string id, string label, string description, string inherits,
                      bool? unique, bool? deleted, string icon, string comments,
                      Dictionary<string, int> aspects, Dictionary<string, int> aspects_extend,
                      List<string> aspects_remove, List<Slot> slots, List<Slot> slots_prepend,
                      List<Slot> slots_append, List<string> slots_remove,
                      Dictionary<string, List<XTrigger>> xtriggers,
                      Dictionary<string, List<XTrigger>> xtriggers_extend,
                      List<string> xtriggers_remove, int? animframes, int? animframes_add,
                      int? animframes_minus, int? lifetime, int? lifetime_add, int? lifetime_minus,
                      string decayTo, string uniquenessgroup, List<string> extends, bool? resaturate,
                      bool? isHidden, bool? noartneeded, List<Induces> induces,
                      List<Induces> induces_prepend, List<Induces> induces_append,
                      List<string> induces_remove, bool? isAspect) : base(id, label, description,
                          inherits, unique, deleted, icon, comments, aspects, aspects_extend,
                          aspects_remove, slots, slots_prepend, slots_append, slots_remove,
                          xtriggers, xtriggers_extend, xtriggers_remove, animframes, animframes_add,
                          animframes_minus, lifetime, lifetime_add, lifetime_minus, decayTo,
                          uniquenessgroup, extends, resaturate)
        {
            // optional
            this.isAspect = isAspect;
            // optional
            this.isHidden = isHidden;
            // optional
            this.induces = induces;
            this.induces_prepend = induces_prepend;
            this.induces_append = induces_append;
            this.induces_remove = induces_remove;
            // optional
            this.noartneeded = noartneeded;
        }

        public Aspect()
        {
            isAspect = true;
        }
        
        new public Aspect Copy()
        {
            return new Aspect(id, label, description, inherits, unique, deleted, icon, comments, aspects != null ? new Dictionary<string, int>(aspects) : null, aspects_extend != null ? new Dictionary<string, int>(aspects_extend) : null, aspects_remove != null ? new List<string>(aspects_remove) : null, slots != null ? new List<Slot>(slots) : null, slots_prepend != null ? new List<Slot>(slots_prepend) : null, slots_append != null ? new List<Slot>(slots_append) : null, slots_remove != null ? new List<string>(slots_remove) : null, xtriggers != null ? new Dictionary<string, List<XTrigger>>(xtriggers) : null, xtriggers_extend != null ? new Dictionary<string, List<XTrigger>>(xtriggers_extend) : null, xtriggers_remove != null ? new List<string>(xtriggers_remove) : null, animframes, animframes_add, animframes_minus, lifetime, lifetime_add, lifetime_minus, decayTo, uniquenessgroup, extends != null ? new List<string>(extends) : null, resaturate, isHidden, noartneeded, induces != null ? new List<Induces>(induces) : null, induces_prepend != null ? new List<Induces>(induces_prepend) : null, induces_append != null ? new List<Induces>(induces_append) : null, induces_remove != null ? new List<string>(induces_remove) : null, isAspect);
        }
    }
}