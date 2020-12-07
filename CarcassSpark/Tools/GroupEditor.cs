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
    public partial class GroupEditor : Form
    {
        public string group;

        public GroupEditor(string currentGroup, List<string> groups)
        {
            InitializeComponent();
            groupComboBox.Items.AddRange(groups.ToArray());
            groupComboBox.Text = currentGroup;
            group = currentGroup;
        }

        private void GroupComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            group = groupComboBox.SelectedValue as string;
        }

        private void GroupComboBox_TextUpdate(object sender, EventArgs e)
        {
            group = groupComboBox.Text;
        }

        private void okBbutton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
