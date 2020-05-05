namespace Cultist_Simulator_Modding_Toolkit
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
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.labelTextBox = new System.Windows.Forms.TextBox();
            this.commentsTextBox = new System.Windows.Forms.TextBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.specListBox = new System.Windows.Forms.ListBox();
            this.drawmessagesDataGridView = new System.Windows.Forms.DataGridView();
            this.elementId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resetOnExhaustionCheckBox = new System.Windows.Forms.CheckBox();
            this.defaultCardTextBox = new System.Windows.Forms.TextBox();
            this.drawsTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.drawmessagesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(12, 12);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(125, 20);
            this.idTextBox.TabIndex = 0;
            this.idTextBox.Text = "ID";
            this.idTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelTextBox
            // 
            this.labelTextBox.Location = new System.Drawing.Point(155, 12);
            this.labelTextBox.Name = "labelTextBox";
            this.labelTextBox.Size = new System.Drawing.Size(125, 20);
            this.labelTextBox.TabIndex = 1;
            this.labelTextBox.Text = "Label";
            this.labelTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // commentsTextBox
            // 
            this.commentsTextBox.Location = new System.Drawing.Point(286, 12);
            this.commentsTextBox.Name = "commentsTextBox";
            this.commentsTextBox.Size = new System.Drawing.Size(260, 20);
            this.commentsTextBox.TabIndex = 2;
            this.commentsTextBox.Text = "Comments";
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(12, 38);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descriptionTextBox.Size = new System.Drawing.Size(268, 82);
            this.descriptionTextBox.TabIndex = 3;
            this.descriptionTextBox.Text = "Description";
            // 
            // specListBox
            // 
            this.specListBox.FormattingEnabled = true;
            this.specListBox.Location = new System.Drawing.Point(286, 38);
            this.specListBox.Name = "specListBox";
            this.specListBox.ScrollAlwaysVisible = true;
            this.specListBox.Size = new System.Drawing.Size(260, 238);
            this.specListBox.TabIndex = 4;
            this.specListBox.DoubleClick += new System.EventHandler(this.specListBox_DoubleClick);
            // 
            // drawmessagesDataGridView
            // 
            this.drawmessagesDataGridView.AllowUserToAddRows = false;
            this.drawmessagesDataGridView.AllowUserToDeleteRows = false;
            this.drawmessagesDataGridView.AllowUserToResizeColumns = false;
            this.drawmessagesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.drawmessagesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.elementId,
            this.message});
            this.drawmessagesDataGridView.Location = new System.Drawing.Point(12, 149);
            this.drawmessagesDataGridView.Name = "drawmessagesDataGridView";
            this.drawmessagesDataGridView.Size = new System.Drawing.Size(268, 153);
            this.drawmessagesDataGridView.TabIndex = 5;
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
            this.resetOnExhaustionCheckBox.Location = new System.Drawing.Point(165, 126);
            this.resetOnExhaustionCheckBox.Name = "resetOnExhaustionCheckBox";
            this.resetOnExhaustionCheckBox.Size = new System.Drawing.Size(115, 17);
            this.resetOnExhaustionCheckBox.TabIndex = 6;
            this.resetOnExhaustionCheckBox.Text = "resetOnExhaustion";
            this.resetOnExhaustionCheckBox.UseVisualStyleBackColor = true;
            // 
            // defaultCardTextBox
            // 
            this.defaultCardTextBox.Location = new System.Drawing.Point(286, 282);
            this.defaultCardTextBox.Name = "defaultCardTextBox";
            this.defaultCardTextBox.Size = new System.Drawing.Size(260, 20);
            this.defaultCardTextBox.TabIndex = 7;
            this.defaultCardTextBox.Text = "Default Card";
            this.defaultCardTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // drawsTextBox
            // 
            this.drawsTextBox.Location = new System.Drawing.Point(12, 124);
            this.drawsTextBox.Name = "drawsTextBox";
            this.drawsTextBox.Size = new System.Drawing.Size(100, 20);
            this.drawsTextBox.TabIndex = 8;
            this.drawsTextBox.Text = "Draws";
            this.drawsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DeckViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 314);
            this.Controls.Add(this.drawsTextBox);
            this.Controls.Add(this.defaultCardTextBox);
            this.Controls.Add(this.resetOnExhaustionCheckBox);
            this.Controls.Add(this.drawmessagesDataGridView);
            this.Controls.Add(this.specListBox);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.commentsTextBox);
            this.Controls.Add(this.labelTextBox);
            this.Controls.Add(this.idTextBox);
            this.Name = "DeckViewer";
            this.Text = "DeckViewer";
            ((System.ComponentModel.ISupportInitialize)(this.drawmessagesDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.TextBox labelTextBox;
        private System.Windows.Forms.TextBox commentsTextBox;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.ListBox specListBox;
        private System.Windows.Forms.DataGridView drawmessagesDataGridView;
        private System.Windows.Forms.CheckBox resetOnExhaustionCheckBox;
        private System.Windows.Forms.TextBox defaultCardTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn elementId;
        private System.Windows.Forms.DataGridViewTextBoxColumn message;
        private System.Windows.Forms.TextBox drawsTextBox;
    }
}