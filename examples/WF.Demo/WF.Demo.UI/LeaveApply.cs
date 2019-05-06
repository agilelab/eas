using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EAS.Modularization;
using EAS.Workflow;
using WF.Demo.DAL;
using EAS.BPM.BLL;
using EAS.Services;
using EAS.Explorer;

namespace EAS.BPM.WinDemo
{
    [Module("79700B78-EAE3-438D-8A04-F88FF5088B4A", "请假申请", "请假申请审批处理表单")]
    [WorkflowAddIn("da93f925-edeb-4053-bcc4-38a337659dd1", typeof(Leave))]
    public partial class LeaveApply : UserControl
    {
        [ModuleStart]
        public void StartEx()
        {

        }

        /// <summary>
        /// 工作流引擎驱动显示表单接口。
        /// </summary>
        [WorkflowInstanceId()]
        public Guid InstanceId
        {
            get
            {
                return m_InstanceId;
            }
            set
            {
                this.m_InstanceId = value;
                this.LoadLeaveData();
            }
        }        

        Guid m_InstanceId = Guid.Empty;
        Leave m_Leave = null;

        public LeaveApply()
        {
            InitializeComponent();
            this.tbGuid.Text = Guid.NewGuid().ToString().ToUpper();

            try
            {
                this.tbName.Text = (EAS.Application.Instance.Session.Client as IAccount).Name;
                this.nudDays.Value = 3;
            }
            catch { }
        }

        void LoadLeaveData()
        {
            IWorkflowService service = ServiceContainer.GetService<IWorkflowService>();
            WorkflowResult result = service.GetWorkflowDataEntity(this.m_InstanceId);
            this.OnWorkflowResultChaned(result);

            this.m_Leave = result.DataEntity as Leave;
            this.tbGuid.Text = this.m_Leave.ID;
            this.tbName.Text = this.m_Leave.Name;
            this.nudDays.Value = this.m_Leave.Days;
            this.tbCause.Text = this.m_Leave.Cause;
        }

        void OnWorkflowResultChaned(WorkflowResult result)
        {
            this.btnSubmit.Enabled = result.Submit;
            this.btnApproval.Enabled = result.Approval;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            this.btnSubmit.Enabled = false;

            WorkflowResult result = null;
            if (this.InstanceId == Guid.Empty)
            {
                this.m_Leave = new Leave();
                this.m_Leave.ID = this.tbGuid.Text;
                this.m_Leave.Name = this.tbName.Text;
                this.m_Leave.Days = (int)this.nudDays.Value;
                this.m_Leave.Cause = tbCause.Text;
                this.m_Leave.iState = 0;
                result = EAS.Application.Instance.WorkflowRuntime.CreateWorkflow(this, this.m_Leave);
                this.m_InstanceId = result.InstanceId;
            }
            else
            {
                result = EAS.Application.Instance.WorkflowRuntime.Submit(this);
            }

            this.OnWorkflowResultChaned(result);
            this.btnClose_Click(this.btnClose, e);
        }

        private void btnApproval_Click(object sender, EventArgs e)
        {
            WorkflowResult result = EAS.Application.Instance.WorkflowRuntime.Approval(this);
            this.OnWorkflowResultChaned(result);
            this.btnClose_Click(this.btnClose, e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            EAS.Application.Instance.CloseModule(this);
        }
    }
}
