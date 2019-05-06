using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAS.Workflow;
using EAS.BPM.Entities;

namespace EAS.BPM.BLL
{
    /// <summary>
    /// 工作流服务。
    /// </summary>
    public interface IWorkflowService
    {
        /// <summary>
        /// 改变工作流定义的Guid。
        /// </summary>
        /// <param name="sourceID">流程定义ID。</param>
        /// <remarks>
        /// 工作流版本升级，原来的流程ID需要让位于新版本，原有流程的ID变为一个新的Guid并且改变所以实例的ID。 
        /// </remarks>
        void ChangeWFDefineID(Guid sourceID); 

        /// <summary>
        /// 保存工作流定义。
        /// </summary>
        /// <param name="wfDefine">工作流定义对象。</param>
        void SaveWFDefine(WFDefineRoot wfDefine);

        /// <summary>
        /// 发布工作流定义。
        /// </summary>
        /// <param name="flowID">流程ID。</param>
        /// <param name="loginID">发布人。</param>
        void PublishWorkflow(Guid flowID,string loginID);

        /// <summary>
        /// 获取工作流定义。
        /// </summary>
        /// <param name="flowID">工作流Guid。</param>
        /// <returns>工作流定义对象。</returns>
        WFDefineRoot GetWFDefineRoot(Guid flowID);

        /// <summary>
        /// 获取定工作流定义。
        /// </summary>
        /// <param name="flowID">流程ID。</param>
        /// <returns>工作流定义。</returns>
        WFDefine GetWFDefine(Guid flowID);

        /// <summary>
        /// 查询指定账户具有发起权限的工作流定义。
        /// </summary>
        /// <param name="loginId">登录账号。</param>
        /// <param name="flowName">流程名称。</param>
        /// <returns>工作流定义清单。</returns>
        List<WFDefine> GetWFDefineList(string loginId,string flowName);

        /// <summary>
        /// 验证工作流定义。
        /// </summary>
        /// <param name="xomlString">xoml定义。</param>
        /// <param name="rulesString">规则定义。</param>
        /// <param name="variable">变量定义。</param>
        void VerifyWorkflow(string xomlString, string rulesString, string variable);

        /// <summary>
        /// 创建工作流实例。
        /// </summary>
        /// <param name="flowID">流程ID。</param>
        /// <param name="wfData">工作流业务数据。</param>
        /// <param name="loginID">登录账户。</param>
        /// <returns>工作流返回结果。</returns>
        WorkflowResult CreateWorkflow(Guid flowID, IWorkflowDataEntity wfData, string loginID);

        /// <summary>
        /// 创建工作流实例。
        /// </summary>
        /// <param name="instanceID">实例Guid。</param>
        /// <param name="flowID">工作流定义GUID。</param>
        /// <param name="wfData">工作流业务数据。</param>
        /// <param name="loginID">登录账户。</param>
        /// <returns>工作流返回结果。</returns>
        WorkflowResult CreateWorkflow(Guid flowID, Guid instanceID, IWorkflowDataEntity wfData, string loginID);

        /// <summary>
        /// 申请提交。
        /// </summary>
        /// <param name="instanceID">实例ID。</param>
        /// <param name="loginID">提交账号。</param>
        /// <param name="comment">提交原因。</param>
        /// <returns>工作流返回结果。</returns>
        WorkflowResult Submit(Guid instanceID, string loginID, string comment);

        /// <summary>
        /// 申请提交。
        /// </summary>
        /// <param name="instanceID">实例ID。</param>
        /// <param name="wfData">业务数据。</param>
        /// <param name="loginID">提交账号。</param>
        /// <param name="comment">提交原因。</param>
        /// <returns>工作流返回结果。</returns>
        WorkflowResult Submit(Guid instanceID, IWorkflowDataEntity wfData, string loginID, string comment);

        /// <summary>
        /// 审批提交。
        /// </summary>
        /// <param name="instanceID">实例ID。</param>
        /// <param name="loginID">审批账号。</param>
        /// <param name="result">审批结果。</param>
        /// <param name="comment">审批意见。</param>
        /// <returns>工作流返回结果。</returns>
        WorkflowResult Approval(Guid instanceID, string loginID, bool result, string comment);

        /// <summary>
        /// 审批提交。
        /// </summary>
        /// <param name="instanceID">实例ID。</param>
        /// <param name="wfData">业务数据。</param>
        /// <param name="loginID">审批账号。</param>
        /// <param name="result">审批结果。</param>
        /// <param name="comment">审批意见。</param>
        /// <returns>工作流返回结果。</returns>
        WorkflowResult Approval(Guid instanceID, IWorkflowDataEntity wfData, string loginID, bool result, string comment);

        /// <summary>
        /// 终止流程
        /// </summary>
        /// <param name="instanceID">实例ID。</param>
        /// <param name="cause">终止原因。</param>
        /// <returns>工作流返回结果。</returns>
        WorkflowResult Terminate(Guid instanceID,string cause);

        /// <summary>
        /// 取得待办流程实例。
        /// </summary>
        /// <param name="loginID">登录账号。</param>
        /// <param name="flowID">流程ID。</param>
        /// <param name="startTime">开始时间。</param>
        /// <param name="endTime">结束时间。</param>
        /// <returns>待办流程清单。</returns>
        List<WFInstance> GetToDoWorkflows(string loginID,string flowID,DateTime startTime,DateTime endTime);

        /// <summary>
        /// 取得已办流程实例。
        /// </summary>
        /// <param name="loginID">登录账号。</param>
        /// <param name="flowID">流程ID。</param>
        /// <param name="startTime">开始时间。</param>
        /// <param name="endTime">结束时间。</param>
        /// <param name="state">流程状态。</param>
        /// <returns>已办流程清单。</returns>
        List<WFInstance> GetDoWorkflows(string loginID, string flowID, DateTime startTime, DateTime endTime,int state);

        /// <summary>
        /// 取指定工作流实例ID的数据对象。
        /// </summary>
        /// <param name="instanceID">实例ID。</param>
        /// <returns>工作流返回结果。</returns>
        WorkflowResult GetWorkflowDataEntity(Guid instanceID);

        /// <summary>
        /// 取指定工作流实例ID的当前处理结果。
        /// </summary>
        /// <param name="instanceID">实例ID。</param>
        /// <returns>工作流返回结果。</returns>
        WorkflowResult GetWorkFlowResult(Guid instanceID);
    }
}
