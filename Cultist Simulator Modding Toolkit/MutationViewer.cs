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
    public partial class MutationViewer : Form
    {
        Recipe.Mutation displayedMutation;
        ModViewer currentMod;

        public MutationViewer(Recipe.Mutation mutation, ModViewer currentMod)
        {
            InitializeComponent();
            displayedMutation = mutation;
            this.currentMod = currentMod;
            filterTextBox.Text = mutation.filter;
            mutateAspectIdTextBox.Text = mutation.mutateAspectId;
            levelNumericUpDown.Value = mutation.level;
        }
    }
}
