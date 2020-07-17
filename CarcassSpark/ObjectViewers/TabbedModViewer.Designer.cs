namespace CarcassSpark.ObjectViewers
{
    partial class TabbedModViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TabbedModViewer));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.fileToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.openModToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newModToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openManifestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadContentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveModToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveModToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeModToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleEditModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleAutosaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newItemToolStripButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.aspectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.elementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recipeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.legacyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.endingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verbToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.fromClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aspectToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.elementToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.recipeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deckToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.legacyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.endingToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.verbToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.summonGeneratorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageImporterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.ModViewerTabs = new System.Windows.Forms.TabControl();
            this.modFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripDropDownButton,
            this.newItemToolStripButton,
            this.importToolStripButton,
            this.toolsToolStripButton,
            this.settingsToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(935, 25);
            this.toolStrip1.TabIndex = 15;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // fileToolStripDropDownButton
            // 
            this.fileToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fileToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openModToolStripMenuItem,
            this.newModToolStripMenuItem,
            this.openManifestToolStripMenuItem,
            this.reloadContentToolStripMenuItem,
            this.saveModToolStripMenuItem,
            this.saveModToToolStripMenuItem,
            this.closeModToolStripMenuItem,
            this.toggleEditModeToolStripMenuItem,
            this.toggleAutosaveToolStripMenuItem});
            this.fileToolStripDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("fileToolStripDropDownButton.Image")));
            this.fileToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fileToolStripDropDownButton.Name = "fileToolStripDropDownButton";
            this.fileToolStripDropDownButton.Size = new System.Drawing.Size(38, 22);
            this.fileToolStripDropDownButton.Text = "File";
            // 
            // openModToolStripMenuItem
            // 
            this.openModToolStripMenuItem.Name = "openModToolStripMenuItem";
            this.openModToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openModToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.openModToolStripMenuItem.Text = "Open Mod";
            this.openModToolStripMenuItem.Click += new System.EventHandler(this.openModToolStripMenuItem_Click);
            // 
            // newModToolStripMenuItem
            // 
            this.newModToolStripMenuItem.Name = "newModToolStripMenuItem";
            this.newModToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.newModToolStripMenuItem.Text = "New Mod";
            this.newModToolStripMenuItem.Click += new System.EventHandler(this.newModToolStripMenuItem_Click);
            // 
            // openManifestToolStripMenuItem
            // 
            this.openManifestToolStripMenuItem.Name = "openManifestToolStripMenuItem";
            this.openManifestToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.openManifestToolStripMenuItem.Text = "Open Manifest";
            this.openManifestToolStripMenuItem.Click += new System.EventHandler(this.openManifestToolStripMenuItem_Click);
            // 
            // reloadContentToolStripMenuItem
            // 
            this.reloadContentToolStripMenuItem.Name = "reloadContentToolStripMenuItem";
            this.reloadContentToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.reloadContentToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.reloadContentToolStripMenuItem.Text = "Reload Content";
            this.reloadContentToolStripMenuItem.Click += new System.EventHandler(this.reloadContentToolStripMenuItem_Click);
            // 
            // saveModToolStripMenuItem
            // 
            this.saveModToolStripMenuItem.Name = "saveModToolStripMenuItem";
            this.saveModToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveModToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.saveModToolStripMenuItem.Text = "Save Mod";
            this.saveModToolStripMenuItem.Click += new System.EventHandler(this.saveModToolStripMenuItem_Click);
            // 
            // saveModToToolStripMenuItem
            // 
            this.saveModToToolStripMenuItem.Name = "saveModToToolStripMenuItem";
            this.saveModToToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveModToToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.saveModToToolStripMenuItem.Text = "Save Mod To...";
            this.saveModToToolStripMenuItem.Click += new System.EventHandler(this.saveModToToolStripMenuItem_Click);
            // 
            // closeModToolStripMenuItem
            // 
            this.closeModToolStripMenuItem.Name = "closeModToolStripMenuItem";
            this.closeModToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.closeModToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.closeModToolStripMenuItem.Text = "Close Mod";
            this.closeModToolStripMenuItem.Click += new System.EventHandler(this.closeModToolStripMenuItem_Click);
            // 
            // toggleEditModeToolStripMenuItem
            // 
            this.toggleEditModeToolStripMenuItem.Name = "toggleEditModeToolStripMenuItem";
            this.toggleEditModeToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.toggleEditModeToolStripMenuItem.Text = "Toggle Edit Mode";
            this.toggleEditModeToolStripMenuItem.Click += new System.EventHandler(this.toggleEditModeToolStripMenuItem_Click);
            // 
            // toggleAutosaveToolStripMenuItem
            // 
            this.toggleAutosaveToolStripMenuItem.Name = "toggleAutosaveToolStripMenuItem";
            this.toggleAutosaveToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.toggleAutosaveToolStripMenuItem.Text = "Toggle Autosave";
            this.toggleAutosaveToolStripMenuItem.Click += new System.EventHandler(this.toggleAutosaveToolStripMenuItem_Click);
            // 
            // newItemToolStripButton
            // 
            this.newItemToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.newItemToolStripButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aspectToolStripMenuItem,
            this.elementToolStripMenuItem,
            this.recipeToolStripMenuItem,
            this.deckToolStripMenuItem,
            this.legacyToolStripMenuItem,
            this.endingToolStripMenuItem,
            this.verbToolStripMenuItem});
            this.newItemToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newItemToolStripButton.Image")));
            this.newItemToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newItemToolStripButton.Name = "newItemToolStripButton";
            this.newItemToolStripButton.Size = new System.Drawing.Size(44, 22);
            this.newItemToolStripButton.Text = "New";
            // 
            // aspectToolStripMenuItem
            // 
            this.aspectToolStripMenuItem.Name = "aspectToolStripMenuItem";
            this.aspectToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.aspectToolStripMenuItem.Text = "Aspect";
            this.aspectToolStripMenuItem.Click += new System.EventHandler(this.aspectToolStripMenuItem_Click);
            // 
            // elementToolStripMenuItem
            // 
            this.elementToolStripMenuItem.Name = "elementToolStripMenuItem";
            this.elementToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.elementToolStripMenuItem.Text = "Element";
            this.elementToolStripMenuItem.Click += new System.EventHandler(this.elementToolStripMenuItem_Click);
            // 
            // recipeToolStripMenuItem
            // 
            this.recipeToolStripMenuItem.Name = "recipeToolStripMenuItem";
            this.recipeToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.recipeToolStripMenuItem.Text = "Recipe";
            this.recipeToolStripMenuItem.Click += new System.EventHandler(this.recipeToolStripMenuItem_Click);
            // 
            // deckToolStripMenuItem
            // 
            this.deckToolStripMenuItem.Name = "deckToolStripMenuItem";
            this.deckToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.deckToolStripMenuItem.Text = "Deck";
            this.deckToolStripMenuItem.Click += new System.EventHandler(this.deckToolStripMenuItem_Click);
            // 
            // legacyToolStripMenuItem
            // 
            this.legacyToolStripMenuItem.Name = "legacyToolStripMenuItem";
            this.legacyToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.legacyToolStripMenuItem.Text = "Legacy";
            this.legacyToolStripMenuItem.Click += new System.EventHandler(this.legacyToolStripMenuItem_Click);
            // 
            // endingToolStripMenuItem
            // 
            this.endingToolStripMenuItem.Name = "endingToolStripMenuItem";
            this.endingToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.endingToolStripMenuItem.Text = "Ending";
            this.endingToolStripMenuItem.Click += new System.EventHandler(this.endingToolStripMenuItem_Click);
            // 
            // verbToolStripMenuItem
            // 
            this.verbToolStripMenuItem.Name = "verbToolStripMenuItem";
            this.verbToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.verbToolStripMenuItem.Text = "Verb";
            this.verbToolStripMenuItem.Click += new System.EventHandler(this.verbToolStripMenuItem_Click);
            // 
            // importToolStripButton
            // 
            this.importToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.importToolStripButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fromClipboardToolStripMenuItem,
            this.aspectToolStripMenuItem1,
            this.elementToolStripMenuItem1,
            this.recipeToolStripMenuItem1,
            this.deckToolStripMenuItem1,
            this.legacyToolStripMenuItem1,
            this.endingToolStripMenuItem1,
            this.verbToolStripMenuItem1});
            this.importToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("importToolStripButton.Image")));
            this.importToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.importToolStripButton.Name = "importToolStripButton";
            this.importToolStripButton.Size = new System.Drawing.Size(56, 22);
            this.importToolStripButton.Text = "Import";
            // 
            // fromClipboardToolStripMenuItem
            // 
            this.fromClipboardToolStripMenuItem.Name = "fromClipboardToolStripMenuItem";
            this.fromClipboardToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.fromClipboardToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.fromClipboardToolStripMenuItem.Text = "From Clipboard";
            this.fromClipboardToolStripMenuItem.Click += new System.EventHandler(this.fromClipboardToolStripMenuItem_Click);
            // 
            // aspectToolStripMenuItem1
            // 
            this.aspectToolStripMenuItem1.Name = "aspectToolStripMenuItem1";
            this.aspectToolStripMenuItem1.Size = new System.Drawing.Size(198, 22);
            this.aspectToolStripMenuItem1.Text = "Aspect";
            // 
            // elementToolStripMenuItem1
            // 
            this.elementToolStripMenuItem1.Name = "elementToolStripMenuItem1";
            this.elementToolStripMenuItem1.Size = new System.Drawing.Size(198, 22);
            this.elementToolStripMenuItem1.Text = "Element";
            // 
            // recipeToolStripMenuItem1
            // 
            this.recipeToolStripMenuItem1.Name = "recipeToolStripMenuItem1";
            this.recipeToolStripMenuItem1.Size = new System.Drawing.Size(198, 22);
            this.recipeToolStripMenuItem1.Text = "Recipe";
            // 
            // deckToolStripMenuItem1
            // 
            this.deckToolStripMenuItem1.Name = "deckToolStripMenuItem1";
            this.deckToolStripMenuItem1.Size = new System.Drawing.Size(198, 22);
            this.deckToolStripMenuItem1.Text = "Deck";
            this.deckToolStripMenuItem1.Click += new System.EventHandler(this.deckToolStripMenuItem1_Click);
            // 
            // legacyToolStripMenuItem1
            // 
            this.legacyToolStripMenuItem1.Name = "legacyToolStripMenuItem1";
            this.legacyToolStripMenuItem1.Size = new System.Drawing.Size(198, 22);
            this.legacyToolStripMenuItem1.Text = "Legacy";
            this.legacyToolStripMenuItem1.Click += new System.EventHandler(this.legacyToolStripMenuItem1_Click);
            // 
            // endingToolStripMenuItem1
            // 
            this.endingToolStripMenuItem1.Name = "endingToolStripMenuItem1";
            this.endingToolStripMenuItem1.Size = new System.Drawing.Size(198, 22);
            this.endingToolStripMenuItem1.Text = "Ending";
            this.endingToolStripMenuItem1.Click += new System.EventHandler(this.endingToolStripMenuItem1_Click);
            // 
            // verbToolStripMenuItem1
            // 
            this.verbToolStripMenuItem1.Name = "verbToolStripMenuItem1";
            this.verbToolStripMenuItem1.Size = new System.Drawing.Size(198, 22);
            this.verbToolStripMenuItem1.Text = "Verb";
            this.verbToolStripMenuItem1.Click += new System.EventHandler(this.verbToolStripMenuItem1_Click);
            // 
            // toolsToolStripButton
            // 
            this.toolsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolsToolStripButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.summonGeneratorToolStripMenuItem,
            this.imageImporterToolStripMenuItem});
            this.toolsToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("toolsToolStripButton.Image")));
            this.toolsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolsToolStripButton.Name = "toolsToolStripButton";
            this.toolsToolStripButton.Size = new System.Drawing.Size(47, 22);
            this.toolsToolStripButton.Text = "Tools";
            // 
            // summonGeneratorToolStripMenuItem
            // 
            this.summonGeneratorToolStripMenuItem.Name = "summonGeneratorToolStripMenuItem";
            this.summonGeneratorToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.summonGeneratorToolStripMenuItem.Text = "Summon Generator";
            this.summonGeneratorToolStripMenuItem.Click += new System.EventHandler(this.summonGeneratorToolStripMenuItem_Click);
            // 
            // imageImporterToolStripMenuItem
            // 
            this.imageImporterToolStripMenuItem.Name = "imageImporterToolStripMenuItem";
            this.imageImporterToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.imageImporterToolStripMenuItem.Text = "Image Importer";
            this.imageImporterToolStripMenuItem.Click += new System.EventHandler(this.imageImporterToolStripMenuItem_Click);
            // 
            // settingsToolStripButton
            // 
            this.settingsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.settingsToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("settingsToolStripButton.Image")));
            this.settingsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsToolStripButton.Name = "settingsToolStripButton";
            this.settingsToolStripButton.Size = new System.Drawing.Size(53, 22);
            this.settingsToolStripButton.Text = "Settings";
            this.settingsToolStripButton.Click += new System.EventHandler(this.settingsToolStripButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 347);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(935, 22);
            this.statusStrip1.TabIndex = 26;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // ModViewerTabs
            // 
            this.ModViewerTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ModViewerTabs.Location = new System.Drawing.Point(0, 28);
            this.ModViewerTabs.Name = "ModViewerTabs";
            this.ModViewerTabs.SelectedIndex = 0;
            this.ModViewerTabs.Size = new System.Drawing.Size(935, 316);
            this.ModViewerTabs.TabIndex = 27;
            this.ModViewerTabs.SelectedIndexChanged += new System.EventHandler(this.ModViewerTabs_SelectedIndexChanged);
            // 
            // modFolderBrowserDialog
            // 
            this.modFolderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "JSON files|*.json";
            // 
            // TabbedModViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 369);
            this.Controls.Add(this.ModViewerTabs);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TabbedModViewer";
            this.Text = "Carcass Spark";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton fileToolStripDropDownButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.TabControl ModViewerTabs;
        private System.Windows.Forms.ToolStripMenuItem openModToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog modFolderBrowserDialog;
        private System.Windows.Forms.ToolStripMenuItem closeModToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newModToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveModToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveModToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openManifestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadContentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleEditModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleAutosaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton newItemToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem aspectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem elementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recipeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deckToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem legacyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem endingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verbToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton importToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem fromClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aspectToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem elementToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem recipeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deckToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem legacyToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem endingToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem verbToolStripMenuItem1;
        private System.Windows.Forms.ToolStripDropDownButton toolsToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem summonGeneratorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imageImporterToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripButton settingsToolStripButton;
    }
}

