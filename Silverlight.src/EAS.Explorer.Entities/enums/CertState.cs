using System;
using System.Collections.Generic;
using System.Text;

namespace EAS.Explorer
{
    /// <summary>
    /// 证书状态。
    /// </summary>
    public enum CertState
    {
        /// <summary>
        /// 证书申请中。
        /// </summary>
        Applying = 1, 

        /// <summary>
        /// 审核已通过。
        /// </summary>
        Audited  = 2,

        /// <summary>
        /// 申请未通过。
        /// </summary>
        Refused = 3, 

        /// <summary>
        /// 证书已作废。
        /// </summary>
        Obsolete = 4,        
    }
}
