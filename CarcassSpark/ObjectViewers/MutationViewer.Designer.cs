namespace CarcassSpark.ObjectViewers
{
    partial class MutationViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MutationViewer));
            this.filterTextBox = new System.Windows.Forms.TextBox();
            this.mutateAspectIdTextBox = new System.Windows.Forms.TextBox();
            this.levelNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.additiveCheckBox = new System.Windows.Forms.CheckBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.filterLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.levelNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // filterTextBox
            // 
            this.filterTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterTextBox.Location = new System.Drawing.Point(12, 25);
            this.filterTextBox.Name = "filterTextBox";
            this.filterTextBox.Size = new System.Drawing.Size(194, 20);
            this.filterTextBox.TabIndex = 0;
            this.filterTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.filterTextBox, resources.GetString("filterTextBox.ToolTip"));
            this.filterTextBox.TextChanged += new System.EventHandler(this.FilterTextBox_TextChanged);
            // 
            // mutateAspectIdTextBox
            // 
            this.mutateAspectIdTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mutateAspectIdTextBox.Location = new System.Drawing.Point(12, 64);
            this.mutateAspectIdTextBox.Name = "mutateAspectIdTextBox";
            this.mutateAspectIdTextBox.Size = new System.Drawing.Size(194, 20);
            this.mutateAspectIdTextBox.TabIndex = 1;
            this.mutateAspectIdTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.mutateAspectIdTextBox, "This is the aspect you want to modify on the element.\r\nMutations are stored separ" +
        "ately to an element\'s base aspects, and will persist when the element is transfo" +
        "rmed.");
            this.mutateAspectIdTextBox.TextChanged += new System.EventHandler(this.MutateAspectIdTextBox_TextChanged);
            // 
            // levelNumericUpDown
            // 
            this.levelNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.levelNumericUpDown.Location = new System.Drawing.Point(12, 103);
            this.levelNumericUpDown.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.levelNumericUpDown.Name = "levelNumericUpDown";
            this.levelNumericUpDown.Size = new System.Drawing.Size(194, 20);
            this.levelNumericUpDown.TabIndex = 2;
            this.ToolTip.SetToolTip(this.levelNumericUpDown, resources.GetString("levelNumericUpDown.ToolTip"));
            this.levelNumericUpDown.ValueChanged += new System.EventHandler(this.LevelNumericUpDown_ValueChanged);
            // 
            // additiveCheckBox
            // 
            this.additiveCheckBox.AutoSize = true;
            this.additiveCheckBox.Location = new System.Drawing.Point(12, 128);
            this.additiveCheckBox.Name = "additiveCheckBox";
            this.additiveCheckBox.Size = new System.Drawing.Size(64, 17);
            this.additiveCheckBox.TabIndex = 3;
            this.additiveCheckBox.Text = "Additive";
            this.ToolTip.SetToolTip(this.additiveCheckBox, "When false, the aspect\'s mutation is set to the level specified.\r\nWhen true, the " +
        "mutation level is added to the current level.");
            this.additiveCheckBox.UseVisualStyleBackColor = true;
            this.additiveCheckBox.CheckedChanged += new System.EventHandler(this.AdditiveCheckBox_CheckedChanged);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(12, 151);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(131, 151);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // filterLabel
            // 
            this.filterLabel.AutoSize = true;
            this.filterLabel.Location = new System.Drawing.Point(12, 9);
            this.filterLabel.Name = "filterLabel";
            this.filterLabel.Size = new System.Drawing.Size(166, 13);
            this.filterLabel.TabIndex = 6;
            this.filterLabel.Text = "Element or Aspect ID to Filter with";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Aspect to Mutate";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Mutation Level";
            // 
            // MutationViewer
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(218, 186);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.filterLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.additiveCheckBox);
            this.Controls.Add(this.levelNumericUpDown);
            this.Controls.Add(this.mutateAspectIdTextBox);
            this.Controls.Add(this.filterTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(234, 225);
            this.Name = "MutationViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mutation Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.levelNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox filterTextBox;
        private System.Windows.Forms.TextBox mutateAspectIdTextBox;
        private System.Windows.Forms.NumericUpDown levelNumericUpDown;
        private System.Windows.Forms.CheckBox additiveCheckBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label filterLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip ToolTip;
    }
}