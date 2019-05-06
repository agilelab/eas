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
    [Module("36535EC2-8EF6-4F22-97D1-07A6700F83EB", "参数管理", "用于集中管理AgileEAS.NET平台Silverlight应用的参数。")]
    public partial class AppSettingList : UserControl
    {
        [ModuleStart]
        public void Start()
        {
            this.OnRefresh(this, null);
        }

        int pageIndex = 1;
        int iCount = 0;
        const int PageSize = 18;

        IList<AppSetting> appSettingList = null;

        public AppSettingList()
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
            DataEntityQuery<AppSetting> query = new DataEntityQuery<AppSetting>();
            var v = from c in query
                    where c.Category.StartsWith(this.tbSeach.Text) || c.Key.StartsWith(this.tbSeach.Text) 
                    select c;

            EAS.Controls.Window.ShowLoading("请求数据...");
            DataPortal<AppSetting> dp = new DataPortal<AppSetting>();
            dp.BeginExecuteCountQuery(v).Completed +=
                (s, e2) =>
                {
                    QueryTask<AppSetting> task = s as QueryTask<AppSetting>;
                    if (task.Error != null)
                    {
                        EAS.Controls.Window.HideLoading();
                        MessageBox.Show("请求数据时发生错误：" + task.Error.Message, "错误", MessageBoxButton.OK);
                        return;
                    }
                    else
                    {
                        this.iCount = task.Count;
                        int mod = iCount % PageSize;
                        int pages = mod == 0 ? iCount / PageSize : (iCount / PageSize) + 1;
                        this.pager.PageCount = pages;
                        this.pager.ReBind(pages);
                        this.GetPageList(1);
                    }
                };
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
            
            DataEntityQuery<AppSetting> query = new DataEntityQuery<AppSetting>();
            var v = from c in query
                    where c.Category.StartsWith(this.tbSeach.Text) || c.Key.StartsWith(this.tbSeach.Text) 
                    select c;
            var v2 = v.Skip(skip).Take(take);
            
            DataPortal<AppSetting> dp = new DataPortal<AppSetting>();
            dp.BeginExecuteQuery(v2).Completed +=
                (s, e2) =>
                {
                    EAS.Controls.Window.HideLoading();

                    QueryTask<AppSetting> task = s as QueryTask<AppSetting>;
                    if (task.Error != null)
                    {
                        MessageBox.Show("请求数据时发生错误：" + task.Error.Message, "错误", MessageBoxButton.OK);
                        return;
                    }
                    else
                    {
                        appSettingList = task.Entities;
                        this.dataList.ItemsSource = appSettingList;
                    }
                };
        }

        private void OnRefresh(object sender, MouseButtonEventArgs e)
        {
            this.LoadDataList();
        }
        
        private void OnNew(object sender, MouseButtonEventArgs e)
        {
            AppSettingEditor ase = new AppSettingEditor();
            ase.Show();
            ase.Closed += new EventHandler(ase_Closed);
        }

        void ase_Closed(object sender, EventArgs e)
        {
            bool? dr = (sender as AppSettingEditor).DialogResult;
            if (dr.HasValue && (dr == true))
            {
                this.LoadDataList();
            }
        }

        private void OnDelete(object sender, MouseButtonEventArgs e)
        {
            var deletes = appSettingList.Where(p => p.Checked).ToList();
            if (deletes.Count == 0)
            {
                MessageBox.Show("请先选择要删除的参数！");
            }
            else
            {
                if (MessageBox.Show("是否确定删除所选择的参数?", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    EAS.Controls.Window.ShowLoading("正在删除参数...");
                    int index = 0;

                    foreach (AppSetting item in deletes)
                    {
                        DataPortal<AppSetting> dp = new DataPortal<AppSetting>();
                        dp.BeginDelete(item).Completed +=
                            (s, e2) =>
                            {
                                InvokeTask task = s as InvokeTask;
                                if (task.Error != null)
                                {
                                    EAS.Controls.Window.HideLoading();
                                    MessageBox.Show("删除参数时发生错误：" + task.Error.Message, "错误", MessageBoxButton.OK);
                                    return;
                                }
                                else
                                {
                                    index++;
                                    if (index == deletes.Count)
                                    {
                                        foreach (AppSetting pItem in deletes)
                                            this.appSettingList.Remove(pItem);
                                        this.dataList.ItemsSource = null;
                                        this.dataList.ItemsSource = this.appSettingList;
                                        EAS.Controls.Window.HideLoading();
                                    }
                                }
                            };
                    }
                }
            }
        }

        private void OnProperty(object sender, MouseButtonEventArgs e)
        {
            if (this.dataList.SelectedItems.Count > 0)
            {
                AppSettingEditor ase = new AppSettingEditor();
                ase.DataEntity = this.dataList.SelectedItems[0];
                ase.Show();
                ase.Closed +=
                    (s, e2) =>
                    {

                    };
            }
        }

        private void OnClose(object sender, MouseButtonEventArgs e)
        {
            if (EAS.Application.Instance != null)
            {
                EAS.Application.Instance.CloseModule(this);
            }
        }

        private void tbSeach_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.LoadDataList();
            }
        }        

        private void dataList_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                this.OnProperty(sender, e);
            }
        }
    }
}
