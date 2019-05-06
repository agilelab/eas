using System;
using System.Data;
using EAS.Data;
using EAS.Data.Access;
using EAS.Data.ORM;

namespace EAS.Explorer.Entities
{
   /// <summary>
   /// 实体对象 InputDict(输入字典)。
   /// </summary>
   public partial class InputDict: DataEntity<InputDict>
   {
       public override string ToString()
       {
           return this.Name;
       }
   }
}
