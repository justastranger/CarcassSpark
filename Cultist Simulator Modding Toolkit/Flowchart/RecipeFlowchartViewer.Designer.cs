namespace CultistSimulatorModdingToolkit
{
    partial class RecipeFlowchartViewer
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
            this.diagram1 = new MindFusion.Diagramming.Diagram();
            this.diagramView1 = new MindFusion.Diagramming.WinForms.DiagramView();
            this.SuspendLayout();
            // 
            // diagram1
            // 
            this.diagram1.LinkText = "Link";
            this.diagram1.TouchThreshold = 0F;
            // 
            // diagramView1
            // 
            this.diagramView1.Diagram = this.diagram1;
            this.diagramView1.LicenseKey = null;
            this.diagramView1.Location = new System.Drawing.Point(12, 12);
            this.diagramView1.Name = "diagramView1";
            this.diagramView1.Size = new System.Drawing.Size(652, 388);
            this.diagramView1.TabIndex = 0;
            this.diagramView1.Text = "diagramView1";
            // 
            // RecipeFlowchartViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 412);
            this.Controls.Add(this.diagramView1);
            this.Name = "RecipeFlowchartViewer";
            this.Text = "RecipeFlowchartViewer";
            this.ResumeLayout(false);

        }

        #endregion

        private MindFusion.Diagramming.Diagram diagram1;
        private MindFusion.Diagramming.WinForms.DiagramView diagramView1;
    }
}