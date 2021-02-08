using CarcassSpark.ObjectTypes;
using CarcassSpark.ObjectViewers;
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
    public partial class CulturesViewer : Form
    {
        public Dictionary<string, Culture> displayedCultures;
        public bool editing;

        public CulturesViewer(Dictionary<Guid, Culture> cultures)
        {
            InitializeComponent();
            FillValues(cultures);
            SetEditingMode(false);
        }

        public CulturesViewer(Dictionary<Guid, Culture> cultures, bool? editing)
        {
            InitializeComponent();
            FillValues(cultures);
            if (editing.HasValue)
            {
                SetEditingMode(editing.Value);
            }
            else
            {
                SetEditingMode(false);
            }
        }

        void SetEditingMode(bool editing)
        {
            this.editing = editing;
            okButton.Visible = editing;
            newCultureButton.Visible = editing;
            cancelButton.Text = editing ? "Cancel" : "Close";
        }

        void FillValues(Dictionary<Guid, Culture> cultures)
        {
            displayedCultures = cultures.ToDictionary(entry => entry.Value.id,
                                                      entry => entry.Value.Copy());
            foreach (string key in displayedCultures.Keys)
            {
                culturesListBox.Items.Add(key);
            }
        }

        private void CulturesListBox_DoubleClick(object sender, EventArgs e)
        {
            if (culturesListBox.SelectedItem == null) return;
            string id = culturesListBox.SelectedItem as string;
            CultureViewer cv = new CultureViewer(displayedCultures[id].Copy(), editing);
            if (cv.ShowDialog() == DialogResult.OK)
            {
                if (cv.displayedCulture.id != id)
                {
                    displayedCultures.Remove(id);
                    displayedCultures[cv.displayedCulture.id] = cv.displayedCulture.Copy();
                }
                else
                {
                    displayedCultures[id] = cv.displayedCulture.Copy();
                }
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void NewCultureButton_Click(object sender, EventArgs e)
        {
            CultureViewer cultureViewer = new CultureViewer(new Culture(), true);
            if (cultureViewer.ShowDialog() == DialogResult.OK)
            {
                displayedCultures[cultureViewer.displayedCulture.id] = cultureViewer.displayedCulture.Copy();
                if (!culturesListBox.Items.Contains(cultureViewer.displayedCulture.id))
                {
                    culturesListBox.Items.Add(cultureViewer.displayedCulture.id);
                }
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (culturesListBox.SelectedItem == null) return;
            string id = culturesListBox.SelectedItem as string;
            if (displayedCultures.ContainsKey(id)) displayedCultures.Remove(id);
            if (culturesListBox.Items.Contains(id)) culturesListBox.Items.Remove(id);
        }
    }
}
