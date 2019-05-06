using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using FineUI;
using System.Web.Security;
using EAS.Explorer;

namespace EAS.WebShell
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //如果用户已经登录，则重定向到管理首页
                if (User.Identity.IsAuthenticated)
                {
                    Response.Redirect(FormsAuthentication.DefaultUrl);
                }

                Window1.Title = "系统登录（" + XContext.Instance.ProductName + "）";
                InitCaptchaCode();
            }
        }

        /// <summary>
        /// 初始化验证码
        /// </summary>
        void InitCaptchaCode()
        {
            // 创建一个 6 位的随机数并保存在 Session 对象中
            Session["CaptchaImageText"] = GenerateRandomCode();
            imgCaptcha.ImageUrl = "~/captcha/captcha.ashx?w=150&h=30&t=" + DateTime.Now.Ticks;
        }

        /// <summary>
        /// 创建一个 6 位的随机数
        /// </summary>
        /// <returns></returns>
        string GenerateRandomCode()
        {
            string s = String.Empty;
            Random random = new Random();
            for (int i = 0; i < 6; i++)
            {
                s += random.Next(10).ToString();
            }
            return s;
        }

        /// <summary>
        /// 创建表单验证的票证并存储在客户端Cookie中
        /// </summary>
        /// <param name="account">当前登录账户对象。</param>
        /// <param name="isPersistent">是否跨浏览器会话保存票证</param>
        /// <param name="expiration">过期时间</param>
        void CreateFormsAuthenticationTicket(IAccount account, bool isPersistent, DateTime expiration)
        {
            // 创建Forms身份验证票据
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                account.LoginID,                // 与票证关联的用户名
                DateTime.Now,                   // 票证发出时间
                expiration,                     // 票证过期时间
                isPersistent,                   // 如果票证将存储在持久性 Cookie 中（跨浏览器会话保存），则为 true；否则为 false。
                account.Name                    // 存储在票证中的用户特定的数据
             );

            // 对Forms身份验证票据进行加密，然后保存到客户端Cookie中
            string hashTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashTicket);
            cookie.HttpOnly = true;
            // 1. 关闭浏览器即删除（Session Cookie）：DateTime.MinValue
            // 2. 指定时间后删除：大于 DateTime.Now 的某个值
            // 3. 删除Cookie：小于 DateTime.Now 的某个值
            if (isPersistent)
            {
                cookie.Expires = expiration;
            }
            else
            {
                cookie.Expires = DateTime.MinValue;
            }
            Response.Cookies.Add(cookie);
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            InitCaptchaCode();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (this.tbCaptcha.Text != Session["CaptchaImageText"].ToString())
            {
                Alert.ShowInTop("验证码错误！");
                return;
            }

            try
            {
                (EAS.Application.Instance as EAS.Explorer.Web.IApplication).Login(this.tbLogin.Text, this.tbPassword.Text);
            }
            catch (System.Exception exc)
            {
                Loggers.Logger.Error(exc);
                if (exc.InnerException != null)
                    Alert.ShowInTop(exc.InnerException.Message);
                else
                    Alert.ShowInTop(exc.Message);
                return;
            }

            //保存票证。
            DateTime expiration = DateTime.Now.AddMinutes(120);
            this.CreateFormsAuthenticationTicket(XContext.Account, false, expiration);

            // 重定向到登陆后首页
            Response.Redirect(FormsAuthentication.DefaultUrl);

        }
    }
}