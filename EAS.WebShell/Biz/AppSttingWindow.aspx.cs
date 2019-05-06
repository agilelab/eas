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
    public partial class AppSttingWindow : System.Web.UI.Page
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

            if (Request.QueryString.AllKeys.Contains("category") && Request.QueryString.AllKeys.Contains("key"))
            {
                string vCategory = Request.QueryString["category"];
                string vKey = Request.QueryString["key"];

                var vItem = AppSetting.Lazy(p => p.Category == vCategory && p.Key == vKey);
                if (vItem == null)
                {
                    // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                    Alert.Show("参数错误！", String.Empty, ActiveWindow.GetHideReference());
                    return;
                }

                this.tbCategory.Text = vCategory;
                this.tbKey.Text = vKey;
                this.tbValue.Text = vItem.Value;
                this.tbDescription.Text = vItem.Description;
                this.tbCategory.Enabled = this.tbKey.Enabled = false;
                this.btnSave.Enabled = false;
            }
            else
            {
                this.tbCategory.Enabled = this.tbKey.Enabled = true;
            }
        }

        void SaveData()
        {
            var vData = new AppSetting();
            vData.Category = this.tbCategory.Text;
            vData.Key = this.tbKey.Text;
            vData.Value = this.tbValue.Text;
            vData.Description = this.tbDescription.Text;
            vData.LMTime = EAS.Environment.NowTime;
            vData.Save();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // 1. 这里放置保存窗体中数据的逻辑
            this.SaveData();  
          
            //清空界面
            this.tbCategory.Text = string.Empty;
            this.tbKey.Text = string.Empty;
            this.tbValue.Text = string.Empty;
            this.tbDescription.Text = string.Empty;
            this.tbCategory.Focus();
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