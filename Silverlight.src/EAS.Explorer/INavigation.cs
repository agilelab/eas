using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace EAS.Explorer
{
    /// <summary>
    /// 导航控件接口。
    /// </summary>
    public interface INavigation
    {
        /// <summary>
        /// 初始化导航。
        /// </summary>
        /// <param name="GroupList">导航信息(IList<Group>)。</param>
        /// <param name="ModuleList">模块信息(IList<GroupModule>)。</param>
        void Initialize(object groupList, object moduleList);
    }
}
