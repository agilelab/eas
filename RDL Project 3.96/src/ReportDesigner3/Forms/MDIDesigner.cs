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

namespace fyiReporting.RdlDesign
{
    /// <summary>
    /// 报表设计器。
    /// </summary>
    public partial class MDIDesigner : Form
    {
        /// <summary>
        /// 内部私有变量，打印子窗体。
        /// </summary>
        private MDIChild printChild = null;
        
        /// <summary>
        /// 内部私有变量，最近使用报表集合。
        /// </summary>
        private SortedList _RecentFiles = null;

        /// <summary>
        /// 内部私有变量，当前使用报表集合。
        /// </summary>
        private ArrayList _CurrentFiles = null;

        /// <summary>
        /// 内部私有变量，临时报表文件集合。
        /// </summary>
        private ArrayList _TempReportFiles = null;
        
        /// <summary>
        /// 内部私有变量，最多可显示的最近报表数量。
        /// </summary>
        int _RecentFilesMax = 5;
       
        /// <summary>
        /// 内部私有变量，默认帮助内容地址。
        /// </summary>
        private readonly string DefaultHelpUrl = "http://www.fyireporting.com/helpv2/index.html";

        /// <summary>
        /// 内部私有变量，内容地址。
        /// </summary>
        private string _HelpUrl;

        /// <summary>
        /// 内部私有变量，RDL验证对象。
        /// </summary>
        private DialogValidateRdl _ValidateRdl = null;

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

        /// <summary>
        /// 内部私有变量，是否显示属性窗口。
        /// </summary>
        private bool _ShowProperties = true;

        private RDL.NeedPassword _GetPassword;
        private string _DataSourceReferencePassword = null;
        private bool bGotPassword = false;

        [DllImport("user32.dll")]
        public static extern bool LockWindowUpdate(IntPtr hWndLock);

        public MDIDesigner()
        {
            InitializeComponent();

            this.MdiChildActivate += new EventHandler(MDIDesigner_MdiChildActivate);
            _GetPassword = new RDL.NeedPassword(this.GetPassword);

            this.mainProperties.Parent = this;
            this.mainTC.Parent = this.panPages;
            this.panPages.Parent = this;

            this.Initialize();
        }        

        private void MDIDesigner_MdiChildActivate(object sender, EventArgs e)
        {
            if (this._ValidateRdl != null)		// don't keep the validation open when window changes
                this._ValidateRdl.Close();

            DesignTabChanged(sender, e);
            SelectionChanged(sender, e);
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;
            mc.SetFocus();
            if (mc.Tab != null)
                mainTC.SelectTab(mc.Tab);
        }

        /// <summary>
        /// 已重写，处理关闭事件。
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            SaveStartupState();
            CleanupTempFiles();

            base.OnClosing(e);
        }

