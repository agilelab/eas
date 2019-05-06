using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAS.Explorer.Entities;

namespace EAS.Explorer.BLL
{
    /// <summary>
    /// 系统配置服务。
    /// </summary>
    public interface IAppSettingService
    {
        /// <summary>
        /// 读取系统配置。
        /// </summary>
        /// <param name="category">目录。</param>
        /// <param name="itemKey">项目。</param>
        /// <returns>配置信息。</returns>
        string GetAppSetting(string category, string itemKey);

        /// <summary>
        /// 读取系统配置。
        /// </summary>
        /// <param name="category">目录。</param>
        /// <returns>配置信息。</returns>
        List<AppSetting> GetAppSettingList(string category);
    }
}
