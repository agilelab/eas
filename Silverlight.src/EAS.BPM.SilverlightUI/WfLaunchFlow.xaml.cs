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

namespace EAS.BPM.SilverlightUI
{
    [Module("788BED36-5488-478F-A4F4-58E5B8E01ECC","发起流程","通过Silverlight工作流平台发起一个新的流程实例")]
    public partial class WfLaunchFlow : UserControl
    {
        [ModuleStart]
        public void StartEx()
        {
            this.LoadFlowList();
        }

        IList<WFDefine> vList = null;

        public WfLaunchFlow()
        {
            InitializeComponent();
        }

        void LoadFlowList()
        {
            this.btnStart.IsEnabled = false;
            FuncTask<IList<WFDefine>> task = new FuncTask<IList<WFDefine>>();
            IWorkflowService wfService = ServiceContainer.GetService<IWorkflowService>(task);
            wfService.GetWFDefineList(SLContext.Account.LoginID, this.tbKey.Text.Trim());
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
            this.btnStart.IsEnabled = false;
            if (this.vList == null) return;
            this.dgFlow.ItemsSource = this.vList.Skip(this.dataPager.Skip).Take(this.dataPager.Take).ToList();
        }

        private void btnQuery_Click(object sender, RoutedEventArgs e)
        {
            this.LoadFlowList();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgFlow.SelectedItem != null)
            {
                WFDefine wfDefine = this.dgFlow.SelectedItem as WFDefine;
                WfAddInHelper.StartWorkflowInstance(wfDefine,() =>{});
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (EAS.Application.Instance != null)
            {
                EAS.Application.Instance.CloseModule(this);
            }
        }

        private void dgFlow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.btnStart.IsEnabled = e.AddedItems.Count > 0;
        }        
    }
}
