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

        public AspectViewer(Aspect aspect)
        {
            InitializeComponent();
            this.displayedAspect = aspect;
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
    }
}
