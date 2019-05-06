using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;
using EAS.Data.Linq;
using EAS.Data.ORM;
using EAS.Modularization;

namespace EAS.WebShell.Biz
{
    [Module("A7A748A9-55DB-4DCA-8993-E0351501BC14", "TextBuilder", "为开发人员提供字符串生成StringBuilder实例的工具")]
    public partial class TextBuilder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            string text = this.tbText.Text;
            text = text.Replace("\"", "\\\"");
            text = text.Replace("\\", "\\\\");

            if (text.Length > 0)
            {
                string name = this.tbName.Text.Trim().Length > 0 ? this.tbName.Text.Trim() : "textBuilder";

                this.tbText2.Text = "StringBuilder " + name + " = new StringBuilder();\n";

                string[] sv = text.Split('\n');

                for (int i = 0; i < sv.Length; i++)
                {
                    this.tbText2.Text += name + ".Append(" + "\"" + sv[i] + "\\r\\n" + "\"" + ");\n";
                }
            }
        }
    }
}