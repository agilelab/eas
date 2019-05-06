
using EAS.Explorer;
using EAS.SilverlightClient.UI;

namespace EAS.SilverlightClient
{
    class PlugContext
    {
        System.Type t_LoginForm = null;
        System.Type t_AboutForm = null;
        System.Type t_StartModule = null;
        System.Type t_Banner = null;
        System.Type t_Footer = null;
        System.Type t_Navigation = null;

        string m_Name = string.Empty;
        string m_Title = string.Empty;

        #region 单例模型

        static PlugContext instance;
        static readonly object _lock = new object();

        internal PlugContext()
        {
            try
            {
                t_Banner = SLContext.Instance.ShellResource.GetBannerControl().GetType();
            }
            catch { }

            try
            {
                t_Footer = SLContext.Instance.ShellResource.GetBottomControl().GetType();
            }
            catch { }

            try
            {
                t_Navigation = SLContext.Instance.ShellResource.GetNavigationControl().GetType();
            }
            catch { }

            //
            try
            {
                t_LoginForm = SLContext.Instance.ShellResource.GetLoginForm().GetType();
            }
            catch { }

            //
            //try
            //{
            //    t_AboutForm = SLContext.Instance.ShellResource.GetAboutForm().GetType();
            //}
            //catch { }

            //
            try
            {
                t_StartModule = SLContext.Instance.ShellResource.GetStartModule().GetType();
            }
            catch { }

            //
            try
            {
                m_Name = SLContext.Instance.ShellResource.GetApplicationName();
            }
            catch { }

            //
            try
            {
                m_Title = SLContext.Instance.ShellResource.GetApplicationTitle();
            }
            catch { }

            if (t_Banner == null)
                t_Banner = typeof(Banner);

            if (t_Footer == null)
                t_Footer = typeof(Footer);

            if (t_Navigation == null)
                t_Navigation = typeof(Navigation);

            if (t_LoginForm == null)
                t_LoginForm = typeof(LoginPage);

            //if (t_AboutForm == null)
            //    t_AboutForm = typeof(AboutWindow); 

            if (t_StartModule == null)
                t_StartModule = typeof(StartWF); 

            if (m_Name.Length ==0)
                m_Name = "AgileEAS.NET SOA";

            if (m_Title.Length == 0)
                m_Title = "AgileEAS.NET SOA 中间件";
        }

        static PlugContext Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                            instance = new PlugContext();
                    }
                }

                return instance;
            }
        }

        #endregion

        #region 资源类型

        public static System.Type TBanner
        {
            get
            {
                return Instance.t_Banner;
            }
        }

        public static System.Type TLoginForm
        {
            get
            {
                return Instance.t_LoginForm;
            }
        }

        public static System.Type TAboutForm
        {
            get
            {
                return Instance.t_AboutForm;
            }
        }

        public static System.Type TStartModule
        {
            get
            {
                return Instance.t_StartModule;
            }
        }

        #endregion

        #region 资源实例

        public static object Banner
        {
            get
            {
                return System.Activator.CreateInstance(Instance.t_Banner);
            }
        }

        public static object Footer
        {
            get
            {
                return System.Activator.CreateInstance(Instance.t_Footer);
            }
        }

        public static object Navigation
        {
            get
            {
                return System.Activator.CreateInstance(Instance.t_Navigation);
            }
        }

        public static object LoginForm
        {
            get
            {
                return System.Activator.CreateInstance(Instance.t_LoginForm);
            }
        }

        //public static object AboutForm
        //{
        //    get
        //    {
        //        return System.Activator.CreateInstance(Instance.t_AboutForm);
        //    }
        //}

        public static object StartModule
        {
            get
            {
                return System.Activator.CreateInstance(Instance.t_StartModule);
            }
        }

        #endregion

        public static string ApplicationName
        {
            get
            {
                return Instance.m_Name;
            }
        }

        public static string ApplicationTitle
        {
            get
            {
                return Instance.m_Title;
            }
        }
    }
}
