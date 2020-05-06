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

        public AspectViewer(Aspect aspect, bool? editing)
        {
            InitializeComponent();
            this.displayedAspect = aspect;
            if(aspect.extends != null)
            {
                Aspect extendedAspect = Utilities.getAspect(aspect.extends[0]);
                extendsTextBox.Text = aspect.extends[0];
                fillValues(extendedAspect);
            }
            fillValues(aspect);
            if (editing.HasValue) setEditingMode(editing.Value);
            else setEditingMode(false);
        }

        void setEditingMode(bool editing)
        {
            idTextBox.Enabled = editing;
            labelTextBox.Enabled = editing;
            iconTextBox.Enabled = editing;
            descriptionTextBox.Enabled = editing;
            extendsTextBox.Enabled = editing;
            inducesDataGridView.ReadOnly = !editing;
        }

        public void fillValues(Aspect aspect)
        {
            if (aspect.id != null) idTextBox.Text = aspect.id;
            if (aspect.label != null) labelTextBox.Text = aspect.label;
            if (aspect.icon != null) iconTextBox.Text = aspect.icon;
            if (aspect.description != null) descriptionTextBox.Text = aspect.description;
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
            RecipeViewer rv = new RecipeViewer(Utilities.getRecipe(id), false);
            rv.ShowDialog();
        }
    }
}
