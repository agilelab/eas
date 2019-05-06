using System;
using System.Data;
using EAS.Data.Access;
using EAS.Data.ORM;
using EAS.Data;

namespace EAS.Explorer.Entities
{
    /// <summary>
    /// 实体对象 Certificate(证书记录)。
    /// </summary>
    public partial class Certificate : DataEntity<Certificate>
    {
        /// <summary>
        /// 状态 。
        /// </summary>
        [Column("StateName", "状态"), DataSize(64)]
        [AutoUI(80, UIAlignment.Center)]
        public string StateName
        {
            get
           {
               switch (this.CertState)
               {
                   case (int)EAS.Explorer.CertState.Applying:
                       return "申请中";
                   case (int)EAS.Explorer.CertState.Audited:
                       return "已通过";
                   case (int)EAS.Explorer.CertState.Refused:
                       return "未通过";
                   case (int)EAS.Explorer.CertState.Obsolete:
                       return "已作废";
                   default:
                       return string.Empty;
               }
           }
        }
    }
}
