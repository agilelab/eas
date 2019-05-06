using System;
using System.Collections.Generic;
using System.Text;
using EAS.Explorer.Entities;
using System.Collections;
using EAS.Sessions;
using EAS.Explorer.BLL;
using EAS.Services;

namespace EAS.SilverlightClient
{
    class LoginProxy 
    {
        public FuncTask GetAccountLoginInfo(string organ, string loginid, string password)
        {
            FuncTask task = new FuncTask();
            ILoginService loginService = ServiceContainer.GetService<ILoginService>(task);
            loginService.GetAccountInfo(organ, loginid, password);
            return task;
        }

        public FuncTask GetAccountLoginInfo(string loginid, string password)
        {
            return this.GetAccountLoginInfo(string.Empty, loginid, password);
        }

        public FuncTask GetAccountLogoutInfo(string loginid)
        {
            return this.GetAccountLoginInfo("Guest", string.Empty);
        }        
    }
}
