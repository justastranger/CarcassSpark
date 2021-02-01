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
            this.saveSplitterLocationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.jSONCleanerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.culturesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.assetBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.templateManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.aboutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.ModViewerTabs = new System.Windows.Forms.TabControl();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.openInExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.settingsToolStripButton,
            this.aboutToolStripButton});
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
            this.openInExplorerToolStripMenuItem,
            this.openManifestToolStripMenuItem,
            this.reloadContentToolStripMenuItem,
            this.saveModToolStripMenuItem,
            this.saveModToToolStripMenuItem,
            this.closeModToolStripMenuItem,
            this.toggleEditModeToolStripMenuItem,
            this.toggleAutosaveToolStripMenuItem,
            this.saveSplitterLocationsToolStripMenuItem});
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
            this.openModToolStripMenuItem.Click += new System.EventHandler(this.OpenModToolStripMenuItem_Click);
            // 
            // newModToolStripMenuItem
            // 
            this.newModToolStripMenuItem.Name = "newModToolStripMenuItem";
            this.newModToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.newModToolStripMenuItem.Text = "New Mod";
            this.newModToolStripMenuItem.Click += new System.EventHandler(this.NewModToolStripMenuItem_Click);
            // 
            // openManifestToolStripMenuItem
            // 
            this.openManifestToolStripMenuItem.Name = "openManifestToolStripMenuItem";
            this.openManifestToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.openManifestToolStripMenuItem.Text = "Open Synopsis";
            this.openManifestToolStripMenuItem.Click += new System.EventHandler(this.OpenSynopsisToolStripMenuItem_Click);
            // 
            // reloadContentToolStripMenuItem
            // 
            this.reloadContentToolStripMenuItem.Name = "reloadContentToolStripMenuItem";
            this.reloadContentToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.reloadContentToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.reloadContentToolStripMenuItem.Text = "Reload Content";
            this.reloadContentToolStripMenuItem.Click += new System.EventHandler(this.ReloadContentToolStripMenuItem_Click);
            // 
            // saveModToolStripMenuItem
            // 
            this.saveModToolStripMenuItem.Name = "saveModToolStripMenuItem";
            this.saveModToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveModToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.saveModToolStripMenuItem.Text = "Save Mod";
            this.saveModToolStripMenuItem.Click += new System.EventHandler(this.SaveModToolStripMenuItem_Click);
            // 
            // saveModToToolStripMenuItem
            // 
            this.saveModToToolStripMenuItem.Name = "saveModToToolStripMenuItem";
            this.saveModToToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveModToToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.saveModToToolStripMenuItem.Text = "Save Mod To...";
            this.saveModToToolStripMenuItem.Click += new System.EventHandler(this.SaveModToToolStripMenuItem_Click);
            // 
            // closeModToolStripMenuItem
            // 
            this.closeModToolStripMenuItem.Name = "closeModToolStripMenuItem";
            this.closeModToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.closeModToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.closeModToolStripMenuItem.Text = "Close Mod";
            this.closeModToolStripMenuItem.Click += new System.EventHandler(this.CloseModToolStripMenuItem_Click);
            // 
            // toggleEditModeToolStripMenuItem
            // 
            this.toggleEditModeToolStripMenuItem.Name = "toggleEditModeToolStripMenuItem";
            this.toggleEditModeToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.toggleEditModeToolStripMenuItem.Text = "Toggle Edit Mode";
            this.toggleEditModeToolStripMenuItem.Click += new System.EventHandler(this.ToggleEditModeToolStripMenuItem_Click);
            // 
            // toggleAutosaveToolStripMenuItem
            // 
            this.toggleAutosaveToolStripMenuItem.Name = "toggleAutosaveToolStripMenuItem";
            this.toggleAutosaveToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.toggleAutosaveToolStripMenuItem.Text = "Toggle Autosave";
            this.toggleAutosaveToolStripMenuItem.Click += new System.EventHandler(this.ToggleAutosaveToolStripMenuItem_Click);
            // 
            // saveSplitterLocationsToolStripMenuItem
            // 
            this.saveSplitterLocationsToolStripMenuItem.Name = "saveSplitterLocationsToolStripMenuItem";
            this.saveSplitterLocationsToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.saveSplitterLocationsToolStripMenuItem.Text = "Save Splitter Locations";
            this.saveSplitterLocationsToolStripMenuItem.Click += new System.EventHandler(this.SaveSplitterLocationsToolStripMenuItem_Click);
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
            this.aspectToolStripMenuItem.Click += new System.EventHandler(this.AspectToolStripMenuItem_Click);
            // 
            // elementToolStripMenuItem
            // 
            this.elementToolStripMenuItem.Name = "elementToolStripMenuItem";
            this.elementToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.elementToolStripMenuItem.Text = "Element";
            this.elementToolStripMenuItem.Click += new System.EventHandler(this.ElementToolStripMenuItem_Click);
            // 
            // recipeToolStripMenuItem
            // 
            this.recipeToolStripMenuItem.Name = "recipeToolStripMenuItem";
            this.recipeToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.recipeToolStripMenuItem.Text = "Recipe";
            this.recipeToolStripMenuItem.Click += new System.EventHandler(this.RecipeToolStripMenuItem_Click);
            // 
            // deckToolStripMenuItem
            // 
            this.deckToolStripMenuItem.Name = "deckToolStripMenuItem";
            this.deckToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.deckToolStripMenuItem.Text = "Deck";
            this.deckToolStripMenuItem.Click += new System.EventHandler(this.DeckToolStripMenuItem_Click);
            // 
            // legacyToolStripMenuItem
            // 
            this.legacyToolStripMenuItem.Name = "legacyToolStripMenuItem";
            this.legacyToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.legacyToolStripMenuItem.Text = "Legacy";
            this.legacyToolStripMenuItem.Click += new System.EventHandler(this.LegacyToolStripMenuItem_Click);
            // 
            // endingToolStripMenuItem
            // 
            this.endingToolStripMenuItem.Name = "endingToolStripMenuItem";
            this.endingToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.endingToolStripMenuItem.Text = "Ending";
            this.endingToolStripMenuItem.Click += new System.EventHandler(this.EndingToolStripMenuItem_Click);
            // 
            // verbToolStripMenuItem
            // 
            this.verbToolStripMenuItem.Name = "verbToolStripMenuItem";
            this.verbToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.verbToolStripMenuItem.Text = "Verb";
            this.verbToolStripMenuItem.Click += new System.EventHandler(this.VerbToolStripMenuItem_Click);
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
            this.fromClipboardToolStripMenuItem.Click += new System.EventHandler(this.FromClipboardToolStripMenuItem_Click);
            // 
            // aspectToolStripMenuItem1
            // 
            this.aspectToolStripMenuItem1.Name = "aspectToolStripMenuItem1";
            this.aspectToolStripMenuItem1.Size = new System.Drawing.Size(198, 22);
            this.aspectToolStripMenuItem1.Text = "Aspect";
            this.aspectToolStripMenuItem1.Click += new System.EventHandler(this.AspectToolStripMenuItem1_Click);
            // 
            // elementToolStripMenuItem1
            // 
            this.elementToolStripMenuItem1.Name = "elementToolStripMenuItem1";
            this.elementToolStripMenuItem1.Size = new System.Drawing.Size(198, 22);
            this.elementToolStripMenuItem1.Text = "Element";
            this.elementToolStripMenuItem1.Click += new System.EventHandler(this.ElementToolStripMenuItem1_Click);
            // 
            // recipeToolStripMenuItem1
            // 
            this.recipeToolStripMenuItem1.Name = "recipeToolStripMenuItem1";
            this.recipeToolStripMenuItem1.Size = new System.Drawing.Size(198, 22);
            this.recipeToolStripMenuItem1.Text = "Recipe";
            this.recipeToolStripMenuItem1.Click += new System.EventHandler(this.RecipeToolStripMenuItem1_Click);
            // 
            // deckToolStripMenuItem1
            // 
            this.deckToolStripMenuItem1.Name = "deckToolStripMenuItem1";
            this.deckToolStripMenuItem1.Size = new System.Drawing.Size(198, 22);
            this.deckToolStripMenuItem1.Text = "Deck";
            this.deckToolStripMenuItem1.Click += new System.EventHandler(this.DeckToolStripMenuItem1_Click);
            // 
            // legacyToolStripMenuItem1
            // 
            this.legacyToolStripMenuItem1.Name = "legacyToolStripMenuItem1";
            this.legacyToolStripMenuItem1.Size = new System.Drawing.Size(198, 22);
            this.legacyToolStripMenuItem1.Text = "Legacy";
            this.legacyToolStripMenuItem1.Click += new System.EventHandler(this.LegacyToolStripMenuItem1_Click);
            // 
            // endingToolStripMenuItem1
            // 
            this.endingToolStripMenuItem1.Name = "endingToolStripMenuItem1";
            this.endingToolStripMenuItem1.Size = new System.Drawing.Size(198, 22);
            this.endingToolStripMenuItem1.Text = "Ending";
            this.endingToolStripMenuItem1.Click += new System.EventHandler(this.EndingToolStripMenuItem1_Click);
            // 
            // verbToolStripMenuItem1
            // 
            this.verbToolStripMenuItem1.Name = "verbToolStripMenuItem1";
            this.verbToolStripMenuItem1.Size = new System.Drawing.Size(198, 22);
            this.verbToolStripMenuItem1.Text = "Verb";
            this.verbToolStripMenuItem1.Click += new System.EventHandler(this.VerbToolStripMenuItem1_Click);
            // 
            // toolsToolStripButton
            // 
            this.toolsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolsToolStripButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.summonGeneratorToolStripMenuItem,
            this.imageImporterToolStripMenuItem,
            this.jSONCleanerToolStripMenuItem,
            this.culturesToolStripMenuItem,
            this.assetBrowserToolStripMenuItem,
            this.templateManagerToolStripMenuItem});
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
            this.summonGeneratorToolStripMenuItem.Click += new System.EventHandler(this.SummonGeneratorToolStripMenuItem_Click);
            // 
            // imageImporterToolStripMenuItem
            // 
            this.imageImporterToolStripMenuItem.Name = "imageImporterToolStripMenuItem";
            this.imageImporterToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.imageImporterToolStripMenuItem.Text = "Image Importer";
            this.imageImporterToolStripMenuItem.Click += new System.EventHandler(this.ImageImporterToolStripMenuItem_Click);
            // 
            // jSONCleanerToolStripMenuItem
            // 
            this.jSONCleanerToolStripMenuItem.Name = "jSONCleanerToolStripMenuItem";
            this.jSONCleanerToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.jSONCleanerToolStripMenuItem.Text = "JSON Cleaner";
            this.jSONCleanerToolStripMenuItem.Click += new System.EventHandler(this.JSONCleanerToolStripMenuItem_Click);
            // 
            // culturesToolStripMenuItem
            // 
            this.culturesToolStripMenuItem.Name = "culturesToolStripMenuItem";
            this.culturesToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.culturesToolStripMenuItem.Text = "Cultures";
            this.culturesToolStripMenuItem.Click += new System.EventHandler(this.CulturesToolStripMenuItem_Click);
            // 
            // assetBrowserToolStripMenuItem
            // 
            this.assetBrowserToolStripMenuItem.Name = "assetBrowserToolStripMenuItem";
            this.assetBrowserToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.assetBrowserToolStripMenuItem.Text = "Asset Browser";
            this.assetBrowserToolStripMenuItem.Click += new System.EventHandler(this.AssetBrowserToolStripMenuItem_Click);
            // 
            // templateManagerToolStripMenuItem
            // 
            this.templateManagerToolStripMenuItem.Name = "templateManagerToolStripMenuItem";
            this.templateManagerToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.templateManagerToolStripMenuItem.Text = "Template Manager";
            this.templateManagerToolStripMenuItem.Click += new System.EventHandler(this.TemplateManagerToolStripMenuItem_Click);
            // 
            // settingsToolStripButton
            // 
            this.settingsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.settingsToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("settingsToolStripButton.Image")));
            this.settingsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsToolStripButton.Name = "settingsToolStripButton";
            this.settingsToolStripButton.Size = new System.Drawing.Size(53, 22);
            this.settingsToolStripButton.Text = "Settings";
            this.settingsToolStripButton.Click += new System.EventHandler(this.SettingsToolStripButton_Click);
            // 
            // aboutToolStripButton
            // 
            this.aboutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.aboutToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("aboutToolStripButton.Image")));
            this.aboutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.aboutToolStripButton.Name = "aboutToolStripButton";
            this.aboutToolStripButton.Size = new System.Drawing.Size(44, 22);
            this.aboutToolStripButton.Text = "About";
            this.aboutToolStripButton.Click += new System.EventHandler(this.AboutToolStripButton_Click);
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
            this.ModViewerTabs.AllowDrop = true;
            this.ModViewerTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ModViewerTabs.Location = new System.Drawing.Point(0, 28);
            this.ModViewerTabs.Name = "ModViewerTabs";
            this.ModViewerTabs.SelectedIndex = 0;
            this.ModViewerTabs.Size = new System.Drawing.Size(935, 316);
            this.ModViewerTabs.TabIndex = 27;
            this.ModViewerTabs.SelectedIndexChanged += new System.EventHandler(this.ModViewerTabs_SelectedIndexChanged);
            this.ModViewerTabs.DragDrop += new System.Windows.Forms.DragEventHandler(this.ModViewerTabs_DragDrop);
            this.ModViewerTabs.DragEnter += new System.Windows.Forms.DragEventHandler(this.ModViewerTabs_DragEnter);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "JSON files|*.json";
            // 
            // openInExplorerToolStripMenuItem
            // 
            this.openInExplorerToolStripMenuItem.Name = "openInExplorerToolStripMenuItem";
            this.openInExplorerToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.openInExplorerToolStripMenuItem.Text = "Open in Explorer";
            this.openInExplorerToolStripMenuItem.Click += new System.EventHandler(this.OpenInExplorerToolStripMenuItem_Click);
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
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
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
        private System.Windows.Forms.ToolStripMenuItem jSONCleanerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSplitterLocationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem culturesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem assetBrowserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem templateManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton aboutToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem openInExplorerToolStripMenuItem;
    }
}

