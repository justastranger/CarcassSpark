namespace Cultist_Simulator_Modding_Toolkit
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
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.labelTextBox = new System.Windows.Forms.TextBox();
            this.atStartCheckBox = new System.Windows.Forms.CheckBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.slotsListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(12, 12);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(127, 20);
            this.idTextBox.TabIndex = 0;
            this.idTextBox.Text = "ID";
            this.idTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelTextBox
            // 
            this.labelTextBox.Location = new System.Drawing.Point(145, 12);
            this.labelTextBox.Name = "labelTextBox";
            this.labelTextBox.Size = new System.Drawing.Size(127, 20);
            this.labelTextBox.TabIndex = 1;
            this.labelTextBox.Text = "Label";
            this.labelTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // atStartCheckBox
            // 
            this.atStartCheckBox.AutoSize = true;
            this.atStartCheckBox.Enabled = false;
            this.atStartCheckBox.Location = new System.Drawing.Point(12, 38);
            this.atStartCheckBox.Name = "atStartCheckBox";
            this.atStartCheckBox.Size = new System.Drawing.Size(165, 17);
            this.atStartCheckBox.TabIndex = 2;
            this.atStartCheckBox.Text = "Start new game with this verb";
            this.atStartCheckBox.UseVisualStyleBackColor = true;
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(12, 61);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(260, 87);
            this.descriptionTextBox.TabIndex = 3;
            this.descriptionTextBox.Text = "Description";
            // 
            // slotsListBox
            // 
            this.slotsListBox.FormattingEnabled = true;
            this.slotsListBox.Location = new System.Drawing.Point(12, 154);
            this.slotsListBox.Name = "slotsListBox";
            this.slotsListBox.Size = new System.Drawing.Size(260, 95);
            this.slotsListBox.TabIndex = 4;
            this.slotsListBox.DoubleClick += new System.EventHandler(this.slotsListBox_DoubleClick);
            // 
            // VerbViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.slotsListBox);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.atStartCheckBox);
            this.Controls.Add(this.labelTextBox);
            this.Controls.Add(this.idTextBox);
            this.Name = "VerbViewer";
            this.Text = "VerbViewer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.TextBox labelTextBox;
        private System.Windows.Forms.CheckBox atStartCheckBox;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.ListBox slotsListBox;
    }
}