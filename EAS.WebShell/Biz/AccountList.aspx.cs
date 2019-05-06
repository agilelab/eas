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
    [Module("4AED4445-64B1-420E-AAA9-076555DB58DA", "账号管理", "管理系统之中的组织机构以账号信息")]
    public partial class AccountList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnNew.OnClientClick = Window1.GetShowReference("~/Biz/AccountWindow.aspx", "新建账号");
                this.InitializeOrganTree();
                this.BindGrid();
            }

        }

        #region 初始化组织机构树。

        private void InitializeOrganTree()
        {
            this.treeOrgan.Nodes.Clear();
            using (DbEntities db =new DbEntities())
            {
                var vList = db.Organizations.ToList();
                this.ResolveOrganTree(vList,Guid.Empty.ToString(), this.treeOrgan.Nodes);    // 生成树

                if (treeOrgan.Nodes.Count == 0)
                {
                    Response.Write("组织机构信息尚未初始化！");
                    Response.End();

                    return;
                }
                // 展开第一个树节点
                this.treeOrgan.Nodes[0].Expanded = true;
            }
        }

        void ResolveOrganTree(List<Organization> dataList, string parentID, FineUI.TreeNodeCollection nodes)
        {
            List<Organization> vList = null;
            if (string.IsNullOrEmpty(parentID) || parentID == Guid.Empty.ToString())
                vList = dataList.Where(p => p.ParentID == parentID || p.ParentID == Guid.Empty.ToString()).ToList();
            else
                vList = dataList.Where(p => p.ParentID == parentID).ToList();

            foreach (var vItem in vList)
            {
                FineUI.TreeNode node = new FineUI.TreeNode();
                node.NodeID = vItem.Guid.ToString();
                node.Text = vItem.Name;
                node.EnableClickEvent = true;
                nodes.Add(node);
                this.ResolveOrganTree(dataList,vItem.Guid, node.Nodes);
            }
        }

        #endregion
 
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
            string key = this.tbKey.Text.Trim();
            string organID = this.treeOrgan.SelectedNodeID;
            if (this.rbALL.Checked)
                organID = string.Empty;

            using (var db = new DbEntities())
            {
                var v = db.Accounts.Where(p => p.Name.StartsWith(key) || p.LoginID.StartsWith(key));
                if (!string.IsNullOrEmpty(organID))
                {
                    v = v.Where(p => p.OrganID == organID);
                }
                return v.Count();
            }
        }

        /// <summary>
        /// 取当前页的数据。
        /// </summary>
        /// <returns></returns>
        private List<Account> GetPagedData()
        {
            int pageIndex = this.Grid2.PageIndex;
            int pageSize = this.Grid2.PageSize;

            string sortField = this.Grid2.SortField;
            string sortDirection = this.Grid2.SortDirection;

            string key = this.tbKey.Text.Trim();
            string organID = this.treeOrgan.SelectedNodeID;
            if (this.rbALL.Checked)
            organID = string.Empty;

            using (var db = new DbEntities())
            {
                var v = db.Accounts.Where(p => p.Name.StartsWith(key) || p.LoginID.StartsWith(key));
                if (!string.IsNullOrEmpty(organID))
                {
                    v = v.Where(p => p.OrganID == organID);
                }
                if (!string.IsNullOrEmpty(sortField))
                    v = v.DynamicSorting(sortField, sortDirection);

                var vList = v.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                return vList;
            }
        }
 
        #endregion

        protected void treeOrgan_Selected(object sender, FineUI.TreeCommandEventArgs e)
        {
            BindGrid();
        }

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
                    IAccountService service = ServiceContainer.GetService<IAccountService>();
                    service.Delete(new Account { LoginID = keys[0].ToString() });
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

        protected void rbSlef_CheckedChanged(object sender, CheckedEventArgs e)
        {
            this.BindGrid();
        }

        protected void rbALL_CheckedChanged(object sender, CheckedEventArgs e)
        {
            this.BindGrid();
        }
    }
}