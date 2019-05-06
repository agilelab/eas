using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAS.Data.ORM;

namespace EAS.Explorer
{
    /// <summary>
    /// 导航模块接口。
    /// </summary>
    public interface INavigateModule : IDataEntity
    {
        /// <summary>
        /// Guid。
        /// </summary>
        string Guid { get; set; }

        /// <summary>
        /// 模块名称。
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 程序集。
        /// </summary>
        string Assembly { get; set; }

        /// <summary>
        /// 类型。
        /// </summary>
        string Type { get; set; }

        /// <summary>
        /// 描述。
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// 开发者。
        /// </summary>
        string Developer { get; set; }

        /// <summary>
        /// 所有组ID。
        /// </summary>
        string GroupID { get; set; }

        /// <summary>
        /// 组名。
        /// </summary>
        string GroupName { get; set; }
        
        /// <summary>
        /// 图标。
        /// </summary>
        byte[] Icon { get; set; }

        /// <summary>
        /// 最后更新时间。
        /// </summary>
        DateTime LMTime { get; set; }
        
        /// <summary>
        /// 所在程序包。
        /// </summary>
        string Package { get; set; }

        /// <summary>
        /// 排序码。
        /// </summary>
        int SortCode { get; set; }
        
        /// <summary>
        /// URL地址信息。
        /// </summary>
        string Url { get; set; }

        /// <summary>
        /// 版本号。
        /// </summary>
        string Version { get; set; }
    }
}
