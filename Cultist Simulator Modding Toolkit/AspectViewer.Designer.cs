namespace Cultist_Simulator_Modding_Toolkit
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
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.labelTextBox = new System.Windows.Forms.TextBox();
            this.iconTextBox = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.inducesLabel = new System.Windows.Forms.Label();
            this.inducesDataGridView = new System.Windows.Forms.DataGridView();
            this.recipeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inducesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(118, 12);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(154, 20);
            this.idTextBox.TabIndex = 0;
            this.idTextBox.Text = "ID";
            this.idTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelTextBox
            // 
            this.labelTextBox.Location = new System.Drawing.Point(118, 38);
            this.labelTextBox.Name = "labelTextBox";
            this.labelTextBox.Size = new System.Drawing.Size(154, 20);
            this.labelTextBox.TabIndex = 1;
            this.labelTextBox.Text = "Label";
            this.labelTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // iconTextBox
            // 
            this.iconTextBox.Location = new System.Drawing.Point(118, 64);
            this.iconTextBox.Name = "iconTextBox";
            this.iconTextBox.Size = new System.Drawing.Size(154, 20);
            this.iconTextBox.TabIndex = 2;
            this.iconTextBox.Text = "Icon";
            this.iconTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 100);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(12, 118);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(260, 131);
            this.descriptionTextBox.TabIndex = 4;
            this.descriptionTextBox.Text = "Description";
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
            this.inducesDataGridView.AllowUserToAddRows = false;
            this.inducesDataGridView.AllowUserToDeleteRows = false;
            this.inducesDataGridView.AllowUserToResizeColumns = false;
            this.inducesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.inducesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.recipeId,
            this.chance});
            this.inducesDataGridView.Location = new System.Drawing.Point(278, 31);
            this.inducesDataGridView.Name = "inducesDataGridView";
            this.inducesDataGridView.ReadOnly = true;
            this.inducesDataGridView.Size = new System.Drawing.Size(217, 218);
            this.inducesDataGridView.TabIndex = 6;
            // 
            // recipeId
            // 
            this.recipeId.HeaderText = "Recipe ID";
            this.recipeId.Name = "recipeId";
            this.recipeId.ReadOnly = true;
            this.recipeId.Width = 87;
            // 
            // chance
            // 
            this.chance.HeaderText = "Chance";
            this.chance.Name = "chance";
            this.chance.ReadOnly = true;
            this.chance.Width = 87;
            // 
            // AspectViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 261);
            this.Controls.Add(this.inducesDataGridView);
            this.Controls.Add(this.inducesLabel);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.iconTextBox);
            this.Controls.Add(this.labelTextBox);
            this.Controls.Add(this.idTextBox);
            this.Name = "AspectViewer";
            this.Text = "Aspect Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inducesDataGridView)).EndInit();
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
    }
}