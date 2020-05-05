namespace Cultist_Simulator_Modding_Toolkit
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
            this.filterTextBox = new System.Windows.Forms.TextBox();
            this.mutateAspectIdTextBox = new System.Windows.Forms.TextBox();
            this.levelNumericUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.levelNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // filterTextBox
            // 
            this.filterTextBox.Location = new System.Drawing.Point(48, 26);
            this.filterTextBox.Name = "filterTextBox";
            this.filterTextBox.Size = new System.Drawing.Size(120, 20);
            this.filterTextBox.TabIndex = 0;
            this.filterTextBox.Text = "Filter";
            this.filterTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // mutateAspectIdTextBox
            // 
            this.mutateAspectIdTextBox.Location = new System.Drawing.Point(48, 52);
            this.mutateAspectIdTextBox.Name = "mutateAspectIdTextBox";
            this.mutateAspectIdTextBox.Size = new System.Drawing.Size(120, 20);
            this.mutateAspectIdTextBox.TabIndex = 1;
            this.mutateAspectIdTextBox.Text = "mutateAspectId";
            this.mutateAspectIdTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // levelNumericUpDown
            // 
            this.levelNumericUpDown.Enabled = false;
            this.levelNumericUpDown.Location = new System.Drawing.Point(48, 78);
            this.levelNumericUpDown.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.levelNumericUpDown.Name = "levelNumericUpDown";
            this.levelNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.levelNumericUpDown.TabIndex = 2;
            // 
            // MutationViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 125);
            this.Controls.Add(this.levelNumericUpDown);
            this.Controls.Add(this.mutateAspectIdTextBox);
            this.Controls.Add(this.filterTextBox);
            this.Name = "MutationViewer";
            this.Text = "MutationViewer";
            ((System.ComponentModel.ISupportInitialize)(this.levelNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox filterTextBox;
        private System.Windows.Forms.TextBox mutateAspectIdTextBox;
        private System.Windows.Forms.NumericUpDown levelNumericUpDown;
    }
}