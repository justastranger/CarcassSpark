﻿namespace CarcassSpark
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.loadPreviousModsCheckBox = new System.Windows.Forms.CheckBox();
            this.previousModsTextBox = new System.Windows.Forms.TextBox();
            this.previousModLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.loadAllFlowchartNodesCheckBox = new System.Windows.Forms.CheckBox();
            this.portableCheckBox = new System.Windows.Forms.CheckBox();
            this.GamePathTextBox = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // loadPreviousModsCheckBox
            // 
            this.loadPreviousModsCheckBox.AutoSize = true;
            this.loadPreviousModsCheckBox.Location = new System.Drawing.Point(12, 35);
            this.loadPreviousModsCheckBox.Name = "loadPreviousModsCheckBox";
            this.loadPreviousModsCheckBox.Size = new System.Drawing.Size(218, 17);
            this.loadPreviousModsCheckBox.TabIndex = 1;
            this.loadPreviousModsCheckBox.Text = "Load Remembered Mods When Opened";
            this.ToolTip.SetToolTip(this.loadPreviousModsCheckBox, "When true, all mods that were open when you closed Carcass Spark will be loaded w" +
        "hen Carcass Spark opens.\r\nBelow are paths to the mods\' directories.");
            this.loadPreviousModsCheckBox.UseVisualStyleBackColor = true;
            this.loadPreviousModsCheckBox.CheckedChanged += new System.EventHandler(this.LoadPreviousModsCheckBox_CheckedChanged);
            // 
            // previousModsTextBox
            // 
            this.previousModsTextBox.AcceptsReturn = true;
            this.previousModsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.previousModsTextBox.Location = new System.Drawing.Point(12, 71);
            this.previousModsTextBox.Multiline = true;
            this.previousModsTextBox.Name = "previousModsTextBox";
            this.previousModsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.previousModsTextBox.Size = new System.Drawing.Size(370, 50);
            this.previousModsTextBox.TabIndex = 2;
            this.ToolTip.SetToolTip(this.previousModsTextBox, resources.GetString("previousModsTextBox.ToolTip"));
            // 
            // previousModLabel
            // 
            this.previousModLabel.AutoSize = true;
            this.previousModLabel.Location = new System.Drawing.Point(12, 55);
            this.previousModLabel.Name = "previousModLabel";
            this.previousModLabel.Size = new System.Drawing.Size(123, 13);
            this.previousModLabel.TabIndex = 3;
            this.previousModLabel.Text = "Previously Loaded Mods";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(12, 189);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(307, 189);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // loadAllFlowchartNodesCheckBox
            // 
            this.loadAllFlowchartNodesCheckBox.AutoSize = true;
            this.loadAllFlowchartNodesCheckBox.Location = new System.Drawing.Point(12, 12);
            this.loadAllFlowchartNodesCheckBox.Name = "loadAllFlowchartNodesCheckBox";
            this.loadAllFlowchartNodesCheckBox.Size = new System.Drawing.Size(189, 17);
            this.loadAllFlowchartNodesCheckBox.TabIndex = 7;
            this.loadAllFlowchartNodesCheckBox.Text = "Load All Flowchart Nodes At Once";
            this.ToolTip.SetToolTip(this.loadAllFlowchartNodesCheckBox, resources.GetString("loadAllFlowchartNodesCheckBox.ToolTip"));
            this.loadAllFlowchartNodesCheckBox.UseVisualStyleBackColor = true;
            this.loadAllFlowchartNodesCheckBox.CheckedChanged += new System.EventHandler(this.LoadAllFlowchartNodesCheckBox_CheckedChanged);
            // 
            // portableCheckBox
            // 
            this.portableCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.portableCheckBox.AutoSize = true;
            this.portableCheckBox.Location = new System.Drawing.Point(12, 127);
            this.portableCheckBox.Name = "portableCheckBox";
            this.portableCheckBox.Size = new System.Drawing.Size(95, 17);
            this.portableCheckBox.TabIndex = 8;
            this.portableCheckBox.Text = "Portable Mode";
            this.ToolTip.SetToolTip(this.portableCheckBox, "When true, Carcass Spark will look in the below folder for Cultist Simulator\'s JS" +
        "ON files.");
            this.portableCheckBox.UseVisualStyleBackColor = true;
            this.portableCheckBox.CheckedChanged += new System.EventHandler(this.PortableCheckBox_CheckedChanged);
            // 
            // GamePathTextBox
            // 
            this.GamePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GamePathTextBox.Location = new System.Drawing.Point(12, 163);
            this.GamePathTextBox.Name = "GamePathTextBox";
            this.GamePathTextBox.ReadOnly = true;
            this.GamePathTextBox.Size = new System.Drawing.Size(370, 20);
            this.GamePathTextBox.TabIndex = 9;
            this.ToolTip.SetToolTip(this.GamePathTextBox, "This is the path to your installation of Cultist Simulator.\r\nIt must point to the" +
        " folder containing cultistsimulator.exe");
            this.GamePathTextBox.DoubleClick += new System.EventHandler(this.GamePathTextBox_DoubleClick);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.Recent;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "Assembly-CSharp.dll";
            this.openFileDialog.Filter = "Assembly-CSharp.dll|Assembly-CSharp.dll";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Cultist Simulator Game Path";
            // 
            // Settings
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(394, 224);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GamePathTextBox);
            this.Controls.Add(this.portableCheckBox);
            this.Controls.Add(this.loadAllFlowchartNodesCheckBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.previousModLabel);
            this.Controls.Add(this.previousModsTextBox);
            this.Controls.Add(this.loadPreviousModsCheckBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(250, 263);
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox loadPreviousModsCheckBox;
        private System.Windows.Forms.TextBox previousModsTextBox;
        private System.Windows.Forms.Label previousModLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.CheckBox loadAllFlowchartNodesCheckBox;
        private System.Windows.Forms.CheckBox portableCheckBox;
        private System.Windows.Forms.TextBox GamePathTextBox;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip ToolTip;
    }
}