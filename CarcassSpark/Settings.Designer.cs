namespace CarcassSpark
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.openWithVanillaCheckBox = new System.Windows.Forms.CheckBox();
            this.loadPreviousModsCheckBox = new System.Windows.Forms.CheckBox();
            this.previousModsTextBox = new System.Windows.Forms.TextBox();
            this.previousModLabel = new System.Windows.Forms.Label();
            this.saveCleanedVanillaContentCheckBox = new System.Windows.Forms.CheckBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.loadAllFlowchartNodesCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // openWithVanillaCheckBox
            // 
            this.openWithVanillaCheckBox.AutoSize = true;
            this.openWithVanillaCheckBox.Location = new System.Drawing.Point(12, 12);
            this.openWithVanillaCheckBox.Name = "openWithVanillaCheckBox";
            this.openWithVanillaCheckBox.Size = new System.Drawing.Size(197, 17);
            this.openWithVanillaCheckBox.TabIndex = 0;
            this.openWithVanillaCheckBox.Text = "Load Vanilla Content When Opened";
            this.openWithVanillaCheckBox.UseVisualStyleBackColor = true;
            this.openWithVanillaCheckBox.CheckedChanged += new System.EventHandler(this.OpenWithVanillaCheckBox_CheckedChanged);
            // 
            // loadPreviousModsCheckBox
            // 
            this.loadPreviousModsCheckBox.AutoSize = true;
            this.loadPreviousModsCheckBox.Location = new System.Drawing.Point(12, 81);
            this.loadPreviousModsCheckBox.Name = "loadPreviousModsCheckBox";
            this.loadPreviousModsCheckBox.Size = new System.Drawing.Size(218, 17);
            this.loadPreviousModsCheckBox.TabIndex = 1;
            this.loadPreviousModsCheckBox.Text = "Load Remembered Mods When Opened";
            this.loadPreviousModsCheckBox.UseVisualStyleBackColor = true;
            this.loadPreviousModsCheckBox.CheckedChanged += new System.EventHandler(this.LoadPreviousModsCheckBox_CheckedChanged);
            // 
            // previousModsTextBox
            // 
            this.previousModsTextBox.AcceptsReturn = true;
            this.previousModsTextBox.Location = new System.Drawing.Point(12, 117);
            this.previousModsTextBox.Multiline = true;
            this.previousModsTextBox.Name = "previousModsTextBox";
            this.previousModsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.previousModsTextBox.Size = new System.Drawing.Size(370, 225);
            this.previousModsTextBox.TabIndex = 2;
            this.previousModsTextBox.TextChanged += new System.EventHandler(this.PreviousModsTextBox_TextChanged);
            // 
            // previousModLabel
            // 
            this.previousModLabel.AutoSize = true;
            this.previousModLabel.Location = new System.Drawing.Point(12, 101);
            this.previousModLabel.Name = "previousModLabel";
            this.previousModLabel.Size = new System.Drawing.Size(123, 13);
            this.previousModLabel.TabIndex = 3;
            this.previousModLabel.Text = "Previously Loaded Mods";
            // 
            // saveCleanedVanillaContentCheckBox
            // 
            this.saveCleanedVanillaContentCheckBox.AutoSize = true;
            this.saveCleanedVanillaContentCheckBox.Location = new System.Drawing.Point(12, 35);
            this.saveCleanedVanillaContentCheckBox.Name = "saveCleanedVanillaContentCheckBox";
            this.saveCleanedVanillaContentCheckBox.Size = new System.Drawing.Size(342, 17);
            this.saveCleanedVanillaContentCheckBox.TabIndex = 4;
            this.saveCleanedVanillaContentCheckBox.Text = "Save a cleaned copy of the vanilla content files when loading them";
            this.saveCleanedVanillaContentCheckBox.UseVisualStyleBackColor = true;
            this.saveCleanedVanillaContentCheckBox.CheckedChanged += new System.EventHandler(this.SaveCleanedVanillaContentCheckBox_CheckedChanged);
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(12, 348);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(307, 348);
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
            this.loadAllFlowchartNodesCheckBox.Location = new System.Drawing.Point(12, 58);
            this.loadAllFlowchartNodesCheckBox.Name = "loadAllFlowchartNodesCheckBox";
            this.loadAllFlowchartNodesCheckBox.Size = new System.Drawing.Size(189, 17);
            this.loadAllFlowchartNodesCheckBox.TabIndex = 7;
            this.loadAllFlowchartNodesCheckBox.Text = "Load All Flowchart Nodes At Once";
            this.loadAllFlowchartNodesCheckBox.UseVisualStyleBackColor = true;
            this.loadAllFlowchartNodesCheckBox.CheckedChanged += new System.EventHandler(this.LoadAllFlowchartNodesCheckBox_CheckedChanged);
            // 
            // Settings
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(394, 383);
            this.Controls.Add(this.loadAllFlowchartNodesCheckBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.saveCleanedVanillaContentCheckBox);
            this.Controls.Add(this.previousModLabel);
            this.Controls.Add(this.previousModsTextBox);
            this.Controls.Add(this.loadPreviousModsCheckBox);
            this.Controls.Add(this.openWithVanillaCheckBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Settings";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox openWithVanillaCheckBox;
        private System.Windows.Forms.CheckBox loadPreviousModsCheckBox;
        private System.Windows.Forms.TextBox previousModsTextBox;
        private System.Windows.Forms.Label previousModLabel;
        private System.Windows.Forms.CheckBox saveCleanedVanillaContentCheckBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.CheckBox loadAllFlowchartNodesCheckBox;
    }
}