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
            this.rememberPreviousModCheckBox = new System.Windows.Forms.CheckBox();
            this.previousModTextBox = new System.Windows.Forms.TextBox();
            this.previousModLabel = new System.Windows.Forms.Label();
            this.saveCleanedVanillaContentCheckBox = new System.Windows.Forms.CheckBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
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
            this.openWithVanillaCheckBox.CheckedChanged += new System.EventHandler(this.openWithVanillaCheckBox_CheckedChanged);
            // 
            // rememberPreviousModCheckBox
            // 
            this.rememberPreviousModCheckBox.AutoSize = true;
            this.rememberPreviousModCheckBox.Location = new System.Drawing.Point(12, 58);
            this.rememberPreviousModCheckBox.Name = "rememberPreviousModCheckBox";
            this.rememberPreviousModCheckBox.Size = new System.Drawing.Size(314, 17);
            this.rememberPreviousModCheckBox.TabIndex = 1;
            this.rememberPreviousModCheckBox.Text = "Remember and Load Previously Opened Mod When Opened";
            this.rememberPreviousModCheckBox.UseVisualStyleBackColor = true;
            this.rememberPreviousModCheckBox.CheckedChanged += new System.EventHandler(this.rememberPreviousModCheckBox_CheckedChanged);
            // 
            // previousModTextBox
            // 
            this.previousModTextBox.Location = new System.Drawing.Point(12, 94);
            this.previousModTextBox.Name = "previousModTextBox";
            this.previousModTextBox.ReadOnly = true;
            this.previousModTextBox.Size = new System.Drawing.Size(370, 20);
            this.previousModTextBox.TabIndex = 2;
            // 
            // previousModLabel
            // 
            this.previousModLabel.AutoSize = true;
            this.previousModLabel.Location = new System.Drawing.Point(9, 78);
            this.previousModLabel.Name = "previousModLabel";
            this.previousModLabel.Size = new System.Drawing.Size(118, 13);
            this.previousModLabel.TabIndex = 3;
            this.previousModLabel.Text = "Previously Loaded Mod";
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
            this.saveCleanedVanillaContentCheckBox.CheckedChanged += new System.EventHandler(this.saveCleanedVanillaContentCheckBox_CheckedChanged);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(12, 226);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(307, 226);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 261);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.saveCleanedVanillaContentCheckBox);
            this.Controls.Add(this.previousModLabel);
            this.Controls.Add(this.previousModTextBox);
            this.Controls.Add(this.rememberPreviousModCheckBox);
            this.Controls.Add(this.openWithVanillaCheckBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Settings";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox openWithVanillaCheckBox;
        private System.Windows.Forms.CheckBox rememberPreviousModCheckBox;
        private System.Windows.Forms.TextBox previousModTextBox;
        private System.Windows.Forms.Label previousModLabel;
        private System.Windows.Forms.CheckBox saveCleanedVanillaContentCheckBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
    }
}