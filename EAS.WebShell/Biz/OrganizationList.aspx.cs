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
using EAS.Data.ORM;
using EAS.Explorer.BLL;
using EAS.Services;

namespace EAS.WebShell.Biz
{
    [Module("852315ED-0210-488C-A007-6189716FB864", "机构管理", "管理AgileEAS.NET SOA中间件平台之中的组织机构|账号分组信息")]
    public partial class OrganizationList : System.Web.UI.Page
    {
        #region class TreeNode

        internal class TreeGridData : Organization
        {
            /// <summary>
            /// 分组/上级。
            /// </summary>
            public string Group
            {
                get;
                set;
            }

            //树级别。
            public int TreeLevel
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
                this.btnNew.OnClientClick = Window1.GetShowReference("~/Biz/OrganizationWindow.aspx", "新建机构");
                this.BindGrid();
            }
 
            Panel7.Title = "表格 - 页面加载时间：" + DateTime.Now.ToLongTimeString();
        }
 
        #region BindGrid
 
        private void BindGrid()
        {
            this.Grid2.DataSource = GetTreeGridData();
            this.Grid2.DataBind();
        }

        /// <summary>
        /// 提取TreeGridData。
        /// </summary>
        /// <returns></returns>
        internal static List<TreeGridData> GetTreeGridData()
        {
            using (var db = new DbEntities())
            {
                var v = db.Organizations.ToList();
                List<TreeGridData> xList = new List<TreeGridData>();
                foreach (var item in v)
                {
                    TreeGridData node = new TreeGridData();
                    node.Group = string.Empty;
                    node.TreeLevel = 0;
                    item.CopyTo(node);
                    xList.Add(node);
                }

                List<TreeGridData> vList = new List<TreeGridData>();
                ResolveTreeGridData(xList, vList, Guid.Empty.ToString(), 0);
                return vList;
            }
        }

        static void ResolveTreeGridData(List<TreeGridData> inputs, List<TreeGridData> outs, string parentID, int treeLevel)
        {
            List<TreeGridData> vList =null;
            if (string.IsNullOrEmpty(parentID) || parentID == Guid.Empty.ToString())
                vList = inputs.Where(p => p.ParentID == parentID || p.ParentID == Guid.Empty.ToString()).ToList();
            else
                vList = inputs.Where(p => p.ParentID == parentID ).ToList();

            foreach (var item in vList)
            {
                item.TreeLevel = treeLevel;
                item.Group = parentID;
                outs.Add(item);
                ResolveTreeGridData(inputs, outs, item.Guid, treeLevel + 1);
            }
        }
 
        #endregion
 
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
                    service.DeleteOrganization(keys[0].ToString());
                }
                catch(System.Exception exc)
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