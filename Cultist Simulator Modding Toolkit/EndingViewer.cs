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

        public EndingViewer(Ending ending, bool? editing)
        {
            InitializeComponent();
            displayedEnding = ending;
            idTextBox.Text = ending.id;
            labelTextBox.Text = ending.label;
            imageTextBox.Text = ending.image;
            flavorTextBox.Text = ending.flavour;
            animTextBox.Text = ending.anim;
            descriptionTextBox.Text = ending.description;
            if (editing.HasValue) setEditingMode(editing.Value);
            else setEditingMode(false);
        }

        void setEditingMode(bool editing)
        {
            idTextBox.Enabled = editing;
            labelTextBox.Enabled = editing;
            imageTextBox.Enabled = editing;
            flavorTextBox.Enabled = editing;
            animTextBox.Enabled = editing;
            descriptionTextBox.Enabled = editing;
        }
    }
}
