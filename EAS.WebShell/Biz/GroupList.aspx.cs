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
    [Module("BCD43D01-045D-468D-9733-2B330EAAC28C", "程序导航", "管理系统之中的导航、程序组信息，对程序模块进行按功能进行分组组织")]
    public partial class GroupList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnNewGroup.OnClientClick = Window1.GetShowReference("~/Biz/GroupWindow.aspx", "新建导航");
                this.btnRemoves.OnClientClick = this.Grid2.GetNoSelectionAlertReference("请先至少选中一个要移除的模块！");
                this.InitializeGroupTree();
            }
            else
            {
                string eventArgument = Request.Form["__EVENTARGUMENT"];
                if (eventArgument.StartsWith("DoAddModules$"))
                {
                    string parm = eventArgument.Substring("DoAddModules$".Length);
                    this.AddModules(parm);
                }
            }
        }

        #region 初始化程序分组。

        private void InitializeGroupTree()
        {
            this.btnAddModules.Enabled = this.btnRemoves.Enabled = this.btnQuery.Enabled = false;
            this.btnModify.Enabled = false;

            this.treeGroup.Nodes.Clear();
            using (DbEntities db =new DbEntities())
            {
                var vList = db.NavigateGroups.ToList();
                
                //根节点
                FineUI.TreeNode rootNode = new FineUI.TreeNode();
                //rootNode.NodeID = Guid.Empty.ToString();
                rootNode.Text = "AgileEAS.NET SOA";
                rootNode.EnableClickEvent = false;
                rootNode.EnableCheckBox = false;
                this.treeGroup.Nodes.Add(rootNode);

                //Windows
                var node = new FineUI.TreeNode();
                //node.NodeID = Guid.NewGuid().ToString();
                node.Text = "Windows";
                node.EnableClickEvent = false;
                node.EnableCheckBox = false;
                this.ResolveGroupTree(vList, Guid.Empty.ToString(), 0x0004, node.Nodes);
                rootNode.Nodes.Add(node);

                //Web
                node = new FineUI.TreeNode();
                //node.NodeID = Guid.NewGuid().ToString();
                node.Text = "Web";
                node.EnableClickEvent = false;
                node.EnableCheckBox = false;
                this.ResolveGroupTree(vList, Guid.Empty.ToString(), 0x0008, node.Nodes);
                rootNode.Nodes.Add(node);

                //Silverlight
                node = new FineUI.TreeNode();
                //node.NodeID = Guid.NewGuid().ToString();
                node.Text = "Silverlight";
                node.EnableClickEvent = false;
                node.EnableCheckBox = false;
                this.ResolveGroupTree(vList, Guid.Empty.ToString(), 0x0010, node.Nodes);
                rootNode.Nodes.Add(node);
                
                // 展开第一个树节点
                this.treeGroup.Nodes[0].Expanded = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataList"></param>
        /// <param name="parentID"></param>
        /// <param name="attributes"></param>
        /// <param name="nodes"></param>
        void ResolveGroupTree(List<NavigateGroup> dataList, string parentID,int attributes, FineUI.TreeNodeCollection nodes)
        {
            List<NavigateGroup> vList = dataList.Where(p => (p.Attributes & attributes) == attributes).ToList();
            if (string.IsNullOrEmpty(parentID) || parentID == Guid.Empty.ToString())
                vList = vList.Where(p => p.ParentID == parentID || p.ParentID == Guid.Empty.ToString()).ToList();
            else
                vList = vList.Where(p => p.ParentID == parentID).ToList();

            foreach (var vItem in vList)
            {
                FineUI.TreeNode node = new FineUI.TreeNode();
                node.NodeID = vItem.ID;
                node.Text = vItem.Name;
                node.EnableClickEvent = true;
                node.EnableCheckBox = false;
                nodes.Add(node);
                this.ResolveGroupTree(dataList, vItem.ID,attributes,node.Nodes);
            }
        }

        #endregion
 
        #region BindGrid
 
        private void BindGrid()
        {
            if (string.IsNullOrEmpty(this.treeGroup.SelectedNodeID)) return;

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
            string groupID = this.treeGroup.SelectedNodeID;
            using (var db = new DbEntities())
            {
                var v = from c in db.ModuleGroups
                        from d in db.Modules.Where(x => x.Guid == c.ObjectID).DefaultIfEmpty()
                        where c.GroupID == groupID
                        select new Module
                        {
                            Guid = c.ObjectID,
                        };                
                return v.Count();
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

            string groupID = this.treeGroup.SelectedNodeID;

            using (var db = new DbEntities())
            {
                var v = from c in db.ModuleGroups
                        from d in db.Modules.Where(x => x.Guid == c.ObjectID).DefaultIfEmpty()
                        where c.GroupID == groupID
                        select new Module
                        {
                            Guid = c.ObjectID,
                            Name = d.Name,
                            Assembly = d.Assembly,
                            Type = d.Type,
                            Version = d.Version,
                            Developer = d.Developer,
                            SortCode = d.SortCode,
                            Description = d.Description
                        };

                if (!string.IsNullOrEmpty(sortField))
                    v = v.DynamicSorting(sortField, sortDirection);

                var vList = v.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                return vList;
            }
        }
 
        #endregion

        /// <summary>
        /// 添加模块。
        /// </summary>
        /// <param name="parm"></param>
        void AddModules(string parm)
        {
            string vGroupID = this.treeGroup.SelectedNodeID;
            if (string.IsNullOrEmpty(vGroupID)) return;

            var vList = parm.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Where(p => !string.IsNullOrEmpty(p)).ToList();
            var vService = ServiceContainer.GetService<IGroupService>();
            vService.AddMember(new NavigateGroup { ID = vGroupID }, vList);
            this.BindGrid();
        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            this.InitializeGroupTree();
        }

        protected void treeGroup_Selected(object sender, FineUI.TreeCommandEventArgs e)
        {
            var vItem = NavigateGroup.Lazy(p => p.ID == this.treeGroup.SelectedNodeID);
            if (vItem != null)
            {
                this.btnAddModules.Enabled = this.btnRemoves.Enabled = this.btnQuery.Enabled = true;
                this.btnModify.Enabled = true;
                int type = 0;
                if ((vItem.Attributes & 0x0004) == 0x0004)
                    type = (int)EAS.Explorer.GoComType.WinUI;
                else if ((vItem.Attributes & 0x0008) == 0x0008)
                    type = (int)EAS.Explorer.GoComType.WebUI;
                else if ((vItem.Attributes & 0x0010) == 0x0010)
                    type = (int)EAS.Explorer.GoComType.SilverUI;
                this.btnAddModules.OnClientClick = Window2.GetShowReference(string.Format("~/Biz/ModuleSelector.aspx?type={0}&mask=3", type), "选择模块");
                this.btnModify.OnClientClick = Window1.GetShowReference(string.Format("~/Biz/GroupWindow.aspx?id={0}", vItem.ID), "修改导航");
                BindGrid();
            }
        }

        protected void Window1_Close(object sender, FineUI.WindowCloseEventArgs e)
        {
            this.InitializeGroupTree();
        }

        protected void btnDelte_Click(object sender, EventArgs e)
        {
            string selectedId = this.treeGroup.SelectedNodeID;
            if (String.IsNullOrEmpty(selectedId))
            {
                Alert.Show("请先选择要删除的导航!", "提示", MessageBoxIcon.Information);
                return;
            }

            try
            {
                var vService = ServiceContainer.GetService<IGroupService>();
                vService.DeleteGroup(new NavigateGroup { ID = selectedId });
            }
            catch (System.Exception exc)
            {
                Alert.Show(exc.Message, String.Empty, MessageBoxIcon.Error);
                return;
            }

            this.InitializeGroupTree();
        }
            
        protected void btnModify_Click(object sender, EventArgs e)
        {
            string selectedId = this.treeGroup.SelectedNodeID;
            if (String.IsNullOrEmpty(selectedId))
            {
                Alert.Show("请先选择要编辑的导航!", "提示", MessageBoxIcon.Information);
                return;
            }
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

        protected void btnRemoves_Click(object sender, EventArgs e)
        {
            string groupId = this.treeGroup.SelectedNodeID;
            if (String.IsNullOrEmpty(groupId)) return;

            var array = this.Grid2.SelectedRowIndexArray;

            List<string> modules = new List<string>();
            foreach (var rowIndex in array)
            {
                modules.Add(this.Grid2.DataKeys[rowIndex][0].ToString());
            }

            try
            {
                var vService = ServiceContainer.GetService<IGroupService>();
                vService.RemoveMember(new NavigateGroup { ID = groupId },modules);
            }
            catch (System.Exception exc)
            {
                Alert.Show(exc.Message, String.Empty, MessageBoxIcon.Error);
                return;
            }

            this.BindGrid();
        }
    }
}