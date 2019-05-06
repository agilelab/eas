using System;

using EAS.Data.Access;
using EAS.Data.ORM;
using EAS.Data;

namespace EAS.Explorer.Entities
{
   /// <summary>
   /// 实体对象 Module(模块信息表)。
   /// </summary>
   public partial class Module: DataEntity<Module>
   {
       [AutoUI(Caption = "类别", Width = 80, Alignment = UIAlignment.Left, Index = 10)]
       //[Column("MTName", "MTName"),Virtual]
       public string MTName
       {
           get
           {
               switch (this.Attributes % 0x0100)
               {
                   case (int)GoComType.WinUI:
                       return "WinUI";
                   case (int)GoComType.WebUI:
                       return "WebUI";
                   case (int)GoComType.SilverUI:
                       return "SilverUI";
                   case (int)GoComType.Business:
                       return "Business";
                   default:
                       return "Function";
               }
           }
       }

       /// <summary>
       /// 常用模块。
       /// </summary>
       public bool Used
       {
           get
           {
               return (this.Attributes & 0x2000) == 0x2000;
           }
           set
           {
               if (value)
               {
                   this.Attributes |= 0x2000;
               }
               else
               {
                   this.Attributes &= 0x0fffdfff;
               }
           }
       }

       /// <summary>
       /// 工作流插件。
       /// </summary>
       public bool WorkFlowAddIn
       {
           get
           {
               return (this.Attributes & 0x1000) == 0x1000;
           }
           set
           {
               if (value)
               {
                   this.Attributes |= 0x1000;
               }
               else
               {
                   this.Attributes &= 0x0fffefff;
               }
           }
       }

       public override string ToString()
       {
           return string.Format("{0}({1})", this.Name, this.Guid);
       }
   }
}
