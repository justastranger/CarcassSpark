namespace CarcassSpark.Tools
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.programNameLabel = new System.Windows.Forms.Label();
            this.versionLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.sourceLinkLabel = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // programNameLabel
            // 
            this.programNameLabel.AutoSize = true;
            this.programNameLabel.Location = new System.Drawing.Point(12, 271);
            this.programNameLabel.Name = "programNameLabel";
            this.programNameLabel.Size = new System.Drawing.Size(76, 13);
            this.programNameLabel.TabIndex = 0;
            this.programNameLabel.Text = "Carcass Spark";
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new System.Drawing.Point(12, 284);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(92, 13);
            this.versionLabel.TabIndex = 1;
            this.versionLabel.Text = "Version: unknown";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 256);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // closeButton
            // 
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(193, 415);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 3;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 297);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "By Just A Stranger";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 310);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "With Contributions From: SarahK";
            // 
            // sourceLinkLabel
            // 
            this.sourceLinkLabel.AutoSize = true;
            this.sourceLinkLabel.Location = new System.Drawing.Point(12, 323);
            this.sourceLinkLabel.Name = "sourceLinkLabel";
            this.sourceLinkLabel.Size = new System.Drawing.Size(236, 13);
            this.sourceLinkLabel.TabIndex = 6;
            this.sourceLinkLabel.TabStop = true;
            this.sourceLinkLabel.Text = "https://github.com/justastranger/CarcassSpark/";
            this.sourceLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.SourceLinkLabel_LinkClicked);
            // 
            // AboutForm
            // 
            this.AcceptButton = this.closeButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(280, 450);
            this.Controls.Add(this.sourceLinkLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.programNameLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AboutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label programNameLabel;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel sourceLinkLabel;
    }
}