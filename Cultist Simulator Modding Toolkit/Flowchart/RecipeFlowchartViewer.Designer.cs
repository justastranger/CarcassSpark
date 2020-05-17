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
            this.zoomControl1 = new MindFusion.Common.WinForms.ZoomControl();
            this.treeLayout1 = new MindFusion.Diagramming.Layout.TreeLayout();
            this.SuspendLayout();
            // 
            // diagram1
            // 
            this.diagram1.AutoAlignNodes = true;
            this.diagram1.LinkCrossings = MindFusion.Diagramming.LinkCrossings.Arcs;
            this.diagram1.LinkHeadShape = MindFusion.Diagramming.Shape.FromId("PointerArrow");
            this.diagram1.ShapeText = "Node";
            this.diagram1.TouchThreshold = 0F;
            this.diagram1.TreeViewNodeStyle.Brush = new MindFusion.Drawing.SolidBrush("#FF9ACD32");
            // 
            // diagramView1
            // 
            this.diagramView1.AllowInplaceEdit = true;
            this.diagramView1.Behavior = MindFusion.Diagramming.Behavior.Modify;
            this.diagramView1.Diagram = this.diagram1;
            this.diagramView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diagramView1.LicenseKey = "asdf";
            this.diagramView1.Location = new System.Drawing.Point(0, 0);
            this.diagramView1.Name = "diagramView1";
            this.diagramView1.Size = new System.Drawing.Size(676, 412);
            this.diagramView1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.diagramView1.TabIndex = 0;
            this.diagramView1.Text = "diagramView1";
            // 
            // zoomControl1
            // 
            this.zoomControl1.BackColor = System.Drawing.Color.Transparent;
            this.zoomControl1.Location = new System.Drawing.Point(12, 12);
            this.zoomControl1.Name = "zoomControl1";
            this.zoomControl1.Padding = new System.Windows.Forms.Padding(5);
            this.zoomControl1.Size = new System.Drawing.Size(59, 388);
            this.zoomControl1.TabIndex = 1;
            this.zoomControl1.Target = this.diagramView1;
            this.zoomControl1.TickPosition = MindFusion.Common.WinForms.TickPosition.Left;
            // 
            // RecipeFlowchartViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 412);
            this.Controls.Add(this.zoomControl1);
            this.Controls.Add(this.diagramView1);
            this.Name = "RecipeFlowchartViewer";
            this.Text = "RecipeFlowchartViewer";
            this.Load += new System.EventHandler(this.RecipeFlowchartViewer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private MindFusion.Diagramming.Diagram diagram1;
        private MindFusion.Diagramming.WinForms.DiagramView diagramView1;
        private MindFusion.Common.WinForms.ZoomControl zoomControl1;
        private MindFusion.Diagramming.Layout.TreeLayout treeLayout1;
    }
}