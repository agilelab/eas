using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAS.Explorer.Entities;

namespace EAS.Explorer.BLL
{
    /// <summary>
    /// 桌面服务。
    /// </summary>
    public interface IDesktopService
    {
        /// <summary>
        /// 获取指定登录账号的桌面。
        /// </summary>
        /// <param name="loginID">登录ID。</param>
        /// <returns>桌面项目清单。</returns>
        List<DesktopItem> GetDesktopItems(string loginID);

        /// <summary>
        /// 更新桌面项目计数。
        /// </summary>
        /// <param name="loginID">登录ID。</param>
        /// <param name="module">模块Guid。</param>
        void UpdateDesktopItemCounter(string loginID,string module);
    }
}
