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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using System.Text;
using System.Reflection;
using fyiReporting.RDL;

namespace fyiReporting.RdlDesign
{
	/// <summary>
	/// DialogFilterOperator: puts up a dialog that lets a user pick a Filter Operator
	/// </summary>
	class DialogFilterOperator : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button bOK;
		private System.Windows.Forms.Button bCancel;
		private System.Windows.Forms.Label lOp;
		private System.Windows.Forms.ComboBox cbOperator;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		internal DialogFilterOperator(string op)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if (op != null && op.Length > 0)
				cbOperator.Text = op;

			return;
		}

		public string Operator
		{
			get	{return this.cbOperator.Text; }
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
            this.lOp = new System.Windows.Forms.Label();
            this.cbOperator = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOK.Location = new System.Drawing.Point(103, 178);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(90, 25);
            this.bOK.TabIndex = 3;
            this.bOK.Text = "确定(&O)";
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.CausesValidation = false;
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(208, 178);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(90, 25);
            this.bCancel.TabIndex = 4;
            this.bCancel.Text = "取消(&C)";
            // 
            // lOp
            // 
            this.lOp.AutoSize = true;
            this.lOp.Location = new System.Drawing.Point(10, 11);
            this.lOp.Name = "lOp";
            this.lOp.Size = new System.Drawing.Size(89, 12);
            this.lOp.TabIndex = 13;
            this.lOp.Text = "选择条件操作：";
            // 
            // cbOperator
            // 
            this.cbOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cbOperator.Items.AddRange(new object[] {
            "Equal",
            "Like",
            "NotEqual",
            "GreaterThan",
            "GreaterThanOrEqual",
            "LessThan",
            "LessThanOrEqual",
            "TopN",
            "BottomN",
            "TopPercent",
            "BottomPercent",
            "In",
            "Between"});
            this.cbOperator.Location = new System.Drawing.Point(105, 9);
            this.cbOperator.Name = "cbOperator";
            this.cbOperator.Size = new System.Drawing.Size(193, 161);
            this.cbOperator.TabIndex = 14;
            this.cbOperator.Text = "Equal";
            this.cbOperator.Validating += new System.ComponentModel.CancelEventHandler(this.DialogFilterOperator_Validating);
            // 
            // DialogFilterOperator
            // 
            this.AcceptButton = this.bOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(304, 210);
            this.Controls.Add(this.cbOperator);
            this.Controls.Add(this.lOp);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogFilterOperator";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择条件拾取器";
            this.Validating += new System.ComponentModel.CancelEventHandler(this.DialogFilterOperator_Validating);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void DialogFilterOperator_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			foreach (string op in cbOperator.Items)
			{
				if (op == cbOperator.Text)
					return;
			}
			MessageBox.Show(string.Format("Operator '{0}' must be in the operator list", cbOperator.Text), "Pick Filter Operator");
			e.Cancel = true;
		}

	}

}
