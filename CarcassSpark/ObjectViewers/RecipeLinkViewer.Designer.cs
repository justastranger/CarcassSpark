﻿namespace CarcassSpark.ObjectViewers
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecipeLinkViewer));
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.chanceNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.additionalCheckBox = new System.Windows.Forms.CheckBox();
            this.challengesDataGridView = new System.Windows.Forms.DataGridView();
            this.aspectId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.challengeType = new System.Windows.Forms.DataGridViewComboBoxColumn();
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
            this.expulsionTotalLimitLabel = new System.Windows.Forms.Label();
            this.totalExpulsionLimitNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.RecipeIDErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.selectRecipeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chanceNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.challengesDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.expulsionDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.totalExpulsionLimitNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecipeIDErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // idTextBox
            // 
            this.idTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.idTextBox.Location = new System.Drawing.Point(12, 25);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(244, 20);
            this.idTextBox.TabIndex = 0;
            this.idTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.idTextBox, "This is the recipe that is to be executed if all of its requirements are met and " +
        "challenges passed.");
            this.idTextBox.TextChanged += new System.EventHandler(this.IdTextBox_TextChanged);
            // 
            // chanceNumericUpDown
            // 
            this.chanceNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chanceNumericUpDown.Location = new System.Drawing.Point(278, 26);
            this.chanceNumericUpDown.Name = "chanceNumericUpDown";
            this.chanceNumericUpDown.Size = new System.Drawing.Size(90, 20);
            this.chanceNumericUpDown.TabIndex = 1;
            this.ToolTip.SetToolTip(this.chanceNumericUpDown, "This is the chance of the recipe being selected.\r\nValues of 0 and 100 both guaran" +
        "tee it will execute.\r\nNot necessary for alternative recipes.");
            this.chanceNumericUpDown.ValueChanged += new System.EventHandler(this.ChanceNumericUpDown_ValueChanged);
            // 
            // additionalCheckBox
            // 
            this.additionalCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.additionalCheckBox.AutoSize = true;
            this.additionalCheckBox.Location = new System.Drawing.Point(278, 52);
            this.additionalCheckBox.Name = "additionalCheckBox";
            this.additionalCheckBox.Size = new System.Drawing.Size(72, 17);
            this.additionalCheckBox.TabIndex = 2;
            this.additionalCheckBox.Text = "Additional";
            this.ToolTip.SetToolTip(this.additionalCheckBox, "When true, the specified recipe is opened in its own token when executed through " +
        "this link.");
            this.additionalCheckBox.UseVisualStyleBackColor = true;
            this.additionalCheckBox.CheckedChanged += new System.EventHandler(this.AdditionalCheckBox_CheckedChanged);
            // 
            // challengesDataGridView
            // 
            this.challengesDataGridView.AllowUserToResizeColumns = false;
            this.challengesDataGridView.AllowUserToResizeRows = false;
            this.challengesDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.challengesDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.challengesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.challengesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.aspectId,
            this.challengeType});
            this.challengesDataGridView.Location = new System.Drawing.Point(12, 98);
            this.challengesDataGridView.Name = "challengesDataGridView";
            this.challengesDataGridView.Size = new System.Drawing.Size(356, 104);
            this.challengesDataGridView.TabIndex = 3;
            this.ToolTip.SetToolTip(this.challengesDataGridView, resources.GetString("challengesDataGridView.ToolTip"));
            this.challengesDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ChallengesDataGridView_CellDoubleClick);
            this.challengesDataGridView.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.ChallengesDataGridView_UserDeletedRow);
            // 
            // aspectId
            // 
            this.aspectId.HeaderText = "Aspect ID";
            this.aspectId.Name = "aspectId";
            // 
            // challengeType
            // 
            this.challengeType.HeaderText = "Type";
            this.challengeType.Items.AddRange(new object[] {
            "base",
            "advanced"});
            this.challengeType.Name = "challengeType";
            this.challengeType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.challengeType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // openRecipeButton
            // 
            this.openRecipeButton.Location = new System.Drawing.Point(12, 48);
            this.openRecipeButton.Name = "openRecipeButton";
            this.openRecipeButton.Size = new System.Drawing.Size(119, 23);
            this.openRecipeButton.TabIndex = 4;
            this.openRecipeButton.Text = "Open Linked Recipe";
            this.ToolTip.SetToolTip(this.openRecipeButton, "Opens a Recipe Viewer for the above recipe ID.");
            this.openRecipeButton.UseVisualStyleBackColor = true;
            this.openRecipeButton.Click += new System.EventHandler(this.OpenRecipeButton_Click);
            // 
            // challengesLabel
            // 
            this.challengesLabel.AutoSize = true;
            this.challengesLabel.Location = new System.Drawing.Point(161, 82);
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
            this.chanceLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chanceLabel.AutoSize = true;
            this.chanceLabel.Location = new System.Drawing.Point(275, 9);
            this.chanceLabel.Name = "chanceLabel";
            this.chanceLabel.Size = new System.Drawing.Size(88, 13);
            this.chanceLabel.TabIndex = 7;
            this.chanceLabel.Text = "Chance (0-100%)";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(12, 339);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 8;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(293, 339);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // expulsionLabel
            // 
            this.expulsionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.expulsionLabel.AutoSize = true;
            this.expulsionLabel.Location = new System.Drawing.Point(12, 210);
            this.expulsionLabel.Name = "expulsionLabel";
            this.expulsionLabel.Size = new System.Drawing.Size(52, 13);
            this.expulsionLabel.TabIndex = 10;
            this.expulsionLabel.Text = "Expulsion";
            // 
            // expulsionDataGridView
            // 
            this.expulsionDataGridView.AllowUserToResizeColumns = false;
            this.expulsionDataGridView.AllowUserToResizeRows = false;
            this.expulsionDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.expulsionDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.expulsionDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.expulsionDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.expulsionFilter,
            this.expulsionLimit});
            this.expulsionDataGridView.Location = new System.Drawing.Point(12, 229);
            this.expulsionDataGridView.Name = "expulsionDataGridView";
            this.expulsionDataGridView.Size = new System.Drawing.Size(356, 104);
            this.expulsionDataGridView.TabIndex = 11;
            this.ToolTip.SetToolTip(this.expulsionDataGridView, resources.GetString("expulsionDataGridView.ToolTip"));
            this.expulsionDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ExpulsionsDataGridView_CellDoubleClick);
            this.expulsionDataGridView.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.ExpulsionDataGridView_UserDeletedRow);
            // 
            // expulsionFilter
            // 
            this.expulsionFilter.HeaderText = "Filter";
            this.expulsionFilter.Name = "expulsionFilter";
            // 
            // expulsionLimit
            // 
            this.expulsionLimit.HeaderText = "Limit";
            this.expulsionLimit.Name = "expulsionLimit";
            // 
            // expulsionTotalLimitLabel
            // 
            this.expulsionTotalLimitLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.expulsionTotalLimitLabel.AutoSize = true;
            this.expulsionTotalLimitLabel.Location = new System.Drawing.Point(78, 210);
            this.expulsionTotalLimitLabel.Name = "expulsionTotalLimitLabel";
            this.expulsionTotalLimitLabel.Size = new System.Drawing.Size(103, 13);
            this.expulsionTotalLimitLabel.TabIndex = 12;
            this.expulsionTotalLimitLabel.Text = "Total Expulsion Limit";
            // 
            // totalExpulsionLimitNumericUpDown
            // 
            this.totalExpulsionLimitNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.totalExpulsionLimitNumericUpDown.Location = new System.Drawing.Point(187, 208);
            this.totalExpulsionLimitNumericUpDown.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.totalExpulsionLimitNumericUpDown.Name = "totalExpulsionLimitNumericUpDown";
            this.totalExpulsionLimitNumericUpDown.Size = new System.Drawing.Size(55, 20);
            this.totalExpulsionLimitNumericUpDown.TabIndex = 13;
            this.ToolTip.SetToolTip(this.totalExpulsionLimitNumericUpDown, "Upper limit on how many total elements can be moved to a new verb token, even if " +
        "you tried to move all of an element.");
            this.totalExpulsionLimitNumericUpDown.ValueChanged += new System.EventHandler(this.TotalExpulsionLimitNumericUpDown_ValueChanged);
            // 
            // RecipeIDErrorProvider
            // 
            this.RecipeIDErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.RecipeIDErrorProvider.ContainerControl = this;
            // 
            // selectRecipeButton
            // 
            this.selectRecipeButton.Location = new System.Drawing.Point(137, 48);
            this.selectRecipeButton.Name = "selectRecipeButton";
            this.selectRecipeButton.Size = new System.Drawing.Size(119, 23);
            this.selectRecipeButton.TabIndex = 14;
            this.selectRecipeButton.Text = "Select Recipe";
            this.selectRecipeButton.UseVisualStyleBackColor = true;
            this.selectRecipeButton.Click += new System.EventHandler(this.SelectRecipeButton_Click);
            // 
            // RecipeLinkViewer
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(380, 374);
            this.Controls.Add(this.selectRecipeButton);
            this.Controls.Add(this.totalExpulsionLimitNumericUpDown);
            this.Controls.Add(this.expulsionTotalLimitLabel);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(396, 413);
            this.Name = "RecipeLinkViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recipe Link Viewer";
            this.Shown += new System.EventHandler(this.RecipeLinkViewer_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.chanceNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.challengesDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.expulsionDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.totalExpulsionLimitNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecipeIDErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.NumericUpDown chanceNumericUpDown;
        private System.Windows.Forms.CheckBox additionalCheckBox;
        private System.Windows.Forms.DataGridView challengesDataGridView;
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
        private System.Windows.Forms.Label expulsionTotalLimitLabel;
        private System.Windows.Forms.NumericUpDown totalExpulsionLimitNumericUpDown;
        private System.Windows.Forms.ToolTip ToolTip;
        private System.Windows.Forms.DataGridViewTextBoxColumn aspectId;
        private System.Windows.Forms.DataGridViewComboBoxColumn challengeType;
        private System.Windows.Forms.ErrorProvider RecipeIDErrorProvider;
        private System.Windows.Forms.Button selectRecipeButton;
    }
}