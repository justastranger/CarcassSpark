namespace CarcassSpark.ObjectViewers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecipeLinkViewer));
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.chanceNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.additionalCheckBox = new System.Windows.Forms.CheckBox();
            this.challengesDataGridView = new System.Windows.Forms.DataGridView();
            this.aspectId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isBaseOrNull = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.openRecipeButton = new System.Windows.Forms.Button();
            this.challengesLabel = new System.Windows.Forms.Label();
            this.linkedLabel = new System.Windows.Forms.Label();
            this.chanceLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.expulsionLabel = new System.Windows.Forms.Label();
            this.expulsionDataGridView = new System.Windows.Forms.DataGridView();
            this.expulsionFilter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expulsionLimit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.chanceNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.challengesDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.expulsionDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(12, 25);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(175, 20);
            this.idTextBox.TabIndex = 0;
            this.idTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.idTextBox.TextChanged += new System.EventHandler(this.idTextBox_TextChanged);
            // 
            // chanceNumericUpDown
            // 
            this.chanceNumericUpDown.Location = new System.Drawing.Point(193, 26);
            this.chanceNumericUpDown.Name = "chanceNumericUpDown";
            this.chanceNumericUpDown.Size = new System.Drawing.Size(175, 20);
            this.chanceNumericUpDown.TabIndex = 1;
            this.chanceNumericUpDown.ValueChanged += new System.EventHandler(this.chanceNumericUpDown_ValueChanged);
            // 
            // additionalCheckBox
            // 
            this.additionalCheckBox.AutoSize = true;
            this.additionalCheckBox.Location = new System.Drawing.Point(235, 56);
            this.additionalCheckBox.Name = "additionalCheckBox";
            this.additionalCheckBox.Size = new System.Drawing.Size(72, 17);
            this.additionalCheckBox.TabIndex = 2;
            this.additionalCheckBox.Text = "Additional";
            this.additionalCheckBox.UseVisualStyleBackColor = true;
            this.additionalCheckBox.CheckedChanged += new System.EventHandler(this.additionalCheckBox_CheckedChanged);
            // 
            // challengesDataGridView
            // 
            this.challengesDataGridView.AllowUserToResizeColumns = false;
            this.challengesDataGridView.AllowUserToResizeRows = false;
            this.challengesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.challengesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.aspectId,
            this.isBaseOrNull});
            this.challengesDataGridView.Location = new System.Drawing.Point(12, 75);
            this.challengesDataGridView.Name = "challengesDataGridView";
            this.challengesDataGridView.Size = new System.Drawing.Size(356, 122);
            this.challengesDataGridView.TabIndex = 3;
            this.challengesDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.challengesDataGridView_CellDoubleClick);
            this.challengesDataGridView.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.challengesDataGridView_UserDeletedRow);
            // 
            // aspectId
            // 
            this.aspectId.HeaderText = "Aspect ID";
            this.aspectId.Name = "aspectId";
            this.aspectId.Width = 157;
            // 
            // isBaseOrNull
            // 
            this.isBaseOrNull.HeaderText = "Base";
            this.isBaseOrNull.Name = "isBaseOrNull";
            this.isBaseOrNull.Width = 156;
            // 
            // openRecipeButton
            // 
            this.openRecipeButton.Location = new System.Drawing.Point(44, 51);
            this.openRecipeButton.Name = "openRecipeButton";
            this.openRecipeButton.Size = new System.Drawing.Size(104, 23);
            this.openRecipeButton.TabIndex = 4;
            this.openRecipeButton.Text = "Follow Recipe";
            this.openRecipeButton.UseVisualStyleBackColor = true;
            this.openRecipeButton.Click += new System.EventHandler(this.openRecipeButton_Click);
            // 
            // challengesLabel
            // 
            this.challengesLabel.AutoSize = true;
            this.challengesLabel.Location = new System.Drawing.Point(154, 56);
            this.challengesLabel.Name = "challengesLabel";
            this.challengesLabel.Size = new System.Drawing.Size(59, 13);
            this.challengesLabel.TabIndex = 5;
            this.challengesLabel.Text = "Challenges";
            // 
            // linkedLabel
            // 
            this.linkedLabel.AutoSize = true;
            this.linkedLabel.Location = new System.Drawing.Point(9, 9);
            this.linkedLabel.Name = "linkedLabel";
            this.linkedLabel.Size = new System.Drawing.Size(90, 13);
            this.linkedLabel.TabIndex = 6;
            this.linkedLabel.Text = "Linked Recipe ID";
            // 
            // chanceLabel
            // 
            this.chanceLabel.AutoSize = true;
            this.chanceLabel.Location = new System.Drawing.Point(190, 9);
            this.chanceLabel.Name = "chanceLabel";
            this.chanceLabel.Size = new System.Drawing.Size(88, 13);
            this.chanceLabel.TabIndex = 7;
            this.chanceLabel.Text = "Chance (0-100%)";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(12, 339);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 8;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(293, 339);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // expulsionLabel
            // 
            this.expulsionLabel.AutoSize = true;
            this.expulsionLabel.Location = new System.Drawing.Point(156, 200);
            this.expulsionLabel.Name = "expulsionLabel";
            this.expulsionLabel.Size = new System.Drawing.Size(52, 13);
            this.expulsionLabel.TabIndex = 10;
            this.expulsionLabel.Text = "Expulsion";
            // 
            // expulsionDataGridView
            // 
            this.expulsionDataGridView.AllowUserToResizeColumns = false;
            this.expulsionDataGridView.AllowUserToResizeRows = false;
            this.expulsionDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.expulsionDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.expulsionFilter,
            this.expulsionLimit});
            this.expulsionDataGridView.Location = new System.Drawing.Point(12, 216);
            this.expulsionDataGridView.Name = "expulsionDataGridView";
            this.expulsionDataGridView.Size = new System.Drawing.Size(356, 117);
            this.expulsionDataGridView.TabIndex = 11;
            this.expulsionDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.expulsionsDataGridView_CellDoubleClick);
            this.expulsionDataGridView.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.expulsionDataGridView_UserDeletedRow);
            // 
            // expulsionFilter
            // 
            this.expulsionFilter.HeaderText = "Filter";
            this.expulsionFilter.Name = "expulsionFilter";
            this.expulsionFilter.Width = 157;
            // 
            // expulsionLimit
            // 
            this.expulsionLimit.HeaderText = "Limit";
            this.expulsionLimit.Name = "expulsionLimit";
            this.expulsionLimit.Width = 156;
            // 
            // RecipeLinkViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 374);
            this.Controls.Add(this.expulsionDataGridView);
            this.Controls.Add(this.expulsionLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.chanceLabel);
            this.Controls.Add(this.linkedLabel);
            this.Controls.Add(this.challengesLabel);
            this.Controls.Add(this.openRecipeButton);
            this.Controls.Add(this.challengesDataGridView);
            this.Controls.Add(this.additionalCheckBox);
            this.Controls.Add(this.chanceNumericUpDown);
            this.Controls.Add(this.idTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RecipeLinkViewer";
            this.Text = "RecipeLinkViewer";
            ((System.ComponentModel.ISupportInitialize)(this.chanceNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.challengesDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.expulsionDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.NumericUpDown chanceNumericUpDown;
        private System.Windows.Forms.CheckBox additionalCheckBox;
        private System.Windows.Forms.DataGridView challengesDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn aspectId;
        private System.Windows.Forms.DataGridViewTextBoxColumn isBaseOrNull;
        private System.Windows.Forms.Button openRecipeButton;
        private System.Windows.Forms.Label challengesLabel;
        private System.Windows.Forms.Label linkedLabel;
        private System.Windows.Forms.Label chanceLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label expulsionLabel;
        private System.Windows.Forms.DataGridView expulsionDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn expulsionFilter;
        private System.Windows.Forms.DataGridViewTextBoxColumn expulsionLimit;
    }
}