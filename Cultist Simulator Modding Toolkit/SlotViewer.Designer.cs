namespace Cultist_Simulator_Modding_Toolkit
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
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.labelTextBox = new System.Windows.Forms.TextBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.actionIdTextBox = new System.Windows.Forms.TextBox();
            this.requiredDataGridView = new System.Windows.Forms.DataGridView();
            this.requiredId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.requiredAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.forbiddenDataGridView = new System.Windows.Forms.DataGridView();
            this.forbiddenId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.forbiddenAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.greedyCheckBox = new System.Windows.Forms.CheckBox();
            this.requiredLabel = new System.Windows.Forms.Label();
            this.forbiddenLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.requiredDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.forbiddenDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(12, 12);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(100, 20);
            this.idTextBox.TabIndex = 0;
            this.idTextBox.Text = "ID";
            this.idTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelTextBox
            // 
            this.labelTextBox.Location = new System.Drawing.Point(172, 12);
            this.labelTextBox.Name = "labelTextBox";
            this.labelTextBox.Size = new System.Drawing.Size(100, 20);
            this.labelTextBox.TabIndex = 1;
            this.labelTextBox.Text = "Label";
            this.labelTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(12, 38);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descriptionTextBox.Size = new System.Drawing.Size(260, 75);
            this.descriptionTextBox.TabIndex = 2;
            this.descriptionTextBox.Text = "Description";
            // 
            // actionIdTextBox
            // 
            this.actionIdTextBox.Location = new System.Drawing.Point(12, 119);
            this.actionIdTextBox.Name = "actionIdTextBox";
            this.actionIdTextBox.Size = new System.Drawing.Size(127, 20);
            this.actionIdTextBox.TabIndex = 3;
            this.actionIdTextBox.Text = "Verb ID";
            this.actionIdTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // requiredDataGridView
            // 
            this.requiredDataGridView.AllowUserToAddRows = false;
            this.requiredDataGridView.AllowUserToDeleteRows = false;
            this.requiredDataGridView.AllowUserToResizeColumns = false;
            this.requiredDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.requiredDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.requiredId,
            this.requiredAmount});
            this.requiredDataGridView.Location = new System.Drawing.Point(12, 158);
            this.requiredDataGridView.Name = "requiredDataGridView";
            this.requiredDataGridView.ReadOnly = true;
            this.requiredDataGridView.Size = new System.Drawing.Size(260, 100);
            this.requiredDataGridView.TabIndex = 4;
            // 
            // requiredId
            // 
            this.requiredId.HeaderText = "Element ID";
            this.requiredId.Name = "requiredId";
            this.requiredId.ReadOnly = true;
            this.requiredId.Width = 109;
            // 
            // requiredAmount
            // 
            this.requiredAmount.HeaderText = "Amount";
            this.requiredAmount.Name = "requiredAmount";
            this.requiredAmount.ReadOnly = true;
            this.requiredAmount.Width = 108;
            // 
            // forbiddenDataGridView
            // 
            this.forbiddenDataGridView.AllowUserToAddRows = false;
            this.forbiddenDataGridView.AllowUserToDeleteRows = false;
            this.forbiddenDataGridView.AllowUserToResizeColumns = false;
            this.forbiddenDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.forbiddenDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.forbiddenId,
            this.forbiddenAmount});
            this.forbiddenDataGridView.Location = new System.Drawing.Point(12, 277);
            this.forbiddenDataGridView.Name = "forbiddenDataGridView";
            this.forbiddenDataGridView.ReadOnly = true;
            this.forbiddenDataGridView.Size = new System.Drawing.Size(260, 100);
            this.forbiddenDataGridView.TabIndex = 5;
            // 
            // forbiddenId
            // 
            this.forbiddenId.HeaderText = "Element ID";
            this.forbiddenId.Name = "forbiddenId";
            this.forbiddenId.ReadOnly = true;
            this.forbiddenId.Width = 109;
            // 
            // forbiddenAmount
            // 
            this.forbiddenAmount.HeaderText = "Amount";
            this.forbiddenAmount.Name = "forbiddenAmount";
            this.forbiddenAmount.ReadOnly = true;
            this.forbiddenAmount.Width = 108;
            // 
            // greedyCheckBox
            // 
            this.greedyCheckBox.AutoSize = true;
            this.greedyCheckBox.Location = new System.Drawing.Point(192, 121);
            this.greedyCheckBox.Name = "greedyCheckBox";
            this.greedyCheckBox.Size = new System.Drawing.Size(60, 17);
            this.greedyCheckBox.TabIndex = 6;
            this.greedyCheckBox.Text = "Greedy";
            this.greedyCheckBox.UseVisualStyleBackColor = true;
            // 
            // requiredLabel
            // 
            this.requiredLabel.AutoSize = true;
            this.requiredLabel.Location = new System.Drawing.Point(120, 142);
            this.requiredLabel.Name = "requiredLabel";
            this.requiredLabel.Size = new System.Drawing.Size(50, 13);
            this.requiredLabel.TabIndex = 7;
            this.requiredLabel.Text = "Required";
            // 
            // forbiddenLabel
            // 
            this.forbiddenLabel.AutoSize = true;
            this.forbiddenLabel.Location = new System.Drawing.Point(120, 261);
            this.forbiddenLabel.Name = "forbiddenLabel";
            this.forbiddenLabel.Size = new System.Drawing.Size(54, 13);
            this.forbiddenLabel.TabIndex = 8;
            this.forbiddenLabel.Text = "Forbidden";
            // 
            // SlotViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 409);
            this.Controls.Add(this.forbiddenLabel);
            this.Controls.Add(this.requiredLabel);
            this.Controls.Add(this.greedyCheckBox);
            this.Controls.Add(this.forbiddenDataGridView);
            this.Controls.Add(this.requiredDataGridView);
            this.Controls.Add(this.actionIdTextBox);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.labelTextBox);
            this.Controls.Add(this.idTextBox);
            this.Name = "SlotViewer";
            this.Text = "SlotViewer";
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
        private System.Windows.Forms.DataGridView requiredDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn requiredId;
        private System.Windows.Forms.DataGridViewTextBoxColumn requiredAmount;
        private System.Windows.Forms.DataGridView forbiddenDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn forbiddenId;
        private System.Windows.Forms.DataGridViewTextBoxColumn forbiddenAmount;
        private System.Windows.Forms.CheckBox greedyCheckBox;
        private System.Windows.Forms.Label requiredLabel;
        private System.Windows.Forms.Label forbiddenLabel;
    }
}