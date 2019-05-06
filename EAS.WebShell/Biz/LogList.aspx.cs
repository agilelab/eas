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
    [EAS.Modularization.Module("8E6D8B7B-A52C-4BDE-969A-0179AF191002", "操作日志", "查询、浏览操作人员的操作行为")]
    public partial class LogList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.dtpStart.Text = DateTime.Now.Date.AddDays(-7).ToString("yyyy-MM-dd");
                this.dtpEnd.Text = DateTime.Now.Date.AddDays(1).ToString("yyyy-MM-dd");
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

            DateTime startTime = DateTime.Now.Date.AddDays(-7);
            DateTime.TryParse(this.dtpStart.Text, out startTime);

            DateTime endTime = DateTime.Now.Date.AddDays(1);
            DateTime.TryParse(this.dtpEnd.Text, out endTime);

            using (var db = new DbEntities())
            {
                var count = db.Logs
                        .Where(p => p.EventTime >= startTime && p.EventTime <= endTime)
                        .Where(p => p.LoginID.StartsWith(key) || p.HostName.StartsWith(key) || p.IpAddress.StartsWith(key))
                        .Select(p => p.ID).Count();
                return count;
            }
        }

        /// <summary>
        /// 取当前页的数据。
        /// </summary>
        /// <returns></returns>
        private List<Log> GetPagedData()
        {
            int pageIndex = this.Grid2.PageIndex;
            int pageSize = this.Grid2.PageSize;

            string sortField = this.Grid2.SortField;
            string sortDirection = this.Grid2.SortDirection;

            string key = string.Empty;

            DateTime startTime = DateTime.Now.Date.AddDays(-7);
            DateTime.TryParse(this.dtpStart.Text, out startTime);

            DateTime endTime = DateTime.Now.Date.AddDays(1);
            DateTime.TryParse(this.dtpEnd.Text, out endTime);

            using (var db = new DbEntities())
            {
                var v = db.Logs
                        .Where(p => p.EventTime >= startTime && p.EventTime <= endTime)
                        .Where(p => p.LoginID.StartsWith(key) || p.HostName.StartsWith(key) || p.IpAddress.StartsWith(key));

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

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            this.BindGrid();
        }
    }
}