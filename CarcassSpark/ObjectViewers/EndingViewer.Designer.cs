namespace CarcassSpark.ObjectViewers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EndingViewer));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.labelTextBox = new System.Windows.Forms.TextBox();
            this.imageTextBox = new System.Windows.Forms.TextBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.idLabel = new System.Windows.Forms.Label();
            this.labelLabel = new System.Windows.Forms.Label();
            this.imageLabel = new System.Windows.Forms.Label();
            this.flavourLabel = new System.Windows.Forms.Label();
            this.flavourDomainUpDown = new System.Windows.Forms.DomainUpDown();
            this.animDomainUpDown = new System.Windows.Forms.DomainUpDown();
            this.animLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.achievementTextBox = new System.Windows.Forms.TextBox();
            this.achievementLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 290);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(218, 25);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(166, 20);
            this.idTextBox.TabIndex = 1;
            this.idTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.idTextBox.TextChanged += new System.EventHandler(this.idTextBox_TextChanged);
            // 
            // labelTextBox
            // 
            this.labelTextBox.Location = new System.Drawing.Point(218, 64);
            this.labelTextBox.Name = "labelTextBox";
            this.labelTextBox.Size = new System.Drawing.Size(166, 20);
            this.labelTextBox.TabIndex = 2;
            this.labelTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.labelTextBox.TextChanged += new System.EventHandler(this.labelTextBox_TextChanged);
            // 
            // imageTextBox
            // 
            this.imageTextBox.Location = new System.Drawing.Point(218, 103);
            this.imageTextBox.Name = "imageTextBox";
            this.imageTextBox.Size = new System.Drawing.Size(166, 20);
            this.imageTextBox.TabIndex = 3;
            this.imageTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.imageTextBox.TextChanged += new System.EventHandler(this.imageTextBox_TextChanged);
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(218, 220);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descriptionTextBox.Size = new System.Drawing.Size(166, 43);
            this.descriptionTextBox.TabIndex = 6;
            this.descriptionTextBox.TextChanged += new System.EventHandler(this.descriptionTextBox_TextChanged);
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Location = new System.Drawing.Point(328, 9);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(54, 13);
            this.idLabel.TabIndex = 7;
            this.idLabel.Text = "Ending ID";
            // 
            // labelLabel
            // 
            this.labelLabel.AutoSize = true;
            this.labelLabel.Location = new System.Drawing.Point(313, 48);
            this.labelLabel.Name = "labelLabel";
            this.labelLabel.Size = new System.Drawing.Size(69, 13);
            this.labelLabel.TabIndex = 8;
            this.labelLabel.Text = "Ending Label";
            // 
            // imageLabel
            // 
            this.imageLabel.AutoSize = true;
            this.imageLabel.Location = new System.Drawing.Point(310, 87);
            this.imageLabel.Name = "imageLabel";
            this.imageLabel.Size = new System.Drawing.Size(72, 13);
            this.imageLabel.TabIndex = 9;
            this.imageLabel.Text = "Ending Image";
            // 
            // flavourLabel
            // 
            this.flavourLabel.AutoSize = true;
            this.flavourLabel.Location = new System.Drawing.Point(304, 126);
            this.flavourLabel.Name = "flavourLabel";
            this.flavourLabel.Size = new System.Drawing.Size(78, 13);
            this.flavourLabel.TabIndex = 10;
            this.flavourLabel.Text = "Ending Flavour";
            // 
            // flavourDomainUpDown
            // 
            this.flavourDomainUpDown.Items.Add("None");
            this.flavourDomainUpDown.Items.Add("Grand");
            this.flavourDomainUpDown.Items.Add("Melancholy");
            this.flavourDomainUpDown.Items.Add("Pale");
            this.flavourDomainUpDown.Items.Add("Vile");
            this.flavourDomainUpDown.Location = new System.Drawing.Point(218, 142);
            this.flavourDomainUpDown.Name = "flavourDomainUpDown";
            this.flavourDomainUpDown.Size = new System.Drawing.Size(166, 20);
            this.flavourDomainUpDown.TabIndex = 11;
            this.flavourDomainUpDown.SelectedItemChanged += new System.EventHandler(this.flavourDomainUpDown_SelectedItemChanged);
            // 
            // animDomainUpDown
            // 
            this.animDomainUpDown.Items.Add("DramaticLight");
            this.animDomainUpDown.Items.Add("DramaticLightCool");
            this.animDomainUpDown.Items.Add("DramaticLightEvil");
            this.animDomainUpDown.Location = new System.Drawing.Point(218, 181);
            this.animDomainUpDown.Name = "animDomainUpDown";
            this.animDomainUpDown.Size = new System.Drawing.Size(166, 20);
            this.animDomainUpDown.TabIndex = 12;
            this.animDomainUpDown.SelectedItemChanged += new System.EventHandler(this.animDomainUpDown_SelectedItemChanged);
            // 
            // animLabel
            // 
            this.animLabel.AutoSize = true;
            this.animLabel.Location = new System.Drawing.Point(331, 165);
            this.animLabel.Name = "animLabel";
            this.animLabel.Size = new System.Drawing.Size(53, 13);
            this.animLabel.TabIndex = 13;
            this.animLabel.Text = "Animation";
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(324, 204);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(60, 13);
            this.descriptionLabel.TabIndex = 14;
            this.descriptionLabel.Text = "Description";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(12, 308);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 15;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(307, 308);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 16;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // achievementTextBox
            // 
            this.achievementTextBox.Location = new System.Drawing.Point(216, 282);
            this.achievementTextBox.Name = "achievementTextBox";
            this.achievementTextBox.Size = new System.Drawing.Size(168, 20);
            this.achievementTextBox.TabIndex = 17;
            this.achievementTextBox.TextChanged += new System.EventHandler(this.achievementTextBox_TextChanged);
            // 
            // achievementLabel
            // 
            this.achievementLabel.AutoSize = true;
            this.achievementLabel.Location = new System.Drawing.Point(313, 266);
            this.achievementLabel.Name = "achievementLabel";
            this.achievementLabel.Size = new System.Drawing.Size(69, 13);
            this.achievementLabel.TabIndex = 18;
            this.achievementLabel.Text = "Achievement";
            // 
            // EndingViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 343);
            this.Controls.Add(this.achievementLabel);
            this.Controls.Add(this.achievementTextBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.animLabel);
            this.Controls.Add(this.animDomainUpDown);
            this.Controls.Add(this.flavourDomainUpDown);
            this.Controls.Add(this.flavourLabel);
            this.Controls.Add(this.imageLabel);
            this.Controls.Add(this.labelLabel);
            this.Controls.Add(this.idLabel);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.imageTextBox);
            this.Controls.Add(this.labelTextBox);
            this.Controls.Add(this.idTextBox);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label labelLabel;
        private System.Windows.Forms.Label imageLabel;
        private System.Windows.Forms.Label flavourLabel;
        private System.Windows.Forms.DomainUpDown flavourDomainUpDown;
        private System.Windows.Forms.DomainUpDown animDomainUpDown;
        private System.Windows.Forms.Label animLabel;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox achievementTextBox;
        private System.Windows.Forms.Label achievementLabel;
    }
}