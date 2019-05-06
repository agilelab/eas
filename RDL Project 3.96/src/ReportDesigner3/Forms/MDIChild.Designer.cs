namespace fyiReporting.RdlDesign
{
    partial class MDIChild
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDIChild));
            this.rdlDesigner = new fyiReporting.RdlDesign.RdlEditPreview();
            this.SuspendLayout();
            // 
            // rdlDesigner
            // 
            this.rdlDesigner.CurrentInsert = null;
            this.rdlDesigner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdlDesigner.Location = new System.Drawing.Point(0, 0);
            this.rdlDesigner.Modified = false;
            this.rdlDesigner.Name = "rdlDesigner";
            this.rdlDesigner.SelectedText = "";
            this.rdlDesigner.SelectionTool = false;
            this.rdlDesigner.Size = new System.Drawing.Size(722, 295);
            this.rdlDesigner.TabIndex = 0;
            this.rdlDesigner.Zoom = 1F;
            this.rdlDesigner.ZoomMode = fyiReporting.RdlViewer.ZoomEnum.UseZoom;
            this.rdlDesigner.OnSelectionMoved += new fyiReporting.RdlDesign.RdlEditPreview.RdlChangeHandler(this.rdlDesigner_SelectionMoved);
            this.rdlDesigner.OnDesignTabChanged += new fyiReporting.RdlDesign.RdlEditPreview.RdlChangeHandler(this.rdlDesigner_DesignTabChanged);
            this.rdlDesigner.OnOpenSubreport += new fyiReporting.RdlDesign.DesignCtl.OpenSubreportEventHandler(this.rdlDesigner_OpenSubreport);
            this.rdlDesigner.OnSelectionChanged += new fyiReporting.RdlDesign.RdlEditPreview.RdlChangeHandler(this.rdlDesigner_SelectionChanged);
            this.rdlDesigner.OnRdlChanged += new fyiReporting.RdlDesign.RdlEditPreview.RdlChangeHandler(this.rdlDesigner_RdlChanged);
            this.rdlDesigner.OnReportItemInserted += new fyiReporting.RdlDesign.RdlEditPreview.RdlChangeHandler(this.rdlDesigner_ReportItemInserted);
            // 
            // MDIChild
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 295);
            this.Controls.Add(this.rdlDesigner);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MDIChild";
            this.ResumeLayout(false);

        }

        #endregion

        private RdlEditPreview rdlDesigner;
    }
}