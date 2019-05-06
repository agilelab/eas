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
using EAS.Services;

namespace EAS.SilverlightClient
{
    /// <summary>
    /// 时间服务客户端。
    /// </summary>
    class DateTimeClient
    {
        #region 单例模式

        static DateTimeClient instance;
        static object _lock = new object();

        DateTimeClient()
        {

        }

        static DateTimeClient Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                            instance = new DateTimeClient();
                    }
                }

                return instance;
            }
        }

        #endregion

        DateTime m_MinTime = DateTime.Parse("1900-01-01 00:00:00");

        /// <summary>
        /// 开始同步时间。
        /// </summary>
        DateTime m_SyncTime = DateTime.Now;

        /// <summary>
        /// 远程返回时间。
        /// </summary>
        DateTime m_RemoteTime = DateTime.Now;

        public static void Initialize()
        {
            FuncTask<DateTime> task = new FuncTask<DateTime>();
            ITimeService service = ServiceContainer.GetService<ITimeService>();

            try
            {
                Instance.m_RemoteTime = service.GetCurrentTime();
                Instance.m_SyncTime = DateTime.Now;
            }
            catch { }
        }

        /// <summary>
        /// 取系统当前时间。
        /// </summary>
        /// <returns>当前系统时间。</returns>
        public static DateTime CurrentTime
        {
            get
            {
                return Instance.m_RemoteTime.Add(DateTime.Now.Subtract(Instance.m_SyncTime));
            }
        }

        /// <summary>
        /// 取系统最小时间。
        /// </summary>
        /// <returns>系统最小时间。</returns>
        public static DateTime MinTime
        {
            get
            {
                return Instance.m_MinTime;
            }
        }
    }
}
