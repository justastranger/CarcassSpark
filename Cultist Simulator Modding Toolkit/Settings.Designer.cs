namespace Cultist_Simulator_Modding_Toolkit
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
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.openWithVanillaCheckBox);
            this.Name = "Settings";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox openWithVanillaCheckBox;
    }
}