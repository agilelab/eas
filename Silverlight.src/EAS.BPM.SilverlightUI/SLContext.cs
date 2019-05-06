using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using EAS.Explorer.Entities;
using System.Reflection;
using EAS.Explorer;
using System.Windows;

namespace EAS.BPM.SilverlightUI
{
    /// <summary>
    /// 上下文环境。
    /// </summary>
    class SLContext
    {
        #region 单例模式

        static SLContext instance = new SLContext();
        static readonly object _lock = new object();        

        /// <summary>
        /// 单例。
        /// </summary>
        public static SLContext Instance
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new SLContext();
                    }
                }

                return instance;
            }
        }

        internal SLContext()
        {

        }

        #endregion       

        #region 上下文参数

        /// <summary>
        /// 取得当前应用程序。
        /// </summary>
        public static IApplication Application
        {
            get
            {
                return EAS.Application.Instance as IApplication;
            }
        }

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
                return Session.Client as EAS.Explorer.IAccount;
            }
        }

        /// <summary>
        /// 取得系统会话。
        /// </summary>
        public static EAS.Sessions.ISession Session
        {
            get
            {
                return (EAS.Application.Instance as Application).Session;
            }
        }

        #endregion        
    }
}
