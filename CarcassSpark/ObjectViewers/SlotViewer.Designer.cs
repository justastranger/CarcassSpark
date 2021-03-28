namespace CarcassSpark.ObjectViewers
{
    partial class SlotViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SlotViewer));
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.labelTextBox = new System.Windows.Forms.TextBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.actionIdTextBox = new System.Windows.Forms.TextBox();
            this.greedyCheckBox = new System.Windows.Forms.CheckBox();
            this.requiredLabel = new System.Windows.Forms.Label();
            this.forbiddenLabel = new System.Windows.Forms.Label();
            this.actionIdLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.idLabel = new System.Windows.Forms.Label();
            this.labelLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.requiredDataGridView = new System.Windows.Forms.DataGridView();
            this.requiredId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.requiredAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.forbiddenDataGridView = new System.Windows.Forms.DataGridView();
            this.forbiddenId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.forbiddenAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.consumesCheckBox = new System.Windows.Forms.CheckBox();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.requiredDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.forbiddenDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(12, 25);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(100, 20);
            this.idTextBox.TabIndex = 0;
            this.idTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.idTextBox, "Internal name for the slot. Only used to differentiate the slots and is not refer" +
        "enced anywhere.");
            this.idTextBox.TextChanged += new System.EventHandler(this.IdTextBox_TextChanged);
            // 
            // labelTextBox
            // 
            this.labelTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTextBox.Location = new System.Drawing.Point(123, 25);
            this.labelTextBox.Name = "labelTextBox";
            this.labelTextBox.Size = new System.Drawing.Size(149, 20);
            this.labelTextBox.TabIndex = 1;
            this.labelTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.labelTextBox, "Label to display above the slot and in the details panel.");
            this.labelTextBox.TextChanged += new System.EventHandler(this.LabelTextBox_TextChanged);
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.AcceptsReturn = true;
            this.descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionTextBox.Location = new System.Drawing.Point(12, 64);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descriptionTextBox.Size = new System.Drawing.Size(260, 75);
            this.descriptionTextBox.TabIndex = 2;
            this.ToolTip.SetToolTip(this.descriptionTextBox, "Description for the slot to be displayed in its details panel.");
            this.descriptionTextBox.TextChanged += new System.EventHandler(this.DescriptionTextBox_TextChanged);
            // 
            // actionIdTextBox
            // 
            this.actionIdTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.actionIdTextBox.Location = new System.Drawing.Point(12, 158);
            this.actionIdTextBox.Name = "actionIdTextBox";
            this.actionIdTextBox.Size = new System.Drawing.Size(174, 20);
            this.actionIdTextBox.TabIndex = 3;
            this.actionIdTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.actionIdTextBox, "ID of the verb that this slot will only appear in.");
            this.actionIdTextBox.Visible = false;
            this.actionIdTextBox.TextChanged += new System.EventHandler(this.ActionIdTextBox_TextChanged);
            // 
            // greedyCheckBox
            // 
            this.greedyCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.greedyCheckBox.AutoSize = true;
            this.greedyCheckBox.Location = new System.Drawing.Point(192, 168);
            this.greedyCheckBox.Name = "greedyCheckBox";
            this.greedyCheckBox.Size = new System.Drawing.Size(60, 17);
            this.greedyCheckBox.TabIndex = 6;
            this.greedyCheckBox.Text = "Greedy";
            this.ToolTip.SetToolTip(this.greedyCheckBox, "When true, this slot will suck in the first card that fits the slot criteria.\r\nPl" +
        "ayers lose control of any cards grabbed by the greedy slot and can not choose wh" +
        "ich card gets selected.");
            this.greedyCheckBox.UseVisualStyleBackColor = true;
            this.greedyCheckBox.Visible = false;
            this.greedyCheckBox.CheckedChanged += new System.EventHandler(this.GreedyCheckBox_CheckedChanged);
            // 
            // requiredLabel
            // 
            this.requiredLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.requiredLabel.AutoSize = true;
            this.requiredLabel.Location = new System.Drawing.Point(120, 181);
            this.requiredLabel.Name = "requiredLabel";
            this.requiredLabel.Size = new System.Drawing.Size(50, 13);
            this.requiredLabel.TabIndex = 7;
            this.requiredLabel.Text = "Required";
            // 
            // forbiddenLabel
            // 
            this.forbiddenLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.forbiddenLabel.AutoSize = true;
            this.forbiddenLabel.Location = new System.Drawing.Point(120, 300);
            this.forbiddenLabel.Name = "forbiddenLabel";
            this.forbiddenLabel.Size = new System.Drawing.Size(54, 13);
            this.forbiddenLabel.TabIndex = 8;
            this.forbiddenLabel.Text = "Forbidden";
            // 
            // actionIdLabel
            // 
            this.actionIdLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.actionIdLabel.AutoSize = true;
            this.actionIdLabel.Location = new System.Drawing.Point(12, 142);
            this.actionIdLabel.Name = "actionIdLabel";
            this.actionIdLabel.Size = new System.Drawing.Size(43, 13);
            this.actionIdLabel.TabIndex = 9;
            this.actionIdLabel.Text = "Verb ID";
            this.actionIdLabel.Visible = false;
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(12, 48);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(60, 13);
            this.descriptionLabel.TabIndex = 10;
            this.descriptionLabel.Text = "Description";
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Location = new System.Drawing.Point(12, 9);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(39, 13);
            this.idLabel.TabIndex = 11;
            this.idLabel.Text = "Slot ID";
            // 
            // labelLabel
            // 
            this.labelLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLabel.AutoSize = true;
            this.labelLabel.Location = new System.Drawing.Point(218, 9);
            this.labelLabel.Name = "labelLabel";
            this.labelLabel.Size = new System.Drawing.Size(54, 13);
            this.labelLabel.TabIndex = 12;
            this.labelLabel.Text = "Slot Label";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(12, 431);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 13;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(197, 431);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 14;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // requiredDataGridView
            // 
            this.requiredDataGridView.AllowUserToResizeColumns = false;
            this.requiredDataGridView.AllowUserToResizeRows = false;
            this.requiredDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.requiredDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.requiredDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.requiredDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.requiredId,
            this.requiredAmount});
            this.requiredDataGridView.Location = new System.Drawing.Point(12, 197);
            this.requiredDataGridView.Name = "requiredDataGridView";
            this.requiredDataGridView.Size = new System.Drawing.Size(260, 100);
            this.requiredDataGridView.TabIndex = 15;
            this.ToolTip.SetToolTip(this.requiredDataGridView, resources.GetString("requiredDataGridView.ToolTip"));
            this.requiredDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RequiredDataGridView_CellDoubleClick);
            this.requiredDataGridView.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.RequiredDataGridView_UserDeletedRow);
            // 
            // requiredId
            // 
            this.requiredId.HeaderText = "Required ID";
            this.requiredId.Name = "requiredId";
            // 
            // requiredAmount
            // 
            this.requiredAmount.HeaderText = "Amount";
            this.requiredAmount.Name = "requiredAmount";
            // 
            // forbiddenDataGridView
            // 
            this.forbiddenDataGridView.AllowUserToResizeColumns = false;
            this.forbiddenDataGridView.AllowUserToResizeRows = false;
            this.forbiddenDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.forbiddenDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.forbiddenDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.forbiddenDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.forbiddenId,
            this.forbiddenAmount});
            this.forbiddenDataGridView.Location = new System.Drawing.Point(12, 316);
            this.forbiddenDataGridView.Name = "forbiddenDataGridView";
            this.forbiddenDataGridView.Size = new System.Drawing.Size(260, 109);
            this.forbiddenDataGridView.TabIndex = 16;
            this.ToolTip.SetToolTip(this.forbiddenDataGridView, "Cards can not satisfy any of the conditions below.\r\nNegative values indicate a \"l" +
        "ess than\" operator, allowing only cards with at least that much of an aspect.");
            this.forbiddenDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ForbiddenDataGridView_CellDoubleClick);
            this.forbiddenDataGridView.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.ForbiddenDataGridView_UserDeletedRow);
            // 
            // forbiddenId
            // 
            this.forbiddenId.HeaderText = "Forbidden ID";
            this.forbiddenId.Name = "forbiddenId";
            // 
            // forbiddenAmount
            // 
            this.forbiddenAmount.HeaderText = "Amount";
            this.forbiddenAmount.Name = "forbiddenAmount";
            // 
            // consumesCheckBox
            // 
            this.consumesCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.consumesCheckBox.AutoSize = true;
            this.consumesCheckBox.Location = new System.Drawing.Point(192, 145);
            this.consumesCheckBox.Name = "consumesCheckBox";
            this.consumesCheckBox.Size = new System.Drawing.Size(75, 17);
            this.consumesCheckBox.TabIndex = 17;
            this.consumesCheckBox.Text = "Consumes";
            this.ToolTip.SetToolTip(this.consumesCheckBox, "When true, any card placed in the slot will be deleted at the conclusion of the r" +
        "ecipe.");
            this.consumesCheckBox.UseVisualStyleBackColor = true;
            this.consumesCheckBox.Visible = false;
            this.consumesCheckBox.CheckedChanged += new System.EventHandler(this.ConsumesCheckBox_CheckedChanged);
            // 
            // SlotViewer
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(284, 466);
            this.Controls.Add(this.consumesCheckBox);
            this.Controls.Add(this.forbiddenDataGridView);
            this.Controls.Add(this.requiredDataGridView);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.labelLabel);
            this.Controls.Add(this.idLabel);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.actionIdLabel);
            this.Controls.Add(this.forbiddenLabel);
            this.Controls.Add(this.requiredLabel);
            this.Controls.Add(this.greedyCheckBox);
            this.Controls.Add(this.actionIdTextBox);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.labelTextBox);
            this.Controls.Add(this.idTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(300, 505);
            this.Name = "SlotViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Slot Viewer";
            this.Shown += new System.EventHandler(this.SlotViewer_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.requiredDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.forbiddenDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.TextBox labelTextBox;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.TextBox actionIdTextBox;
        private System.Windows.Forms.CheckBox greedyCheckBox;
        private System.Windows.Forms.Label requiredLabel;
        private System.Windows.Forms.Label forbiddenLabel;
        private System.Windows.Forms.Label actionIdLabel;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label labelLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.DataGridView requiredDataGridView;
        private System.Windows.Forms.DataGridView forbiddenDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn requiredId;
        private System.Windows.Forms.DataGridViewTextBoxColumn requiredAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn forbiddenId;
        private System.Windows.Forms.DataGridViewTextBoxColumn forbiddenAmount;
        private System.Windows.Forms.CheckBox consumesCheckBox;
        private System.Windows.Forms.ToolTip ToolTip;
    }
}