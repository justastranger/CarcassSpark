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
            ((System.ComponentModel.ISupportInitialize)(this.xtriggersDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // catalystTextBox
            // 
            this.catalystTextBox.Location = new System.Drawing.Point(12, 25);
            this.catalystTextBox.Name = "catalystTextBox";
            this.catalystTextBox.Size = new System.Drawing.Size(120, 20);
            this.catalystTextBox.TabIndex = 0;
            this.catalystTextBox.TextChanged += new System.EventHandler(this.catalystTextBox_TextChanged);
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
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(12, 270);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(33, 23);
            this.okButton.TabIndex = 10;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(405, 270);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(49, 23);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // xtriggersDataGridView
            // 
            this.xtriggersDataGridView.AllowUserToResizeColumns = false;
            this.xtriggersDataGridView.AllowUserToResizeRows = false;
            this.xtriggersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.xtriggersDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.xtriggersResult,
            this.xtriggersChance,
            this.xtriggersLevel,
            this.xtriggersMorphEffect});
            this.xtriggersDataGridView.Location = new System.Drawing.Point(12, 51);
            this.xtriggersDataGridView.Name = "xtriggersDataGridView";
            this.xtriggersDataGridView.Size = new System.Drawing.Size(442, 213);
            this.xtriggersDataGridView.TabIndex = 12;
            // 
            // xtriggersResult
            // 
            this.xtriggersResult.HeaderText = "Result";
            this.xtriggersResult.Name = "xtriggersResult";
            // 
            // xtriggersChance
            // 
            this.xtriggersChance.HeaderText = "Chance";
            this.xtriggersChance.Name = "xtriggersChance";
            // 
            // xtriggersLevel
            // 
            this.xtriggersLevel.HeaderText = "Level";
            this.xtriggersLevel.Name = "xtriggersLevel";
            this.xtriggersLevel.Width = 99;
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
            // 
            // xTriggersLabel
            // 
            this.xTriggersLabel.AutoSize = true;
            this.xTriggersLabel.Location = new System.Drawing.Point(194, 35);
            this.xTriggersLabel.Name = "xTriggersLabel";
            this.xTriggersLabel.Size = new System.Drawing.Size(52, 13);
            this.xTriggersLabel.TabIndex = 13;
            this.xTriggersLabel.Text = "XTriggers";
            // 
            // XTriggerViewer
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 302);
            this.Controls.Add(this.xTriggersLabel);
            this.Controls.Add(this.xtriggersDataGridView);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.catalystLabel);
            this.Controls.Add(this.catalystTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "XTriggerViewer";
            this.Text = "XTriggerViewer";
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
        private System.Windows.Forms.DataGridViewTextBoxColumn xtriggersResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn xtriggersChance;
        private System.Windows.Forms.DataGridViewTextBoxColumn xtriggersLevel;
        private System.Windows.Forms.DataGridViewComboBoxColumn xtriggersMorphEffect;
    }
}