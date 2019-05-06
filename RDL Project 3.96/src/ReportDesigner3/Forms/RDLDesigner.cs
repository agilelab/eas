using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Xml;
using fyiReporting.RdlViewer;
using fyiReporting.RDL;
using System.Globalization;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
//using EAS.Windows.UI.Controls;

namespace fyiReporting.RdlDesign
{
    /// <summary>
    /// 报表设计器。
    /// </summary>
    public partial class RDLDesigner : Form//,IReportDesigner
    {       
        /// <summary>
        /// 内部私有变量，默认帮助内容地址。
        /// </summary>
        private readonly string DefaultHelpUrl = "http://eastjade.cnblogs.com/";

        /// <summary>
        /// 内部私有变量，内容地址。
        /// </summary>
        private string _HelpUrl;

        /// <summary>
        /// 内部私有变量，工具条是否锁定。
        /// </summary>
        private bool bSuppressChange = false;

        /// <summary>
        /// 内部私有变量，当前插入按钮。
        /// </summary>
        private ToolStripButton btnInsertCurrent = null;

        /// <summary>
        /// 内部私有变量，插入按钮集合。
        /// </summary>
        private ArrayList InsertButtonList = null;

        private RDL.NeedPassword _GetPassword;
        private string _DataSourceReferencePassword = null;
        private bool bGotPassword = false;

        private bool _ShowProperties = false;

        /// <summary>
        /// 报表名称。
        /// </summary>
        private string rdlName = string.Empty;

        /// <summary>
        /// 保存事件。
        /// </summary>
        private string m_QueryText = string.Empty;
        private string m_ConnectionString = string.Empty;

        public RDLDesigner()
        {
            InitializeComponent();

            _GetPassword = new RDL.NeedPassword(this.GetPassword);

            this.Initialize();
        }
        
        /// <summary>
        /// 初始化报表设计器相关参数。
        /// </summary>
        private void Initialize()
        {
            this.cbxFont.Items.Clear();

            foreach (FontFamily ff in FontFamily.Families)
            {
                this.cbxFont.Items.Add(ff.Name);
            }

            string[] sizes = new string[] { "8", "9", "10", "11", "12", "14", "16", "18", "20", "22", "24", "26", "28", "36", "48", "72" };
            this.cbxFontSize.Items.Clear();
            this.cbxFontSize.Items.AddRange(sizes);

            this.cbxZoom.Items.Clear();
            this.cbxZoom.Items.AddRange(StaticLists.ZoomList);      

            this.InsertButtonList = new ArrayList();

            this.InsertButtonList.Add(this.btnTextBox);
            this.InsertButtonList.Add(this.btnChart);
            this.InsertButtonList.Add(this.btnTable);
            this.InsertButtonList.Add(this.btnList);
            this.InsertButtonList.Add(this.btnImage);
            this.InsertButtonList.Add(this.btnMatrix);
            this.InsertButtonList.Add(this.btnSubReport);
            this.InsertButtonList.Add(this.btnRect);
            this.InsertButtonList.Add(this.btnLine);

			DesignTabChanged(this, new EventArgs());
        }

        private bool OkToSave()
        {            
            return true;
        }

        private void EnableEditTextBox()
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            bool bEnable = false;

            if (this.tbFunction == null || mc == null ||
                mc.DesignTab != "design" || mc.DrawCtl.SelectedCount != 1)
            { }
            else
            {
                XmlNode tn = mc.DrawCtl.SelectedList[0] as XmlNode;
                if (tn != null && tn.Name == "Textbox")
                {
                    this.tbFunction.Text = mc.DrawCtl.GetElementValue(tn, "Value", "");
                    bEnable = true;
                }
            }
            if (this.tbFunction != null)
            {
                if (!bEnable)
                    tbFunction.Text = "";
                this.tbFunction.Enabled = bEnable;
                this.btnFun.Enabled = bEnable;
            }
        }

        internal string HelpUrl
        {
            get
            {
                if (_HelpUrl != null && _HelpUrl.Length > 0)
                    return _HelpUrl;
                return DefaultHelpUrl;
            }
            set
            {
                _HelpUrl = value.Length > 0 ? value : DefaultHelpUrl;
            }
        }

        /// <summary>
        /// 报表名称。
        /// </summary>
        public string RdlName
        {
            get
            {
                return this.rdlName;
            }
            set
            {
                this.rdlName = value;
            }
        }

        /// <summary>
        /// 报表定义。
        /// </summary>
        public string SourceRdl
        {
            get
            {
                return this.RdlEditor.GetRdlText();
            }
            set
            {
                if(value.Length > 0)
                    this.RdlEditor.SetRdlText(value);
            }
        }

        #region MDIChild2