        protected override void OnMdiChildActivate(EventArgs e)
        {
            base.OnMdiChildActivate(e);

            if (this._ValidateRdl != null)		// don't keep the validation open when window changes
                this._ValidateRdl.Close();

            DesignTabChanged(this, e);
            SelectionChanged(this, e);
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;
            mc.SetFocus();
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
                        
            this.GetStartupState();

			if (_CurrentFiles != null)
			{
				foreach (string file in _CurrentFiles)
				{
					CreateMDIChild(file, null, false);
				}
				_CurrentFiles = null;
			}

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

        /// <summary>
        /// Handles right mouse button processing on current tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mainTC_MouseClick(object sender, MouseEventArgs e)
        {
            TabControl tc = sender as TabControl;
            if (tc == null)
                return;

            if (e.Button != MouseButtons.Right)
                return;
            Point p = e.Location;
            int current = -1;
            for (int i = 0; i < tc.TabCount; i++)
            {
                Rectangle r = tc.GetTabRect(i);
                if (r.Contains(p))
                {
                    current = i;
                    break;
                }
            }
            if (current != tc.SelectedIndex)
                return;

            ToolStripMenuItem mc = new ToolStripMenuItem("关闭(&C)");
            mc.Click+=new EventHandler(this.miClose_Click);

            ToolStripMenuItem ms = new ToolStripMenuItem("保存(&S)");
            mc.Click += new EventHandler(this.miSave_Click);

            ToolStripMenuItem ma = new ToolStripMenuItem("除此之外全部关闭(&A)");
            mc.Click += new EventHandler(this.miCloseAllButCurrent_Click);

            ContextMenuStrip cm = new ContextMenuStrip();
            cm.Items.AddRange(new ToolStripMenuItem[] { ms, mc, ma });
            cm.Show(tc, p);

        }

        void mainTC_SelectedIndexChanged(object sender, EventArgs e)
        {
            MDIChild mc = mainTC.SelectedTab == null ? null : mainTC.SelectedTab.Tag as MDIChild;

            if(mc!=null)
                mdi_Activate(mc);
        }

        private void mdi_Activate(MDIChild mc)
        {
            LockWindowUpdate(this.Handle);
            mc.Activate();
            this.Refresh(); //Forces a synchronous redraw of all controls

            LockWindowUpdate(IntPtr.Zero);
        }

        private bool OkToSave()
        {
            foreach (MDIChild mc in this.MdiChildren)
            {
                if (!mc.OkToClose())
                    return false;
            }
            return true;
        }

        private MDIChild CreateMDIChild(string file, string rdl, bool bMenuUpdate)
        {
            MDIChild mcOpen = null;
            if (file != null)
            {
                file = file.Trim();

                foreach (MDIChild mc in this.MdiChildren)
                {
                    if (mc.SourceFile != null && file == mc.SourceFile.Trim())
                    {							// we found it
                        mcOpen = mc;
                        break;
                    }
                }
            }
            if (mcOpen == null)
            {
                MDIChild mc = null;
                try
                {
                    mc = new MDIChild(this.ClientRectangle.Width * 3 / 5, this.ClientRectangle.Height * 3 / 5);
                    
                    mc.OnSelectionChanged += new MDIChild.RdlChangeHandler(SelectionChanged);
                    mc.OnSelectionMoved += new MDIChild.RdlChangeHandler(SelectionMoved);
                    mc.OnReportItemInserted += new MDIChild.RdlChangeHandler(ReportItemInserted);
                    mc.OnDesignTabChanged += new MDIChild.RdlChangeHandler(DesignTabChanged);
                    mc.OnOpenSubreport += new DesignCtl.OpenSubreportEventHandler(OpenSubReportEvent);
                    mc.OnHeightChanged += new DesignCtl.HeightEventHandler(HeightChanged);

                    mc.MdiParent = this;
                    
                    if (file != null)
                    {
                        mc.Viewer.Folder = Path.GetDirectoryName(file);
                        mc.SourceFile = file;
                        mc.Text = Path.GetFileName(file);
                        mc.Viewer.Folder = Path.GetDirectoryName(file);
                        mc.Viewer.ReportName = Path.GetFileNameWithoutExtension(file);
                        NoteRecentFiles(file, bMenuUpdate);
                    }
                    else
                    {
                        mc.SourceRdl = rdl;
                        mc.Viewer.ReportName = mc.Text = "Untitled";
                    }

                    //mc.ShowEditLines(true);
                    //mc.ShowReportItemOutline = false;

                    // 向Tab页注册
                    TabPage tp = new TabPage();
                    tp.Location = new System.Drawing.Point(0, 0);
                    tp.Name = mc.Text;
                    tp.TabIndex = 1;
                    tp.Text = mc.Text;
                    tp.Tag = mc;                // tie the mdichild to the tabpage
                    tp.ToolTipText = file;
                    mainTC.Controls.Add(tp);
                    mc.Tab = tp;

                    mc.Show();
                    mcOpen = mc;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    if (mc != null)
                        mc.Close();
                    return null;
                }
            }
            else
                mcOpen.Activate();
            return mcOpen;
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

        #region MDIChild2

        private void DesignTabChanged(object sender, System.EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            
            string tab = "";
            if (mc != null)
                tab = mc.DesignTab;

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

            if (!bEnableEdit && this._ValidateRdl != null)
            {
                this._ValidateRdl.Close();
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
                if (mc != null)
                {
                    switch (mc.ZoomMode)
                    {
                        case ZoomEnum.FitWidth:
                            zText = "Fit Width";
                            break;
                        case ZoomEnum.FitPage:
                            zText = "Fit Page";
                            break;
                        case ZoomEnum.UseZoom:
                            if (mc.Zoom == 1)
                                zText = "Actual Size";
                            else
                                zText = string.Format("{0:0}", mc.Zoom * 100f);
                            break;
                    }
                    this.cbxZoom.Text = zText;
                }
            }
            // when no active sheet
            if (this.btnSave != null)
                this.btnSave.Enabled = mc != null;

            // Update the status and position information
            SetStatusNameAndPosition();
        }

        private void OpenSubReportEvent(object sender, SubReportEventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            string file = mc.Viewer.Folder;
            if (e.SubReportName[0] == Path.DirectorySeparatorChar)
                file = file + e.SubReportName;
            else
                file = file + Path.DirectorySeparatorChar + e.SubReportName + ".rdl";

            CreateMDIChild(file, null, true);
        }

        private void HeightChanged(object sender, HeightEventArgs e)
        {
            if (e.Height == null)
            {
                SetProperties(this.ActiveMdiChild as MDIChild);

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
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            SetStatusNameAndPosition();
        }

        private void SelectionChanged(object sender, System.EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            
            if (mc == null)
                return;
            
            if (mc.RdlEditor.DesignTab == "edit")
            {
                SetStatusNameAndPosition();
                return;
            }

            bSuppressChange = true;			// don't process changes in status bar

            SetStatusNameAndPosition();

            this.EnableEditTextBox();	// handling enabling/disabling of textbox

            StyleInfo si = mc.SelectedStyle;

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
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            // turn off the current selection after an item is inserted
            if (btnInsertCurrent != null)
            {
                btnInsertCurrent.Checked = false;
                mc.CurrentInsert = null;
                btnInsertCurrent = null;
            }
        }

        #endregion

        internal RdlEditPreview GetEditor()
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return null;
            return mc.Editor;
        }

        internal void ValidateSchemaClosing()
        {
            this._ValidateRdl = null;
        }

        private void NoteRecentFiles(string name, bool bResetMenu)
        {
            if (name == null)
                return;

            name = name.Trim();
            if (_RecentFiles.ContainsValue(name))
            {	// need to move it to top of list; so remove old one
                int loc = _RecentFiles.IndexOfValue(name);
                _RecentFiles.RemoveAt(loc);
            }
            if (_RecentFiles.Count >= _RecentFilesMax)
            {
                _RecentFiles.RemoveAt(0);	// remove the first entry
            }
            _RecentFiles.Add(DateTime.Now, name);
            if (bResetMenu)
                RecentFilesMenu();
            return;
        }

        internal void RecentFilesMenu()
        {
            this.miRecentFile.DropDownItems.Clear();
            int mi = 1;
            for (int i = _RecentFiles.Count - 1; i >= 0; i--)
            {
                string menuText = string.Format("&{0} {1}", mi++, (string)(_RecentFiles.GetValueList()[i]));
                ToolStripMenuItem m = new ToolStripMenuItem(menuText);
                m.Click += new EventHandler(this.miRecentItem_Click);
                this.miRecentFile.DropDownItems.Add(m);
            }
        }        

        #region 启动状态

        private void GetStartupState()
        {
            string optFileName = AppDomain.CurrentDomain.BaseDirectory + "designerstate.xml";
            _RecentFiles = new SortedList();
            _CurrentFiles = new ArrayList();
            _HelpUrl = DefaultHelpUrl;				// set as default
            
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.PreserveWhitespace = false;
                xDoc.Load(optFileName);
                XmlNode xNode;
                xNode = xDoc.SelectSingleNode("//designerstate");

                string[] args = Environment.GetCommandLineArgs();
                for (int i = 1; i < args.Length; i++)
                {
                    string larg = args[i].ToLower();
                    if (larg == "/m" || larg == "-m")
                        continue;

                    if (File.Exists(args[i]))			// only add it if it exists
                        _CurrentFiles.Add(args[i]);
                }

                // Loop thru all the child nodes
                foreach (XmlNode xNodeLoop in xNode.ChildNodes)
                {
                    switch (xNodeLoop.Name)
                    {
                        case "RecentFiles":
                            DateTime now = DateTime.Now;
                            now = now.Subtract(new TimeSpan(0, 1, 0, 0, 0));	// subtract an hour
                            foreach (XmlNode xN in xNodeLoop.ChildNodes)
                            {
                                string file = xN.InnerText.Trim();
                                if (File.Exists(file))			// only add it if it exists
                                {
                                    _RecentFiles.Add(now, file);
                                    now = now.AddSeconds(1);
                                }
                            }
                            break;
                        case "RecentFilesMax":
                            try
                            {
                                this._RecentFilesMax = Convert.ToInt32(xNodeLoop.InnerText);
                            }
                            catch
                            {
                                this._RecentFilesMax = 5;
                            }
                            break;
                        case "CurrentFiles":
                            if (_CurrentFiles.Count > 0)	// don't open other current files if opened with argument
                                break;
                            foreach (XmlNode xN in xNodeLoop.ChildNodes)
                            {
                                string file = xN.InnerText.Trim();
                                if (File.Exists(file))			// only add it if it exists
                                    _CurrentFiles.Add(file);
                            }
                            break;
                        case "Help":
                            if (xNodeLoop.InnerText.Length > 0)		//empty means to use the default
                                _HelpUrl = xNodeLoop.InnerText;
                            break;                        
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {		// Didn't sucessfully get the startup state but don't really care
                Console.WriteLine(string.Format("Exception in GetStartupState ignored.\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
            
            return;
        }

        private void SaveStartupState()
        {
            try
            {
                int[] colors = GetCustomColors();		// get custom colors

                XmlDocument xDoc = new XmlDocument();
                XmlProcessingInstruction xPI;
                xPI = xDoc.CreateProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
                xDoc.AppendChild(xPI);

                XmlNode xDS = xDoc.CreateElement("designerstate");
                xDoc.AppendChild(xDS);

                XmlNode xN;
                // Loop thru the current files
                XmlNode xFiles = xDoc.CreateElement("CurrentFiles");
                xDS.AppendChild(xFiles);
                foreach (MDIChild mc in this.MdiChildren)
                {
                    string file = mc.SourceFile;
                    if (file == null)
                        continue;
                    xN = xDoc.CreateElement("file");
                    xN.InnerText = file;
                    xFiles.AppendChild(xN);
                }

                // Recent File Count
                XmlNode rfc = xDoc.CreateElement("RecentFilesMax");
                xDS.AppendChild(rfc);
                rfc.InnerText = this._RecentFilesMax.ToString();

                // Loop thru recent files list
                xFiles = xDoc.CreateElement("RecentFiles");
                xDS.AppendChild(xFiles);
                foreach (string f in _RecentFiles.Values)
                {
                    xN = xDoc.CreateElement("file");
                    xN.InnerText = f;
                    xFiles.AppendChild(xN);
                }

                // Help File URL
                XmlNode hfu = xDoc.CreateElement("Help");
                xDS.AppendChild(hfu);
                hfu.InnerText = this._HelpUrl;

                // Save the custom colors
                StringBuilder sb = new StringBuilder();
                foreach (int c in colors)
                {
                    sb.Append(c.ToString());
                    sb.Append(",");
                }
                sb.Remove(sb.Length - 1, 1);	// remove last ","

                xN = xDoc.CreateElement("CustomColors");
                xN.InnerText = sb.ToString();
                xDS.AppendChild(xN);

                // Save the file
                string optFileName = AppDomain.CurrentDomain.BaseDirectory + "designerstate.xml";

                xDoc.Save(optFileName);
            }
            catch { }		// still want to leave even on error

            return;
        }

        #endregion

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

        private void SetMDIChild2Focus(MDIChild mc)
        {
            // We don't want to be triggering any change events when the focus is changing
            bool bSuppress = bSuppressChange;
            bSuppressChange = true;
            mc.SetFocus();
            bSuppressChange = bSuppress;
        }

        private void SetStatusNameAndPosition()
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;

            SetProperties(mc);

            if (mc == null)
            {
                statusPosition.Text = statusSelected.Text = "";
            }
            else if (mc.DesignTab == "design")
                SetStatusNameAndPositionDesign(mc);
            else if (mc.DesignTab == "edit")
                SetStatusNameAndPositionEdit(mc);
            else
            {
                statusPosition.Text = statusSelected.Text = "";
            }
            return;
        }

        private void SetStatusNameAndPositionDesign(MDIChild mc)
        {
            if (mc.DrawCtl.SelectedCount <= 0)
            {
                statusPosition.Text = statusSelected.Text = "";
                return;
            }

            // Handle position
            PointF pos = mc.SelectionPosition;
            SizeF sz = mc.SelectionSize;
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
            string sname = mc.SelectionName;
            if (sname != statusSelected.Text)
                statusSelected.Text = sname;
            return;
        }

        private void SetStatusNameAndPositionEdit(MDIChild mc)
        {
            string spos = string.Format("Ln {0}  Ch {1}", mc.CurrentLine, mc.CurrentCh);
            if (spos != statusSelected.Text)
                statusSelected.Text = spos;

            if (statusPosition.Text != "")
                statusPosition.Text = "";

            return;
        }

        private void SetProperties(MDIChild mc)
        {
            if (mc == null || !_ShowProperties || mc.DesignTab != "design")
                mainProperties.ResetSelection(null, null);
            else
                mainProperties.ResetSelection(mc.RdlEditor.DrawCtl, mc.RdlEditor.DesignCtl);
        }

        internal void ShowProperties(bool bShow)
        {
            _ShowProperties = bShow;
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null || !_ShowProperties || mc.DesignTab != "design")
                mainProperties.ResetSelection(null, null);
            else
                mainProperties.ResetSelection(mc.RdlEditor.DrawCtl, mc.RdlEditor.DesignCtl);

            if (mc != null && !_ShowProperties)
                mc.SetFocus();
            mainProperties.Visible = mainSp.Visible = _ShowProperties;
            this.miProperties.Checked = _ShowProperties;
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

        private void CleanupTempFiles()
        {
            if (_TempReportFiles == null)
                return;
            foreach (string file in _TempReportFiles)
            {
                try
                {	// It's ok for the delete to fail
                    File.Delete(file);
                }
                catch
                { }
            }
            _TempReportFiles = null;
        }

        #region 一级菜单

        private void miFile_Click(object sender, EventArgs e)
        {
            bool bEnable = this.MdiChildren.Length > 0 ? true : false;

            this.miClose.Enabled = bEnable;
            this.miSave.Enabled = bEnable;
            this.miSaveAs.Enabled = bEnable;

            MDIChild mc = this.ActiveMdiChild as MDIChild;
            this.miPrint.Enabled = this.miPrint.Enabled = (mc != null && mc.DesignTab == "preview");
 
            this.miRecentFile.Enabled = this._RecentFiles.Count <= 0 ? false : true;
        }

        private void miData_Click(object sender, EventArgs ea)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;

            bool bEnable = false;

            if (mc != null && mc.DesignTab == "design")
                bEnable = true;

            this.miDataSource.Enabled = this.miDataSet.Enabled = this.miEmbeddedImage.Enabled = bEnable;
            if (!bEnable)
                return;

            // Run thru all the existing DataSets
            this.miDataSet.DropDownItems.Clear();
            this.miDataSet.DropDownItems.Add(new ToolStripMenuItem("New...",null,
                        new EventHandler(this.miDataSet_Click)));

            DesignXmlDraw draw = mc.DrawCtl;
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
            MDIChild mc = this.ActiveMdiChild as MDIChild;

            // These menus require an MDIChild2 in order to work
            RdlEditPreview e = GetEditor();            
            
            if (e == null || e.DesignTab != "edit")
            {
                if (mc == null || e == null)
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

            //bool bAnyText = e.Text.Length > 0;			// any text to search at all?

            //menuEditFind.Enabled = menuEditFindNext.Enabled =
            //    menuEditReplace.Enabled = menuEditGoto.Enabled = bAnyText;
        }

        private void miTools_Click(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            this.miValidateRDL.Enabled = (mc != null && mc.DesignTab == "edit");
        }

        private void miWindow_Click(object sender, EventArgs e)
        {
            // These menus require an MDIChild2 in order to work
            bool bEnable = this.MdiChildren.Length > 0 ? true : false;

            this.miCascade.Enabled = bEnable;
            this.miTileHor.Enabled = bEnable;
            this.miTileVer.Enabled = bEnable;
            this.miCloseAll.Enabled = bEnable;
        }

        private void miFormat_Click(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;

            // Determine if group operation on selected is currently allowed
            bool bEnable = (mc != null && mc.DesignTab == "design" && mc.DrawCtl.AllowGroupOperationOnSelected);

            this.miAlignBottom.Enabled = this.miAlignCenter.Enabled =
                this.miAlignLeft.Enabled = this.miAlignMiddle.Enabled =
                this.miAlignRight.Enabled = this.miAlignTop.Enabled = bEnable;

            this.miSizeWidth.Enabled = this.miSizeHeight.Enabled = this.miSizeBoth.Enabled = bEnable;

            this.miHorSpacM.Enabled = this.miHorSpacI.Enabled = this.miHorSpacD.Enabled =
                this.miHorSpacE.Enabled = bEnable;

            this.miVerSpacM.Enabled = this.miVerSpacI.Enabled = this.miVerSpacD.Enabled =
                this.miVerSpacE.Enabled = bEnable;

            bEnable = (mc != null && mc.DesignTab == "design" && mc.DrawCtl.SelectedCount > 0);

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
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            bool bEnable = mc != null;

            this.miDesign.Enabled = this.miSource.Enabled = this.miPreview.Enabled = bEnable;

            this.miProperties.Enabled = bEnable && mc.DesignTab == "design";
        }

        #endregion

        #region 文件菜单

        private void miClose_Click(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc != null)
                mc.Close();
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            if (!OkToSave())
                return;

            SaveStartupState();

            //menuToolsCloseProcess(false);

            CleanupTempFiles();
            Environment.Exit(0);
        }

        private void miOpen_Click(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            OpenFileDialog ofd = new OpenFileDialog();
            
            if (mc != null)
            {
                try
                {
                    ofd.InitialDirectory = Path.GetDirectoryName(mc.SourceFile);
                }
                catch
                {
                }
            }

            ofd.DefaultExt = "rdl";
            ofd.Filter = "Report files (*.rdl)|*.rdl|" +
                "All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.CheckFileExists = true;
            ofd.Multiselect = true;
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                foreach (string file in ofd.FileNames)
                {
                    CreateMDIChild(file, null, false);
                }
                RecentFilesMenu();		// update the menu for recent files
            }
        }

        private void miNewDb_Click(object sender, System.EventArgs e)
        {
            DialogDatabase dlgDB = new DialogDatabase();
            dlgDB.StartPosition = FormStartPosition.CenterParent;
            //dlgDB.FormBorderStyle = FormBorderStyle.SizableToolWindow;

            // show modally
            dlgDB.ShowDialog();
            if (dlgDB.DialogResult == DialogResult.Cancel)
                return;
            string rdl = dlgDB.ResultReport;

            // Create the MDI child using the RDL syntax the wizard generates
            MDIChild mc = CreateMDIChild(null, rdl, false);
            mc.Modified = true;
            // Force building of report names for new reports
            if (mc.DrawCtl.ReportNames == null) { }
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
            string rdl = dlgObject.ResultReport;

            // Create the MDI child using the RDL syntax the wizard generates
            MDIChild mc = CreateMDIChild(null, rdl, false);
            mc.Modified = true;
            // Force building of report names for new reports
            if (mc.DrawCtl.ReportNames == null) { }
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
            string rdl = dlgObject.ResultReport;

            // Create the MDI child using the RDL syntax the wizard generates
            MDIChild mc = CreateMDIChild(null, rdl, false);
            mc.Modified = true;
            // Force building of report names for new reports
            if (mc.DrawCtl.ReportNames == null) { }
        }

        private void miPrint_Click(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;
            if (printChild != null)			// already printing
            {
                MessageBox.Show("Can only print one file at a time.", "RDL Design");
                return;
            }

            printChild = mc;

            PrintDocument pd = new PrintDocument();
            pd.DocumentName = mc.SourceFile;
            pd.PrinterSettings.FromPage = 1;
            pd.PrinterSettings.ToPage = mc.PageCount;
            pd.PrinterSettings.MaximumPage = mc.PageCount;
            pd.PrinterSettings.MinimumPage = 1;
            pd.DefaultPageSettings.Landscape = mc.PageWidth > mc.PageHeight ? true : false;

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
                        pd.PrinterSettings.FromPage = mc.PageCurrent;
                    }
                    mc.Print(pd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Print error: " + ex.Message, "RDL Design");
                }
            }
            printChild = null;
        }

        private void miSave_Click(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            if (!mc.FileSave())
                return;

            NoteRecentFiles(mc.SourceFile, true);

            if (mc.Editor != null)
                mc.Editor.ClearUndo();

            return;
        }

        private void miSaveAs_Click(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            if (!mc.FileSaveAs())
                return;

            mc.Viewer.Folder = Path.GetDirectoryName(mc.SourceFile);
            mc.Viewer.ReportName = Path.GetFileNameWithoutExtension(mc.SourceFile);
            mc.Text = Path.GetFileName(mc.SourceFile);

            NoteRecentFiles(mc.SourceFile, true);

            if (mc.Editor != null)
                mc.Editor.ClearUndo();

            return;
        }

        private void miExportXml_Click(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            mc.Export("xml");
            return;
        }

        private void miExportHtml_Click(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            mc.Export("html");
            return;
        }

        private void miExportMHtml_Click(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            mc.Export("mht");
            return;
        }

        private void miExportPdf_Click(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            mc.Export("pdf");
            return;
        }

        private void miExportExcel_Click(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            mc.Export("xlsx");
            return;
        }

        private void miExportRtf_Click(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            mc.Export("rtf");
            return;
        }

        private void miRecentItem_Click(object sender, System.EventArgs e)
        {
            ToolStripMenuItem m = (ToolStripMenuItem)sender;
            int si = m.Text.IndexOf(" ");
            string file = m.Text.Substring(si + 1);

            CreateMDIChild(file, null, true);
        }

        #endregion        

        #region 数据菜单

        private void miDataSource_Click(object sender, System.EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            mc.Editor.StartUndoGroup("DataSources Dialog");
            using (DialogDataSources dlgDS = new DialogDataSources(mc.SourceFile, mc.DrawCtl))
            {
                dlgDS.StartPosition = FormStartPosition.CenterParent;
                DialogResult dr = dlgDS.ShowDialog();
                mc.Editor.EndUndoGroup(dr == DialogResult.OK);
                if (dr == DialogResult.OK)
                    mc.Modified = true;
            }
        }

        private void miDataSet_Click(object sender, System.EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null || mc.DrawCtl == null || mc.ReportDocument == null)
                return;

            MenuItem menu = sender as MenuItem;
            if (menu == null)
                return;
            mc.Editor.StartUndoGroup("DataSet Dialog");

            string dsname = menu.Text;

            // Find the dataset we need
            List<XmlNode> ds = new List<XmlNode>();
            DesignXmlDraw draw = mc.DrawCtl;
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

            using (PropertyDialog pd = new PropertyDialog(mc.DrawCtl, ds, PropertyTypeEnum.DataSets))
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
                    mc.Modified = true;
                }
                else if (bNew)	// if canceled and new DataSet get rid of temp node
                {
                    dsNode.RemoveChild(dataset);
                }
                if (pd.Delete)	// user must have hit a delete button for this to get set
                    dsNode.RemoveChild(dataset);

                if (!dsNode.HasChildNodes)		// If no dataset exists we remove DataSets
                    draw.RemoveElement(rNode, "DataSets");

                mc.Editor.EndUndoGroup(pd.Changed || dr == DialogResult.OK);
            }
        }

        private void miEmbeddedImage_Click(object sender, System.EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            mc.Editor.StartUndoGroup("Embedded Images Dialog");
            DialogEmbeddedImages dlgEI = new DialogEmbeddedImages(mc.DrawCtl);
            dlgEI.StartPosition = FormStartPosition.CenterParent;
            DialogResult dr = dlgEI.ShowDialog();
            mc.Editor.EndUndoGroup(dr == DialogResult.OK);
            if (dr == DialogResult.OK)
                mc.Modified = true;
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

        private void miProperties_Click(object sender, System.EventArgs ea)
        {
            this.ShowProperties(!this._ShowProperties);
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

        #region 工具菜单

        private void miOption_Click(object sender, EventArgs e)
        {
            
        }

        private void miValidateRDL_Click(object sender, EventArgs e)
        {
            if (_ValidateRdl == null)
            {
                _ValidateRdl = new DialogValidateRdl(this);
                _ValidateRdl.Show();
            }
            else
                _ValidateRdl.BringToFront();
            return;
        }

        #endregion

        #region 窗口菜单

        private void miCascade_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void miCloseAll_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                f.Close();
            }
        }

        private void miCloseAllButCurrent_Click(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            foreach (Form f in this.MdiChildren)
            {
                if (mc == f as MDIChild)
                    continue;
                f.Close();
            }
            return;
        }

        private void miTileHor_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void miTileVer_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
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
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            this.ButtonInsertCurrent = sender as ToolStripButton;

            mc.SetFocus();
            mc.CurrentInsert = "Textbox";
        }

        private void btnChart_Click(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            this.ButtonInsertCurrent = sender as ToolStripButton;

            mc.SetFocus();
            mc.CurrentInsert = "Chart";
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            this.ButtonInsertCurrent = sender as ToolStripButton;

            mc.SetFocus();
            mc.CurrentInsert = "Table";
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            this.ButtonInsertCurrent = sender as ToolStripButton;

            mc.SetFocus();
            mc.CurrentInsert = "List";
        }

        private void btnImage_Click(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            this.ButtonInsertCurrent = sender as ToolStripButton;

            mc.SetFocus();
            mc.CurrentInsert = "Image";
        }

        private void btnMatrix_Click(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            this.ButtonInsertCurrent = sender as ToolStripButton;

            mc.SetFocus();
            mc.CurrentInsert = "Matrix";
        }

        private void btnSubReport_Click(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            this.ButtonInsertCurrent = sender as ToolStripButton;

            mc.SetFocus();
            mc.CurrentInsert = "Subreport";
        }

        private void btnRect_Click(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            this.ButtonInsertCurrent = sender as ToolStripButton;

            mc.SetFocus();
            mc.CurrentInsert = "Rectangle";
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            this.ButtonInsertCurrent = sender as ToolStripButton;

            mc.SetFocus();
            mc.CurrentInsert = "Line";
        }

        private void tbFunction_Validated(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null || mc == null ||
                mc.DesignTab != "design" || mc.DrawCtl.SelectedCount != 1 ||
                mc.Editor == null)
                return;

            mc.Editor.SetSelectedText(this.tbFunction.Text);
        }

        private void tbFunction_KeyDown(object sender, KeyEventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            // Force scroll up and down
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    mc.SetFocus();
                    e.Handled = true;
                    break;
                case Keys.Escape:
                    if (mc.DrawCtl.SelectedCount == 1)
                    {
                        XmlNode tn = mc.DrawCtl.SelectedList[0] as XmlNode;
                        if (tn != null && tn.Name == "Textbox")
                        {
                            this.tbFunction.Text = mc.DrawCtl.GetElementValue(tn, "Value", "");
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
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            btnBold.Checked = !btnBold.Checked;
            mc.ApplyStyleToSelected("FontWeight", this.btnBold.Checked ? "Bold" : "Normal");
            SetMDIChild2Focus(mc);
        }

        private void btnItalic_Click(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            btnItalic.Checked = !btnItalic.Checked;
            mc.ApplyStyleToSelected("FontStyle", this.btnItalic.Checked ? "Italic" : "Normal");
            SetMDIChild2Focus(mc);
        }

        private void btnUnderline_Click(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            this.btnUnderLine.Checked = !this.btnUnderLine.Checked;
            mc.ApplyStyleToSelected("TextDecoration", this.btnUnderLine.Checked ? "Underline" : "None");
            SetMDIChild2Focus(mc);
        }

        private void btnAlignLeft_Click(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            if (!this.btnAlignLeft.Checked)
            {
                this.btnAlignLeft.Checked = true;
                this.btnAlignCenter.Checked = false;
                this.btnAlignRight.Checked = false;

                mc.ApplyStyleToSelected("TextAlign", "Left");
            }
        }

        private void btnAlignCenter_Click(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            if (!this.btnAlignCenter.Checked)
            {
                this.btnAlignLeft.Checked = false;
                this.btnAlignCenter.Checked = true;
                this.btnAlignRight.Checked = false;

                mc.ApplyStyleToSelected("TextAlign", "Center");
            }
        }

        private void btnAlignRight_Click(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            if (!this.btnAlignRight.Checked)
            {
                this.btnAlignLeft.Checked = false;
                this.btnAlignCenter.Checked = false;
                this.btnAlignRight.Checked = true;

                mc.ApplyStyleToSelected("TextAlign", "Right");
            }
        }

        private void btnForeColor_Click(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            
            if (mc == null)
                return;

            this.colorDialog.AnyColor = true;
            this.colorDialog.FullOpen = true;

            if (this.colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                string htmlColor = ColorTranslator.ToHtml(colorDialog.Color);
                mc.ApplyStyleToSelected("Color", htmlColor);

                SetMDIChild2Focus(mc);
            }
        }

        private void btnBackColor_Click(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;

            if (mc == null)
                return;

            this.colorDialog.AnyColor = true;
            this.colorDialog.FullOpen = true;

            if (this.colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                string htmlColor = ColorTranslator.ToHtml(colorDialog.Color);
                mc.ApplyStyleToSelected("BackgroundColor", htmlColor);

                SetMDIChild2Focus(mc);
            }
        }

        private void cbxFont_Change(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            if (!bSuppressChange)
                mc.ApplyStyleToSelected("FontFamily", this.cbxFont.Text);

            SetMDIChild2Focus(mc);
        }

        private void cbxFontSize_Change(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            if (!bSuppressChange)
                mc.ApplyStyleToSelected("FontSize", this.cbxFontSize.Text + "pt");

            SetMDIChild2Focus(mc);
        }

        private void btnZoomUp_Click(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            int offset = -1;

            for (int i = 0; i < RDLHelper.ZoomList.Length; i++)
            {
                if (System.Math.Abs(mc.Zoom - RDLHelper.ZoomList[i]) < 0.01f)
                {
                    offset = i;
                    break;
                }
            }

            if (offset != 0)
                mc.Zoom = RDLHelper.ZoomList[offset - 1];
        }

        private void btnZoomDown_Click(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            int offset = -1;

            for (int i = 0; i < RDLHelper.ZoomList.Length; i++)
            {
                if (System.Math.Abs(mc.Zoom - RDLHelper.ZoomList[i]) < 0.01f)
                {
                    offset = i;
                    break;
                }
            }

            if (offset != RDLHelper.ZoomList.Length - 1)
                mc.Zoom = RDLHelper.ZoomList[offset + 1];
        }

        private void cbxZoom_Change(object sender, EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;
            mc.SetFocus();

            switch (this.cbxZoom.Text)
            {
                case "Actual Size":
                    mc.Zoom = 1;
                    break;
                case "Fit Page":
                    mc.ZoomMode = ZoomEnum.FitPage;
                    break;
                case "Fit Width":
                    mc.ZoomMode = ZoomEnum.FitWidth;
                    break;
                default:
                    string s = this.cbxZoom.Text.Substring(0, this.cbxZoom.Text.Length - 1);
                    float z;
                    try
                    {
                        z = Convert.ToSingle(s) / 100f;
                        mc.Zoom = z;
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
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            mc.Editor.DesignCtl.AlignCenters();
        }

        private void miAlignLeft_Click(object sender, System.EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            mc.Editor.DesignCtl.AlignLefts();
        }

        private void miAlignRight_Click(object sender, System.EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            mc.Editor.DesignCtl.AlignRights();
        }

        private void miAlignBottom_Click(object sender, System.EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            mc.Editor.DesignCtl.AlignBottoms();
        }

        private void miAlignTop_Click(object sender, System.EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            mc.Editor.DesignCtl.AlignTops();
        }

        private void miAlignMiddle_Click(object sender, System.EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            mc.Editor.DesignCtl.AlignMiddles();
        }

        private void miSizeHeight_Click(object sender, System.EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            mc.Editor.DesignCtl.SizeHeights();
        }

        private void miSizeWidth_Click(object sender, System.EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            mc.Editor.DesignCtl.SizeWidths();
        }

        private void miSizeBoth_Click(object sender, System.EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            mc.Editor.DesignCtl.SizeBoth();
        }

        private void miHorSpacM_Click(object sender, System.EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            mc.Editor.DesignCtl.HorzSpacingMakeEqual();
        }

        private void miHorSpacI_Click(object sender, System.EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            mc.Editor.DesignCtl.HorzSpacingIncrease();
        }

        private void miHorSpacD_Click(object sender, System.EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            mc.Editor.DesignCtl.HorzSpacingDecrease();
        }

        private void miHorSpacE_Click(object sender, System.EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            mc.Editor.DesignCtl.HorzSpacingMakeZero();
        }

        private void miVerSpacM_Click(object sender, System.EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            mc.Editor.DesignCtl.VertSpacingMakeEqual();
        }

        private void miVerSpacI_Click(object sender, System.EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            mc.Editor.DesignCtl.VertSpacingIncrease();
        }

        private void miVerSpacD_Click(object sender, System.EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            mc.Editor.DesignCtl.VertSpacingDecrease();
        }

        private void miVerSpacE_Click(object sender, System.EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

            mc.Editor.DesignCtl.VertSpacingMakeZero();
        }

        private void miPadding_Click(object sender, System.EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;

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
                mc.Editor.DesignCtl.SetPadding(padname, paddiff);
            }
        }

        #endregion        

        #region 视图菜单

        private void miDesign_Click(object sender, System.EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;
            mc.RdlEditor.DesignTab = "design";
        }

        private void miSource_Click(object sender, System.EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;
            mc.RdlEditor.DesignTab = "edit";
        }                

        private void miPreview_Click(object sender, System.EventArgs e)
        {
            MDIChild mc = this.ActiveMdiChild as MDIChild;
            if (mc == null)
                return;
            mc.RdlEditor.DesignTab = "preview";
        }

        #endregion                        
    }
}