using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultist_Simulator_Modding_Toolkit
{
    public class Manifest
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string name;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string author;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string version;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string description;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string description_long;

        public Manifest(string name, string author, string version, string description, string description_long)
        {
            this.name = name;
            this.author = author;
            this.version = version;
            this.description = description;
            this.description_long = description_long;
        }

        public Manifest()
        {
            name = "";
            author = "";
            version = "";
            description = "";
            description_long = "";
        }
    }
}
