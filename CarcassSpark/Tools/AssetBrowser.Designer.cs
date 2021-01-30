namespace CarcassSpark.Tools
{
    partial class AssetBrowser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssetBrowser));
            this.assetsListView = new System.Windows.Forms.ListView();
            this.assetsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyImageIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.assetsContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // assetsListView
            // 
            this.assetsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.assetsListView.ContextMenuStrip = this.assetsContextMenuStrip;
            this.assetsListView.HideSelection = false;
            this.assetsListView.Location = new System.Drawing.Point(12, 12);
            this.assetsListView.MultiSelect = false;
            this.assetsListView.Name = "assetsListView";
            this.assetsListView.Size = new System.Drawing.Size(776, 397);
            this.assetsListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.assetsListView.TabIndex = 0;
            this.assetsListView.UseCompatibleStateImageBehavior = false;
            this.assetsListView.View = System.Windows.Forms.View.Tile;
            this.assetsListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.AssetsListView_MouseDoubleClick);
            // 
            // assetsContextMenuStrip
            // 
            this.assetsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyImageIDToolStripMenuItem});
            this.assetsContextMenuStrip.Name = "assetsContextMenuStrip";
            this.assetsContextMenuStrip.Size = new System.Drawing.Size(153, 26);
            // 
            // copyImageIDToolStripMenuItem
            // 
            this.copyImageIDToolStripMenuItem.Name = "copyImageIDToolStripMenuItem";
            this.copyImageIDToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.copyImageIDToolStripMenuItem.Text = "Copy Image ID";
            this.copyImageIDToolStripMenuItem.Click += new System.EventHandler(this.CopyImageIDToolStripMenuItem_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(713, 415);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.Location = new System.Drawing.Point(12, 415);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // AssetBrowser
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.assetsListView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AssetBrowser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asset Browser";
            this.assetsContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView assetsListView;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.ContextMenuStrip assetsContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem copyImageIDToolStripMenuItem;
    }
}