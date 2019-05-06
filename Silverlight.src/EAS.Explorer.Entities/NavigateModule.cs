using System;

using EAS.Data.Access;
using EAS.Data.ORM;

namespace EAS.Explorer.Entities
{
    /// <summary>
    /// 实体对象 NavigateModule(模块/分组信息)。
    /// </summary>
    public partial class NavigateModule : INavigateModule
    {
        public override string ToString()
        {
            return this.Name;
        }
    }
}
