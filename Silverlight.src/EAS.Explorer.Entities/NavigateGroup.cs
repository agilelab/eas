using System;

using EAS.Data.Access;
using EAS.Data.ORM;

namespace EAS.Explorer.Entities
{
    /// <summary>
    /// 实体对象 INavigateGroup(功能分组/导航信息)。
    /// </summary>
    public partial class NavigateGroup : DataEntity<NavigateGroup>, INavigateGroup
    {
        public override string ToString()
        {
            return this.Name;
        }
    }
}
