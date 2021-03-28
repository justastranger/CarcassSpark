using System;
using System.Windows.Forms;

namespace CarcassSpark.Tools
{
    public partial class ImageImporter : Form
    {
        public string DisplayedImagePath;
        public string DisplayedImageType;
        public string DisplayedFileName;

        public ImageImporter()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (DisplayedImagePath == null || DisplayedImageType == null)
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
                DisplayedImagePath = openImageFileDialog.FileName;
                DisplayedFileName = openImageFileDialog.SafeFileName;
            }
        }

        private void TypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayedImageType = typeComboBox.Text;
        }
    }
}
