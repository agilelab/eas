using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Profile;
using EAS.Explorer.Web;
using EAS.Explorer.Entities;

namespace EAS.ActiveXForm.Web
{
    public partial class Default :System.Web.UI.Page
    {
        private string applicationURLRoot = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request.QueryString["loginout"] != null)
            {
                try
                {
                    (EAS.Application.Instance as EAS.Explorer.Web.IApplication).Logout();
                }
                catch (System.Exception exc)
                {
                    //
                }
            }

            this.tbLogin.Focus();
            //this.Message.Text = string.Empty;

            if (!IsPostBack)
            {
                this.LoadCookie();
            }
        }

        private void SetCookie()
        {
            HttpCookie cookie = new HttpCookie("ActiveXForm");
            cookie.Values.Add("login", HttpUtility.UrlEncode(this.tbLogin.Text));
            base.Response.AppendCookie(cookie);
            cookie.Expires = DateTime.Now.AddDays(30.0);
        }

        private void LoadCookie()
        {
            try
            {
                HttpCookie cookie = base.Request.Cookies["ActiveXForm"];
                this.tbLogin.Text = HttpUtility.UrlDecode(cookie.Values["login"].ToString());
            }
            catch
            {
            }
        }

        /// <summary>
        /// 应用程序根目录。
        /// </summary>
        protected string ApplicationURLRoot
        {
            get
            {
                if (string.IsNullOrEmpty(this.applicationURLRoot))
                {
                    this.applicationURLRoot = base.Request.Url.GetLeftPart(UriPartial.Authority) + base.Request.ApplicationPath;
                    if (!this.applicationURLRoot.EndsWith("/"))
                    {
                        this.applicationURLRoot = this.applicationURLRoot + "/";
                    }
                }
                return this.applicationURLRoot;
            }
        }

        /// <summary>
        /// 配置文件实例。
        /// </summary>
        protected DefaultProfile Profile
        {
            get
            {
                return (DefaultProfile)this.Context.Profile;
            }
        }

        /// <summary>
        /// 会话ＩＤ。
        /// </summary>
        protected string SessionId
        {
            get
            {
                return this.Session.SessionID;
            }
        }

        public void OnAccountChanged(System.EventArgs e)
        {
            if (string.Compare(WebContext.Account.LoginID, "Guest", true) != 0)
            {
                Response.Redirect("Biz/Default.aspx");
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void buttonOK_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                (EAS.Application.Instance as EAS.Explorer.Web.IApplication).Login(this.tbLogin.Text, this.tbPass.Text);
                this.OnAccountChanged(e);
            }
            catch (System.Exception exc)
            {
                if (exc.InnerException != null)
                    WebMsgBox.Show(exc.InnerException.Message);
                else
                    WebMsgBox.Show(exc.Message);
            }
        }
    }
}
