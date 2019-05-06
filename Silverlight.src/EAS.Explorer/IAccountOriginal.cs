using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAS.Data.ORM;

namespace EAS.Explorer
{
    /// <summary>
    /// 账户原型接口。
    /// </summary>
    public interface IAccountOriginal : IDataEntity
    {
        /// <summary>
        /// 唯一Guid。
        /// </summary>
        Guid Guid
        {
            get;
            set;
        }

        /// <summary>
        /// Key/主键值，与账户OriginalID相匹配。
        /// </summary>
        string Key
        {
            get;
            set;
        }

        /// <summary>
        /// 所在机构/部门。
        /// </summary>
        IOrganization Organization
        {
            get;
            set;
        }
    }
}
