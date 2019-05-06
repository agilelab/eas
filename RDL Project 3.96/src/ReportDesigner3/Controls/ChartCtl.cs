/* ====================================================================
   Copyright (C) 2004-2008  fyiReporting Software, LLC

   This file is part of the fyiReporting RDL project.
	
   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.


   For additional information, email info@fyireporting.com or visit
   the website www.fyiReporting.com.
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Xml;

namespace fyiReporting.RdlDesign
{
	/// <summary>
	/// Summary description for ChartCtl.
	/// </summary>
	internal class ChartCtl : System.Windows.Forms.UserControl, IProperty
	{
        private List<XmlNode> _ReportItems;
		private DesignXmlDraw _Draw;
		bool fChartType, fSubtype, fPalette, fRenderElement, fPercentWidth;
		bool fNoRows, fDataSet, fPageBreakStart, fPageBreakEnd;
		bool fChartData;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox cbChartType;
		private System.Windows.Forms.ComboBox cbSubType;
		private System.Windows.Forms.ComboBox cbPalette;
		private System.Windows.Forms.ComboBox cbRenderElement;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown tbPercentWidth;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox tbNoRows;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox cbDataSet;
		private System.Windows.Forms.CheckBox chkPageBreakStart;
		private System.Windows.Forms.CheckBox chkPageBreakEnd;
        private System.Windows.Forms.ComboBox cbChartData;
        private ComboBox cbDataLabel;
        private CheckBox chkDataLabel;
        private Button bDataLabelExpr;
		private System.Windows.Forms.Label lData1;
        private ComboBox cbChartData2;
        private Label lData2;
        private ComboBox cbChartData3;
        private Label lData3;
        private Button bDataExpr;
        private Button bDataExpr3;
        private Button bDataExpr2;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        internal ChartCtl(DesignXmlDraw dxDraw, List<XmlNode> ris)
		{
			_ReportItems = ris;
			_Draw = dxDraw;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// Initialize form using the style node values
			InitValues();			
		}

		private void InitValues()
		{
			XmlNode node = _ReportItems[0];

            string type = _Draw.GetElementValue(node, "Type", "Column");
            this.cbChartType.Text = type;
            type = type.ToLowerInvariant();

            lData2.Enabled = cbChartData2.Enabled = bDataExpr2.Enabled = (type == "scatter" || type == "bubble");
            lData3.Enabled = cbChartData3.Enabled = bDataExpr3.Enabled = (type == "bubble");

			this.cbSubType.Text = _Draw.GetElementValue(node, "Subtype", "Plain");
			this.cbPalette.Text = _Draw.GetElementValue(node, "Palette", "Default");
			this.cbRenderElement.Text = _Draw.GetElementValue(node, "ChartElementOutput", "Output");
			this.tbPercentWidth.Text = _Draw.GetElementValue(node, "PointWidth", "0");
			this.tbNoRows.Text = _Draw.GetElementValue(node, "NoRows", "");
			// Handle the dataset for this dataregion
			object[] dsNames = _Draw.DataSetNames;
			string defName="";
			if (dsNames != null && dsNames.Length > 0)
			{
				this.cbDataSet.Items.AddRange(_Draw.DataSetNames);
				defName = (string) dsNames[0];
			}
			cbDataSet.Text = _Draw.GetDataSetNameValue(node);
			if (_Draw.GetReportItemDataRegionContainer(node) != null)
				cbDataSet.Enabled = false;
			// page breaks
			this.chkPageBreakStart.Checked = _Draw.GetElementValue(node, "PageBreakAtStart", "false").ToLower() == "true"? true: false;
			this.chkPageBreakEnd.Checked = _Draw.GetElementValue(node, "PageBreakAtEnd", "false").ToLower() == "true"? true: false;

			// Chart data-- this is a simplification of what is possible (TODO) 
			string cdata=string.Empty;
            string cdata2 = string.Empty;
            string cdata3 = string.Empty;
//        <ChartData>
//          <ChartSeries>
//            <DataPoints>
//              <DataPoint>
            //<DataValues>
            //      <DataValue>
            //        <Value>=Sum(Fields!Sales.Value)</Value>
            //      </DataValue>
            //      <DataValue>
            //        <Value>=Fields!Year.Value</Value>         ----- only scatter and bubble
            //      </DataValue>
            //      <DataValue>
            //        <Value>=Sum(Fields!Sales.Value)</Value>   ----- only bubble
            //      </DataValue>
            //    </DataValues>
//                <DataLabel>
//                  <Style>
//                    <Format>c</Format>
//                  </Style>
//                </DataLabel>
//                <Marker />
//              </DataPoint>
//            </DataPoints>
//          </ChartSeries>
//        </ChartData>

          
            //Determine if we have a static series or not... We are not allowing this to be changed here. That decision is taken when creating the chart. 05122007GJL
            XmlNode ss = DesignXmlDraw.FindNextInHierarchy(node, "SeriesGroupings", "SeriesGrouping", "StaticSeries");
            bool StaticSeries = ss != null;    
                                  
            XmlNode dvs = DesignXmlDraw.FindNextInHierarchy(node,
                "ChartData", "ChartSeries", "DataPoints", "DataPoint", "DataValues");
            int iter = 0;
            XmlNode cnode;
            foreach (XmlNode dv in dvs.ChildNodes)
            {
                if (dv.Name != "DataValue")
                    continue;
                iter++;
                cnode = DesignXmlDraw.FindNextInHierarchy(dv, "Value");
                if (cnode == null)
                    continue;
                switch (iter)
                {
                    case 1:
                        cdata = cnode.InnerText;
                        break;
                    case 2:
                        cdata2 = cnode.InnerText;
                        break;
                    case 3:
                        cdata3 = cnode.InnerText;
                        break;
                    default:
                        break;
                }
            }
			this.cbChartData.Text = cdata;
            this.cbChartData2.Text = cdata2;
            this.cbChartData3.Text = cdata3;
 
            //If the chart doesn't have a static series then dont show the datalabel values. 05122007GJL
            if (!StaticSeries) 
            {     
                //GJL 131107 Added data labels
                XmlNode labelExprNode = DesignXmlDraw.FindNextInHierarchy(node,
                    "ChartData", "ChartSeries", "DataPoints", "DataPoint", "DataLabel", "Value");
                if (labelExprNode != null)
                    this.cbDataLabel.Text = labelExprNode.InnerText;
                XmlNode labelVisNode = DesignXmlDraw.FindNextInHierarchy(node,
                    "ChartData", "ChartSeries", "DataPoints", "DataPoint", "DataLabel", "Visible");
                if (labelVisNode != null)
                    this.chkDataLabel.Checked = labelVisNode.InnerText.ToUpper().Equals("TRUE");
            }

            chkDataLabel.Enabled = bDataLabelExpr.Enabled = cbDataLabel.Enabled = 
                bDataExpr.Enabled = cbChartData.Enabled = !StaticSeries; 
            // Don't allow the datalabel OR datavalues to be changed here if we have a static series GJL


			fChartType = fSubtype = fPalette = fRenderElement = fPercentWidth =
				fNoRows = fDataSet = fPageBreakStart = fPageBreakEnd = fChartData = false;           
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbChartType = new System.Windows.Forms.ComboBox();
            this.cbSubType = new System.Windows.Forms.ComboBox();
            this.cbPalette = new System.Windows.Forms.ComboBox();
            this.cbRenderElement = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbPercentWidth = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.tbNoRows = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbDataSet = new System.Windows.Forms.ComboBox();
            this.chkPageBreakStart = new System.Windows.Forms.CheckBox();
            this.chkPageBreakEnd = new System.Windows.Forms.CheckBox();
            this.cbChartData = new System.Windows.Forms.ComboBox();
            this.cbDataLabel = new System.Windows.Forms.ComboBox();
            this.chkDataLabel = new System.Windows.Forms.CheckBox();
            this.bDataLabelExpr = new System.Windows.Forms.Button();
            this.lData1 = new System.Windows.Forms.Label();
            this.cbChartData2 = new System.Windows.Forms.ComboBox();
            this.lData2 = new System.Windows.Forms.Label();
            this.cbChartData3 = new System.Windows.Forms.ComboBox();
            this.lData3 = new System.Windows.Forms.Label();
            this.bDataExpr = new System.Windows.Forms.Button();
            this.bDataExpr3 = new System.Windows.Forms.Button();
            this.bDataExpr2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tbPercentWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "图表类型：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(57, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "色调参考：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "提供XML元素：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "饼图百分比：";
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
            this.cbChartType.Location = new System.Drawing.Point(120, 9);
            this.cbChartType.Name = "cbChartType";
            this.cbChartType.Size = new System.Drawing.Size(121, 20);
            this.cbChartType.TabIndex = 0;
            this.cbChartType.SelectedIndexChanged += new System.EventHandler(this.cbChartType_SelectedIndexChanged);
            // 
            // cbSubType
            // 
            this.cbSubType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSubType.Location = new System.Drawing.Point(308, 9);
            this.cbSubType.Name = "cbSubType";
            this.cbSubType.Size = new System.Drawing.Size(98, 20);
            this.cbSubType.TabIndex = 1;
            this.cbSubType.SelectedIndexChanged += new System.EventHandler(this.cbSubType_SelectedIndexChanged);
            // 
            // cbPalette
            // 
            this.cbPalette.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPalette.Items.AddRange(new object[] {
            "Default",
            "EarthTones",
            "Excel",
            "GrayScale",
            "Light",
            "Pastel",
            "SemiTransparent",
            "Patterned",
            "PatternedBlack"});
            this.cbPalette.Location = new System.Drawing.Point(120, 34);
            this.cbPalette.Name = "cbPalette";
            this.cbPalette.Size = new System.Drawing.Size(121, 20);
            this.cbPalette.TabIndex = 2;
            this.cbPalette.SelectedIndexChanged += new System.EventHandler(this.cbPalette_SelectedIndexChanged);
            // 
            // cbRenderElement
            // 
            this.cbRenderElement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRenderElement.Items.AddRange(new object[] {
            "Output",
            "NoOutput"});
            this.cbRenderElement.Location = new System.Drawing.Point(120, 59);
            this.cbRenderElement.Name = "cbRenderElement";
            this.cbRenderElement.Size = new System.Drawing.Size(121, 20);
            this.cbRenderElement.TabIndex = 3;
            this.cbRenderElement.SelectedIndexChanged += new System.EventHandler(this.cbRenderElement_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(258, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "子类型：";
            // 
            // tbPercentWidth
            // 
            this.tbPercentWidth.Location = new System.Drawing.Point(120, 84);
            this.tbPercentWidth.Name = "tbPercentWidth";
            this.tbPercentWidth.Size = new System.Drawing.Size(121, 21);
            this.tbPercentWidth.TabIndex = 4;
            this.tbPercentWidth.ValueChanged += new System.EventHandler(this.tbPercentWidth_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(33, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "空行提示消息：";
            // 
            // tbNoRows
            // 
            this.tbNoRows.Location = new System.Drawing.Point(120, 110);
            this.tbNoRows.Name = "tbNoRows";
            this.tbNoRows.Size = new System.Drawing.Size(255, 21);
            this.tbNoRows.TabIndex = 5;
            this.tbNoRows.TextChanged += new System.EventHandler(this.tbNoRows_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(69, 140);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "数据集：";
            // 
            // cbDataSet
            // 
            this.cbDataSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataSet.Location = new System.Drawing.Point(120, 136);
            this.cbDataSet.Name = "cbDataSet";
            this.cbDataSet.Size = new System.Drawing.Size(255, 20);
            this.cbDataSet.TabIndex = 6;
            this.cbDataSet.SelectedIndexChanged += new System.EventHandler(this.cbDataSet_SelectedIndexChanged);
            // 
            // chkPageBreakStart
            // 
            this.chkPageBreakStart.AutoSize = true;
            this.chkPageBreakStart.Location = new System.Drawing.Point(20, 264);
            this.chkPageBreakStart.Name = "chkPageBreakStart";
            this.chkPageBreakStart.Size = new System.Drawing.Size(120, 16);
            this.chkPageBreakStart.TabIndex = 13;
            this.chkPageBreakStart.Text = "开始前插入分页符";
            this.chkPageBreakStart.CheckedChanged += new System.EventHandler(this.chkPageBreakStart_CheckedChanged);
            // 
            // chkPageBreakEnd
            // 
            this.chkPageBreakEnd.AutoSize = true;
            this.chkPageBreakEnd.Location = new System.Drawing.Point(280, 264);
            this.chkPageBreakEnd.Name = "chkPageBreakEnd";
            this.chkPageBreakEnd.Size = new System.Drawing.Size(120, 16);
            this.chkPageBreakEnd.TabIndex = 14;
            this.chkPageBreakEnd.Text = "结束后插入分页符";
            this.chkPageBreakEnd.CheckedChanged += new System.EventHandler(this.chkPageBreakEnd_CheckedChanged);
            // 
            // cbChartData
            // 
            this.cbChartData.Location = new System.Drawing.Point(120, 161);
            this.cbChartData.Name = "cbChartData";
            this.cbChartData.Size = new System.Drawing.Size(255, 20);
            this.cbChartData.TabIndex = 7;
            this.cbChartData.TextChanged += new System.EventHandler(this.cbChartData_Changed);
            // 
            // cbDataLabel
            // 
            this.cbDataLabel.Enabled = false;
            this.cbDataLabel.Location = new System.Drawing.Point(120, 236);
            this.cbDataLabel.Name = "cbDataLabel";
            this.cbDataLabel.Size = new System.Drawing.Size(254, 20);
            this.cbDataLabel.TabIndex = 17;
            this.cbDataLabel.TextChanged += new System.EventHandler(this.cbChartData_Changed);
            // 
            // chkDataLabel
            // 
            this.chkDataLabel.Location = new System.Drawing.Point(42, 238);
            this.chkDataLabel.Name = "chkDataLabel";
            this.chkDataLabel.Size = new System.Drawing.Size(84, 16);
            this.chkDataLabel.TabIndex = 19;
            this.chkDataLabel.Text = "数据标签：";
            this.chkDataLabel.UseVisualStyleBackColor = true;
            this.chkDataLabel.CheckedChanged += new System.EventHandler(this.chkDataLabel_CheckedChanged);
            // 
            // bDataLabelExpr
            // 
            this.bDataLabelExpr.Enabled = false;
            this.bDataLabelExpr.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.bDataLabelExpr.Location = new System.Drawing.Point(381, 236);
            this.bDataLabelExpr.Name = "bDataLabelExpr";
            this.bDataLabelExpr.Size = new System.Drawing.Size(30, 21);
            this.bDataLabelExpr.TabIndex = 20;
            this.bDataLabelExpr.Text = "fx";
            this.bDataLabelExpr.UseVisualStyleBackColor = true;
            this.bDataLabelExpr.Click += new System.EventHandler(this.bDataLabelExpr_Click);
            // 
            // lData1
            // 
            this.lData1.AutoSize = true;
            this.lData1.Location = new System.Drawing.Point(15, 165);
            this.lData1.Name = "lData1";
            this.lData1.Size = new System.Drawing.Size(107, 12);
            this.lData1.TabIndex = 16;
            this.lData1.Text = "图表数据(X坐标)：";
            // 
            // cbChartData2
            // 
            this.cbChartData2.Location = new System.Drawing.Point(120, 186);
            this.cbChartData2.Name = "cbChartData2";
            this.cbChartData2.Size = new System.Drawing.Size(255, 20);
            this.cbChartData2.TabIndex = 9;
            this.cbChartData2.TextChanged += new System.EventHandler(this.cbChartData_Changed);
            // 
            // lData2
            // 
            this.lData2.AutoSize = true;
            this.lData2.Location = new System.Drawing.Point(69, 190);
            this.lData2.Name = "lData2";
            this.lData2.Size = new System.Drawing.Size(53, 12);
            this.lData2.TabIndex = 18;
            this.lData2.Text = "Y 坐标：";
            // 
            // cbChartData3
            // 
            this.cbChartData3.Location = new System.Drawing.Point(120, 211);
            this.cbChartData3.Name = "cbChartData3";
            this.cbChartData3.Size = new System.Drawing.Size(255, 20);
            this.cbChartData3.TabIndex = 11;
            this.cbChartData3.TextChanged += new System.EventHandler(this.cbChartData_Changed);
            // 
            // lData3
            // 
            this.lData3.AutoSize = true;
            this.lData3.Location = new System.Drawing.Point(57, 215);
            this.lData3.Name = "lData3";
            this.lData3.Size = new System.Drawing.Size(65, 12);
            this.lData3.TabIndex = 20;
            this.lData3.Text = "冒泡大小：";
            // 
            // bDataExpr
            // 
            this.bDataExpr.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bDataExpr.Location = new System.Drawing.Point(381, 161);
            this.bDataExpr.Name = "bDataExpr";
            this.bDataExpr.Size = new System.Drawing.Size(30, 21);
            this.bDataExpr.TabIndex = 8;
            this.bDataExpr.Tag = "d1";
            this.bDataExpr.Text = "fx";
            this.bDataExpr.Click += new System.EventHandler(this.bDataExpr_Click);
            // 
            // bDataExpr3
            // 
            this.bDataExpr3.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bDataExpr3.Location = new System.Drawing.Point(381, 211);
            this.bDataExpr3.Name = "bDataExpr3";
            this.bDataExpr3.Size = new System.Drawing.Size(30, 21);
            this.bDataExpr3.TabIndex = 12;
            this.bDataExpr3.Tag = "d3";
            this.bDataExpr3.Text = "fx";
            this.bDataExpr3.Click += new System.EventHandler(this.bDataExpr_Click);
            // 
            // bDataExpr2
            // 
            this.bDataExpr2.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bDataExpr2.Location = new System.Drawing.Point(381, 186);
            this.bDataExpr2.Name = "bDataExpr2";
            this.bDataExpr2.Size = new System.Drawing.Size(30, 21);
            this.bDataExpr2.TabIndex = 10;
            this.bDataExpr2.Tag = "d2";
            this.bDataExpr2.Text = "fx";
            this.bDataExpr2.Click += new System.EventHandler(this.bDataExpr_Click);
            // 
            // ChartCtl
            // 
            this.Controls.Add(this.cbDataLabel);
            this.Controls.Add(this.cbSubType);
            this.Controls.Add(this.bDataExpr2);
            this.Controls.Add(this.bDataExpr3);
            this.Controls.Add(this.bDataExpr);
            this.Controls.Add(this.cbChartData3);
            this.Controls.Add(this.lData3);
            this.Controls.Add(this.cbChartData2);
            this.Controls.Add(this.lData2);
            this.Controls.Add(this.bDataLabelExpr);
            this.Controls.Add(this.chkDataLabel);
            this.Controls.Add(this.cbChartData);
            this.Controls.Add(this.lData1);
            this.Controls.Add(this.chkPageBreakEnd);
            this.Controls.Add(this.chkPageBreakStart);
            this.Controls.Add(this.cbDataSet);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbNoRows);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbPercentWidth);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbRenderElement);
            this.Controls.Add(this.cbPalette);
            this.Controls.Add(this.cbChartType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ChartCtl";
            this.Size = new System.Drawing.Size(428, 288);
            ((System.ComponentModel.ISupportInitialize)(this.tbPercentWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		public bool IsValid()
		{
			return true;
		}

		public void Apply()
		{
			// take information in control and apply to all the style nodes
			//  Only change information that has been marked as modified;
			//   this way when group is selected it is possible to change just
			//   the items you want and keep the rest the same.
				
			foreach (XmlNode riNode in this._ReportItems)
				ApplyChanges(riNode);

			// No more changes
			fChartType = fSubtype = fPalette = fRenderElement = fPercentWidth =
				fNoRows = fDataSet = fPageBreakStart = fPageBreakEnd = fChartData = false;
		}

		public void ApplyChanges(XmlNode node)
		{
			if (fChartType)
			{
				_Draw.SetElement(node, "Type", this.cbChartType.Text);
			}
			if (fSubtype)
			{
				_Draw.SetElement(node, "Subtype", this.cbSubType.Text);
			}
			if (fPalette)
			{
				_Draw.SetElement(node, "Palette", this.cbPalette.Text);
			}
			if (fRenderElement)
			{
				_Draw.SetElement(node, "ChartElementOutput", this.cbRenderElement.Text);
			}
			if (fPercentWidth)
			{
				_Draw.SetElement(node, "PointWidth", this.tbPercentWidth.Text);
			}
			if (fNoRows)
			{
				_Draw.SetElement(node, "NoRows", this.tbNoRows.Text);
			}
			if (fDataSet)
			{
				_Draw.SetElement(node, "DataSetName", this.cbDataSet.Text);
			}
			if (fPageBreakStart)
			{
				_Draw.SetElement(node, "PageBreakAtStart", this.chkPageBreakStart.Checked? "true": "false");
			}
			if (fPageBreakEnd)
			{
				_Draw.SetElement(node, "PageBreakAtEnd", this.chkPageBreakEnd.Checked? "true": "false");
			}
			if (fChartData)
			{
				//        <ChartData>
				//          <ChartSeries>
				//            <DataPoints>
				//              <DataPoint>
				//                <DataValues>
				//                  <DataValue>
				//                    <Value>=Sum(Fields!Sales.Value)</Value>
				//                  </DataValue>   --- you can have up to 3 DataValue elements
				//                </DataValues>
				//                <DataLabel>
				//                  <Style>
				//                    <Format>c</Format>
				//                  </Style>
				//                </DataLabel>
				//                <Marker />
				//              </DataPoint>
				//            </DataPoints>
				//          </ChartSeries>
				//        </ChartData>
				XmlNode chartdata = _Draw.SetElement(node, "ChartData", null);
				XmlNode chartseries = _Draw.SetElement(chartdata, "ChartSeries", null);
				XmlNode datapoints = _Draw.SetElement(chartseries, "DataPoints", null);
				XmlNode datapoint = _Draw.SetElement(datapoints, "DataPoint", null);
				XmlNode datavalues = _Draw.SetElement(datapoint, "DataValues", null);
                _Draw.RemoveElementAll(datavalues, "DataValue");
                XmlNode datalabel = _Draw.SetElement(datapoint, "DataLabel", null);
				XmlNode datavalue = _Draw.SetElement(datavalues, "DataValue", null);
				_Draw.SetElement(datavalue, "Value", this.cbChartData.Text);

                string type = cbChartType.Text.ToLowerInvariant();
                if (type == "scatter" || type == "bubble")
                {
                    datavalue = _Draw.CreateElement(datavalues, "DataValue", null);
                    _Draw.SetElement(datavalue, "Value", this.cbChartData2.Text);
                    if (type == "bubble")
                    {
                        datavalue = _Draw.CreateElement(datavalues, "DataValue", null);
                        _Draw.SetElement(datavalue, "Value", this.cbChartData3.Text);
                    }
                }
                _Draw.SetElement(datalabel, "Value", this.cbDataLabel.Text);
                _Draw.SetElement(datalabel, "Visible", this.chkDataLabel.Checked.ToString());
			}
		}

		private void cbChartType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fChartType = true;
			// Change the potential sub-types
			string savesub = cbSubType.Text;
			string[] subItems = new string [] {"Plain"};
            bool bEnableY = false;
            bool bEnableBubble = false;
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
					break;
				case "Scatter":
					subItems = new string [] {"Plain", "Line", "SmoothLine"};
                    bEnableY = true;
                    break;
				case "Stock":
                    break;
				case "Bubble":
                    bEnableY = bEnableBubble = true;
                    break;
				default:
					break;
			}
			cbSubType.Items.Clear();
			cbSubType.Items.AddRange(subItems);

            lData2.Enabled = cbChartData2.Enabled = bDataExpr2.Enabled = bEnableY;
            lData3.Enabled = cbChartData3.Enabled = bDataExpr3.Enabled = bEnableBubble;
            
            int i=0;
			foreach (string s in subItems)
			{
				if (s == savesub)
				{
					cbSubType.SelectedIndex = i;
					return;
				}
				i++;
			}
			// Didn't match old style
			cbSubType.SelectedIndex = 0;
		}

		private void cbSubType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fSubtype = true;
		}

		private void cbPalette_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fPalette = true;
		}

		private void cbRenderElement_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fRenderElement = true;
		}

		private void tbPercentWidth_ValueChanged(object sender, System.EventArgs e)
		{
			fPercentWidth = true;
		}

		private void tbNoRows_TextChanged(object sender, System.EventArgs e)
		{
			fNoRows = true;
		}

		private void cbDataSet_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fDataSet = true;
		}

		private void chkPageBreakStart_CheckedChanged(object sender, System.EventArgs e)
		{
			fPageBreakStart = true;
		}

		private void chkPageBreakEnd_CheckedChanged(object sender, System.EventArgs e)
		{
			fPageBreakEnd = true;
		}

		private void cbChartData_Changed(object sender, System.EventArgs e)
		{
			fChartData = true;		
		}
        private void bDataExpr_Click(object sender, System.EventArgs e)
        {
            Button bs = sender as Button;
            if (bs == null)
                return;
            Control ctl = null; 
            switch (bs.Tag as string)
            {
                case "d1":
                    ctl = cbChartData;
                    break;
                case "d2":
                    ctl = cbChartData2;
                    break;
                case "d3":
                    ctl = cbChartData3;
                    break;
                default:
                    return;
            }
            DialogExprEditor ee = new DialogExprEditor(_Draw, ctl.Text, _ReportItems[0], false);
            try
            {
                DialogResult dlgr = ee.ShowDialog();
                if (dlgr == DialogResult.OK)
                {
                    ctl.Text = ee.Expression;
                    fChartData = true;
                }
            }
            finally
            {
                ee.Dispose();
            }
        }

        private void chkDataLabel_CheckedChanged(object sender, EventArgs e)
        {
            cbDataLabel.Enabled = bDataLabelExpr.Enabled = chkDataLabel.Checked; 
        }

        private void bDataLabelExpr_Click(object sender, EventArgs e)
        {
            DialogExprEditor ee = new DialogExprEditor(_Draw, cbDataLabel.Text,_ReportItems[0] , false);
            try
            {
                if (ee.ShowDialog() == DialogResult.OK)
                {                 
                    cbDataLabel.Text = ee.Expression;
                }

            }
            finally
            {
                ee.Dispose();
            }
            return;
        }
	}
}
