﻿namespace CarcassSpark.ObjectViewers
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
            this.propertyOperationContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setAsPrependToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setAsAppendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setAsRemoveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.isHiddenCheckBox = new System.Windows.Forms.CheckBox();
            this.noartworkneededCheckBox = new System.Windows.Forms.CheckBox();
            this.idLabel = new System.Windows.Forms.Label();
            this.labelLabel = new System.Windows.Forms.Label();
            this.iconLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.commentsLabel = new System.Windows.Forms.Label();
            this.commentTextBox = new System.Windows.Forms.TextBox();
            this.inheritsLabel = new System.Windows.Forms.Label();
            this.inheritsTextBox = new System.Windows.Forms.TextBox();
            this.deletedCheckBox = new System.Windows.Forms.CheckBox();
            this.extendsTextBox = new System.Windows.Forms.TextBox();
            this.extendsLabel = new System.Windows.Forms.Label();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.verbIconLabel = new System.Windows.Forms.Label();
            this.verbIconTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inducesDataGridView)).BeginInit();
            this.propertyOperationContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // idTextBox
            // 
            this.idTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.idTextBox.Location = new System.Drawing.Point(146, 25);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(134, 20);
            this.idTextBox.TabIndex = 0;
            this.idTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.idTextBox, "The internal name of the aspect.");
            this.idTextBox.TextChanged += new System.EventHandler(this.IdTextBox_TextChanged);
            // 
            // labelTextBox
            // 
            this.labelTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTextBox.Location = new System.Drawing.Point(146, 64);
            this.labelTextBox.Name = "labelTextBox";
            this.labelTextBox.Size = new System.Drawing.Size(134, 20);
            this.labelTextBox.TabIndex = 1;
            this.labelTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.labelTextBox, "The name of the aspect when shown to the player.");
            this.labelTextBox.TextChanged += new System.EventHandler(this.LabelTextBox_TextChanged);
            // 
            // iconTextBox
            // 
            this.iconTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.iconTextBox.Location = new System.Drawing.Point(146, 103);
            this.iconTextBox.Name = "iconTextBox";
            this.iconTextBox.Size = new System.Drawing.Size(134, 20);
            this.iconTextBox.TabIndex = 2;
            this.iconTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.iconTextBox, "File name, minus extension, of the aspect icon from\"imaged/aspects\" you want disp" +
        "layed for this aspect.\r\nIf this property is not present, the ID will be used ins" +
        "tead.");
            this.iconTextBox.TextChanged += new System.EventHandler(this.IconTextBox_TextChanged);
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
            this.descriptionTextBox.AcceptsReturn = true;
            this.descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionTextBox.Location = new System.Drawing.Point(12, 247);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descriptionTextBox.Size = new System.Drawing.Size(630, 66);
            this.descriptionTextBox.TabIndex = 4;
            this.descriptionTextBox.TextChanged += new System.EventHandler(this.DescriptionTextBox_TextChanged);
            // 
            // inducesLabel
            // 
            this.inducesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.inducesLabel.AutoSize = true;
            this.inducesLabel.Location = new System.Drawing.Point(286, 12);
            this.inducesLabel.Name = "inducesLabel";
            this.inducesLabel.Size = new System.Drawing.Size(45, 13);
            this.inducesLabel.TabIndex = 5;
            this.inducesLabel.Text = "Induces";
            // 
            // inducesDataGridView
            // 
            this.inducesDataGridView.AllowUserToResizeColumns = false;
            this.inducesDataGridView.AllowUserToResizeRows = false;
            this.inducesDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.inducesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.inducesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.recipeId,
            this.chance,
            this.inducesAdditional});
            this.inducesDataGridView.ContextMenuStrip = this.propertyOperationContextMenuStrip;
            this.inducesDataGridView.Location = new System.Drawing.Point(287, 28);
            this.inducesDataGridView.Name = "inducesDataGridView";
            this.inducesDataGridView.Size = new System.Drawing.Size(355, 213);
            this.inducesDataGridView.TabIndex = 6;
            this.ToolTip.SetToolTip(this.inducesDataGridView, resources.GetString("inducesDataGridView.ToolTip"));
            this.inducesDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.InducesDataGridView_CellDoubleClick);
            this.inducesDataGridView.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.InducesDataGridView_UserDeletedRow);
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
            // propertyOperationContextMenuStrip
            // 
            this.propertyOperationContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setAsPrependToolStripMenuItem,
            this.setAsAppendToolStripMenuItem,
            this.setAsRemoveToolStripMenuItem});
            this.propertyOperationContextMenuStrip.Name = "propertyOperationContextMenuStrip";
            this.propertyOperationContextMenuStrip.ShowImageMargin = false;
            this.propertyOperationContextMenuStrip.Size = new System.Drawing.Size(127, 70);
            // 
            // setAsPrependToolStripMenuItem
            // 
            this.setAsPrependToolStripMenuItem.Name = "setAsPrependToolStripMenuItem";
            this.setAsPrependToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.setAsPrependToolStripMenuItem.Text = "Set as Prepend";
            this.setAsPrependToolStripMenuItem.Click += new System.EventHandler(this.SetAsPrependToolStripMenuItem_Click);
            // 
            // setAsAppendToolStripMenuItem
            // 
            this.setAsAppendToolStripMenuItem.Name = "setAsAppendToolStripMenuItem";
            this.setAsAppendToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.setAsAppendToolStripMenuItem.Text = "Set as Append";
            this.setAsAppendToolStripMenuItem.Click += new System.EventHandler(this.SetAsAppendToolStripMenuItem_Click);
            // 
            // setAsRemoveToolStripMenuItem
            // 
            this.setAsRemoveToolStripMenuItem.Name = "setAsRemoveToolStripMenuItem";
            this.setAsRemoveToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.setAsRemoveToolStripMenuItem.Text = "Set as Remove";
            this.setAsRemoveToolStripMenuItem.Click += new System.EventHandler(this.SetAsRemoveToolStripMenuItem_Click);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(12, 332);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(100, 38);
            this.okButton.TabIndex = 8;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(542, 332);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 38);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // isHiddenCheckBox
            // 
            this.isHiddenCheckBox.AutoSize = true;
            this.isHiddenCheckBox.Location = new System.Drawing.Point(12, 145);
            this.isHiddenCheckBox.Name = "isHiddenCheckBox";
            this.isHiddenCheckBox.Size = new System.Drawing.Size(66, 17);
            this.isHiddenCheckBox.TabIndex = 10;
            this.isHiddenCheckBox.Text = "Hidden?";
            this.isHiddenCheckBox.ThreeState = true;
            this.ToolTip.SetToolTip(this.isHiddenCheckBox, "Hides the aspect from the aspect list of an element.");
            this.isHiddenCheckBox.UseVisualStyleBackColor = true;
            this.isHiddenCheckBox.CheckStateChanged += new System.EventHandler(this.IsHiddenCheckBox_CheckStateChanged);
            // 
            // noartworkneededCheckBox
            // 
            this.noartworkneededCheckBox.AutoSize = true;
            this.noartworkneededCheckBox.Location = new System.Drawing.Point(12, 168);
            this.noartworkneededCheckBox.Name = "noartworkneededCheckBox";
            this.noartworkneededCheckBox.Size = new System.Drawing.Size(120, 17);
            this.noartworkneededCheckBox.TabIndex = 12;
            this.noartworkneededCheckBox.Text = "No Artwork Needed";
            this.noartworkneededCheckBox.ThreeState = true;
            this.ToolTip.SetToolTip(this.noartworkneededCheckBox, "Used in combination with isHidden, prevents the game from looking for artwork for" +
        " this asset.");
            this.noartworkneededCheckBox.UseVisualStyleBackColor = true;
            this.noartworkneededCheckBox.CheckStateChanged += new System.EventHandler(this.NoartworkneededCheckBox_CheckStateChanged);
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
            // descriptionLabel
            // 
            this.descriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(12, 231);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(60, 13);
            this.descriptionLabel.TabIndex = 17;
            this.descriptionLabel.Text = "Description";
            // 
            // commentsLabel
            // 
            this.commentsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.commentsLabel.AutoSize = true;
            this.commentsLabel.Location = new System.Drawing.Point(122, 316);
            this.commentsLabel.Name = "commentsLabel";
            this.commentsLabel.Size = new System.Drawing.Size(56, 13);
            this.commentsLabel.TabIndex = 18;
            this.commentsLabel.Text = "Comments";
            // 
            // commentTextBox
            // 
            this.commentTextBox.AcceptsReturn = true;
            this.commentTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.commentTextBox.Location = new System.Drawing.Point(118, 332);
            this.commentTextBox.Multiline = true;
            this.commentTextBox.Name = "commentTextBox";
            this.commentTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.commentTextBox.Size = new System.Drawing.Size(418, 38);
            this.commentTextBox.TabIndex = 19;
            this.ToolTip.SetToolTip(this.commentTextBox, "Comments are not shown ingame.");
            this.commentTextBox.TextChanged += new System.EventHandler(this.CommentTextBox_TextChanged);
            // 
            // inheritsLabel
            // 
            this.inheritsLabel.AutoSize = true;
            this.inheritsLabel.Location = new System.Drawing.Point(143, 126);
            this.inheritsLabel.Name = "inheritsLabel";
            this.inheritsLabel.Size = new System.Drawing.Size(41, 13);
            this.inheritsLabel.TabIndex = 20;
            this.inheritsLabel.Text = "Inherits";
            // 
            // inheritsTextBox
            // 
            this.inheritsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inheritsTextBox.Location = new System.Drawing.Point(146, 143);
            this.inheritsTextBox.Name = "inheritsTextBox";
            this.inheritsTextBox.Size = new System.Drawing.Size(134, 20);
            this.inheritsTextBox.TabIndex = 21;
            this.ToolTip.SetToolTip(this.inheritsTextBox, "Used to inherit properties from a single element/aspect.\r\nFunctionality has been " +
        "replaced by the new way that Extends is handled.");
            this.inheritsTextBox.TextChanged += new System.EventHandler(this.InheritsTextBox_TextChanged);
            // 
            // deletedCheckBox
            // 
            this.deletedCheckBox.AutoSize = true;
            this.deletedCheckBox.Location = new System.Drawing.Point(12, 191);
            this.deletedCheckBox.Name = "deletedCheckBox";
            this.deletedCheckBox.Size = new System.Drawing.Size(63, 17);
            this.deletedCheckBox.TabIndex = 22;
            this.deletedCheckBox.Text = "Deleted";
            this.deletedCheckBox.ThreeState = true;
            this.ToolTip.SetToolTip(this.deletedCheckBox, "This deletes the aspect if you\'re modifying an existing aspect.");
            this.deletedCheckBox.UseVisualStyleBackColor = true;
            this.deletedCheckBox.CheckStateChanged += new System.EventHandler(this.DeletedCheckBox_CheckStateChanged);
            // 
            // extendsTextBox
            // 
            this.extendsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.extendsTextBox.Location = new System.Drawing.Point(146, 182);
            this.extendsTextBox.Name = "extendsTextBox";
            this.extendsTextBox.Size = new System.Drawing.Size(134, 20);
            this.extendsTextBox.TabIndex = 23;
            this.ToolTip.SetToolTip(this.extendsTextBox, resources.GetString("extendsTextBox.ToolTip"));
            this.extendsTextBox.TextChanged += new System.EventHandler(this.ExtendsTextBox_TextChanged);
            // 
            // extendsLabel
            // 
            this.extendsLabel.AutoSize = true;
            this.extendsLabel.Location = new System.Drawing.Point(143, 166);
            this.extendsLabel.Name = "extendsLabel";
            this.extendsLabel.Size = new System.Drawing.Size(45, 13);
            this.extendsLabel.TabIndex = 24;
            this.extendsLabel.Text = "Extends";
            // 
            // verbIconLabel
            // 
            this.verbIconLabel.AutoSize = true;
            this.verbIconLabel.Location = new System.Drawing.Point(143, 205);
            this.verbIconLabel.Name = "verbIconLabel";
            this.verbIconLabel.Size = new System.Drawing.Size(53, 13);
            this.verbIconLabel.TabIndex = 25;
            this.verbIconLabel.Text = "Verb Icon";
            // 
            // verbIconTextBox
            // 
            this.verbIconTextBox.Location = new System.Drawing.Point(146, 221);
            this.verbIconTextBox.Name = "verbIconTextBox";
            this.verbIconTextBox.Size = new System.Drawing.Size(134, 20);
            this.verbIconTextBox.TabIndex = 26;
            this.verbIconTextBox.TextChanged += new System.EventHandler(this.VerbIconTextBox_TextChanged);
            // 
            // AspectViewer
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(654, 382);
            this.Controls.Add(this.verbIconTextBox);
            this.Controls.Add(this.verbIconLabel);
            this.Controls.Add(this.extendsLabel);
            this.Controls.Add(this.extendsTextBox);
            this.Controls.Add(this.deletedCheckBox);
            this.Controls.Add(this.inheritsTextBox);
            this.Controls.Add(this.inheritsLabel);
            this.Controls.Add(this.commentTextBox);
            this.Controls.Add(this.commentsLabel);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.iconLabel);
            this.Controls.Add(this.labelLabel);
            this.Controls.Add(this.idLabel);
            this.Controls.Add(this.noartworkneededCheckBox);
            this.Controls.Add(this.isHiddenCheckBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.inducesDataGridView);
            this.Controls.Add(this.inducesLabel);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.iconTextBox);
            this.Controls.Add(this.labelTextBox);
            this.Controls.Add(this.idTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(670, 421);
            this.Name = "AspectViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aspect Viewer";
            this.Shown += new System.EventHandler(this.AspectViewer_Shown);
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
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.CheckBox isHiddenCheckBox;
        private System.Windows.Forms.CheckBox noartworkneededCheckBox;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label labelLabel;
        private System.Windows.Forms.Label iconLabel;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.DataGridViewCheckBoxColumn inducesAdditional;
        private System.Windows.Forms.ContextMenuStrip propertyOperationContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem setAsPrependToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setAsAppendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setAsRemoveToolStripMenuItem;
        private System.Windows.Forms.Label commentsLabel;
        private System.Windows.Forms.TextBox commentTextBox;
        private System.Windows.Forms.Label inheritsLabel;
        private System.Windows.Forms.TextBox inheritsTextBox;
        private System.Windows.Forms.CheckBox deletedCheckBox;
        private System.Windows.Forms.TextBox extendsTextBox;
        private System.Windows.Forms.Label extendsLabel;
        private System.Windows.Forms.ToolTip ToolTip;
        private System.Windows.Forms.Label verbIconLabel;
        private System.Windows.Forms.TextBox verbIconTextBox;
    }
}