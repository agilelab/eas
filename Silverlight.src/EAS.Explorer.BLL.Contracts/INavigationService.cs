using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAS.Explorer.Entities;

namespace EAS.Explorer.BLL
{
    /// <summary>
    /// 登录导航服务。
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// 取导航菜单/目录。
        /// </summary>
        /// <param name="loginID"></param>
        /// <returns></returns>
        NavigationResult GetNavigation(string loginID);
    }
}
