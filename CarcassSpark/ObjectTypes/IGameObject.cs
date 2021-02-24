using Newtonsoft.Json;
using System;

namespace CarcassSpark.ObjectTypes
{
    public interface IGameObject : IHasGuidAndID
    {
        [JsonIgnore]
        string Filename { get; set; }
    }

    public interface IHasGuidAndID
    {
        [JsonIgnore]
        Guid Guid { get; set; }
        [JsonIgnore]
        string ID { get; set; }
    }
}