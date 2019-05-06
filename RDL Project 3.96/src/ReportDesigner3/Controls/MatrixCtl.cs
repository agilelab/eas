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
	/// Summary description for ReportCtl.
	/// </summary>
	internal class MatrixCtl : System.Windows.Forms.UserControl, IProperty
	{
        private List<XmlNode> _ReportItems;
		private DesignXmlDraw _Draw;
		bool fDataSet, fPBBefore, fPBAfter, fNoRows, fCellDataElementOutput, fCellDataElementName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cbDataSet;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox chkPBBefore;
		private System.Windows.Forms.CheckBox chkPBAfter;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbNoRows;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox chkCellContents;
		private System.Windows.Forms.TextBox tbCellDataElementName;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        internal MatrixCtl(DesignXmlDraw dxDraw, List<XmlNode> ris)
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
			XmlNode riNode = _ReportItems[0];

			tbNoRows.Text = _Draw.GetElementValue(riNode, "NoRows", "");
			cbDataSet.Items.AddRange(_Draw.DataSetNames);
			cbDataSet.Text = _Draw.GetDataSetNameValue(riNode);
			if (_Draw.GetReportItemDataRegionContainer(riNode) != null)
				cbDataSet.Enabled = false;
			chkPBBefore.Checked = _Draw.GetElementValue(riNode, "PageBreakAtStart", "false").ToLower()=="true"? true:false;
			chkPBAfter.Checked = _Draw.GetElementValue(riNode, "PageBreakAtEnd", "false").ToLower()=="true"? true:false;
			this.chkCellContents.Checked = _Draw.GetElementValue(riNode, "CellDataElementOutput", "Output")=="Output"?true:false;
			this.tbCellDataElementName.Text =  _Draw.GetElementValue(riNode, "CellDataElementName", "Cell");

			fNoRows = fDataSet = fPBBefore = fPBAfter = fCellDataElementOutput = fCellDataElementName = false;
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
            this.label2 = new System.Windows.Forms.Label();
            this.cbDataSet = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkPBAfter = new System.Windows.Forms.CheckBox();
            this.chkPBBefore = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbNoRows = new System.Windows.Forms.TextBox();
            this.tbCellDataElementName = new System.Windows.Forms.TextBox();
            this.chkCellContents = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "数据集名称：";
            // 
            // cbDataSet
            // 
            this.cbDataSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataSet.Location = new System.Drawing.Point(98, 19);
            this.cbDataSet.Name = "cbDataSet";
            this.cbDataSet.Size = new System.Drawing.Size(326, 20);
            this.cbDataSet.TabIndex = 1;
            this.cbDataSet.SelectedIndexChanged += new System.EventHandler(this.cbDataSet_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkPBAfter);
            this.groupBox1.Controls.Add(this.chkPBBefore);
            this.groupBox1.Location = new System.Drawing.Point(24, 91);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 48);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "换页符";
            // 
            // chkPBAfter
            // 
            this.chkPBAfter.AutoSize = true;
            this.chkPBAfter.Location = new System.Drawing.Point(192, 24);
            this.chkPBAfter.Name = "chkPBAfter";
            this.chkPBAfter.Size = new System.Drawing.Size(108, 16);
            this.chkPBAfter.TabIndex = 1;
            this.chkPBAfter.Text = "在矩阵之后插入";
            this.chkPBAfter.CheckedChanged += new System.EventHandler(this.chkPBAfter_CheckedChanged);
            // 
            // chkPBBefore
            // 
            this.chkPBBefore.AutoSize = true;
            this.chkPBBefore.Location = new System.Drawing.Point(16, 24);
            this.chkPBBefore.Name = "chkPBBefore";
            this.chkPBBefore.Size = new System.Drawing.Size(108, 16);
            this.chkPBBefore.TabIndex = 0;
            this.chkPBBefore.Text = "在矩阵之前插入";
            this.chkPBBefore.CheckedChanged += new System.EventHandler(this.chkPBBefore_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "空行时提示：";
            // 
            // tbNoRows
            // 
            this.tbNoRows.Location = new System.Drawing.Point(98, 51);
            this.tbNoRows.Name = "tbNoRows";
            this.tbNoRows.Size = new System.Drawing.Size(326, 21);
            this.tbNoRows.TabIndex = 3;
            this.tbNoRows.Text = "textBox1";
            this.tbNoRows.TextChanged += new System.EventHandler(this.tbNoRows_TextChanged);
            // 
            // tbCellDataElementName
            // 
            this.tbCellDataElementName.Location = new System.Drawing.Point(117, 42);
            this.tbCellDataElementName.Name = "tbCellDataElementName";
            this.tbCellDataElementName.Size = new System.Drawing.Size(277, 21);
            this.tbCellDataElementName.TabIndex = 0;
            this.tbCellDataElementName.TextChanged += new System.EventHandler(this.tbCellDataElementName_TextChanged);
            // 
            // chkCellContents
            // 
            this.chkCellContents.AutoSize = true;
            this.chkCellContents.Location = new System.Drawing.Point(20, 21);
            this.chkCellContents.Name = "chkCellContents";
            this.chkCellContents.Size = new System.Drawing.Size(96, 16);
            this.chkCellContents.TabIndex = 0;
            this.chkCellContents.Text = "提供明细输出";
            this.chkCellContents.CheckedChanged += new System.EventHandler(this.chkCellContents_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "单元格元素名称：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbCellDataElementName);
            this.groupBox2.Controls.Add(this.chkCellContents);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(24, 155);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(400, 72);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "XML";
            // 
            // MatrixCtl
            // 
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.tbNoRows);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbDataSet);
            this.Controls.Add(this.label2);
            this.Name = "MatrixCtl";
            this.Size = new System.Drawing.Size(446, 255);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
			fNoRows = fDataSet = fPBBefore = fPBAfter= fCellDataElementOutput = fCellDataElementName = false;
		}

		public void ApplyChanges(XmlNode node)
		{
			if (fNoRows)
				_Draw.SetElement(node, "NoRows", this.tbNoRows.Text);
			if (fDataSet)
				_Draw.SetElement(node, "DataSetName", this.cbDataSet.Text);
			if (fPBBefore)
				_Draw.SetElement(node, "PageBreakAtStart", this.chkPBBefore.Checked? "true":"false");
			if (fPBAfter)
				_Draw.SetElement(node, "PageBreakAtEnd", this.chkPBAfter.Checked? "true":"false");
			if (fCellDataElementOutput)
				_Draw.SetElement(node, "CellDataElementOutput", this.chkCellContents.Checked? "Output":"NoOutput");
			if (fCellDataElementName)
			{
				if (this.tbCellDataElementName.Text.Length > 0)
					_Draw.SetElement(node, "CellDataElementName", this.tbCellDataElementName.Text);
				else
					_Draw.RemoveElement(node, "CellDataElementName");
			}
		}

		private void cbDataSet_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fDataSet = true;
		}

		private void chkPBBefore_CheckedChanged(object sender, System.EventArgs e)
		{
			fPBBefore = true;
		}

		private void chkPBAfter_CheckedChanged(object sender, System.EventArgs e)
		{
			fPBAfter = true;
		}

		private void tbNoRows_TextChanged(object sender, System.EventArgs e)
		{
			fNoRows = true;
		}

		private void tbCellDataElementName_TextChanged(object sender, System.EventArgs e)
		{
			fCellDataElementName = true;
		}

		private void chkCellContents_CheckedChanged(object sender, System.EventArgs e)
		{
			this.fCellDataElementOutput = true;
		}
	}
}
