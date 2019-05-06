using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAS.Data.ORM;

namespace EAS.Explorer
{
    /// <summary>
    /// 导航分组接口。
    /// </summary>
    public interface INavigateGroup : IDataEntity
    {
        /// <summary>
        /// ID。
        /// </summary>
        string ID { get; set; }

        /// <summary>
        /// 名称。
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 属性。
        /// </summary>
        int Attributes { get; set; }
        
        /// <summary>
        /// 说明。
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// 图标。
        /// </summary>
        byte[] Icon { get; set; }       

        /// <summary>
        /// 上级ID。
        /// </summary>
        string ParentID { get; set; }

        /// <summary>
        /// 排序码。
        /// </summary>
        int SortCode { get; set; }

        /// <summary>
        /// 关联流程模块。
        /// </summary>
        string WFModule { get; set; }
    }
}
