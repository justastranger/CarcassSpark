using Newtonsoft.Json;
using System;

namespace CarcassSpark.ObjectTypes
{
    public interface IGameObject
    {
        [JsonIgnore]
        Guid Guid { get; set; }
        [JsonIgnore]
        string ID { get; set; }
        [JsonIgnore]
        string Filename { get; set; }
    }
}