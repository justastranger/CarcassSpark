namespace Cultist_Simulator_Modding_Toolkit
{
    partial class ManifestViewer
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
            this.modNameTextBox = new System.Windows.Forms.TextBox();
            this.modAuthorTextBox = new System.Windows.Forms.TextBox();
            this.modVersionTextBox = new System.Windows.Forms.TextBox();
            this.modDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.longDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // modNameTextBox
            // 
            this.modNameTextBox.Location = new System.Drawing.Point(12, 12);
            this.modNameTextBox.Name = "modNameTextBox";
            this.modNameTextBox.Size = new System.Drawing.Size(260, 20);
            this.modNameTextBox.TabIndex = 0;
            this.modNameTextBox.Text = "Mod Name";
            this.modNameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.modNameTextBox.TextChanged += new System.EventHandler(this.modNameTextBox_TextChanged);
            // 
            // modAuthorTextBox
            // 
            this.modAuthorTextBox.Location = new System.Drawing.Point(12, 38);
            this.modAuthorTextBox.Name = "modAuthorTextBox";
            this.modAuthorTextBox.Size = new System.Drawing.Size(260, 20);
            this.modAuthorTextBox.TabIndex = 1;
            this.modAuthorTextBox.Text = "Mod Author";
            this.modAuthorTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.modAuthorTextBox.TextChanged += new System.EventHandler(this.modAuthorTextBox_TextChanged);
            // 
            // modVersionTextBox
            // 
            this.modVersionTextBox.Location = new System.Drawing.Point(12, 64);
            this.modVersionTextBox.Name = "modVersionTextBox";
            this.modVersionTextBox.Size = new System.Drawing.Size(260, 20);
            this.modVersionTextBox.TabIndex = 2;
            this.modVersionTextBox.Text = "Mod Version";
            this.modVersionTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.modVersionTextBox.TextChanged += new System.EventHandler(this.modVersionTextBox_TextChanged);
            // 
            // modDescriptionTextBox
            // 
            this.modDescriptionTextBox.Location = new System.Drawing.Point(12, 90);
            this.modDescriptionTextBox.Multiline = true;
            this.modDescriptionTextBox.Name = "modDescriptionTextBox";
            this.modDescriptionTextBox.Size = new System.Drawing.Size(260, 60);
            this.modDescriptionTextBox.TabIndex = 3;
            this.modDescriptionTextBox.Text = "Mod Description";
            this.modDescriptionTextBox.TextChanged += new System.EventHandler(this.modDescriptionTextBox_TextChanged);
            // 
            // longDescriptionTextBox
            // 
            this.longDescriptionTextBox.Location = new System.Drawing.Point(12, 156);
            this.longDescriptionTextBox.Multiline = true;
            this.longDescriptionTextBox.Name = "longDescriptionTextBox";
            this.longDescriptionTextBox.Size = new System.Drawing.Size(260, 93);
            this.longDescriptionTextBox.TabIndex = 4;
            this.longDescriptionTextBox.Text = "Mod Description";
            this.longDescriptionTextBox.TextChanged += new System.EventHandler(this.longDescriptionTextBox_TextChanged);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(12, 264);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(197, 264);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // ManifestViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 299);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.longDescriptionTextBox);
            this.Controls.Add(this.modDescriptionTextBox);
            this.Controls.Add(this.modVersionTextBox);
            this.Controls.Add(this.modAuthorTextBox);
            this.Controls.Add(this.modNameTextBox);
            this.Name = "ManifestViewer";
            this.Text = "ManifestViewer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox modNameTextBox;
        private System.Windows.Forms.TextBox modAuthorTextBox;
        private System.Windows.Forms.TextBox modVersionTextBox;
        private System.Windows.Forms.TextBox modDescriptionTextBox;
        private System.Windows.Forms.TextBox longDescriptionTextBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
    }
}