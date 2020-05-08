namespace Cultist_Simulator_Modding_Toolkit
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
            this.excludesOnEndingListBox = new System.Windows.Forms.ListBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.effectsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(130, 130);
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
            this.descriptionTextBox.Size = new System.Drawing.Size(206, 74);
            this.descriptionTextBox.TabIndex = 3;
            this.descriptionTextBox.TextChanged += new System.EventHandler(this.descriptionTextBox_TextChanged);
            // 
            // imageTextBox
            // 
            this.imageTextBox.Location = new System.Drawing.Point(12, 161);
            this.imageTextBox.Name = "imageTextBox";
            this.imageTextBox.Size = new System.Drawing.Size(130, 20);
            this.imageTextBox.TabIndex = 4;
            this.imageTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.imageTextBox.TextChanged += new System.EventHandler(this.imageTextBox_TextChanged);
            // 
            // fromEndingTextBox
            // 
            this.fromEndingTextBox.Location = new System.Drawing.Point(12, 200);
            this.fromEndingTextBox.Name = "fromEndingTextBox";
            this.fromEndingTextBox.Size = new System.Drawing.Size(130, 20);
            this.fromEndingTextBox.TabIndex = 5;
            this.fromEndingTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.fromEndingTextBox.TextChanged += new System.EventHandler(this.fromEndingTextBox_TextChanged);
            // 
            // startdescriptionTextBox
            // 
            this.startdescriptionTextBox.Location = new System.Drawing.Point(148, 157);
            this.startdescriptionTextBox.Multiline = true;
            this.startdescriptionTextBox.Name = "startdescriptionTextBox";
            this.startdescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.startdescriptionTextBox.Size = new System.Drawing.Size(206, 46);
            this.startdescriptionTextBox.TabIndex = 6;
            this.startdescriptionTextBox.TextChanged += new System.EventHandler(this.startdescriptionTextBox_TextChanged);
            // 
            // availableWithoutEndingMatchCheckBox
            // 
            this.availableWithoutEndingMatchCheckBox.AutoSize = true;
            this.availableWithoutEndingMatchCheckBox.Location = new System.Drawing.Point(12, 226);
            this.availableWithoutEndingMatchCheckBox.Name = "availableWithoutEndingMatchCheckBox";
            this.availableWithoutEndingMatchCheckBox.Size = new System.Drawing.Size(178, 17);
            this.availableWithoutEndingMatchCheckBox.TabIndex = 7;
            this.availableWithoutEndingMatchCheckBox.Text = "Available Without Ending Match";
            this.availableWithoutEndingMatchCheckBox.UseVisualStyleBackColor = true;
            this.availableWithoutEndingMatchCheckBox.CheckedChanged += new System.EventHandler(this.availableWithoutEndingMatch_CheckedChanged);
            // 
            // effectsDataGridView
            // 
            this.effectsDataGridView.AllowUserToResizeColumns = false;
            this.effectsDataGridView.AllowUserToResizeRows = false;
            this.effectsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.effectsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.effectId,
            this.effectsAmount});
            this.effectsDataGridView.Location = new System.Drawing.Point(12, 262);
            this.effectsDataGridView.Name = "effectsDataGridView";
            this.effectsDataGridView.Size = new System.Drawing.Size(342, 103);
            this.effectsDataGridView.TabIndex = 8;
            this.effectsDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.effectsDataGridView_CellDoubleClick);
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
            // excludesOnEndingListBox
            // 
            this.excludesOnEndingListBox.FormattingEnabled = true;
            this.excludesOnEndingListBox.Location = new System.Drawing.Point(130, 390);
            this.excludesOnEndingListBox.Name = "excludesOnEndingListBox";
            this.excludesOnEndingListBox.ScrollAlwaysVisible = true;
            this.excludesOnEndingListBox.Size = new System.Drawing.Size(225, 56);
            this.excludesOnEndingListBox.TabIndex = 9;
            this.excludesOnEndingListBox.DoubleClick += new System.EventHandler(this.excludesOnEndingListBox_DoubleClick);
            // 
            // startingVerbIdTextBox
            // 
            this.startingVerbIdTextBox.Location = new System.Drawing.Point(226, 226);
            this.startingVerbIdTextBox.Name = "startingVerbIdTextBox";
            this.startingVerbIdTextBox.Size = new System.Drawing.Size(128, 20);
            this.startingVerbIdTextBox.TabIndex = 10;
            this.startingVerbIdTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.startingVerbIdTextBox.TextChanged += new System.EventHandler(this.startingVerbIdTextBox_TextChanged);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(12, 433);
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
            this.idLabel.Location = new System.Drawing.Point(145, 12);
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
            this.startDescriptionLabel.Location = new System.Drawing.Point(145, 141);
            this.startDescriptionLabel.Name = "startDescriptionLabel";
            this.startDescriptionLabel.Size = new System.Drawing.Size(85, 13);
            this.startDescriptionLabel.TabIndex = 15;
            this.startDescriptionLabel.Text = "Start Description";
            // 
            // imageLabel
            // 
            this.imageLabel.AutoSize = true;
            this.imageLabel.Location = new System.Drawing.Point(12, 145);
            this.imageLabel.Name = "imageLabel";
            this.imageLabel.Size = new System.Drawing.Size(36, 13);
            this.imageLabel.TabIndex = 16;
            this.imageLabel.Text = "Image";
            // 
            // fromEndingLabel
            // 
            this.fromEndingLabel.AutoSize = true;
            this.fromEndingLabel.Location = new System.Drawing.Point(9, 184);
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
            this.excludesOnEndingLabel.Location = new System.Drawing.Point(53, 368);
            this.excludesOnEndingLabel.Name = "excludesOnEndingLabel";
            this.excludesOnEndingLabel.Size = new System.Drawing.Size(302, 13);
            this.excludesOnEndingLabel.TabIndex = 19;
            this.excludesOnEndingLabel.Text = "Exclude This Legacy After Completing One Of These Legacies";
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(12, 464);
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
            this.startingVerbLabel.Location = new System.Drawing.Point(223, 207);
            this.startingVerbLabel.Name = "startingVerbLabel";
            this.startingVerbLabel.Size = new System.Drawing.Size(68, 13);
            this.startingVerbLabel.TabIndex = 21;
            this.startingVerbLabel.Text = "Starting Verb";
            // 
            // addExcludesTextBox
            // 
            this.addExcludesTextBox.Location = new System.Drawing.Point(130, 468);
            this.addExcludesTextBox.Name = "addExcludesTextBox";
            this.addExcludesTextBox.Size = new System.Drawing.Size(87, 20);
            this.addExcludesTextBox.TabIndex = 22;
            this.addExcludesTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.addExcludesTextBox_KeyDown);
            // 
            // addExcludesButton
            // 
            this.addExcludesButton.Location = new System.Drawing.Point(241, 452);
            this.addExcludesButton.Name = "addExcludesButton";
            this.addExcludesButton.Size = new System.Drawing.Size(113, 35);
            this.addExcludesButton.TabIndex = 23;
            this.addExcludesButton.Text = "Add to excludesOnEnding";
            this.addExcludesButton.UseVisualStyleBackColor = true;
            this.addExcludesButton.Click += new System.EventHandler(this.addExcludesButton_Click);
            // 
            // excludeAddLabel
            // 
            this.excludeAddLabel.AutoSize = true;
            this.excludeAddLabel.Location = new System.Drawing.Point(127, 452);
            this.excludeAddLabel.Name = "excludeAddLabel";
            this.excludeAddLabel.Size = new System.Drawing.Size(109, 13);
            this.excludeAddLabel.TabIndex = 24;
            this.excludeAddLabel.Text = "Legacy ID to Exclude";
            // 
            // LegacyViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 499);
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
            this.Controls.Add(this.excludesOnEndingListBox);
            this.Controls.Add(this.effectsDataGridView);
            this.Controls.Add(this.availableWithoutEndingMatchCheckBox);
            this.Controls.Add(this.startdescriptionTextBox);
            this.Controls.Add(this.fromEndingTextBox);
            this.Controls.Add(this.imageTextBox);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.labelTextBox);
            this.Controls.Add(this.idTextBox);
            this.Controls.Add(this.pictureBox1);
            this.Name = "LegacyViewer";
            this.Text = "LegacyViewer";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.effectsDataGridView)).EndInit();
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
        private System.Windows.Forms.ListBox excludesOnEndingListBox;
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
    }
}