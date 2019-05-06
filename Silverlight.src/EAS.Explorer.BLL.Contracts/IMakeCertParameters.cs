using System;
using System.Collections.Generic;
using System.Text;

namespace EAS.Explorer.BLL
{
    /// <summary>
    /// 证书生成参数。
    /// </summary>
    public interface IMakeCertParameters
    {
        /// <summary>
        /// 证书生成工具路径。
        /// </summary>
        string MakeCertPath
        {
            get;
            set;
        }

        /// <summary>
        /// 证书在放路径。
        /// </summary>
        string CertSavePath
        {
            get;
            set;
        }

        /// <summary>
        /// 证书文件名称。
        /// </summary>
        string CertFileName
        {
            get;
            set;
        }

        /// <summary>
        /// 证书有效期
        /// </summary>
        int LimitTime
        {
            get;
            set;
        }

        /// <summary>
        /// 是否需要审核。
        /// </summary>
        bool Auditing
        {
            get;
            set;
        }
    }
}
