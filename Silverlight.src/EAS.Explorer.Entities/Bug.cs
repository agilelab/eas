using System;
using System.Data;
using EAS.Data.Access;
using EAS.Data.ORM;
using EAS.Data;

namespace EAS.Explorer.Entities
{
   /// <summary>
   /// 实体对象 Bug(Bug记录)。
   /// </summary>
   public partial class Bug: DataEntity<Bug>
   {
       /// <summary>
       /// 状态 。
       /// </summary>
       //[Column("StateName", "状态"), DataSize(64),Virtual]
       [AutoUI(Width= 70, Alignment = UIAlignment.Center, Index = 8)]
       [System.ComponentModel.DisplayName("状态")]
       public string StateName
       {
           get
           {
               switch (this.BugState)
               {
                   case 0:
                       return "已提交";
                   case 1:
                       return "处理中";
                   case 2:
                       return "己解决";
                   case 3:
                       return "未解决";
                   default:
                       return string.Empty;
               }
           }
       }
   }
}
