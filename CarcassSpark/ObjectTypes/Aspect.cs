using Newtonsoft.Json;
using System.Collections.Generic;

namespace CarcassSpark.ObjectTypes
{
    public class Aspect : Element, IGameObject
    {
        // These have been moved into the Element object to get me one step closer to unifying them
        // [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        // public bool? isAspect, isHidden, noartneeded;

        [JsonConstructor]
        public Aspect(string id, string label, string description, string inherits,
                      bool? unique, bool? deleted, string icon, string comments,
                      Dictionary<string, int> aspects, Dictionary<string, int> aspects_extend,
                      List<string> aspectsRemove, List<Slot> slots, List<Slot> slotsPrepend,
                      List<Slot> slotsAppend, List<string> slotsRemove,
                      Dictionary<string, List<XTrigger>> xtriggers,
                      Dictionary<string, List<XTrigger>> xtriggersExtend,
                      List<string> xtriggersRemove, int? lifetime,
                      string decayTo, string uniquenessgroup, bool? resaturate,
                      bool? isHidden, bool? noartneeded, List<Induces> induces,
                      List<Induces> inducesPrepend, List<Induces> inducesAppend,
                      List<string> inducesRemove, bool? isAspect, List<string> extends) : base(id, label, description,
                          inherits, unique, deleted, icon, comments, aspects, aspects_extend,
                          aspectsRemove, slots, slotsPrepend, slotsAppend, slotsRemove,
                          xtriggers, xtriggersExtend, xtriggersRemove, lifetime,
                          decayTo, uniquenessgroup, resaturate, extends)
        {
            // optional
            this.isAspect = isAspect;
            // optional
            this.isHidden = isHidden;
            // optional
            this.induces = induces;
            this.induces_prepend = inducesPrepend;
            this.induces_append = inducesAppend;
            this.induces_remove = inducesRemove;
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

        Aspect IGameObject.Copy<Aspect>()
        {
            string serializedObject = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<Aspect>(serializedObject);
        }
    }
}