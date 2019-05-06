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
using EAS.Services;
using EAS.Explorer.BLL;

namespace EAS.WebShell.Biz
{
    public partial class AccountWindow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnAddRole.OnClientClick = Window1.GetShowReference("~/Biz/PrivilegersSelector.aspx?mask=2", "选择较色");
                this.btnAddModule.OnClientClick = Window2.GetShowReference("~/Biz/ModuleSelector.aspx", "选择模块");
                LoadData();
            }
            else
            {
                string eventArgument = Request.Form["__EVENTARGUMENT"];
                if (eventArgument.StartsWith("DoAddRoles$"))
                {
                    string parm = eventArgument.Substring("DoAddRoles$".Length);
                    this.AddRoles(parm);
                }
                else if (eventArgument.StartsWith("DoAddModules$"))
                {
                    string parm = eventArgument.Substring("DoAddModules$".Length);
                    this.AddModules(parm);
                }
            }
        }        

        /// <summary>
        /// 添加角色。
        /// </summary>
        /// <param name="parm"></param>
        void AddRoles(string parm)
        {
            string vLoginID = this.tbLoinID.Text.Trim();
            var vList = parm.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(p => p.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries)[1]).ToList();
            var vService = ServiceContainer.GetService<IAccountService>();
            vService.AddRoles(vLoginID, vList);
            this.LoadRoles();
        }

        /// <summary>
        /// 添加模块。
        /// </summary>
        /// <param name="parm"></param>
        void AddModules(string parm)
        {
            string vLoginID = this.tbLoinID.Text.Trim();
            var vList = parm.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(p => new Guid(p)).ToList();
            var vService = ServiceContainer.GetService<IACLService>();
            vService.Grant(vLoginID, 1, vList, int.MaxValue);
            this.LoadModules();
        }

        void BindDdlOrgans()
        {
            this.ddlOrgan.EnableSimulateTree = true;
            this.ddlOrgan.DataTextField = "Name";
            this.ddlOrgan.DataValueField = "Guid";
            this.ddlOrgan.DataSimulateTreeLevelField = "TreeLevel";
            this.ddlOrgan.DataSource = OrganizationList.GetTreeGridData();
            this.ddlOrgan.DataBind();
        }

        /// <summary>
        /// 提取数据。
        /// </summary>
        void LoadData()
        {
            this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
            this.BindDdlOrgans();

            if (Request.QueryString.AllKeys.Contains("loginid"))
            {
                string vLoginID = Request.QueryString["loginid"];

                var vItem = Account.Lazy(p => p.LoginID == vLoginID);
                if (vItem == null)
                {
                    Alert.Show("读取账号数据错误！", String.Empty, ActiveWindow.GetHideReference());
                    return;
                }

                this.hfLoginID.Value = vItem.LoginID;
                this.tbLoinID.Text = vItem.LoginID;
                this.tbName.Text = vItem.Name;
                this.ddlOrgan.SelectedValue = vItem.OrganID;
                this.tbRowID.Text = vItem.OriginalID;
                this.tbDescription.Text = vItem.Description;
                this.lbLoginCount.Text = vItem.LoginCount.ToString();

                this.cbLeader.Checked = vItem.Leader == 1;

                this.cbLock.Checked = (vItem.Attributes & 0x0008) == 0x0008;
                this.cbLockPass.Checked = (vItem.Attributes & 0x0002) == 0x0002;
                this.cbLongPass.Checked = (vItem.Attributes & 0x0004) == 0x0004;

                var readOnly = this.CanLockAccount(vItem.LoginID);
                
                this.tbLoinID.Enabled = !readOnly;
                this.tbName.Enabled = !readOnly;
                this.ddlOrgan.Enabled = !readOnly;
                this.tbRowID.Enabled = !readOnly;
                this.tbDescription.Enabled = !readOnly;

                this.cbLeader.Enabled = !readOnly;
                this.cbLock.Enabled = !readOnly;
                this.cbLockPass.Enabled = !readOnly;
                this.cbLongPass.Enabled = !readOnly;

                this.tbPass2.Enabled = this.tbPass.Enabled = string.Compare(vItem.LoginID, "guest", true) != 0;

                this.btnAddRole.Enabled = !readOnly;
                this.btnAddModule.Enabled = !readOnly;

                this.btnSave.Enabled = false;

                this.LoadRoles();
                this.LoadModules();
            }
            else
            {
                this.tbLoinID.Enabled = true;
                this.btnAddModule.Enabled = false;
                this.btnAddRole.Enabled = false;
            }
        }

        /// <summary>
        /// 加载角色信息。
        /// </summary>
        void LoadRoles()
        {
            string vLoginID = this.tbLoinID.Text.Trim();
            using (DbEntities db = new DbEntities())
            {
                var v = from c in db.AccountGroupings
                        from d in db.Roles.Where(x => x.Name == c.RoleName).DefaultIfEmpty()
                        where c.LoginID == vLoginID
                        select new Role
                        {
                            Name = d.Name,
                            Description =d.Description
                        };

                var vList = v.ToList();
                string sortField = this.Grid2.SortField;
                string sortDirection = this.Grid2.SortDirection;
                if (!string.IsNullOrEmpty(sortField))
                    vList = vList.AsQueryable().DynamicSorting(sortField, sortDirection).ToList();

                this.Grid2.DataSource = vList;
                this.Grid2.DataBind();
            }
        }

        void LoadModules()
        {
            string vLoginID = this.tbLoinID.Text.Trim();
            using (DbEntities db = new DbEntities())
            {
                var v = from c in db.ACLs
                        from d in db.Modules.Where(x => x.Guid == c.PObject).DefaultIfEmpty()
                        where c.Privileger == vLoginID && c.PType == 1
                        select new Module
                        {
                            Guid = c.PObject,
                            Name = d.Name,
                            Description = d.Description
                        };

                var vList = v.ToList();
                string sortField = this.Grid1.SortField;
                string sortDirection = this.Grid1.SortDirection;

                if (!string.IsNullOrEmpty(sortField))
                    vList = vList.AsQueryable().DynamicSorting(sortField, sortDirection).ToList();

                this.Grid1.DataSource = vList;
                this.Grid1.DataBind();
            }
        }

        void SaveData()
        {
            var vData = new Account();
            vData.LoginID = this.tbLoinID.Text;
            if (!string.IsNullOrEmpty(this.hfLoginID.Value))
            {
                vData.Read();
            }

            vData.Name = this.tbName.Text;
            vData.OrganID = this.ddlOrgan.SelectedValue;
            vData.OrganName = this.ddlOrgan.SelectedText;
            vData.OriginalID = this.tbRowID.Text;
            vData.Description = this.tbDescription.Text;
            vData.Leader = this.cbLeader.Checked ?1:0;

            if (this.cbLock.Checked)
                vData.Attributes |= 0x0008;
            if (this.cbLockPass.Checked)
                vData.Attributes |= 0x0002;
            if (this.cbLongPass.Checked)
                vData.Attributes |= 0x0004;

            if (!string.IsNullOrEmpty(this.hfLoginID.Value))
            {
                if (string.IsNullOrEmpty(this.tbPass.Text))
                    vData.Update();
                else
                {
                    var vService = ServiceContainer.GetService<IAccountService>();
                    vService.UpdateAccount(vData, this.tbPass.Text);
                }
            }
            else
            {
                var vService = ServiceContainer.GetService<IAccountService>();
                vService.UpdateAccount(vData,this.tbPass.Text);
            }
        }

        /// <summary>
        /// 是否为锁定账号。
        /// </summary>
        /// <param name="loginID"></param>
        /// <returns></returns>
        bool CanLockAccount(string loginID)
        {
            return string.Compare(loginID, "Administrator", true) == 0 || string.Compare(loginID, "guest", true) == 0;
        }

        /// <summary>
        /// 验证输入。
        /// </summary>
        /// <returns></returns>
        bool ValidateInput()
        {
            if (string.IsNullOrEmpty(this.hfLoginID.Value))
            {
                if (this.tbPass.Text == string.Empty)
                {
                    Alert.Show("账号密码不能为空", "操作提示", MessageBoxIcon.Information);
                    this.tbPass.Focus();
                    return false;
                }
            }

            if (this.tbPass.Text != this.tbPass2.Text)
            {
                Alert.Show("两次输入密码不一致", "操作提示", MessageBoxIcon.Information);
                this.tbPass2.Focus();
                return false;
            }
            return true;
        }

        protected void Grid2_Sort(object sender, FineUI.GridSortEventArgs e)
        {
            this.Grid2.SortDirection = e.SortDirection;
            this.Grid2.SortField = e.SortField;
            this.LoadRoles();
        }

        protected void Grid2_RowCommand(object sender, GridCommandEventArgs e)
        {
            var keys = this.Grid2.DataKeys[e.RowIndex];
            string vLoginID = this.tbName.Text.Trim();

            if (e.CommandName == "Delete")
            {
                if (!this.CanLockAccount(vLoginID))
                {
                    using (DbEntities db = new DbEntities())
                    {
                        db.AccountGroupings.Delete(p => p.LoginID == vLoginID && p.RoleName == keys[0].ToString());
                    }
                }
                this.LoadRoles();
            }
        }

        protected void Grid1_Sort(object sender, FineUI.GridSortEventArgs e)
        {
            this.Grid1.SortDirection = e.SortDirection;
            this.Grid1.SortField = e.SortField;
            this.LoadModules();
        }

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            var keys = this.Grid1.DataKeys[e.RowIndex];
            string vLoginID = this.tbLoinID.Text.Trim();

            if (e.CommandName == "Delete")
            {
                if (!this.CanLockAccount(vLoginID))
                {
                    var vService = ServiceContainer.GetService<IACLService>();
                    vService.Clear(new Guid(keys[0].ToString()), vLoginID);
                }
                this.LoadModules();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateInput())
                return;

            // 1. 这里放置保存窗体中数据的逻辑
            this.SaveData();

            //清空界面
            this.tbLoinID.Text = string.Empty;
            this.tbName.Text = string.Empty;
            //this.ddlOrgan.SelectedValue = string.Empty;
            this.tbRowID.Text = string.Empty;
            this.tbDescription.Text = string.Empty;
            this.lbLoginCount.Text = "0";

            this.tbPass.Text = string.Empty;
            this.tbPass2.Text = string.Empty;

            this.cbLeader.Checked = false;
            this.cbLock.Checked = false;
            this.cbLockPass.Checked = false;
            this.cbLongPass.Checked = false;

            this.Grid1.DataSource = new List<Module>();
            this.Grid1.DataBind();

            this.Grid2.DataSource = new List<Role>();
            this.Grid2.DataBind();

            this.tbLoinID.Focus();
        }

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            if (!this.ValidateInput())
                return;

            // 1. 这里放置保存窗体中数据的逻辑
            this.SaveData();

            // 2. 关闭本窗体，然后刷新父窗体
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
    }
}