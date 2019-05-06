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
using EAS.Explorer.BLL;
using EAS.Services;

namespace EAS.WebShell.Biz
{
    public partial class ModuleWindow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnAdd.OnClientClick = Window1.GetShowReference("~/Biz/PrivilegersSelector.aspx", "选择账户&较色");
                LoadData();
            }
            else
            {
                string eventArgument = Request.Form["__EVENTARGUMENT"];
                if (eventArgument.StartsWith("DoGrant$"))
                {
                    string parm = eventArgument.Substring("DoGrant$".Length);
                    GrantTo(parm);
                }
            }
        }

        /// <summary>
        /// 授权。
        /// </summary>
        /// <param name="parm"></param>
        void GrantTo(string parm)
        {
            var vList = parm.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(p => new
            {
                Type = p.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries)[0],
                Name = p.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries)[1]
            }).ToList();
            var vService = ServiceContainer.GetService<IACLService>();
            var v1 = vList.Where(p => p.Type == "1").Select(p => p.Name).ToList();
            if (v1.Count > 0)
                vService.Grant(new Guid(this.tbID.Text), v1, 1, int.MaxValue);
            var v2 = vList.Where(p => p.Type == "2").Select(p => p.Name).ToList();
            if (v2.Count > 0)
                vService.Grant(new Guid(this.tbID.Text), v1, 2, int.MaxValue);
            this.BindGrid();
        }

        void LoadData()
        {
            this.btnClose.OnClientClick = ActiveWindow.GetHideReference();

            if (Request.QueryString.AllKeys.Contains("guid"))
            {
                using (DbEntities db = new DbEntities())
                {
                    string vGuid = Request.QueryString["guid"];
                    string vKey = Request.QueryString["key"];

                    var vItem = db.Modules.Where(p => p.Guid == vGuid).FirstOrDefault();
                    if (vItem == null)
                    {
                        Alert.Show("加载模块信息错误！", String.Empty, ActiveWindow.GetHideReference());
                        return;
                    }

                    this.tbName.Text = vItem.Name;
                    this.tbID.Text = vItem.Guid.ToUpper();
                    this.tbType.Text = vItem.Type;
                    this.tbAssembly.Text = vItem.Assembly;
                    this.tbVersion.Text = vItem.Version;
                    this.tbDeveloper.Text = vItem.Developer;
                    this.tbAssembly.Text = vItem.Assembly;
                    this.tbUrl.Text = vItem.Url;
                    this.nbSortCode.Text = vItem.SortCode.ToString();
                    this.tbDescription.Text = vItem.Description;

                    this.BindGrid();
                }
            }
            else
            {
                return;
            }
        }

        private void BindGrid()
        {
            string sortField = this.Grid2.SortField;
            string sortDirection = this.Grid2.SortDirection;

            using (DbEntities db = new DbEntities())
            {
                var v = db.ACLs.Where(p => p.PObject == this.tbID.Text);
                if (!string.IsNullOrEmpty(sortField))
                    v = v.DynamicSorting(sortField, sortDirection);
                this.Grid2.DataSource = v.ToList();
                this.Grid2.DataBind();
            }
        }

        void SaveData()
        {
            var vData = Module.Lazy(p => p.Guid == this.tbID.Text);
            vData.Name = this.tbName.Text;
            vData.Url = this.tbUrl.Text;
            vData.SortCode = this.nbSortCode.Text.ToInt();
            vData.Description = this.tbDescription.Text;
            vData.LMTime = EAS.Environment.NowTime;
            vData.Update();
        }

        protected string GetPTypeText(object pType)
        {
            return pType.ToString() == "1" ? "账号" : "角色";
        }

        protected void Grid2_RowCommand(object sender, GridCommandEventArgs e)
        {
            var keys = this.Grid2.DataKeys[e.RowIndex];

            if (e.CommandName == "Delete")
            {
                var vService = ServiceContainer.GetService<IACLService>();
                vService.Clear(new Guid(keys[0].ToString()), keys[1].ToString());
                this.BindGrid();
            }
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