namespace Cultist_Simulator_Modding_Toolkit
{
    partial class AddAspectForm
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
            this.aspectListBox = new System.Windows.Forms.ListBox();
            this.aspectAmountUpDown = new System.Windows.Forms.NumericUpDown();
            this.addAspectAcceptButton = new System.Windows.Forms.Button();
            this.addAspectCancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.aspectAmountUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // aspectListBox
            // 
            this.aspectListBox.FormattingEnabled = true;
            this.aspectListBox.Location = new System.Drawing.Point(12, 12);
            this.aspectListBox.Name = "aspectListBox";
            this.aspectListBox.Size = new System.Drawing.Size(260, 199);
            this.aspectListBox.TabIndex = 0;
            // 
            // aspectAmountUpDown
            // 
            this.aspectAmountUpDown.Location = new System.Drawing.Point(102, 225);
            this.aspectAmountUpDown.Name = "aspectAmountUpDown";
            this.aspectAmountUpDown.Size = new System.Drawing.Size(66, 20);
            this.aspectAmountUpDown.TabIndex = 1;
            // 
            // addAspectAcceptButton
            // 
            this.addAspectAcceptButton.Location = new System.Drawing.Point(12, 217);
            this.addAspectAcceptButton.Name = "addAspectAcceptButton";
            this.addAspectAcceptButton.Size = new System.Drawing.Size(84, 32);
            this.addAspectAcceptButton.TabIndex = 2;
            this.addAspectAcceptButton.Text = "Accept";
            this.addAspectAcceptButton.UseVisualStyleBackColor = true;
            this.addAspectAcceptButton.Click += new System.EventHandler(this.addAspectAcceptButton_Click);
            // 
            // addAspectCancelButton
            // 
            this.addAspectCancelButton.Location = new System.Drawing.Point(174, 217);
            this.addAspectCancelButton.Name = "addAspectCancelButton";
            this.addAspectCancelButton.Size = new System.Drawing.Size(97, 32);
            this.addAspectCancelButton.TabIndex = 3;
            this.addAspectCancelButton.Text = "Cancel";
            this.addAspectCancelButton.UseVisualStyleBackColor = true;
            this.addAspectCancelButton.Click += new System.EventHandler(this.addAspectCancelButton_Click);
            // 
            // AddAspectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.addAspectCancelButton);
            this.Controls.Add(this.addAspectAcceptButton);
            this.Controls.Add(this.aspectAmountUpDown);
            this.Controls.Add(this.aspectListBox);
            this.Name = "AddAspectForm";
            this.Text = "AddAspectForm";
            ((System.ComponentModel.ISupportInitialize)(this.aspectAmountUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox aspectListBox;
        private System.Windows.Forms.NumericUpDown aspectAmountUpDown;
        private System.Windows.Forms.Button addAspectAcceptButton;
        private System.Windows.Forms.Button addAspectCancelButton;
    }
}