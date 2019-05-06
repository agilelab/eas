using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FineUI;

namespace EAS.WebShell
{
    /// <summary>
    /// 处理页码主题。
    /// </summary>
    public class ThemeHttpModule : IHttpModule
    {
        #region IHttpModule 成员

        public void Dispose()
        {
            
        }

        public void Init(HttpApplication context)
        {
            context.AcquireRequestState += new EventHandler(context_AcquireRequestState);
        }

        private void context_AcquireRequestState(object sender, EventArgs e)
        {
            System.Web.HttpContext httpContext = System.Web.HttpContext.Current;
            IHttpHandler handler = httpContext.Handler;

            if (handler is System.Web.UI.Page)
            {
                (handler as System.Web.UI.Page).Init += new EventHandler(ThemeHttpModule_Init);
            }
        }

        void ThemeHttpModule_Init(object sender, EventArgs e)
        {
            // 设置主题
            if (PageManager.Instance != null)
            {
                var theme = this.GetTheme();
                if (!string.IsNullOrEmpty(theme))
                {
                    PageManager.Instance.Theme = (Theme)Enum.Parse(typeof(Theme), theme, true);
                }
            }
        }

        #endregion

        /// <summary>
        /// 求当前主题。
        /// </summary>
        /// <returns></returns>
        string GetTheme()
        {
            HttpCookie themeCookie = HttpContext.Current.Request.Cookies["EAS_Theme_v5"];
            if (themeCookie != null)
            {
                return themeCookie.Value;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}