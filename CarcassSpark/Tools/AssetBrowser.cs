using AssetStudio;
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
    public partial class AssetBrowser : Form
    {
        // private ImageList imageList = new ImageList();
        private string objectType = "aspects";

        public AssetBrowser()
        {
            InitializeComponent();
            LoadAssets();
        }

        private void LoadAssets()
        {
            if (Utilities.ImageList == null)
            {
                Utilities.ImageList = new ImageList
                {
                    ImageSize = new Size(128, 128)
                };
            }

            assetsListView.LargeImageList = Utilities.ImageList;
            HashSet<ListViewItem> listViewItems = new HashSet<ListViewItem>();

            foreach (string path in Utilities.assets.Keys)
            {
                string folder = path.Split('/').Count() > 1 ? path.Split('/')[1] : path;
                ListViewGroup folderGroup = assetsListView.Groups[folder] ?? new ListViewGroup(folder, folder);

                if (objectType != "all" && objectType != folder)
                {
                    continue;
                }

                if (!assetsListView.Groups.Contains(folderGroup))
                {
                    assetsListView.Groups.Add(folderGroup);
                }

                if (!Utilities.ImageList.Images.ContainsKey(path))
                {
                    Utilities.ImageList.Images.Add(path, Utilities.assets[path].GetImage());
                }

                ListViewItem item = new ListViewItem(path)
                {
                    ImageKey = path,
                    Group = folderGroup
                };

                folderGroup.Items.Add(item);
                listViewItems.Add(item);
            }
            assetsListView.Items.AddRange(listViewItems.ToArray());
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
                ImageViewer iv = new ImageViewer(Utilities.assets[selectedItem.Text].GetImage());
                iv.Show();
            }
        }

        private void CopyImageIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (assetsListView.SelectedItems.Count == 1)
            {
                ListViewItem selectedItem = assetsListView.SelectedItems[0];
                string selectedID = selectedItem.Text.Split('/').Last();
                Clipboard.SetText(selectedID);
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
    }
}
