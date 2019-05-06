using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EAS.Explorer.BLL
{
    /// <summary>
    /// 系统日志服务。
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// 记录日志。
        /// </summary>
        /// <param name="LoginID">登录ID。</param>
        /// <param name="Info">日志信息。</param>
        /// <param name="HostName">主机名称。</param>
        /// <param name="IpAddress">IP地址。</param>
        void Log(string LoginID, string Info, string HostName, string IpAddress);
    }
}
