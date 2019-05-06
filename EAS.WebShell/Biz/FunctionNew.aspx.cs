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
using EAS.Explorer;
using EAS.Explorer.BLL;
using EAS.Services;

namespace EAS.WebShell.Biz
{
    public partial class FunctionNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.tbGuid.Text = Guid.NewGuid().ToString().ToUpper();
                this.tbDeveloper.Text = "agilelab.cn";
                this.tbGuid.Focus();
            }
        }

        bool SaveData()
        {
            var module = new Module();
            module.Assembly = "Function";
            module.Type = module.Assembly;
            module.Developer = this.tbDeveloper.Text.Trim();
            module.Version = "1.0.0.0";
            module.Guid = this.tbGuid.Text.Trim().ToUpper();
            module.Name = this.tbName.Text;
            module.Description = this.tbDescription.Text.Trim();
            module.Attributes = (int)GoComType.Function;
            module.Method = string.Empty;
            module.LMTime = DateTime.Now;
            module.SortCode = 0;
            module.Package = Guid.Empty.ToString();

            using (DbEntities db = new DbEntities())
            {
                int count = db.Modules.Where(p => p.Guid == module.Guid).Count();
                if (count > 0)
                {
                    Alert.Show(string.Format("系统中已经存在GUID为{0}的构件！", module.Guid));
                    return false;
                }
            }

            try
            {
                IModuleService service = ServiceContainer.GetService<IModuleService>();
                service.InstallModule(module);
            }
            catch (System.Exception exc)
            {
                Alert.Show("在配置函数过程之中发生错误：\n\n" + exc.Message, "错误", MessageBoxIcon.Error);
                return false; 
            }

            return true;
        }

        /// <summary>
        /// 验证输入。
        /// </summary>
        /// <returns></returns>
        bool ValidateInput()
        {
            Guid ID = Guid.Empty;
            if (!Guid.TryParse(this.tbGuid.Text.Trim(), out ID))
            {
                this.tbGuid.Focus();
                Alert.Show("函数GUID格式不正确，请输入有效的全局唯一标识符 (GUID)。");
                return false;
            }
            
            return true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateInput()) return;

            // 1. 这里放置保存窗体中数据的逻辑
            if (this.SaveData())
            {
                //清空界面
                this.tbGuid.Text = Guid.NewGuid().ToString().ToUpper();
                this.tbDeveloper.Text = "agilelab.cn";
                this.tbName.Text = string.Empty;
                this.tbDescription.Text = string.Empty;
                this.tbGuid.Focus();
            }
        }

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            if (!this.ValidateInput()) return;

            // 1. 这里放置保存窗体中数据的逻辑
            if (this.SaveData())
            {
                // 2. 关闭本窗体，然后刷新父窗体
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }
    }
}