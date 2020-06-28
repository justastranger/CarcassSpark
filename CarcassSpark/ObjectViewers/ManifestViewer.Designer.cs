namespace CarcassSpark.ObjectViewers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManifestViewer));
            this.modNameTextBox = new System.Windows.Forms.TextBox();
            this.modAuthorTextBox = new System.Windows.Forms.TextBox();
            this.modVersionTextBox = new System.Windows.Forms.TextBox();
            this.modDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.longDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.modNameLabel = new System.Windows.Forms.Label();
            this.modAuthorLabel = new System.Windows.Forms.Label();
            this.modVersionLabel = new System.Windows.Forms.Label();
            this.modDescriptionLabel = new System.Windows.Forms.Label();
            this.longModDescriptionLabel = new System.Windows.Forms.Label();
            this.modDependenciesLabel = new System.Windows.Forms.Label();
            this.dependeniesDataGridView = new System.Windows.Forms.DataGridView();
            this.dependencyModId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dependencyVersionOperator = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dependencyVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dependeniesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // modNameTextBox
            // 
            this.modNameTextBox.Location = new System.Drawing.Point(12, 25);
            this.modNameTextBox.Name = "modNameTextBox";
            this.modNameTextBox.Size = new System.Drawing.Size(357, 20);
            this.modNameTextBox.TabIndex = 0;
            this.modNameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.modNameTextBox.TextChanged += new System.EventHandler(this.modNameTextBox_TextChanged);
            // 
            // modAuthorTextBox
            // 
            this.modAuthorTextBox.Location = new System.Drawing.Point(12, 64);
            this.modAuthorTextBox.Name = "modAuthorTextBox";
            this.modAuthorTextBox.Size = new System.Drawing.Size(357, 20);
            this.modAuthorTextBox.TabIndex = 1;
            this.modAuthorTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.modAuthorTextBox.TextChanged += new System.EventHandler(this.modAuthorTextBox_TextChanged);
            // 
            // modVersionTextBox
            // 
            this.modVersionTextBox.Location = new System.Drawing.Point(12, 103);
            this.modVersionTextBox.Name = "modVersionTextBox";
            this.modVersionTextBox.Size = new System.Drawing.Size(357, 20);
            this.modVersionTextBox.TabIndex = 2;
            this.modVersionTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.modVersionTextBox.TextChanged += new System.EventHandler(this.modVersionTextBox_TextChanged);
            // 
            // modDescriptionTextBox
            // 
            this.modDescriptionTextBox.AcceptsReturn = true;
            this.modDescriptionTextBox.Location = new System.Drawing.Point(12, 142);
            this.modDescriptionTextBox.Multiline = true;
            this.modDescriptionTextBox.Name = "modDescriptionTextBox";
            this.modDescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.modDescriptionTextBox.Size = new System.Drawing.Size(357, 60);
            this.modDescriptionTextBox.TabIndex = 3;
            this.modDescriptionTextBox.TextChanged += new System.EventHandler(this.modDescriptionTextBox_TextChanged);
            // 
            // longDescriptionTextBox
            // 
            this.longDescriptionTextBox.AcceptsReturn = true;
            this.longDescriptionTextBox.Location = new System.Drawing.Point(12, 221);
            this.longDescriptionTextBox.Multiline = true;
            this.longDescriptionTextBox.Name = "longDescriptionTextBox";
            this.longDescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.longDescriptionTextBox.Size = new System.Drawing.Size(357, 60);
            this.longDescriptionTextBox.TabIndex = 4;
            this.longDescriptionTextBox.TextChanged += new System.EventHandler(this.longDescriptionTextBox_TextChanged);
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(12, 414);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(294, 414);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // modNameLabel
            // 
            this.modNameLabel.AutoSize = true;
            this.modNameLabel.Location = new System.Drawing.Point(12, 9);
            this.modNameLabel.Name = "modNameLabel";
            this.modNameLabel.Size = new System.Drawing.Size(59, 13);
            this.modNameLabel.TabIndex = 7;
            this.modNameLabel.Text = "Mod Name";
            // 
            // modAuthorLabel
            // 
            this.modAuthorLabel.AutoSize = true;
            this.modAuthorLabel.Location = new System.Drawing.Point(12, 48);
            this.modAuthorLabel.Name = "modAuthorLabel";
            this.modAuthorLabel.Size = new System.Drawing.Size(62, 13);
            this.modAuthorLabel.TabIndex = 8;
            this.modAuthorLabel.Text = "Mod Author";
            // 
            // modVersionLabel
            // 
            this.modVersionLabel.AutoSize = true;
            this.modVersionLabel.Location = new System.Drawing.Point(12, 87);
            this.modVersionLabel.Name = "modVersionLabel";
            this.modVersionLabel.Size = new System.Drawing.Size(66, 13);
            this.modVersionLabel.TabIndex = 9;
            this.modVersionLabel.Text = "Mod Version";
            // 
            // modDescriptionLabel
            // 
            this.modDescriptionLabel.AutoSize = true;
            this.modDescriptionLabel.Location = new System.Drawing.Point(12, 126);
            this.modDescriptionLabel.Name = "modDescriptionLabel";
            this.modDescriptionLabel.Size = new System.Drawing.Size(112, 13);
            this.modDescriptionLabel.TabIndex = 10;
            this.modDescriptionLabel.Text = "Short Mod Description";
            // 
            // longModDescriptionLabel
            // 
            this.longModDescriptionLabel.AutoSize = true;
            this.longModDescriptionLabel.Location = new System.Drawing.Point(12, 205);
            this.longModDescriptionLabel.Name = "longModDescriptionLabel";
            this.longModDescriptionLabel.Size = new System.Drawing.Size(111, 13);
            this.longModDescriptionLabel.TabIndex = 11;
            this.longModDescriptionLabel.Text = "Long Mod Description";
            // 
            // modDependenciesLabel
            // 
            this.modDependenciesLabel.AutoSize = true;
            this.modDependenciesLabel.Location = new System.Drawing.Point(12, 284);
            this.modDependenciesLabel.Name = "modDependenciesLabel";
            this.modDependenciesLabel.Size = new System.Drawing.Size(100, 13);
            this.modDependenciesLabel.TabIndex = 12;
            this.modDependenciesLabel.Text = "Mod Dependencies";
            // 
            // dependeniesDataGridView
            // 
            this.dependeniesDataGridView.AllowUserToResizeColumns = false;
            this.dependeniesDataGridView.AllowUserToResizeRows = false;
            this.dependeniesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dependeniesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dependencyModId,
            this.dependencyVersionOperator,
            this.dependencyVersion});
            this.dependeniesDataGridView.Location = new System.Drawing.Point(12, 300);
            this.dependeniesDataGridView.Name = "dependeniesDataGridView";
            this.dependeniesDataGridView.Size = new System.Drawing.Size(357, 108);
            this.dependeniesDataGridView.TabIndex = 13;
            // 
            // dependencyModId
            // 
            this.dependencyModId.HeaderText = "Mod Name/ID";
            this.dependencyModId.Name = "dependencyModId";
            this.dependencyModId.Width = 105;
            // 
            // dependencyVersionOperator
            // 
            this.dependencyVersionOperator.HeaderText = "VersionOperator";
            this.dependencyVersionOperator.Items.AddRange(new object[] {
            "==",
            ">=",
            ">",
            "<=",
            "<"});
            this.dependencyVersionOperator.Name = "dependencyVersionOperator";
            this.dependencyVersionOperator.Width = 104;
            // 
            // dependencyVersion
            // 
            this.dependencyVersion.HeaderText = "Version";
            this.dependencyVersion.Name = "dependencyVersion";
            this.dependencyVersion.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dependencyVersion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dependencyVersion.Width = 105;
            // 
            // ManifestViewer
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(381, 449);
            this.Controls.Add(this.dependeniesDataGridView);
            this.Controls.Add(this.modDependenciesLabel);
            this.Controls.Add(this.longModDescriptionLabel);
            this.Controls.Add(this.modDescriptionLabel);
            this.Controls.Add(this.modVersionLabel);
            this.Controls.Add(this.modAuthorLabel);
            this.Controls.Add(this.modNameLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.longDescriptionTextBox);
            this.Controls.Add(this.modDescriptionTextBox);
            this.Controls.Add(this.modVersionTextBox);
            this.Controls.Add(this.modAuthorTextBox);
            this.Controls.Add(this.modNameTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ManifestViewer";
            this.Text = "ManifestViewer";
            ((System.ComponentModel.ISupportInitialize)(this.dependeniesDataGridView)).EndInit();
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
        private System.Windows.Forms.Label modNameLabel;
        private System.Windows.Forms.Label modAuthorLabel;
        private System.Windows.Forms.Label modVersionLabel;
        private System.Windows.Forms.Label modDescriptionLabel;
        private System.Windows.Forms.Label longModDescriptionLabel;
        private System.Windows.Forms.Label modDependenciesLabel;
        private System.Windows.Forms.DataGridView dependeniesDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dependencyModId;
        private System.Windows.Forms.DataGridViewComboBoxColumn dependencyVersionOperator;
        private System.Windows.Forms.DataGridViewTextBoxColumn dependencyVersion;
    }
}