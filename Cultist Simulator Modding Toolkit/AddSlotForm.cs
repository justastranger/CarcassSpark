using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Cultist_Simulator_Modding_Toolkit.Element;

namespace Cultist_Simulator_Modding_Toolkit
{
    public partial class AddSlotForm : Form
    {

        // both of these start as null
        Dictionary<string, int> required, forbidden;


        public AddSlotForm()
        {
            InitializeComponent();
        }

    }
}
