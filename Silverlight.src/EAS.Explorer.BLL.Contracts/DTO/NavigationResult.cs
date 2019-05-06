using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAS.Explorer.Entities;

namespace EAS.Explorer.BLL
{
    /// <summary>
    /// 登录结果信息。
    /// </summary>
    [Serializable]
    public class NavigationResult
    {
        /// <summary>
        /// 分组信息。
        /// </summary>
        public List<NavigateGroup> Groups
        {
            get;
            set;
        }

        /// <summary>
        /// 模块信息。
        /// </summary>
        public List<NavigateModule> Modules
        {
            get;
            set;
        }
    }
}
