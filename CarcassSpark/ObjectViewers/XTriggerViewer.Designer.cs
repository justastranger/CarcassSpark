namespace CarcassSpark.ObjectViewers
{
    partial class XTriggerViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XTriggerViewer));
            this.catalystTextBox = new System.Windows.Forms.TextBox();
            this.catalystLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.xtriggersDataGridView = new System.Windows.Forms.DataGridView();
            this.xtriggersResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xtriggersChance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xtriggersLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xtriggersMorphEffect = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.xTriggersLabel = new System.Windows.Forms.Label();
            this.deleteButton = new System.Windows.Forms.Button();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.xtriggersDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // catalystTextBox
            // 
            this.catalystTextBox.Location = new System.Drawing.Point(12, 25);
            this.catalystTextBox.Name = "catalystTextBox";
            this.catalystTextBox.Size = new System.Drawing.Size(120, 20);
            this.catalystTextBox.TabIndex = 0;
            this.ToolTip.SetToolTip(this.catalystTextBox, "This is the element or aspect ID that will trigger the following XTriggers when a" +
        " recipe concludes.");
            this.catalystTextBox.TextChanged += new System.EventHandler(this.CatalystTextBox_TextChanged);
            // 
            // catalystLabel
            // 
            this.catalystLabel.AutoSize = true;
            this.catalystLabel.Location = new System.Drawing.Point(12, 9);
            this.catalystLabel.Name = "catalystLabel";
            this.catalystLabel.Size = new System.Drawing.Size(44, 13);
            this.catalystLabel.TabIndex = 1;
            this.catalystLabel.Text = "Catalyst";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(12, 267);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 10;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(379, 267);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // xtriggersDataGridView
            // 
            this.xtriggersDataGridView.AllowUserToResizeColumns = false;
            this.xtriggersDataGridView.AllowUserToResizeRows = false;
            this.xtriggersDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xtriggersDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.xtriggersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.xtriggersDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.xtriggersResult,
            this.xtriggersChance,
            this.xtriggersLevel,
            this.xtriggersMorphEffect});
            this.xtriggersDataGridView.Location = new System.Drawing.Point(12, 51);
            this.xtriggersDataGridView.Name = "xtriggersDataGridView";
            this.xtriggersDataGridView.Size = new System.Drawing.Size(442, 210);
            this.xtriggersDataGridView.TabIndex = 12;
            this.ToolTip.SetToolTip(this.xtriggersDataGridView, "These are the XTrigger effects that are triggered in response to exposure to the " +
        "catalyst during the conclusion of a recipe.");
            // 
            // xtriggersResult
            // 
            this.xtriggersResult.HeaderText = "Result";
            this.xtriggersResult.Name = "xtriggersResult";
            this.xtriggersResult.ToolTipText = "This is the element or aspect ID to target with this XTrigger";
            // 
            // xtriggersChance
            // 
            this.xtriggersChance.HeaderText = "Chance";
            this.xtriggersChance.Name = "xtriggersChance";
            this.xtriggersChance.ToolTipText = "This is the chance the XTrigger will be executed";
            // 
            // xtriggersLevel
            // 
            this.xtriggersLevel.HeaderText = "Level";
            this.xtriggersLevel.Name = "xtriggersLevel";
            this.xtriggersLevel.ToolTipText = "When spawning or mutating, this is the amount that it\'s done by.";
            // 
            // xtriggersMorphEffect
            // 
            this.xtriggersMorphEffect.HeaderText = "Morph Effect";
            this.xtriggersMorphEffect.Items.AddRange(new object[] {
            "transform",
            "mutate",
            "spawn"});
            this.xtriggersMorphEffect.Name = "xtriggersMorphEffect";
            this.xtriggersMorphEffect.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.xtriggersMorphEffect.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.xtriggersMorphEffect.ToolTipText = "Determines the type of XTrigger. Transform changes one card into another, preserv" +
    "ing mutations, spawn creates new cards, and mutate allows for mutations to be ap" +
    "plied.";
            // 
            // xTriggersLabel
            // 
            this.xTriggersLabel.AutoSize = true;
            this.xTriggersLabel.Location = new System.Drawing.Point(207, 35);
            this.xTriggersLabel.Name = "xTriggersLabel";
            this.xTriggersLabel.Size = new System.Drawing.Size(52, 13);
            this.xTriggersLabel.TabIndex = 13;
            this.xTriggersLabel.Text = "XTriggers";
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.deleteButton.Location = new System.Drawing.Point(196, 267);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 14;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // XTriggerViewer
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(466, 302);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.xTriggersLabel);
            this.Controls.Add(this.xtriggersDataGridView);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.catalystLabel);
            this.Controls.Add(this.catalystTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(482, 341);
            this.Name = "XTriggerViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XTrigger Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.xtriggersDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox catalystTextBox;
        private System.Windows.Forms.Label catalystLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.DataGridView xtriggersDataGridView;
        private System.Windows.Forms.Label xTriggersLabel;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.ToolTip ToolTip;
        private System.Windows.Forms.DataGridViewTextBoxColumn xtriggersResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn xtriggersChance;
        private System.Windows.Forms.DataGridViewTextBoxColumn xtriggersLevel;
        private System.Windows.Forms.DataGridViewComboBoxColumn xtriggersMorphEffect;
    }
}