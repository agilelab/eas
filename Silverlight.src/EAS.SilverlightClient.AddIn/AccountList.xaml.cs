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
    [Module("8B625A2D-45A8-439A-8C23-1A9A55F3496C", "帐号管理", "用于管理AgileEAS.NET平台Silverlight应用的系统账户。")]
    public partial class AccountList : UserControl
    {
        [ModuleStart]
        public void Start()
        {
            this.OnRefresh(this, null);
        }

        TreeItem rootItem = new TreeItem();

        IList<Organization> categoryList = null;
        IList<Account> accountList = null;

        Organization selectCategory = null;
        Account selectAccount = null;

        int pageIndex = 1;
        int iCount = 0;
        const int PageSize = 18;

        public AccountList()
        {
            InitializeComponent();
            this.categoryList = new List<Organization>();
            this.accountList = new List<Account>();
            this.pager.PageCount = 15;
            this.pager.OnPageIndexChange += new PageIndexChange(pager_OnPageIndexChange);
        }

        void LoadACategory()
        {
            DataEntityQuery<Organization> query = new DataEntityQuery<Organization>();

            EAS.Controls.Window.ShowLoading("请求数据...");
            DataPortal<Organization> dp = new DataPortal<Organization>();
            dp.BeginExecuteQuery(query).Completed +=
                (s, e2) =>
                {
                    EAS.Controls.Window.HideLoading();

                    QueryTask<Organization> task = s as QueryTask<Organization>;
                    if (task.Error != null)
                    {                        
                        MessageBox.Show("请求数据时发生错误：" + task.Error.Message, "错误", MessageBoxButton.OK);
                        return;
                    }
                    else
                    {
                        this.categoryList = task.Entities;
                        this.InitializeTree();
                    }
                };
        }

        #region 初始化树

        void InitializeTree()
        {
            //this.Tree.Items.Clear();
            
            Organization xRoot = this.categoryList.Where(p => p.ParentID == Guid.Empty.ToString() || p.ParentID == string.Empty).FirstOrDefault();
            if (xRoot == null)
            {
                xRoot = new Organization { Name = "AgileEAS.NET SOA", Guid = Guid.NewGuid().ToString().ToUpper(), ParentID = Guid.Empty.ToString() };
                //xRoot.Insert();
            }

            this.rootItem = new TreeItem();
            this.rootItem.Name = xRoot.Name;
            this.rootItem.Icon = "images2/desktop16.png";
            this.rootItem.Tag = xRoot;
            this.InitializeTree(this.rootItem, xRoot);
            List<TreeItem> items = new List<TreeItem>();
            items.Add(this.rootItem);
            this.Tree.ItemsSource = items;
        }

        void InitializeTree(TreeItem pItem, Organization pRoot)
        {
            IList<Organization> List = this.categoryList.Where(p => p.ParentID == pRoot.Guid).ToList();

            foreach (Organization var in List)
            {
                TreeItem subItem = new TreeItem();
                subItem.Icon = "images2/program_group.png";
                subItem.Name = var.Name;
                subItem.Tag = var;
                subItem.Parent = pItem;
                this.InitializeTree(subItem, var);
                pItem.Items.Add(subItem);
            }
        }

        #endregion

        void LoadDataList(Organization category)
        {
            DataEntityQuery<Account> query = new DataEntityQuery<Account>();
            var v = from c in query
                    where c.OrganID == category.Guid
                    select c;

            EAS.Controls.Window.ShowLoading("请求数据...");
            DataPortal<Account> dp = new DataPortal<Account>();
            dp.BeginExecuteCountQuery(v).Completed +=
                (s, e2) =>
                {
                    QueryTask<Account> task = s as QueryTask<Account>;
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

            DataEntityQuery<Account> query = new DataEntityQuery<Account>();
            var v = from c in query
                    where c.OrganID == this.selectCategory.Guid
                    select c;
            var v2 = v.Skip(skip).Take(take);

            DataPortal<Account> dp = new DataPortal<Account>();
            dp.BeginExecuteQuery(v2).Completed +=
                (s, e2) =>
                {
                    EAS.Controls.Window.HideLoading();

                    QueryTask<Account> task = s as QueryTask<Account>;
                    if (task.Error != null)
                    {
                        MessageBox.Show("请求数据时发生错误：" + task.Error.Message, "错误", MessageBoxButton.OK);
                        return;
                    }
                    else
                    {
                        this.accountList = task.Entities;
                        this.dataAccounts.ItemsSource = this.accountList;
                    }
                };
        }

        void pager_OnPageIndexChange(int pageIndex)
        {
            this.pageIndex = pageIndex;
            EAS.Controls.Window.ShowLoading("请求数据,第" + pageIndex + "页...");
            this.GetPageList(pageIndex);
        }

        private void OnRefresh(object sender, MouseButtonEventArgs e)
        {
            this.LoadACategory();
        }

        private void OnNew(object sender, MouseButtonEventArgs e)
        {
            if (this.selectCategory != null)
            {
                AccountEditor aEditor = new AccountEditor();
                aEditor.Organ = this.selectCategory;
                aEditor.Show();
                aEditor.Closed += new EventHandler(aEditor_Closed);
            }
        }

        void aEditor_Closed(object sender, EventArgs e)
        {
            bool? dr = (sender as AccountEditor).DialogResult;
            if (dr.HasValue && (dr == true))
            {
                this.LoadDataList(this.selectCategory);
            }
        }

        private void OnProperty(object sender, MouseButtonEventArgs e)
        {
            if (this.dataAccounts.SelectedItems.Count > 0)
            {
                this.selectAccount = this.dataAccounts.SelectedItems[0] as Account;
                AccountEditor ad = new AccountEditor();
                ad.DataEntity = this.selectAccount;
                ad.Show();
                ad.Closed +=
                    (s, e2) =>
                        {
                            if (ad.DialogResult ?? false)
                            {
                                this.selectAccount.Name = ad.Account.Name;
                                this.selectAccount.Description = ad.Account.Description;
                            }
                        };
            }
        }

        private void OnDelete(object sender, MouseButtonEventArgs e)
        {
            var deletes = this.accountList.Where(p => p.Checked).ToList();
            if (deletes.Count == 0)
            {
                MessageBox.Show("请先选择要删除的帐号！");
            }
            else
            {
                if (MessageBox.Show("是否确定删除所选择的帐号?", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    EAS.Controls.Window.ShowLoading("正在删除帐号..");
                    int index = 0;

                    foreach (Account item in deletes)
                    {
                        InvokeTask task = new InvokeTask();
                        IAccountService service = ServiceContainer.GetService<IAccountService>(task);
                        service.Delete(item);
                        task.Completed +=
                            (s, e2) =>
                            {
                                if (task.Error != null)
                                {
                                    EAS.Controls.Window.HideLoading();
                                    MessageBox.Show("删除帐号时发生错误：" + task.Error.Message, "错误", MessageBoxButton.OK);
                                    return;
                                }
                                else
                                {
                                    index++;
                                    if (index == deletes.Count)
                                    {
                                        foreach (Account pItem in deletes)
                                            this.accountList.Remove(pItem);
                                        this.dataAccounts.ItemsSource = null;
                                        this.dataAccounts.ItemsSource = this.accountList;

                                        EAS.Controls.Window.HideLoading();
                                    }
                                }
                            };
                    }
                }
            }
        }

        private void OnClose(object sender, MouseButtonEventArgs e)
        {
            if (EAS.Application.Instance != null)
            {
                EAS.Application.Instance.CloseModule(this);
            }
        }

        private void TbItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            if (tb.Tag is Organization)
            {
                this.selectCategory = tb.Tag as Organization;
                this.LoadDataList(this.selectCategory);
            }
        }        

        private void miAdd_Click(object sender, RoutedEventArgs e)
        {
            if (this.Tree.SelectedItem != null)
            {
                TreeItem item = this.Tree.SelectedItem as TreeItem;
                AccountTypeEditor tEditor = new AccountTypeEditor();
                tEditor.ParentOrgan = item.Tag as Organization;
                tEditor.Show();
                tEditor.Closed += new EventHandler(tEditor_Closed);
            }
        }

        void tEditor_Closed(object sender, EventArgs e)
        {
            AccountTypeEditor ate = sender as AccountTypeEditor;
            bool? dr = ate.DialogResult;
            if (dr.HasValue && (dr == true))
            {
                int count = this.categoryList.Where(p => p.Name == ate.Organization.Name).Count();
                if (count < 1)
                    this.categoryList.Add(ate.Organization);
            }
        }

        private void miProp_Click(object sender, RoutedEventArgs e)
        {
            if (this.Tree.SelectedItem != null)
            {
                TreeItem item = this.Tree.SelectedItem as TreeItem;
                AccountTypeEditor tEditor = new AccountTypeEditor();
                tEditor.Organization = item.Tag as Organization;
                tEditor.Show();
                tEditor.Closed += (s, e2) =>
                    {
                        item.Name = tEditor.Organization.Name;
                    };
            }
        }

        private void miDelete_Click(object sender, RoutedEventArgs e)
        {
            if (this.Tree.SelectedItem == null) return;
            TreeItem xItem = this.Tree.SelectedItem as TreeItem;
            if (xItem == null) return;
            Organization category = xItem.Tag as Organization;
            if (category == null) return;

            if (MessageBox.Show("您选择了要删除组织机构“" + category.Name + "”以及该组织机构中的所有帐户！\n\n是否确定要删除系统组织机构“" + category.Name + "”？", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                EAS.Controls.Window.ShowLoading("正在组织机构...");
                InvokeTask task = new InvokeTask();
                IAccountService service = ServiceContainer.GetService<IAccountService>(task);
                service.DeleteOrganization(category.Guid);
                task.Completed +=
                    (s, e2) =>
                    {
                        EAS.Controls.Window.HideLoading();
                        if (task.Error != null)
                        {
                            MessageBox.Show("删除组织机构时发生错误：" + task.Error.Message, "错误", MessageBoxButton.OK);
                        }
                        else
                        {
                            this.LoadACategory();
                        }
                    };
            }
        }

        private void dataAccounts_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                this.OnProperty(sender, e);
            }
        }
    }
}
