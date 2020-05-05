using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cultist_Simulator_Modding_Toolkit
{
    public partial class EndingViewer : Form
    {
        Ending displayedEnding;
        ModViewer currentMod;

        public EndingViewer(Ending ending, ModViewer currentMod)
        {
            InitializeComponent();
            displayedEnding = ending;
            this.currentMod = currentMod;
            idTextBox.Text = ending.id;
            labelTextBox.Text = ending.label;
            imageTextBox.Text = ending.image;
            flavorTextBox.Text = ending.flavour;
            animTextBox.Text = ending.anim;
            descriptionTextBox.Text = ending.description;
        }
    }
}
