using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;
using EAS.Explorer.Entities;
using EAS.Data.Linq;
using EAS.Modularization;
using EAS.Explorer.BLL;
using EAS.Services;

namespace EAS.WebShell.Biz
{
    [Module("25173D6B-E917-4286-8B34-CC6CDC67A5BE", "角色管理", "提供系统中角色的管理功能")]
    public partial class RoleList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnNew.OnClientClick = Window1.GetShowReference("~/Biz/RoleWindow.aspx", "新建角色");
                this.BindGrid();
            }
 
            Panel7.Title = "表格 - 页面加载时间：" + DateTime.Now.ToLongTimeString();
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
            using (var db = new DbEntities())
            {
                return db.Roles
                        .Where(p => p.Name.StartsWith(key) || p.Description.StartsWith(key)).Count();
            }
        }

        /// <summary>
        /// 取当前页的数据。
        /// </summary>
        /// <returns></returns>
        private List<Role> GetPagedData()
        {
            int pageIndex = this.Grid2.PageIndex;
            int pageSize = this.Grid2.PageSize;

            string sortField = this.Grid2.SortField;
            string sortDirection = this.Grid2.SortDirection;

            string key = this.tbKey.Text.Trim();
            using (var db = new DbEntities())
            {
                var v = db.Roles
                        .Where(p => p.Name.StartsWith(key) || p.Description.StartsWith(key));
                if(!string.IsNullOrEmpty(sortField))
                    v = v.DynamicSorting(sortField, sortDirection);

                var vList = v.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                return vList;
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
                    IRoleService service = ServiceContainer.GetService<IRoleService>();
                    service.DeleteRole(new Role { Name = keys[0].ToString() });
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