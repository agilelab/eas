using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using EAS.Workflow;
using EAS.BPM.BLL;
using EAS.Services;

namespace EAS.BPM.SilverlightUI
{
    /// <summary>
    /// 工作流运行时环境。
    /// </summary>
    public class WorkflowRuntime : IWorkflowRuntime
    {
        #region IWorkflowRuntime成员

        public FuncTask<WorkflowResult> CreateWorkflow(object wfAddIn, IWorkflowDataEntity wfData)
        {
            FuncTask<WorkflowResult> task = new FuncTask<WorkflowResult>();
            WorkflowResult wfResult = this.GetFlowID(wfAddIn);
            if (wfResult.FlowID == Guid.Empty)
            {
                task.Complete(wfResult);
            }
            else
            {
                IWorkflowService service = ServiceContainer.GetService<IWorkflowService>(task);
                service.CreateWorkflow(wfResult.FlowID, wfData, ContextHelper.Account.LoginID);
            }

            return task;
        }

        public FuncTask<WorkflowResult> Submit(object wfAddIn)
        {
            FuncTask<WorkflowResult> task = new FuncTask<WorkflowResult>();
            WorkflowResult wfResult = this.GetInstanceID(wfAddIn);
            if (wfResult.InstanceId != Guid.Empty)
            {
                WfSubmitWindow wfSubmit = new WfSubmitWindow();
                wfSubmit.Closed += (s, e) =>
                    {
                        bool flag = wfSubmit.DialogResult.HasValue ? wfSubmit.DialogResult.Value : false;
                        if (flag)
                        {
                            task = this.Submit(wfResult.InstanceId, ContextHelper.Account.LoginID, wfSubmit.Comment);
                        }
                        else
                        {
                            task.Complete(wfResult);
                        }
                    };
                wfSubmit.Show();
            }
            else
            {
                task.Complete(wfResult);
            }

            return task;
        }

        public FuncTask<WorkflowResult> Approval(object wfAddIn)
        {
            FuncTask<WorkflowResult> task = new FuncTask<WorkflowResult>();
            WorkflowResult wfResult = this.GetInstanceID(wfAddIn);
            if (wfResult.InstanceId != Guid.Empty)
            {
                WfApprovalWindow wfApproval = new WfApprovalWindow();
                wfApproval.Closed += (s, e) =>
                {
                    bool flag = wfApproval.DialogResult.HasValue ? wfApproval.DialogResult.Value : false;
                    if (flag)
                    {
                        task = this.Approval(wfResult.InstanceId, ContextHelper.Account.LoginID, wfApproval.Result, wfApproval.Comment);
                    }
                    else
                    {
                        task.Complete(wfResult);
                    }
                };
                wfApproval.Show();
            }
            else
            {
                task.Complete(wfResult);
            }
            return task;
        }

        public FuncTask<WorkflowResult> Submit(Guid instanceID, string loginID, string comment)
        {
            FuncTask<WorkflowResult> task = new FuncTask<WorkflowResult>();
            IWorkflowService service = ServiceContainer.GetService<IWorkflowService>(task);
            service.Submit(instanceID, loginID, comment);
            return task;
        }

        public FuncTask<WorkflowResult> Approval(Guid instanceID, string loginID, bool result, string comment)
        {
            FuncTask<WorkflowResult> task = new FuncTask<WorkflowResult>();
            IWorkflowService service = ServiceContainer.GetService<IWorkflowService>(task);
            service.Approval(instanceID, loginID, result, comment);
            return task;
        }

        public FuncTask<WorkflowResult> GetWorkFlowResult(Guid instanceID)
        {
            FuncTask<WorkflowResult> task = new FuncTask<WorkflowResult>();
            IWorkflowService service = ServiceContainer.GetService<IWorkflowService>(task);
            service.GetWorkFlowResult(instanceID);
            return task;
        }

        public FuncTask<WorkflowResult> GetWorkflowDataEntity(Guid instanceID)
        {
            FuncTask<WorkflowResult> task = new FuncTask<WorkflowResult>();
            IWorkflowService service = ServiceContainer.GetService<IWorkflowService>(task);
            service.GetWorkflowDataEntity(instanceID);
            return task;
        }

        #endregion

        #region 读本地流程ID/实例ID

        WorkflowResult GetFlowID(object wfAddIn)
        {
            WorkflowResult wfResult = new WorkflowResult();
            if (wfAddIn == null)
            {
                wfResult.Error = new ArgumentNullException("wfAddIn");
                return wfResult;
            }

            System.Type T = wfAddIn.GetType();
            WorkflowAddInAttribute wfa = Attribute.GetCustomAttribute(T, typeof(WorkflowAddInAttribute)) as WorkflowAddInAttribute;
            if (Object.Equals(null, wfa))
            {
                wfResult.Error = new ArgumentException("wfAddIn");
                //wfResult.Error = new Exception("wfAddIn");
                return wfResult;
            }

            wfResult.FlowID = new Guid(wfa.FlowID);
            return wfResult;
        }

        WorkflowResult GetInstanceID(object wfAddIn)
        {
            WorkflowResult wfResult = new WorkflowResult();
            if (wfAddIn == null)
            {
                wfResult.Error = new ArgumentNullException("wfAddIn");
                return wfResult;
            }

            System.Type T = wfAddIn.GetType();
            WorkflowAddInAttribute wfa = Attribute.GetCustomAttribute(T, typeof(WorkflowAddInAttribute)) as WorkflowAddInAttribute;
            if (Object.Equals(null, wfa))
            {
                wfResult.Error = new ArgumentException("wfAddIn");
                return wfResult;
            }

            System.Reflection.PropertyInfo propertyInfo = null;
            foreach (System.Reflection.PropertyInfo item in T.GetProperties())
            {
                WorkflowInstanceIdAttribute wfai = Attribute.GetCustomAttribute(item, typeof(WorkflowInstanceIdAttribute)) as WorkflowInstanceIdAttribute;
                if (!Object.Equals(null, wfai))
                {
                    propertyInfo = item;
                    break;
                }
            }

            if (Object.Equals(null, propertyInfo))
            {
                wfResult.Error = new Exception("没有发现\"WorkflowInstanceId\"属性标记");
                return wfResult;
            }

            wfResult.InstanceId = (Guid)propertyInfo.GetValue(wfAddIn, new object[] { });
            return wfResult;
        }

        #endregion
    }
}
