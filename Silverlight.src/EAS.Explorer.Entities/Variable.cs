using System;
using System.Data;
using EAS.Data;
using EAS.Data.Access;
using EAS.Data.ORM;

namespace EAS.Explorer.Entities
{
   /// <summary>
   /// 实体对象 Variable(内部变量定义)。
   /// </summary>
   public partial class Variable: DataEntity<Variable>
   {
       /// <summary>
       /// 值 。
       /// </summary>
       public object Value
       {
           get;
           set;
       }

       /// <summary>
       /// 文本值。
       /// </summary>
       public object TextValue
       {
           get
           {
               return string.Empty;
           }
       }
   }
}
