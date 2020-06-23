namespace CarcassSpark.Tools
{
    partial class JsonEditor
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
            this.contentTypeComboBox = new System.Windows.Forms.ComboBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.scintillaEditor = new ScintillaNET.Scintilla();
            this.SuspendLayout();
            // 
            // contentTypeComboBox
            // 
            this.contentTypeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contentTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.contentTypeComboBox.FormattingEnabled = true;
            this.contentTypeComboBox.Items.AddRange(new object[] {
            "Aspect",
            "Element",
            "Recipe",
            "Deck",
            "Legacy",
            "Ending",
            "Verb"});
            this.contentTypeComboBox.Location = new System.Drawing.Point(93, 449);
            this.contentTypeComboBox.Name = "contentTypeComboBox";
            this.contentTypeComboBox.Size = new System.Drawing.Size(401, 21);
            this.contentTypeComboBox.TabIndex = 1;
            this.contentTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.contentTypeComboBox_SelectedIndexChanged);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(12, 447);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(500, 447);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // scintillaEditor
            // 
            this.scintillaEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scintillaEditor.Lexer = ScintillaNET.Lexer.Json;
            this.scintillaEditor.Location = new System.Drawing.Point(12, 12);
            this.scintillaEditor.Name = "scintillaEditor";
            this.scintillaEditor.Size = new System.Drawing.Size(563, 429);
            this.scintillaEditor.TabIndex = 4;
            this.scintillaEditor.UseTabs = true;
            this.scintillaEditor.TextChanged += new System.EventHandler(this.scintillaEditor_TextChanged);
            // 
            // JsonEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 482);
            this.Controls.Add(this.scintillaEditor);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.contentTypeComboBox);
            this.Name = "JsonEditor";
            this.Text = "JSON Editor";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox contentTypeComboBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private ScintillaNET.Scintilla scintillaEditor;
    }
}