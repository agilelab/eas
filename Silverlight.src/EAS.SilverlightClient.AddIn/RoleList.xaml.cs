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
    [Module("B74F9E31-C427-4922-AAE5-C639025D194B", "角色管理", "用于AgileEAS.NET平台Silverlight应用的角色管理。")]
    public partial class RoleList : UserControl
    {
        [ModuleStart]
        public void Start()
        {
            this.OnRefresh(this, null);
        }

        int pageIndex = 1;
        int iCount = 0;
        const int PageSize = 18;

        IList<Role> roleList = null;

        public RoleList()
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
            DataEntityQuery<Role> query = new DataEntityQuery<Role>();
            var v = from c in query
                    where c.Name.StartsWith(this.tbSeach.Text) || c.Description.StartsWith(this.tbSeach.Text) 
                    select c;

            EAS.Controls.Window.ShowLoading("请求数据...");
            DataPortal<Role> dp = new DataPortal<Role>();
            dp.BeginExecuteCountQuery(v).Completed +=
                (s, e2) =>
                {
                    QueryTask<Role> task = s as QueryTask<Role>;
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
            
            DataEntityQuery<Role> query = new DataEntityQuery<Role>();
            var v = from c in query
                    where c.Name.StartsWith(this.tbSeach.Text) || c.Description.StartsWith(this.tbSeach.Text)
                    select c;
            var v2 = v.Skip(skip).Take(take);
            
            DataPortal<Role> dp = new DataPortal<Role>();
            dp.BeginExecuteQuery(v2).Completed +=
                (s, e2) =>
                {
                    EAS.Controls.Window.HideLoading();

                    QueryTask<Role> task = s as QueryTask<Role>;
                    if (task.Error != null)
                    {
                        MessageBox.Show("请求数据时发生错误：" + task.Error.Message, "错误", MessageBoxButton.OK);
                        return;
                    }
                    else
                    {
                        roleList = task.Entities;
                        this.dataList.ItemsSource = roleList;
                    }
                };
        }

        private void OnRefresh(object sender, MouseButtonEventArgs e)
        {
            this.LoadDataList();
        }
        
        private void OnNew(object sender, MouseButtonEventArgs e)
        {
            RoleEditor installer = new RoleEditor();
            installer.Show();
            installer.Closed += new EventHandler(installer_Closed);
        }

        void installer_Closed(object sender, EventArgs e)
        {
            bool? dr = (sender as ModuleInstaller).DialogResult;
            if (dr.HasValue && (dr == true))
            {
                this.LoadDataList();
            }
        }

        private void OnDelete(object sender, MouseButtonEventArgs e)
        {
            var deletes = roleList.Where(p => p.Checked).ToList();
            if (deletes.Count == 0)
            {
                MessageBox.Show("请先选择要删除的角色！");
            }
            else
            {
                if (MessageBox.Show("是否确定卸载所选择的角色?", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    EAS.Controls.Window.ShowLoading("正在删除角色...");
                    int index = 0;

                    foreach (Role item in deletes)
                    {
                        InvokeTask task = new InvokeTask();
                        IRoleService service = ServiceContainer.GetService<IRoleService>(task);
                        service.DeleteRole(item);
                        task.Completed +=
                            (s, e2) =>
                            {
                                if (task.Error != null)
                                {
                                    EAS.Controls.Window.HideLoading();
                                    MessageBox.Show("删除角色时发生错误：" + task.Error.Message, "错误", MessageBoxButton.OK);
                                    return;
                                }
                                else
                                {
                                    index++;
                                    if (index == deletes.Count)
                                    {
                                        foreach (Role pItem in deletes)
                                            this.roleList.Remove(pItem);
                                        this.dataList.ItemsSource = null;
                                        this.dataList.ItemsSource = this.roleList;

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
                RoleEditor rd = new RoleEditor();
                rd.DataEntity = this.dataList.SelectedItems[0];
                rd.Show();
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
