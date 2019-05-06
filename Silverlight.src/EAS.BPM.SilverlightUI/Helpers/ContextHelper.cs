using System;
using System.Collections.Generic;
using System.Text;
using EAS.Explorer;

namespace EAS.BPM.SilverlightUI
{
    /// <summary>
    /// 公共卫生平台上下文环境。
    /// </summary>
    internal class ContextHelper
    {
        /// <summary>
        /// 取得系统账号。
        /// </summary>
        public static IAccount Account
        {
            get
            {
                return Session.Client as IAccount;
            }
        }

        /// <summary>
        /// 取得系统会话。
        /// </summary>
        public static EAS.Sessions.ISession Session
        {
            get
            {
                return EAS.Application.Instance.Session;
            }
        }

        /// <summary>
        /// 取得系统上下文。
        /// </summary>
        public static EAS.Context.IContext Context
        {
            get
            {
                return EAS.Application.Instance.Context;
            }
        }

        //public static IResource ShellResource
        //{
        //    get
        //    {

        //        return EAS.Explorer.ResourceManager.Resource ;
        //    }
        //}
    }
}
