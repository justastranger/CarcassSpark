using CarcassSpark.ObjectTypes;
using CarcassSpark.ObjectViewers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CarcassSpark.Tools
{
    public partial class CulturesViewer : Form
    {
        public Dictionary<string, Culture> DisplayedCultures;
        public bool Editing;

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
            SetEditingMode(editing.HasValue && editing.Value);
        }

        private void SetEditingMode(bool editing)
        {
            this.Editing = editing;
            okButton.Visible = editing;
            newCultureButton.Visible = editing;
            cancelButton.Text = editing ? "Cancel" : "Close";
        }

        private void FillValues(Dictionary<Guid, Culture> cultures)
        {
            DisplayedCultures = cultures.ToDictionary(entry => entry.Value.id,
                                                      entry => entry.Value.Copy());
            foreach (string key in DisplayedCultures.Keys)
            {
                culturesListBox.Items.Add(key);
            }
        }

        private void CulturesListBox_DoubleClick(object sender, EventArgs e)
        {
            if (culturesListBox.SelectedItem == null)
            {
                return;
            }

            string id = culturesListBox.SelectedItem as string;
            CultureViewer cv = new CultureViewer(DisplayedCultures[id].Copy(), Editing);
            if (cv.ShowDialog() == DialogResult.OK)
            {
                if (cv.DisplayedCulture.id != id)
                {
                    DisplayedCultures.Remove(id);
                    DisplayedCultures[cv.DisplayedCulture.id] = cv.DisplayedCulture.Copy();
                }
                else
                {
                    DisplayedCultures[id] = cv.DisplayedCulture.Copy();
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
                DisplayedCultures[cultureViewer.DisplayedCulture.id] = cultureViewer.DisplayedCulture.Copy();
                if (!culturesListBox.Items.Contains(cultureViewer.DisplayedCulture.id))
                {
                    culturesListBox.Items.Add(cultureViewer.DisplayedCulture.id);
                }
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (culturesListBox.SelectedItem == null)
            {
                return;
            }

            string id = culturesListBox.SelectedItem as string;
            if (DisplayedCultures.ContainsKey(id))
            {
                DisplayedCultures.Remove(id);
            }

            if (culturesListBox.Items.Contains(id))
            {
                culturesListBox.Items.Remove(id);
            }
        }
    }
}
