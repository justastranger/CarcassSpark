namespace CarcassSpark.ObjectViewers
{
    partial class CultureViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CultureViewer));
            this.idLabel = new System.Windows.Forms.Label();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.endonymTextBox = new System.Windows.Forms.TextBox();
            this.endonymLabel = new System.Windows.Forms.Label();
            this.exonymTextBox = new System.Windows.Forms.TextBox();
            this.exonymLabel = new System.Windows.Forms.Label();
            this.fontScriptTextBox = new System.Windows.Forms.TextBox();
            this.fontScriptLabel = new System.Windows.Forms.Label();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.boldAllowedCheckBox = new System.Windows.Forms.CheckBox();
            this.releasedCheckBox = new System.Windows.Forms.CheckBox();
            this.UiLabelDataGridView = new System.Windows.Forms.DataGridView();
            this.uilabelkeys = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uilabelvalues = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.UiLabelDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Location = new System.Drawing.Point(12, 9);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(18, 13);
            this.idLabel.TabIndex = 0;
            this.idLabel.Text = "ID";
            this.ToolTip.SetToolTip(this.idLabel, "Internal ID of the culture");
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(15, 25);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(100, 20);
            this.idTextBox.TabIndex = 1;
            this.ToolTip.SetToolTip(this.idTextBox, "Internal ID of the culture");
            this.idTextBox.TextChanged += new System.EventHandler(this.IdTextBox_TextChanged);
            // 
            // endonymTextBox
            // 
            this.endonymTextBox.Location = new System.Drawing.Point(121, 25);
            this.endonymTextBox.Name = "endonymTextBox";
            this.endonymTextBox.Size = new System.Drawing.Size(100, 20);
            this.endonymTextBox.TabIndex = 2;
            this.ToolTip.SetToolTip(this.endonymTextBox, "Name of the language in its own language");
            this.endonymTextBox.TextChanged += new System.EventHandler(this.EndonymTextBox_TextChanged);
            // 
            // endonymLabel
            // 
            this.endonymLabel.AutoSize = true;
            this.endonymLabel.Location = new System.Drawing.Point(118, 9);
            this.endonymLabel.Name = "endonymLabel";
            this.endonymLabel.Size = new System.Drawing.Size(51, 13);
            this.endonymLabel.TabIndex = 3;
            this.endonymLabel.Text = "Endonym";
            this.ToolTip.SetToolTip(this.endonymLabel, "Name of the language in its own language");
            // 
            // exonymTextBox
            // 
            this.exonymTextBox.Location = new System.Drawing.Point(227, 25);
            this.exonymTextBox.Name = "exonymTextBox";
            this.exonymTextBox.Size = new System.Drawing.Size(100, 20);
            this.exonymTextBox.TabIndex = 4;
            this.ToolTip.SetToolTip(this.exonymTextBox, "Name of language in English");
            this.exonymTextBox.TextChanged += new System.EventHandler(this.ExonymTextBox_TextChanged);
            // 
            // exonymLabel
            // 
            this.exonymLabel.AutoSize = true;
            this.exonymLabel.Location = new System.Drawing.Point(224, 9);
            this.exonymLabel.Name = "exonymLabel";
            this.exonymLabel.Size = new System.Drawing.Size(44, 13);
            this.exonymLabel.TabIndex = 5;
            this.exonymLabel.Text = "Exonym";
            this.ToolTip.SetToolTip(this.exonymLabel, "Name of language in English");
            // 
            // fontScriptTextBox
            // 
            this.fontScriptTextBox.Location = new System.Drawing.Point(333, 25);
            this.fontScriptTextBox.Name = "fontScriptTextBox";
            this.fontScriptTextBox.Size = new System.Drawing.Size(100, 20);
            this.fontScriptTextBox.TabIndex = 6;
            this.ToolTip.SetToolTip(this.fontScriptTextBox, "Font Script used to display the text ingame. ex: latin, cyrillic, jp, cjk");
            this.fontScriptTextBox.TextChanged += new System.EventHandler(this.FontScriptTextBox_TextChanged);
            // 
            // fontScriptLabel
            // 
            this.fontScriptLabel.AutoSize = true;
            this.fontScriptLabel.Location = new System.Drawing.Point(330, 9);
            this.fontScriptLabel.Name = "fontScriptLabel";
            this.fontScriptLabel.Size = new System.Drawing.Size(58, 13);
            this.fontScriptLabel.TabIndex = 7;
            this.fontScriptLabel.Text = "Font Script";
            this.ToolTip.SetToolTip(this.fontScriptLabel, "Font Script used to display the text ingame. ex: latin, cyrillic, jp, cjk");
            // 
            // boldAllowedCheckBox
            // 
            this.boldAllowedCheckBox.AutoSize = true;
            this.boldAllowedCheckBox.Location = new System.Drawing.Point(15, 51);
            this.boldAllowedCheckBox.Name = "boldAllowedCheckBox";
            this.boldAllowedCheckBox.Size = new System.Drawing.Size(87, 17);
            this.boldAllowedCheckBox.TabIndex = 8;
            this.boldAllowedCheckBox.Text = "Bold Allowed";
            this.ToolTip.SetToolTip(this.boldAllowedCheckBox, "Tells the game whether or not the font script used in this culture will support b" +
        "eing bolded");
            this.boldAllowedCheckBox.UseVisualStyleBackColor = true;
            this.boldAllowedCheckBox.CheckedChanged += new System.EventHandler(this.BoldAllowedCheckBox_CheckedChanged);
            // 
            // releasedCheckBox
            // 
            this.releasedCheckBox.AutoSize = true;
            this.releasedCheckBox.Location = new System.Drawing.Point(108, 51);
            this.releasedCheckBox.Name = "releasedCheckBox";
            this.releasedCheckBox.Size = new System.Drawing.Size(71, 17);
            this.releasedCheckBox.TabIndex = 9;
            this.releasedCheckBox.Text = "Released";
            this.ToolTip.SetToolTip(this.releasedCheckBox, "Used to indicate whether the translation is still being worked on or if it\'s been" +
        " completed");
            this.releasedCheckBox.UseVisualStyleBackColor = true;
            this.releasedCheckBox.CheckedChanged += new System.EventHandler(this.ReleasedCheckBox_CheckedChanged);
            // 
            // UiLabelDataGridView
            // 
            this.UiLabelDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UiLabelDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.UiLabelDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UiLabelDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.uilabelkeys,
            this.uilabelvalues});
            this.UiLabelDataGridView.Location = new System.Drawing.Point(12, 74);
            this.UiLabelDataGridView.Name = "UiLabelDataGridView";
            this.UiLabelDataGridView.Size = new System.Drawing.Size(421, 318);
            this.UiLabelDataGridView.TabIndex = 10;
            // 
            // uilabelkeys
            // 
            this.uilabelkeys.HeaderText = "UI Label Keys";
            this.uilabelkeys.Name = "uilabelkeys";
            // 
            // uilabelvalues
            // 
            this.uilabelvalues.HeaderText = "UI Label Values";
            this.uilabelvalues.Name = "uilabelvalues";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.Location = new System.Drawing.Point(12, 398);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 11;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(358, 398);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 12;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // CultureViewer
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(445, 433);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.UiLabelDataGridView);
            this.Controls.Add(this.releasedCheckBox);
            this.Controls.Add(this.boldAllowedCheckBox);
            this.Controls.Add(this.fontScriptLabel);
            this.Controls.Add(this.fontScriptTextBox);
            this.Controls.Add(this.exonymLabel);
            this.Controls.Add(this.exonymTextBox);
            this.Controls.Add(this.endonymLabel);
            this.Controls.Add(this.endonymTextBox);
            this.Controls.Add(this.idTextBox);
            this.Controls.Add(this.idLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CultureViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Culture Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.UiLabelDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.TextBox endonymTextBox;
        private System.Windows.Forms.Label endonymLabel;
        private System.Windows.Forms.TextBox exonymTextBox;
        private System.Windows.Forms.Label exonymLabel;
        private System.Windows.Forms.TextBox fontScriptTextBox;
        private System.Windows.Forms.Label fontScriptLabel;
        private System.Windows.Forms.ToolTip ToolTip;
        private System.Windows.Forms.CheckBox boldAllowedCheckBox;
        private System.Windows.Forms.CheckBox releasedCheckBox;
        private System.Windows.Forms.DataGridView UiLabelDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn uilabelkeys;
        private System.Windows.Forms.DataGridViewTextBoxColumn uilabelvalues;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
    }
}