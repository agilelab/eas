using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;
using System.Xml;
using fyiReporting.RDL;


namespace fyiReporting.RdlDesign
{
	/// <summary>
	/// Summary description for DialogDataSourceRef.
	/// </summary>
	class DialogNewChart : System.Windows.Forms.Form
	{
		private DesignXmlDraw _Draw;
		private System.Windows.Forms.Button bOK;
		private System.Windows.Forms.Button bCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cbDataSets;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListBox lbFields;
		private System.Windows.Forms.ListBox lbChartCategories;
		private System.Windows.Forms.Button bCategoryUp;
		private System.Windows.Forms.Button bCategoryDown;
		private System.Windows.Forms.Button bCategory;
		private System.Windows.Forms.Button bSeries;
		private System.Windows.Forms.ListBox lbChartSeries;
		private System.Windows.Forms.Button bCategoryDelete;
		private System.Windows.Forms.Button bSeriesDelete;
		private System.Windows.Forms.Button bSeriesDown;
		private System.Windows.Forms.Button bSeriesUp;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label lChartData;
		private System.Windows.Forms.ComboBox cbChartData;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox cbSubType;
		private System.Windows.Forms.ComboBox cbChartType;
		private System.Windows.Forms.Label label7;
        private ComboBox cbChartData2;
        private Label lChartData2;
        private ComboBox cbChartData3;
        private Label lChartData3;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		internal DialogNewChart(DesignXmlDraw dxDraw, XmlNode container)
		{
			_Draw = dxDraw;
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			InitValues(container);
		}

		private void InitValues(XmlNode container)
		{
			this.bOK.Enabled = false;		
			//
			// Obtain the existing DataSets info
			//
			object[] datasets = _Draw.DataSetNames;
			if (datasets == null)
				return;		// not much to do if no DataSets

			if (_Draw.IsDataRegion(container))
			{
				string s = _Draw.GetDataSetNameValue(container);
				if (s == null)
					return;
				this.cbDataSets.Items.Add(s);
				this.cbDataSets.Enabled = false;
			}
			else
				this.cbDataSets.Items.AddRange(datasets);
			cbDataSets.SelectedIndex = 0;

			this.cbChartType.SelectedIndex = 2;
		}

