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
    [Module("CCDEE125-EC5B-4030-8375-7FE50E3302DD", "已办事宜", "通过Silverlight工作流平台查询已处理过的工作事务")]
    public partial class WfDoList : UserControl
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

        public WfDoList()
        {
            InitializeComponent();
        }

        void LoadInstanceList()
        {
            this.btnTrack.IsEnabled = this.btnExecute.IsEnabled = false;

            DateTime startTime = this.dtpStart.SelectedDate.HasValue == true ? dtpStart.SelectedDate.Value.Date : DateTime.Now.Date;
            DateTime endTime = this.dtpEnd.SelectedDate.HasValue == true ? this.dtpEnd.SelectedDate.Value.Date : DateTime.Now.Date;
            string flowID = this.tbKey.Tag.ToString();
            int isComplate = this.checkBox1.IsChecked.HasValue ? (this.checkBox1.IsChecked.Value ? 1 : 0) : 0;

            FuncTask<IList<WFInstance>> task = new FuncTask<IList<WFInstance>>();
            IWorkflowService wfService = ServiceContainer.GetService<IWorkflowService>(task);
            wfService.GetDoWorkflows(ContextHelper.Account.LoginID, flowID, startTime, endTime.AddDays(1), isComplate);

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
            this.btnTrack.IsEnabled = this.btnExecute.IsEnabled = false;
            if (this.vList == null) return;

            this.dgFlow.ItemsSource  = this.vList.Skip(this.dataPager.Skip).Take(this.dataPager.Take).ToList();
        }

        private void dgFlow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.btnTrack.IsEnabled = this.btnExecute.IsEnabled = e.AddedItems.Count > 0;
        }

        private void dgFlow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                this.btnTrack_Click(this.btnTrack, e);
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

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
