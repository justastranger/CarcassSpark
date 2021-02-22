using System;

namespace CarcassSpark.ObjectTypes
{
    public interface IGameObject : IHasGuidAndID
    {
        string Filename { get; set; }
    }

    public interface IHasGuidAndID
    {
        Guid Guid { get; set; }
        string ID { get; set; }
    }
}