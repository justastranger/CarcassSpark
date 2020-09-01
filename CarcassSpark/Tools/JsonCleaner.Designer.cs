namespace CarcassSpark.Tools
{
    partial class JsonCleaner
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
            this.inputBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.outputBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.selectFolderButton = new System.Windows.Forms.Button();
            this.saveToFolderButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // selectFolderButton
            // 
            this.selectFolderButton.Location = new System.Drawing.Point(12, 12);
            this.selectFolderButton.Name = "selectFolderButton";
            this.selectFolderButton.Size = new System.Drawing.Size(150, 50);
            this.selectFolderButton.TabIndex = 0;
            this.selectFolderButton.Text = "Select Folder to Clean";
            this.selectFolderButton.UseVisualStyleBackColor = true;
            this.selectFolderButton.Click += new System.EventHandler(this.SelectFolderButton_Click);
            // 
            // saveToFolderButton
            // 
            this.saveToFolderButton.Location = new System.Drawing.Point(12, 68);
            this.saveToFolderButton.Name = "saveToFolderButton";
            this.saveToFolderButton.Size = new System.Drawing.Size(150, 50);
            this.saveToFolderButton.TabIndex = 1;
            this.saveToFolderButton.Text = "Select Folder to Save to";
            this.saveToFolderButton.UseVisualStyleBackColor = true;
            this.saveToFolderButton.Click += new System.EventHandler(this.SaveToFolderButton_Click);
            // 
            // JsonCleaner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(175, 131);
            this.Controls.Add(this.saveToFolderButton);
            this.Controls.Add(this.selectFolderButton);
            this.Name = "JsonCleaner";
            this.Text = "JsonCleaner";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog inputBrowserDialog;
        private System.Windows.Forms.FolderBrowserDialog outputBrowserDialog;
        private System.Windows.Forms.Button selectFolderButton;
        private System.Windows.Forms.Button saveToFolderButton;
    }
}