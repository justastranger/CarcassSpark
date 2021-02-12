using System;
using System.Drawing;
using System.Windows.Forms;

namespace CarcassSpark.Tools
{
    public partial class ImageViewer : Form
    {
        public ImageViewer(Image image)
        {
            InitializeComponent();
            pictureBox.Image = image;
            if (pictureBox.Size.Width < image.Size.Width)
            {
                int diff = image.Size.Width - pictureBox.Size.Width;
                Width += diff;
            }
            if (pictureBox.Size.Height < image.Size.Height)
            {
                int diff = image.Size.Height - pictureBox.Size.Height;
                Height += diff;
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
