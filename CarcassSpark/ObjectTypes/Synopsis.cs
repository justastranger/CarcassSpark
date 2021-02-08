using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassSpark.ObjectTypes
{
    public class Synopsis
    {
        [JsonIgnore]
        public Guid guid = Guid.NewGuid();
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
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> dependencies;

        public Synopsis(string name, string author, string version, string description, string description_long)
        {
            this.name = name;
            this.author = author;
            this.version = version;
            this.description = description;
            this.description_long = description_long;
        }

        public Synopsis(string name, string author, string version, string description, string description_long, List<string> dependencies)
        {
            this.name = name;
            this.author = author;
            this.version = version;
            this.description = description;
            this.description_long = description_long;
            this.dependencies = dependencies;
        }


        public Synopsis()
        {
            name = "";
            author = "";
            version = "";
            description = "";
            description_long = "";
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public Synopsis Copy()
        {
            return new Synopsis(name, author, version, description, description_long, dependencies != null ? new List<string>(dependencies) : null);
        }

        public class Dependency
        {
            public string modId;
            public string version;
            public string VersionOperator;

            public Dependency(string modId, string version, string VersionOperator)
            {
                this.modId = modId;
                this.version = version;
                this.VersionOperator = VersionOperator;
            }

            public Dependency()
            {

            }

            public override string ToString()
            {
                if (modId != null && version != null && VersionOperator != null)
                {
                    return modId + " " + VersionOperator + " " + version;
                }
                else if (modId != null && (version == null || VersionOperator == null))
                {
                    return modId;
                }
                else return modId;
            }

            public Dependency Copy()
            {
                string serializedObject = JsonConvert.SerializeObject(this);
                return JsonConvert.DeserializeObject<Dependency>(serializedObject);
            }
        }
    }
}
