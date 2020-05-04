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
        Required required, forbidden;


        public AddSlotForm()
        {
            InitializeComponent();
        }

        Slot generateSlot()
        {
            // Slot(string id, string label, ElementDictionary required, string description, string actionId, ElementDictionary forbidden = null)
            return new Slot(idTextBox.Text, labelTextBox.Text, required, descriptionTextBox.Text, actionIdTextBox.Text, forbidden);
        }

    }
}
