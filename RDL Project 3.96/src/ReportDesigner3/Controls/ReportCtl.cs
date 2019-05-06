/* ====================================================================
    Copyright (C) 2004-2006  fyiReporting Software, LLC

    This file is part of the fyiReporting RDL project.
	
    The RDL project is free software; you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation; either version 2 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

    For additional information, email info@fyireporting.com or visit
    the website www.fyiReporting.com.
*/
using System;
using System.Collections;
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
	internal class ReportCtl : System.Windows.Forms.UserControl, IProperty
	{
		private DesignXmlDraw _Draw;
		// flags for controlling whether syntax changed for a particular property
		private System.Windows.Forms.TextBox tbReportAuthor;
		private System.Windows.Forms.TextBox tbReportDescription;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox tbPageWidth;
		private System.Windows.Forms.TextBox tbPageHeight;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox tbMarginLeft;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox tbMarginRight;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox tbMarginBottom;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox tbMarginTop;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox tbWidth;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox Ò³½Å;
		private System.Windows.Forms.CheckBox chkPFFirst;
		private System.Windows.Forms.CheckBox chkPHFirst;
		private System.Windows.Forms.CheckBox chkPHLast;
		private System.Windows.Forms.CheckBox chkPFLast;
        private ComboBox cbxPages;
        private Label label10;
        private ComboBox cbxXY;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		internal ReportCtl(DesignXmlDraw dxDraw)
		{
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            this.cbxPages.Items.Clear();
            foreach (PrintPage page in CacheHelper.Instance.Pages)
            {
                this.cbxPages.Items.Add(page.Name);
            }

            this.cbxPages.SelectedIndexChanged += new EventHandler(cbxPages_SelectedIndexChanged);
            this.cbxPages.SelectedIndex = this.cbxPages.Items.Count -1;

			_Draw = dxDraw;		

			// Initialize form using the style node values
			InitValues();            
		}

		private void InitValues()
		{
			XmlNode rNode = _Draw.GetReportNode();

            tbWidth.Text = _Draw.GetElementValue(rNode, "Width", "");

			tbReportAuthor.Text = _Draw.GetElementValue(rNode, "Author", "");
			tbReportDescription.Text = _Draw.GetElementValue(rNode, "Description", "");

            tbPageWidth.Text = _Draw.GetElementValue(rNode, "PageWidth", "8.5in");
            tbPageHeight.Text = _Draw.GetElementValue(rNode, "PageHeight", "11in");

            tbMarginLeft.Text = _Draw.GetElementValue(rNode, "LeftMargin", "");
            tbMarginRight.Text = _Draw.GetElementValue(rNode, "RightMargin", "");
            tbMarginBottom.Text = _Draw.GetElementValue(rNode, "BottomMargin", "");
            tbMarginTop.Text = _Draw.GetElementValue(rNode, "TopMargin", "");

			// Page header settings
			XmlNode phNode = _Draw.GetCreateNamedChildNode(rNode, "PageHeader");
			this.chkPHFirst.Checked = _Draw.GetElementValue(phNode, "PrintOnFirstPage", "true").ToLower() == "true"? true: false;
			this.chkPHLast.Checked = _Draw.GetElementValue(phNode, "PrintOnLastPage", "true").ToLower() == "true"? true: false;
			// Page footer settings
			XmlNode pfNode = _Draw.GetCreateNamedChildNode(rNode, "PageFooter");
			this.chkPFFirst.Checked = _Draw.GetElementValue(pfNode, "PrintOnFirstPage", "true").ToLower() == "true"? true: false;
			this.chkPFLast.Checked = _Draw.GetElementValue(pfNode, "PrintOnLastPage", "true").ToLower() == "true"? true: false;

            string sW = _Draw.GetElementValue(rNode, "PageWidth", "8.5in");
            string sH = _Draw.GetElementValue(rNode, "PageHeight", "11in");

            int index = -1;
            int XY = -1;

            for (int i = 0;i<CacheHelper.Instance.Pages.Length;i++)
            {
                PrintPage page = CacheHelper.Instance.Pages[i];

                if ((sW == page.WidthString) & (sH == page.HeightString))
                {
                    index = i;
                    XY = 0;
                    break;
                }

                if ((sH == page.WidthString) & (sW == page.HeightString))
                {
                    index = i;
                    XY = 1;
                    break;
                }
            }

            if (index > -1)
            {
                this.cbxPages.SelectedIndex = index;
                
                if (XY > -1)
                {
                    this.cbxXY.SelectedIndex = XY;
                    this.cbxXY.Enabled = true;
                }
            }
            else
            {
                this.tbPageWidth.Text = sW;
                this.tbPageHeight.Text = sH;

                this.cbxPages.SelectedIndex = CacheHelper.Instance.Pages.Length - 1;
                this.cbxXY.Enabled = false;
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.tbReportAuthor = new System.Windows.Forms.TextBox();
            this.tbReportDescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxXY = new System.Windows.Forms.ComboBox();
            this.cbxPages = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbPageHeight = new System.Windows.Forms.TextBox();
            this.tbPageWidth = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbMarginBottom = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbMarginTop = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbMarginRight = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbMarginLeft = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbWidth = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkPHLast = new System.Windows.Forms.CheckBox();
            this.chkPHFirst = new System.Windows.Forms.CheckBox();
            this.Ò³½Å = new System.Windows.Forms.GroupBox();
            this.chkPFLast = new System.Windows.Forms.CheckBox();
            this.chkPFFirst = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.Ò³½Å.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbReportAuthor
            // 
            this.tbReportAuthor.Location = new System.Drawing.Point(63, 10);
            this.tbReportAuthor.Name = "tbReportAuthor";
            this.tbReportAuthor.Size = new System.Drawing.Size(355, 21);
            this.tbReportAuthor.TabIndex = 0;
            // 
            // tbReportDescription
            // 
            this.tbReportDescription.Location = new System.Drawing.Point(63, 44);
            this.tbReportDescription.Name = "tbReportDescription";
            this.tbReportDescription.Size = new System.Drawing.Size(355, 21);
            this.tbReportDescription.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "×÷Õß£º";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "ËµÃ÷£º";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxXY);
            this.groupBox1.Controls.Add(this.cbxPages);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.tbPageHeight);
            this.groupBox1.Controls.Add(this.tbPageWidth);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(16, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(402, 48);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ò³Ãæ";
            // 
            // cbxXY
            // 
            this.cbxXY.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxXY.Items.AddRange(new object[] {
            "×ÝÏò",
            "ºáÏò"});
            this.cbxXY.Location = new System.Drawing.Point(332, 20);
            this.cbxXY.Name = "cbxXY";
            this.cbxXY.Size = new System.Drawing.Size(63, 20);
            this.cbxXY.TabIndex = 20;
            // 
            // cbxPages
            // 
            this.cbxPages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPages.Location = new System.Drawing.Point(47, 20);
            this.cbxPages.Name = "cbxPages";
            this.cbxPages.Size = new System.Drawing.Size(89, 20);
            this.cbxPages.TabIndex = 17;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 16;
            this.label10.Text = "Ö½ÕÅ£º";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbPageHeight
            // 
            this.tbPageHeight.Location = new System.Drawing.Point(268, 20);
            this.tbPageHeight.MaxLength = 10;
            this.tbPageHeight.Name = "tbPageHeight";
            this.tbPageHeight.Size = new System.Drawing.Size(57, 21);
            this.tbPageHeight.TabIndex = 1;
            this.tbPageHeight.Tag = "Page Height";
            this.tbPageHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbPageHeight.Validating += new System.ComponentModel.CancelEventHandler(this.tbSize_Validating);
            // 
            // tbPageWidth
            // 
            this.tbPageWidth.Location = new System.Drawing.Point(179, 20);
            this.tbPageWidth.MaxLength = 10;
            this.tbPageWidth.Name = "tbPageWidth";
            this.tbPageWidth.Size = new System.Drawing.Size(57, 21);
            this.tbPageWidth.TabIndex = 0;
            this.tbPageWidth.Tag = "Page Width";
            this.tbPageWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbPageWidth.Validating += new System.ComponentModel.CancelEventHandler(this.tbSize_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(242, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "¸ß£º";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(152, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "¿í£º";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbMarginBottom);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.tbMarginTop);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.tbMarginRight);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.tbMarginLeft);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(16, 136);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(402, 72);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ò³Âë±ß¾à";
            // 
            // tbMarginBottom
            // 
            this.tbMarginBottom.Location = new System.Drawing.Point(221, 43);
            this.tbMarginBottom.Name = "tbMarginBottom";
            this.tbMarginBottom.Size = new System.Drawing.Size(72, 21);
            this.tbMarginBottom.TabIndex = 3;
            this.tbMarginBottom.Tag = "Bottom Margin";
            this.tbMarginBottom.Validating += new System.ComponentModel.CancelEventHandler(this.tbSize_Validating);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(165, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "ÏÂ±ß¾à£º";
            // 
            // tbMarginTop
            // 
            this.tbMarginTop.Location = new System.Drawing.Point(64, 43);
            this.tbMarginTop.Name = "tbMarginTop";
            this.tbMarginTop.Size = new System.Drawing.Size(72, 21);
            this.tbMarginTop.TabIndex = 2;
            this.tbMarginTop.Tag = "Top Margin";
            this.tbMarginTop.Validating += new System.ComponentModel.CancelEventHandler(this.tbSize_Validating);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 4;
            this.label8.Text = "ÉÏ±ß¾à£º";
            // 
            // tbMarginRight
            // 
            this.tbMarginRight.Location = new System.Drawing.Point(221, 16);
            this.tbMarginRight.Name = "tbMarginRight";
            this.tbMarginRight.Size = new System.Drawing.Size(72, 21);
            this.tbMarginRight.TabIndex = 1;
            this.tbMarginRight.Tag = "Right Margin";
            this.tbMarginRight.Validating += new System.ComponentModel.CancelEventHandler(this.tbSize_Validating);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(165, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "ÓÒ±ß¾à£º";
            // 
            // tbMarginLeft
            // 
            this.tbMarginLeft.Location = new System.Drawing.Point(64, 16);
            this.tbMarginLeft.Name = "tbMarginLeft";
            this.tbMarginLeft.Size = new System.Drawing.Size(72, 21);
            this.tbMarginLeft.TabIndex = 0;
            this.tbMarginLeft.Tag = "Left Margin";
            this.tbMarginLeft.Validating += new System.ComponentModel.CancelEventHandler(this.tbSize_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "×ó±ß¾à£º";
            // 
            // tbWidth
            // 
            this.tbWidth.Location = new System.Drawing.Point(307, 56);
            this.tbWidth.Name = "tbWidth";
            this.tbWidth.Size = new System.Drawing.Size(93, 21);
            this.tbWidth.TabIndex = 2;
            this.tbWidth.Tag = "Width";
            this.tbWidth.Visible = false;
            this.tbWidth.Validating += new System.ComponentModel.CancelEventHandler(this.tbSize_Validating);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(260, 60);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 15;
            this.label9.Text = "¿í¶È£º";
            this.label9.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkPHLast);
            this.groupBox3.Controls.Add(this.chkPHFirst);
            this.groupBox3.Location = new System.Drawing.Point(16, 216);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(184, 56);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ò³Ã¼";
            // 
            // chkPHLast
            // 
            this.chkPHLast.AutoSize = true;
            this.chkPHLast.Location = new System.Drawing.Point(97, 26);
            this.chkPHLast.Name = "chkPHLast";
            this.chkPHLast.Size = new System.Drawing.Size(72, 16);
            this.chkPHLast.TabIndex = 2;
            this.chkPHLast.Text = "Ä©Ò³°üº¬";
            // 
            // chkPHFirst
            // 
            this.chkPHFirst.AutoSize = true;
            this.chkPHFirst.Location = new System.Drawing.Point(9, 26);
            this.chkPHFirst.Name = "chkPHFirst";
            this.chkPHFirst.Size = new System.Drawing.Size(72, 16);
            this.chkPHFirst.TabIndex = 1;
            this.chkPHFirst.Text = "Ê×Ò³°üº¬";
            // 
            // Ò³½Å
            // 
            this.Ò³½Å.Controls.Add(this.chkPFLast);
            this.Ò³½Å.Controls.Add(this.chkPFFirst);
            this.Ò³½Å.Location = new System.Drawing.Point(216, 216);
            this.Ò³½Å.Name = "Ò³½Å";
            this.Ò³½Å.Size = new System.Drawing.Size(202, 56);
            this.Ò³½Å.TabIndex = 6;
            this.Ò³½Å.TabStop = false;
            this.Ò³½Å.Text = "Ò³½Å";
            // 
            // chkPFLast
            // 
            this.chkPFLast.AutoSize = true;
            this.chkPFLast.Location = new System.Drawing.Point(100, 26);
            this.chkPFLast.Name = "chkPFLast";
            this.chkPFLast.Size = new System.Drawing.Size(72, 16);
            this.chkPFLast.TabIndex = 1;
            this.chkPFLast.Text = "Ä©Ò³°üº¬";
            // 
            // chkPFFirst
            // 
            this.chkPFFirst.AutoSize = true;
            this.chkPFFirst.Location = new System.Drawing.Point(14, 26);
            this.chkPFFirst.Name = "chkPFFirst";
            this.chkPFFirst.Size = new System.Drawing.Size(72, 16);
            this.chkPFFirst.TabIndex = 0;
            this.chkPFFirst.Text = "Ê×Ò³°üº¬";
            // 
            // ReportCtl
            // 
            this.Controls.Add(this.tbReportDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Ò³½Å);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.tbWidth);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbReportAuthor);
            this.Controls.Add(this.label3);
            this.Name = "ReportCtl";
            this.Size = new System.Drawing.Size(432, 288);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.Ò³½Å.ResumeLayout(false);
            this.Ò³½Å.PerformLayout();
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
			XmlNode rNode = _Draw.GetReportNode();
			_Draw.SetElement(rNode, "Width", DesignerUtility.MakeValidSize(tbWidth.Text, false));
			_Draw.SetElement(rNode, "Author", tbReportAuthor.Text);
			_Draw.SetElement(rNode, "Description", tbReportDescription.Text);

            if ((this.cbxPages.SelectedIndex < this.cbxPages.Items.Count - 1) &(this.cbxXY.SelectedIndex == 1))
            {
                _Draw.SetElement(rNode, "PageWidth", tbPageHeight .Text);
                _Draw.SetElement(rNode, "PageHeight", tbPageWidth.Text);
            }
            else
            {
                _Draw.SetElement(rNode, "PageWidth", tbPageWidth.Text);
                _Draw.SetElement(rNode, "PageHeight", tbPageHeight.Text);
            }

            if (tbMarginLeft.Text.Trim().Length > 0)
                _Draw.SetElement(rNode, "LeftMargin", tbMarginLeft.Text);
            else
                _Draw.RemoveElement(rNode, "LeftMargin");
            if (tbMarginRight.Text.Trim().Length > 0)
                _Draw.SetElement(rNode, "RightMargin", tbMarginRight.Text);
            else
                _Draw.RemoveElement(rNode, "RightMargin");
            if (tbMarginBottom.Text.Trim().Length > 0)
                _Draw.SetElement(rNode, "BottomMargin", tbMarginBottom.Text);
            else
                _Draw.RemoveElement(rNode, "BottomMargin");
            if (tbMarginTop.Text.Trim().Length > 0)
                _Draw.SetElement(rNode, "TopMargin", tbMarginTop.Text);
            else
                _Draw.RemoveElement(rNode, "TopMargin");

			// Page header settings
			XmlNode phNode = _Draw.GetCreateNamedChildNode(rNode, "PageHeader");
			_Draw.SetElement(phNode, "PrintOnFirstPage", this.chkPHFirst.Checked?"true":"false");
			_Draw.SetElement(phNode, "PrintOnLastPage", this.chkPHLast.Checked?"true":"false");
			// Page footer settings
			XmlNode pfNode = _Draw.GetCreateNamedChildNode(rNode, "PageFooter");
			_Draw.SetElement(pfNode, "PrintOnFirstPage", this.chkPFFirst.Checked?"true":"false");
			_Draw.SetElement(pfNode, "PrintOnLastPage", this.chkPFLast.Checked?"true":"false");
		}

		private void tbSize_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
            TextBox tb = sender as TextBox;
            if (tb == null)
                return;

            try { DesignerUtility.ValidateSize(tb.Text, false, false); }
            catch (Exception ex)
            {
                e.Cancel = true;
                MessageBox.Show(string.Format("Size value of {0} is invalid.\r\n", tb.Text, ex.Message), tb.Tag + " Field Invalid");
            }
		}

        void cbxPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            PrintPage page = CacheHelper.Instance.Pages[this.cbxPages.SelectedIndex];

            if (this.cbxPages.SelectedIndex != this.cbxPages.Items.Count - 1)
            {
                this.tbPageWidth.Text = page.WidthString;
                this.tbPageHeight.Text = page.HeightString;

                this.tbPageWidth.Enabled = this.tbPageHeight.Enabled = false;
                this.cbxXY.Enabled = true;
                this.cbxXY.SelectedIndex = 0;
            }
            else
            {
                this.tbPageWidth.Enabled = this.tbPageHeight.Enabled = true;
                this.tbPageWidth.Text = string.Empty;
                this.tbPageHeight.Text = string.Empty;

                this.cbxXY.Enabled = false;
            }
        }        
	}
}
