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
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.labelTextBox = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addAspectContextMenuItem = new System.Windows.Forms.ToolStripTextBox();
            this.removeAspectContextMenuItem = new System.Windows.Forms.ToolStripTextBox();
            this.changeQuantityContextMenuItem = new System.Windows.Forms.ToolStripTextBox();
            this.iconSelectButton = new System.Windows.Forms.Button();
            this.openIconDialog = new System.Windows.Forms.OpenFileDialog();
            this.iconPictureBox = new System.Windows.Forms.PictureBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.commentsTextBox = new System.Windows.Forms.TextBox();
            this.aspectListBoxLabel = new System.Windows.Forms.Label();
            this.slotsTreeViewLabel = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.aspectId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addAspectButton = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
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
            this.descriptionTextBox.Location = new System.Drawing.Point(268, 9);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descriptionTextBox.Size = new System.Drawing.Size(310, 75);
            this.descriptionTextBox.TabIndex = 4;
            this.descriptionTextBox.Text = "Description";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addAspectContextMenuItem,
            this.removeAspectContextMenuItem,
            this.changeQuantityContextMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(272, 79);
            // 
            // addAspectContextMenuItem
            // 
            this.addAspectContextMenuItem.Name = "addAspectContextMenuItem";
            this.addAspectContextMenuItem.Size = new System.Drawing.Size(166, 23);
            this.addAspectContextMenuItem.Text = "Add Aspect";
            this.addAspectContextMenuItem.Click += new System.EventHandler(this.addAspectContextMenuItem_Click);
            // 
            // removeAspectContextMenuItem
            // 
            this.removeAspectContextMenuItem.Name = "removeAspectContextMenuItem";
            this.removeAspectContextMenuItem.Size = new System.Drawing.Size(201, 23);
            this.removeAspectContextMenuItem.Text = "Remove Aspect";
            this.removeAspectContextMenuItem.Click += new System.EventHandler(this.removeAspectContextMenuItem_Click);
            // 
            // changeQuantityContextMenuItem
            // 
            this.changeQuantityContextMenuItem.Name = "changeQuantityContextMenuItem";
            this.changeQuantityContextMenuItem.Size = new System.Drawing.Size(236, 23);
            this.changeQuantityContextMenuItem.Text = "Change Quantity";
            this.changeQuantityContextMenuItem.Click += new System.EventHandler(this.changeQuantityContextMenuItem_Click);
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
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(12, 392);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 9;
            this.okButton.Text = "Create";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(503, 392);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // commentsTextBox
            // 
            this.commentsTextBox.Location = new System.Drawing.Point(268, 90);
            this.commentsTextBox.Multiline = true;
            this.commentsTextBox.Name = "commentsTextBox";
            this.commentsTextBox.Size = new System.Drawing.Size(310, 50);
            this.commentsTextBox.TabIndex = 12;
            this.commentsTextBox.Text = "Comments";
            // 
            // aspectListBoxLabel
            // 
            this.aspectListBoxLabel.AutoSize = true;
            this.aspectListBoxLabel.Location = new System.Drawing.Point(36, 166);
            this.aspectListBoxLabel.Name = "aspectListBoxLabel";
            this.aspectListBoxLabel.Size = new System.Drawing.Size(45, 13);
            this.aspectListBoxLabel.TabIndex = 16;
            this.aspectListBoxLabel.Text = "Aspects";
            // 
            // slotsTreeViewLabel
            // 
            this.slotsTreeViewLabel.AutoSize = true;
            this.slotsTreeViewLabel.Location = new System.Drawing.Point(383, 166);
            this.slotsTreeViewLabel.Name = "slotsTreeViewLabel";
            this.slotsTreeViewLabel.Size = new System.Drawing.Size(30, 13);
            this.slotsTreeViewLabel.TabIndex = 17;
            this.slotsTreeViewLabel.Text = "Slots";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.aspectId,
            this.amount});
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(12, 182);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(240, 144);
            this.dataGridView1.TabIndex = 18;
            this.dataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentDoubleClick);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // aspectId
            // 
            this.aspectId.HeaderText = "Aspect ID";
            this.aspectId.Name = "aspectId";
            this.aspectId.Width = 79;
            // 
            // amount
            // 
            this.amount.HeaderText = "amount";
            this.amount.Name = "amount";
            this.amount.Width = 67;
            // 
            // addAspectButton
            // 
            this.addAspectButton.Location = new System.Drawing.Point(13, 332);
            this.addAspectButton.Name = "addAspectButton";
            this.addAspectButton.Size = new System.Drawing.Size(75, 23);
            this.addAspectButton.TabIndex = 19;
            this.addAspectButton.Text = "Add Aspect";
            this.addAspectButton.UseVisualStyleBackColor = true;
            this.addAspectButton.Click += new System.EventHandler(this.addAspectButton_Click);
            // 
            // CreateElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 427);
            this.Controls.Add(this.addAspectButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.slotsTreeViewLabel);
            this.Controls.Add(this.aspectListBoxLabel);
            this.Controls.Add(this.commentsTextBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.iconPictureBox);
            this.Controls.Add(this.iconSelectButton);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.labelTextBox);
            this.Controls.Add(this.idTextBox);
            this.MaximizeBox = false;
            this.Name = "CreateElement";
            this.Text = "Create Element";
            this.TopMost = true;
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.TextBox labelTextBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Button iconSelectButton;
        private System.Windows.Forms.OpenFileDialog openIconDialog;
        private System.Windows.Forms.PictureBox iconPictureBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripTextBox addAspectContextMenuItem;
        private System.Windows.Forms.ToolStripTextBox removeAspectContextMenuItem;
        private System.Windows.Forms.ToolStripTextBox changeQuantityContextMenuItem;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox commentsTextBox;
        private System.Windows.Forms.Label aspectListBoxLabel;
        private System.Windows.Forms.Label slotsTreeViewLabel;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn aspectId;
        private System.Windows.Forms.DataGridViewTextBoxColumn amount;
        private System.Windows.Forms.Button addAspectButton;
    }
}