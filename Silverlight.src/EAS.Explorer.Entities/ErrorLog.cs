using System;
using System.Data;
using EAS.Data;
using EAS.Data.Access;
using EAS.Data.ORM;
using System.Text;

namespace EAS.Explorer.Entities
{
   /// <summary>
   /// 实体对象 ErrorLog(错误日志记录)。
   /// </summary>
   public partial class ErrorLog: DataEntity<ErrorLog>
   {
       public override string ToString()
       {
           StringBuilder sb = new StringBuilder();
           if (this.Type == 0)
               sb.AppendLine("[类型]:错误");
           else
               sb.AppendLine("[类型]:消息");
           sb.AppendLine("[时间]:" + this.Time.ToString("yyyy-MM-dd HH:mm:ss"));
           sb.AppendLine("[内容]:" + this.Message);
           sb.AppendLine("[来源]:" + this.Source);
           sb.AppendLine("[方法]:" + this.TargetSite);
           sb.AppendLine("[堆栈]:" + this.StackTrace);
           return sb.ToString();
       }
   }
}
