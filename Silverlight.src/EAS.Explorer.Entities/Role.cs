using System;

using EAS.Data.Access;
using EAS.Data.ORM;

namespace EAS.Explorer.Entities
{
   /// <summary>
   /// 实体对象 Role(角色信息)。
   /// </summary>
   public partial class Role: DataEntity<Role>
   {
       public override string ToString()
       {
           return this.Name;
       }
   }
}
