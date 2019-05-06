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
using EAS.Controls;
using EAS.Data.Linq;
using EAS.Explorer.Entities;
using EAS.Data;
using EAS.Data.ORM;
using EAS.Services;
using EAS.Explorer.BLL;

namespace EAS.SilverlightClient.AddIn
{
    [Module("E9C5715B-FC26-4756-AC71-0684A65E0364", "登录日志", "用于查询AgileEAS.NET平台应用的账号登录日志。")]
    public partial class LogList : UserControl
    {
        IQueryable<Log> vList = null;

        [ModuleStart]
        public void Start()
        {
            this.dpStart.SelectedDate = DateTime.Now.Date;
            this.dpEnd.SelectedDate = DateTime.Now.Date;
        }

        int pageIndex = 1;
        int iCount = 0;
        const int PageSize = 18;

        IList<Log> logList = null;

        public LogList()
        {
            InitializeComponent();
            this.pager.PageCount = 15;
            this.pager.OnPageIndexChange += new PageIndexChange(pager_OnPageIndexChange);
        }

        void pager_OnPageIndexChange(int pageIndex)
        {
            this.pageIndex = pageIndex;
            EAS.Controls.Window.ShowLoading("请求数据,第" + pageIndex + "页...");
            this.GetPageList(pageIndex);
        }

        void LoadDataList()
        {
            string login = this.tbLoginID.Text.Trim();
            string ip = this.tbIp.Text.Trim();

            DateTime T1 = this.dpStart.SelectedDate.HasValue ? this.dpStart.SelectedDate.Value.Date : DateTime.Now.Date;
            DateTime T2 = this.dpEnd.SelectedDate.HasValue ? this.dpEnd.SelectedDate.Value.Date.AddDays(1) : DateTime.Now.Date.AddDays(1);

            DataEntityQuery<Log> query = new DataEntityQuery<Log>();
            var v = from c in query
                    where c.EventTime >= T1 && c.EventTime <= T2.AddDays(1)
                    select c;

            if (login.Length > 0)
            {
                v =v.Where(p=>p.LoginID.StartsWith(login));
            }

            if (ip.Length > 0)
            {
                v = v.Where(p => p.IpAddress == ip || p.HostName == ip);
            }

            v = v.OrderByDescending(p => p.ID);

            this.vList = v as IQueryable<Log>;

            System.Threading.WaitCallback callBack = (state) =>
            {
                try
                {
                    this.iCount = v.Count();
                }
                catch(System.Exception exc )
                {
                    Dispatcher.BeginInvoke(() =>
                    {
                        EAS.Controls.Window.HideLoading();
                        MessageBox.Show("请求数据时发生错误：" + exc.Message, "错误", MessageBoxButton.OK);
                    });
                    return;
                }

                Dispatcher.BeginInvoke(() =>
                    {
                        int mod = iCount % PageSize;
                        int pages = mod == 0 ? iCount / PageSize : (iCount / PageSize) + 1;
                        this.pager.PageCount = pages;
                        this.pager.ReBind(pages);
                        this.GetPageList(1);
                    });
            };
            System.Threading.ThreadPool.QueueUserWorkItem(callBack);
        }

        void GetPageList(int pIndex)
        {
            int index = pIndex;

            if (index < 1)
                index = 1;
            if (index > this.pager.PageCount)
                index = this.pager.PageCount;

            int skip = index * PageSize - PageSize;
            int take = this.iCount - skip;

            if (take > PageSize) take = PageSize;
            if (take == 0) take = PageSize;

            var v2 = this.vList.Skip(skip).Take(take);

            System.Threading.WaitCallback callBack = (state) =>
            {
                try
                {
                    logList = v2.ToList();
                }
                catch (System.Exception exc)
                {
                    Dispatcher.BeginInvoke(() =>
                    {
                        EAS.Controls.Window.HideLoading();
                        MessageBox.Show("请求数据时发生错误：" + exc.Message, "错误", MessageBoxButton.OK);
                    });
                    return;
                }

                Dispatcher.BeginInvoke(() =>
                {
                    EAS.Controls.Window.HideLoading();
                    this.dataList.ItemsSource = logList;
                });
            };
            System.Threading.ThreadPool.QueueUserWorkItem(callBack);
        }

        private void btnSeach_Click(object sender, RoutedEventArgs e)
        {
            this.LoadDataList();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (EAS.Application.Instance != null)
            {
                EAS.Application.Instance.CloseModule(this);
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //FuncTask<int> task3 = new FuncTask<int>();
            //IMaxCodeService service3 = ServiceContainer.GetService<IMaxCodeService>(task3);
            //service3.GetMaxCode("EAS_LOGS");
            //task3.Completed +=
            //    (s, e2) =>
            //    {
            //        if (task3.Error != null)
            //        {
            //            //EAS.Controls.Window.HideLoading();
            //            MessageBox.Show("取最大号错误：" + task3.Error.Message, "错误", MessageBoxButton.OK);
            //            return;
            //        }
            //        else
            //        {
            //            int x  = task3.TResult;
            //            MessageBox.Show("X：" + task3.TResult.ToString(), "错误", MessageBoxButton.OK);
            //        }
            //    };
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            //FuncTask<string> task3 = new FuncTask<string>();
            //IMaxCodeService service3 = ServiceContainer.GetService<IMaxCodeService>(task3);
            //service3.GetMaxFieldGroupCode();
            //task3.Completed +=
            //    (s, e2) =>
            //    {
            //        if (task3.Error != null)
            //        {
            //            EAS.Controls.Window.HideLoading();
            //            MessageBox.Show("取最大号错误：" + task3.Error.Message, "错误", MessageBoxButton.OK);
            //            return;
            //        }
            //        else
            //        {
            //            MessageBox.Show("X：" + task3.TResult, "错误", MessageBoxButton.OK);
            //        }
            //    };
        }
    }
}