        private void DesignTabChanged(object sender, System.EventArgs e)
        {
            string tab  = this.RdlEditor.DesignTab;

            bool bEnableEdit = false;
            bool bEnableDesign = false;
            bool bEnablePreview = false;

            switch (tab)
            {
                case "edit":
                    bEnableEdit = true;
                    break;
                case "design":
                    bEnableDesign = true;
                    break;
                case "preview":
                    bEnablePreview = true;
                    break;
            }

            if (this.btnBold != null)
                this.btnBold.Enabled = bEnableDesign;
            if (this.btnItalic != null)
                this.btnItalic.Enabled = bEnableDesign;
            if (this.btnUnderLine != null)
                this.btnUnderLine.Enabled = bEnableDesign;
            if (this.cbxFont != null)
                this.cbxFont.Enabled = bEnableDesign;
            if (this.cbxFontSize != null)
                this.cbxFontSize.Enabled = bEnableDesign;
            if (this.btnForeColor != null)
                this.btnForeColor.Enabled = bEnableDesign;
            if (this.btnBackColor != null)
                this.btnBackColor.Enabled = bEnableDesign;
            if (this.btnCut != null)
                this.btnCut.Enabled = bEnableDesign | bEnableEdit;
            if (this.btnCopy != null)
                this.btnCopy.Enabled = bEnableDesign | bEnableEdit;
            if (this.btnUndo != null)
                this.btnUndo.Enabled = bEnableDesign | bEnableEdit;
            if (this.btnPaste != null)
                this.btnPaste.Enabled = bEnableDesign | bEnableEdit;
            if (this.btnPrint != null)
                this.btnPrint.Enabled = bEnablePreview;

            this.btnAlignLeft.Enabled = bEnableDesign;
            this.btnAlignCenter.Enabled = bEnableDesign;
            this.btnAlignRight.Enabled = bEnableDesign;

            this.btnForeColor.Enabled = bEnableDesign;
            this.btnBackColor.Enabled = bEnableDesign;

            if (this.btnTextBox != null)
                this.btnTextBox.Enabled = bEnableDesign;
            if (this.btnChart != null)
                this.btnChart.Enabled = bEnableDesign;
            if (this.btnRect != null)
                this.btnRect.Enabled = bEnableDesign;
            if (this.btnTable != null)
                this.btnTable.Enabled = bEnableDesign;
            if (this.btnMatrix != null)
                this.btnMatrix.Enabled = bEnableDesign;
            if (this.btnList != null)
                this.btnList.Enabled = bEnableDesign;
            if (this.btnLine != null)
                this.btnLine.Enabled = bEnableDesign;
            if (this.btnImage != null)
                this.btnImage.Enabled = bEnableDesign;
            if (this.btnSubReport != null)
                this.btnSubReport.Enabled = bEnableDesign;
            if (this.btnPDF != null)
                this.btnPDF.Enabled = bEnablePreview;
            if (this.btnXML != null)
                this.btnXML.Enabled = bEnablePreview;
            if (this.btnHtml != null)
                this.btnHtml.Enabled = bEnablePreview;
            if (this.btnMHT != null)
                this.btnMHT.Enabled = bEnablePreview;

            this.EnableEditTextBox();

            if (this.cbxZoom != null)
            {
                this.cbxZoom.Enabled = bEnablePreview;
                string zText = "Actual Size";

                switch (this.RdlEditor.ZoomMode)
                {
                    case ZoomEnum.FitWidth:
                        zText = "Fit Width";
                        break;
                    case ZoomEnum.FitPage:
                        zText = "Fit Page";
                        break;
                    case ZoomEnum.UseZoom:
                        if (this.RdlEditor.Zoom == 1)
                            zText = "Actual Size";
                        else
                            zText = string.Format("{0:0}", this.RdlEditor.Zoom * 100f);
                        break;
                }
                this.cbxZoom.Text = zText;
            }
            // when no active sheet
            if (this.btnSave != null)
                this.btnSave.Enabled = true;

            // Update the status and position information
            SetStatusNameAndPosition();
        }

        private void HeightChanged(object sender, HeightEventArgs e)
        {
            if (e.Height == null)
            {
                statusPosition.Text = "";
                return;
            }

            RegionInfo rinfo = new RegionInfo(CultureInfo.CurrentCulture.LCID);
            float h = DesignXmlDraw.GetSize(e.Height);
            string sh;
            if (rinfo.IsMetric)
            {
                sh = string.Format("   height={0:0.00}cm        ",
                        h / (DesignXmlDraw.POINTSIZED / 2.54d));
            }
            else
            {
                sh = string.Format("   height={0:0.00}\"        ",
                        h / DesignXmlDraw.POINTSIZED);
            }
            statusPosition.Text = sh;
        }

        private void SelectionMoved(object sender, System.EventArgs e)
        {
            if (this.SourceRdl.Length == 0)
                return;

            SetStatusNameAndPosition();
        }

        private void SelectionChanged(object sender, System.EventArgs e)
        {
            if (this.SourceRdl.Length == 0)
                return;
            
            if (this.RdlEditor.DesignTab == "edit")
            {
                SetStatusNameAndPosition();
                return;
            }

            bSuppressChange = true;			// don't process changes in status bar

            SetStatusNameAndPosition();

            this.EnableEditTextBox();	// handling enabling/disabling of textbox

            StyleInfo si = this.RdlEditor.SelectedStyle;

            if (si == null)
                return;

            this.btnBold.Checked = si.IsFontBold() ? true : false;            
            this.btnItalic.Checked = si.FontStyle == FontStyleEnum.Italic ? true : false;
            this.btnUnderLine.Checked = si.TextDecoration == TextDecorationEnum.Underline ? true : false;

            this.cbxFont.Text = si.FontFamily;

            string rs = string.Format(NumberFormatInfo.InvariantInfo, "{0:0.#}", si.FontSize);
            this.cbxFontSize.Text = rs;

            this.btnAlignLeft.Checked = si.TextAlign == TextAlignEnum.Left;
            this.btnAlignCenter.Checked = si.TextAlign == TextAlignEnum.Center;
            this.btnAlignRight.Checked = si.TextAlign == TextAlignEnum.Right;

            bSuppressChange = false;
        }

        private void ReportItemInserted(object sender, System.EventArgs e)
        {
            if (this.SourceRdl.Length == 0)
                return;

            // turn off the current selection after an item is inserted
            if (btnInsertCurrent != null)
            {
                btnInsertCurrent.Checked = false;
                this.RdlEditor.CurrentInsert = null;
                btnInsertCurrent = null;
            }
        }

        #endregion

        internal RdlEditPreview GetEditor()
        {
            return this.RdlEditor;
        }

        #region 自定义颜色

