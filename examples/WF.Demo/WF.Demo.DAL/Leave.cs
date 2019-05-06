using System;
using System.Data;
using EAS.Data.Access;
using EAS.Data.ORM;
using EAS.Workflow;

namespace WF.Demo.DAL
{
   /// <summary>
   /// 实体对象 Leave(请假记录)。
   /// </summary>
   public partial class Leave: DataEntity<Leave>
   {
       /// <summary>
       /// 状态。
       /// </summary>
       public string iStateText
       {
           get
           {
               switch (this.iState)
               {
                   case 1:
                       return "已指准";
                   case -1:
                       return "未指准";
                   case 2:
                       return "已执行";
                   default:
                       return "已申请";
               }
           }
       }

       /// <summary>
       /// 工作流申批完成之后执行查操作。
       /// </summary>
       /// <remarks>
       /// 本方法用于工作流服务回调，用于实现工作流的完成通知。
       /// </remarks>
       [WorkflowComplated]
       public void OnWorkflowComplated()
       {
           this.iState = 1;
           this.Save();
       }

       /// <summary>
       /// 工作流申批终止之后执行查操作。
       /// </summary>
       /// <remarks>
       /// 本方法用于工作流服务回调，用于实现工作流的终止通知。
       /// </remarks>
       [WorkflowTerminated]
       public void OnWorkflowTerminated()
       {
           this.iState = -1;
           this.Save();
       }

       /// <summary>
       /// 流程实例主题/摘要。
       /// </summary>
       /// <returns></returns>
       public override string ToString()
       {
           return string.Format("{0}请假{1}天,事由:{2}", this.Name, this.Days, this.Cause);
       }
   }
}
