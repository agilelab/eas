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
	/// DialogListOfStrings: puts up a dialog that lets a user enter a list of strings
	/// </summary>
	class DialogExprEditor : System.Windows.Forms.Form
	{
		Type[] BASE_TYPES = new Type[] {System.String.Empty.GetType(),
												  System.Double.MinValue.GetType(),
												  System.Single.MinValue.GetType(),
												  System.Decimal.MinValue.GetType(),
												  System.DateTime.MinValue.GetType(),
												  System.Char.MinValue.GetType(),
												  new bool().GetType(),
												  System.Int32.MinValue.GetType(),
												  System.Int16.MinValue.GetType(),
												  System.Int64.MinValue.GetType(),
												  System.Byte.MinValue.GetType(),
												  System.UInt16.MinValue.GetType(),
												  System.UInt32.MinValue.GetType(),
												  System.UInt64.MinValue.GetType()};
		private DesignXmlDraw _Draw;		// design draw 
		private bool _Color;				// true if color list should be displayed

		private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bCancel;
		private System.Windows.Forms.TreeView tvOp;
		private System.Windows.Forms.Label lExpr;
		private System.Windows.Forms.Label lOp;
        private Panel panel1;
        private Panel panel2;
        private Panel panel4;
        private Panel panel3;
        private Panel panOpr;
        private Panel panel6;
        private Panel panel7;
        private Panel panel8;
        private Button btnCopy;
        private RichTextBox tbExpr;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		internal DialogExprEditor(DesignXmlDraw dxDraw, string expr, XmlNode node) : 
			this(dxDraw, expr, node, false)
		{
		}

		internal DialogExprEditor(DesignXmlDraw dxDraw, string expr, XmlNode node, bool bColor)
		{
			_Draw = dxDraw;
			_Color = bColor;
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			tbExpr.Text = expr;

			// Fill out the fields list 
			string[] fields = null;
			// Find the dataregion that contains the item (if any)
			for (XmlNode pNode = node; pNode != null; pNode = pNode.ParentNode)
			{
				if (pNode.Name == "List" ||
					pNode.Name == "Table" ||
					pNode.Name == "Matrix" ||
					pNode.Name == "Chart")
				{
					string dsname = _Draw.GetDataSetNameValue(pNode);
					if (dsname != null)	// found it
					{
						fields = _Draw.GetFields(dsname, true);
					}
				}
			}
			BuildTree(fields);

            this.btnCopy.Top = (this.panOpr.Height - this.btnCopy.Height) / 2;

			return;
		}

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            this.btnCopy.Top = (this.panOpr.Height - this.btnCopy.Height) / 2;
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            this.btnCopy.Top = (this.panOpr.Height - this.btnCopy.Height) / 2;
        }

		void BuildTree(string[] flds)
		{
			// suppress redraw until tree view is complete
			tvOp.BeginUpdate();

			// Handle the globals
			//TreeNode ndRoot = new TreeNode("Globals");
            TreeNode ndRoot = new TreeNode("全局定义");
			tvOp.Nodes.Add(ndRoot);
			foreach (string item in StaticLists.GlobalList)
			{
				// Add the node to the tree
				TreeNode aRoot = new TreeNode(item.StartsWith("=")? item.Substring(1): item);
				ndRoot.Nodes.Add(aRoot);
			}

			// Fields - only when a dataset is specified
			if (flds != null && flds.Length > 0)
			{
				//ndRoot = new TreeNode("Fields");
                ndRoot = new TreeNode("数据字段");
				tvOp.Nodes.Add(ndRoot);

				foreach (string f in flds)
				{	
					TreeNode aRoot = new TreeNode(f.StartsWith("=")? f.Substring(1): f);
					ndRoot.Nodes.Add(aRoot);
				}
			}

			// Report parameters
			InitReportParameters();

			// Handle the functions
			//ndRoot = new TreeNode("Functions");
            ndRoot = new TreeNode("数据函数");
			tvOp.Nodes.Add(ndRoot);
			InitFunctions(ndRoot);

			// Aggregate functions
			//ndRoot = new TreeNode("Aggregate Functions");
            ndRoot = new TreeNode("统计函数");
			tvOp.Nodes.Add(ndRoot);
			foreach (string item in StaticLists.AggrFunctionList)
			{
				// Add the node to the tree
				TreeNode aRoot = new TreeNode(item);
				ndRoot.Nodes.Add(aRoot);
			}

			// Operators
			//ndRoot = new TreeNode("Operators");
            ndRoot = new TreeNode("运算符");
			tvOp.Nodes.Add(ndRoot);
			foreach (string item in StaticLists.OperatorList)
			{
				// Add the node to the tree
				TreeNode aRoot = new TreeNode(item);
				ndRoot.Nodes.Add(aRoot);
			}

			// Colors (if requested)
			if (_Color)
			{
				//ndRoot = new TreeNode("Colors");
                ndRoot = new TreeNode("颜色");
				tvOp.Nodes.Add(ndRoot);
				foreach (string item in StaticLists.ColorList)
				{
					// Add the node to the tree
					TreeNode aRoot = new TreeNode(item);
					ndRoot.Nodes.Add(aRoot);
				}
			}


			tvOp.EndUpdate();

		}

		/// <summary>
		/// Populate tree view with the report parameters (if any)
		/// </summary>
		void InitReportParameters()
		{
			string[] ps = _Draw.GetReportParameters(true);
			
			if (ps == null || ps.Length == 0)
				return;

			TreeNode ndRoot = new TreeNode("Parameters");
			tvOp.Nodes.Add(ndRoot);

			foreach (string p in ps)
			{
				TreeNode aRoot = new TreeNode(p.StartsWith("=")?p.Substring(1): p);
				ndRoot.Nodes.Add(aRoot);
			}

			return;
		}

		void InitFunctions(TreeNode ndRoot)
		{
			ArrayList ar = new ArrayList();
			
			ar.AddRange(StaticLists.FunctionList);

			// Build list of methods in the  VBFunctions class
			fyiReporting.RDL.FontStyleEnum fsi = FontStyleEnum.Italic;	// just want a class from RdlEngine.dll assembly
			Assembly a = Assembly.GetAssembly(fsi.GetType());
			if (a == null)
				return;
			Type ft = a.GetType("fyiReporting.RDL.VBFunctions");	 
			BuildMethods(ar, ft, "");

			// build list of financial methods in Financial class
			ft = a.GetType("fyiReporting.RDL.Financial");
			BuildMethods(ar, ft, "Financial.");

			a = Assembly.GetAssembly("".GetType());
			ft = a.GetType("System.Math");
			BuildMethods(ar, ft, "Math.");

			ft = a.GetType("System.Convert");
			BuildMethods(ar, ft, "Convert.");

			ft = a.GetType("System.String");
			BuildMethods(ar, ft, "String.");

			ar.Sort();
			string previous="";
			foreach (string item in ar)
			{
				if (item != previous)	// don't add duplicates
				{
					// Add the node to the tree
					TreeNode aRoot = new TreeNode(item);
					ndRoot.Nodes.Add(aRoot);
				}
				previous = item;
			}

		}

		void BuildMethods(ArrayList ar, Type ft, string prefix)
		{
			if (ft == null)
				return;
			MethodInfo[] mis = ft.GetMethods(BindingFlags.Static | BindingFlags.Public);
			foreach (MethodInfo mi in mis)
			{
				// Add the node to the tree
				string name = BuildMethodName(mi);
				if (name != null)
					ar.Add(prefix + name);
			}
		}

		string BuildMethodName(MethodInfo mi)
		{
			StringBuilder sb = new StringBuilder(mi.Name);
			sb.Append("(");
			ParameterInfo[] pis = mi.GetParameters();
			bool bFirst=true;
			foreach (ParameterInfo pi in pis)
			{
				if (!IsBaseType(pi.ParameterType))
					return null;
				if (bFirst)
					bFirst = false;
				else
					sb.Append(", ");
				sb.Append(pi.Name);
			}
			sb.Append(")");
			return sb.ToString();
		}

		// Determines if underlying type is a primitive
		bool IsBaseType(Type t)
		{
			foreach (Type bt in BASE_TYPES)
			{
				if (bt == t)
					return true;
			}

			return false;
		}

		public string Expression
		{
			get	{return tbExpr.Text; }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogExprEditor));
            this.bOK = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.tvOp = new System.Windows.Forms.TreeView();
            this.lExpr = new System.Windows.Forms.Label();
            this.lOp = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panOpr = new System.Windows.Forms.Panel();
            this.btnCopy = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.tbExpr = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panOpr.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOK.Location = new System.Drawing.Point(473, 11);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(90, 24);
            this.bOK.TabIndex = 0;
            this.bOK.Text = "确定(&O)";
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(578, 11);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(90, 24);
            this.bCancel.TabIndex = 1;
            this.bCancel.Text = "取消(&C)";
            // 
            // tvOp
            // 
            this.tvOp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvOp.Location = new System.Drawing.Point(0, 0);
            this.tvOp.Name = "tvOp";
            this.tvOp.Size = new System.Drawing.Size(196, 376);
            this.tvOp.TabIndex = 0;
            this.tvOp.DoubleClick += new System.EventHandler(this.btnCopy_Click);
            // 
            // lExpr
            // 
            this.lExpr.AutoSize = true;
            this.lExpr.Location = new System.Drawing.Point(8, 7);
            this.lExpr.Name = "lExpr";
            this.lExpr.Size = new System.Drawing.Size(107, 12);
            this.lExpr.TabIndex = 0;
            this.lExpr.Text = "表达式以\'=\'开始：";
            // 
            // lOp
            // 
            this.lOp.AutoSize = true;
            this.lOp.Location = new System.Drawing.Point(10, 7);
            this.lOp.Name = "lOp";
            this.lOp.Size = new System.Drawing.Size(89, 12);
            this.lOp.TabIndex = 0;
            this.lOp.Text = "选择并且添加：";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bCancel);
            this.panel1.Controls.Add(this.bOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 401);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(677, 46);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(196, 401);
            this.panel2.TabIndex = 15;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tvOp);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 25);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(196, 376);
            this.panel4.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lOp);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(196, 25);
            this.panel3.TabIndex = 0;
            // 
            // panOpr
            // 
            this.panOpr.Controls.Add(this.btnCopy);
            this.panOpr.Dock = System.Windows.Forms.DockStyle.Left;
            this.panOpr.Location = new System.Drawing.Point(196, 0);
            this.panOpr.Margin = new System.Windows.Forms.Padding(0);
            this.panOpr.Name = "panOpr";
            this.panOpr.Size = new System.Drawing.Size(65, 401);
            this.panOpr.TabIndex = 1;
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(6, 191);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(50, 24);
            this.btnCopy.TabIndex = 1;
            this.btnCopy.Text = ">>";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.panel7);
            this.panel6.Controls.Add(this.panel8);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(261, 0);
            this.panel6.Margin = new System.Windows.Forms.Padding(0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(416, 401);
            this.panel6.TabIndex = 17;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.tbExpr);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 25);
            this.panel7.Margin = new System.Windows.Forms.Padding(0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(416, 376);
            this.panel7.TabIndex = 1;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.lExpr);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Margin = new System.Windows.Forms.Padding(0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(416, 25);
            this.panel8.TabIndex = 0;
            // 
            // tbExpr
            // 
            this.tbExpr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbExpr.Location = new System.Drawing.Point(0, 0);
            this.tbExpr.Name = "tbExpr";
            this.tbExpr.Size = new System.Drawing.Size(416, 376);
            this.tbExpr.TabIndex = 1;
            this.tbExpr.Text = "";
            // 
            // DialogExprEditor
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(677, 447);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panOpr);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DialogExprEditor";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "编辑表达式";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panOpr.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void btnCopy_Click(object sender, System.EventArgs e)
		{
			if (tvOp.SelectedNode == null ||
				tvOp.SelectedNode.Parent == null)
				return;		// this is the top level nodes (Fields, Parameters, ...)

			TreeNode node = tvOp.SelectedNode;
			string t = node.Text;
			tbExpr.SelectedText = t;
		}		
    }
}
