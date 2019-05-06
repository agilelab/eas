using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using EAS.Modularization;
using EAS.BPM.Entities;
using EAS.BPM.BLL;
using EAS.Services;
using EAS.Data.Linq;
using EAS.Data;

namespace EAS.BPM.SilverlightUI
{
    [Module("7DAFFAB4-5DE4-4E18-AD76-739FBB524DA7", "待办事宜", "通过Silverlight工作流平台处理日常的工作事务")]
    public partial class WfToDoList : UserControl
    {
        [ModuleStart]
        public void StartEx()
        {
            DateTime m_Time = EAS.Application.Instance.Time.Date;
            this.dtpStart.SelectedDate = m_Time.AddDays(-7);
            this.dtpEnd.SelectedDate = m_Time;
            this.tbKey.Tag = string.Empty;
            this.LoadInstanceList();
        }

        IList<WFInstance> vList = null;

        public WfToDoList()
        {
            InitializeComponent();
        }

        void LoadInstanceList()
        {
            this.btnTrack.IsEnabled = this.btnExecute.IsEnabled  = this.btnProcess.IsEnabled = false;

            DateTime startTime = this.dtpStart.SelectedDate.HasValue == true ? dtpStart.SelectedDate.Value.Date : DateTime.Now.Date;
            DateTime endTime = this.dtpEnd.SelectedDate.HasValue == true ? this.dtpEnd.SelectedDate.Value.Date : DateTime.Now.Date;
            string flowID = this.tbKey.Tag.ToString();            

            FuncTask<IList<WFInstance>> task = new FuncTask<IList<WFInstance>>();
            IWorkflowService wfService = ServiceContainer.GetService<IWorkflowService>(task);
            wfService.GetToDoWorkflows(ContextHelper.Account.LoginID, flowID, startTime, endTime.AddDays(1));

            task.Completed += (s, e)
                =>
                {
                    if (task.Error != null)
                    {
                        MessageBox.Show("请求数据时发生错误：" + task.Error.Message, "错误", MessageBoxButton.OK);
                    }
                    else
                    {
                        this.vList = task.TResult;
                        this.dataPager.RecordCount = this.vList.Count;
                    }
                };
        }

        private void dataPager_PageChanged(object sender, RoutedEventArgs e)
        {
            this.btnTrack.IsEnabled = this.btnExecute.IsEnabled = this.btnProcess.IsEnabled = false;
            if (this.vList == null) return;

            this.dgFlow.ItemsSource  = this.vList.Skip(this.dataPager.Skip).Take(this.dataPager.Take).ToList();
        }

        private void dgFlow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.btnTrack.IsEnabled = this.btnExecute.IsEnabled = this.btnProcess.IsEnabled = e.AddedItems.Count > 0;
        }

        private void dgFlow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                this.btnProcess_Click(this.btnProcess, e);
            }
        }

        private void btnQuery_Click(object sender, RoutedEventArgs e)
        {
            this.LoadInstanceList();
        }

        private void btnTrack_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgFlow.SelectedItem != null)
            {
                WFInstance wfInstance = this.dgFlow.SelectedItem as WFInstance;
                WfTrackWindow wfTrack = new WfTrackWindow();
                wfTrack.WFInstance = wfInstance;
                wfTrack.Show();
            }
        }

        private void btnExecute_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgFlow.SelectedItem != null)
            {
                WFInstance wfInstance = this.dgFlow.SelectedItem as WFInstance;
                WfExecuteWindow window = new WfExecuteWindow();
                window.InstanceID = wfInstance.ID;
                window.Show();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (EAS.Application.Instance != null)
            {
                EAS.Application.Instance.CloseModule(this);
            }
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            WfDefineInput wfForm = new WfDefineInput();
            wfForm.Closed += (s, e2) =>
                {
                    if (wfForm.DialogResult.HasValue)
                    {
                        if (wfForm.DialogResult.Value)
                        {
                            this.tbKey.Text = wfForm.SelectedItem.Name;
                            this.tbKey.Tag = wfForm.SelectedItem.FlowID;
                        }
                    }
                };
            wfForm.Show();
        }

        private void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgFlow.SelectedItem != null)
            {
                WFInstance wfInstance = this.dgFlow.SelectedItem as WFInstance;
                DateTime T1 = EAS.Application.Instance.Time;

                //回调函数。
                WfAddInHelper.WorkflowInstanceCallBack callback = () =>
                    {
                        IList<WFInstance> xList = this.dgFlow.ItemsSource as IList<WFInstance>;

                        if (xList.Contains(wfInstance))
                        {
                            DataPortal<WFInstance> dataPortal = new DataPortal<WFInstance>();
                            dataPortal.BeginRead(wfInstance).Completed += (s1, e1) =>
                                {
                                    QueryTask<WFInstance> task = s1 as QueryTask<WFInstance>;
                                    string[] sv = task.DataEntity.Handlers.Split(',');
                                    if (sv.Where(p => p == ContextHelper.Account.LoginID).Count() > 0)
                                    {
                                        xList.Remove(wfInstance);
                                        this.dgFlow.ItemsSource = null;
                                        this.dgFlow.ItemsSource = xList;
                                    }
                                };
                        }
                    };

                //处理流程
                WfAddInHelper.LoadWorkflowInstance(wfInstance, true, callback);
            }
        }        
    }
}