        static internal int[] GetCustomColors()
        {
            string optFileName = AppDomain.CurrentDomain.BaseDirectory + "designerstate.xml";
            int white = 16777215;	// default to white (magic number)
            int[] cArray = new int[] {white, white, white, white,white, white, white, white,
								    white, white, white, white, white, white, white, white};
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.PreserveWhitespace = false;
                xDoc.Load(optFileName);
                XmlNode xNode;
                xNode = xDoc.SelectSingleNode("//designerstate");

                string tcolors = "";
                // Loop thru all the child nodes
                foreach (XmlNode xNodeLoop in xNode.ChildNodes)
                {
                    if (xNodeLoop.Name != "CustomColors")
                        continue;
                    tcolors = xNodeLoop.InnerText;
                    break;
                }
                string[] colorList = tcolors.Split(',');
                int i = 0;

                foreach (string c in colorList)
                {
                    try { cArray[i] = int.Parse(c); }
                    catch { cArray[i] = white; }
                    i++;
                    if (i >= cArray.Length)		// Only allow 16 custom colors
                        break;
                }
            }
            catch
            {		// Didn't sucessfully get the startup state but don't really care
            }
            return cArray;
        }

        static internal void SetCustomColors(int[] colors)
        {
            string optFileName = AppDomain.CurrentDomain.BaseDirectory + "designerstate.xml";

            StringBuilder sb = new StringBuilder();
            foreach (int c in colors)
            {
                sb.Append(c.ToString());
                sb.Append(",");
            }

            sb.Remove(sb.Length - 1, 1);	// remove last ","
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.PreserveWhitespace = false;
                xDoc.Load(optFileName);
                XmlNode xNode;
                xNode = xDoc.SelectSingleNode("//designerstate");

                // Loop thru all the child nodes
                XmlNode cNode = null;
                foreach (XmlNode xNodeLoop in xNode.ChildNodes)
                {
                    if (xNodeLoop.Name == "CustomColors")
                    {
                        cNode = xNodeLoop;
                        break;
                    }
                }

                if (cNode == null)
                {
                    cNode = xDoc.CreateElement("CustomColors");
                    xNode.AppendChild(cNode);
                }

                cNode.InnerText = sb.ToString();

                xDoc.Save(optFileName);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Custom Color Save Failed");
            }
            return;
        }

        #endregion

        private void SetStatusNameAndPosition()
        {
            if (this.RdlEditor.DesignTab == "design")
                SetStatusNameAndPositionDesign(this.RdlEditor);
            else if (this.RdlEditor.DesignTab == "edit")
                SetStatusNameAndPositionEdit(this.RdlEditor);
            else
            {
                statusPosition.Text = statusSelected.Text = "";
            }
            return;
        }

        private void SetStatusNameAndPositionDesign(RdlEditPreview rdlEditor)
        {
            if (rdlEditor.DrawCtl.SelectedCount <= 0)
            {
                statusPosition.Text = statusSelected.Text = "";
                return;
            }

            // Handle position
            PointF pos = rdlEditor.SelectionPosition;
            SizeF sz = rdlEditor.SelectionSize;
            string spos;
            if (pos.X == float.MinValue)	// no item selected is probable cause
                spos = "";
            else
            {
                RegionInfo rinfo = new RegionInfo(CultureInfo.CurrentCulture.LCID);
                if (rinfo.IsMetric)
                {
                    if (sz.Width == float.MinValue)	// item is in a table/matrix is probably cause
                        spos = string.Format("   x={0:0.00}cm, y={1:0.00}cm        ",
                            pos.X / (72 / 2.54d), pos.Y / (72 / 2.54d));
                    else
                        spos = string.Format("   x={0:0.00}cm, y={1:0.00}cm, w={2:0.00}cm, h={3:0.00}cm        ",
                            pos.X / (72 / 2.54d), pos.Y / (72 / 2.54d),
                            sz.Width / (72 / 2.54d), sz.Height / (72 / 2.54d));
                }
                else
                {
                    if (sz.Width == float.MinValue)
                        spos = string.Format("   x={0:0.00}\", y={1:0.00}\"        ",
                            pos.X / 72, pos.Y / 72);
                    else
                        spos = string.Format("   x={0:0.00}\", y={1:0.00}\", w={2:0.00}\", h={3:0.00}\"        ",
                            pos.X / 72, pos.Y / 72, sz.Width / 72, sz.Height / 72);
                }
            }
            if (spos != statusPosition.Text)
                statusPosition.Text = spos;

            // Handle text
            string sname = rdlEditor.SelectionName;
            if (sname != statusSelected.Text)
                statusSelected.Text = sname;
            return;
        }

        private void SetStatusNameAndPositionEdit(RdlEditPreview  rdlEditor)
        {
            string spos = string.Format("Ln {0}  Ch {1}", rdlEditor.CurrentLine, rdlEditor.CurrentCh);
            if (spos != statusSelected.Text)
                statusSelected.Text = spos;

            if (statusPosition.Text != "")
                statusPosition.Text = "";

            return;
        }

        internal void ResetPassword()
        {
            bGotPassword = false;
            _DataSourceReferencePassword = null;
        }

        internal string GetPassword()
        {
            if (bGotPassword)
                return _DataSourceReferencePassword;

            using (DataSourcePassword dlg = new DataSourcePassword())
            {
                DialogResult rc = dlg.ShowDialog();
                bGotPassword = true;
                if (rc == DialogResult.OK)
                    _DataSourceReferencePassword = dlg.PassPhrase;

                return _DataSourceReferencePassword;
            }
        }

        #region 一级菜单

        private void miData_Click(object sender, EventArgs ea)
        {
            bool bEnable = false;

            if (this.SourceRdl.Length >0 && this.RdlEditor.DesignTab == "design")
                bEnable = true;

            this.miDataSource.Enabled = this.miDataSet.Enabled = this.miEmbeddedImage.Enabled = bEnable;
            if (!bEnable)
                return;

            // Run thru all the existing DataSets
            this.miDataSet.DropDownItems.Clear();
            this.miDataSet.DropDownItems.Add(new ToolStripMenuItem("New...",null,
                        new EventHandler(this.miDataSet_Click)));

            DesignXmlDraw draw = this.RdlEditor.DrawCtl;
            XmlNode rNode = draw.GetReportNode();
            XmlNode dsNode = draw.GetNamedChildNode(rNode, "DataSets");
            if (dsNode != null)
            {
                foreach (XmlNode dNode in dsNode)
                {
                    if (dNode.Name != "DataSet")
                        continue;
                    XmlAttribute nAttr = dNode.Attributes["Name"];
                    if (nAttr == null)	// shouldn't really happen
                        continue;
                    this.miDataSet.DropDownItems.Add(new ToolStripMenuItem(nAttr.Value,null,
                        new EventHandler(this.miDataSet_Click)));
                }
            }
        }

        private void miEdit_Click(object sender, EventArgs ea)
        {
            RdlEditPreview e = GetEditor();            
            
            if (e == null || e.DesignTab != "edit")
            {
                if ( e == null)
                {
                    this.miUndo.Enabled = this.miRedo.Enabled = this.miCut.Enabled = this.miCopy.Enabled =
                        this.miPaste.Enabled = this.miDelete.Enabled = this.miSelectAll.Enabled = false;
                    return;
                }
            }
            
            this.miUndo.Enabled = e.CanUndo;
            this.miRedo.Enabled = e.CanRedo;

            bool bSelection = e.SelectionLength > 0;	// any text selected?

            this.miCut.Enabled = bSelection;
            this.miCopy.Enabled = bSelection;
            this.miPaste.Enabled = Clipboard.GetDataObject().GetDataPresent(DataFormats.Text);
            this.miDelete.Enabled = bSelection;

            this.miSelectAll.Enabled = true;
        }

        private void miFormat_Click(object sender, EventArgs e)
        {
            bool bEnable = (this.SourceRdl.Length > 0 && this.RdlEditor.DesignTab == "design" && this.RdlEditor.DrawCtl.AllowGroupOperationOnSelected);

            this.miAlignBottom.Enabled = this.miAlignCenter.Enabled =
                this.miAlignLeft.Enabled = this.miAlignMiddle.Enabled =
                this.miAlignRight.Enabled = this.miAlignTop.Enabled = bEnable;

            this.miSizeWidth.Enabled = this.miSizeHeight.Enabled = this.miSizeBoth.Enabled = bEnable;

            this.miHorSpacM.Enabled = this.miHorSpacI.Enabled = this.miHorSpacD.Enabled =
                this.miHorSpacE.Enabled = bEnable;

            this.miVerSpacM.Enabled = this.miVerSpacI.Enabled = this.miVerSpacD.Enabled =
                this.miVerSpacE.Enabled = bEnable;

            bEnable = (this.SourceRdl.Length > 0 && this.RdlEditor.DesignTab == "design" && this.RdlEditor.DrawCtl.SelectedCount > 0);

            this.miPadBottomI.Enabled =
                this.miPadBottomD.Enabled =
                this.miPadBottomE.Enabled =
                this.miPadTopI.Enabled =
                this.miPadTopD.Enabled =
                this.miPadTopE.Enabled =
                this.miPadLeftI.Enabled =
                this.miPadLeftD.Enabled =
                this.miPadLeftE.Enabled =
                this.miPadRightI.Enabled =
                this.miPadRightD.Enabled =
                this.miPadRightE.Enabled =
                    bEnable;
        }

        private void miView_Click(object sender, EventArgs e)
        {
            bool bEnable = this.SourceRdl.Length >0;

            this.miDesign.Enabled = this.miSource.Enabled = this.miPreview.Enabled = bEnable;
        }

        #endregion

        #region 文件菜单

        private void miExit_Click(object sender, EventArgs e)
        {
            if (!OkToSave())
                return;

            this.Close();
        }

        private void miNewDb_Click(object sender, System.EventArgs e)
        {
            DialogDatabase2 dlgDB = new DialogDatabase2();
            dlgDB.StartPosition = FormStartPosition.CenterParent;
            dlgDB.QueyText = this.QueryText;

            //dlgDB.FormBorderStyle = FormBorderStyle.SizableToolWindow;

            // show modally
            dlgDB.ShowDialog();
            if (dlgDB.DialogResult == DialogResult.Cancel)
                return;

            this.SourceRdl = dlgDB.ResultReport;
            if (this.RdlEditor.DrawCtl.ReportNames == null) { }
        }

        private void miNewObjec_Click(object sender, System.EventArgs e)
        {
            DialogDataObject2 dlgObject = new DialogDataObject2();
            dlgObject.StartPosition = FormStartPosition.CenterParent;
            //dlgDB.FormBorderStyle = FormBorderStyle.SizableToolWindow;

            // show modally
            dlgObject.ShowDialog();
            if (dlgObject.DialogResult == DialogResult.Cancel)
                return;

            this.SourceRdl = dlgObject.ResultReport;
            if (this.RdlEditor.DrawCtl.ReportNames == null) { }
        }

        private void miNewDataObject_Click(object sender, EventArgs e)
        {
            DialogDataObject2 dlgObject = new DialogDataObject2();
            dlgObject.StartPosition = FormStartPosition.CenterParent;
            //dlgDB.FormBorderStyle = FormBorderStyle.SizableToolWindow;

            // show modally
            dlgObject.ShowDialog();
            if (dlgObject.DialogResult == DialogResult.Cancel)
                return;

            this.SourceRdl = dlgObject.ResultReport;
            if (this.RdlEditor.DrawCtl.ReportNames == null) { }
        }

        private void miPrint_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();

            pd.DocumentName = this.rdlName;
            pd.PrinterSettings.FromPage = 1;
            pd.PrinterSettings.ToPage = this.RdlEditor.PageCount;
            pd.PrinterSettings.MaximumPage = this.RdlEditor.PageCount;
            pd.PrinterSettings.MinimumPage = 1;
            pd.DefaultPageSettings.Landscape = this.RdlEditor.PageWidth > this.RdlEditor.PageHeight ? true : false;

            PrintDialog dlg = new PrintDialog();
            dlg.Document = pd;
            dlg.AllowSelection = true;
            dlg.AllowSomePages = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (pd.PrinterSettings.PrintRange == PrintRange.Selection)
                    {
                        //pd.PrinterSettings.FromPage = mc.PageCurrent;
                    }
                    //mc.Print(pd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Print error: " + ex.Message, "RDL Design");
                }
            }
        }

        private void miSave_Click(object sender, EventArgs e)
        {
            if (this.Saved != null)
                this.Saved(this, e);
        }

        private void miExportXml_Click(object sender, EventArgs e)
        {
            this.Export("xml");
            return;
        }

        private void miExportHtml_Click(object sender, EventArgs e)
        {
            this.Export("html");
            return;
        }

        private void miExportMHtml_Click(object sender, EventArgs e)
        {
            this.Export("mht");
            return;
        }

        private void miExportPdf_Click(object sender, EventArgs e)
        {
            this.Export("pdf");
            return;
        }

        private void miExportExcel_Click(object sender, EventArgs e)
        {
            this.Export("xlsx");
            return;
        }

        private void miExportRtf_Click(object sender, EventArgs e)
        {
            this.Export("rtf");
            return;
        }

        #endregion

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

            if (this.rdlName != null)
                sfd.FileName = Path.GetFileNameWithoutExtension(rdlName) + "." + type;
            else
                sfd.FileName = "*." + type;

            if (sfd.ShowDialog(this) != DialogResult.OK)
                return false;

            return true;
        }

        #region 数据菜单

        private void miDataSource_Click(object sender, System.EventArgs e)
        {
            //this.RdlEditor.StartUndoGroup("DataSources Dialog");

            //using (DialogDataSources dlgDS = new DialogDataSources(mc.SourceFile, mc.DrawCtl))
            //{
            //    dlgDS.StartPosition = FormStartPosition.CenterParent;
            //    DialogResult dr = dlgDS.ShowDialog();
            //    mc.Editor.EndUndoGroup(dr == DialogResult.OK);
            //    if (dr == DialogResult.OK)
            //        mc.Modified = true;
            //}
        }

        private void miDataSet_Click(object sender, System.EventArgs e)
        {
            MenuItem menu = sender as MenuItem;
            if (menu == null)
                return;
            this.RdlEditor.StartUndoGroup("DataSet Dialog");

            string dsname = menu.Text;

            // Find the dataset we need
            List<XmlNode> ds = new List<XmlNode>();
            DesignXmlDraw draw = this.RdlEditor.DrawCtl;
            XmlNode rNode = draw.GetReportNode();
            XmlNode dsNode = draw.GetCreateNamedChildNode(rNode, "DataSets");
            XmlNode dataset = null;

            // find the requested dataset: the menu text equals the name of the dataset
            int dsCount = 0;		// count of the datasets
            string onlyOneDsname = null;
            foreach (XmlNode dNode in dsNode)
            {
                if (dNode.Name != "DataSet")
                    continue;
                XmlAttribute nAttr = dNode.Attributes["Name"];
                if (nAttr == null)	// shouldn't really happen
                    continue;
                if (dsCount == 0)
                    onlyOneDsname = nAttr.Value;		// we keep track of 1st name; 

                dsCount++;
                if (nAttr.Value == dsname)
                    dataset = dNode;
            }

            bool bNew = false;
            if (dataset == null)	// This must be the new menu item
            {
                dataset = draw.CreateElement(dsNode, "DataSet", null);	// create empty node
                bNew = true;
            }
            ds.Add(dataset);

            using (PropertyDialog pd = new PropertyDialog(this.RdlEditor.DrawCtl, ds, PropertyTypeEnum.DataSets))
            {
                DialogResult dr = pd.ShowDialog();
                if (pd.Changed || dr == DialogResult.OK)
                {
                    if (dsCount == 1)
                    // if we used to just have one DataSet we may need to fix up DataRegions 
                    //	that were defaulting to that name
                    {
                        dsCount = 0;
                        bool bUseName = false;
                        foreach (XmlNode dNode in dsNode)
                        {
                            if (dNode.Name != "DataSet")
                                continue;
                            XmlAttribute nAttr = dNode.Attributes["Name"];
                            if (nAttr == null)	// shouldn't really happen
                                continue;

                            dsCount++;
                            if (onlyOneDsname == nAttr.Value)
                                bUseName = true;
                        }
                        if (bUseName && dsCount > 1)
                        {
                            foreach (XmlNode drNode in draw.ReportNames.ReportItems)
                            {
                                switch (drNode.Name)
                                {
                                    // If a DataRegion doesn't have a dataset name specified use previous one
                                    case "Table":
                                    case "List":
                                    case "Matrix":
                                    case "Chart":
                                        XmlNode aNode = draw.GetNamedChildNode(drNode, "DataSetName");
                                        if (aNode == null)
                                            draw.CreateElement(drNode, "DataSetName", onlyOneDsname);
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                    this.RdlEditor.Modified = true;
                }
                else if (bNew)	// if canceled and new DataSet get rid of temp node
                {
                    dsNode.RemoveChild(dataset);
                }
                if (pd.Delete)	// user must have hit a delete button for this to get set
                    dsNode.RemoveChild(dataset);

                if (!dsNode.HasChildNodes)		// If no dataset exists we remove DataSets
                    draw.RemoveElement(rNode, "DataSets");

                this.RdlEditor.EndUndoGroup(pd.Changed || dr == DialogResult.OK);
            }
        }

        private void miEmbeddedImage_Click(object sender, System.EventArgs e)
        {
            this.RdlEditor.StartUndoGroup("Embedded Images Dialog");
            DialogEmbeddedImages dlgEI = new DialogEmbeddedImages(this.RdlEditor.DrawCtl);
            dlgEI.StartPosition = FormStartPosition.CenterParent;
            DialogResult dr = dlgEI.ShowDialog();
            this.RdlEditor.EndUndoGroup(dr == DialogResult.OK);
            if (dr == DialogResult.OK)
                this.RdlEditor.Modified = true;
        }

        #endregion                

        #region 编辑菜单

        private void miUndo_Click(object sender, System.EventArgs ea)
        {
            if (this.tbFunction != null && this.tbFunction.Focused)
            {
                this.tbFunction.Undo();
                return;
            }

            RdlEditPreview e = GetEditor();
            if (e == null)
                return;

            if (e.CanUndo == true)
            {
                e.Undo();

                MDIChild mc = this.ActiveMdiChild as MDIChild;
                if (mc != null && mc.DesignTab == "design")
                {
                    e.DesignCtl.SetScrollControls();
                }
                this.SelectionChanged(this, new EventArgs());
            }
        }

        private void miRedo_Click(object sender, System.EventArgs ea)
        {
            RdlEditPreview e = GetEditor();
            if (e == null)
                return;

            if (e.CanRedo == true)
            {
                e.Redo();
            }
        }

        private void miCut_Click(object sender, System.EventArgs ea)
        {
            if (this.tbFunction != null && this.tbFunction.Focused)
            {
                this.tbFunction.Cut();
                return;
            }

            RdlEditPreview e = GetEditor();
            if (e == null)
                return;

            if (e.SelectionLength > 0)
                e.Cut();
        }

        private void miCopy_Click(object sender, System.EventArgs ea)
        {
            if (this.tbFunction != null && this.tbFunction.Focused)
            {
                this.tbFunction.Copy();
                return;
            }
            RdlEditPreview e = GetEditor();
            if (e == null)
                return;

            if (e.SelectionLength > 0)
                e.Copy();
        }

        private void miPaste_Click(object sender, System.EventArgs ea)
        {
            if (this.tbFunction != null && this.tbFunction.Focused)
            {
                this.tbFunction.Paste();
                return;
            }

            RdlEditPreview e = GetEditor();
            if (e == null)
                return;

            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text) == true ||
                Clipboard.GetDataObject().GetDataPresent(DataFormats.Bitmap) == true)
                e.Paste();
        }

        private void miDelete_Click(object sender, System.EventArgs ea)
        {
            RdlEditPreview e = GetEditor();
            if (e == null)
                return;

            if (e.SelectionLength > 0)
                e.SelectedText = "";
        }

        private void miSelectAll_Click(object sender, System.EventArgs ea)
        {
            if (this.tbFunction != null && this.tbFunction.Focused)
            {
                this.tbFunction.SelectAll();
                return;
            }
            RdlEditPreview e = GetEditor();
            if (e == null)
                return;

            e.SelectAll();
        }

        private void miFind_Click(object sender, System.EventArgs ea)
        {
            RdlEditPreview e = GetEditor();
            if (e == null)
                return;

            FindTab tab = new FindTab(e);
            tab.Show();
        }

        private void miFindNext_Click(object sender, System.EventArgs ea)
        {
            RdlEditPreview e = GetEditor();
            if (e == null)
                return;

            FindTab tab = new FindTab(e);
            tab.Show();
        }

        private void miFormatXml_Click(object sender, System.EventArgs ea)
        {
            RdlEditPreview e = GetEditor();
            if (e == null)
                return;

            if (e.Text.Length > 0)
            {
                try
                {
                    e.Text = DesignerUtility.FormatXml(e.Text);
                    e.Modified = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Format XML");
                }
            }
        }

        private void miReplace_Click(object sender, System.EventArgs ea)
        {
            RdlEditPreview e = GetEditor();
            if (e == null)
                return;
            FindTab tab = new FindTab(e);
            tab.tcFRG.SelectedTab = tab.tabReplace;
            tab.Show();
        }

        private void miGoto_Click(object sender, System.EventArgs ea)
        {
            RdlEditPreview e = GetEditor();
            if (e == null)
                return;

            FindTab tab = new FindTab(e);
            tab.tcFRG.SelectedTab = tab.tabGoTo;
            tab.Show();
        }

        #endregion

        #region 帮助菜单

        private void miAbout_Click(object sender, System.EventArgs ea)
        {
            DialogAbout dlg = new DialogAbout();
            dlg.ShowDialog();
        }

        private void miContent_Click(object sender, System.EventArgs ea)
        {
            try
            {
                System.Diagnostics.Process.Start(HelpUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + "Resetting Help URL to default.", "Help URL Invalid");
                _HelpUrl = DefaultHelpUrl;
            }
        }

        #endregion        

        #region 工具条

        internal ToolStripButton ButtonInsertCurrent
        {
            get
            {
                return this.btnInsertCurrent;
            }
            set
            {
                this.btnInsertCurrent = value;

                foreach (ToolStripButton button in this.InsertButtonList)
                {
                    if (button == this.btnInsertCurrent)
                        button.Checked = true;
                    else
                        button.Checked = false;
                }
            }
        }

        private void btnTextBox_Click(object sender, EventArgs e)
        {
            this.ButtonInsertCurrent = sender as ToolStripButton;

            this.RdlEditor.SetFocus();
            this.RdlEditor.CurrentInsert = "Textbox";
        }

        private void btnChart_Click(object sender, EventArgs e)
        {
            this.ButtonInsertCurrent = sender as ToolStripButton;

            this.RdlEditor.SetFocus();
            this.RdlEditor.CurrentInsert = "Chart";
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            this.ButtonInsertCurrent = sender as ToolStripButton;

            this.RdlEditor.SetFocus();
            this.RdlEditor.CurrentInsert = "Table";
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            this.ButtonInsertCurrent = sender as ToolStripButton;

            this.RdlEditor.SetFocus();
            this.RdlEditor.CurrentInsert = "List";
        }

        private void btnImage_Click(object sender, EventArgs e)
        {
            this.ButtonInsertCurrent = sender as ToolStripButton;

            this.RdlEditor.SetFocus();
            this.RdlEditor.CurrentInsert = "Image";
        }

        private void btnMatrix_Click(object sender, EventArgs e)
        {
            this.ButtonInsertCurrent = sender as ToolStripButton;

            this.RdlEditor.SetFocus();
            this.RdlEditor.CurrentInsert = "Matrix";
        }

        private void btnSubReport_Click(object sender, EventArgs e)
        {
            this.ButtonInsertCurrent = sender as ToolStripButton;

            this.RdlEditor.SetFocus();
            this.RdlEditor.CurrentInsert = "Subreport";
        }

        private void btnRect_Click(object sender, EventArgs e)
        {
            this.ButtonInsertCurrent = sender as ToolStripButton;

            this.RdlEditor.SetFocus();
            this.RdlEditor.CurrentInsert = "Rectangle";
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            this.ButtonInsertCurrent = sender as ToolStripButton;

            this.RdlEditor.SetFocus();
            this.RdlEditor.CurrentInsert = "Line";
        }

        private void tbFunction_Validated(object sender, EventArgs e)
        {
            if (this.RdlEditor.DesignTab != "design" || this.RdlEditor.DrawCtl.SelectedCount != 1 )
                return;

            this.RdlEditor.SetSelectedText(this.tbFunction.Text);
        }

        private void tbFunction_KeyDown(object sender, KeyEventArgs e)
        {
            // Force scroll up and down
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    this.RdlEditor.SetFocus();
                    e.Handled = true;
                    break;
                case Keys.Escape:
                    if (this.RdlEditor.DrawCtl.SelectedCount == 1)
                    {
                        XmlNode tn = this.RdlEditor.DrawCtl.SelectedList[0] as XmlNode;
                        if (tn != null && tn.Name == "Textbox")
                        {
                            this.tbFunction.Text = this.RdlEditor.DrawCtl.GetElementValue(tn, "Value", "");
                            e.Handled = true;
                        }
                    }
                    break;
                default:
                    break;
            }

        }

        private void btnBold_Click(object sender, EventArgs e)
        {
            btnBold.Checked = !btnBold.Checked;
            this.RdlEditor.ApplyStyleToSelected("FontWeight", this.btnBold.Checked ? "Bold" : "Normal");
        }

        private void btnItalic_Click(object sender, EventArgs e)
        {
            btnItalic.Checked = !btnItalic.Checked;
            this.RdlEditor.ApplyStyleToSelected("FontStyle", this.btnItalic.Checked ? "Italic" : "Normal");
        }

        private void btnUnderline_Click(object sender, EventArgs e)
        {
            this.btnUnderLine.Checked = !this.btnUnderLine.Checked;
            this.RdlEditor.ApplyStyleToSelected("TextDecoration", this.btnUnderLine.Checked ? "Underline" : "None");
        }

        private void btnAlignLeft_Click(object sender, EventArgs e)
        {
            if (!this.btnAlignLeft.Checked)
            {
                this.btnAlignLeft.Checked = true;
                this.btnAlignCenter.Checked = false;
                this.btnAlignRight.Checked = false;

                this.RdlEditor.ApplyStyleToSelected("TextAlign", "Left");
            }
        }

        private void btnAlignCenter_Click(object sender, EventArgs e)
        {
            if (!this.btnAlignCenter.Checked)
            {
                this.btnAlignLeft.Checked = false;
                this.btnAlignCenter.Checked = true;
                this.btnAlignRight.Checked = false;

                this.RdlEditor.ApplyStyleToSelected("TextAlign", "Center");
            }
        }

        private void btnAlignRight_Click(object sender, EventArgs e)
        {
            if (!this.btnAlignRight.Checked)
            {
                this.btnAlignLeft.Checked = false;
                this.btnAlignCenter.Checked = false;
                this.btnAlignRight.Checked = true;

                this.RdlEditor.ApplyStyleToSelected("TextAlign", "Right");
            }
        }

        private void btnForeColor_Click(object sender, EventArgs e)
        {
            this.colorDialog.AnyColor = true;
            this.colorDialog.FullOpen = true;

            if (this.colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                string htmlColor = ColorTranslator.ToHtml(colorDialog.Color);
                this.RdlEditor.ApplyStyleToSelected("Color", htmlColor);

            }
        }

        private void btnBackColor_Click(object sender, EventArgs e)
        {
            if (this.SourceRdl.Length == 0)
                return;

            this.colorDialog.AnyColor = true;
            this.colorDialog.FullOpen = true;

            if (this.colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                string htmlColor = ColorTranslator.ToHtml(colorDialog.Color);
                this.RdlEditor.ApplyStyleToSelected("BackgroundColor", htmlColor);
            }
        }

        private void cbxFont_Change(object sender, EventArgs e)
        {
            if (!bSuppressChange)
                this.RdlEditor.ApplyStyleToSelected("FontFamily", this.cbxFont.Text);
        }

        private void cbxFontSize_Change(object sender, EventArgs e)
        {
            if (!bSuppressChange)
                this.RdlEditor.ApplyStyleToSelected("FontSize", this.cbxFontSize.Text + "pt");
        }

        private void btnZoomUp_Click(object sender, EventArgs e)
        {
            int offset = -1;

            for (int i = 0; i < RDLHelper.ZoomList.Length; i++)
            {
                if (System.Math.Abs(this.RdlEditor.Zoom - RDLHelper.ZoomList[i]) < 0.01f)
                {
                    offset = i;
                    break;
                }
            }

            if (offset != 0)
                this.RdlEditor.Zoom = RDLHelper.ZoomList[offset - 1];
        }

        private void btnZoomDown_Click(object sender, EventArgs e)
        {
            int offset = -1;

            for (int i = 0; i < RDLHelper.ZoomList.Length; i++)
            {
                if (System.Math.Abs(this.RdlEditor.Zoom - RDLHelper.ZoomList[i]) < 0.01f)
                {
                    offset = i;
                    break;
                }
            }

            if (offset != RDLHelper.ZoomList.Length - 1)
                this.RdlEditor.Zoom = RDLHelper.ZoomList[offset + 1];
        }

        private void cbxZoom_Change(object sender, EventArgs e)
        {
            switch (this.cbxZoom.Text)
            {
                case "Actual Size":
                    this.RdlEditor.Zoom = 1;
                    break;
                case "Fit Page":
                    this.RdlEditor.ZoomMode = ZoomEnum.FitPage;
                    break;
                case "Fit Width":
                    this.RdlEditor.ZoomMode = ZoomEnum.FitWidth;
                    break;
                default:
                    string s = this.cbxZoom.Text.Substring(0, this.cbxZoom.Text.Length - 1);
                    float z;
                    try
                    {
                        z = Convert.ToSingle(s) / 100f;
                        this.RdlEditor.Zoom = z;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Zoom Value Invalid");
                    }
                    break;
            }
        }

        #endregion                

        #region 格式菜单

        private void miAlignCenter_Click(object sender, System.EventArgs e)
        {
            this.RdlEditor.DesignCtl.AlignCenters();
        }

        private void miAlignLeft_Click(object sender, System.EventArgs e)
        {
            this.RdlEditor.DesignCtl.AlignLefts();
        }

        private void miAlignRight_Click(object sender, System.EventArgs e)
        {
            this.RdlEditor.DesignCtl.AlignRights();
        }

        private void miAlignBottom_Click(object sender, System.EventArgs e)
        {
            this.RdlEditor.DesignCtl.AlignBottoms();
        }

        private void miAlignTop_Click(object sender, System.EventArgs e)
        {
            this.RdlEditor.DesignCtl.AlignTops();
        }

        private void miAlignMiddle_Click(object sender, System.EventArgs e)
        {
            this.RdlEditor.DesignCtl.AlignMiddles();
        }

        private void miSizeHeight_Click(object sender, System.EventArgs e)
        {
            this.RdlEditor.DesignCtl.SizeHeights();
        }

        private void miSizeWidth_Click(object sender, System.EventArgs e)
        {
            this.RdlEditor.DesignCtl.SizeWidths();
        }

        private void miSizeBoth_Click(object sender, System.EventArgs e)
        {
            this.RdlEditor.DesignCtl.SizeBoth();
        }

        private void miHorSpacM_Click(object sender, System.EventArgs e)
        {
            this.RdlEditor.DesignCtl.HorzSpacingMakeEqual();
        }

        private void miHorSpacI_Click(object sender, System.EventArgs e)
        {
            this.RdlEditor.DesignCtl.HorzSpacingIncrease();
        }

        private void miHorSpacD_Click(object sender, System.EventArgs e)
        {
            this.RdlEditor.DesignCtl.HorzSpacingDecrease();
        }

        private void miHorSpacE_Click(object sender, System.EventArgs e)
        {
            this.RdlEditor.DesignCtl.HorzSpacingMakeZero();
        }

        private void miVerSpacM_Click(object sender, System.EventArgs e)
        {
            this.RdlEditor.DesignCtl.VertSpacingMakeEqual();
        }

        private void miVerSpacI_Click(object sender, System.EventArgs e)
        {
            this.RdlEditor.DesignCtl.VertSpacingIncrease();
        }

        private void miVerSpacD_Click(object sender, System.EventArgs e)
        {
            this.RdlEditor.DesignCtl.VertSpacingDecrease();
        }

        private void miVerSpacE_Click(object sender, System.EventArgs e)
        {
            this.RdlEditor.DesignCtl.VertSpacingMakeZero();
        }

        private void miPadding_Click(object sender, System.EventArgs e)
        {
            ToolStripMenuItem mi = sender as ToolStripMenuItem;

            string padname = null;
            int paddiff = 0;
            if (mi == this.miPadLeftI)
            {
                padname = "PaddingLeft";
                paddiff = 4;
            }
            else if (mi == this.miPadLeftD)
            {
                padname = "PaddingLeft";
                paddiff = -4;
            }
            else if (mi == this.miPadLeftE)
            {
                padname = "PaddingLeft";
                paddiff = 0;
            }
            else if (mi == this.miPadRightI)
            {
                padname = "PaddingRight";
                paddiff = 4;
            }
            else if (mi == this.miPadRightD)
            {
                padname = "PaddingRight";
                paddiff = -4;
            }
            else if (mi == this.miPadRightE)
            {
                padname = "PaddingRight";
                paddiff = 0;
            }
            else if (mi == this.miPadTopI)
            {
                padname = "PaddingTop";
                paddiff = 4;
            }
            else if (mi == this.miPadTopD)
            {
                padname = "PaddingTop";
                paddiff = -4;
            }
            else if (mi == this.miPadTopE)
            {
                padname = "PaddingTop";
                paddiff = 0;
            }
            else if (mi == this.miPadBottomI)
            {
                padname = "PaddingBottom";
                paddiff = 4;
            }
            else if (mi == this.miPadBottomD)
            {
                padname = "PaddingBottom";
                paddiff = -4;
            }
            else if (mi == this.miPadBottomE)
            {
                padname = "PaddingBottom";
                paddiff = 0;
            }

            if (padname != null)
            {
                this.RdlEditor.DesignCtl.SetPadding(padname, paddiff);
            }
        }

        #endregion        

        #region 视图菜单

        private void miDesign_Click(object sender, System.EventArgs e)
        {
            if (this.SourceRdl.Length > 0)
                this.RdlEditor.DesignTab = "design";
        }

        private void miSource_Click(object sender, System.EventArgs e)
        {
            if (this.SourceRdl.Length > 0)
                this.RdlEditor.DesignTab = "edit";
        }

        private void miPreview_Click(object sender, System.EventArgs e)
        {
            if (this.SourceRdl.Length > 0)
                this.RdlEditor.DesignTab = "preview";
        }

        #endregion                        

        #region IReportDesigner接口成员

        /// <summary>
        /// 报表名称。
        /// </summary>
        public string ReportName
        {
            get
            {
                return this.rdlName;
            }
            set
            {
                this.rdlName = value;
            }
        }

        /// <summary>
        /// 连接字符串。
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return this.m_ConnectionString;
            }
            set
            {
                this.m_ConnectionString = value;
            }
        }

        /// <summary>
        /// 查询文本。
        /// </summary>
        public string QueryText
        {
            get
            {
                return this.m_QueryText;
            }
            set
            {
                this.m_QueryText = value;
            }
        }

        /// <summary>
        /// 报表定义。
        /// </summary>
        public string Define
        {
            get
            {
                return this.SourceRdl;
            }
            set
            {
                this.SourceRdl = value;
            }
        }

        /// <summary>
        /// 事件，保存事件。
        /// </summary>
        public event EventHandler Saved;

        #endregion
    }
}