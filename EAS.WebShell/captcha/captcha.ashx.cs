using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;
using System.Drawing.Imaging;

namespace FineUI.Examples.basic.Captcha
{
    /// <summary>
    /// 生成验证码图片
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class captcha : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            int width = 200;
            int height = 30;

            try
            {
                width = Convert.ToInt32(context.Request.QueryString["w"]);
                height = Convert.ToInt32(context.Request.QueryString["h"]);
            }
            catch (Exception)
            {
                // Nothing
            }

            // 从 Session 中读取验证码，并创建图片
            CaptchaImage.CaptchaImage ci = new CaptchaImage.CaptchaImage(context.Session["CaptchaImageText"].ToString(), width, height, "Consolas");

            // 输出图片
            context.Response.Clear();
            context.Response.ContentType = "image/jpeg";

            ci.Image.Save(context.Response.OutputStream, ImageFormat.Jpeg);

            ci.Dispose();
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
