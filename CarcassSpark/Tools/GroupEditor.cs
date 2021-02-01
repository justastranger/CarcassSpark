using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        public GroupEditor(string currentGroup, string recentGroup, List<string> groups)
        {
            InitializeComponent();
            groupComboBox.Items.AddRange(groups.ToArray());
            if (recentGroup != null)
            {
                groupComboBox.Text = recentGroup;
                group = recentGroup;
            }
            else
            {
                groupComboBox.Text = currentGroup;
                group = currentGroup;
            }
        }

        private void GroupComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            group = groupComboBox.Text;
        }

        private void GroupComboBox_TextUpdate(object sender, EventArgs e)
        {
            group = groupComboBox.Text;
        }

        private void OkBbutton_Click(object sender, EventArgs e)
        {
            if (group == "" || group == null)
            {
                MessageBox.Show("Group Name can not be blank.");
                return;
            }
            if (group.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                MessageBox.Show("Invalid characters in group name.");
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void GroupComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            group = groupComboBox.Text;
        }
    }
}
