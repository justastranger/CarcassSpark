using System;

namespace CarcassSpark.ObjectTypes
{
    public interface IGameObject
    {
        Guid Guid { get; set; }
        string ID { get; set; }
        string Filename { get; set; }
    }
}