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
    public partial class RoleWindow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnAddAccount.OnClientClick = Window1.GetShowReference("~/Biz/PrivilegersSelector.aspx?mask=1", "选择账户");
                this.btnAddModule.OnClientClick = Window2.GetShowReference("~/Biz/ModuleSelector.aspx", "选择模块");
                LoadData();
            }
            else
            {
                string eventArgument = Request.Form["__EVENTARGUMENT"];
                if (eventArgument.StartsWith("DoAddAccounts$"))
                {
                    string parm = eventArgument.Substring("DoAddAccounts$".Length);
                    this.AddAccounts(parm);
                }
                else if (eventArgument.StartsWith("DoAddModules$"))
                {
                    string parm = eventArgument.Substring("DoAddModules$".Length);
                    this.AddModules(parm);
                }
            }
        }

        /// <summary>
        /// 添加成员。
        /// </summary>
        /// <param name="parm"></param>
        void AddAccounts(string parm)
        {
            string vRole = this.tbName.Text.Trim();
            var vList = parm.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(p => p.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries)[1]).ToList();
            var vService = ServiceContainer.GetService<IRoleService>();
            vService.AddMembers(vRole, vList);
            this.LoadAccounts();
        }

        /// <summary>
        /// 添加模块。
        /// </summary>
        /// <param name="parm"></param>
        void AddModules(string parm)
        {
            string vRole = this.tbName.Text.Trim();
            var vList = parm.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(p => new Guid(p)).ToList();
            var vService = ServiceContainer.GetService<IACLService>();
            vService.Grant(vRole, 2, vList, int.MaxValue);
            this.LoadModules();
        }

        /// <summary>
        /// 提取数据。
        /// </summary>
        void LoadData()
        {
            this.btnClose.OnClientClick = ActiveWindow.GetHideReference();

            if (Request.QueryString.AllKeys.Contains("name"))
            {
                string vName = Request.QueryString["name"];

                var vItem = Role.Lazy(p => p.Name == vName);
                if (vItem == null)
                {
                    Alert.Show("角色数据错误！", String.Empty, ActiveWindow.GetHideReference());
                    return;
                }

                this.hfName.Value = vName;
                this.tbName.Text = vName;
                this.tbDescription.Text = vItem.Description;
                this.btnSave.Enabled = false;

                this.LoadAccounts();
                this.LoadModules();
            }
            else
            {
                this.tbName.Enabled  = true;
            }
        }

        /// <summary>
        /// 加载成员信息。
        /// </summary>
        void LoadAccounts()
        {
            string vRole = this.tbName.Text.Trim();
            using (DbEntities db = new DbEntities())
            {
                var v = from c in db.AccountGroupings
                        from d in db.Accounts.Where(x => x.LoginID == c.LoginID).DefaultIfEmpty()
                        where c.RoleName == vRole
                        select new Account
                        {
                            LoginID = c.LoginID,
                            Name = d.Name,
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
            string vRole = this.tbName.Text.Trim();
            using (DbEntities db = new DbEntities())
            {
                var v = from c in db.ACLs
                        from d in db.Modules.Where(x => x.Guid == c.PObject).DefaultIfEmpty()
                        where c.Privileger == vRole && c.PType == 2
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
            var vData = new Role();
            vData.Name = this.tbName.Text;
            vData.Description = this.tbDescription.Text;
            vData.Save();
        }

        protected void Grid2_Sort(object sender, FineUI.GridSortEventArgs e)
        {
            this.Grid2.SortDirection = e.SortDirection;
            this.Grid2.SortField = e.SortField;
            this.LoadAccounts();
        }

        protected void Grid2_RowCommand(object sender, GridCommandEventArgs e)
        {
            var keys = this.Grid2.DataKeys[e.RowIndex];
            string vRole = this.tbName.Text.Trim();

            if (e.CommandName == "Delete")
            {
                using (DbEntities db = new DbEntities())
                {
                    db.AccountGroupings.Delete(p => p.RoleName == vRole && p.LoginID == keys[0].ToString());
                }
                this.LoadAccounts();
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
            string vRole = this.tbName.Text.Trim();

            if (e.CommandName == "Delete")
            {
                var vService = ServiceContainer.GetService<IACLService>();
                vService.Clear(new Guid(keys[0].ToString()), vRole);
                this.LoadModules();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // 1. 这里放置保存窗体中数据的逻辑
            this.SaveData();

            this.tbName.Text = string.Empty;
            this.tbDescription.Text = string.Empty;

            this.Grid1.DataSource = new List<Module>();
            this.Grid1.DataBind();

            this.Grid2.DataSource = new List<Account>();
            this.Grid2.DataBind();

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