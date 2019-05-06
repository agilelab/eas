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
using EAS.Explorer;

namespace EAS.SilverlightClient
{
    internal class LoginContext
    {
        #region Singleton

        static readonly object _lock = new object();
        static LoginContext instance = null;

        public static LoginContext Singleton
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                            instance = new LoginContext();
                    }
                }

                return instance;
            }
        }


        #endregion

        LoginContext()
        {
            IsolatedStorage = false;
        }

        /// <summary>
        /// 本次是否需要分配独立存储。
        /// </summary>
        public bool IsolatedStorage
        {
            get;
            set;
        }

        /// <summary>
        /// 当前账号。
        /// </summary>
        public IAccount Account
        {
            get;
            internal set;
        }
    }
}
