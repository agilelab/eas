namespace WF.Demo.UI 
{ 
  partial class LeaveList 
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
    
    #region 组件设计器生成的代码 
    
    /// <summary>  
    /// 设计器支持所需的方法 - 不要 
    /// 使用代码编辑器修改此方法的内容。 
    /// </summary> 
    private void InitializeComponent() 
    { 
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.columnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnDays = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columniState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCause = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.dataPager = new EAS.Windows.UI.Controls.DataPager();
            this.panDataPager = new System.Windows.Forms.Panel();
            this.lbName = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.datasourcedataGridView1 = new System.Windows.Forms.BindingSource(this.components);
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.Panel2.SuspendLayout();
            this.panDataPager.SuspendLayout();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datasourcedataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnName,
            this.columnDays,
            this.columniState,
            this.columnCause,
            this.columnID});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(784, 289);
            this.dataGridView1.TabIndex = 0;
            // 
            // columnName
            // 
            this.columnName.DataPropertyName = "Name";
            this.columnName.HeaderText = "姓名";
            this.columnName.Name = "columnName";
            this.columnName.ReadOnly = true;
            // 
            // columnDays
            // 
            this.columnDays.DataPropertyName = "Days";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Format = "N0";
            dataGridViewCellStyle5.NullValue = null;
            this.columnDays.DefaultCellStyle = dataGridViewCellStyle5;
            this.columnDays.HeaderText = "请假天数";
            this.columnDays.Name = "columnDays";
            this.columnDays.ReadOnly = true;
            this.columnDays.Width = 80;
            // 
            // columniState
            // 
            this.columniState.DataPropertyName = "iStateText";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.columniState.DefaultCellStyle = dataGridViewCellStyle6;
            this.columniState.HeaderText = "状态";
            this.columniState.Name = "columniState";
            this.columniState.ReadOnly = true;
            this.columniState.Width = 75;
            // 
            // columnCause
            // 
            this.columnCause.DataPropertyName = "Cause";
            this.columnCause.HeaderText = "原因";
            this.columnCause.Name = "columnCause";
            this.columnCause.ReadOnly = true;
            this.columnCause.Width = 180;
            // 
            // columnID
            // 
            this.columnID.DataPropertyName = "ID";
            this.columnID.HeaderText = "ID";
            this.columnID.Name = "columnID";
            this.columnID.ReadOnly = true;
            this.columnID.Width = 240;
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.dataGridView1);
            this.Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel2.Location = new System.Drawing.Point(0, 48);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(784, 289);
            this.Panel2.TabIndex = 0;
            // 
            // dataPager
            // 
            this.dataPager.CurrentPage = 1;
            this.dataPager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataPager.Location = new System.Drawing.Point(0, 0);
            this.dataPager.Name = "dataPager";
            this.dataPager.RecordCount = 0;
            this.dataPager.Size = new System.Drawing.Size(784, 25);
            this.dataPager.TabIndex = 0;
            this.dataPager.PageChanged += new System.EventHandler(this.dataPager_PageChanged);
            // 
            // panDataPager
            // 
            this.panDataPager.Controls.Add(this.dataPager);
            this.panDataPager.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panDataPager.Location = new System.Drawing.Point(0, 337);
            this.panDataPager.Name = "panDataPager";
            this.panDataPager.Size = new System.Drawing.Size(784, 25);
            this.panDataPager.TabIndex = 1;
            // 
            // lbName
            // 
            this.lbName.Location = new System.Drawing.Point(15, 14);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(80, 20);
            this.lbName.TabIndex = 0;
            this.lbName.Text = "姓名：";
            this.lbName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(100, 14);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(200, 21);
            this.tbName.TabIndex = 1;
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.btnClose);
            this.Panel1.Controls.Add(this.btnQuery);
            this.Panel1.Controls.Add(this.lbName);
            this.Panel1.Controls.Add(this.tbName);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(784, 48);
            this.Panel1.TabIndex = 2;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(321, 13);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 2;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(430, 13);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // LeaveList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.panDataPager);
            this.Controls.Add(this.Panel1);
            this.Name = "LeaveList";
            this.Size = new System.Drawing.Size(784, 362);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.Panel2.ResumeLayout(false);
            this.panDataPager.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datasourcedataGridView1)).EndInit();
            this.ResumeLayout(false);

    } 
    
    #endregion 
 
    private System.Windows.Forms.DataGridView dataGridView1; 
    private System.Windows.Forms.BindingSource datasourcedataGridView1; 
    private System.Windows.Forms.Panel Panel2; 
    private EAS.Windows.UI.Controls.DataPager dataPager; 
    private System.Windows.Forms.Panel panDataPager; 
    private System.Windows.Forms.Label lbName; 
    private System.Windows.Forms.TextBox tbName;
    private System.Windows.Forms.Panel Panel1;
    private System.Windows.Forms.DataGridViewTextBoxColumn columnName;
    private System.Windows.Forms.DataGridViewTextBoxColumn columnDays;
    private System.Windows.Forms.DataGridViewTextBoxColumn columniState;
    private System.Windows.Forms.DataGridViewTextBoxColumn columnCause;
    private System.Windows.Forms.DataGridViewTextBoxColumn columnID;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.Button btnQuery; 
  } 
}
