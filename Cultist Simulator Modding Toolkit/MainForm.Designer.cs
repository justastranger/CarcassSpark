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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.recipesLabel = new System.Windows.Forms.Label();
            this.listBox2 = new System.Windows.Forms.ListBox();
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
            this.toolStrip1.Size = new System.Drawing.Size(677, 25);
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
            this.aspectListBox.Size = new System.Drawing.Size(150, 186);
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
            this.aspectsLabel.Location = new System.Drawing.Point(40, 25);
            this.aspectsLabel.Name = "aspectsLabel";
            this.aspectsLabel.Size = new System.Drawing.Size(79, 13);
            this.aspectsLabel.TabIndex = 2;
            this.aspectsLabel.Text = "Vanilla Aspects";
            // 
            // elementsLabel
            // 
            this.elementsLabel.AutoSize = true;
            this.elementsLabel.Location = new System.Drawing.Point(199, 25);
            this.elementsLabel.Name = "elementsLabel";
            this.elementsLabel.Size = new System.Drawing.Size(84, 13);
            this.elementsLabel.TabIndex = 3;
            this.elementsLabel.Text = "Vanilla Elements";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(168, 41);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(150, 186);
            this.listBox1.Sorted = true;
            this.listBox1.TabIndex = 4;
            // 
            // recipesLabel
            // 
            this.recipesLabel.AutoSize = true;
            this.recipesLabel.Location = new System.Drawing.Point(357, 25);
            this.recipesLabel.Name = "recipesLabel";
            this.recipesLabel.Size = new System.Drawing.Size(80, 13);
            this.recipesLabel.TabIndex = 5;
            this.recipesLabel.Text = "Vanilla Recipes";
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(324, 41);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(150, 186);
            this.listBox2.Sorted = true;
            this.listBox2.TabIndex = 6;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 369);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.recipesLabel);
            this.Controls.Add(this.listBox1);
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
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label recipesLabel;
        private System.Windows.Forms.ListBox listBox2;
    }
}

