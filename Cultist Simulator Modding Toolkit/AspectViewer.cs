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
    public partial class AspectViewer : Form
    {
        Aspect displayedAspect;
        ModViewer currentMod;

        public AspectViewer(Aspect aspect, ModViewer currentMod)
        {
            InitializeComponent();
            this.displayedAspect = aspect;
            this.currentMod = currentMod;
            idTextBox.Text = aspect.id;
            labelTextBox.Text = aspect.label;
            iconTextBox.Text = aspect.icon;
            descriptionTextBox.Text = aspect.description;
            if (aspect.induces != null)
            {
                foreach (Aspect.Induces induces in aspect.induces)
                {
                    inducesDataGridView.Rows.Add(induces.id, induces.chance);
                }
            }
        }

        private void inducesDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = inducesDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            RecipeViewer rv = new RecipeViewer(currentMod.getRecipe(id), currentMod);
            rv.ShowDialog();
        }
    }
}
