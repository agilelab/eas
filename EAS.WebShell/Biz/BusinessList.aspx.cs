using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;
using EAS.Explorer.Entities;
using EAS.Data.Linq;
using EAS.Explorer;
using EAS.Explorer.BLL;
using EAS.Services;

namespace EAS.WebShell.Biz
{
    [EAS.Modularization.Module("6BC9A4EE-3DEF-4062-8FA1-9441EA2973BD", "业务管理", "管理系统之之中所有业务计算构件")]
    public partial class BusinessList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnInstall.OnClientClick = Window1.GetShowReference("~/Biz/BusinessInstaller.aspx", "业务构件安装");
                this.BindGrid();
            }
        }
 
        #region BindGrid
 
        private void BindGrid()
        {
            // 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
            this.Grid2.RecordCount = this.GetTotalCount();
            // 2.获取当前分页数据
            this.Grid2.DataSource = this.GetPagedData();
            this.Grid2.DataBind();
        }

        /// <summary>
        /// 模拟返回总项数
        /// </summary>
        /// <returns></returns>
        private int GetTotalCount()
        {
            string key = string.Empty;
            int type = (int)GoComType.Business;
            using (var db = new DbEntities())
            {
                var vList = db.Modules
                        .Where(p => p.Name.StartsWith(key) || p.Type.StartsWith(key) || p.Assembly.StartsWith(key))
                        .Select(p => p.Attributes).ToList();
                return vList.Where(p => (p & type) == type).Count();
            }
        }

        /// <summary>
        /// 取当前页的数据。
        /// </summary>
        /// <returns></returns>
        private List<Module> GetPagedData()
        {
            int pageIndex = this.Grid2.PageIndex;
            int pageSize = this.Grid2.PageSize;

            string sortField = this.Grid2.SortField;
            string sortDirection = this.Grid2.SortDirection;

            string key = this.tbKey.Text.Trim();
            int type = (int)GoComType.Business;

            using (var db = new DbEntities())
            {
                var vList = db.Modules
                        .Where(p => p.Name.StartsWith(key) || p.Type.StartsWith(key) || p.Assembly.StartsWith(key))
                        .Select(p => new
                                    Module
                                                {
                                                    Guid = p.Guid,
                                                    Name = p.Name,
                                                    Assembly = p.Assembly,
                                                    Type = p.Type,
                                                    Method = p.Method,
                                                    Description = p.Description,
                                                    Attributes = p.Attributes,
                                                    Version = p.Version,
                                                    Developer = p.Developer,
                                                    SortCode = p.SortCode,
                                                    Url = p.Url,
                                                    Package = p.Package,
                                                    LMTime = p.LMTime
                                                })
                    .ToList();

                var v = vList.AsQueryable().Where(p => (p.Attributes & type) == type);
                if(!string.IsNullOrEmpty(sortField))
                    v = v.DynamicSorting(sortField, sortDirection);
                var vList2 = v.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                return vList2;
            }
        }
 
        #endregion

        protected void Grid2_Sort(object sender, FineUI.GridSortEventArgs e)
        {
            this.Grid2.SortDirection = e.SortDirection;
            this.Grid2.SortField = e.SortField;
            this.BindGrid();
        }
 
        protected void Grid2_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            this.Grid2.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }
 
        protected void Window1_Close(object sender, FineUI.WindowCloseEventArgs e)
        {
            this.BindGrid();
        }

        protected void Grid2_RowCommand(object sender, GridCommandEventArgs e)
        {
            var keys = this.Grid2.DataKeys[e.RowIndex];

            if (e.CommandName == "Delete")
            {
                try
                {
                    IModuleService service = ServiceContainer.GetService<IModuleService>();
                    service.UnstallModule(new Module { Guid = keys[0].ToString() });
                }
                catch (System.Exception exc)
                {
                    Alert.Show(exc.Message, String.Empty, MessageBoxIcon.Error);
                    return;
                }
                this.BindGrid();
            }
        }        

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            this.BindGrid();
        }
    }
}