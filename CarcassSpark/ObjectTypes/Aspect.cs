using Newtonsoft.Json;
using System.Collections.Generic;

namespace CarcassSpark.ObjectTypes
{
    public class Aspect : Element
    {
        // These have been moved into the Element object to get me one step closer to unifying them
        // [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        // public bool? isAspect, isHidden, noartneeded;

        [JsonConstructor]
        public Aspect(string id, string label, string description, string inherits,
                      bool? unique, bool? deleted, string icon, string comments,
                      Dictionary<string, int> aspects, Dictionary<string, int> aspects_extend,
                      List<string> aspects_remove, List<Slot> slots, List<Slot> slots_prepend,
                      List<Slot> slots_append, List<string> slots_remove,
                      Dictionary<string, List<XTrigger>> xtriggers,
                      Dictionary<string, List<XTrigger>> xtriggers_extend,
                      List<string> xtriggers_remove, int? lifetime,
                      string decayTo, string uniquenessgroup, bool? resaturate,
                      bool? isHidden, bool? noartneeded, List<Induces> induces,
                      List<Induces> induces_prepend, List<Induces> induces_append,
                      List<string> induces_remove, bool? isAspect, List<string> extends) : base(id, label, description,
                          inherits, unique, deleted, icon, comments, aspects, aspects_extend,
                          aspects_remove, slots, slots_prepend, slots_append, slots_remove,
                          xtriggers, xtriggers_extend, xtriggers_remove, lifetime,
                          decayTo, uniquenessgroup, resaturate, extends)
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

        public new Aspect Copy()
        {
            string serializedObject = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<Aspect>(serializedObject);
        }
    }
}