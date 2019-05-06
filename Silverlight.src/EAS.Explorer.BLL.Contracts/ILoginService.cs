using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAS.Explorer.Entities;

namespace EAS.Explorer.BLL
{
    /// <summary>
    /// 登录服务。
    /// </summary>
    public interface ILoginService
    {
        LoginResult GetAccountInfo(string organ, string loginID, string password);
        List<Role> GetAccountRoleList(string loginID);
    }
}
