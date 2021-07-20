using Newtonsoft.Json;
using System;

namespace CarcassSpark.ObjectTypes
{
    public interface IGameObject : IHasGuidAndID
    {
        [JsonIgnore]
        string Filename { get; set; }
        [JsonIgnore]
        string Filepath { get; set; }

        T Copy<T>() where T : IGameObject;
    }

    public interface IHasGuidAndID
    {
        [JsonIgnore]
        Guid Guid { get; set; }
        [JsonIgnore]
        string ID { get; set; }
    }
}