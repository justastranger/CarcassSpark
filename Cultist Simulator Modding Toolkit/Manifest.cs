using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultist_Simulator_Modding_Toolkit
{
    public class Manifest
    {
        public string name;
        public string author;
        public string version;
        public string description;
        public string description_long;

        public Manifest(string name, string author, string version, string description, string description_long)
        {
            this.name = name;
            this.author = author;
            this.version = version;
            this.description = description;
            this.description_long = description_long;
        }
    }
}
