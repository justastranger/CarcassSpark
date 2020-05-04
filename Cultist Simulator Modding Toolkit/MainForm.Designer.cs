namespace Cultist_Simulator_Modding_Toolkit
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.newModToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectModToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadContentButton = new System.Windows.Forms.ToolStripButton();
            this.aspectListBox = new System.Windows.Forms.ListBox();
            this.aspectImageList = new System.Windows.Forms.ImageList(this.components);
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
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
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.loadContentButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(930, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newModToolStripMenuItem,
            this.selectModToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(38, 22);
            this.toolStripDropDownButton1.Text = "File";
            // 
            // newModToolStripMenuItem
            // 
            this.newModToolStripMenuItem.Name = "newModToolStripMenuItem";
            this.newModToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newModToolStripMenuItem.Text = "New Mod";
            this.newModToolStripMenuItem.Click += new System.EventHandler(this.newModToolStripMenuItem_Click);
            // 
            // selectModToolStripMenuItem
            // 
            this.selectModToolStripMenuItem.Name = "selectModToolStripMenuItem";
            this.selectModToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.selectModToolStripMenuItem.Text = "Select Mod";
            this.selectModToolStripMenuItem.Click += new System.EventHandler(this.selectModToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // loadContentButton
            // 
            this.loadContentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.loadContentButton.Image = ((System.Drawing.Image)(resources.GetObject("loadContentButton.Image")));
            this.loadContentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadContentButton.Name = "loadContentButton";
            this.loadContentButton.Size = new System.Drawing.Size(93, 22);
            this.loadContentButton.Text = "Reload Content";
            this.loadContentButton.Click += new System.EventHandler(this.loadContentButton_Click);
            // 
            // aspectListBox
            // 
            this.aspectListBox.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.aspectListBox.Items.AddRange(new object[] {
            "exampleAspect1",
            "exampleAspect2",
            "exampleAspect3"});
            this.aspectListBox.Location = new System.Drawing.Point(12, 41);
            this.aspectListBox.Name = "aspectListBox";
            this.aspectListBox.ScrollAlwaysVisible = true;
            this.aspectListBox.Size = new System.Drawing.Size(125, 303);
            this.aspectListBox.Sorted = true;
            this.aspectListBox.TabIndex = 1;
            // 
            // aspectImageList
            // 
            this.aspectImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.aspectImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.aspectImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // aspectsLabel
            // 
            this.aspectsLabel.AutoSize = true;
            this.aspectsLabel.Location = new System.Drawing.Point(31, 25);
            this.aspectsLabel.Name = "aspectsLabel";
            this.aspectsLabel.Size = new System.Drawing.Size(79, 13);
            this.aspectsLabel.TabIndex = 2;
            this.aspectsLabel.Text = "Vanilla Aspects";
            // 
            // elementsLabel
            // 
            this.elementsLabel.AutoSize = true;
            this.elementsLabel.Location = new System.Drawing.Point(162, 25);
            this.elementsLabel.Name = "elementsLabel";
            this.elementsLabel.Size = new System.Drawing.Size(84, 13);
            this.elementsLabel.TabIndex = 3;
            this.elementsLabel.Text = "Vanilla Elements";
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
            // 
            // recipesLabel
            // 
            this.recipesLabel.AutoSize = true;
            this.recipesLabel.Location = new System.Drawing.Point(297, 25);
            this.recipesLabel.Name = "recipesLabel";
            this.recipesLabel.Size = new System.Drawing.Size(80, 13);
            this.recipesLabel.TabIndex = 5;
            this.recipesLabel.Text = "Vanilla Recipes";
            // 
            // recipesListBox
            // 
            this.recipesListBox.FormattingEnabled = true;
            this.recipesListBox.Location = new System.Drawing.Point(274, 41);
            this.recipesListBox.Name = "recipesListBox";
            this.recipesListBox.Size = new System.Drawing.Size(125, 303);
            this.recipesListBox.Sorted = true;
            this.recipesListBox.TabIndex = 6;
            // 
            // decksLabel
            // 
            this.decksLabel.AutoSize = true;
            this.decksLabel.Location = new System.Drawing.Point(427, 25);
            this.decksLabel.Name = "decksLabel";
            this.decksLabel.Size = new System.Drawing.Size(72, 13);
            this.decksLabel.TabIndex = 7;
            this.decksLabel.Text = "Vanilla Decks";
            // 
            // decksListBox
            // 
            this.decksListBox.FormattingEnabled = true;
            this.decksListBox.Location = new System.Drawing.Point(405, 41);
            this.decksListBox.Name = "decksListBox";
            this.decksListBox.Size = new System.Drawing.Size(125, 303);
            this.decksListBox.TabIndex = 8;
            // 
            // legaciesLabel
            // 
            this.legaciesLabel.AutoSize = true;
            this.legaciesLabel.Location = new System.Drawing.Point(554, 25);
            this.legaciesLabel.Name = "legaciesLabel";
            this.legaciesLabel.Size = new System.Drawing.Size(84, 13);
            this.legaciesLabel.TabIndex = 9;
            this.legaciesLabel.Text = "Vanilla Legacies";
            // 
            // legaciesListBox
            // 
            this.legaciesListBox.FormattingEnabled = true;
            this.legaciesListBox.Location = new System.Drawing.Point(536, 41);
            this.legaciesListBox.Name = "legaciesListBox";
            this.legaciesListBox.Size = new System.Drawing.Size(125, 303);
            this.legaciesListBox.TabIndex = 10;
            // 
            // endingsListBox
            // 
            this.endingsListBox.FormattingEnabled = true;
            this.endingsListBox.Location = new System.Drawing.Point(667, 41);
            this.endingsListBox.Name = "endingsListBox";
            this.endingsListBox.Size = new System.Drawing.Size(125, 303);
            this.endingsListBox.TabIndex = 11;
            // 
            // endingsLabel
            // 
            this.endingsLabel.AutoSize = true;
            this.endingsLabel.Location = new System.Drawing.Point(686, 25);
            this.endingsLabel.Name = "endingsLabel";
            this.endingsLabel.Size = new System.Drawing.Size(79, 13);
            this.endingsLabel.TabIndex = 12;
            this.endingsLabel.Text = "Vanilla Endings";
            // 
            // verbsListBox
            // 
            this.verbsListBox.FormattingEnabled = true;
            this.verbsListBox.Location = new System.Drawing.Point(798, 41);
            this.verbsListBox.Name = "verbsListBox";
            this.verbsListBox.Size = new System.Drawing.Size(120, 303);
            this.verbsListBox.TabIndex = 13;
            // 
            // verbsLabel
            // 
            this.verbsLabel.AutoSize = true;
            this.verbsLabel.Location = new System.Drawing.Point(825, 25);
            this.verbsLabel.Name = "verbsLabel";
            this.verbsLabel.Size = new System.Drawing.Size(68, 13);
            this.verbsLabel.TabIndex = 14;
            this.verbsLabel.Text = "Vanilla Verbs";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 369);
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
            this.Controls.Add(this.aspectListBox);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MainForm";
            this.Text = "Cultist Simulator Modding Toolkit";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton loadContentButton;
        private System.Windows.Forms.ListBox aspectListBox;
        private System.Windows.Forms.ImageList aspectImageList;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem newModToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectModToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
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
    }
}

