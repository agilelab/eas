namespace fyiReporting.RdlDesign
{
    partial class DialogDataObject
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogDataObject));
            this.DataObject = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bParmDown = new System.Windows.Forms.Button();
            this.bParmUp = new System.Windows.Forms.Button();
            this.tbParmValidValues = new System.Windows.Forms.TextBox();
            this.lbParmValidValues = new System.Windows.Forms.Label();
            this.ckbParmAllowBlank = new System.Windows.Forms.CheckBox();
            this.ckbParmAllowNull = new System.Windows.Forms.CheckBox();
            this.tbParmPrompt = new System.Windows.Forms.TextBox();
            this.lParmPrompt = new System.Windows.Forms.Label();
            this.cbParmType = new System.Windows.Forms.ComboBox();
            this.lParmType = new System.Windows.Forms.Label();
            this.tbParmName = new System.Windows.Forms.TextBox();
            this.lParmName = new System.Windows.Forms.Label();
            this.bRemove = new System.Windows.Forms.Button();
            this.bAdd = new System.Windows.Forms.Button();
            this.lbParameters = new System.Windows.Forms.ListBox();
            this.tbReportSyntax = new System.Windows.Forms.TextBox();
            this.ReportPreview = new System.Windows.Forms.TabPage();
            this.rdlViewer1 = new fyiReporting.RdlViewer.RdlViewer();
            this.cbColumnList = new System.Windows.Forms.ComboBox();
            this.ReportSyntax = new System.Windows.Forms.TabPage();
            this.tcDialog = new System.Windows.Forms.TabControl();
            this.ReportType = new System.Windows.Forms.TabPage();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tbHeight = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbWidth = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbxPages = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbSchema2005 = new System.Windows.Forms.RadioButton();
            this.rbSchema2003 = new System.Windows.Forms.RadioButton();
            this.rbSchemaNo = new System.Windows.Forms.RadioButton();
            this.cbOrientation = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbReportAuthor = new System.Windows.Forms.TextBox();
            this.tbReportDescription = new System.Windows.Forms.TextBox();
            this.tbReportName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbChart = new System.Windows.Forms.RadioButton();
            this.rbMatrix = new System.Windows.Forms.RadioButton();
            this.rbList = new System.Windows.Forms.RadioButton();
            this.rbTable = new System.Windows.Forms.RadioButton();
            this.DBConnection = new System.Windows.Forms.TabPage();
            this.tbConnection = new System.Windows.Forms.TextBox();
            this.btnSetConnect = new System.Windows.Forms.Button();
            this.bTestConnection = new System.Windows.Forms.Button();
            this.lConnection = new System.Windows.Forms.Label();
            this.cbConnectionTypes = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ReportParameters = new System.Windows.Forms.TabPage();
            this.bValidValues = new System.Windows.Forms.Button();
            this.tbParmDefaultValue = new System.Windows.Forms.TextBox();
            this.lDefaultValue = new System.Windows.Forms.Label();
            this.TabularGroup = new System.Windows.Forms.TabPage();
            this.clbSubtotal = new System.Windows.Forms.CheckedListBox();
            this.ckbGrandTotal = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnNext = new System.Windows.Forms.Button();
            this.tbAssembly = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnBrowser = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.lvData = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.cbTable = new System.Windows.Forms.CheckBox();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.cbxTypes = new System.Windows.Forms.ComboBox();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.DataObject.SuspendLayout();
            this.panel2.SuspendLayout();
            this.ReportPreview.SuspendLayout();
            this.ReportSyntax.SuspendLayout();
            this.tcDialog.SuspendLayout();
            this.ReportType.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.DBConnection.SuspendLayout();
            this.ReportParameters.SuspendLayout();
            this.TabularGroup.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataObject
            // 
            this.DataObject.Controls.Add(this.panel2);
            this.DataObject.Location = new System.Drawing.Point(4, 21);
            this.DataObject.Name = "DataObject";
            this.DataObject.Size = new System.Drawing.Size(531, 330);
            this.DataObject.TabIndex = 1;
            this.DataObject.Tag = "sql";
            this.DataObject.Text = "数据对象";
            this.DataObject.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbxTypes);
            this.panel2.Controls.Add(this.cbTable);
            this.panel2.Controls.Add(this.lvData);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.btnBrowser);
            this.panel2.Controls.Add(this.tbAssembly);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(531, 330);
            this.panel2.TabIndex = 1;
            // 
            // bParmDown
            // 
            this.bParmDown.Font = new System.Drawing.Font("Wingdings", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.bParmDown.Location = new System.Drawing.Point(198, 129);
            this.bParmDown.Name = "bParmDown";
            this.bParmDown.Size = new System.Drawing.Size(28, 26);
            this.bParmDown.TabIndex = 14;
            this.bParmDown.Text = "";
            this.bParmDown.Click += new System.EventHandler(this.bParmDown_Click);
            // 
            // bParmUp
            // 
            this.bParmUp.Font = new System.Drawing.Font("Wingdings", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.bParmUp.Location = new System.Drawing.Point(198, 86);
            this.bParmUp.Name = "bParmUp";
            this.bParmUp.Size = new System.Drawing.Size(28, 26);
            this.bParmUp.TabIndex = 13;
            this.bParmUp.Text = "";
            this.bParmUp.Click += new System.EventHandler(this.bParmUp_Click);
            // 
            // tbParmValidValues
            // 
            this.tbParmValidValues.Location = new System.Drawing.Point(250, 164);
            this.tbParmValidValues.Name = "tbParmValidValues";
            this.tbParmValidValues.ReadOnly = true;
            this.tbParmValidValues.Size = new System.Drawing.Size(231, 21);
            this.tbParmValidValues.TabIndex = 9;
            // 
            // lbParmValidValues
            // 
            this.lbParmValidValues.AutoSize = true;
            this.lbParmValidValues.Location = new System.Drawing.Point(250, 146);
            this.lbParmValidValues.Name = "lbParmValidValues";
            this.lbParmValidValues.Size = new System.Drawing.Size(65, 12);
            this.lbParmValidValues.TabIndex = 11;
            this.lbParmValidValues.Text = "有 效 值：";
            this.lbParmValidValues.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ckbParmAllowBlank
            // 
            this.ckbParmAllowBlank.Location = new System.Drawing.Point(346, 250);
            this.ckbParmAllowBlank.Name = "ckbParmAllowBlank";
            this.ckbParmAllowBlank.Size = new System.Drawing.Size(96, 26);
            this.ckbParmAllowBlank.TabIndex = 12;
            this.ckbParmAllowBlank.Text = "充许空字符";
            this.ckbParmAllowBlank.CheckedChanged += new System.EventHandler(this.ckbParmAllowBlank_CheckedChanged);
            // 
            // ckbParmAllowNull
            // 
            this.ckbParmAllowNull.Location = new System.Drawing.Point(250, 250);
            this.ckbParmAllowNull.Name = "ckbParmAllowNull";
            this.ckbParmAllowNull.Size = new System.Drawing.Size(86, 26);
            this.ckbParmAllowNull.TabIndex = 11;
            this.ckbParmAllowNull.Text = "允许空值";
            this.ckbParmAllowNull.CheckedChanged += new System.EventHandler(this.ckbParmAllowNull_CheckedChanged);
            // 
            // tbParmPrompt
            // 
            this.tbParmPrompt.Location = new System.Drawing.Point(309, 95);
            this.tbParmPrompt.Name = "tbParmPrompt";
            this.tbParmPrompt.Size = new System.Drawing.Size(172, 21);
            this.tbParmPrompt.TabIndex = 8;
            this.tbParmPrompt.TextChanged += new System.EventHandler(this.tbParmPrompt_TextChanged);
            // 
            // lParmPrompt
            // 
            this.lParmPrompt.AutoSize = true;
            this.lParmPrompt.Location = new System.Drawing.Point(250, 99);
            this.lParmPrompt.Name = "lParmPrompt";
            this.lParmPrompt.Size = new System.Drawing.Size(65, 12);
            this.lParmPrompt.TabIndex = 7;
            this.lParmPrompt.Text = "格    式：";
            this.lParmPrompt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbParmType
            // 
            this.cbParmType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbParmType.Items.AddRange(new object[] {
            "Boolean",
            "DateTime",
            "Integer",
            "Float",
            "String"});
            this.cbParmType.Location = new System.Drawing.Point(309, 57);
            this.cbParmType.Name = "cbParmType";
            this.cbParmType.Size = new System.Drawing.Size(96, 20);
            this.cbParmType.TabIndex = 6;
            this.cbParmType.SelectedIndexChanged += new System.EventHandler(this.cbParmType_SelectedIndexChanged);
            // 
            // lParmType
            // 
            this.lParmType.AutoSize = true;
            this.lParmType.Location = new System.Drawing.Point(250, 61);
            this.lParmType.Name = "lParmType";
            this.lParmType.Size = new System.Drawing.Size(65, 12);
            this.lParmType.TabIndex = 5;
            this.lParmType.Text = "参数类型：";
            this.lParmType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbParmName
            // 
            this.tbParmName.Location = new System.Drawing.Point(309, 17);
            this.tbParmName.Name = "tbParmName";
            this.tbParmName.Size = new System.Drawing.Size(172, 21);
            this.tbParmName.TabIndex = 4;
            this.tbParmName.TextChanged += new System.EventHandler(this.tbParmName_TextChanged);
            // 
            // lParmName
            // 
            this.lParmName.AutoSize = true;
            this.lParmName.Location = new System.Drawing.Point(250, 21);
            this.lParmName.Name = "lParmName";
            this.lParmName.Size = new System.Drawing.Size(65, 12);
            this.lParmName.TabIndex = 3;
            this.lParmName.Text = "参数名称：";
            this.lParmName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bRemove
            // 
            this.bRemove.Location = new System.Drawing.Point(125, 284);
            this.bRemove.Name = "bRemove";
            this.bRemove.Size = new System.Drawing.Size(67, 25);
            this.bRemove.TabIndex = 2;
            this.bRemove.Text = "删除";
            this.bRemove.Click += new System.EventHandler(this.bRemove_Click);
            // 
            // bAdd
            // 
            this.bAdd.Location = new System.Drawing.Point(19, 284);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(67, 25);
            this.bAdd.TabIndex = 1;
            this.bAdd.Text = "添加";
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // lbParameters
            // 
            this.lbParameters.ItemHeight = 12;
            this.lbParameters.Location = new System.Drawing.Point(19, 17);
            this.lbParameters.Name = "lbParameters";
            this.lbParameters.Size = new System.Drawing.Size(173, 244);
            this.lbParameters.TabIndex = 0;
            this.lbParameters.SelectedIndexChanged += new System.EventHandler(this.lbParameters_SelectedIndexChanged);
            // 
            // tbReportSyntax
            // 
            this.tbReportSyntax.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbReportSyntax.Location = new System.Drawing.Point(0, 0);
            this.tbReportSyntax.Multiline = true;
            this.tbReportSyntax.Name = "tbReportSyntax";
            this.tbReportSyntax.ReadOnly = true;
            this.tbReportSyntax.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbReportSyntax.Size = new System.Drawing.Size(531, 330);
            this.tbReportSyntax.TabIndex = 0;
            this.tbReportSyntax.WordWrap = false;
            // 
            // ReportPreview
            // 
            this.ReportPreview.Controls.Add(this.rdlViewer1);
            this.ReportPreview.Location = new System.Drawing.Point(4, 21);
            this.ReportPreview.Name = "ReportPreview";
            this.ReportPreview.Size = new System.Drawing.Size(531, 330);
            this.ReportPreview.TabIndex = 5;
            this.ReportPreview.Tag = "preview";
            this.ReportPreview.Text = "预览";
            this.ReportPreview.UseVisualStyleBackColor = true;
            // 
            // rdlViewer1
            // 
            this.rdlViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.rdlViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdlViewer1.Folder = null;
            this.rdlViewer1.Location = new System.Drawing.Point(0, 0);
            this.rdlViewer1.Name = "rdlViewer1";
            this.rdlViewer1.PageCurrent = 1;
            this.rdlViewer1.Parameters = null;
            this.rdlViewer1.ReportName = null;
            this.rdlViewer1.ScrollMode = fyiReporting.RdlViewer.ScrollModeEnum.Continuous;
            this.rdlViewer1.ShowParameterPanel = false;
            this.rdlViewer1.Size = new System.Drawing.Size(531, 330);
            this.rdlViewer1.SourceFile = null;
            this.rdlViewer1.SourceRdl = null;
            this.rdlViewer1.TabIndex = 0;
            this.rdlViewer1.Text = "rdlViewer1";
            this.rdlViewer1.Zoom = 0.6077614F;
            this.rdlViewer1.ZoomMode = fyiReporting.RdlViewer.ZoomEnum.FitWidth;
            // 
            // cbColumnList
            // 
            this.cbColumnList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColumnList.Location = new System.Drawing.Point(19, 34);
            this.cbColumnList.Name = "cbColumnList";
            this.cbColumnList.Size = new System.Drawing.Size(240, 20);
            this.cbColumnList.TabIndex = 0;
            this.cbColumnList.SelectedIndexChanged += new System.EventHandler(this.emptyReportSyntax);
            // 
            // ReportSyntax
            // 
            this.ReportSyntax.Controls.Add(this.tbReportSyntax);
            this.ReportSyntax.Location = new System.Drawing.Point(4, 21);
            this.ReportSyntax.Name = "ReportSyntax";
            this.ReportSyntax.Size = new System.Drawing.Size(531, 330);
            this.ReportSyntax.TabIndex = 4;
            this.ReportSyntax.Tag = "syntax";
            this.ReportSyntax.Text = "RDL定义";
            this.ReportSyntax.UseVisualStyleBackColor = true;
            // 
            // tcDialog
            // 
            this.tcDialog.Controls.Add(this.ReportType);
            this.tcDialog.Controls.Add(this.DBConnection);
            this.tcDialog.Controls.Add(this.ReportParameters);
            this.tcDialog.Controls.Add(this.DataObject);
            this.tcDialog.Controls.Add(this.TabularGroup);
            this.tcDialog.Controls.Add(this.ReportSyntax);
            this.tcDialog.Controls.Add(this.ReportPreview);
            this.tcDialog.Location = new System.Drawing.Point(8, 9);
            this.tcDialog.Name = "tcDialog";
            this.tcDialog.SelectedIndex = 0;
            this.tcDialog.Size = new System.Drawing.Size(539, 355);
            this.tcDialog.TabIndex = 4;
            this.tcDialog.MouseHover += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // ReportType
            // 
            this.ReportType.Controls.Add(this.label12);
            this.ReportType.Controls.Add(this.label11);
            this.ReportType.Controls.Add(this.tbHeight);
            this.ReportType.Controls.Add(this.label10);
            this.ReportType.Controls.Add(this.tbWidth);
            this.ReportType.Controls.Add(this.label9);
            this.ReportType.Controls.Add(this.cbxPages);
            this.ReportType.Controls.Add(this.label8);
            this.ReportType.Controls.Add(this.groupBox2);
            this.ReportType.Controls.Add(this.cbOrientation);
            this.ReportType.Controls.Add(this.label6);
            this.ReportType.Controls.Add(this.tbReportAuthor);
            this.ReportType.Controls.Add(this.tbReportDescription);
            this.ReportType.Controls.Add(this.tbReportName);
            this.ReportType.Controls.Add(this.label3);
            this.ReportType.Controls.Add(this.label2);
            this.ReportType.Controls.Add(this.label1);
            this.ReportType.Controls.Add(this.groupBox1);
            this.ReportType.Location = new System.Drawing.Point(4, 21);
            this.ReportType.Name = "ReportType";
            this.ReportType.Size = new System.Drawing.Size(531, 330);
            this.ReportType.TabIndex = 3;
            this.ReportType.Tag = "type";
            this.ReportType.Text = "报表信息";
            this.ReportType.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(475, 234);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(17, 12);
            this.label12.TabIndex = 17;
            this.label12.Text = "MM";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(357, 234);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 12);
            this.label11.TabIndex = 16;
            this.label11.Text = "MM";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbHeight
            // 
            this.tbHeight.BackColor = System.Drawing.SystemColors.Window;
            this.tbHeight.Enabled = false;
            this.tbHeight.Location = new System.Drawing.Point(419, 230);
            this.tbHeight.MaxLength = 10;
            this.tbHeight.Name = "tbHeight";
            this.tbHeight.Size = new System.Drawing.Size(56, 21);
            this.tbHeight.TabIndex = 15;
            this.tbHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(383, 234);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 14;
            this.label10.Text = "高度：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbWidth
            // 
            this.tbWidth.BackColor = System.Drawing.SystemColors.Window;
            this.tbWidth.Enabled = false;
            this.tbWidth.Location = new System.Drawing.Point(300, 230);
            this.tbWidth.MaxLength = 10;
            this.tbWidth.Name = "tbWidth";
            this.tbWidth.Size = new System.Drawing.Size(56, 21);
            this.tbWidth.TabIndex = 13;
            this.tbWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(262, 234);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 12;
            this.label9.Text = "宽度：";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbxPages
            // 
            this.cbxPages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPages.Items.AddRange(new object[] {
            "纵向",
            "横向"});
            this.cbxPages.Location = new System.Drawing.Point(98, 230);
            this.cbxPages.Name = "cbxPages";
            this.cbxPages.Size = new System.Drawing.Size(152, 20);
            this.cbxPages.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(31, 234);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 10;
            this.label8.Text = "纸张大小：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbSchema2005);
            this.groupBox2.Controls.Add(this.rbSchema2003);
            this.groupBox2.Controls.Add(this.rbSchemaNo);
            this.groupBox2.Location = new System.Drawing.Point(31, 267);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(461, 43);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "RDL Schema";
            // 
            // rbSchema2005
            // 
            this.rbSchema2005.Checked = true;
            this.rbSchema2005.Location = new System.Drawing.Point(298, 17);
            this.rbSchema2005.Name = "rbSchema2005";
            this.rbSchema2005.Size = new System.Drawing.Size(54, 17);
            this.rbSchema2005.TabIndex = 2;
            this.rbSchema2005.TabStop = true;
            this.rbSchema2005.Text = "2005";
            // 
            // rbSchema2003
            // 
            this.rbSchema2003.Location = new System.Drawing.Point(144, 17);
            this.rbSchema2003.Name = "rbSchema2003";
            this.rbSchema2003.Size = new System.Drawing.Size(54, 17);
            this.rbSchema2003.TabIndex = 1;
            this.rbSchema2003.Text = "2003";
            // 
            // rbSchemaNo
            // 
            this.rbSchemaNo.Location = new System.Drawing.Point(10, 17);
            this.rbSchemaNo.Name = "rbSchemaNo";
            this.rbSchemaNo.Size = new System.Drawing.Size(54, 17);
            this.rbSchemaNo.TabIndex = 0;
            this.rbSchemaNo.Text = "None";
            // 
            // cbOrientation
            // 
            this.cbOrientation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrientation.Items.AddRange(new object[] {
            "纵向",
            "横向"});
            this.cbOrientation.Location = new System.Drawing.Point(98, 201);
            this.cbOrientation.Name = "cbOrientation";
            this.cbOrientation.Size = new System.Drawing.Size(152, 20);
            this.cbOrientation.TabIndex = 8;
            this.cbOrientation.SelectedIndexChanged += new System.EventHandler(this.emptyReportSyntax);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 205);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "方    向：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbReportAuthor
            // 
            this.tbReportAuthor.Location = new System.Drawing.Point(98, 171);
            this.tbReportAuthor.Name = "tbReportAuthor";
            this.tbReportAuthor.Size = new System.Drawing.Size(394, 21);
            this.tbReportAuthor.TabIndex = 6;
            this.tbReportAuthor.TextChanged += new System.EventHandler(this.tbReportAuthor_TextChanged);
            // 
            // tbReportDescription
            // 
            this.tbReportDescription.Location = new System.Drawing.Point(98, 141);
            this.tbReportDescription.Name = "tbReportDescription";
            this.tbReportDescription.Size = new System.Drawing.Size(394, 21);
            this.tbReportDescription.TabIndex = 5;
            this.tbReportDescription.TextChanged += new System.EventHandler(this.tbReportDescription_TextChanged);
            // 
            // tbReportName
            // 
            this.tbReportName.Location = new System.Drawing.Point(98, 111);
            this.tbReportName.Name = "tbReportName";
            this.tbReportName.Size = new System.Drawing.Size(394, 21);
            this.tbReportName.TabIndex = 4;
            this.tbReportName.TextChanged += new System.EventHandler(this.tbReportName_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "作    者：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "说    明：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "报表名称：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbChart);
            this.groupBox1.Controls.Add(this.rbMatrix);
            this.groupBox1.Controls.Add(this.rbList);
            this.groupBox1.Controls.Add(this.rbTable);
            this.groupBox1.Location = new System.Drawing.Point(31, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(461, 79);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "报表类型";
            // 
            // rbChart
            // 
            this.rbChart.AutoSize = true;
            this.rbChart.Location = new System.Drawing.Point(181, 49);
            this.rbChart.Name = "rbChart";
            this.rbChart.Size = new System.Drawing.Size(47, 16);
            this.rbChart.TabIndex = 3;
            this.rbChart.Text = "图表";
            this.rbChart.CheckedChanged += new System.EventHandler(this.rbChart_CheckedChanged);
            // 
            // rbMatrix
            // 
            this.rbMatrix.AutoSize = true;
            this.rbMatrix.Location = new System.Drawing.Point(181, 19);
            this.rbMatrix.Name = "rbMatrix";
            this.rbMatrix.Size = new System.Drawing.Size(47, 16);
            this.rbMatrix.TabIndex = 2;
            this.rbMatrix.Text = "矩阵";
            this.rbMatrix.CheckedChanged += new System.EventHandler(this.rbMatrix_CheckedChanged);
            // 
            // rbList
            // 
            this.rbList.AutoSize = true;
            this.rbList.Location = new System.Drawing.Point(17, 49);
            this.rbList.Name = "rbList";
            this.rbList.Size = new System.Drawing.Size(47, 16);
            this.rbList.TabIndex = 1;
            this.rbList.Text = "列表";
            this.rbList.CheckedChanged += new System.EventHandler(this.rbList_CheckedChanged);
            // 
            // rbTable
            // 
            this.rbTable.AutoSize = true;
            this.rbTable.Checked = true;
            this.rbTable.Location = new System.Drawing.Point(17, 19);
            this.rbTable.Name = "rbTable";
            this.rbTable.Size = new System.Drawing.Size(47, 16);
            this.rbTable.TabIndex = 0;
            this.rbTable.TabStop = true;
            this.rbTable.Text = "表格";
            this.rbTable.CheckedChanged += new System.EventHandler(this.rbTable_CheckedChanged);
            // 
            // DBConnection
            // 
            this.DBConnection.CausesValidation = false;
            this.DBConnection.Controls.Add(this.tbConnection);
            this.DBConnection.Controls.Add(this.btnSetConnect);
            this.DBConnection.Controls.Add(this.bTestConnection);
            this.DBConnection.Controls.Add(this.lConnection);
            this.DBConnection.Controls.Add(this.cbConnectionTypes);
            this.DBConnection.Controls.Add(this.label7);
            this.DBConnection.Location = new System.Drawing.Point(4, 21);
            this.DBConnection.Name = "DBConnection";
            this.DBConnection.Size = new System.Drawing.Size(531, 330);
            this.DBConnection.TabIndex = 0;
            this.DBConnection.Tag = "connect";
            this.DBConnection.Text = "数据连接";
            this.DBConnection.UseVisualStyleBackColor = true;
            // 
            // tbConnection
            // 
            this.tbConnection.Location = new System.Drawing.Point(84, 61);
            this.tbConnection.MaxLength = 512;
            this.tbConnection.Multiline = true;
            this.tbConnection.Name = "tbConnection";
            this.tbConnection.Size = new System.Drawing.Size(412, 97);
            this.tbConnection.TabIndex = 2;
            this.tbConnection.Text = "Server=(local)\\VSDotNet;DataBase=Northwind;Integrated Security=SSPI;Connect Timeo" +
                "ut=5";
            // 
            // btnSetConnect
            // 
            this.btnSetConnect.Location = new System.Drawing.Point(396, 25);
            this.btnSetConnect.Name = "btnSetConnect";
            this.btnSetConnect.Size = new System.Drawing.Size(100, 24);
            this.btnSetConnect.TabIndex = 5;
            this.btnSetConnect.Text = "设置连接";
            this.btnSetConnect.Click += new System.EventHandler(this.btnSetConnect_Click);
            // 
            // bTestConnection
            // 
            this.bTestConnection.Location = new System.Drawing.Point(16, 185);
            this.bTestConnection.Name = "bTestConnection";
            this.bTestConnection.Size = new System.Drawing.Size(100, 24);
            this.bTestConnection.TabIndex = 3;
            this.bTestConnection.Text = "测试连接";
            this.bTestConnection.Click += new System.EventHandler(this.bTestConnection_Click);
            // 
            // lConnection
            // 
            this.lConnection.AutoSize = true;
            this.lConnection.Location = new System.Drawing.Point(14, 103);
            this.lConnection.Name = "lConnection";
            this.lConnection.Size = new System.Drawing.Size(71, 12);
            this.lConnection.TabIndex = 4;
            this.lConnection.Text = "连接字符串:";
            // 
            // cbConnectionTypes
            // 
            this.cbConnectionTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbConnectionTypes.Items.AddRange(new object[] {
            "MSSqlServer",
            "OleDBSupported",
            "Oracle",
            "ODBC"});
            this.cbConnectionTypes.Location = new System.Drawing.Point(84, 27);
            this.cbConnectionTypes.Name = "cbConnectionTypes";
            this.cbConnectionTypes.Size = new System.Drawing.Size(291, 20);
            this.cbConnectionTypes.TabIndex = 0;
            this.cbConnectionTypes.SelectedIndexChanged += new System.EventHandler(this.cbConnectionTypes_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "连接类型:";
            // 
            // ReportParameters
            // 
            this.ReportParameters.Controls.Add(this.bValidValues);
            this.ReportParameters.Controls.Add(this.tbParmDefaultValue);
            this.ReportParameters.Controls.Add(this.lDefaultValue);
            this.ReportParameters.Controls.Add(this.bParmDown);
            this.ReportParameters.Controls.Add(this.bParmUp);
            this.ReportParameters.Controls.Add(this.tbParmValidValues);
            this.ReportParameters.Controls.Add(this.lbParmValidValues);
            this.ReportParameters.Controls.Add(this.ckbParmAllowBlank);
            this.ReportParameters.Controls.Add(this.ckbParmAllowNull);
            this.ReportParameters.Controls.Add(this.tbParmPrompt);
            this.ReportParameters.Controls.Add(this.lParmPrompt);
            this.ReportParameters.Controls.Add(this.cbParmType);
            this.ReportParameters.Controls.Add(this.lParmType);
            this.ReportParameters.Controls.Add(this.tbParmName);
            this.ReportParameters.Controls.Add(this.lParmName);
            this.ReportParameters.Controls.Add(this.bRemove);
            this.ReportParameters.Controls.Add(this.bAdd);
            this.ReportParameters.Controls.Add(this.lbParameters);
            this.ReportParameters.Location = new System.Drawing.Point(4, 21);
            this.ReportParameters.Name = "ReportParameters";
            this.ReportParameters.Size = new System.Drawing.Size(531, 330);
            this.ReportParameters.TabIndex = 6;
            this.ReportParameters.Tag = "parameters";
            this.ReportParameters.Text = "报表参数";
            this.ReportParameters.UseVisualStyleBackColor = true;
            // 
            // bValidValues
            // 
            this.bValidValues.Location = new System.Drawing.Point(489, 164);
            this.bValidValues.Name = "bValidValues";
            this.bValidValues.Size = new System.Drawing.Size(29, 24);
            this.bValidValues.TabIndex = 16;
            this.bValidValues.Text = "...";
            this.bValidValues.Click += new System.EventHandler(this.bValidValues_Click);
            // 
            // tbParmDefaultValue
            // 
            this.tbParmDefaultValue.Location = new System.Drawing.Point(250, 215);
            this.tbParmDefaultValue.Name = "tbParmDefaultValue";
            this.tbParmDefaultValue.Size = new System.Drawing.Size(268, 21);
            this.tbParmDefaultValue.TabIndex = 10;
            this.tbParmDefaultValue.TextChanged += new System.EventHandler(this.tbParmDefaultValue_TextChanged);
            // 
            // lDefaultValue
            // 
            this.lDefaultValue.AutoSize = true;
            this.lDefaultValue.Location = new System.Drawing.Point(250, 198);
            this.lDefaultValue.Name = "lDefaultValue";
            this.lDefaultValue.Size = new System.Drawing.Size(53, 12);
            this.lDefaultValue.TabIndex = 15;
            this.lDefaultValue.Text = "默认值：";
            this.lDefaultValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TabularGroup
            // 
            this.TabularGroup.Controls.Add(this.clbSubtotal);
            this.TabularGroup.Controls.Add(this.ckbGrandTotal);
            this.TabularGroup.Controls.Add(this.label5);
            this.TabularGroup.Controls.Add(this.label4);
            this.TabularGroup.Controls.Add(this.cbColumnList);
            this.TabularGroup.Location = new System.Drawing.Point(4, 21);
            this.TabularGroup.Name = "TabularGroup";
            this.TabularGroup.Size = new System.Drawing.Size(531, 330);
            this.TabularGroup.TabIndex = 7;
            this.TabularGroup.Tag = "group";
            this.TabularGroup.Text = "分组";
            this.TabularGroup.UseVisualStyleBackColor = true;
            // 
            // clbSubtotal
            // 
            this.clbSubtotal.CheckOnClick = true;
            this.clbSubtotal.Location = new System.Drawing.Point(278, 34);
            this.clbSubtotal.Name = "clbSubtotal";
            this.clbSubtotal.Size = new System.Drawing.Size(231, 132);
            this.clbSubtotal.TabIndex = 5;
            this.clbSubtotal.SelectedIndexChanged += new System.EventHandler(this.emptyReportSyntax);
            // 
            // ckbGrandTotal
            // 
            this.ckbGrandTotal.Location = new System.Drawing.Point(19, 95);
            this.ckbGrandTotal.Name = "ckbGrandTotal";
            this.ckbGrandTotal.Size = new System.Drawing.Size(192, 26);
            this.ckbGrandTotal.TabIndex = 4;
            this.ckbGrandTotal.Text = "Calculate grand totals";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(278, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(209, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "Check columns you want to subtotal";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(251, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "Pick a column to group (create hierarchy)";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(376, 10);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定(&O)";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.CausesValidation = false;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(470, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消(&C)";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 373);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(563, 43);
            this.panel1.TabIndex = 5;
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Location = new System.Drawing.Point(282, 10);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 25);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "下一步(&N)";
            // 
            // tbAssembly
            // 
            this.tbAssembly.Location = new System.Drawing.Point(61, 18);
            this.tbAssembly.Name = "tbAssembly";
            this.tbAssembly.ReadOnly = true;
            this.tbAssembly.Size = new System.Drawing.Size(376, 21);
            this.tbAssembly.TabIndex = 6;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 5;
            this.label13.Text = "程序集：";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnBrowser
            // 
            this.btnBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowser.Location = new System.Drawing.Point(443, 16);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(75, 25);
            this.btnBrowser.TabIndex = 7;
            this.btnBrowser.Text = "浏览(&B)";
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 59);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 8;
            this.label14.Text = "对  象：";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lvData
            // 
            this.lvData.Location = new System.Drawing.Point(14, 100);
            this.lvData.Name = "lvData";
            this.lvData.Size = new System.Drawing.Size(504, 216);
            this.lvData.TabIndex = 10;
            this.lvData.UseCompatibleStateImageBehavior = false;
            this.lvData.View = System.Windows.Forms.View.Details;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "TreeLayout.ico");
            this.imageList1.Images.SetKeyName(1, "doc excel.ico");
            // 
            // cbTable
            // 
            this.cbTable.AutoSize = true;
            this.cbTable.Checked = true;
            this.cbTable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTable.Location = new System.Drawing.Point(446, 59);
            this.cbTable.Name = "cbTable";
            this.cbTable.Size = new System.Drawing.Size(72, 16);
            this.cbTable.TabIndex = 11;
            this.cbTable.Text = "过虑实体";
            this.cbTable.UseVisualStyleBackColor = true;
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "Leaf.ICO");
            this.imageList2.Images.SetKeyName(1, "LeafH.ICO");
            this.imageList2.Images.SetKeyName(2, "TreeLayout.ico");
            this.imageList2.Images.SetKeyName(3, "directx.ico");
            this.imageList2.Images.SetKeyName(4, "ComPlusSvc.ico");
            this.imageList2.Images.SetKeyName(5, "doc excel.ico");
            this.imageList2.Images.SetKeyName(6, "dialog.ico");
            this.imageList2.Images.SetKeyName(7, "doc pdf.ico");
            this.imageList2.Images.SetKeyName(8, "doc word.ico");
            this.imageList2.Images.SetKeyName(9, "download and open.ico");
            this.imageList2.Images.SetKeyName(10, "decimals decrease.ico");
            this.imageList2.Images.SetKeyName(11, "decimals increase.ico");
            this.imageList2.Images.SetKeyName(12, "design.ico");
            this.imageList2.Images.SetKeyName(13, "4561.ico");
            // 
            // cbxTypes
            // 
            this.cbxTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTypes.FormattingEnabled = true;
            this.cbxTypes.Location = new System.Drawing.Point(61, 55);
            this.cbxTypes.Name = "cbxTypes";
            this.cbxTypes.Size = new System.Drawing.Size(376, 20);
            this.cbxTypes.TabIndex = 12;
            // 
            // ofd
            // 
            this.ofd.Filter = "程序集文件|*.dll";
            // 
            // DialogDataObject
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(563, 416);
            this.Controls.Add(this.tcDialog);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogDataObject";
            this.Text = "新建数据对象报表";
            this.DataObject.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ReportPreview.ResumeLayout(false);
            this.ReportSyntax.ResumeLayout(false);
            this.ReportSyntax.PerformLayout();
            this.tcDialog.ResumeLayout(false);
            this.ReportType.ResumeLayout(false);
            this.ReportType.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.DBConnection.ResumeLayout(false);
            this.DBConnection.PerformLayout();
            this.ReportParameters.ResumeLayout(false);
            this.ReportParameters.PerformLayout();
            this.TabularGroup.ResumeLayout(false);
            this.TabularGroup.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage DataObject;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button bParmDown;
        private System.Windows.Forms.Button bParmUp;
        private System.Windows.Forms.TextBox tbParmValidValues;
        private System.Windows.Forms.Label lbParmValidValues;
        private System.Windows.Forms.CheckBox ckbParmAllowBlank;
        private System.Windows.Forms.CheckBox ckbParmAllowNull;
        private System.Windows.Forms.TextBox tbParmPrompt;
        private System.Windows.Forms.Label lParmPrompt;
        private System.Windows.Forms.ComboBox cbParmType;
        private System.Windows.Forms.Label lParmType;
        private System.Windows.Forms.TextBox tbParmName;
        private System.Windows.Forms.Label lParmName;
        private System.Windows.Forms.Button bRemove;
        private System.Windows.Forms.Button bAdd;
        private System.Windows.Forms.ListBox lbParameters;
        private System.Windows.Forms.TextBox tbReportSyntax;
        private System.Windows.Forms.TabPage ReportPreview;
        private fyiReporting.RdlViewer.RdlViewer rdlViewer1;
        private System.Windows.Forms.ComboBox cbColumnList;
        private System.Windows.Forms.TabPage ReportSyntax;
        private System.Windows.Forms.TabControl tcDialog;
        private System.Windows.Forms.TabPage ReportType;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbSchema2005;
        private System.Windows.Forms.RadioButton rbSchema2003;
        private System.Windows.Forms.RadioButton rbSchemaNo;
        private System.Windows.Forms.ComboBox cbOrientation;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbReportAuthor;
        private System.Windows.Forms.TextBox tbReportDescription;
        private System.Windows.Forms.TextBox tbReportName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbChart;
        private System.Windows.Forms.RadioButton rbMatrix;
        private System.Windows.Forms.RadioButton rbList;
        private System.Windows.Forms.RadioButton rbTable;
        private System.Windows.Forms.TabPage DBConnection;
        private System.Windows.Forms.Button bTestConnection;
        private System.Windows.Forms.Label lConnection;
        private System.Windows.Forms.ComboBox cbConnectionTypes;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbConnection;
        private System.Windows.Forms.TabPage ReportParameters;
        private System.Windows.Forms.Button bValidValues;
        private System.Windows.Forms.TextBox tbParmDefaultValue;
        private System.Windows.Forms.Label lDefaultValue;
        private System.Windows.Forms.TabPage TabularGroup;
        private System.Windows.Forms.CheckedListBox clbSubtotal;
        private System.Windows.Forms.CheckBox ckbGrandTotal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSetConnect;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.ComboBox cbxPages;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbHeight;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbWidth;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbAssembly;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnBrowser;
        private System.Windows.Forms.ListView lvData;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.CheckBox cbTable;
        private System.Windows.Forms.ComboBox cbxTypes;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.OpenFileDialog ofd;
    }
}