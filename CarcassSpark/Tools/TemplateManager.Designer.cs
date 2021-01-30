using ScintillaNET;
using System.Drawing;

namespace CarcassSpark.Tools
{
    partial class TemplateManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemplateManager));
            this.templatesListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.scintilla1 = new ScintillaNET.Scintilla();
            this.newTemplateButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.selectButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // templatesListView
            // 
            this.templatesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.templatesListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.templatesListView.HideSelection = false;
            this.templatesListView.Location = new System.Drawing.Point(12, 12);
            this.templatesListView.MultiSelect = false;
            this.templatesListView.Name = "templatesListView";
            this.templatesListView.Size = new System.Drawing.Size(183, 397);
            this.templatesListView.TabIndex = 0;
            this.templatesListView.UseCompatibleStateImageBehavior = false;
            this.templatesListView.View = System.Windows.Forms.View.Details;
            this.templatesListView.SelectedIndexChanged += new System.EventHandler(this.TemplatesListView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 175;
            // 
            // scintilla1
            // 
            this.scintilla1.Lexer = ScintillaNET.Lexer.Json;
            this.scintilla1.Location = new System.Drawing.Point(201, 12);
            this.scintilla1.Name = "scintilla1";
            this.scintilla1.Size = new System.Drawing.Size(587, 397);
            this.scintilla1.TabIndex = 1;
            this.scintilla1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Scintilla1_KeyPress);
            // 
            // newTemplateButton
            // 
            this.newTemplateButton.Location = new System.Drawing.Point(12, 415);
            this.newTemplateButton.Name = "newTemplateButton";
            this.newTemplateButton.Size = new System.Drawing.Size(88, 23);
            this.newTemplateButton.TabIndex = 2;
            this.newTemplateButton.Text = "New Template";
            this.newTemplateButton.UseVisualStyleBackColor = true;
            this.newTemplateButton.Click += new System.EventHandler(this.NewTemplateButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(107, 415);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(88, 23);
            this.deleteButton.TabIndex = 3;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(201, 415);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(713, 415);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 5;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // selectButton
            // 
            this.selectButton.Location = new System.Drawing.Point(613, 415);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(94, 23);
            this.selectButton.TabIndex = 6;
            this.selectButton.Text = "Select Template";
            this.selectButton.UseVisualStyleBackColor = true;
            this.selectButton.Visible = false;
            this.selectButton.Click += new System.EventHandler(this.SelectButton_Click);
            // 
            // TemplateManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.selectButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.newTemplateButton);
            this.Controls.Add(this.scintilla1);
            this.Controls.Add(this.templatesListView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TemplateManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Template Manager";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView templatesListView;
        private System.Windows.Forms.Button newTemplateButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button selectButton;
        public Scintilla scintilla1;
    }
}