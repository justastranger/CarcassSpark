namespace Cultist_Simulator_Modding_Toolkit
{
    partial class CreateElement
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
            this.isAspectCheckbox = new System.Windows.Forms.CheckBox();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.labelTextBox = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.aspectsListView = new System.Windows.Forms.ListView();
            this.iconSelectButton = new System.Windows.Forms.Button();
            this.openIconDialog = new System.Windows.Forms.OpenFileDialog();
            this.iconPictureBox = new System.Windows.Forms.PictureBox();
            this.isHiddenCheckBox = new System.Windows.Forms.CheckBox();
            this.okButton = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // isAspectCheckbox
            // 
            this.isAspectCheckbox.AutoSize = true;
            this.isAspectCheckbox.Location = new System.Drawing.Point(12, 123);
            this.isAspectCheckbox.Name = "isAspectCheckbox";
            this.isAspectCheckbox.Size = new System.Drawing.Size(59, 17);
            this.isAspectCheckbox.TabIndex = 0;
            this.isAspectCheckbox.Text = "Aspect";
            this.isAspectCheckbox.UseVisualStyleBackColor = true;
            this.isAspectCheckbox.CheckedChanged += new System.EventHandler(this.isAspectCheckbox_CheckedChanged);
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(12, 12);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(100, 20);
            this.idTextBox.TabIndex = 1;
            this.idTextBox.Text = "ID";
            this.idTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelTextBox
            // 
            this.labelTextBox.Location = new System.Drawing.Point(12, 38);
            this.labelTextBox.Name = "labelTextBox";
            this.labelTextBox.Size = new System.Drawing.Size(100, 20);
            this.labelTextBox.TabIndex = 2;
            this.labelTextBox.Text = "Label";
            this.labelTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 64);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "Icon";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(12, 146);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descriptionTextBox.Size = new System.Drawing.Size(250, 75);
            this.descriptionTextBox.TabIndex = 4;
            this.descriptionTextBox.Text = "Description";
            // 
            // aspectsListView
            // 
            this.aspectsListView.Location = new System.Drawing.Point(13, 227);
            this.aspectsListView.Name = "aspectsListView";
            this.aspectsListView.Size = new System.Drawing.Size(249, 69);
            this.aspectsListView.TabIndex = 5;
            this.aspectsListView.UseCompatibleStateImageBehavior = false;
            // 
            // iconSelectButton
            // 
            this.iconSelectButton.Location = new System.Drawing.Point(12, 90);
            this.iconSelectButton.Name = "iconSelectButton";
            this.iconSelectButton.Size = new System.Drawing.Size(100, 24);
            this.iconSelectButton.TabIndex = 6;
            this.iconSelectButton.Text = "Select Icon";
            this.iconSelectButton.UseVisualStyleBackColor = true;
            this.iconSelectButton.Click += new System.EventHandler(this.iconSelectButton_Click);
            // 
            // openIconDialog
            // 
            this.openIconDialog.FileName = "openFileDialog1";
            this.openIconDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openIconDialog_FileOk);
            // 
            // iconPictureBox
            // 
            this.iconPictureBox.Location = new System.Drawing.Point(134, 12);
            this.iconPictureBox.Name = "iconPictureBox";
            this.iconPictureBox.Size = new System.Drawing.Size(128, 128);
            this.iconPictureBox.TabIndex = 7;
            this.iconPictureBox.TabStop = false;
            // 
            // isHiddenCheckBox
            // 
            this.isHiddenCheckBox.AutoSize = true;
            this.isHiddenCheckBox.Location = new System.Drawing.Point(12, 227);
            this.isHiddenCheckBox.Name = "isHiddenCheckBox";
            this.isHiddenCheckBox.Size = new System.Drawing.Size(102, 17);
            this.isHiddenCheckBox.TabIndex = 8;
            this.isHiddenCheckBox.Text = "Hidden Aspect?";
            this.isHiddenCheckBox.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(12, 305);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 9;
            this.okButton.Text = "Create";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 26);
            // 
            // CreateElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 340);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.isHiddenCheckBox);
            this.Controls.Add(this.iconPictureBox);
            this.Controls.Add(this.iconSelectButton);
            this.Controls.Add(this.aspectsListView);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.labelTextBox);
            this.Controls.Add(this.idTextBox);
            this.Controls.Add(this.isAspectCheckbox);
            this.MaximizeBox = false;
            this.Name = "CreateElement";
            this.Text = "Create Element";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox isAspectCheckbox;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.TextBox labelTextBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.ListView aspectsListView;
        private System.Windows.Forms.Button iconSelectButton;
        private System.Windows.Forms.OpenFileDialog openIconDialog;
        private System.Windows.Forms.PictureBox iconPictureBox;
        private System.Windows.Forms.CheckBox isHiddenCheckBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}