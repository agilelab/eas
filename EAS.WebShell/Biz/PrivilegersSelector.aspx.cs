using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EAS.Data.Linq;
using EAS.Explorer.Entities;
using FineUI;
using System.Text;

namespace EAS.WebShell.Biz
{
    public partial class PrivilegersSelector : System.Web.UI.Page
    {
        #region class PrivilegerItem

        class PrivilegerItem
        {
            public string Code
            {
                get;
                set;
            }

            public string Name
            {
                get;
                set;
            }

            public int PType
            {
                get;
                set;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnOK.OnClientClick = this.Grid2.GetNoSelectionAlertInTopReference("请先选中至少一项目！");
                int mask = 3;
                if (Request.QueryString.AllKeys.Contains("mask"))
                {
                    string xText = Request.QueryString["mask"];
                    int.TryParse(xText, out mask);
                }

                this.cbAccount.Enabled = (mask & 1) == 1;
                if (this.cbAccount.Enabled)
                    this.cbAccount.Checked = true;
                this.cbRole.Enabled = (mask & 2) == 2;
                if (this.cbRole.Enabled)
                    this.cbRole.Checked = true;

                this.LoadData();
            }
        }       

        private void LoadData()
        {
            string key = this.tbKey.Text.Trim();

            List<PrivilegerItem> vList = new List<PrivilegerItem>();
            using (DbEntities db = new DbEntities())
            {
                if (this.cbRole.Checked)
                {
                    var v1 = db.Roles
                        .Where(p => p.Name.StartsWith(key))
                        .Select(p=>new PrivilegerItem{Code=p.Name,Name=p.Name,PType=2})
                        .ToList();
                    vList.AddRange(v1);
                }

                if (this.cbAccount.Checked)
                {
                    var v1 = db.Accounts
                        .Where(p => p.Name.StartsWith(key) || p.LoginID.StartsWith(key))
                        .Select(p => new PrivilegerItem { Code = p.LoginID, Name = p.Name, PType = 1 })
                        .ToList();
                    vList.AddRange(v1);
                }            
            }

            this.Grid2.DataSource = vList;
            this.Grid2.DataBind();
        }

        protected string GetPTypeText(object pType)
        {
            return pType.ToString() == "1" ? "账号" : "角色";
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            int[] selections = this.Grid2.SelectedRowIndexArray;
            foreach (int rowIndex in selections)
            {
                if (sb.Length > 0)
                    sb.Append(";");

                string Text= string.Format("{0}|{1}",this.Grid2.DataKeys[rowIndex][0],this.Grid2.DataKeys[rowIndex][1]);
                sb.Append(Text);
            }

            //这里是调用ModuleWindow里面的JS
            PageContext.RegisterStartupScript(ActiveWindow.GetHideExecuteScriptReference("DoSelectPrivilegers('" + sb.ToString() + "');"));
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(ActiveWindow.GetHideReference());
        }
    }
}