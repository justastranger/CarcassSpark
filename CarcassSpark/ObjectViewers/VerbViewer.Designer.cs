namespace CarcassSpark.ObjectViewers
{
    partial class VerbViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VerbViewer));
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.labelTextBox = new System.Windows.Forms.TextBox();
            this.atStartCheckBox = new System.Windows.Forms.CheckBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.idLabel = new System.Windows.Forms.Label();
            this.labelLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.slotsLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.addSlotButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.slotsListView = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(149, 25);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(127, 20);
            this.idTextBox.TabIndex = 0;
            this.idTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.idTextBox.TextChanged += new System.EventHandler(this.idTextBox_TextChanged);
            // 
            // labelTextBox
            // 
            this.labelTextBox.Location = new System.Drawing.Point(149, 64);
            this.labelTextBox.Name = "labelTextBox";
            this.labelTextBox.Size = new System.Drawing.Size(127, 20);
            this.labelTextBox.TabIndex = 1;
            this.labelTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.labelTextBox.TextChanged += new System.EventHandler(this.labelTextBox_TextChanged);
            // 
            // atStartCheckBox
            // 
            this.atStartCheckBox.AutoSize = true;
            this.atStartCheckBox.Enabled = false;
            this.atStartCheckBox.Location = new System.Drawing.Point(149, 90);
            this.atStartCheckBox.Name = "atStartCheckBox";
            this.atStartCheckBox.Size = new System.Drawing.Size(165, 17);
            this.atStartCheckBox.TabIndex = 2;
            this.atStartCheckBox.Text = "Start new game with this verb";
            this.atStartCheckBox.ThreeState = true;
            this.atStartCheckBox.UseVisualStyleBackColor = true;
            this.atStartCheckBox.CheckStateChanged += new System.EventHandler(this.atStartCheckBox_CheckStateChanged);
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(12, 170);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(330, 87);
            this.descriptionTextBox.TabIndex = 3;
            this.descriptionTextBox.Text = "Description";
            this.descriptionTextBox.TextChanged += new System.EventHandler(this.descriptionTextBox_TextChanged);
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Location = new System.Drawing.Point(146, 9);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(43, 13);
            this.idLabel.TabIndex = 5;
            this.idLabel.Text = "Verb ID";
            // 
            // labelLabel
            // 
            this.labelLabel.AutoSize = true;
            this.labelLabel.Location = new System.Drawing.Point(146, 48);
            this.labelLabel.Name = "labelLabel";
            this.labelLabel.Size = new System.Drawing.Size(33, 13);
            this.labelLabel.TabIndex = 6;
            this.labelLabel.Text = "Label";
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(9, 154);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(60, 13);
            this.descriptionLabel.TabIndex = 7;
            this.descriptionLabel.Text = "Description";
            // 
            // slotsLabel
            // 
            this.slotsLabel.AutoSize = true;
            this.slotsLabel.Location = new System.Drawing.Point(9, 260);
            this.slotsLabel.Name = "slotsLabel";
            this.slotsLabel.Size = new System.Drawing.Size(30, 13);
            this.slotsLabel.TabIndex = 8;
            this.slotsLabel.Text = "Slots";
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(12, 377);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 31);
            this.okButton.TabIndex = 9;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // addSlotButton
            // 
            this.addSlotButton.Location = new System.Drawing.Point(93, 377);
            this.addSlotButton.Name = "addSlotButton";
            this.addSlotButton.Size = new System.Drawing.Size(81, 31);
            this.addSlotButton.TabIndex = 10;
            this.addSlotButton.Text = "Add Slot";
            this.addSlotButton.UseVisualStyleBackColor = true;
            this.addSlotButton.Click += new System.EventHandler(this.addSlotButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(267, 377);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 31);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(180, 377);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(81, 31);
            this.removeButton.TabIndex = 12;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 128);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // slotsListView
            // 
            this.slotsListView.Location = new System.Drawing.Point(12, 276);
            this.slotsListView.MultiSelect = false;
            this.slotsListView.Name = "slotsListView";
            this.slotsListView.Size = new System.Drawing.Size(330, 95);
            this.slotsListView.TabIndex = 14;
            this.slotsListView.UseCompatibleStateImageBehavior = false;
            this.slotsListView.View = System.Windows.Forms.View.List;
            this.slotsListView.DoubleClick += new System.EventHandler(this.slotsListBox_DoubleClick);
            // 
            // VerbViewer
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 420);
            this.Controls.Add(this.slotsListView);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.addSlotButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.slotsLabel);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.labelLabel);
            this.Controls.Add(this.idLabel);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.atStartCheckBox);
            this.Controls.Add(this.labelTextBox);
            this.Controls.Add(this.idTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VerbViewer";
            this.Text = "VerbViewer";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.TextBox labelTextBox;
        private System.Windows.Forms.CheckBox atStartCheckBox;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label labelLabel;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.Label slotsLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button addSlotButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListView slotsListView;
    }
}