namespace Cultist_Simulator_Modding_Toolkit
{
    partial class EndingViewer
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.labelTextBox = new System.Windows.Forms.TextBox();
            this.imageTextBox = new System.Windows.Forms.TextBox();
            this.flavorTextBox = new System.Windows.Forms.TextBox();
            this.animTextBox = new System.Windows.Forms.TextBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 126);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(118, 12);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(154, 20);
            this.idTextBox.TabIndex = 1;
            this.idTextBox.Text = "ID";
            this.idTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelTextBox
            // 
            this.labelTextBox.Location = new System.Drawing.Point(118, 38);
            this.labelTextBox.Name = "labelTextBox";
            this.labelTextBox.Size = new System.Drawing.Size(154, 20);
            this.labelTextBox.TabIndex = 2;
            this.labelTextBox.Text = "Label";
            this.labelTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // imageTextBox
            // 
            this.imageTextBox.Location = new System.Drawing.Point(118, 64);
            this.imageTextBox.Name = "imageTextBox";
            this.imageTextBox.Size = new System.Drawing.Size(154, 20);
            this.imageTextBox.TabIndex = 3;
            this.imageTextBox.Text = "Image";
            this.imageTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // flavorTextBox
            // 
            this.flavorTextBox.Location = new System.Drawing.Point(118, 92);
            this.flavorTextBox.Name = "flavorTextBox";
            this.flavorTextBox.Size = new System.Drawing.Size(154, 20);
            this.flavorTextBox.TabIndex = 4;
            this.flavorTextBox.Text = "Flavor";
            this.flavorTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // animTextBox
            // 
            this.animTextBox.Location = new System.Drawing.Point(118, 118);
            this.animTextBox.Name = "animTextBox";
            this.animTextBox.Size = new System.Drawing.Size(154, 20);
            this.animTextBox.TabIndex = 5;
            this.animTextBox.Text = "Animation";
            this.animTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(12, 144);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descriptionTextBox.Size = new System.Drawing.Size(260, 105);
            this.descriptionTextBox.TabIndex = 6;
            this.descriptionTextBox.Text = "Description";
            // 
            // EndingViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.animTextBox);
            this.Controls.Add(this.flavorTextBox);
            this.Controls.Add(this.imageTextBox);
            this.Controls.Add(this.labelTextBox);
            this.Controls.Add(this.idTextBox);
            this.Controls.Add(this.pictureBox1);
            this.Name = "EndingViewer";
            this.Text = "EndingViewer";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.TextBox labelTextBox;
        private System.Windows.Forms.TextBox imageTextBox;
        private System.Windows.Forms.TextBox flavorTextBox;
        private System.Windows.Forms.TextBox animTextBox;
        private System.Windows.Forms.TextBox descriptionTextBox;
    }
}