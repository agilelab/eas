using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EAS.Context;
using EAS.Sessions;
using EAS.Explorer;

namespace EAS.ActiveXForm.Web
{
    class WebContext
    {
        public static IContext Context
        {
            get
            {
                return EAS.Application.Instance.Context;
            }
        }

        public static ISession Session
        {
            get
            {
                return EAS.Application.Instance.Session;
            }
        }

        public static IAccount Account
        {
            get
            {
                return EAS.Application.Instance.Session.Client as IAccount;
            }
        }
    }
}
