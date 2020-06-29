namespace CarcassSpark.ObjectViewers
{
    partial class DeckViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeckViewer));
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.labelTextBox = new System.Windows.Forms.TextBox();
            this.commentsTextBox = new System.Windows.Forms.TextBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.drawmessagesDataGridView = new System.Windows.Forms.DataGridView();
            this.elementId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resetOnExhaustionCheckBox = new System.Windows.Forms.CheckBox();
            this.defaultCardTextBox = new System.Windows.Forms.TextBox();
            this.drawsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.drawsLabel = new System.Windows.Forms.Label();
            this.newCardTextBox = new System.Windows.Forms.TextBox();
            this.newCardButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.idLabel = new System.Windows.Forms.Label();
            this.deckLabelLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.commentsLabel = new System.Windows.Forms.Label();
            this.defaultCardLabel = new System.Windows.Forms.Label();
            this.drawmessagesLlabel = new System.Windows.Forms.Label();
            this.specListView = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.specPrependButton = new System.Windows.Forms.Button();
            this.specAppendButton = new System.Windows.Forms.Button();
            this.extendsTextBox = new System.Windows.Forms.TextBox();
            this.extendsLabel = new System.Windows.Forms.Label();
            this.specRemoveButton = new System.Windows.Forms.Button();
            this.deletedCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.drawmessagesDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.drawsNumericUpDown)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(12, 25);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(125, 20);
            this.idTextBox.TabIndex = 0;
            this.idTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.idTextBox.TextChanged += new System.EventHandler(this.idTextBox_TextChanged);
            // 
            // labelTextBox
            // 
            this.labelTextBox.Location = new System.Drawing.Point(155, 25);
            this.labelTextBox.Name = "labelTextBox";
            this.labelTextBox.Size = new System.Drawing.Size(125, 20);
            this.labelTextBox.TabIndex = 1;
            this.labelTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.labelTextBox.TextChanged += new System.EventHandler(this.labelTextBox_TextChanged);
            // 
            // commentsTextBox
            // 
            this.commentsTextBox.Location = new System.Drawing.Point(286, 25);
            this.commentsTextBox.Name = "commentsTextBox";
            this.commentsTextBox.Size = new System.Drawing.Size(154, 20);
            this.commentsTextBox.TabIndex = 2;
            this.commentsTextBox.TextChanged += new System.EventHandler(this.commentsTextBox_TextChanged);
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.AcceptsReturn = true;
            this.descriptionTextBox.Location = new System.Drawing.Point(12, 64);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descriptionTextBox.Size = new System.Drawing.Size(268, 30);
            this.descriptionTextBox.TabIndex = 3;
            this.descriptionTextBox.TextChanged += new System.EventHandler(this.descriptionTextBox_TextChanged);
            // 
            // drawmessagesDataGridView
            // 
            this.drawmessagesDataGridView.AllowUserToResizeColumns = false;
            this.drawmessagesDataGridView.AllowUserToResizeRows = false;
            this.drawmessagesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.drawmessagesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.elementId,
            this.message});
            this.drawmessagesDataGridView.Location = new System.Drawing.Point(12, 221);
            this.drawmessagesDataGridView.Name = "drawmessagesDataGridView";
            this.drawmessagesDataGridView.Size = new System.Drawing.Size(268, 150);
            this.drawmessagesDataGridView.TabIndex = 5;
            this.drawmessagesDataGridView.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.drawmessagesDataGridView_UserDeletedRow);
            // 
            // elementId
            // 
            this.elementId.HeaderText = "Element ID";
            this.elementId.Name = "elementId";
            this.elementId.Width = 113;
            // 
            // message
            // 
            this.message.HeaderText = "Message";
            this.message.Name = "message";
            this.message.Width = 112;
            // 
            // resetOnExhaustionCheckBox
            // 
            this.resetOnExhaustionCheckBox.AutoSize = true;
            this.resetOnExhaustionCheckBox.Location = new System.Drawing.Point(12, 151);
            this.resetOnExhaustionCheckBox.Name = "resetOnExhaustionCheckBox";
            this.resetOnExhaustionCheckBox.Size = new System.Drawing.Size(115, 17);
            this.resetOnExhaustionCheckBox.TabIndex = 6;
            this.resetOnExhaustionCheckBox.Text = "resetOnExhaustion";
            this.resetOnExhaustionCheckBox.ThreeState = true;
            this.resetOnExhaustionCheckBox.UseVisualStyleBackColor = true;
            this.resetOnExhaustionCheckBox.CheckStateChanged += new System.EventHandler(this.resetOnExhaustionCheckBox_CheckStateChanged);
            // 
            // defaultCardTextBox
            // 
            this.defaultCardTextBox.Location = new System.Drawing.Point(92, 100);
            this.defaultCardTextBox.Name = "defaultCardTextBox";
            this.defaultCardTextBox.Size = new System.Drawing.Size(188, 20);
            this.defaultCardTextBox.TabIndex = 7;
            this.defaultCardTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.defaultCardTextBox.TextChanged += new System.EventHandler(this.defaultCardTextBox_TextChanged);
            // 
            // drawsNumericUpDown
            // 
            this.drawsNumericUpDown.Location = new System.Drawing.Point(55, 125);
            this.drawsNumericUpDown.Name = "drawsNumericUpDown";
            this.drawsNumericUpDown.Size = new System.Drawing.Size(60, 20);
            this.drawsNumericUpDown.TabIndex = 8;
            // 
            // drawsLabel
            // 
            this.drawsLabel.AutoSize = true;
            this.drawsLabel.Location = new System.Drawing.Point(12, 127);
            this.drawsLabel.Name = "drawsLabel";
            this.drawsLabel.Size = new System.Drawing.Size(37, 13);
            this.drawsLabel.TabIndex = 9;
            this.drawsLabel.Text = "Draws";
            // 
            // newCardTextBox
            // 
            this.newCardTextBox.Location = new System.Drawing.Point(286, 322);
            this.newCardTextBox.Name = "newCardTextBox";
            this.newCardTextBox.Size = new System.Drawing.Size(260, 20);
            this.newCardTextBox.TabIndex = 10;
            this.newCardTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.newCardTextBox_KeyDown);
            // 
            // newCardButton
            // 
            this.newCardButton.Location = new System.Drawing.Point(286, 348);
            this.newCardButton.Name = "newCardButton";
            this.newCardButton.Size = new System.Drawing.Size(53, 23);
            this.newCardButton.TabIndex = 11;
            this.newCardButton.Text = "Insert";
            this.newCardButton.UseVisualStyleBackColor = true;
            this.newCardButton.Click += new System.EventHandler(this.newCardButton_Click);
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(12, 377);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 12;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(471, 377);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 13;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Location = new System.Drawing.Point(12, 9);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(47, 13);
            this.idLabel.TabIndex = 14;
            this.idLabel.Text = "Deck ID";
            // 
            // deckLabelLabel
            // 
            this.deckLabelLabel.AutoSize = true;
            this.deckLabelLabel.Location = new System.Drawing.Point(152, 9);
            this.deckLabelLabel.Name = "deckLabelLabel";
            this.deckLabelLabel.Size = new System.Drawing.Size(62, 13);
            this.deckLabelLabel.TabIndex = 15;
            this.deckLabelLabel.Text = "Deck Label";
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(12, 48);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(60, 13);
            this.descriptionLabel.TabIndex = 16;
            this.descriptionLabel.Text = "Description";
            // 
            // commentsLabel
            // 
            this.commentsLabel.AutoSize = true;
            this.commentsLabel.Location = new System.Drawing.Point(283, 9);
            this.commentsLabel.Name = "commentsLabel";
            this.commentsLabel.Size = new System.Drawing.Size(56, 13);
            this.commentsLabel.TabIndex = 17;
            this.commentsLabel.Text = "Comments";
            // 
            // defaultCardLabel
            // 
            this.defaultCardLabel.AutoSize = true;
            this.defaultCardLabel.Location = new System.Drawing.Point(20, 103);
            this.defaultCardLabel.Name = "defaultCardLabel";
            this.defaultCardLabel.Size = new System.Drawing.Size(66, 13);
            this.defaultCardLabel.TabIndex = 18;
            this.defaultCardLabel.Text = "Default Card";
            // 
            // drawmessagesLlabel
            // 
            this.drawmessagesLlabel.AutoSize = true;
            this.drawmessagesLlabel.Location = new System.Drawing.Point(12, 205);
            this.drawmessagesLlabel.Name = "drawmessagesLlabel";
            this.drawmessagesLlabel.Size = new System.Drawing.Size(83, 13);
            this.drawmessagesLlabel.TabIndex = 19;
            this.drawmessagesLlabel.Text = "Draw Messages";
            // 
            // specListView
            // 
            this.specListView.ContextMenuStrip = this.contextMenuStrip1;
            this.specListView.Location = new System.Drawing.Point(286, 64);
            this.specListView.MultiSelect = false;
            this.specListView.Name = "specListView";
            this.specListView.Size = new System.Drawing.Size(260, 252);
            this.specListView.TabIndex = 21;
            this.specListView.UseCompatibleStateImageBehavior = false;
            this.specListView.View = System.Windows.Forms.View.List;
            this.specListView.DoubleClick += new System.EventHandler(this.specListView_DoubleClick);
            this.specListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.specListView_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(108, 26);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // specPrependButton
            // 
            this.specPrependButton.Location = new System.Drawing.Point(345, 348);
            this.specPrependButton.Name = "specPrependButton";
            this.specPrependButton.Size = new System.Drawing.Size(57, 23);
            this.specPrependButton.TabIndex = 22;
            this.specPrependButton.Text = "Prepend";
            this.specPrependButton.UseVisualStyleBackColor = true;
            this.specPrependButton.Click += new System.EventHandler(this.specPrependButton_Click);
            // 
            // specAppendButton
            // 
            this.specAppendButton.Location = new System.Drawing.Point(408, 348);
            this.specAppendButton.Name = "specAppendButton";
            this.specAppendButton.Size = new System.Drawing.Size(66, 23);
            this.specAppendButton.TabIndex = 23;
            this.specAppendButton.Text = "Append";
            this.specAppendButton.UseVisualStyleBackColor = true;
            this.specAppendButton.Click += new System.EventHandler(this.specAppendButton_Click);
            // 
            // extendsTextBox
            // 
            this.extendsTextBox.Location = new System.Drawing.Point(446, 25);
            this.extendsTextBox.Name = "extendsTextBox";
            this.extendsTextBox.Size = new System.Drawing.Size(100, 20);
            this.extendsTextBox.TabIndex = 24;
            this.extendsTextBox.TextChanged += new System.EventHandler(this.extendsTextBox_TextChanged);
            // 
            // extendsLabel
            // 
            this.extendsLabel.AutoSize = true;
            this.extendsLabel.Location = new System.Drawing.Point(443, 9);
            this.extendsLabel.Name = "extendsLabel";
            this.extendsLabel.Size = new System.Drawing.Size(45, 13);
            this.extendsLabel.TabIndex = 25;
            this.extendsLabel.Text = "Extends";
            // 
            // specRemoveButton
            // 
            this.specRemoveButton.Location = new System.Drawing.Point(480, 348);
            this.specRemoveButton.Name = "specRemoveButton";
            this.specRemoveButton.Size = new System.Drawing.Size(66, 23);
            this.specRemoveButton.TabIndex = 26;
            this.specRemoveButton.Text = "Remove";
            this.specRemoveButton.UseVisualStyleBackColor = true;
            this.specRemoveButton.Click += new System.EventHandler(this.specRemoveButton_Click);
            // 
            // deletedCheckBox
            // 
            this.deletedCheckBox.AutoSize = true;
            this.deletedCheckBox.Location = new System.Drawing.Point(12, 174);
            this.deletedCheckBox.Name = "deletedCheckBox";
            this.deletedCheckBox.Size = new System.Drawing.Size(63, 17);
            this.deletedCheckBox.TabIndex = 27;
            this.deletedCheckBox.Text = "Deleted";
            this.deletedCheckBox.ThreeState = true;
            this.deletedCheckBox.UseVisualStyleBackColor = true;
            this.deletedCheckBox.CheckStateChanged += new System.EventHandler(this.deletedCheckBox_CheckStateChanged);
            // 
            // DeckViewer
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(558, 412);
            this.Controls.Add(this.deletedCheckBox);
            this.Controls.Add(this.specRemoveButton);
            this.Controls.Add(this.extendsLabel);
            this.Controls.Add(this.extendsTextBox);
            this.Controls.Add(this.specAppendButton);
            this.Controls.Add(this.specPrependButton);
            this.Controls.Add(this.specListView);
            this.Controls.Add(this.drawmessagesLlabel);
            this.Controls.Add(this.defaultCardLabel);
            this.Controls.Add(this.commentsLabel);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.deckLabelLabel);
            this.Controls.Add(this.idLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.newCardButton);
            this.Controls.Add(this.newCardTextBox);
            this.Controls.Add(this.drawsLabel);
            this.Controls.Add(this.drawsNumericUpDown);
            this.Controls.Add(this.defaultCardTextBox);
            this.Controls.Add(this.resetOnExhaustionCheckBox);
            this.Controls.Add(this.drawmessagesDataGridView);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.commentsTextBox);
            this.Controls.Add(this.labelTextBox);
            this.Controls.Add(this.idTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DeckViewer";
            this.Text = "DeckViewer";
            ((System.ComponentModel.ISupportInitialize)(this.drawmessagesDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.drawsNumericUpDown)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.TextBox labelTextBox;
        private System.Windows.Forms.TextBox commentsTextBox;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.DataGridView drawmessagesDataGridView;
        private System.Windows.Forms.CheckBox resetOnExhaustionCheckBox;
        private System.Windows.Forms.TextBox defaultCardTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn elementId;
        private System.Windows.Forms.DataGridViewTextBoxColumn message;
        private System.Windows.Forms.NumericUpDown drawsNumericUpDown;
        private System.Windows.Forms.Label drawsLabel;
        private System.Windows.Forms.TextBox newCardTextBox;
        private System.Windows.Forms.Button newCardButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label deckLabelLabel;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.Label commentsLabel;
        private System.Windows.Forms.Label defaultCardLabel;
        private System.Windows.Forms.Label drawmessagesLlabel;
        private System.Windows.Forms.ListView specListView;
        private System.Windows.Forms.Button specPrependButton;
        private System.Windows.Forms.Button specAppendButton;
        private System.Windows.Forms.TextBox extendsTextBox;
        private System.Windows.Forms.Label extendsLabel;
        private System.Windows.Forms.Button specRemoveButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.CheckBox deletedCheckBox;
    }
}