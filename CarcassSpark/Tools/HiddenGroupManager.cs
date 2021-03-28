using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarcassSpark.Tools
{
    public partial class HiddenGroupManager : Form
    {
        public Dictionary<string, List<string>> HiddenGroups;

        public HiddenGroupManager(Dictionary<string, List<string>> hiddenGroups)
        {
            InitializeComponent();
            HiddenGroups = hiddenGroups;
        }

        private void LoadHiddenGroups()
        {
            if (HiddenGroups != null)
            {
                foreach (KeyValuePair<string, List<string>> type in HiddenGroups)
                {
                    ListViewGroup hiddenGroupsTypeGroup = new ListViewGroup(type.Key, type.Key);
                    hiddenGroupsListView.Groups.Add(hiddenGroupsTypeGroup);
                    List<ListViewItem> hiddenGroupsItems = new List<ListViewItem>();
                    foreach (string hiddenGroup in type.Value)
                    {
                        hiddenGroupsItems.Add(new ListViewItem(hiddenGroup));
                    }
                    hiddenGroupsTypeGroup.Items.AddRange(hiddenGroupsItems.ToArray());
                    hiddenGroupsListView.Items.AddRange(hiddenGroupsItems.ToArray());
                }
            }
        }

        private void HiddenGroupManager_Shown(object sender, EventArgs e)
        {
            LoadHiddenGroups();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void HiddenGroupsListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (hiddenGroupsListView.SelectedItems.Count != 1) return;
            ListViewItem itemToRemove = hiddenGroupsListView.SelectedItems[0];
            ListViewGroup itemGroup = itemToRemove.Group;
            HiddenGroups[itemToRemove.Group.Name].Remove(itemToRemove.Text);
            // itemGroup.Items.Remove(itemToRemove);
            itemToRemove.Remove();
            if (itemGroup.Items.Count == 0)
            {
                HiddenGroups.Remove(itemGroup.Name);
            }
        }
    }
}
