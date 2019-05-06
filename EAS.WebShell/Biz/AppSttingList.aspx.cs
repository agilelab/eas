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

namespace EAS.WebShell.Biz
{
    [Module("359FAB2A-C142-44CC-AEC4-F11A946401F7", "系统参数", "集中管理系统之中的参数")]
    public partial class AppSttingList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnNew.OnClientClick = Window1.GetShowReference("~/Biz/AppSttingWindow.aspx", "新建参数");

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
                return db.AppSettings
                        .Where(p => p.Category.StartsWith(key) || p.Key.StartsWith(key)).Count();
            }
        }

        /// <summary>
        /// 取当前页的数据。
        /// </summary>
        /// <returns></returns>
        private List<AppSetting> GetPagedData()
        {
            int pageIndex = this.Grid2.PageIndex;
            int pageSize = this.Grid2.PageSize;

            string sortField = this.Grid2.SortField;
            string sortDirection = this.Grid2.SortDirection;

            string key = this.tbKey.Text.Trim();
            using (var db = new DbEntities())
            {
                var v = db.AppSettings
                        .Where(p => p.Category.StartsWith(key) || p.Key.StartsWith(key));
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
                using (var db = new DbEntities())
                {
                    db.AppSettings.Delete(p=>p.Category==keys[0].ToString() && p.Key == keys[1].ToString());
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