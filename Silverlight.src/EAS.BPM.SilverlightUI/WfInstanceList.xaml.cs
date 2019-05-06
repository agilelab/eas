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
    [Module("1153DC96-0AC8-4F42-803E-7130367D07D4", "流程实例", "通过Silverlight工作流平台管理监控工作流平台中的流程实例")]
    public partial class WfInstanceList : UserControl
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

        IQueryable<WFInstance> vList = null;

        public WfInstanceList()
        {
            InitializeComponent();
        }

        void LoadInstanceList()
        {
            this.btnTrack.IsEnabled = this.btnExecute.IsEnabled = this.btnKill.IsEnabled = false;

            DateTime startTime = this.dtpStart.SelectedDate.HasValue == true ? dtpStart.SelectedDate.Value.Date : DateTime.Now.Date;
            DateTime endTime =this.dtpEnd.SelectedDate.HasValue == true ? this.dtpEnd.SelectedDate.Value.Date : DateTime.Now.Date;
            string flowID = this.tbKey.Tag.ToString();
            int isComplate = this.checkBox1.IsChecked.HasValue ? (this.checkBox1.IsChecked.Value ?1 : 0) :0;

            DataEntityQuery<WFInstance> query = new DataEntityQuery<WFInstance>();

            var v = from c in query
                    where c.CreateTime >= startTime && c.CreateTime <= endTime.AddDays(1)
                    select new WFInstance
                    {
                        ID = c.ID,
                        FlowID = c.FlowID,
                        FlowName = c.FlowName,
                        Handler = c.Handler,
                        Subject = c.Subject,
                        CreateTime = c.CreateTime,
                        ProcessTime = c.ProcessTime,
                        ComplateTime = c.ComplateTime,
                        Sender = c.Sender,
                        CurrentState = c.CurrentState,
                        IsComplate = c.IsComplate
                    };

            if (flowID.Length > 0)
            {
                v = v.Where(p => p.FlowID == flowID);
            }
            if (isComplate > 0)
            {
                v = v.Where(p => p.IsComplate != 0);
            }
            else
            {
                v = v.Where(p => p.IsComplate == 0);
            }
            v = v.OrderByDescending(p => p.CreateTime);

            this.vList = v as IQueryable<WFInstance>;

            DataPortal<WFInstance> dataPortal = new DataPortal<WFInstance>();
            dataPortal.BeginExecuteCountQuery(this.vList).Completed += (s, e)
                =>
                {
                    QueryTask<WFInstance> task = s as QueryTask<WFInstance>;
                    if (task.Error != null)
                    {
                        MessageBox.Show("请求数据时发生错误：" + task.Error.Message, "错误", MessageBoxButton.OK);
                    }
                    else
                    {
                        this.dataPager.RecordCount = task.Count;
                    }
                };
        }

        private void dataPager_PageChanged(object sender, RoutedEventArgs e)
        {
            this.btnTrack.IsEnabled = this.btnExecute.IsEnabled = this.btnKill.IsEnabled = false;
            if (this.vList == null) return;

            var v = this.vList.Skip(this.dataPager.Skip).Take(this.dataPager.Take);
            DataPortal<WFInstance> dataPortal = new DataPortal<WFInstance>();
            dataPortal.BeginExecuteQuery(v).Completed += (s, e2)
                =>
            {
                QueryTask<WFInstance> task = s as QueryTask<WFInstance>;
                if (task.Error != null)
                {
                    MessageBox.Show("请求数据时发生错误：" + task.Error.Message, "错误", MessageBoxButton.OK);
                }
                else
                {
                    this.dgFlow.ItemsSource = task.Entities;
                }
            };
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

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            IList<WFInstance> vList = this.dgFlow.ItemsSource as IList<WFInstance>;
            int count = vList.Where(p => p.Checked).Count();
            this.btnKill.IsEnabled = count > 0;
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

        private void btnKill_Click(object sender, RoutedEventArgs e)
        {
            IList<WFInstance> vList = this.dgFlow.ItemsSource as IList<WFInstance>;
            IList<WFInstance> xList = vList.Where(p => p.Checked).ToList();
            if (xList.Count > 0)
            {
                MsgBoxWindow.MsgBoxCloseCallBack callback = (s, e2) =>
                    {
                        MsgBoxWindow.MsgBoxCloseCallBackArgs ex = e2 as MsgBoxWindow.MsgBoxCloseCallBackArgs;

                        int index = 0;

                        string cause = string.Format("{0}手动终止", ContextHelper.Account.LoginID);
                        InvokeTask task = new InvokeTask();
                        IWorkflowService service = ServiceContainer.GetService<IWorkflowService>(task);
                        service.Terminate(new Guid(xList[index].ID), cause);
                        task.Completed += (s3, e3) =>
                            {
                                if (task.Error != null)
                                {
                                    MessageBox.Show("请求数据时发生错误：" + task.Error.Message, "错误", MessageBoxButton.OK);
                                    return;
                                }
                                else
                                {
                                    if (index == xList.Count - 1)
                                    {
                                        this.dgFlow.ItemsSource = vList;
                                    }
                                    else
                                    {
                                        vList.Remove(xList[index]);
                                        index++;
                                        service.Terminate(new Guid(xList[index].ID), cause);
                                    }
                                }
                            };

                        for (int i = 0; i < xList.Count; i++)
                        {
                            service.Terminate(new Guid(xList[i].ID), cause);
                            vList.Remove(xList[i]);
                        }
                        foreach (var item in xList)
                        {
                            service.Terminate(new Guid(item.ID), cause);
                            vList.Remove(item);
                        }
                    };

                MsgBoxWindow msgBox = new MsgBoxWindow();
                msgBox.Show("操作提示", "确定要终止选中的流程实例？", MsgBoxWindow.MsgIcon.Question, MsgBoxWindow.MsgBtns.YesNo, callback);
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
