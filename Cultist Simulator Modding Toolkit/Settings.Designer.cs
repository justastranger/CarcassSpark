namespace CultistSimulatorModdingToolkit
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
            this.openWithVanillaCheckBox = new System.Windows.Forms.CheckBox();
            this.rememberPreviousModCheckBox = new System.Windows.Forms.CheckBox();
            this.previousModTextBox = new System.Windows.Forms.TextBox();
            this.previousModLabel = new System.Windows.Forms.Label();
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
            this.rememberPreviousModCheckBox.Location = new System.Drawing.Point(12, 35);
            this.rememberPreviousModCheckBox.Name = "rememberPreviousModCheckBox";
            this.rememberPreviousModCheckBox.Size = new System.Drawing.Size(314, 17);
            this.rememberPreviousModCheckBox.TabIndex = 1;
            this.rememberPreviousModCheckBox.Text = "Remember and Load Previously Opened Mod When Opened";
            this.rememberPreviousModCheckBox.UseVisualStyleBackColor = true;
            this.rememberPreviousModCheckBox.CheckedChanged += new System.EventHandler(this.rememberPreviousModCheckBox_CheckedChanged);
            // 
            // previousModTextBox
            // 
            this.previousModTextBox.Location = new System.Drawing.Point(12, 71);
            this.previousModTextBox.Name = "previousModTextBox";
            this.previousModTextBox.ReadOnly = true;
            this.previousModTextBox.Size = new System.Drawing.Size(303, 20);
            this.previousModTextBox.TabIndex = 2;
            // 
            // previousModLabel
            // 
            this.previousModLabel.AutoSize = true;
            this.previousModLabel.Location = new System.Drawing.Point(12, 55);
            this.previousModLabel.Name = "previousModLabel";
            this.previousModLabel.Size = new System.Drawing.Size(118, 13);
            this.previousModLabel.TabIndex = 3;
            this.previousModLabel.Text = "Previously Loaded Mod";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 261);
            this.Controls.Add(this.previousModLabel);
            this.Controls.Add(this.previousModTextBox);
            this.Controls.Add(this.rememberPreviousModCheckBox);
            this.Controls.Add(this.openWithVanillaCheckBox);
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
    }
}