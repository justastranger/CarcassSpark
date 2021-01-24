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
    public partial class ImageImporter : Form
    {
        public string displayedImagePath;
        public string displayedImageType;
        public string displayedFileName;

        public ImageImporter()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (displayedImagePath == null || displayedImageType == null)
            {
                MessageBox.Show("Please select an image and image type.");
                return;
            }
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ChooseButton_Click(object sender, EventArgs e)
        {
            if (openImageFileDialog.ShowDialog() == DialogResult.OK)
            {
                displayPictureBox.ImageLocation = openImageFileDialog.FileName;
                displayedImagePath = openImageFileDialog.FileName;
                displayedFileName = openImageFileDialog.SafeFileName;
            }
        }

        private void TypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            displayedImageType = typeComboBox.Text;
        }
    }
}
