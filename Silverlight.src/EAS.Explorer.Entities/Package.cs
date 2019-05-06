using System;

using EAS.Data.Access;
using EAS.Data.ORM;

namespace EAS.Explorer.Entities
{
   /// <summary>
   /// 实体对象 Package(模块包)。
   /// </summary>
   public partial class Package: DataEntity<Package>
   {
       public override string ToString()
       {
           return this.Name;
       }
   }
}
