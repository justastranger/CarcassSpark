namespace CarcassSpark.ObjectViewers
{
    partial class SynopsisViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SynopsisViewer));
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
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dependeniesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // modNameTextBox
            // 
            this.modNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modNameTextBox.Location = new System.Drawing.Point(12, 25);
            this.modNameTextBox.Name = "modNameTextBox";
            this.modNameTextBox.Size = new System.Drawing.Size(380, 20);
            this.modNameTextBox.TabIndex = 0;
            this.modNameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.modNameTextBox, "This is the name and (for the purpose of dependencies) the ID of the mod, as disp" +
        "layed in the mod menu.");
            this.modNameTextBox.TextChanged += new System.EventHandler(this.ModNameTextBox_TextChanged);
            // 
            // modAuthorTextBox
            // 
            this.modAuthorTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modAuthorTextBox.Location = new System.Drawing.Point(12, 64);
            this.modAuthorTextBox.Name = "modAuthorTextBox";
            this.modAuthorTextBox.Size = new System.Drawing.Size(380, 20);
            this.modAuthorTextBox.TabIndex = 1;
            this.modAuthorTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.modAuthorTextBox, "This is where the credits go, usually just you.");
            this.modAuthorTextBox.TextChanged += new System.EventHandler(this.ModAuthorTextBox_TextChanged);
            // 
            // modVersionTextBox
            // 
            this.modVersionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modVersionTextBox.Location = new System.Drawing.Point(12, 103);
            this.modVersionTextBox.Name = "modVersionTextBox";
            this.modVersionTextBox.Size = new System.Drawing.Size(380, 20);
            this.modVersionTextBox.TabIndex = 2;
            this.modVersionTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.modVersionTextBox, resources.GetString("modVersionTextBox.ToolTip"));
            this.modVersionTextBox.TextChanged += new System.EventHandler(this.ModVersionTextBox_TextChanged);
            // 
            // modDescriptionTextBox
            // 
            this.modDescriptionTextBox.AcceptsReturn = true;
            this.modDescriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modDescriptionTextBox.Location = new System.Drawing.Point(12, 142);
            this.modDescriptionTextBox.Multiline = true;
            this.modDescriptionTextBox.Name = "modDescriptionTextBox";
            this.modDescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.modDescriptionTextBox.Size = new System.Drawing.Size(380, 60);
            this.modDescriptionTextBox.TabIndex = 3;
            this.ToolTip.SetToolTip(this.modDescriptionTextBox, "This is the mod description shown in the mod menu and on the Steam Workshop.");
            this.modDescriptionTextBox.TextChanged += new System.EventHandler(this.ModDescriptionTextBox_TextChanged);
            // 
            // longDescriptionTextBox
            // 
            this.longDescriptionTextBox.AcceptsReturn = true;
            this.longDescriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.longDescriptionTextBox.Location = new System.Drawing.Point(12, 221);
            this.longDescriptionTextBox.Multiline = true;
            this.longDescriptionTextBox.Name = "longDescriptionTextBox";
            this.longDescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.longDescriptionTextBox.Size = new System.Drawing.Size(380, 132);
            this.longDescriptionTextBox.TabIndex = 4;
            this.ToolTip.SetToolTip(this.longDescriptionTextBox, "This version is not currently shown anywhere ingame.");
            this.longDescriptionTextBox.TextChanged += new System.EventHandler(this.LongDescriptionTextBox_TextChanged);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(12, 486);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(317, 486);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
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
            this.modDependenciesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.modDependenciesLabel.AutoSize = true;
            this.modDependenciesLabel.Location = new System.Drawing.Point(12, 356);
            this.modDependenciesLabel.Name = "modDependenciesLabel";
            this.modDependenciesLabel.Size = new System.Drawing.Size(100, 13);
            this.modDependenciesLabel.TabIndex = 12;
            this.modDependenciesLabel.Text = "Mod Dependencies";
            // 
            // dependeniesDataGridView
            // 
            this.dependeniesDataGridView.AllowUserToResizeColumns = false;
            this.dependeniesDataGridView.AllowUserToResizeRows = false;
            this.dependeniesDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dependeniesDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dependeniesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dependeniesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dependencyModId,
            this.dependencyVersionOperator,
            this.dependencyVersion});
            this.dependeniesDataGridView.Location = new System.Drawing.Point(12, 372);
            this.dependeniesDataGridView.Name = "dependeniesDataGridView";
            this.dependeniesDataGridView.Size = new System.Drawing.Size(380, 108);
            this.dependeniesDataGridView.TabIndex = 13;
            this.ToolTip.SetToolTip(this.dependeniesDataGridView, "If your mod depends on another mod, you can say so and prevent your mod from bein" +
        "g used without its dependencies.");
            // 
            // dependencyModId
            // 
            this.dependencyModId.HeaderText = "Mod Name/ID";
            this.dependencyModId.Name = "dependencyModId";
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
            // 
            // dependencyVersion
            // 
            this.dependencyVersion.HeaderText = "Version";
            this.dependencyVersion.Name = "dependencyVersion";
            this.dependencyVersion.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dependencyVersion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SynopsisViewer
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(404, 521);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(397, 488);
            this.Name = "SynopsisViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Synopsis Viewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SynopsisViewer_FormClosing);
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
        private System.Windows.Forms.ToolTip ToolTip;
    }
}