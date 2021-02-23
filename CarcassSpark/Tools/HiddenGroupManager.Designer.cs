namespace CarcassSpark.Tools
{
    partial class HiddenGroupManager
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
            this.hiddenGroupsListView = new System.Windows.Forms.ListView();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.HiddenGroupsHiddenColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // hiddenGroupsListView
            // 
            this.hiddenGroupsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hiddenGroupsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.HiddenGroupsHiddenColumnHeader});
            this.hiddenGroupsListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.hiddenGroupsListView.Location = new System.Drawing.Point(12, 12);
            this.hiddenGroupsListView.MultiSelect = false;
            this.hiddenGroupsListView.Name = "hiddenGroupsListView";
            this.hiddenGroupsListView.Size = new System.Drawing.Size(390, 403);
            this.hiddenGroupsListView.TabIndex = 0;
            this.hiddenGroupsListView.UseCompatibleStateImageBehavior = false;
            this.hiddenGroupsListView.View = System.Windows.Forms.View.Details;
            this.hiddenGroupsListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.HiddenGroupsListView_MouseDoubleClick);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(12, 421);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(327, 421);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // HiddenGroupsHiddenColumnHeader
            // 
            this.HiddenGroupsHiddenColumnHeader.Width = 300;
            // 
            // HiddenGroupManager
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(414, 456);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.hiddenGroupsListView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "HiddenGroupManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hidden Group Manager";
            this.Shown += new System.EventHandler(this.HiddenGroupManager_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView hiddenGroupsListView;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ColumnHeader HiddenGroupsHiddenColumnHeader;
    }
}