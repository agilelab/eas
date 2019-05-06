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
    /// 运行容器的外壳资源。
    /// </summary>
    public interface IResource
    {
        /// <summary>
        /// 获取应用系统的导航控件，用于替换平台的导航控件。
        /// </summary>
        /// <returns>Silverlight用户控件。</returns>
        object GetNavigationControl();

        /// <summary>
        /// 获取应用系统的Banner控件，用于替换平台的Banner条。
        /// </summary>
        /// <returns>Silverlight用户控件。</returns>
        object GetBannerControl();

        /// <summary>
        /// 获取应用系统的Bottom控件，用于替换平台的Bottom条。
        /// </summary>
        /// <returns>Silverlight用户控件。</returns>
        object GetBottomControl();

        ///// <summary>
        ///// 获取Silverlight容器的关于对话框，用于替换平台的关于对话框。
        ///// </summary>
        ///// <returns>Silverlight窗体。</returns>
        //object GetAboutForm();

        /// <summary>
        /// 获取Silverlight/Silverlight容器的登录对话框，用于替换平台的登录对话框。
        /// </summary>
        /// <returns>Silverlight/Silverlight窗体。</returns>
        object GetLoginForm();

        /// <summary>
        /// 获取Silverlight容器的起始页/初始模块，用于替换平台的起始页。
        /// </summary>
        /// <returns>Silverlight用户控件。</returns>
        object GetStartModule();

        /// <summary>
        /// 获取系统的名称，显示在运行环境的导航栏。
        /// </summary>
        /// <returns>应用系统名称。</returns>
        string GetApplicationName();

        /// <summary>
        /// 获取系统的标题，显示在运行环境的主窗口之上。
        /// </summary>
        /// <returns>应用系统名称。</returns>
        string GetApplicationTitle();
    }
}
