using AssetStudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CarcassSpark.Tools
{
    public partial class AssetBrowser : Form
    {
        // private ImageList imageList = new ImageList();
        private string objectType = "aspects";

        public AssetBrowser()
        {
            InitializeComponent();
            assetsListView.LargeImageList = Utilities.ImageList;
            // LoadAssets();
        }

        private void LoadAssets()
        {
            assetsListView.Items.Clear();
            HashSet<ListViewItem> listViewItems = new HashSet<ListViewItem>();
            if (objectType == "all")
            {
                foreach (string path in Utilities.Assets.Keys)
                {
                    string folder = path.Split('/').Length > 1 ? path.Split('/')[1] : path;
                    ListViewGroup folderGroup = assetsListView.Groups[folder] ?? new ListViewGroup(folder, folder);

                    if (!assetsListView.Groups.Contains(folderGroup))
                    {
                        assetsListView.Groups.Add(folderGroup);
                    }

                    ListViewItem item = new ListViewItem(path)
                    {
                        ImageKey = path,
                        Group = folderGroup
                    };

                    listViewItems.Add(item);
                }
                assetsListView.Items.AddRange(listViewItems.ToArray());
            }
            else
            {
                ListViewGroup folderGroup = assetsListView.Groups[objectType] ?? new ListViewGroup(objectType, objectType);

                if (!assetsListView.Groups.Contains(folderGroup))
                {
                    assetsListView.Groups.Add(folderGroup);
                }

                foreach (string path in Utilities.Assets.Keys)
                {
                    string folder = path.Split('/').Length > 1 ? path.Split('/')[1] : path;

                    if (folder == objectType.ToLower())
                    {
                        ListViewItem item = new ListViewItem(path)
                        {
                            ImageKey = path,
                            Group = folderGroup
                        };

                        listViewItems.Add(item);
                    }
                }
                assetsListView.Items.AddRange(listViewItems.ToArray());
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Dispose();
            Close();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Dispose();
            Close();
        }

        private void AssetsListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (assetsListView.SelectedItems.Count == 1)
            {
                ListViewItem selectedItem = assetsListView.SelectedItems[0];
                // Image selectedImage = assetsListView.LargeImageList.Images[selectedItem.Text];
                ImageViewer iv = new ImageViewer(Utilities.Assets[selectedItem.Text].GetImage());
                iv.Show();
            }
        }

        private void CopyImageIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (assetsListView.SelectedItems.Count == 1)
            {
                ListViewItem selectedItem = assetsListView.SelectedItems[0];
                string selectedId = selectedItem.Text.Split('/').Last();
                Clipboard.SetText(selectedId);
            }
        }

        private void ContentTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            objectType = contentTypeComboBox.Text.ToLower();
            if (contentTypeComboBox.SelectedIndex != contentTypeComboBox.Items.Count)
            {
                assetsListView.Groups.Clear();
            }
            LoadAssets();
        }

        private void AssetBrowser_Shown(object sender, EventArgs e)
        {

            LoadAssets();
        }
    }
}
