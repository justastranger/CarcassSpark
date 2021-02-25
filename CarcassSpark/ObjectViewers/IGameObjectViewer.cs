using CarcassSpark.ObjectTypes;
using Newtonsoft.Json;
using System;
using System.Windows.Forms;

namespace CarcassSpark.ObjectViewers
{
    public interface IGameObjectViewer<T> where T : IGameObject
    {
        ListViewItem AssociatedListViewItem{ get; set; }

        IGameObjectViewer<T> CreateNew(T gameObject, EventHandler<T> successCallback, ListViewItem item);
    }
}