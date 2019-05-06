using System;
using System.Data;
using EAS.Data;
using EAS.Data.Access;
using EAS.Data.ORM;

namespace EAS.Explorer.Entities
{
   /// <summary>
   /// 实体对象 Desktop(桌面定义)。
   /// </summary>
   public partial class DesktopItem: DataEntity<DesktopItem>
   {
       /// <summary>
       /// 排序码 。
       /// </summary>
       [Column("SORTCODE", "排序码"), DataSize(10),Virtual]
       public int SortCode
       {
           get
           {
               return this.GetValue<int>("SORTCODE");
           }
           set
           {
               this["SORTCODE"] = value;
           }
       }

   }
}
