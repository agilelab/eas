namespace EAS.Data.Design
{
    partial class MappingsEditorForm
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
            this.cbControl = new System.Windows.Forms.ComboBox();
            this.tpHelp = new System.Windows.Forms.ToolTip(this.components);
            this.cbModelProperty = new System.Windows.Forms.ComboBox();
            this.cbControlProperty = new System.Windows.Forms.ComboBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cbFormat = new System.Windows.Forms.ComboBox();
            this.rbSelect = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.lstMappings = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.pnlGeneral = new System.Windows.Forms.Panel();
            this.tbModelProperty = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lnStatus = new System.Windows.Forms.GroupBox();
            this.stStatus = new System.Windows.Forms.StatusBar();
            this.pnlGeneral.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbControl
            // 
            this.cbControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbControl.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbControl.Location = new System.Drawing.Point(8, 30);
            this.cbControl.Name = "cbControl";
            this.cbControl.Size = new System.Drawing.Size(245, 22);
            this.cbControl.TabIndex = 1;
            this.tpHelp.SetToolTip(this.cbControl, "需要进行数据绑定的控件");
            this.cbControl.SelectedIndexChanged += new System.EventHandler(this.cbControl_SelectedIndexChanged);
            // 
            // cbModelProperty
            // 
            this.cbModelProperty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbModelProperty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModelProperty.Location = new System.Drawing.Point(8, 136);
            this.cbModelProperty.Name = "cbModelProperty";
            this.cbModelProperty.Size = new System.Drawing.Size(153, 20);
            this.cbModelProperty.TabIndex = 5;
            this.tpHelp.SetToolTip(this.cbModelProperty, "执行数据绑带的实体属性");
            this.cbModelProperty.SelectedIndexChanged += new System.EventHandler(this.cbModelProperty_SelectedIndexChanged);
            // 
            // cbControlProperty
            // 
            this.cbControlProperty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbControlProperty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbControlProperty.Location = new System.Drawing.Point(8, 82);
            this.cbControlProperty.Name = "cbControlProperty";
            this.cbControlProperty.Size = new System.Drawing.Size(153, 20);
            this.cbControlProperty.TabIndex = 3;
            this.tpHelp.SetToolTip(this.cbControlProperty, "执行数据绑带的控件属性");
            this.cbControlProperty.SelectedIndexChanged += new System.EventHandler(this.cbControlProperty_SelectedIndexChanged);
            this.cbControlProperty.DropDown += new System.EventHandler(this.cbControlProperty_DropDown);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemove.Location = new System.Drawing.Point(132, 268);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(95, 24);
            this.btnRemove.TabIndex = 8;
            this.btnRemove.Text = "移除";
            this.tpHelp.SetToolTip(this.btnRemove, "移除绑定记录");
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Location = new System.Drawing.Point(8, 268);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(95, 24);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "添加";
            this.tpHelp.SetToolTip(this.btnAdd, "添加绑定信息");
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // cbFormat
            // 
            this.cbFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFormat.Items.AddRange(new object[] {
            "None",
            "Date",
            "Time",
            "DateAndTime",
            "F2",
            "F4",
            "F6",
            "MF2",
            "MF4",
            "MF6"});
            this.cbFormat.Location = new System.Drawing.Point(8, 191);
            this.cbFormat.Name = "cbFormat";
            this.cbFormat.Size = new System.Drawing.Size(153, 20);
            this.cbFormat.TabIndex = 7;
            this.tpHelp.SetToolTip(this.cbFormat, "数据的显示格式，默认None");
            this.cbFormat.SelectedIndexChanged += new System.EventHandler(this.cbFormat_SelectedIndexChanged);
            // 
            // rbSelect
            // 
            this.rbSelect.AutoSize = true;
            this.rbSelect.Checked = true;
            this.rbSelect.Location = new System.Drawing.Point(10, 7);
            this.rbSelect.Name = "rbSelect";
            this.rbSelect.Size = new System.Drawing.Size(47, 16);
            this.rbSelect.TabIndex = 1;
            this.rbSelect.TabStop = true;
            this.rbSelect.Text = "常用";
            this.tpHelp.SetToolTip(this.rbSelect, "选择加载常用的控件与属性");
            this.rbSelect.UseVisualStyleBackColor = true;
            this.rbSelect.CheckedChanged += new System.EventHandler(this.rbSelect_CheckedChanged);
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Location = new System.Drawing.Point(72, 7);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(47, 16);
            this.rbAll.TabIndex = 2;
            this.rbAll.Text = "全部";
            this.tpHelp.SetToolTip(this.rbAll, "加载所有的控件与属性");
            this.rbAll.UseVisualStyleBackColor = true;
            this.rbAll.CheckedChanged += new System.EventHandler(this.rbAll_CheckedChanged);
            // 
            // lstMappings
            // 
            this.lstMappings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstMappings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.lstMappings.FullRowSelect = true;
            this.lstMappings.Location = new System.Drawing.Point(8, 36);
            this.lstMappings.MultiSelect = false;
            this.lstMappings.Name = "lstMappings";
            this.lstMappings.Size = new System.Drawing.Size(219, 220);
            this.lstMappings.SmallImageList = this.imageList;
            this.lstMappings.TabIndex = 3;
            this.tpHelp.SetToolTip(this.lstMappings, "映射集合，选中编辑");
            this.lstMappings.UseCompatibleStateImageBehavior = false;
            this.lstMappings.View = System.Windows.Forms.View.Details;
            this.lstMappings.SelectedIndexChanged += new System.EventHandler(this.lstMappings_SelectedIndexChanged);
            this.lstMappings.Enter += new System.EventHandler(this.lstMappings_Enter);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "映射";
            this.columnHeader2.Width = 280;
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList.ImageSize = new System.Drawing.Size(5, 18);
            this.imageList.TransparentColor = System.Drawing.Color.Lime;
            // 
            // pnlGeneral
            // 
            this.pnlGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlGeneral.Controls.Add(this.tbModelProperty);
            this.pnlGeneral.Controls.Add(this.cbFormat);
            this.pnlGeneral.Controls.Add(this.label4);
            this.pnlGeneral.Controls.Add(this.cbControl);
            this.pnlGeneral.Controls.Add(this.label12);
            this.pnlGeneral.Controls.Add(this.cbModelProperty);
            this.pnlGeneral.Controls.Add(this.label6);
            this.pnlGeneral.Controls.Add(this.cbControlProperty);
            this.pnlGeneral.Controls.Add(this.label3);
            this.pnlGeneral.Location = new System.Drawing.Point(233, 36);
            this.pnlGeneral.Name = "pnlGeneral";
            this.pnlGeneral.Size = new System.Drawing.Size(263, 220);
            this.pnlGeneral.TabIndex = 6;
            // 
            // tbModelProperty
            // 
            this.tbModelProperty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbModelProperty.BackColor = System.Drawing.SystemColors.Window;
            this.tbModelProperty.Location = new System.Drawing.Point(8, 136);
            this.tbModelProperty.MaxLength = 32;
            this.tbModelProperty.Name = "tbModelProperty";
            this.tbModelProperty.Size = new System.Drawing.Size(153, 21);
            this.tbModelProperty.TabIndex = 4;
            this.tbModelProperty.TextChanged += new System.EventHandler(this.tbModelProperty_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "格式:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 10);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 12);
            this.label12.TabIndex = 0;
            this.label12.Text = "控件:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 116);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "数据属性:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "控件属性:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(241, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "映射:";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Location = new System.Drawing.Point(260, 16);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(237, 8);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCancel.Location = new System.Drawing.Point(425, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(60, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(170, 8);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOK.Location = new System.Drawing.Point(345, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(8, 300);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(493, 4);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "集合:";
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.rbAll);
            this.pnlButtons.Controls.Add(this.rbSelect);
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Controls.Add(this.btnOK);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 334);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(509, 32);
            this.pnlButtons.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Location = new System.Drawing.Point(8, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(489, 4);
            this.panel1.TabIndex = 0;
            // 
            // lnStatus
            // 
            this.lnStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lnStatus.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lnStatus.Location = new System.Drawing.Point(0, 366);
            this.lnStatus.Name = "lnStatus";
            this.lnStatus.Size = new System.Drawing.Size(509, 4);
            this.lnStatus.TabIndex = 12;
            this.lnStatus.TabStop = false;
            // 
            // stStatus
            // 
            this.stStatus.Location = new System.Drawing.Point(0, 370);
            this.stStatus.Name = "stStatus";
            this.stStatus.Size = new System.Drawing.Size(509, 22);
            this.stStatus.TabIndex = 11;
            this.stStatus.Text = "就绪";
            // 
            // MappingsEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(509, 392);
            this.Controls.Add(this.pnlGeneral);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lstMappings);
            this.Controls.Add(this.lnStatus);
            this.Controls.Add(this.stStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MappingsEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查看/编辑映射关系";
            this.pnlGeneral.ResumeLayout(false);
            this.pnlGeneral.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.pnlButtons.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ComboBox cbControl;
        private System.Windows.Forms.ToolTip tpHelp;
        private System.Windows.Forms.ComboBox cbModelProperty;
        private System.Windows.Forms.ComboBox cbControlProperty;
        internal System.Windows.Forms.Button btnRemove;
        internal System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel pnlGeneral;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox lnStatus;
        private System.Windows.Forms.ComboBox cbFormat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbSelect;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        internal System.Windows.Forms.ListView lstMappings;
        private System.Windows.Forms.TextBox tbModelProperty;
        private System.Windows.Forms.StatusBar stStatus;

    }
}