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
    [Module("E5BAC2C2-D64E-476B-A39D-439E3C15DEEA", "模块管理", "用于AgileEAS.NET平台Silverlight应用的组装、配置过程。")]
    public partial class ModuleList : UserControl
    {
        IQueryable<Module> vList = null;

        [ModuleStart]
        public void Start()
        {
            this.OnRefresh(this, null);
        }

        int pageIndex = 1;
        int iCount = 0;
        const int PageSize = 18;

        IList<Module> moduleList = null;

        public ModuleList()
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
            string key = this.tbSeach.Text.Trim();

            System.Threading.WaitCallback callBack = (state) =>
            {
                using (DbEntities db = new DbEntities())
                {
                    var v = from c in db.Modules
                            where c.Assembly.StartsWith(key) || c.Type.StartsWith(key) || c.Developer.StartsWith(key)
                            select c;
                    this.vList = v as IQueryable<Module>;

                    try
                    {
                        this.iCount = v.Count();
                    }
                    catch (System.Exception exc)
                    {
                        Dispatcher.BeginInvoke(() =>
                        {
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
                }                
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

            System.Threading.WaitCallback callBack = (state) =>
            {
                var v2 = this.vList.Skip(skip).Take(take);

                try
                {
                    moduleList = v2.ToList();
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
                    this.dataList.ItemsSource = moduleList;
                });
            };

            System.Threading.ThreadPool.QueueUserWorkItem(callBack);

        }

        private void OnRefresh(object sender, MouseButtonEventArgs e)
        {
            this.LoadDataList();
        }
        
        private void OnInstall(object sender, MouseButtonEventArgs e)
        {
            ModuleInstaller installer = new ModuleInstaller();
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

        private void OnUnstall(object sender, MouseButtonEventArgs e)
        {
            var deletes = moduleList.Where(p => p.Checked).ToList();
            if (deletes.Count == 0)
            {
                MessageBox.Show("请先选择要卸载的模块！");
            }
            else
            {
                if (MessageBox.Show("是否确定卸载所选择的模块?", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    EAS.Controls.Window.ShowLoading("正在卸载模块...");
                    int index = 0;

                    foreach (Module item in deletes)
                    {
                        InvokeTask task = new InvokeTask();
                        IModuleService service = ServiceContainer.GetService<IModuleService>(task);
                        service.UnstallModule(item);
                        task.Completed +=
                            (s, e2) =>
                            {
                                if (task.Error != null)
                                {
                                    EAS.Controls.Window.HideLoading();
                                    MessageBox.Show("卸载模块时发生错误：" + task.Error.Message, "错误", MessageBoxButton.OK);
                                    return;
                                }
                                else
                                {
                                    index++;
                                    if (index == deletes.Count)
                                    {
                                        foreach (Module pItem in deletes)
                                            this.moduleList.Remove(pItem);
                                        this.dataList.ItemsSource = null;
                                        this.dataList.ItemsSource = this.moduleList;

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
                ModuleEditor md = new ModuleEditor();
                md.DataEntity = this.dataList.SelectedItems[0];
                md.Show();
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
