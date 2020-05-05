namespace Cultist_Simulator_Modding_Toolkit
{
    partial class RecipeLinkViewer
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
            this.chanceNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.additionalCheckBox = new System.Windows.Forms.CheckBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.aspectId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isBaseOrNull = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.challengesLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chanceNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // idTextBox
            // 
            this.idTextBox.Enabled = false;
            this.idTextBox.Location = new System.Drawing.Point(12, 12);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(175, 20);
            this.idTextBox.TabIndex = 0;
            this.idTextBox.Text = "Linked Recipe ID";
            this.idTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chanceNumericUpDown
            // 
            this.chanceNumericUpDown.Enabled = false;
            this.chanceNumericUpDown.Location = new System.Drawing.Point(193, 12);
            this.chanceNumericUpDown.Name = "chanceNumericUpDown";
            this.chanceNumericUpDown.Size = new System.Drawing.Size(175, 20);
            this.chanceNumericUpDown.TabIndex = 1;
            // 
            // additionalCheckBox
            // 
            this.additionalCheckBox.AutoSize = true;
            this.additionalCheckBox.Enabled = false;
            this.additionalCheckBox.Location = new System.Drawing.Point(249, 42);
            this.additionalCheckBox.Name = "additionalCheckBox";
            this.additionalCheckBox.Size = new System.Drawing.Size(72, 17);
            this.additionalCheckBox.TabIndex = 2;
            this.additionalCheckBox.Text = "Additional";
            this.additionalCheckBox.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.aspectId,
            this.isBaseOrNull});
            this.dataGridView1.Location = new System.Drawing.Point(12, 67);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(356, 182);
            this.dataGridView1.TabIndex = 3;
            // 
            // aspectId
            // 
            this.aspectId.HeaderText = "Aspect ID";
            this.aspectId.Name = "aspectId";
            this.aspectId.ReadOnly = true;
            this.aspectId.Width = 157;
            // 
            // isBaseOrNull
            // 
            this.isBaseOrNull.HeaderText = "Base";
            this.isBaseOrNull.Name = "isBaseOrNull";
            this.isBaseOrNull.ReadOnly = true;
            this.isBaseOrNull.Width = 156;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(46, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Follow Recipe";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // challengesLabel
            // 
            this.challengesLabel.AutoSize = true;
            this.challengesLabel.Location = new System.Drawing.Point(167, 43);
            this.challengesLabel.Name = "challengesLabel";
            this.challengesLabel.Size = new System.Drawing.Size(59, 13);
            this.challengesLabel.TabIndex = 5;
            this.challengesLabel.Text = "Challenges";
            // 
            // RecipeLinkViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 261);
            this.Controls.Add(this.challengesLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.additionalCheckBox);
            this.Controls.Add(this.chanceNumericUpDown);
            this.Controls.Add(this.idTextBox);
            this.Name = "RecipeLinkViewer";
            this.Text = "RecipeLinkViewer";
            ((System.ComponentModel.ISupportInitialize)(this.chanceNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.NumericUpDown chanceNumericUpDown;
        private System.Windows.Forms.CheckBox additionalCheckBox;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn aspectId;
        private System.Windows.Forms.DataGridViewTextBoxColumn isBaseOrNull;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label challengesLabel;
    }
}