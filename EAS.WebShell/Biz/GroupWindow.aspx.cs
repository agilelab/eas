using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;
using EAS.Explorer.Entities;
using EAS.Data.Linq;
using EAS.Data.ORM;

namespace EAS.WebShell.Biz
{
    public partial class GroupWindow : System.Web.UI.Page
    {
        #region class GroupTreeData

        class GroupTreeData : NavigateGroup
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
                LoadData();
            }
        }

        void LoadData()
        {
            this.btnClose.OnClientClick = ActiveWindow.GetHideReference();

            this.BindDdlParent();

            if (Request.QueryString.AllKeys.Contains("id"))
            {
                string vID = Request.QueryString["id"];

                var vItem = NavigateGroup.Lazy(p => p.ID == vID);
                if (vItem == null)
                {
                    Alert.Show("读取导航信息错误！", String.Empty, ActiveWindow.GetHideReference());
                    return;
                }

                this.lbID.Text = vItem.ID;

                int type = 0;
                if ((vItem.Attributes & 0x0004) == 0x0004)
                    type = 0x0004;
                else if ((vItem.Attributes & 0x0008) == 0x0008)
                    type = 0x0008;
                else if ((vItem.Attributes & 0x0010) == 0x0010)
                    type = 0x0010;

                this.ddlParent.SelectedValue = string.Format("{0}|{1}", type, vItem.ParentID);

                this.tbName.Text = vItem.Name;
                this.nbSortCode.Text = vItem.SortCode.ToString();
                this.tbDescription.Text = vItem.Description;

                this.cbExpend.Checked = (vItem.Attributes & 0x0002) == 0x0002;

                this.btnSave.Enabled = false;
                this.ddlParent.Enabled = false;
            }
            else
            {
                this.lbID.Text = Guid.NewGuid().ToString().ToUpper();
            }
        }

        void BindDdlParent()
        {
            this.ddlParent.EnableSimulateTree = true;
            this.ddlParent.DataTextField = "Name";
            this.ddlParent.DataValueField = "ID";
            this.ddlParent.DataSimulateTreeLevelField = "TreeLevel";
            this.ddlParent.DataSource = GetGroupTreeData();
            this.ddlParent.DataBind();
        }

        #region LoadGroupTreeData

        /// <summary>
        /// 提取TreeGridData。
        /// </summary>
        /// <returns></returns>
        static List<GroupTreeData> GetGroupTreeData()
        {
            using (var db = new DbEntities())
            {
                var v = db.NavigateGroups.ToList();
                List<GroupTreeData> xList = new List<GroupTreeData>();
                foreach (var item in v)
                {
                    GroupTreeData node = new GroupTreeData();
                    node.Group = string.Empty;
                    node.TreeLevel = 0;
                    item.CopyTo(node);

                    if ((node.Attributes & 0x0004) == 0x0004)
                        node.Attributes = 0x0004;
                    else if ((node.Attributes & 0x0008) == 0x0008)
                        node.Attributes = 0x0008;
                    else if ((node.Attributes & 0x0010) == 0x0010)
                        node.Attributes = 0x0010;

                    node.ID = string.Format("{0}|{1}", node.Attributes, node.ID);
                    node.ParentID = string.Format("{0}|{1}", node.Attributes, string.IsNullOrEmpty(node.ParentID)?Guid.Empty.ToString():node.ParentID);
                    xList.Add(node);
                }

                xList.Add(new GroupTreeData { ID = string.Format("{0}|{1}", 0x0004, Guid.Empty.ToString()), Attributes = 0x0004, ParentID = string.Empty, Name = "Windows" });
                xList.Add(new GroupTreeData { ID = string.Format("{0}|{1}", 0x0008, Guid.Empty.ToString()), Attributes = 0x0008, ParentID = string.Empty, Name = "Web" });
                xList.Add(new GroupTreeData { ID = string.Format("{0}|{1}", 0x0010, Guid.Empty.ToString()), Attributes = 0x0010, ParentID = string.Empty, Name = "Silverlight" });

                List<GroupTreeData> vList = new List<GroupTreeData>();
                
                List<GroupTreeData> v1 = new List<GroupTreeData>();
                ResolveGroupTreeData(xList, v1,string.Empty,0x0004, 0);
                vList.AddRange(v1);
                v1 = new List<GroupTreeData>();
                ResolveGroupTreeData(xList, v1, string.Empty, 0x0008, 0);
                vList.AddRange(v1);
                v1 = new List<GroupTreeData>();
                ResolveGroupTreeData(xList, v1, string.Empty, 0x0010, 0);
                vList.AddRange(v1);
                return vList;
            }
        }

        static void ResolveGroupTreeData(List<GroupTreeData> inputs, List<GroupTreeData> outs, string parentID,int attributes, int treeLevel)
        {
            List<GroupTreeData> vList = inputs.Where(p => p.ParentID == parentID && (p.Attributes & attributes) == attributes).ToList();

            foreach (var item in vList)
            {
                item.TreeLevel = treeLevel;
                item.Group = parentID;
                outs.Add(item);
                ResolveGroupTreeData(inputs, outs, item.ID, attributes, treeLevel + 1);
            }
        }
 
        #endregion

        void SaveData()
        {
            var vData = new NavigateGroup();
            vData.ID = this.lbID.Text;
            vData.ParentID = this.ddlParent.SelectedValue.Split('|')[1];
            vData.Attributes = this.ddlParent.SelectedValue.Split('|')[0].ToInt();
            if (this.cbExpend.Checked)
                vData.Attributes |= 0x0002;

            vData.Name = this.tbName.Text;
            vData.SortCode = this.nbSortCode.Text.ToInt();
            vData.Description = this.tbDescription.Text;

            if (this.btnSave.Enabled)
                vData.Insert();
            else
                vData.Update();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // 1. 这里放置保存窗体中数据的逻辑
            this.SaveData();  
          
            //清空界面
            this.lbID.Text = Guid.NewGuid().ToString().ToUpper();
            this.tbName.Text = string.Empty;
            this.nbSortCode.Text = "0";
            this.tbDescription.Text = string.Empty;
            this.tbName.Focus();
        }

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            // 1. 这里放置保存窗体中数据的逻辑
            this.SaveData();

            // 2. 关闭本窗体，然后刷新父窗体
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
    }
}