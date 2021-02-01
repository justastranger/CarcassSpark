namespace CarcassSpark.Tools
{
    partial class GroupEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupEditor));
            this.groupComboBox = new System.Windows.Forms.ComboBox();
            this.okBbutton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // groupComboBox
            // 
            this.groupComboBox.FormattingEnabled = true;
            this.groupComboBox.Location = new System.Drawing.Point(12, 12);
            this.groupComboBox.Name = "groupComboBox";
            this.groupComboBox.Size = new System.Drawing.Size(426, 21);
            this.groupComboBox.TabIndex = 0;
            this.groupComboBox.SelectedIndexChanged += new System.EventHandler(this.GroupComboBox_SelectedIndexChanged);
            this.groupComboBox.TextUpdate += new System.EventHandler(this.GroupComboBox_TextUpdate);
            this.groupComboBox.SelectedValueChanged += new System.EventHandler(this.GroupComboBox_SelectedValueChanged);
            // 
            // okBbutton
            // 
            this.okBbutton.Location = new System.Drawing.Point(12, 39);
            this.okBbutton.Name = "okBbutton";
            this.okBbutton.Size = new System.Drawing.Size(75, 23);
            this.okBbutton.TabIndex = 1;
            this.okBbutton.Text = "OK";
            this.okBbutton.UseVisualStyleBackColor = true;
            this.okBbutton.Click += new System.EventHandler(this.OkBbutton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(363, 39);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // GroupEditor
            // 
            this.AcceptButton = this.okBbutton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(450, 74);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okBbutton);
            this.Controls.Add(this.groupComboBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GroupEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Group Editor";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox groupComboBox;
        private System.Windows.Forms.Button okBbutton;
        private System.Windows.Forms.Button cancelButton;
    }
}