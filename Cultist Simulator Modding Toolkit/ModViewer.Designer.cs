namespace Cultist_Simulator_Modding_Toolkit
{
    partial class ModViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModViewer));
            this.aspectsListBox = new System.Windows.Forms.ListBox();
            this.aspectsLabel = new System.Windows.Forms.Label();
            this.elementsLabel = new System.Windows.Forms.Label();
            this.elementsListBox = new System.Windows.Forms.ListBox();
            this.recipesLabel = new System.Windows.Forms.Label();
            this.recipesListBox = new System.Windows.Forms.ListBox();
            this.decksLabel = new System.Windows.Forms.Label();
            this.decksListBox = new System.Windows.Forms.ListBox();
            this.legaciesLabel = new System.Windows.Forms.Label();
            this.legaciesListBox = new System.Windows.Forms.ListBox();
            this.endingsListBox = new System.Windows.Forms.ListBox();
            this.endingsLabel = new System.Windows.Forms.Label();
            this.verbsListBox = new System.Windows.Forms.ListBox();
            this.verbsLabel = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.fileToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aspectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.elementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recipeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.legacyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.endingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verbToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editManifestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveModToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // aspectsListBox
            // 
            this.aspectsListBox.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.aspectsListBox.Location = new System.Drawing.Point(12, 41);
            this.aspectsListBox.Name = "aspectsListBox";
            this.aspectsListBox.ScrollAlwaysVisible = true;
            this.aspectsListBox.Size = new System.Drawing.Size(125, 303);
            this.aspectsListBox.Sorted = true;
            this.aspectsListBox.TabIndex = 1;
            this.aspectsListBox.DoubleClick += new System.EventHandler(this.aspectListBox_DoubleClick);
            // 
            // aspectsLabel
            // 
            this.aspectsLabel.AutoSize = true;
            this.aspectsLabel.Location = new System.Drawing.Point(42, 25);
            this.aspectsLabel.Name = "aspectsLabel";
            this.aspectsLabel.Size = new System.Drawing.Size(45, 13);
            this.aspectsLabel.TabIndex = 2;
            this.aspectsLabel.Text = "Aspects";
            // 
            // elementsLabel
            // 
            this.elementsLabel.AutoSize = true;
            this.elementsLabel.Location = new System.Drawing.Point(175, 25);
            this.elementsLabel.Name = "elementsLabel";
            this.elementsLabel.Size = new System.Drawing.Size(50, 13);
            this.elementsLabel.TabIndex = 3;
            this.elementsLabel.Text = "Elements";
            // 
            // elementsListBox
            // 
            this.elementsListBox.FormattingEnabled = true;
            this.elementsListBox.Location = new System.Drawing.Point(143, 41);
            this.elementsListBox.Name = "elementsListBox";
            this.elementsListBox.ScrollAlwaysVisible = true;
            this.elementsListBox.Size = new System.Drawing.Size(125, 303);
            this.elementsListBox.Sorted = true;
            this.elementsListBox.TabIndex = 4;
            this.elementsListBox.DoubleClick += new System.EventHandler(this.elementsListBox_DoubleClick);
            // 
            // recipesLabel
            // 
            this.recipesLabel.AutoSize = true;
            this.recipesLabel.Location = new System.Drawing.Point(303, 25);
            this.recipesLabel.Name = "recipesLabel";
            this.recipesLabel.Size = new System.Drawing.Size(46, 13);
            this.recipesLabel.TabIndex = 5;
            this.recipesLabel.Text = "Recipes";
            // 
            // recipesListBox
            // 
            this.recipesListBox.FormattingEnabled = true;
            this.recipesListBox.Location = new System.Drawing.Point(274, 41);
            this.recipesListBox.Name = "recipesListBox";
            this.recipesListBox.ScrollAlwaysVisible = true;
            this.recipesListBox.Size = new System.Drawing.Size(125, 303);
            this.recipesListBox.Sorted = true;
            this.recipesListBox.TabIndex = 6;
            this.recipesListBox.DoubleClick += new System.EventHandler(this.recipesListBox_DoubleClick);
            // 
            // decksLabel
            // 
            this.decksLabel.AutoSize = true;
            this.decksLabel.Location = new System.Drawing.Point(439, 25);
            this.decksLabel.Name = "decksLabel";
            this.decksLabel.Size = new System.Drawing.Size(38, 13);
            this.decksLabel.TabIndex = 7;
            this.decksLabel.Text = "Decks";
            // 
            // decksListBox
            // 
            this.decksListBox.FormattingEnabled = true;
            this.decksListBox.Location = new System.Drawing.Point(405, 41);
            this.decksListBox.Name = "decksListBox";
            this.decksListBox.ScrollAlwaysVisible = true;
            this.decksListBox.Size = new System.Drawing.Size(125, 303);
            this.decksListBox.Sorted = true;
            this.decksListBox.TabIndex = 8;
            this.decksListBox.DoubleClick += new System.EventHandler(this.decksListBox_DoubleClick);
            // 
            // legaciesLabel
            // 
            this.legaciesLabel.AutoSize = true;
            this.legaciesLabel.Location = new System.Drawing.Point(566, 25);
            this.legaciesLabel.Name = "legaciesLabel";
            this.legaciesLabel.Size = new System.Drawing.Size(50, 13);
            this.legaciesLabel.TabIndex = 9;
            this.legaciesLabel.Text = "Legacies";
            // 
            // legaciesListBox
            // 
            this.legaciesListBox.FormattingEnabled = true;
            this.legaciesListBox.Location = new System.Drawing.Point(536, 41);
            this.legaciesListBox.Name = "legaciesListBox";
            this.legaciesListBox.ScrollAlwaysVisible = true;
            this.legaciesListBox.Size = new System.Drawing.Size(125, 303);
            this.legaciesListBox.Sorted = true;
            this.legaciesListBox.TabIndex = 10;
            this.legaciesListBox.DoubleClick += new System.EventHandler(this.legaciesListBox_DoubleClick);
            // 
            // endingsListBox
            // 
            this.endingsListBox.FormattingEnabled = true;
            this.endingsListBox.Location = new System.Drawing.Point(667, 41);
            this.endingsListBox.Name = "endingsListBox";
            this.endingsListBox.ScrollAlwaysVisible = true;
            this.endingsListBox.Size = new System.Drawing.Size(125, 303);
            this.endingsListBox.Sorted = true;
            this.endingsListBox.TabIndex = 11;
            this.endingsListBox.DoubleClick += new System.EventHandler(this.endingsListBox_DoubleClick);
            // 
            // endingsLabel
            // 
            this.endingsLabel.AutoSize = true;
            this.endingsLabel.Location = new System.Drawing.Point(701, 25);
            this.endingsLabel.Name = "endingsLabel";
            this.endingsLabel.Size = new System.Drawing.Size(45, 13);
            this.endingsLabel.TabIndex = 12;
            this.endingsLabel.Text = "Endings";
            // 
            // verbsListBox
            // 
            this.verbsListBox.FormattingEnabled = true;
            this.verbsListBox.Location = new System.Drawing.Point(798, 41);
            this.verbsListBox.Name = "verbsListBox";
            this.verbsListBox.ScrollAlwaysVisible = true;
            this.verbsListBox.Size = new System.Drawing.Size(120, 303);
            this.verbsListBox.Sorted = true;
            this.verbsListBox.TabIndex = 13;
            this.verbsListBox.DoubleClick += new System.EventHandler(this.verbsListBox_DoubleClick);
            // 
            // verbsLabel
            // 
            this.verbsLabel.AutoSize = true;
            this.verbsLabel.Location = new System.Drawing.Point(842, 25);
            this.verbsLabel.Name = "verbsLabel";
            this.verbsLabel.Size = new System.Drawing.Size(34, 13);
            this.verbsLabel.TabIndex = 14;
            this.verbsLabel.Text = "Verbs";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripDropDownButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(930, 25);
            this.toolStrip1.TabIndex = 15;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // fileToolStripDropDownButton
            // 
            this.fileToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fileToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.editManifestToolStripMenuItem,
            this.saveModToolStripMenuItem});
            this.fileToolStripDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("fileToolStripDropDownButton.Image")));
            this.fileToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fileToolStripDropDownButton.Name = "fileToolStripDropDownButton";
            this.fileToolStripDropDownButton.Size = new System.Drawing.Size(38, 22);
            this.fileToolStripDropDownButton.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aspectToolStripMenuItem,
            this.elementToolStripMenuItem,
            this.recipeToolStripMenuItem,
            this.deckToolStripMenuItem,
            this.legacyToolStripMenuItem,
            this.endingToolStripMenuItem,
            this.verbToolStripMenuItem});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newToolStripMenuItem.Text = "New...";
            // 
            // aspectToolStripMenuItem
            // 
            this.aspectToolStripMenuItem.Name = "aspectToolStripMenuItem";
            this.aspectToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aspectToolStripMenuItem.Text = "Aspect";
            this.aspectToolStripMenuItem.Click += new System.EventHandler(this.aspectToolStripMenuItem_Click);
            // 
            // elementToolStripMenuItem
            // 
            this.elementToolStripMenuItem.Name = "elementToolStripMenuItem";
            this.elementToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.elementToolStripMenuItem.Text = "Element";
            this.elementToolStripMenuItem.Click += new System.EventHandler(this.elementToolStripMenuItem_Click);
            // 
            // recipeToolStripMenuItem
            // 
            this.recipeToolStripMenuItem.Name = "recipeToolStripMenuItem";
            this.recipeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.recipeToolStripMenuItem.Text = "Recipe";
            // 
            // deckToolStripMenuItem
            // 
            this.deckToolStripMenuItem.Name = "deckToolStripMenuItem";
            this.deckToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deckToolStripMenuItem.Text = "Deck";
            // 
            // legacyToolStripMenuItem
            // 
            this.legacyToolStripMenuItem.Name = "legacyToolStripMenuItem";
            this.legacyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.legacyToolStripMenuItem.Text = "Legacy";
            // 
            // endingToolStripMenuItem
            // 
            this.endingToolStripMenuItem.Name = "endingToolStripMenuItem";
            this.endingToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.endingToolStripMenuItem.Text = "Ending";
            // 
            // verbToolStripMenuItem
            // 
            this.verbToolStripMenuItem.Name = "verbToolStripMenuItem";
            this.verbToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.verbToolStripMenuItem.Text = "Verb";
            // 
            // editManifestToolStripMenuItem
            // 
            this.editManifestToolStripMenuItem.Name = "editManifestToolStripMenuItem";
            this.editManifestToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.editManifestToolStripMenuItem.Text = "Edit Manifest";
            this.editManifestToolStripMenuItem.Click += new System.EventHandler(this.editManifestToolStripMenuItem_Click);
            // 
            // saveModToolStripMenuItem
            // 
            this.saveModToolStripMenuItem.Name = "saveModToolStripMenuItem";
            this.saveModToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveModToolStripMenuItem.Text = "Save Mod";
            this.saveModToolStripMenuItem.Click += new System.EventHandler(this.saveModToolStripMenuItem_Click);
            // 
            // ModViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 369);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.verbsLabel);
            this.Controls.Add(this.verbsListBox);
            this.Controls.Add(this.endingsLabel);
            this.Controls.Add(this.endingsListBox);
            this.Controls.Add(this.legaciesListBox);
            this.Controls.Add(this.legaciesLabel);
            this.Controls.Add(this.decksListBox);
            this.Controls.Add(this.decksLabel);
            this.Controls.Add(this.recipesListBox);
            this.Controls.Add(this.recipesLabel);
            this.Controls.Add(this.elementsListBox);
            this.Controls.Add(this.elementsLabel);
            this.Controls.Add(this.aspectsLabel);
            this.Controls.Add(this.aspectsListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ModViewer";
            this.Text = "Cultist Simulator Modding Toolkit";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox aspectsListBox;
        private System.Windows.Forms.Label aspectsLabel;
        private System.Windows.Forms.Label elementsLabel;
        private System.Windows.Forms.ListBox elementsListBox;
        private System.Windows.Forms.Label recipesLabel;
        private System.Windows.Forms.ListBox recipesListBox;
        private System.Windows.Forms.Label decksLabel;
        private System.Windows.Forms.ListBox decksListBox;
        private System.Windows.Forms.Label legaciesLabel;
        private System.Windows.Forms.ListBox legaciesListBox;
        private System.Windows.Forms.ListBox endingsListBox;
        private System.Windows.Forms.Label endingsLabel;
        private System.Windows.Forms.ListBox verbsListBox;
        private System.Windows.Forms.Label verbsLabel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton fileToolStripDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aspectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem elementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recipeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deckToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem legacyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem endingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verbToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editManifestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveModToolStripMenuItem;
    }
}

