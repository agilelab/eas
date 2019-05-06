using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EAS.WebShell.Ajax
{
    /// <summary>
    /// HelpHandler 的摘要说明
    /// </summary>
    public class HelpHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string command = context.Request["cmd"];
            switch (command)
            {
                case "GetOnlineUserCount":
                    this.GetOnlineUserCount(context);
                    break;
                case "GetCurrentTime":
                    this.GetCurrentTime(context);
                    break;
            };
        }

        private void GetCurrentTime(HttpContext context)
        {
            context.Response.Write(EAS.Environment.NowTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        private void GetOnlineUserCount(HttpContext context)
        {
            context.Response.Write(XContext.Instance.GetOnlineCount());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}