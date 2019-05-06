using System;

using EAS.Data.Access;
using EAS.Data.ORM;

namespace EAS.Explorer.Entities
{
   /// <summary>
   /// 实体对象 Report(报表定义信息)。
   /// </summary>
   public partial class Report: DataEntity<Report>
   {
       public override string ToString()
       {
           return this.Name;
       }

       #region 报表类型

       #if !SILVERLIGHT

       public ReportType RType
       {
           get
           {
               if ((this.Attributes & 1) == 1)
               {
                   return ReportType.RDL;
               }
               else if ((this.Attributes & 2) == 2)
               {
                   return ReportType.GReport;
               }
               {
                   return ReportType.RDL;
               }
           }
           set
           {
               if (value == ReportType.GReport)
               {
                   this.Attributes &= 0xffffffe;
                   this.Attributes |= 2;
               }
               else
               {
                   this.Attributes &= 0xffffffd;
                   this.Attributes |= 1;
               }
           }
       }
#endif

       #endregion

       #region 发布模块

       /// <summary>
       /// 发布为模块
       /// </summary>
       public bool Module
       {
           get
           {
               return (this.Attributes & 0x10000) == 0x10000;
           }
           set
           {
               if (value)
               {
                   this.Attributes |= 0x10000;
               }
               else
               {
                   this.Attributes &= 0xffeffff;
               }
           }
       }

       #endregion
   }
}
