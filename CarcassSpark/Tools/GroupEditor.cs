using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace CarcassSpark.Tools
{
    public partial class GroupEditor : Form
    {
        public string Group;

        public GroupEditor(string currentGroup, List<string> groups)
        {
            InitializeComponent();
            groupComboBox.Items.AddRange(groups.ToArray());
            groupComboBox.Text = currentGroup;
            Group = currentGroup;
        }

        public GroupEditor(string currentGroup, string recentGroup, List<string> groups)
        {
            InitializeComponent();
            groupComboBox.Items.AddRange(groups.ToArray());
            if (recentGroup != null)
            {
                groupComboBox.Text = recentGroup;
                Group = recentGroup;
            }
            else
            {
                groupComboBox.Text = currentGroup;
                Group = currentGroup;
            }
        }

        private void GroupComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            Group = groupComboBox.Text;
        }

        private void GroupComboBox_TextUpdate(object sender, EventArgs e)
        {
            Group = groupComboBox.Text;
        }

        private void OkBbutton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Group))
            {
                MessageBox.Show("Group Name can not be blank.");
                return;
            }
            if (Group.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
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
            Group = groupComboBox.Text;
        }
    }
}
