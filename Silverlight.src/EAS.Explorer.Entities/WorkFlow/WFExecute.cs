using System;
using System.Data;
using EAS.Data.Access;
using EAS.Data.ORM;
using EAS.Explorer;

namespace EAS.BPM.Entities
{
   /// <summary>
   /// 实体对象 WFExcute(流程执行信息)。
   /// </summary>
   public partial class WFExecute: DataEntity<WFExecute>
   {
       /// <summary>
       /// 动作名称。
       /// </summary>
       public string ActionName
       {
           get
           {
               switch (this.Action)
               {
                   case Consts.Wf_Submit:
                       return Consts.Wf_Submit_Text;
                   case Consts.Wf_Approval:
                       return Consts.Wf_Approval_Text;
                   default:
                       return string.Empty;
               }
           }
       }

       /// <summary>
       /// 处理结果。
       /// </summary>
       public string ResultText
       {
           get
           {
               switch (this.Action)
               {
                   case Consts.Wf_Submit:
                       return string.Empty;
                   case Consts.Wf_Approval:
                       return this.Result == Consts.Wf_Agree ? Consts.Wf_Agree_Text:Consts.Wf_Disagree_Text;
                   default:
                       return string.Empty;
               }
           }
       }
   }
}
