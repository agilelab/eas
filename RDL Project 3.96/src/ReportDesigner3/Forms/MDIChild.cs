using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Printing;
using System.IO;
using System.Xml;
using fyiReporting.RDL;
using fyiReporting.RdlViewer;

namespace fyiReporting.RdlDesign
{
    partial class MDIChild : Form
    {
        public delegate void RdlChangeHandler(object sender, EventArgs e);

        public event RdlChangeHandler OnSelectionChanged;
        public event RdlChangeHandler OnSelectionMoved;
        public event RdlChangeHandler OnReportItemInserted;
        public event RdlChangeHandler OnDesignTabChanged;

        public event DesignCtl.OpenSubreportEventHandler OnOpenSubreport;
        public event DesignCtl.HeightEventHandler OnHeightChanged;

        private string _SourceFile;

        private TabPage _Tab;               // TabPage for this MDI Child

        public MDIChild()
        {
            InitializeComponent();
        }

        public MDIChild(int width, int height)
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!OkToClose())
            {
                e.Cancel = true;
                return;
            }

            if (Tab == null)
                return;

            Control ctl = Tab.Parent;
            ctl.Controls.Remove(Tab);
            Tab.Tag = null;             // this is the Tab reference to this
            Tab = null;
        }

        internal TabPage Tab
        {
            get { return _Tab; }
            set { _Tab = value; }
        }

        public RdlEditPreview Editor
        {
            get
            {
                return rdlDesigner.CanEdit ? rdlDesigner : null;	// only return when it can edit
            }
        }

        public RdlEditPreview RdlEditor
        {
            get
            {
                return rdlDesigner;			// always return
            }
        }

        public void ShowEditLines(bool bShow)
        {
            rdlDesigner.ShowEditLines(bShow);
        }

        internal bool ShowReportItemOutline
        {
            get { return rdlDesigner.ShowReportItemOutline; }
            set { rdlDesigner.ShowReportItemOutline = value; }
        }

        public string CurrentInsert
        {
            get { return rdlDesigner.CurrentInsert; }
            set
            {
                rdlDesigner.CurrentInsert = value;
            }
        }

        public int CurrentLine
        {
            get { return rdlDesigner.CurrentLine; }
        }

        public int CurrentCh
        {
            get { return rdlDesigner.CurrentCh; }
        }

        internal string DesignTab
        {
            get { return rdlDesigner.DesignTab; }
            set { rdlDesigner.DesignTab = value; }
        }

        internal DesignXmlDraw DrawCtl
        {
            get { return rdlDesigner.DrawCtl; }
        }

        public XmlDocument ReportDocument
        {
            get { return rdlDesigner.ReportDocument; }
        }

        internal void SetFocus()
        {
            rdlDesigner.SetFocus();
        }

        public StyleInfo SelectedStyle
        {
            get { return rdlDesigner.SelectedStyle; }
        }

        public string SelectionName
        {
            get { return rdlDesigner.SelectionName; }
        }

        public PointF SelectionPosition
        {
            get { return rdlDesigner.SelectionPosition; }
        }

        public SizeF SelectionSize
        {
            get { return rdlDesigner.SelectionSize; }
        }

        public void ApplyStyleToSelected(string name, string v)
        {
            rdlDesigner.ApplyStyleToSelected(name, v);
        }

        public bool FileSave()
        {
            string file = SourceFile;
            if (file == "" || file == null)			// if no file name then do SaveAs
            {
                return FileSaveAs();
            }
            string rdl = GetRdlText();

            return FileSave(file, rdl);
        }

        private bool FileSave(string file, string rdl)
        {
            StreamWriter writer = null;
            bool bOK = true;
            try
            {
                writer = new StreamWriter(file);
                writer.Write(rdl);
            }
            catch (Exception ae)
            {
                bOK = false;
                MessageBox.Show(ae.Message + "\r\n" + ae.StackTrace);
            }
            finally
            {
                writer.Close();
            }
            if (bOK)
                this.Modified = false;
            return bOK;
        }

        public bool Export(string type)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "导出为" + type.ToUpper();
            switch (type.ToLower())
            {
                case "xml":
                    sfd.Filter = "XML file (*.xml)|*.xml|All files (*.*)|*.*";
                    break;
                case "pdf":
                    sfd.Filter = "PDF file (*.pdf)|*.pdf|All files (*.*)|*.*";
                    break;
                case "excel":
                case "xlsx":
                    sfd.Filter = "Excel file (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    break;
                case "rtf":
                    sfd.Filter = "Rtf file (*.rtf)|*.rtf|All files (*.*)|*.*";
                    break;
                case "html":
                case "htm":
                    sfd.Filter = "Web Page (*.html, *.htm)|*.html;*.htm|All files (*.*)|*.*";
                    break;
                case "mhtml":
                case "mht":
                    sfd.Filter = "MHT (*.mht)|*.mhtml;*.mht|All files (*.*)|*.*";
                    break;
                default:
                    throw new Exception("只能导出为HTML, MHT, XML,EXCEL,Rft 和 PDF 文档.");
            }
            sfd.FilterIndex = 1;

            if (SourceFile != null)
                sfd.FileName = Path.GetFileNameWithoutExtension(SourceFile) + "." + type;
            else
                sfd.FileName = "*." + type;

            if (sfd.ShowDialog(this) != DialogResult.OK)
                return false;

            // save the report in the requested rendered format 
            bool rc = true;
            try
            {
                SaveAs(sfd.FileName, type);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    ex.Message, "Export Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                rc = false;
            }
            return rc;
        }

        public bool FileSaveAs()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "RDL files (*.rdl)|*.rdl|All files (*.*)|*.*";
            sfd.FilterIndex = 1;

            string file = SourceFile;

            sfd.FileName = file == null ? "*.rdl" : file;
            try
            {
                if (sfd.ShowDialog(this) != DialogResult.OK)
                    return false;

                // User wants to save!
                string rdl = GetRdlText();
                if (FileSave(sfd.FileName, rdl))
                {	// Save was successful
                    Text = sfd.FileName;
                    Tab.Text = Path.GetFileName(sfd.FileName);
                    _SourceFile = sfd.FileName;
                    Tab.ToolTipText = sfd.FileName;
                    return true;
                }
            }
            finally
            {
                sfd.Dispose();
            }
            return false;
        }

        public string GetRdlText()
        {
            return this.rdlDesigner.GetRdlText();
        }

        public bool Modified
        {
            get { return rdlDesigner.Modified; }
            set
            {
                rdlDesigner.Modified = value;
                SetTitle();
            }
        }

        /// <summary>
        /// The RDL file that should be displayed.
        /// </summary>
        public string SourceFile
        {
            get { return _SourceFile; }
            set
            {
                _SourceFile = value;
                string rdl = GetRdlSource();
                this.rdlDesigner.SetRdlText(rdl == null ? "" : rdl);
            }
        }

        public string SourceRdl
        {
            get { return this.rdlDesigner.GetRdlText(); }
            set { this.rdlDesigner.SetRdlText(value); }
        }

        private string GetRdlSource()
        {
            StreamReader fs = null;
            string prog = null;
            try
            {
                fs = new StreamReader(_SourceFile);
                prog = fs.ReadToEnd();
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }

            return prog;
        }

        /// <summary>
        /// Number of pages in the report.
        /// </summary>
        public int PageCount
        {
            get { return this.rdlDesigner.PageCount; }
        }

        /// <summary>
        /// Current page in view on report
        /// </summary>
        public int PageCurrent
        {
            get { return this.rdlDesigner.PageCurrent; }
        }

        /// <summary>
        /// Page height of the report.
        /// </summary>
        public float PageHeight
        {
            get { return this.rdlDesigner.PageHeight; }
        }
        /// <summary>
        /// Turns the Selection Tool on in report preview
        /// </summary>
        public bool SelectionTool
        {
            get
            {
                return this.rdlDesigner.SelectionTool;
            }
            set
            {
                this.rdlDesigner.SelectionTool = value;
            }
        }

        /// <summary>
        /// Page width of the report.
        /// </summary>
        public float PageWidth
        {
            get { return this.rdlDesigner.PageWidth; }
        }

        /// <summary>
        /// Zoom 
        /// </summary>
        public float Zoom
        {
            get { return this.rdlDesigner.Zoom; }
            set { this.rdlDesigner.Zoom = value; }
        }

        /// <summary>
        /// ZoomMode 
        /// </summary>
        public ZoomEnum ZoomMode
        {
            get { return this.rdlDesigner.ZoomMode; }
            set { this.rdlDesigner.ZoomMode = value; }
        }

        /// <summary>
        /// Print the report.  
        /// </summary>
        public void Print(PrintDocument pd)
        {
            this.rdlDesigner.Print(pd);
        }

        public void SaveAs(string filename, string ext)
        {
            rdlDesigner.SaveAs(filename, ext);
        }

        public bool OkToClose()
        {
            if (!this.Modified)
                return true;

            DialogResult r =
                    MessageBox.Show(this, String.Format("是否保存修改到'{0}'?",
                    _SourceFile == null ? "Untitled" : Path.GetFileName(_SourceFile)),
                    "报表设计器",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3);

            bool bOK = true;
            if (r == DialogResult.Cancel)
                bOK = false;
            else if (r == DialogResult.Yes)
            {
                if (!FileSave())
                    bOK = false;
            }
            return bOK;
        }

        private void rdlDesigner_RdlChanged(object sender, System.EventArgs e)
        {
            SetTitle();
        }

        private void rdlDesigner_HeightChanged(object sender, HeightEventArgs e)
        {
            if (OnHeightChanged != null)
                OnHeightChanged(this, e);
        }

        private void rdlDesigner_SelectionChanged(object sender, System.EventArgs e)
        {
            if (OnSelectionChanged != null)
                OnSelectionChanged(this, e);
        }

        private void rdlDesigner_DesignTabChanged(object sender, System.EventArgs e)
        {
            if (OnDesignTabChanged != null)
                OnDesignTabChanged(this, e);
        }

        private void rdlDesigner_ReportItemInserted(object sender, System.EventArgs e)
        {
            if (OnReportItemInserted != null)
                OnReportItemInserted(this, e);
        }

        private void rdlDesigner_SelectionMoved(object sender, System.EventArgs e)
        {
            if (OnSelectionMoved != null)
                OnSelectionMoved(this, e);
        }

        private void rdlDesigner_OpenSubreport(object sender, SubReportEventArgs e)
        {
            if (OnOpenSubreport != null)
            {
                OnOpenSubreport(this, e);
            }
        }

        private void SetTitle()
        {
            string title = this.Text;
            if (title.Length < 1)
                return;
            char m = title[title.Length - 1];
            if (this.Modified)
            {
                if (m != '*')
                    title = title + "*";
            }
            else if (m == '*')
                title = title.Substring(0, title.Length - 1);

            if (title != this.Text)
                this.Text = title;
            return;
        }

        public fyiReporting.RdlViewer.RdlViewer Viewer
        {
            get { return rdlDesigner.Viewer; }
        }
    }
}