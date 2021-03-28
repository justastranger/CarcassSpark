using Newtonsoft.Json;
using System;
using System.Windows.Forms;

namespace CarcassSpark.ObjectViewers
{
    public interface IGameObjectViewer
    {
        ListViewItem AssociatedListViewItem{ get; set; }
        void Show();
    }
}