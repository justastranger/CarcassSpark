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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.effectsDataGridView = new System.Windows.Forms.DataGridView();
            this.effectId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.effectsAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.excludesOnEndingListBox = new System.Windows.Forms.ListBox();
            this.startingVerbIdTextBox = new System.Windows.Forms.TextBox();
            this.closeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.effectsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 100);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(118, 12);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(100, 20);
            this.idTextBox.TabIndex = 1;
            this.idTextBox.Text = "ID";
            this.idTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelTextBox
            // 
            this.labelTextBox.Location = new System.Drawing.Point(224, 12);
            this.labelTextBox.Name = "labelTextBox";
            this.labelTextBox.Size = new System.Drawing.Size(100, 20);
            this.labelTextBox.TabIndex = 2;
            this.labelTextBox.Text = "Label";
            this.labelTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(118, 38);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descriptionTextBox.Size = new System.Drawing.Size(206, 74);
            this.descriptionTextBox.TabIndex = 3;
            this.descriptionTextBox.Text = "Description";
            // 
            // imageTextBox
            // 
            this.imageTextBox.Location = new System.Drawing.Point(12, 118);
            this.imageTextBox.Name = "imageTextBox";
            this.imageTextBox.Size = new System.Drawing.Size(100, 20);
            this.imageTextBox.TabIndex = 4;
            this.imageTextBox.Text = "Image";
            this.imageTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // fromEndingTextBox
            // 
            this.fromEndingTextBox.Location = new System.Drawing.Point(12, 144);
            this.fromEndingTextBox.Name = "fromEndingTextBox";
            this.fromEndingTextBox.Size = new System.Drawing.Size(100, 20);
            this.fromEndingTextBox.TabIndex = 5;
            this.fromEndingTextBox.Text = "From Ending";
            this.fromEndingTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // startdescriptionTextBox
            // 
            this.startdescriptionTextBox.Location = new System.Drawing.Point(118, 118);
            this.startdescriptionTextBox.Multiline = true;
            this.startdescriptionTextBox.Name = "startdescriptionTextBox";
            this.startdescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.startdescriptionTextBox.Size = new System.Drawing.Size(206, 46);
            this.startdescriptionTextBox.TabIndex = 6;
            this.startdescriptionTextBox.Text = "Start Description";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 170);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(178, 17);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Available Without Ending Match";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // effectsDataGridView
            // 
            this.effectsDataGridView.AllowUserToAddRows = false;
            this.effectsDataGridView.AllowUserToDeleteRows = false;
            this.effectsDataGridView.AllowUserToResizeColumns = false;
            this.effectsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.effectsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.effectId,
            this.effectsAmount});
            this.effectsDataGridView.Location = new System.Drawing.Point(12, 193);
            this.effectsDataGridView.Name = "effectsDataGridView";
            this.effectsDataGridView.ReadOnly = true;
            this.effectsDataGridView.Size = new System.Drawing.Size(312, 103);
            this.effectsDataGridView.TabIndex = 8;
            this.effectsDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.effectsDataGridView_CellDoubleClick);
            // 
            // effectId
            // 
            this.effectId.HeaderText = "Starting Card";
            this.effectId.Name = "effectId";
            this.effectId.ReadOnly = true;
            this.effectId.Width = 135;
            // 
            // effectsAmount
            // 
            this.effectsAmount.HeaderText = "Amount";
            this.effectsAmount.Name = "effectsAmount";
            this.effectsAmount.ReadOnly = true;
            this.effectsAmount.Width = 134;
            // 
            // excludesOnEndingListBox
            // 
            this.excludesOnEndingListBox.FormattingEnabled = true;
            this.excludesOnEndingListBox.Location = new System.Drawing.Point(118, 305);
            this.excludesOnEndingListBox.Name = "excludesOnEndingListBox";
            this.excludesOnEndingListBox.ScrollAlwaysVisible = true;
            this.excludesOnEndingListBox.Size = new System.Drawing.Size(206, 95);
            this.excludesOnEndingListBox.TabIndex = 9;
            this.excludesOnEndingListBox.DoubleClick += new System.EventHandler(this.excludesOnEndingListBox_DoubleClick);
            // 
            // startingVerbIdTextBox
            // 
            this.startingVerbIdTextBox.Location = new System.Drawing.Point(12, 305);
            this.startingVerbIdTextBox.Name = "startingVerbIdTextBox";
            this.startingVerbIdTextBox.Size = new System.Drawing.Size(100, 20);
            this.startingVerbIdTextBox.TabIndex = 10;
            this.startingVerbIdTextBox.Text = "Starting Verb";
            this.startingVerbIdTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(12, 331);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(100, 69);
            this.closeButton.TabIndex = 11;
            this.closeButton.Text = "OK";
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // LegacyViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 412);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.startingVerbIdTextBox);
            this.Controls.Add(this.excludesOnEndingListBox);
            this.Controls.Add(this.effectsDataGridView);
            this.Controls.Add(this.checkBox1);
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
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.DataGridView effectsDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn effectId;
        private System.Windows.Forms.DataGridViewTextBoxColumn effectsAmount;
        private System.Windows.Forms.ListBox excludesOnEndingListBox;
        private System.Windows.Forms.TextBox startingVerbIdTextBox;
        private System.Windows.Forms.Button closeButton;
    }
}