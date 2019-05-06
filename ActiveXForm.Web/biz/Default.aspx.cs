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

namespace EAS.ActiveXForm.Web.Biz
{
    public partial class Default : System.Web.UI.Page
    {
        private string applicationURLRoot;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(string.Compare(this.LoginID,"Guest") ==0)
            {
                this.Response.Redirect("../Default.aspx");
            }
        }

        /// <summary>
        /// URL¡£
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
        /// »á»°£É£Ä¡£
        /// </summary>
        protected string SessionId
        {
            get
            {
                return this.Session.SessionID;
            }
        }

        /// <summary>
        /// ÕËÌ×¡£
        /// </summary>
        protected string DataSet
        {
            get
            {
                return WebContext.Session.DataSet;
            }
        }

        /// <summary>
        /// µÇÂ¼ÕËºÅ¡£
        /// </summary>
        protected string LoginID
        {
            get
            {
                return WebContext.Account.LoginID;
            }
        }

        /// <summary>
        /// ÊÇ·ñÏÔÊ¾´íÎó¡£
        /// </summary>
        protected string ShowException
        {
            get
            {
                return "true";
            }
        }        
    }
}
