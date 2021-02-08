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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JsonCleaner));
            this.inputBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.outputBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.selectInputFolderButton = new System.Windows.Forms.Button();
            this.selectOutputFolderButton = new System.Windows.Forms.Button();
            this.inputTextBox = new System.Windows.Forms.TextBox();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.inputLabel = new System.Windows.Forms.Label();
            this.outputLabel = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // selectInputFolderButton
            // 
            this.selectInputFolderButton.Location = new System.Drawing.Point(12, 12);
            this.selectInputFolderButton.Name = "selectInputFolderButton";
            this.selectInputFolderButton.Size = new System.Drawing.Size(150, 50);
            this.selectInputFolderButton.TabIndex = 0;
            this.selectInputFolderButton.Text = "Select Folder to Clean";
            this.ToolTip.SetToolTip(this.selectInputFolderButton, "Select the root folder from which you\'d like to clean JSON files.\r\nAll files will" +
        " be saved into the same folder structure that they were loaded from.");
            this.selectInputFolderButton.UseVisualStyleBackColor = true;
            this.selectInputFolderButton.Click += new System.EventHandler(this.SelectInputFolderButton_Click);
            // 
            // selectOutputFolderButton
            // 
            this.selectOutputFolderButton.Enabled = false;
            this.selectOutputFolderButton.Location = new System.Drawing.Point(12, 68);
            this.selectOutputFolderButton.Name = "selectOutputFolderButton";
            this.selectOutputFolderButton.Size = new System.Drawing.Size(150, 50);
            this.selectOutputFolderButton.TabIndex = 1;
            this.selectOutputFolderButton.Text = "Select Folder to Save to";
            this.ToolTip.SetToolTip(this.selectOutputFolderButton, "Select a folder to save the cleaned JSON to.\r\nAll files will be saved into the sa" +
        "me folder structure that they were loaded from.");
            this.selectOutputFolderButton.UseVisualStyleBackColor = true;
            this.selectOutputFolderButton.Click += new System.EventHandler(this.SelectOutputFolderButton_Click);
            // 
            // inputTextBox
            // 
            this.inputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputTextBox.Location = new System.Drawing.Point(168, 28);
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.ReadOnly = true;
            this.inputTextBox.Size = new System.Drawing.Size(279, 20);
            this.inputTextBox.TabIndex = 2;
            this.ToolTip.SetToolTip(this.inputTextBox, "This is the folder you\'ve selected to clean the JSON in.");
            // 
            // outputTextBox
            // 
            this.outputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputTextBox.Location = new System.Drawing.Point(168, 84);
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.Size = new System.Drawing.Size(279, 20);
            this.outputTextBox.TabIndex = 3;
            this.ToolTip.SetToolTip(this.outputTextBox, "This is the root directory that you\'re saving the JSON files into.");
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.saveButton.Enabled = false;
            this.saveButton.Location = new System.Drawing.Point(12, 124);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(150, 50);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Save Cleaned JSON";
            this.ToolTip.SetToolTip(this.saveButton, "This will save the cleaned files and open an instance of Explorer to the output f" +
        "older.");
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // inputLabel
            // 
            this.inputLabel.AutoSize = true;
            this.inputLabel.Location = new System.Drawing.Point(168, 12);
            this.inputLabel.Name = "inputLabel";
            this.inputLabel.Size = new System.Drawing.Size(63, 13);
            this.inputLabel.TabIndex = 5;
            this.inputLabel.Text = "Input Folder";
            // 
            // outputLabel
            // 
            this.outputLabel.AutoSize = true;
            this.outputLabel.Location = new System.Drawing.Point(168, 68);
            this.outputLabel.Name = "outputLabel";
            this.outputLabel.Size = new System.Drawing.Size(71, 13);
            this.outputLabel.TabIndex = 6;
            this.outputLabel.Text = "Output Folder";
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(372, 151);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 7;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // JsonCleaner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(459, 184);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.outputLabel);
            this.Controls.Add(this.inputLabel);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.outputTextBox);
            this.Controls.Add(this.inputTextBox);
            this.Controls.Add(this.selectOutputFolderButton);
            this.Controls.Add(this.selectInputFolderButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(475, 223);
            this.Name = "JsonCleaner";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Json Cleaner";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog inputBrowserDialog;
        private System.Windows.Forms.FolderBrowserDialog outputBrowserDialog;
        private System.Windows.Forms.Button selectInputFolderButton;
        private System.Windows.Forms.Button selectOutputFolderButton;
        private System.Windows.Forms.TextBox inputTextBox;
        private System.Windows.Forms.TextBox outputTextBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.ToolTip ToolTip;
        private System.Windows.Forms.Label inputLabel;
        private System.Windows.Forms.Label outputLabel;
        private System.Windows.Forms.Button closeButton;
    }
}