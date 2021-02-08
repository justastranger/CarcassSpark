using CarcassSpark.ObjectTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarcassSpark.ObjectViewers
{
    public partial class CultureViewer : Form
    {
        public Culture displayedCulture;
        List<string> uilabelkeyslist = new List<string>() {
            "UI_PAUSE",
            "UI_UNPAUSE",
            "UI_NORMALSPEED",
            "UI_FASTSPEED",
            "VERB_START",
            "VERB_COLLECT",
            "VERB_RUNNING",
            "VERB_ACCEPT",
            "UI_CLICK_TO_NAME",
            "UI_RELOAD",
            "UI_CONTINUE",
            "UI_RESUME",
            "UI_RESTART",
            "UI_RESTARTSURE",
            "UI_BROWSE",
            "UI_SAVEEXIT",
            "UI_OPTIONS",
            "UI_CORRUPTSAVE",
            "UI_CORRUPTDESC",
            "UI_CORRUPTCONTINUE",
            "UI_CORRUPTRELOAD",
            "UI_CORRUPTABANDON",
            "UI_OPTIONSTITLE",
            "UI_OPTIONSNOTES",
            "UI_OPTIONSNOTES_FE",
            "UI_OPT_MUSICVOL",
            "UI_OPT_SOUNDVOL",
            "UI_OPT_INFODURATION",
            "UI_OPT_UISCALING",
            "UI_OPT_AUTOSAVEINTERVAL",
            "UI_OPT_GRIDSNAPSIZE",
            "UI_OPT_CONTRAST",
            "UI_OPT_ACCESSIBLE_CARDS",
            "UI_OPT_BIRDWORM",
            "UI_BIRD",
            "UI_WORM",
            "UI_CARD",
            "UI_ON",
            "UI_OFF",
            "UI_SNAP_0",
            "UI_SNAP_1",
            "UI_SNAP_2",
            "UI_SNAP_3",
            "UI_KB_STARTRECIPE",
            "UI_KB_COLLECTALL",
            "UI_KB_PAUSE",
            "UI_KB_NORMALSPEED",
            "UI_KB_FASTSPEED",
            "UI_KB_STACKCARDS",
            "UI_KB_ZOOMIN",
            "UI_KB_ZOOMOUT",
            "UI_KB_ZOOM1",
            "UI_KB_ZOOM2",
            "UI_KB_ZOOM3",
            "UI_KB_TRUCKLEFT",
            "UI_KB_TRUCKRIGHT",
            "UI_KB_PEDESTALUP",
            "UI_KB_PEDESTALDOWN",
            "UI_VISUAL",
            "UI_AUDIO",
            "UI_GAMEPLAY",
            "UI_KEYBINDS",
            "UI_FIXEDSPACE",
            "UI_MINUTES_POSTFIX",
            "UI_SECONDS_POSTFIX",
            "UI_SECONDS_POSTFIX_SHORT",
            "UI_TIME_SEPARATOR",
            "UI_KNOWN_LINUX",
            "UI_KNOWN_LINUX_1",
            "UI_PURGE_SAVE",
            "UI_DLC_AND_MODS",
            "UI_LEAVE_GAME",
            "UI_DLC",
            "UI_DLC_PURCHASE",
            "UI_DLC_COMING_SOON",
            "UI_DLC_TITLE_DANCER",
            "UI_DLC_TITLE_PRIEST",
            "UI_DLC_TITLE_GHOUL",
            "UI_DLC_TITLE_EXILE",
            "UI_ALTERNATIVE_STARTS_BLURB",
            "UI_INSTALLED_MODS",
            "UI_MODS_INTRODUCTION",
            "UI_MODS_INTRODUCTION_STEAM",
            "UI_MODS_NO_MODS",
            "UI_CREDITS",
            "UI_BEGIN_GAME",
            "UI_INCOMPATIBLE_SAVE",
            "UI_INCOMPATIBLE_SAVE_1",
            "UI_CORRUPTED_SAVE_TITLE",
            "UI_CORRUPTED_SAVE_DESC",
            "UI_OPTIONS_FE",
            "UI_MANAGE_SAVES",
            "UI_MANAGE_SAVES_TITLE",
            "UI_SAVE_DENIED",
            "UI_SAVE_DENIED_DESC",
            "UI_PURGE_CONFIRM",
            "UI_PURGE_DESC",
            "UI_PURGE_PURGE",
            "UI_PURGE_KEEP",
            "UI_PLAYLEGACY_DESC",
            "UI_PLAYLEGACY_PURGE",
            "UI_PLAYLEGACY_KEEP",
            "UI_LANGUAGE",
            "UI_VERSION_NEWS",
            "UI_VERSION_NEWS_INTRO",
            "UI_VERSION_NEWS_TEXT",
            "UI_CREDITS_TITLE",
            "UI_RESPONSIBILITIES",
            "UI_BROWSE_SAVES",
            "UI_ENABLE",
            "UI_DISABLE",
            "UI_UPLOAD",
            "UI_UPDATE",
            "UI_BRING_THE_DAWN",
            "UI_PERPETUAL_EDITION",
            "UI_LOADEDTITLE",
            "UI_LOADEDDESC",
            "UI_LOADFAILEDTITLE",
            "UI_LOADFAILEDDESC",
            "LOADING_QUOTE_1",
            "LOADING_QUOTE_2",
            "LEGACY_MAIN_MENU",
            "LEGACY_ANOTHER_DESCENT",
            "LEGACY_DETERMINE",
            "LEGACY_BACK_TO_MENU",
            "LEGACY_START_NEW_GAME",
            "LEGACY_BECAUSE_PREFIX",
            "LEGACY_BECAUSE_POSTFIX",
            "UI_ASPECT",
            "UI_SLOT",
            "UI_GREEDY",
            "UI_CONSUMES",
            "UI_UNIQUE",
            "UI_UPCOMINGDRAWS",
            "UI_DECAYS",
            "UI_EMPTYSPACE",
            "UI_CANTMERGE",
            "UI_CANTPUT",
            "UI_ASPECTSREQUIRED",
            "UI_ASPECTSFORBIDDEN",
            "UI_OR",
            "UI_FULLSTOP",
            "UI_HINT",
            "UI_DEFAULTNAME",
            "UI_LANGSWITCHNOTES",
            "UI_OPT_RESOLUTION",
            "UI_OPT_WINDOWED",
            "UI_OPT_GRAPHICS_LEVEL",
            "GRAPHICS_LEVEL_1",
            "GRAPHICS_LEVEL_2",
            "GRAPHICS_LEVEL_3"
        }; 

        public CultureViewer(Culture culture, bool? editing)
        {
            InitializeComponent();
            FillValues(culture);
            if (editing.HasValue)
            {
                SetEditingMode(editing.Value);
            }
        }

        public CultureViewer(Culture culture)
        {
            InitializeComponent();
            FillValues(culture);
            SetEditingMode(false);
        }

        void SetEditingMode(bool editing)
        {
            okButton.Visible = editing;
            cancelButton.Text = editing ? "Cancel" : "Close";
            idTextBox.ReadOnly = !editing;
            endonymTextBox.ReadOnly = !editing;
            exonymTextBox.ReadOnly = !editing;
            fontScriptTextBox.ReadOnly = !editing;
            UiLabelDataGridView.ReadOnly = !editing;
            boldAllowedCheckBox.Enabled = editing;
            releasedCheckBox.Enabled = editing;
        }

        void FillValues(Culture culture)
        {
            displayedCulture = culture;
            idTextBox.Text = culture.id;
            endonymTextBox.Text = culture.endonym;
            exonymTextBox.Text = culture.exonym;
            fontScriptTextBox.Text = culture.fontscript;
            if (culture.boldallowed.HasValue) boldAllowedCheckBox.Checked = culture.boldallowed.Value;
            if (culture.released.HasValue) releasedCheckBox.Checked = culture.released.Value;
            if (culture.uilabels != null && culture.uilabels.Count > 0)
            {
                foreach (KeyValuePair<string, string> uilabel in culture.uilabels)
                {
                    // Add the existing keys to the datagridview
                    UiLabelDataGridView.Rows.Add(uilabel.Key, uilabel.Value);
                    if (uilabelkeyslist.Contains(uilabel.Key))
                    {
                        // and check off that key from the list
                        uilabelkeyslist.Remove(uilabel.Key);
                    }
                }
            }
            // if there are and keys left in the list, add them so that the culture has all of them.
            if (uilabelkeyslist.Count > 0)
            {
                foreach (string uilabelkey in uilabelkeyslist)
                {
                    UiLabelDataGridView.Rows.Add(uilabelkey, "");
                }
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (idTextBox.Text == "" || endonymTextBox.Text == "" || exonymTextBox.Text == "" || fontScriptTextBox.Text == "")
            {
                MessageBox.Show("ID, Endonym, Exonym, and Font Script are all required values.");
                return;
            }
            if (UiLabelDataGridView.Rows.Count > 0)
            {
                displayedCulture.uilabels = new Dictionary<string, string>();
                foreach (DataGridViewRow row in UiLabelDataGridView.Rows)
                {
                    string key = row.Cells[0].Value as string;
                    string value = row.Cells[1].Value as string;
                    // only save the keys with a value. Also prevents the saving of the final, blank row that always exists in a DataGridView for some reason.
                    if (value != "" && value != null) displayedCulture.uilabels.Add(key, value);
                }
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void IdTextBox_TextChanged(object sender, EventArgs e)
        {
            if (idTextBox.Text != "") displayedCulture.id = idTextBox.Text;
            else displayedCulture.id = null;
        }

        private void EndonymTextBox_TextChanged(object sender, EventArgs e)
        {
            if (endonymTextBox.Text != "") displayedCulture.endonym = endonymTextBox.Text;
            else displayedCulture.endonym = null;
        }

        private void ExonymTextBox_TextChanged(object sender, EventArgs e)
        {
            if (exonymTextBox.Text != "") displayedCulture.exonym = exonymTextBox.Text;
            else displayedCulture.exonym = null;
        }

        private void FontScriptTextBox_TextChanged(object sender, EventArgs e)
        {
            if (fontScriptTextBox.Text != "") displayedCulture.fontscript = fontScriptTextBox.Text;
            else displayedCulture.fontscript = null;
        }

        private void BoldAllowedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            displayedCulture.boldallowed = boldAllowedCheckBox.Checked;
        }

        private void ReleasedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            displayedCulture.released = releasedCheckBox.Checked;
        }
    }
}
