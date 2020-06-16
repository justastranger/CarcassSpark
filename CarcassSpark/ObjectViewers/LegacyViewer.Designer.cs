namespace CarcassSpark.ObjectViewers
{
    partial class LegacyViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LegacyViewer));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.labelTextBox = new System.Windows.Forms.TextBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.imageTextBox = new System.Windows.Forms.TextBox();
            this.fromEndingTextBox = new System.Windows.Forms.TextBox();
            this.startdescriptionTextBox = new System.Windows.Forms.TextBox();
            this.availableWithoutEndingMatchCheckBox = new System.Windows.Forms.CheckBox();
            this.effectsDataGridView = new System.Windows.Forms.DataGridView();
            this.effectId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.effectsAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.propertyOperationContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setAsExtendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setAsRemoveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startingVerbIdTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.idLabel = new System.Windows.Forms.Label();
            this.labelLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.startDescriptionLabel = new System.Windows.Forms.Label();
            this.imageLabel = new System.Windows.Forms.Label();
            this.fromEndingLabel = new System.Windows.Forms.Label();
            this.effectsLabel = new System.Windows.Forms.Label();
            this.excludesOnEndingLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.startingVerbLabel = new System.Windows.Forms.Label();
            this.addExcludesTextBox = new System.Windows.Forms.TextBox();
            this.addExcludesButton = new System.Windows.Forms.Button();
            this.excludeAddLabel = new System.Windows.Forms.Label();
            this.removeButton = new System.Windows.Forms.Button();
            this.excludesOnEndingListView = new System.Windows.Forms.ListView();
            this.statusBarElementsListView = new System.Windows.Forms.ListView();
            this.statusBarElementsLabel = new System.Windows.Forms.Label();
            this.statusBarElementTextBox = new System.Windows.Forms.TextBox();
            this.statusBarElementsLabel2 = new System.Windows.Forms.Label();
            this.addStatusBarElementButton = new System.Windows.Forms.Button();
            this.removeStatusBarElementButton = new System.Windows.Forms.Button();
            this.extendsLabel = new System.Windows.Forms.Label();
            this.extendsTextBox = new System.Windows.Forms.TextBox();
            this.commentsLabel = new System.Windows.Forms.Label();
            this.commentsTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.effectsDataGridView)).BeginInit();
            this.propertyOperationContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(130, 130);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(148, 25);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(100, 20);
            this.idTextBox.TabIndex = 1;
            this.idTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.idTextBox.TextChanged += new System.EventHandler(this.idTextBox_TextChanged);
            // 
            // labelTextBox
            // 
            this.labelTextBox.Location = new System.Drawing.Point(254, 25);
            this.labelTextBox.Name = "labelTextBox";
            this.labelTextBox.Size = new System.Drawing.Size(100, 20);
            this.labelTextBox.TabIndex = 2;
            this.labelTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.labelTextBox.TextChanged += new System.EventHandler(this.labelTextBox_TextChanged);
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(148, 64);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descriptionTextBox.Size = new System.Drawing.Size(206, 86);
            this.descriptionTextBox.TabIndex = 3;
            this.descriptionTextBox.TextChanged += new System.EventHandler(this.descriptionTextBox_TextChanged);
            // 
            // imageTextBox
            // 
            this.imageTextBox.Location = new System.Drawing.Point(360, 25);
            this.imageTextBox.Name = "imageTextBox";
            this.imageTextBox.Size = new System.Drawing.Size(100, 20);
            this.imageTextBox.TabIndex = 4;
            this.imageTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.imageTextBox.TextChanged += new System.EventHandler(this.imageTextBox_TextChanged);
            // 
            // fromEndingTextBox
            // 
            this.fromEndingTextBox.Location = new System.Drawing.Point(360, 64);
            this.fromEndingTextBox.Name = "fromEndingTextBox";
            this.fromEndingTextBox.Size = new System.Drawing.Size(100, 20);
            this.fromEndingTextBox.TabIndex = 5;
            this.fromEndingTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.fromEndingTextBox.TextChanged += new System.EventHandler(this.fromEndingTextBox_TextChanged);
            // 
            // startdescriptionTextBox
            // 
            this.startdescriptionTextBox.Location = new System.Drawing.Point(148, 169);
            this.startdescriptionTextBox.Multiline = true;
            this.startdescriptionTextBox.Name = "startdescriptionTextBox";
            this.startdescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.startdescriptionTextBox.Size = new System.Drawing.Size(206, 87);
            this.startdescriptionTextBox.TabIndex = 6;
            this.startdescriptionTextBox.TextChanged += new System.EventHandler(this.startdescriptionTextBox_TextChanged);
            // 
            // availableWithoutEndingMatchCheckBox
            // 
            this.availableWithoutEndingMatchCheckBox.AutoSize = true;
            this.availableWithoutEndingMatchCheckBox.Location = new System.Drawing.Point(360, 90);
            this.availableWithoutEndingMatchCheckBox.Name = "availableWithoutEndingMatchCheckBox";
            this.availableWithoutEndingMatchCheckBox.Size = new System.Drawing.Size(178, 17);
            this.availableWithoutEndingMatchCheckBox.TabIndex = 7;
            this.availableWithoutEndingMatchCheckBox.Text = "Available Without Ending Match";
            this.availableWithoutEndingMatchCheckBox.ThreeState = true;
            this.availableWithoutEndingMatchCheckBox.UseVisualStyleBackColor = true;
            this.availableWithoutEndingMatchCheckBox.CheckStateChanged += new System.EventHandler(this.availableWithoutEndingMatchCheckBox_CheckStateChanged);
            // 
            // effectsDataGridView
            // 
            this.effectsDataGridView.AllowUserToResizeColumns = false;
            this.effectsDataGridView.AllowUserToResizeRows = false;
            this.effectsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.effectsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.effectId,
            this.effectsAmount});
            this.effectsDataGridView.ContextMenuStrip = this.propertyOperationContextMenuStrip;
            this.effectsDataGridView.Location = new System.Drawing.Point(12, 262);
            this.effectsDataGridView.Name = "effectsDataGridView";
            this.effectsDataGridView.Size = new System.Drawing.Size(342, 194);
            this.effectsDataGridView.TabIndex = 8;
            this.effectsDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.effectsDataGridView_CellDoubleClick);
            this.effectsDataGridView.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.effectsDataGridView_UserDeletedRow);
            // 
            // effectId
            // 
            this.effectId.HeaderText = "Element ID";
            this.effectId.Name = "effectId";
            this.effectId.Width = 150;
            // 
            // effectsAmount
            // 
            this.effectsAmount.HeaderText = "Amount";
            this.effectsAmount.Name = "effectsAmount";
            this.effectsAmount.Width = 149;
            // 
            // propertyOperationContextMenuStrip
            // 
            this.propertyOperationContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setAsExtendToolStripMenuItem,
            this.setAsRemoveToolStripMenuItem});
            this.propertyOperationContextMenuStrip.Name = "propertyOperationContextMenuStrip";
            this.propertyOperationContextMenuStrip.Size = new System.Drawing.Size(151, 48);
            // 
            // setAsExtendToolStripMenuItem
            // 
            this.setAsExtendToolStripMenuItem.Name = "setAsExtendToolStripMenuItem";
            this.setAsExtendToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.setAsExtendToolStripMenuItem.Text = "Set as Extend";
            this.setAsExtendToolStripMenuItem.Click += new System.EventHandler(this.setAsExtendToolStripMenuItem_Click);
            // 
            // setAsRemoveToolStripMenuItem
            // 
            this.setAsRemoveToolStripMenuItem.Name = "setAsRemoveToolStripMenuItem";
            this.setAsRemoveToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.setAsRemoveToolStripMenuItem.Text = "Set as Remove";
            this.setAsRemoveToolStripMenuItem.Click += new System.EventHandler(this.setAsRemoveToolStripMenuItem_Click);
            // 
            // startingVerbIdTextBox
            // 
            this.startingVerbIdTextBox.Location = new System.Drawing.Point(466, 25);
            this.startingVerbIdTextBox.Name = "startingVerbIdTextBox";
            this.startingVerbIdTextBox.Size = new System.Drawing.Size(100, 20);
            this.startingVerbIdTextBox.TabIndex = 10;
            this.startingVerbIdTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.startingVerbIdTextBox.TextChanged += new System.EventHandler(this.startingVerbIdTextBox_TextChanged);
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(12, 462);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(100, 25);
            this.okButton.TabIndex = 11;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Location = new System.Drawing.Point(145, 9);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(56, 13);
            this.idLabel.TabIndex = 12;
            this.idLabel.Text = "Legacy ID";
            // 
            // labelLabel
            // 
            this.labelLabel.AutoSize = true;
            this.labelLabel.Location = new System.Drawing.Point(254, 9);
            this.labelLabel.Name = "labelLabel";
            this.labelLabel.Size = new System.Drawing.Size(71, 13);
            this.labelLabel.TabIndex = 13;
            this.labelLabel.Text = "Legacy Label";
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(145, 48);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(60, 13);
            this.descriptionLabel.TabIndex = 14;
            this.descriptionLabel.Text = "Description";
            // 
            // startDescriptionLabel
            // 
            this.startDescriptionLabel.AutoSize = true;
            this.startDescriptionLabel.Location = new System.Drawing.Point(145, 153);
            this.startDescriptionLabel.Name = "startDescriptionLabel";
            this.startDescriptionLabel.Size = new System.Drawing.Size(85, 13);
            this.startDescriptionLabel.TabIndex = 15;
            this.startDescriptionLabel.Text = "Start Description";
            // 
            // imageLabel
            // 
            this.imageLabel.AutoSize = true;
            this.imageLabel.Location = new System.Drawing.Point(357, 9);
            this.imageLabel.Name = "imageLabel";
            this.imageLabel.Size = new System.Drawing.Size(36, 13);
            this.imageLabel.TabIndex = 16;
            this.imageLabel.Text = "Image";
            // 
            // fromEndingLabel
            // 
            this.fromEndingLabel.AutoSize = true;
            this.fromEndingLabel.Location = new System.Drawing.Point(357, 48);
            this.fromEndingLabel.Name = "fromEndingLabel";
            this.fromEndingLabel.Size = new System.Drawing.Size(66, 13);
            this.fromEndingLabel.TabIndex = 17;
            this.fromEndingLabel.Text = "From Ending";
            // 
            // effectsLabel
            // 
            this.effectsLabel.AutoSize = true;
            this.effectsLabel.Location = new System.Drawing.Point(9, 246);
            this.effectsLabel.Name = "effectsLabel";
            this.effectsLabel.Size = new System.Drawing.Size(73, 13);
            this.effectsLabel.TabIndex = 18;
            this.effectsLabel.Text = "Starting Cards";
            // 
            // excludesOnEndingLabel
            // 
            this.excludesOnEndingLabel.AutoSize = true;
            this.excludesOnEndingLabel.Location = new System.Drawing.Point(357, 184);
            this.excludesOnEndingLabel.Name = "excludesOnEndingLabel";
            this.excludesOnEndingLabel.Size = new System.Drawing.Size(159, 13);
            this.excludesOnEndingLabel.TabIndex = 19;
            this.excludesOnEndingLabel.Text = "Exclude this after these legacies";
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(487, 464);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 23);
            this.cancelButton.TabIndex = 20;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // startingVerbLabel
            // 
            this.startingVerbLabel.AutoSize = true;
            this.startingVerbLabel.Location = new System.Drawing.Point(463, 9);
            this.startingVerbLabel.Name = "startingVerbLabel";
            this.startingVerbLabel.Size = new System.Drawing.Size(68, 13);
            this.startingVerbLabel.TabIndex = 21;
            this.startingVerbLabel.Text = "Starting Verb";
            // 
            // addExcludesTextBox
            // 
            this.addExcludesTextBox.Location = new System.Drawing.Point(360, 301);
            this.addExcludesTextBox.Name = "addExcludesTextBox";
            this.addExcludesTextBox.Size = new System.Drawing.Size(115, 20);
            this.addExcludesTextBox.TabIndex = 22;
            this.addExcludesTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.addExcludesTextBox_KeyDown);
            // 
            // addExcludesButton
            // 
            this.addExcludesButton.Location = new System.Drawing.Point(481, 301);
            this.addExcludesButton.Name = "addExcludesButton";
            this.addExcludesButton.Size = new System.Drawing.Size(38, 20);
            this.addExcludesButton.TabIndex = 23;
            this.addExcludesButton.Text = "Add";
            this.addExcludesButton.UseVisualStyleBackColor = true;
            this.addExcludesButton.Click += new System.EventHandler(this.addExcludesButton_Click);
            // 
            // excludeAddLabel
            // 
            this.excludeAddLabel.AutoSize = true;
            this.excludeAddLabel.Location = new System.Drawing.Point(357, 285);
            this.excludeAddLabel.Name = "excludeAddLabel";
            this.excludeAddLabel.Size = new System.Drawing.Size(109, 13);
            this.excludeAddLabel.TabIndex = 24;
            this.excludeAddLabel.Text = "Legacy ID to Exclude";
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(525, 301);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(62, 20);
            this.removeButton.TabIndex = 25;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // excludesOnEndingListView
            // 
            this.excludesOnEndingListView.Location = new System.Drawing.Point(360, 205);
            this.excludesOnEndingListView.MultiSelect = false;
            this.excludesOnEndingListView.Name = "excludesOnEndingListView";
            this.excludesOnEndingListView.Size = new System.Drawing.Size(227, 77);
            this.excludesOnEndingListView.TabIndex = 26;
            this.excludesOnEndingListView.UseCompatibleStateImageBehavior = false;
            this.excludesOnEndingListView.View = System.Windows.Forms.View.List;
            this.excludesOnEndingListView.DoubleClick += new System.EventHandler(this.excludesOnEndingListView_DoubleClick);
            // 
            // statusBarElementsListView
            // 
            this.statusBarElementsListView.Location = new System.Drawing.Point(360, 340);
            this.statusBarElementsListView.Name = "statusBarElementsListView";
            this.statusBarElementsListView.Size = new System.Drawing.Size(227, 77);
            this.statusBarElementsListView.TabIndex = 27;
            this.statusBarElementsListView.UseCompatibleStateImageBehavior = false;
            // 
            // statusBarElementsLabel
            // 
            this.statusBarElementsLabel.AutoSize = true;
            this.statusBarElementsLabel.Location = new System.Drawing.Point(357, 324);
            this.statusBarElementsLabel.Name = "statusBarElementsLabel";
            this.statusBarElementsLabel.Size = new System.Drawing.Size(102, 13);
            this.statusBarElementsLabel.TabIndex = 28;
            this.statusBarElementsLabel.Text = "Status Bar Elements";
            // 
            // statusBarElementTextBox
            // 
            this.statusBarElementTextBox.Location = new System.Drawing.Point(360, 439);
            this.statusBarElementTextBox.Name = "statusBarElementTextBox";
            this.statusBarElementTextBox.Size = new System.Drawing.Size(115, 20);
            this.statusBarElementTextBox.TabIndex = 29;
            this.statusBarElementTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.statusBarElementTextBox_KeyDown);
            // 
            // statusBarElementsLabel2
            // 
            this.statusBarElementsLabel2.AutoSize = true;
            this.statusBarElementsLabel2.Location = new System.Drawing.Point(357, 422);
            this.statusBarElementsLabel2.Name = "statusBarElementsLabel2";
            this.statusBarElementsLabel2.Size = new System.Drawing.Size(157, 13);
            this.statusBarElementsLabel2.TabIndex = 30;
            this.statusBarElementsLabel2.Text = "Element ID to Add to Status Bar";
            // 
            // addStatusBarElementButton
            // 
            this.addStatusBarElementButton.Location = new System.Drawing.Point(481, 438);
            this.addStatusBarElementButton.Name = "addStatusBarElementButton";
            this.addStatusBarElementButton.Size = new System.Drawing.Size(38, 20);
            this.addStatusBarElementButton.TabIndex = 31;
            this.addStatusBarElementButton.Text = "Add";
            this.addStatusBarElementButton.UseVisualStyleBackColor = true;
            this.addStatusBarElementButton.Click += new System.EventHandler(this.addStatusBarElementButton_Click);
            // 
            // removeStatusBarElementButton
            // 
            this.removeStatusBarElementButton.Location = new System.Drawing.Point(525, 437);
            this.removeStatusBarElementButton.Name = "removeStatusBarElementButton";
            this.removeStatusBarElementButton.Size = new System.Drawing.Size(62, 20);
            this.removeStatusBarElementButton.TabIndex = 32;
            this.removeStatusBarElementButton.Text = "Remove";
            this.removeStatusBarElementButton.UseVisualStyleBackColor = true;
            this.removeStatusBarElementButton.Click += new System.EventHandler(this.removeStatusBarElementButton_Click);
            // 
            // extendsLabel
            // 
            this.extendsLabel.AutoSize = true;
            this.extendsLabel.Location = new System.Drawing.Point(463, 48);
            this.extendsLabel.Name = "extendsLabel";
            this.extendsLabel.Size = new System.Drawing.Size(45, 13);
            this.extendsLabel.TabIndex = 34;
            this.extendsLabel.Text = "Extends";
            // 
            // extendsTextBox
            // 
            this.extendsTextBox.Location = new System.Drawing.Point(466, 64);
            this.extendsTextBox.Name = "extendsTextBox";
            this.extendsTextBox.Size = new System.Drawing.Size(100, 20);
            this.extendsTextBox.TabIndex = 35;
            this.extendsTextBox.TextChanged += new System.EventHandler(this.extendsTextBox_TextChanged);
            // 
            // commentsLabel
            // 
            this.commentsLabel.AutoSize = true;
            this.commentsLabel.Location = new System.Drawing.Point(360, 110);
            this.commentsLabel.Name = "commentsLabel";
            this.commentsLabel.Size = new System.Drawing.Size(56, 13);
            this.commentsLabel.TabIndex = 36;
            this.commentsLabel.Text = "Comments";
            // 
            // commentsTextBox
            // 
            this.commentsTextBox.Location = new System.Drawing.Point(360, 126);
            this.commentsTextBox.Multiline = true;
            this.commentsTextBox.Name = "commentsTextBox";
            this.commentsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.commentsTextBox.Size = new System.Drawing.Size(227, 55);
            this.commentsTextBox.TabIndex = 37;
            this.commentsTextBox.TextChanged += new System.EventHandler(this.commentsTextBox_TextChanged);
            // 
            // LegacyViewer
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 499);
            this.Controls.Add(this.commentsTextBox);
            this.Controls.Add(this.commentsLabel);
            this.Controls.Add(this.extendsTextBox);
            this.Controls.Add(this.extendsLabel);
            this.Controls.Add(this.removeStatusBarElementButton);
            this.Controls.Add(this.addStatusBarElementButton);
            this.Controls.Add(this.statusBarElementsLabel2);
            this.Controls.Add(this.statusBarElementTextBox);
            this.Controls.Add(this.statusBarElementsLabel);
            this.Controls.Add(this.statusBarElementsListView);
            this.Controls.Add(this.excludesOnEndingListView);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.excludeAddLabel);
            this.Controls.Add(this.addExcludesButton);
            this.Controls.Add(this.addExcludesTextBox);
            this.Controls.Add(this.startingVerbLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.excludesOnEndingLabel);
            this.Controls.Add(this.effectsLabel);
            this.Controls.Add(this.fromEndingLabel);
            this.Controls.Add(this.imageLabel);
            this.Controls.Add(this.startDescriptionLabel);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.labelLabel);
            this.Controls.Add(this.idLabel);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.startingVerbIdTextBox);
            this.Controls.Add(this.effectsDataGridView);
            this.Controls.Add(this.availableWithoutEndingMatchCheckBox);
            this.Controls.Add(this.startdescriptionTextBox);
            this.Controls.Add(this.fromEndingTextBox);
            this.Controls.Add(this.imageTextBox);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.labelTextBox);
            this.Controls.Add(this.idTextBox);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LegacyViewer";
            this.Text = "LegacyViewer";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.effectsDataGridView)).EndInit();
            this.propertyOperationContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.TextBox labelTextBox;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.TextBox imageTextBox;
        private System.Windows.Forms.TextBox fromEndingTextBox;
        private System.Windows.Forms.TextBox startdescriptionTextBox;
        private System.Windows.Forms.CheckBox availableWithoutEndingMatchCheckBox;
        private System.Windows.Forms.DataGridView effectsDataGridView;
        private System.Windows.Forms.TextBox startingVerbIdTextBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label labelLabel;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.Label startDescriptionLabel;
        private System.Windows.Forms.Label imageLabel;
        private System.Windows.Forms.Label fromEndingLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn effectId;
        private System.Windows.Forms.DataGridViewTextBoxColumn effectsAmount;
        private System.Windows.Forms.Label effectsLabel;
        private System.Windows.Forms.Label excludesOnEndingLabel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label startingVerbLabel;
        private System.Windows.Forms.TextBox addExcludesTextBox;
        private System.Windows.Forms.Button addExcludesButton;
        private System.Windows.Forms.Label excludeAddLabel;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.ListView excludesOnEndingListView;
        private System.Windows.Forms.ListView statusBarElementsListView;
        private System.Windows.Forms.Label statusBarElementsLabel;
        private System.Windows.Forms.TextBox statusBarElementTextBox;
        private System.Windows.Forms.Label statusBarElementsLabel2;
        private System.Windows.Forms.Button addStatusBarElementButton;
        private System.Windows.Forms.Button removeStatusBarElementButton;
        private System.Windows.Forms.ContextMenuStrip propertyOperationContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem setAsExtendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setAsRemoveToolStripMenuItem;
        private System.Windows.Forms.Label extendsLabel;
        private System.Windows.Forms.TextBox extendsTextBox;
        private System.Windows.Forms.Label commentsLabel;
        private System.Windows.Forms.TextBox commentsTextBox;
    }
}