		internal string ChartXml
		{
			get 
			{
				StringBuilder chart = new StringBuilder("<Chart><Height>2in</Height><Width>4in</Width>");
				chart.AppendFormat("<DataSetName>{0}</DataSetName>", this.cbDataSets.Text);
				chart.Append("<NoRows>Query returned no rows!</NoRows><Style>"+
					"<BorderStyle><Default>Solid</Default></BorderStyle>"+
					"<BackgroundColor>White</BackgroundColor>"+
					"<BackgroundGradientType>LeftRight</BackgroundGradientType>"+
					"<BackgroundGradientEndColor>Azure</BackgroundGradientEndColor>"+
					"</Style>");
				chart.AppendFormat("<Type>{0}</Type><Subtype>{1}</Subtype>",
					this.cbChartType.Text, this.cbSubType.Text);
				// do the categories
				string tcat="";
				if (this.lbChartCategories.Items.Count > 0)
				{
					chart.Append("<CategoryGroupings>");
					foreach (string cname in this.lbChartCategories.Items)
					{
						if (tcat == "")
							tcat = cname;
						chart.Append("<CategoryGrouping>");
						chart.Append("<DynamicCategories>");
						chart.AppendFormat("<Grouping><GroupExpressions>"+
							"<GroupExpression>=Fields!{0}.Value</GroupExpression>"+
							"</GroupExpressions></Grouping>", cname);

						chart.Append("</DynamicCategories>");
						chart.Append("</CategoryGrouping>");
					}
					chart.Append("</CategoryGroupings>");
					// Do the category axis
					chart.AppendFormat("<CategoryAxis><Axis><Visible>true</Visible>"+
						"<MajorTickMarks>Inside</MajorTickMarks>"+
						"<MajorGridLines><ShowGridLines>true</ShowGridLines>"+
						"<Style><BorderStyle><Default>Solid</Default></BorderStyle>"+
						"</Style></MajorGridLines>" +
						"<MinorGridLines><ShowGridLines>true</ShowGridLines>"+
						"<Style><BorderStyle><Default>Solid</Default></BorderStyle>"+
						"</Style></MinorGridLines>"+
			            "<Title><Caption>{0}</Caption>"+
						"</Title></Axis></CategoryAxis>",tcat);

				}
				// do the series
				string	tser="";
				if (this.lbChartSeries.Items.Count > 0)
				{
					chart.Append("<SeriesGroupings>");
					//If we have chartData Set then we want dynamic series GJL
                    if (this.cbChartData.Text.Length > 0)
                    {
                        foreach (string sname in this.lbChartSeries.Items)
                        {
                            if (tser == "")
                                tser = sname;
                            chart.Append("<SeriesGrouping>");
                            chart.Append("<DynamicSeries>");
                            chart.AppendFormat("<Grouping><GroupExpressions>" +
                                "<GroupExpression>=Fields!{0}.Value</GroupExpression>" +
                                "</GroupExpressions></Grouping>", sname);
                            chart.AppendFormat("<Label>=Fields!{0}.Value</Label>", sname);
                            chart.Append("</DynamicSeries>");
                            chart.Append("</SeriesGrouping>");
                        }
                    }
                    //If we don't have chart data set we want static series GJL
                    else
                    {
                        chart.Append("<SeriesGrouping>");
                        chart.Append("<StaticSeries>");
                        foreach (string sname in this.lbChartSeries.Items)
                        {
                            chart.Append("<StaticMember>");
                            chart.AppendFormat("<Label>{0}</Label>", sname);
                            chart.AppendFormat("<Value>=Fields!{0}.Value</Value>",sname);
                            chart.Append("</StaticMember>");

                        }
                        
                        chart.Append("</StaticSeries>");
                        chart.Append("</SeriesGrouping>");
                        
                    }
					chart.Append("</SeriesGroupings>");
				}
				// Chart Data
                string vtitle; 
				if (this.cbChartData.Text.Length > 0)
				{
                    chart.Append("<ChartData><ChartSeries><DataPoints><DataPoint>" +
                        "<DataValues>");
                    chart.AppendFormat("<DataValue><Value>{0}</Value></DataValue>",
                        this.cbChartData.Text);
                    string ctype = this.cbChartType.Text.ToLowerInvariant();

                    if (ctype == "scatter" || ctype == "bubble")
                    {
                        chart.AppendFormat("<DataValue><Value>{0}</Value></DataValue>",
                            this.cbChartData2.Text);
                        if (ctype == "bubble")
                        {
                            chart.AppendFormat("<DataValue><Value>{0}</Value></DataValue>",
                                this.cbChartData3.Text);
                        }
                    }
                    chart.Append("</DataValues>" +
                        "</DataPoint></DataPoints></ChartSeries></ChartData>");

                    // Do the value axis
				
					int start = this.cbChartData.Text.LastIndexOf("!");
					if (start > 0)
					{
						int end = this.cbChartData.Text.LastIndexOf(".Value");
						if (end < 0 || end <= start+1)
							vtitle = this.cbChartData.Text.Substring(start+1);
						else
							vtitle = this.cbChartData.Text.Substring(start+1, end-start-1);
					}
					else 
						vtitle = "Values";					
				}
                else
                {
                	//If we don't have chartData then use the items in the series box
                	//to create Static Series
                    chart.Append("<ChartData>");
                    foreach (string sname in this.lbChartSeries.Items)
                    {
                        chart.Append("<ChartSeries>");
                        chart.Append("<DataPoints>");
                        chart.Append("<DataPoint>");
                        chart.Append("<DataValues>");
                       
                        if (cbChartType.SelectedItem.Equals("Scatter"))
                        {
                            //we need a y datavalue as well...
                            string xname = (string)lbChartCategories.Items[0];
                            chart.Append("<DataValue>");
                            chart.AppendFormat("<Value>=Fields!{0}.Value</Value>", xname);
                            chart.Append("</DataValue>");
                        chart.Append("<DataValue>");                       
                        chart.AppendFormat("<Value>=Fields!{0}.Value</Value>", sname);
                        chart.Append("</DataValue>");
                        }
                        else
                        {
                            chart.Append("<DataValue>");
                            chart.AppendFormat("<Value>=Fields!{0}.Value</Value>", sname);
                            chart.Append("</DataValue>");
                        }
                        chart.Append("</DataValues>");
                        chart.Append("</DataPoint>");
                        chart.Append("</DataPoints>");
                        chart.Append("</ChartSeries>");
                    }
                    chart.Append("</ChartData>");
                    vtitle = "Values";		

                }

                chart.AppendFormat("<ValueAxis><Axis><Visible>true</Visible>" +
                        "<MajorTickMarks>Inside</MajorTickMarks>" +
                        "<MajorGridLines><ShowGridLines>true</ShowGridLines>" +
                        "<Style><BorderStyle><Default>Solid</Default></BorderStyle>" +
                        "<FontSize>8pt</FontSize>" +
                        "</Style></MajorGridLines>" +
                        "<MinorGridLines><ShowGridLines>true</ShowGridLines>" +
                        "<Style><BorderStyle><Default>Solid</Default></BorderStyle>" +
                        "</Style></MinorGridLines>" +
                        "<Title><Caption>{0}</Caption>" +
                        "<Style><WritingMode>tb-rl</WritingMode></Style>" +
                        "</Title></Axis></ValueAxis>", vtitle);

				// Legend
				chart.Append("<Legend><Style><BorderStyle><Default>Solid</Default>"+
					"</BorderStyle><PaddingLeft>5pt</PaddingLeft>"+
					"<FontSize>8pt</FontSize></Style><Visible>true</Visible>"+
					"<Position>RightCenter</Position></Legend>");

				// Title
				chart.AppendFormat("<Title><Style><FontWeight>Bold</FontWeight>"+
					"<FontSize>14pt</FontSize><TextAlign>Center</TextAlign>"+
					"</Style><Caption>{0} {1} Chart</Caption></Title>", tcat, tser);

				// end of Chart defintion
				chart.Append("</Chart>");

				return chart.ToString();
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.bOK = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbDataSets = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbFields = new System.Windows.Forms.ListBox();
            this.lbChartCategories = new System.Windows.Forms.ListBox();
            this.bCategoryUp = new System.Windows.Forms.Button();
            this.bCategoryDown = new System.Windows.Forms.Button();
            this.bCategory = new System.Windows.Forms.Button();
            this.bSeries = new System.Windows.Forms.Button();
            this.lbChartSeries = new System.Windows.Forms.ListBox();
            this.bCategoryDelete = new System.Windows.Forms.Button();
            this.bSeriesDelete = new System.Windows.Forms.Button();
            this.bSeriesDown = new System.Windows.Forms.Button();
            this.bSeriesUp = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lChartData = new System.Windows.Forms.Label();
            this.cbChartData = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbSubType = new System.Windows.Forms.ComboBox();
            this.cbChartType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbChartData2 = new System.Windows.Forms.ComboBox();
            this.lChartData2 = new System.Windows.Forms.Label();
            this.cbChartData3 = new System.Windows.Forms.ComboBox();
            this.lChartData3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(326, 406);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(90, 25);
            this.bOK.TabIndex = 16;
            this.bOK.Text = "确定(&O)";
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bCancel
            // 
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(442, 406);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(90, 25);
            this.bCancel.TabIndex = 17;
            this.bCancel.Text = "取消(&C)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "数 据 集：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbDataSets
            // 
            this.cbDataSets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataSets.Location = new System.Drawing.Point(82, 17);
            this.cbDataSets.Name = "cbDataSets";
            this.cbDataSets.Size = new System.Drawing.Size(446, 20);
            this.cbDataSets.TabIndex = 0;
            this.cbDataSets.SelectedIndexChanged += new System.EventHandler(this.cbDataSets_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "数据字段：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(278, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "图表目录(X)分组：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbFields
            // 
            this.lbFields.ItemHeight = 12;
            this.lbFields.Location = new System.Drawing.Point(19, 102);
            this.lbFields.Name = "lbFields";
            this.lbFields.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbFields.Size = new System.Drawing.Size(183, 196);
            this.lbFields.TabIndex = 3;
            // 
            // lbChartCategories
            // 
            this.lbChartCategories.ItemHeight = 12;
            this.lbChartCategories.Location = new System.Drawing.Point(278, 100);
            this.lbChartCategories.Name = "lbChartCategories";
            this.lbChartCategories.Size = new System.Drawing.Size(183, 88);
            this.lbChartCategories.TabIndex = 4;
            // 
            // bCategoryUp
            // 
            this.bCategoryUp.Location = new System.Drawing.Point(478, 99);
            this.bCategoryUp.Name = "bCategoryUp";
            this.bCategoryUp.Size = new System.Drawing.Size(58, 26);
            this.bCategoryUp.TabIndex = 5;
            this.bCategoryUp.Text = "上移";
            this.bCategoryUp.Click += new System.EventHandler(this.bCategoryUp_Click);
            // 
            // bCategoryDown
            // 
            this.bCategoryDown.Location = new System.Drawing.Point(478, 129);
            this.bCategoryDown.Name = "bCategoryDown";
            this.bCategoryDown.Size = new System.Drawing.Size(58, 27);
            this.bCategoryDown.TabIndex = 6;
            this.bCategoryDown.Text = "下移";
            this.bCategoryDown.Click += new System.EventHandler(this.bCategoryDown_Click);
            // 
            // bCategory
            // 
            this.bCategory.Location = new System.Drawing.Point(210, 129);
            this.bCategory.Name = "bCategory";
            this.bCategory.Size = new System.Drawing.Size(59, 27);
            this.bCategory.TabIndex = 2;
            this.bCategory.Text = ">";
            this.bCategory.Click += new System.EventHandler(this.bCategory_Click);
            // 
            // bSeries
            // 
            this.bSeries.Location = new System.Drawing.Point(211, 246);
            this.bSeries.Name = "bSeries";
            this.bSeries.Size = new System.Drawing.Size(59, 26);
            this.bSeries.TabIndex = 8;
            this.bSeries.Text = ">";
            this.bSeries.Click += new System.EventHandler(this.bSeries_Click);
            // 
            // lbChartSeries
            // 
            this.lbChartSeries.ItemHeight = 12;
            this.lbChartSeries.Location = new System.Drawing.Point(278, 215);
            this.lbChartSeries.Name = "lbChartSeries";
            this.lbChartSeries.Size = new System.Drawing.Size(183, 88);
            this.lbChartSeries.TabIndex = 9;
            // 
            // bCategoryDelete
            // 
            this.bCategoryDelete.Location = new System.Drawing.Point(478, 160);
            this.bCategoryDelete.Name = "bCategoryDelete";
            this.bCategoryDelete.Size = new System.Drawing.Size(58, 26);
            this.bCategoryDelete.TabIndex = 7;
            this.bCategoryDelete.Text = "删除";
            this.bCategoryDelete.Click += new System.EventHandler(this.bCategoryDelete_Click);
            // 
            // bSeriesDelete
            // 
            this.bSeriesDelete.Location = new System.Drawing.Point(478, 278);
            this.bSeriesDelete.Name = "bSeriesDelete";
            this.bSeriesDelete.Size = new System.Drawing.Size(58, 25);
            this.bSeriesDelete.TabIndex = 12;
            this.bSeriesDelete.Text = "删除";
            this.bSeriesDelete.Click += new System.EventHandler(this.bSeriesDelete_Click);
            // 
            // bSeriesDown
            // 
            this.bSeriesDown.Location = new System.Drawing.Point(478, 246);
            this.bSeriesDown.Name = "bSeriesDown";
            this.bSeriesDown.Size = new System.Drawing.Size(58, 27);
            this.bSeriesDown.TabIndex = 11;
            this.bSeriesDown.Text = "下移";
            this.bSeriesDown.Click += new System.EventHandler(this.bSeriesDown_Click);
            // 
            // bSeriesUp
            // 
            this.bSeriesUp.Location = new System.Drawing.Point(478, 215);
            this.bSeriesUp.Name = "bSeriesUp";
            this.bSeriesUp.Size = new System.Drawing.Size(58, 26);
            this.bSeriesUp.TabIndex = 10;
            this.bSeriesUp.Text = "上移";
            this.bSeriesUp.Click += new System.EventHandler(this.bSeriesUp_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(278, 195);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 31;
            this.label4.Text = "图表序列：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lChartData
            // 
            this.lChartData.AutoSize = true;
            this.lChartData.Location = new System.Drawing.Point(19, 324);
            this.lChartData.Name = "lChartData";
            this.lChartData.Size = new System.Drawing.Size(77, 12);
            this.lChartData.TabIndex = 32;
            this.lChartData.Text = "数据表达式：";
            this.lChartData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbChartData
            // 
            this.cbChartData.Location = new System.Drawing.Point(92, 320);
            this.cbChartData.Name = "cbChartData";
            this.cbChartData.Size = new System.Drawing.Size(369, 20);
            this.cbChartData.TabIndex = 13;
            this.cbChartData.Enter += new System.EventHandler(this.cbChartData_Enter);
            this.cbChartData.TextChanged += new System.EventHandler(this.cbChartData_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(287, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 36;
            this.label6.Text = "子类型：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbSubType
            // 
            this.cbSubType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSubType.Location = new System.Drawing.Point(337, 47);
            this.cbSubType.Name = "cbSubType";
            this.cbSubType.Size = new System.Drawing.Size(191, 20);
            this.cbSubType.TabIndex = 2;
            // 
            // cbChartType
            // 
            this.cbChartType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChartType.Items.AddRange(new object[] {
            "Area",
            "Bar",
            "Column",
            "Doughnut",
            "Line",
            "Pie",
            "Bubble",
            "Scatter"});
            this.cbChartType.Location = new System.Drawing.Point(82, 47);
            this.cbChartType.Name = "cbChartType";
            this.cbChartType.Size = new System.Drawing.Size(178, 20);
            this.cbChartType.TabIndex = 1;
            this.cbChartType.SelectedIndexChanged += new System.EventHandler(this.cbChartType_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 33;
            this.label7.Text = "图表类型：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbChartData2
            // 
            this.cbChartData2.Location = new System.Drawing.Point(92, 349);
            this.cbChartData2.Name = "cbChartData2";
            this.cbChartData2.Size = new System.Drawing.Size(369, 20);
            this.cbChartData2.TabIndex = 14;
            this.cbChartData2.Enter += new System.EventHandler(this.cbChartData_Enter);
            this.cbChartData2.TextChanged += new System.EventHandler(this.cbChartData_TextChanged);
            // 
            // lChartData2
            // 
            this.lChartData2.AutoSize = true;
            this.lChartData2.Location = new System.Drawing.Point(43, 353);
            this.lChartData2.Name = "lChartData2";
            this.lChartData2.Size = new System.Drawing.Size(53, 12);
            this.lChartData2.TabIndex = 38;
            this.lChartData2.Text = "Y 坐标：";
            this.lChartData2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbChartData3
            // 
            this.cbChartData3.Location = new System.Drawing.Point(92, 378);
            this.cbChartData3.Name = "cbChartData3";
            this.cbChartData3.Size = new System.Drawing.Size(369, 20);
            this.cbChartData3.TabIndex = 15;
            this.cbChartData3.Enter += new System.EventHandler(this.cbChartData_Enter);
            this.cbChartData3.TextChanged += new System.EventHandler(this.cbChartData_TextChanged);
            // 
            // lChartData3
            // 
            this.lChartData3.AutoSize = true;
            this.lChartData3.Location = new System.Drawing.Point(31, 382);
            this.lChartData3.Name = "lChartData3";
            this.lChartData3.Size = new System.Drawing.Size(65, 12);
            this.lChartData3.TabIndex = 40;
            this.lChartData3.Text = "冒泡大小：";
            this.lChartData3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DialogNewChart
            // 
            this.AcceptButton = this.bOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(544, 443);
            this.Controls.Add(this.cbSubType);
            this.Controls.Add(this.cbChartData3);
            this.Controls.Add(this.lChartData3);
            this.Controls.Add(this.cbChartData2);
            this.Controls.Add(this.lChartData2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbChartType);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbChartData);
            this.Controls.Add(this.lChartData);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bSeriesDelete);
            this.Controls.Add(this.bSeriesDown);
            this.Controls.Add(this.bSeriesUp);
            this.Controls.Add(this.bCategoryDelete);
            this.Controls.Add(this.lbChartSeries);
            this.Controls.Add(this.bSeries);
            this.Controls.Add(this.bCategory);
            this.Controls.Add(this.bCategoryDown);
            this.Controls.Add(this.bCategoryUp);
            this.Controls.Add(this.lbChartCategories);
            this.Controls.Add(this.lbFields);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbDataSets);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogNewChart";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "插入图表";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void bOK_Click(object sender, System.EventArgs e)
		{
            bool bFail = false;
            string ctype = cbChartType.Text.ToLowerInvariant();
            if (cbChartData.Text.Length == 0 && lbChartSeries.Items.Count == 0) //Added second condition 05122007GJL
            {
                MessageBox.Show("Please fill out the chart data expression, or include a series.");
                bFail = true;
            }
            else if (ctype == "scatter" && cbChartData2.Text.Length == 0 && lbChartSeries.Items.Count == 0)
            {
                MessageBox.Show("Please fill out the chart Y coordinate data expression.");
                bFail = true;
                
            }
            else if (ctype == "bubble" && (cbChartData2.Text.Length == 0 || cbChartData3.Text.Length == 0))
            {
                MessageBox.Show("Please fill out the chart Y coordinate and Bubble width expressions.");
                bFail = true;
            }
            if (bFail)
                return;
			// apply the result
			DialogResult = DialogResult.OK;
		}

		private void cbDataSets_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.lbChartCategories.Items.Clear();
			this.lbChartSeries.Items.Clear();
			bOK.Enabled = false;
			this.lbFields.Items.Clear();
			string [] fields = _Draw.GetFields(cbDataSets.Text, false);
			if (fields != null)
				lbFields.Items.AddRange(fields);
		}

		private void bCategory_Click(object sender, System.EventArgs e)
		{
			ICollection sic = lbFields.SelectedIndices;
			int count=sic.Count;
			foreach (int i in sic)
			{
				string fname = (string) lbFields.Items[i];
				if (this.lbChartCategories.Items.IndexOf(fname) < 0)
					lbChartCategories.Items.Add(fname);
			}
			OkEnable();
		}

		private void bSeries_Click(object sender, System.EventArgs e)
		{
			ICollection sic = lbFields.SelectedIndices;
			int count=sic.Count;
			foreach (int i in sic)
			{
				string fname = (string) lbFields.Items[i];
				if (this.lbChartSeries.Items.IndexOf(fname) < 0 || cbChartType.SelectedItem.Equals("Scatter"))
					lbChartSeries.Items.Add(fname);
			}
			OkEnable();
		}

		private void bCategoryUp_Click(object sender, System.EventArgs e)
		{
			int index = lbChartCategories.SelectedIndex;
			if (index <= 0)
				return;

			string prename = (string) lbChartCategories.Items[index-1];
			lbChartCategories.Items.RemoveAt(index-1);
			lbChartCategories.Items.Insert(index, prename);
		}

		private void bCategoryDown_Click(object sender, System.EventArgs e)
		{
			int index = lbChartCategories.SelectedIndex;
			if (index < 0 || index + 1 == lbChartCategories.Items.Count)
				return;

			string postname = (string) lbChartCategories.Items[index+1];
			lbChartCategories.Items.RemoveAt(index+1);
			lbChartCategories.Items.Insert(index, postname);
		}

		private void bCategoryDelete_Click(object sender, System.EventArgs e)
		{
			int index = lbChartCategories.SelectedIndex;
			if (index < 0)
				return;

			lbChartCategories.Items.RemoveAt(index);
			OkEnable();
		}

		private void bSeriesUp_Click(object sender, System.EventArgs e)
		{
			int index = lbChartSeries.SelectedIndex;
			if (index <= 0)
				return;

			string prename = (string) lbChartSeries.Items[index-1];
			lbChartSeries.Items.RemoveAt(index-1);
			lbChartSeries.Items.Insert(index, prename);
		}

		private void bSeriesDown_Click(object sender, System.EventArgs e)
		{
			int index = lbChartSeries.SelectedIndex;
			if (index < 0 || index + 1 == lbChartSeries.Items.Count)
				return;

			string postname = (string) lbChartSeries.Items[index+1];
			lbChartSeries.Items.RemoveAt(index+1);
			lbChartSeries.Items.Insert(index, postname);
		}

		private void bSeriesDelete_Click(object sender, System.EventArgs e)
		{
			int index = lbChartSeries.SelectedIndex;
			if (index < 0)
				return;

			lbChartSeries.Items.RemoveAt(index);
			OkEnable();
		}

		private void OkEnable()
		{
			// We need values in datasets and Categories or Series for OK to work correctly
            bool bEnable = (this.lbChartCategories.Items.Count > 0 ||
                          this.lbChartSeries.Items.Count > 0) &&
                        this.cbDataSets.Text != null &&
                        this.cbDataSets.Text.Length > 0;
                       // && this.cbChartData.Text.Length > 0; Not needed with static series 05122007GJL
            string ctype = cbChartType.Text.ToLowerInvariant();
            if (ctype == "scatter")
                bEnable = bEnable && (this.cbChartData2.Text.Length > 0 || lbChartSeries.Items.Count > 0);
            else if (ctype == "bubble")
                bEnable = bEnable && (this.cbChartData2.Text.Length > 0 && this.cbChartData3.Text.Length > 0);
            
            bOK.Enabled = bEnable;
        }

		private void cbChartData_Enter(object sender, System.EventArgs e)
		{
            ComboBox cb = sender as ComboBox;
            if (cb == null)
                return;
			cb.Items.Clear();
			foreach (string field in this.lbFields.Items)
			{
				if (this.lbChartCategories.Items.IndexOf(field) >= 0 ||
					this.lbChartSeries.Items.IndexOf(field) >= 0)
					continue;
				// Field selected in columns and rows
				cb.Items.Add(string.Format("=Sum(Fields!{0}.Value)", field));
			}
		}

		private void cbChartData_TextChanged(object sender, System.EventArgs e)
		{
			OkEnable();
		}

		private void cbChartType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// Change the potential sub-types
			string savesub = cbSubType.Text;
			string[] subItems;
            bool bEnableData2 = false;
            bool bEnableData3 = false;
			switch (cbChartType.Text)
			{
				case "Column":
					subItems = new string [] {"Plain", "Stacked", "PercentStacked"};
					break;
				case "Bar":
					subItems = new string [] {"Plain", "Stacked", "PercentStacked"};
					break;
				case "Line":
					subItems = new string [] {"Plain", "Smooth"};
					break;
				case "Pie":
					subItems = new string [] {"Plain", "Exploded"};
					break;
				case "Area":
					subItems = new string [] {"Plain", "Stacked"};
					break;
				case "Doughnut":
					subItems = new string [] {"Plain"};
					break;
				case "Scatter":
					subItems = new string [] {"Plain", "Line", "SmoothLine"};
                    bEnableData2 = true;
                    break;
				case "Bubble":
                    subItems = new string[] { "Plain" };
                    bEnableData2 = bEnableData3 = true;
                    break;
                case "Stock":
                default:
					subItems = new string [] {"Plain"};
					break;
			}
            lChartData2.Enabled = cbChartData2.Enabled = bEnableData2;
            lChartData3.Enabled = cbChartData3.Enabled = bEnableData3;
            
            // handle the subtype
            cbSubType.Items.Clear();
			cbSubType.Items.AddRange(subItems);
			int i=0;
			foreach (string s in subItems)
			{
				if (s == savesub)
				{
					cbSubType.SelectedIndex = i;
					break;
				}
				i++;
			}
			// Didn't match old style
            if (i >= subItems.Length)
                i = 0;
            cbSubType.SelectedIndex = i;
        }

      
	}
}
