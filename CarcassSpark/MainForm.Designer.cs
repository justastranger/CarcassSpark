namespace CarcassSpark
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.loadVanillaButton = new System.Windows.Forms.Button();
            this.openModButton = new System.Windows.Forms.Button();
            this.modFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openSettingsButton = new System.Windows.Forms.Button();
            this.newModButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // loadVanillaButton
            // 
            this.loadVanillaButton.Location = new System.Drawing.Point(12, 12);
            this.loadVanillaButton.Name = "loadVanillaButton";
            this.loadVanillaButton.Size = new System.Drawing.Size(130, 50);
            this.loadVanillaButton.TabIndex = 0;
            this.loadVanillaButton.Text = "Open Vanilla Assets";
            this.loadVanillaButton.UseVisualStyleBackColor = true;
            this.loadVanillaButton.Click += new System.EventHandler(this.loadVanillaButton_Click);
            // 
            // openModButton
            // 
            this.openModButton.Location = new System.Drawing.Point(12, 124);
            this.openModButton.Name = "openModButton";
            this.openModButton.Size = new System.Drawing.Size(130, 50);
            this.openModButton.TabIndex = 1;
            this.openModButton.Text = "Open Mod";
            this.openModButton.UseVisualStyleBackColor = true;
            this.openModButton.Click += new System.EventHandler(this.openModButton_Click);
            // 
            // modFolderBrowserDialog
            // 
            this.modFolderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // openSettingsButton
            // 
            this.openSettingsButton.Location = new System.Drawing.Point(12, 180);
            this.openSettingsButton.Name = "openSettingsButton";
            this.openSettingsButton.Size = new System.Drawing.Size(130, 50);
            this.openSettingsButton.TabIndex = 2;
            this.openSettingsButton.Text = "Settings";
            this.openSettingsButton.UseVisualStyleBackColor = true;
            this.openSettingsButton.Click += new System.EventHandler(this.openSettingsButton_Click);
            // 
            // newModButton
            // 
            this.newModButton.Location = new System.Drawing.Point(12, 68);
            this.newModButton.Name = "newModButton";
            this.newModButton.Size = new System.Drawing.Size(130, 50);
            this.newModButton.TabIndex = 3;
            this.newModButton.Text = "New Mod";
            this.newModButton.UseVisualStyleBackColor = true;
            this.newModButton.Click += new System.EventHandler(this.newModButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(289, 55);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 36);
            this.button1.TabIndex = 4;
            this.button1.Text = "Tabbed Editor";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 261);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.newModButton);
            this.Controls.Add(this.openSettingsButton);
            this.Controls.Add(this.openModButton);
            this.Controls.Add(this.loadVanillaButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button loadVanillaButton;
        private System.Windows.Forms.Button openModButton;
        private System.Windows.Forms.FolderBrowserDialog modFolderBrowserDialog;
        private System.Windows.Forms.Button openSettingsButton;
        private System.Windows.Forms.Button newModButton;
        private System.Windows.Forms.Button button1;
    }
}