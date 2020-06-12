namespace CarcassSpark.ObjectViewers
{
    partial class AspectViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AspectViewer));
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.labelTextBox = new System.Windows.Forms.TextBox();
            this.iconTextBox = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.inducesLabel = new System.Windows.Forms.Label();
            this.inducesDataGridView = new System.Windows.Forms.DataGridView();
            this.recipeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inducesAdditional = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.extendsTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.isHiddenCheckBox = new System.Windows.Forms.CheckBox();
            this.noartworkneededCheckBox = new System.Windows.Forms.CheckBox();
            this.idLabel = new System.Windows.Forms.Label();
            this.labelLabel = new System.Windows.Forms.Label();
            this.iconLabel = new System.Windows.Forms.Label();
            this.extendsLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.propertyOperationContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setAsPrependToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setAsAppendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setAsRemoveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inducesDataGridView)).BeginInit();
            this.propertyOperationContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(146, 25);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(134, 20);
            this.idTextBox.TabIndex = 0;
            this.idTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.idTextBox.TextChanged += new System.EventHandler(this.idTextBox_TextChanged);
            // 
            // labelTextBox
            // 
            this.labelTextBox.Location = new System.Drawing.Point(146, 64);
            this.labelTextBox.Name = "labelTextBox";
            this.labelTextBox.Size = new System.Drawing.Size(134, 20);
            this.labelTextBox.TabIndex = 1;
            this.labelTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.labelTextBox.TextChanged += new System.EventHandler(this.labelTextBox_TextChanged);
            // 
            // iconTextBox
            // 
            this.iconTextBox.Location = new System.Drawing.Point(146, 103);
            this.iconTextBox.Name = "iconTextBox";
            this.iconTextBox.Size = new System.Drawing.Size(134, 20);
            this.iconTextBox.TabIndex = 2;
            this.iconTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.iconTextBox.TextChanged += new System.EventHandler(this.iconTextBox_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 128);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(12, 180);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descriptionTextBox.Size = new System.Drawing.Size(630, 69);
            this.descriptionTextBox.TabIndex = 4;
            this.descriptionTextBox.TextChanged += new System.EventHandler(this.descriptionTextBox_TextChanged);
            // 
            // inducesLabel
            // 
            this.inducesLabel.AutoSize = true;
            this.inducesLabel.Location = new System.Drawing.Point(364, 15);
            this.inducesLabel.Name = "inducesLabel";
            this.inducesLabel.Size = new System.Drawing.Size(45, 13);
            this.inducesLabel.TabIndex = 5;
            this.inducesLabel.Text = "Induces";
            // 
            // inducesDataGridView
            // 
            this.inducesDataGridView.AllowUserToResizeColumns = false;
            this.inducesDataGridView.AllowUserToResizeRows = false;
            this.inducesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.inducesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.recipeId,
            this.chance,
            this.inducesAdditional});
            this.inducesDataGridView.ContextMenuStrip = this.propertyOperationContextMenuStrip;
            this.inducesDataGridView.Location = new System.Drawing.Point(287, 31);
            this.inducesDataGridView.Name = "inducesDataGridView";
            this.inducesDataGridView.Size = new System.Drawing.Size(355, 143);
            this.inducesDataGridView.TabIndex = 6;
            this.inducesDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.inducesDataGridView_CellDoubleClick);
            this.inducesDataGridView.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.inducesDataGridView_UserDeletedRow);
            // 
            // recipeId
            // 
            this.recipeId.HeaderText = "Recipe ID";
            this.recipeId.Name = "recipeId";
            this.recipeId.Width = 104;
            // 
            // chance
            // 
            this.chance.HeaderText = "Chance";
            this.chance.Name = "chance";
            this.chance.Width = 104;
            // 
            // inducesAdditional
            // 
            this.inducesAdditional.HeaderText = "Additional?";
            this.inducesAdditional.Name = "inducesAdditional";
            this.inducesAdditional.Width = 104;
            // 
            // extendsTextBox
            // 
            this.extendsTextBox.Location = new System.Drawing.Point(146, 141);
            this.extendsTextBox.Name = "extendsTextBox";
            this.extendsTextBox.Size = new System.Drawing.Size(134, 20);
            this.extendsTextBox.TabIndex = 7;
            this.extendsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.extendsTextBox.TextChanged += new System.EventHandler(this.extendsTextBox_TextChanged);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(12, 255);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(100, 38);
            this.okButton.TabIndex = 8;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(542, 260);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 38);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // isHiddenCheckBox
            // 
            this.isHiddenCheckBox.AutoSize = true;
            this.isHiddenCheckBox.Location = new System.Drawing.Point(12, 145);
            this.isHiddenCheckBox.Name = "isHiddenCheckBox";
            this.isHiddenCheckBox.Size = new System.Drawing.Size(66, 17);
            this.isHiddenCheckBox.TabIndex = 10;
            this.isHiddenCheckBox.Text = "Hidden?";
            this.isHiddenCheckBox.UseVisualStyleBackColor = true;
            this.isHiddenCheckBox.CheckedChanged += new System.EventHandler(this.isHiddenCheckBox_CheckedChanged);
            // 
            // noartworkneededCheckBox
            // 
            this.noartworkneededCheckBox.AutoSize = true;
            this.noartworkneededCheckBox.Location = new System.Drawing.Point(12, 160);
            this.noartworkneededCheckBox.Name = "noartworkneededCheckBox";
            this.noartworkneededCheckBox.Size = new System.Drawing.Size(120, 17);
            this.noartworkneededCheckBox.TabIndex = 12;
            this.noartworkneededCheckBox.Text = "No Artwork Needed";
            this.noartworkneededCheckBox.UseVisualStyleBackColor = true;
            this.noartworkneededCheckBox.CheckedChanged += new System.EventHandler(this.noartworkneededCheckBox_CheckedChanged);
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Location = new System.Drawing.Point(143, 9);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(54, 13);
            this.idLabel.TabIndex = 13;
            this.idLabel.Text = "Aspect ID";
            // 
            // labelLabel
            // 
            this.labelLabel.AutoSize = true;
            this.labelLabel.Location = new System.Drawing.Point(143, 48);
            this.labelLabel.Name = "labelLabel";
            this.labelLabel.Size = new System.Drawing.Size(33, 13);
            this.labelLabel.TabIndex = 14;
            this.labelLabel.Text = "Label";
            // 
            // iconLabel
            // 
            this.iconLabel.AutoSize = true;
            this.iconLabel.Location = new System.Drawing.Point(143, 87);
            this.iconLabel.Name = "iconLabel";
            this.iconLabel.Size = new System.Drawing.Size(28, 13);
            this.iconLabel.TabIndex = 15;
            this.iconLabel.Text = "Icon";
            // 
            // extendsLabel
            // 
            this.extendsLabel.AutoSize = true;
            this.extendsLabel.Location = new System.Drawing.Point(143, 126);
            this.extendsLabel.Name = "extendsLabel";
            this.extendsLabel.Size = new System.Drawing.Size(45, 13);
            this.extendsLabel.TabIndex = 16;
            this.extendsLabel.Text = "Extends";
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(143, 164);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(60, 13);
            this.descriptionLabel.TabIndex = 17;
            this.descriptionLabel.Text = "Description";
            // 
            // propertyOperationContextMenuStrip
            // 
            this.propertyOperationContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setAsPrependToolStripMenuItem,
            this.setAsAppendToolStripMenuItem,
            this.setAsRemoveToolStripMenuItem});
            this.propertyOperationContextMenuStrip.Name = "propertyOperationContextMenuStrip";
            this.propertyOperationContextMenuStrip.ShowImageMargin = false;
            this.propertyOperationContextMenuStrip.Size = new System.Drawing.Size(128, 92);
            // 
            // setAsPrependToolStripMenuItem
            // 
            this.setAsPrependToolStripMenuItem.Name = "setAsPrependToolStripMenuItem";
            this.setAsPrependToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.setAsPrependToolStripMenuItem.Text = "Set as Prepend";
            this.setAsPrependToolStripMenuItem.Click += new System.EventHandler(this.setAsPrependToolStripMenuItem_Click);
            // 
            // setAsAppendToolStripMenuItem
            // 
            this.setAsAppendToolStripMenuItem.Name = "setAsAppendToolStripMenuItem";
            this.setAsAppendToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.setAsAppendToolStripMenuItem.Text = "Set as Append";
            this.setAsAppendToolStripMenuItem.Click += new System.EventHandler(this.setAsAppendToolStripMenuItem_Click);
            // 
            // setAsRemoveToolStripMenuItem
            // 
            this.setAsRemoveToolStripMenuItem.Name = "setAsRemoveToolStripMenuItem";
            this.setAsRemoveToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.setAsRemoveToolStripMenuItem.Text = "Set as Remove";
            this.setAsRemoveToolStripMenuItem.Click += new System.EventHandler(this.setAsRemoveToolStripMenuItem_Click);
            // 
            // AspectViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 310);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.extendsLabel);
            this.Controls.Add(this.iconLabel);
            this.Controls.Add(this.labelLabel);
            this.Controls.Add(this.idLabel);
            this.Controls.Add(this.noartworkneededCheckBox);
            this.Controls.Add(this.isHiddenCheckBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.extendsTextBox);
            this.Controls.Add(this.inducesDataGridView);
            this.Controls.Add(this.inducesLabel);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.iconTextBox);
            this.Controls.Add(this.labelTextBox);
            this.Controls.Add(this.idTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AspectViewer";
            this.Text = "Aspect Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inducesDataGridView)).EndInit();
            this.propertyOperationContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.TextBox labelTextBox;
        private System.Windows.Forms.TextBox iconTextBox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Label inducesLabel;
        private System.Windows.Forms.DataGridView inducesDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn recipeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn chance;
        private System.Windows.Forms.TextBox extendsTextBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.CheckBox isHiddenCheckBox;
        private System.Windows.Forms.CheckBox noartworkneededCheckBox;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label labelLabel;
        private System.Windows.Forms.Label iconLabel;
        private System.Windows.Forms.Label extendsLabel;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.DataGridViewCheckBoxColumn inducesAdditional;
        private System.Windows.Forms.ContextMenuStrip propertyOperationContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem setAsPrependToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setAsAppendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setAsRemoveToolStripMenuItem;
    }
}