using System;
using System.Collections.Generic;
using System.Text;
using EAS.Explorer.Entities;
using System.Collections;
using EAS.Sessions;
using EAS.Explorer.BLL;
using EAS.Services;

namespace EAS.Explorer.WinClient
{
    class LoginProxy 
    {
        public IAccount GetAccountLoginInfo(string organ, string loginid, string password)
        {
            ILoginService loginService = ServiceContainer.GetService<ILoginService>();
            LoginResult lr = loginService.GetAccountInfo(organ, loginid, password);

            if ((!lr.Passed) & (string.Compare(loginid, "Guest", true) != 0))
                throw new System.Exception("登录验证时发现未知错误");

            return lr.Account;
        }

        public IAccount GetAccountLoginInfo(string loginid, string password)
        {
            return this.GetAccountLoginInfo(string.Empty, loginid, password);
        }

        public IAccount GetAccountLogoutInfo()
        {
            return this.GetAccountLoginInfo("Guest", string.Empty);
        }
    }
}
