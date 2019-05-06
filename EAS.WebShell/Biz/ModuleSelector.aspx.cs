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
    public partial class ModuleSelector : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnOK.OnClientClick = this.Grid2.GetNoSelectionAlertInTopReference("请至少选中一个模块！");
                //掩码。
                int mask = 7;
                if (Request.QueryString.AllKeys.Contains("mask"))
                {
                    string xText = Request.QueryString["mask"];
                    int.TryParse(xText, out mask);
                }

                //筛选类型。
                int type = int.MaxValue;
                if (Request.QueryString.AllKeys.Contains("type"))
                {
                    string xText = Request.QueryString["type"];
                    int.TryParse(xText, out type);
                }
                this.hfType.Value = type.ToString();

                this.cbModule.Enabled = (mask & 1) == 1;
                if (this.cbModule.Enabled)
                    this.cbModule.Checked = true;
                this.cbReport.Enabled = (mask & 2) == 2;
                if (this.cbReport.Enabled)
                    this.cbReport.Checked = true;

                this.cbBiz.Enabled = (mask & 4) == 4;
                if (this.cbBiz.Enabled)
                    this.cbBiz.Checked = true;

                this.LoadData();
            }
        }
        
        private void LoadData()
        {
            string key = this.tbKey.Text.Trim();
            int type = this.hfType.Value.ToInt();

            using (DbEntities db = new DbEntities())
            {
                var v = db.Modules.Where(p => p.Name.StartsWith(key) || p.Type.StartsWith(key) || p.Assembly.StartsWith(key))
                    .Select(p => new
                                    Module
                                                {
                                                    Guid = p.Guid,
                                                    Name = p.Name,
                                                    Assembly = p.Assembly,
                                                    Type = p.Type,
                                                    Method =p.Method,
                                                    Description = p.Description,
                                                    Attributes = p.Attributes,
                                                    Version = p.Version,
                                                    Developer = p.Developer,
                                                    SortCode = p.SortCode,
                                                    Url=p.Url,
                                                    Package = p.Package,
                                                    LMTime = p.LMTime
                                                });


                string sortField = this.Grid2.SortField;
                string sortDirection = this.Grid2.SortDirection;

                if (!string.IsNullOrEmpty(sortField))
                    v = v.DynamicSorting(sortField, sortDirection);
                var xList = v.ToList();

                var vList = new List<Module>();
                if (this.cbModule.Checked)
                {
                    if ((type & (int)EAS.Explorer.GoComType.WinUI) == (int)EAS.Explorer.GoComType.WinUI)
                    {
                        var v2 = xList.Where(p => (p.Attributes & (int)EAS.Explorer.GoComType.WinUI) == (int)EAS.Explorer.GoComType.WinUI).ToList();
                        vList.AddRange(v2);
                    }

                    if ((type & (int)EAS.Explorer.GoComType.WebUI) == (int)EAS.Explorer.GoComType.WebUI)
                    {
                        var v2 = xList.Where(p => (p.Attributes & (int)EAS.Explorer.GoComType.WebUI) == (int)EAS.Explorer.GoComType.WebUI).ToList();
                        vList.AddRange(v2);
                    }

                    if ((type & (int)EAS.Explorer.GoComType.SilverUI) == (int)EAS.Explorer.GoComType.SilverUI)
                    {
                        var v2 = xList.Where(p => (p.Attributes & (int)EAS.Explorer.GoComType.SilverUI) == (int)EAS.Explorer.GoComType.SilverUI).ToList();
                        vList.AddRange(v2);
                    }
                }

                if (this.cbReport.Checked)
                {
                    var v2 = xList.Where(p => (p.Attributes & (int)EAS.Explorer.GoComType.Report) == (int)EAS.Explorer.GoComType.Report).ToList();
                    vList.AddRange(v2);
                }

                if (this.cbBiz.Checked)
                {
                    var v2 = xList.Where(p => (p.Attributes & (int)EAS.Explorer.GoComType.Business) == (int)EAS.Explorer.GoComType.Business
                        || (p.Attributes & (int)EAS.Explorer.GoComType.Function) == (int)EAS.Explorer.GoComType.Business).ToList();
                    vList.AddRange(v2);
                }

                this.Grid2.DataSource = vList;
                this.Grid2.DataBind();
            }
        }

        protected void Grid2_Sort(object sender, FineUI.GridSortEventArgs e)
        {
            this.Grid2.SortDirection = e.SortDirection;
            this.Grid2.SortField = e.SortField;
            this.LoadData();
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
                sb.Append(this.Grid2.DataKeys[rowIndex][0].ToString());
            }

            //这里是调用ModuleWindow里面的JS
            PageContext.RegisterStartupScript(ActiveWindow.GetHideExecuteScriptReference("DoSelectModules('" + sb.ToString() + "');"));
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(ActiveWindow.GetHideReference());
        }
    }
}