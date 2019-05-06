using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAS.Data.ORM;

namespace EAS.Explorer
{
    /// <summary>
    /// 组织机构接口定义。
    /// </summary>
    public interface IOrganization:IDataEntity
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
        /// 组织机构名称。
        /// </summary>
        string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 上级组织机构。
        /// </summary>
        IOrganization Parent
        {
            get;
            set;
        } 
    }
}
