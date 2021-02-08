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
    public partial class ImageViewer : Form
    {
        public ImageViewer(Image image)
        {
            InitializeComponent();
            pictureBox.Image = image;
            if (pictureBox.Size.Width < image.Size.Width)
            {
                int diff = image.Size.Width - pictureBox.Size.Width;
                this.Width += diff;
            }
            if (pictureBox.Size.Height < image.Size.Height)
            {
                int diff = image.Size.Height - pictureBox.Size.Height;
                this.Height += diff;
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
