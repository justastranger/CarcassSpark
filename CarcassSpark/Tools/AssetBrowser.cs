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

        public AssetBrowser()
        {
            InitializeComponent();
            LoadAssets();
        }

        private void LoadAssets()
        {
            if (Utilities.ImageList != null && Utilities.ImageList.Images.Count > 0)
            {
                assetsListView.LargeImageList = Utilities.ImageList;
                foreach (string path in Utilities.assets.Keys)
                {
                    ListViewItem item = new ListViewItem(path)
                    {
                        ImageKey = path
                    };
                    assetsListView.Items.Add(item);
                }
            }
            else
            {
                Utilities.ImageList = new ImageList();
                Utilities.ImageList.ImageSize = new Size(128, 128);
                assetsListView.LargeImageList = Utilities.ImageList;
                foreach (string path in Utilities.assets.Keys)
                {
                    string folder = path.Split('/').Count() > 1 ? path.Split('/')[1] : path;
                    ListViewGroup folderGroup;
                    if (assetsListView.Groups[folder] != null)
                    {
                        folderGroup = assetsListView.Groups[folder];
                    }
                    else
                    {
                        folderGroup = new ListViewGroup(folder, folder);
                        assetsListView.Groups.Add(folderGroup);
                    }
                    Utilities.ImageList.Images.Add(path, Utilities.assets[path].GetImage());
                    ListViewItem item = new ListViewItem(path)
                    {
                        ImageKey = path
                    };
                    folderGroup.Items.Add(item);
                    assetsListView.Items.Add(item);
                }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Dispose();
            Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Dispose();
            Close();
        }

        private void assetsListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (assetsListView.SelectedItems.Count == 1)
            {
                ListViewItem selectedItem = assetsListView.SelectedItems[0];
                // Image selectedImage = assetsListView.LargeImageList.Images[selectedItem.Text];
                ImageViewer iv = new ImageViewer(Utilities.assets[selectedItem.Text].GetImage());
                iv.Show();
            }
        }

        private void copyImageIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (assetsListView.SelectedItems.Count == 1)
            {
                ListViewItem selectedItem = assetsListView.SelectedItems[0];
                string selectedID = selectedItem.Text.Split('/').Last();
                Clipboard.SetText(selectedID);
            }
        }
    }
}
