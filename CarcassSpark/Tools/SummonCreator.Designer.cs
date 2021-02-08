namespace CarcassSpark.Tools
{
    partial class SummonCreator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SummonCreator));
            this.createBaseElementButton = new System.Windows.Forms.Button();
            this.baseIdTextBox = new System.Windows.Forms.TextBox();
            this.baseElementLlabel = new System.Windows.Forms.Label();
            this.preSummonElementIdLabel = new System.Windows.Forms.Label();
            this.preSummonIdTextBox = new System.Windows.Forms.TextBox();
            this.inspectBaseButton = new System.Windows.Forms.Button();
            this.inspectPreButton = new System.Windows.Forms.Button();
            this.createRecipeButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.baseSummonIdTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.successSummonTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.inspectBaseRecipeButton = new System.Windows.Forms.Button();
            this.inspectSuccessRecipeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // createBaseElementButton
            // 
            this.createBaseElementButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.createBaseElementButton.Location = new System.Drawing.Point(12, 12);
            this.createBaseElementButton.Name = "createBaseElementButton";
            this.createBaseElementButton.Size = new System.Drawing.Size(260, 37);
            this.createBaseElementButton.TabIndex = 0;
            this.createBaseElementButton.Text = "Create Base Element";
            this.createBaseElementButton.UseVisualStyleBackColor = true;
            this.createBaseElementButton.Click += new System.EventHandler(this.CreateBaseElementButton_click);
            // 
            // baseIdTextBox
            // 
            this.baseIdTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.baseIdTextBox.Location = new System.Drawing.Point(12, 68);
            this.baseIdTextBox.Name = "baseIdTextBox";
            this.baseIdTextBox.ReadOnly = true;
            this.baseIdTextBox.Size = new System.Drawing.Size(156, 20);
            this.baseIdTextBox.TabIndex = 1;
            // 
            // baseElementLlabel
            // 
            this.baseElementLlabel.AutoSize = true;
            this.baseElementLlabel.Location = new System.Drawing.Point(12, 52);
            this.baseElementLlabel.Name = "baseElementLlabel";
            this.baseElementLlabel.Size = new System.Drawing.Size(86, 13);
            this.baseElementLlabel.TabIndex = 2;
            this.baseElementLlabel.Text = "Base Element ID";
            // 
            // preSummonElementIdLabel
            // 
            this.preSummonElementIdLabel.AutoSize = true;
            this.preSummonElementIdLabel.Location = new System.Drawing.Point(12, 91);
            this.preSummonElementIdLabel.Name = "preSummonElementIdLabel";
            this.preSummonElementIdLabel.Size = new System.Drawing.Size(122, 13);
            this.preSummonElementIdLabel.TabIndex = 3;
            this.preSummonElementIdLabel.Text = "Pre-Summon Element ID";
            // 
            // preSummonIdTextBox
            // 
            this.preSummonIdTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.preSummonIdTextBox.Location = new System.Drawing.Point(12, 107);
            this.preSummonIdTextBox.Name = "preSummonIdTextBox";
            this.preSummonIdTextBox.ReadOnly = true;
            this.preSummonIdTextBox.Size = new System.Drawing.Size(156, 20);
            this.preSummonIdTextBox.TabIndex = 4;
            // 
            // inspectBaseButton
            // 
            this.inspectBaseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.inspectBaseButton.Enabled = false;
            this.inspectBaseButton.Location = new System.Drawing.Point(174, 66);
            this.inspectBaseButton.Name = "inspectBaseButton";
            this.inspectBaseButton.Size = new System.Drawing.Size(98, 23);
            this.inspectBaseButton.TabIndex = 5;
            this.inspectBaseButton.Text = "Inspect Element";
            this.inspectBaseButton.UseVisualStyleBackColor = true;
            this.inspectBaseButton.Click += new System.EventHandler(this.InspectBaseButton_Click);
            // 
            // inspectPreButton
            // 
            this.inspectPreButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.inspectPreButton.Enabled = false;
            this.inspectPreButton.Location = new System.Drawing.Point(174, 105);
            this.inspectPreButton.Name = "inspectPreButton";
            this.inspectPreButton.Size = new System.Drawing.Size(98, 23);
            this.inspectPreButton.TabIndex = 6;
            this.inspectPreButton.Text = "Inspect Element";
            this.inspectPreButton.UseVisualStyleBackColor = true;
            this.inspectPreButton.Click += new System.EventHandler(this.InspectPreButton_Click);
            // 
            // createRecipeButton
            // 
            this.createRecipeButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.createRecipeButton.Enabled = false;
            this.createRecipeButton.Location = new System.Drawing.Point(12, 133);
            this.createRecipeButton.Name = "createRecipeButton";
            this.createRecipeButton.Size = new System.Drawing.Size(260, 37);
            this.createRecipeButton.TabIndex = 7;
            this.createRecipeButton.Text = "Create Base Summon Recipe";
            this.createRecipeButton.UseVisualStyleBackColor = true;
            this.createRecipeButton.Click += new System.EventHandler(this.CreateRecipeButton_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 173);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Base Summon Recipe";
            // 
            // baseSummonIdTextBox
            // 
            this.baseSummonIdTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.baseSummonIdTextBox.Location = new System.Drawing.Point(12, 189);
            this.baseSummonIdTextBox.Name = "baseSummonIdTextBox";
            this.baseSummonIdTextBox.ReadOnly = true;
            this.baseSummonIdTextBox.Size = new System.Drawing.Size(156, 20);
            this.baseSummonIdTextBox.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 212);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Success Recipe";
            // 
            // successSummonTextBox
            // 
            this.successSummonTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.successSummonTextBox.Location = new System.Drawing.Point(12, 228);
            this.successSummonTextBox.Name = "successSummonTextBox";
            this.successSummonTextBox.ReadOnly = true;
            this.successSummonTextBox.Size = new System.Drawing.Size(156, 20);
            this.successSummonTextBox.TabIndex = 11;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.Location = new System.Drawing.Point(12, 254);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(130, 37);
            this.okButton.TabIndex = 12;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(142, 254);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(130, 37);
            this.cancelButton.TabIndex = 13;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // inspectBaseRecipeButton
            // 
            this.inspectBaseRecipeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.inspectBaseRecipeButton.Enabled = false;
            this.inspectBaseRecipeButton.Location = new System.Drawing.Point(174, 187);
            this.inspectBaseRecipeButton.Name = "inspectBaseRecipeButton";
            this.inspectBaseRecipeButton.Size = new System.Drawing.Size(98, 23);
            this.inspectBaseRecipeButton.TabIndex = 14;
            this.inspectBaseRecipeButton.Text = "Inspect Recipe";
            this.inspectBaseRecipeButton.UseVisualStyleBackColor = true;
            this.inspectBaseRecipeButton.Click += new System.EventHandler(this.InspectBaseRecipeButton_Click);
            // 
            // inspectSuccessRecipeButton
            // 
            this.inspectSuccessRecipeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.inspectSuccessRecipeButton.Enabled = false;
            this.inspectSuccessRecipeButton.Location = new System.Drawing.Point(174, 226);
            this.inspectSuccessRecipeButton.Name = "inspectSuccessRecipeButton";
            this.inspectSuccessRecipeButton.Size = new System.Drawing.Size(98, 23);
            this.inspectSuccessRecipeButton.TabIndex = 15;
            this.inspectSuccessRecipeButton.Text = "Inspect Recipe";
            this.inspectSuccessRecipeButton.UseVisualStyleBackColor = true;
            this.inspectSuccessRecipeButton.Click += new System.EventHandler(this.InspectSuccessRecipeButton_Click);
            // 
            // SummonCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(284, 303);
            this.Controls.Add(this.inspectSuccessRecipeButton);
            this.Controls.Add(this.inspectBaseRecipeButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.successSummonTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.baseSummonIdTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.createRecipeButton);
            this.Controls.Add(this.inspectPreButton);
            this.Controls.Add(this.inspectBaseButton);
            this.Controls.Add(this.preSummonIdTextBox);
            this.Controls.Add(this.preSummonElementIdLabel);
            this.Controls.Add(this.baseElementLlabel);
            this.Controls.Add(this.baseIdTextBox);
            this.Controls.Add(this.createBaseElementButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(300, 342);
            this.Name = "SummonCreator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Summon Creator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button createBaseElementButton;
        private System.Windows.Forms.TextBox baseIdTextBox;
        private System.Windows.Forms.Label baseElementLlabel;
        private System.Windows.Forms.Label preSummonElementIdLabel;
        private System.Windows.Forms.TextBox preSummonIdTextBox;
        private System.Windows.Forms.Button inspectBaseButton;
        private System.Windows.Forms.Button inspectPreButton;
        private System.Windows.Forms.Button createRecipeButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox baseSummonIdTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox successSummonTextBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button inspectBaseRecipeButton;
        private System.Windows.Forms.Button inspectSuccessRecipeButton;
    }
}