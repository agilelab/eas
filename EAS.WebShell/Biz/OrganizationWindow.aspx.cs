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
    public partial class OrganizationWindow : System.Web.UI.Page
    {
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
                string vGuid = Request.QueryString["id"];

                var vItem = Organization.Lazy(p => p.Guid == vGuid.ToUpper());
                if (vItem == null)
                {
                    Alert.Show("组织机构不存在！", String.Empty, ActiveWindow.GetHideReference());
                    return;
                }

                this.lbID.Text = vItem.Guid.ToUpper();
                this.hfGuid.Value = this.lbID.Text;
                this.tbName.Text = vItem.Name;
                this.tbAddress.Text = vItem.Address;
                this.tbContact.Text = vItem.Contact;
                this.tbTel.Text = vItem.Tel;
                this.tbFax.Text = vItem.Fax;
                this.tbMail.Text = vItem.Email;
                this.tbHomepage.Text = vItem.Homepage;
                this.tbOrganCode.Text = vItem.OrganCode;
                this.tbRemarks.Text = vItem.Remarks;

                this.ddlParent.SelectedValue = vItem.ParentID.ToString();

                this.ddlParent.Enabled = false;
                this.btnSave.Enabled = false;
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
            this.ddlParent.DataValueField = "Guid";
            this.ddlParent.DataSimulateTreeLevelField = "TreeLevel";
            //this.ddlParent.DataEnableSelectField = "Enabled";
            this.ddlParent.DataSource = OrganizationList.GetTreeGridData();
            this.ddlParent.DataBind();
        }

        void SaveData()
        {
            var vData = new Organization();
            vData.Guid = this.lbID.Text;
            if (!string.IsNullOrEmpty(this.hfGuid.Value))
            {
                vData.Read();
            }

            vData.ParentID = this.ddlParent.SelectedValue.ToString();
            vData.Name = this.tbName.Text;
            vData.Address = this.tbAddress.Text;
            vData.Contact = this.tbContact.Text;
            vData.Tel = this.tbTel.Text;
            vData.Fax = this.tbFax.Text;
            vData.Email = this.tbMail.Text;
            vData.Homepage = this.tbHomepage.Text;
            vData.OrganCode = this.tbOrganCode.Text;
            vData.Remarks = this.tbRemarks.Text;
            if (!string.IsNullOrEmpty(this.hfGuid.Value))
            {
                vData.Update();
            }
            else
            {
                vData.Insert();
            }
        }        

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // 1. 这里放置保存窗体中数据的逻辑
            this.SaveData();  
          
            //清空界面
            this.lbID.Text = Guid.NewGuid().ToString().ToUpper();
            this.tbName.Text = string.Empty;
            this.tbAddress.Text = string.Empty;
            this.tbContact.Text = string.Empty;
            this.tbTel.Text = string.Empty;
            this.tbFax.Text = string.Empty;
            this.tbMail.Text = string.Empty;
            this.tbHomepage.Text = string.Empty;
            this.tbOrganCode.Text = string.Empty;
            this.tbRemarks.Text = string.Empty;
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