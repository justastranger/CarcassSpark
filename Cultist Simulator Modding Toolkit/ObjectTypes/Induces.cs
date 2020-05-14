using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultistSimulatorModdingToolkit.ObjectTypes
{
    public class Induces
    {
        public string id;
        public int chance;
        public bool? additional;

        [JsonConstructor]
        public Induces(string id, int chance, bool? additional)
        {
            this.id = id;
            this.chance = chance;
            this.additional = additional;
        }
    }
}
