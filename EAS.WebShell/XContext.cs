using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using EAS.Explorer.Entities;
using EAS.Distributed;
using System.Reflection;
using EAS.Sockets;
using EAS.Sockets.Bus;
using EAS.Explorer.BLL;
using EAS.Services;

namespace EAS.WebShell
{
    public class XContext
    {
        #region 单例模式

        static XContext instance = new XContext();
        static readonly object _lock = new object();        

        /// <summary>
        /// 单例。
        /// </summary>
        public static XContext Instance
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new XContext();
                    }
                }

                return instance;
            }
        }

        internal XContext()
        {
            this.LoadAppSettings();
        }        

        #endregion       

        void LoadAppSettings()
        {
            IAppSettingService service = ServiceContainer.GetService<IAppSettingService>();

            this.ProductName = service.GetAppSetting("__About", "ProductName");
            this.HelpMenus = service.GetAppSetting("__Web", "HelpMenus");

            string vText = service.GetAppSetting("__Web", "PageSize");
            int vValue = 0;
            if (int.TryParse(vText, out vValue))
                this.PageSize = vValue;
            else
                this.PageSize = 15;

            this.MenuType = service.GetAppSetting("__Web", "MenuType");
            this.Theme = service.GetAppSetting("__Web", "Theme");
        }

        #region 上下文参数

        /// <summary>
        /// 获取系统上下文。
        /// </summary>
        public static EAS.Context.IContext ApplicationContext
        {
            get
            {
                return EAS.Context.ContextHelper.GetContext();
            }
        }

        /// <summary>
        /// 取得系统账号。
        /// </summary>
        public static EAS.Explorer.IAccount Account
        {
            get
            {
                return EAS.Application.Instance.Session.Client as EAS.Explorer.IAccount;
            }
        }

        #endregion        

        #region 属性

        /// <summary>
        /// 产品名称。
        /// </summary>
        public string ProductName
        {
            get;
            set;
        }

        /// <summary>
        /// 列表每页显示的个数
        /// </summary>
        public int PageSize
        {
            get;
            set;
        }

        /// <summary>
        /// 帮助下拉列表
        /// </summary>
        public string HelpMenus
        {
            get;
            set;
        }

        /// <summary>
        /// 菜单样式
        /// </summary>
        public string MenuType
        {
            get;
            set;
        }

        /// <summary>
        /// 网站主题
        /// </summary>
        public string Theme
        {
            get;
            set;
        }

        #endregion

        /// 在线人数
        /// </summary>
        /// <returns></returns>
        public int GetOnlineCount()
        {
            return 1;
        }
    }
}
