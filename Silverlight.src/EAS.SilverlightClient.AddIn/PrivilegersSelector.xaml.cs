using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EAS.Data.Linq;
using EAS.Explorer.Entities;
using EAS.Security;
using System.Text;
using EAS.Data;

namespace EAS.SilverlightClient.AddIn
{
    public partial class PrivilegersSelector : ChildWindow
	{
		int pst;
        IList<SelectedPrivileger> displayList = null;
        IList<SelectedPrivileger> selectList = null;

        bool m_LoadAccount = false;
        bool m_LoadRole = false;

		public PrivilegersSelector()
		{
			InitializeComponent();
			this.pst = 0x0003;
            this.displayList = new List<SelectedPrivileger>();
            this.selectList = new List<SelectedPrivileger>();
            this.cbAccount.Checked+=new RoutedEventHandler(cbAccount_Checked);
            this.cbRole.Checked+=new RoutedEventHandler(cbRole_Checked);
		}

        void SetType()
        {
            int role = (int)PrivilegerSelectorType.RoleSelector;
            int account = (int)PrivilegerSelectorType.AccountSelector;

            if (this.pst == role)
            {
                this.cbAccount.IsChecked = false;
                this.cbAccount.IsEnabled = false;
                this.cbRole.IsChecked = true;
                this.cbRole.IsEnabled = false;
                this.cbForbidden.IsEnabled = false;
                this.cbForbidden.IsChecked = false;
                this.Title = "选择角色";
                this.labelMsg.Content = "根据下面的搜索选项搜索角色，并从搜索结果中选择。";
                this.labelTip.Content = "已经选中的角色：";
            }
            else if (this.pst == account)
            {
                this.cbAccount.IsChecked = true;
                this.cbAccount.IsEnabled = false;
                this.cbRole.IsChecked = false;
                this.cbRole.IsEnabled = false;
                this.cbForbidden.IsEnabled = true;
                this.cbForbidden.IsChecked = false;
                this.Title = "选择帐户";
                this.labelMsg.Content = "根据下面的搜索选项搜索帐户，并从搜索结果中选择。";
                this.labelTip.Content = "已经选中的用户：";
            }
            else
            {
                this.cbAccount.IsChecked = true;
                this.cbAccount.IsEnabled = true;
                this.cbRole.IsChecked = true;
                this.cbRole.IsEnabled = true;
                this.cbForbidden.IsEnabled = true;
                this.cbForbidden.IsChecked = false;
                this.Title = "选择帐户和角色";
                this.labelMsg.Content = "根据下面的搜索选项搜索帐户或者角色，并从搜索结果中选择。";
                this.labelTip.Content = "已经选中的角色或角色：";
            }
        }

        private void cbAccount_Checked(object sender, RoutedEventArgs e)
		{
            bool account = this.cbAccount.IsChecked??false;
            bool role = this.cbRole.IsChecked ?? false;

            this.cbForbidden.IsEnabled = account;
            this.btnSeach.IsEnabled = account || role;
		}

        private void cbRole_Checked(object sender, RoutedEventArgs e)
        {
            bool account = this.cbAccount.IsChecked ?? false;
            bool role = this.cbRole.IsChecked ?? false;
            this.btnSeach.IsEnabled = account || role;
        }

        private void tbKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.btnSearch_Click(this.btnSeach, new RoutedEventArgs());
            }
        }        

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            this.tbSelected.Text = string.Empty;
            this.displayList = new List<SelectedPrivileger>();

            this.m_LoadAccount = false;
            this.m_LoadRole = false;

            if (this.cbAccount.IsChecked ?? false)
            {
                this.LoadAccounts();
            }
            else if (this.cbRole.IsChecked ?? false)
            {
                this.LoadRoles();
            }
        }

        void LoadRoles()
        {
            DataEntityQuery<Role> query = new DataEntityQuery<Role>();
            var v2 = from c in query
                     where c.Name.StartsWith(this.tbKey.Text)
                     select c;

            DataPortal<Role> dp = new DataPortal<Role>();
            dp.BeginExecuteQuery(v2).Completed +=
                (s, e2) =>
                {
                    this.m_LoadRole = true;

                    QueryTask<Role> task = s as QueryTask<Role>;

                    if (task.Error != null)
                    {
                        MessageBox.Show("查询角色时出错：" + task.Error.Message, "错误", MessageBoxButton.OK);
                        return;
                    }
                    else
                    {
                        IList<Role> roleList = task.Entities;
                        foreach (Role item in roleList)
                        {
                            this.displayList.Add(new SelectedPrivileger { Name = item.Name, Type = PrivilegerType.Role, Permissions = (int)Privileges.Execute, Tag = item });
                        }

                        if ((this.cbAccount.IsChecked ?? false) && (!this.m_LoadAccount))
                        {
                            this.LoadAccounts();
                        }
                        else
                        {
                            this.dataList.ItemsSource = null;
                            this.dataList.ItemsSource = this.displayList;
                        }
                    }
                };            
        }

        void LoadAccounts()
        {
            DataEntityQuery<Account> query = new DataEntityQuery<Account>();
            var v = from c in query
                    where c.LoginID.StartsWith(this.tbKey.Text) || c.Name.StartsWith(this.tbKey.Text)
                    select c;

            DataPortal<Account> dp = new DataPortal<Account>();
            dp.BeginExecuteQuery(v).Completed +=
                (s, e2) =>
                {
                    this.m_LoadAccount = true; 

                    QueryTask<Account> task = s as QueryTask<Account>;
                    if (task.Error != null)
                    {
                        MessageBox.Show("查询账号时出错：" + task.Error.Message, "错误", MessageBoxButton.OK);
                        return;
                    }
                    else
                    {
                        ShowAccounts(task);

                        if ((this.cbRole.IsChecked ?? false) && (!this.m_LoadRole))
                        {
                            this.LoadRoles();
                        }
                        else
                        {
                            this.dataList.ItemsSource = null;
                            this.dataList.ItemsSource = this.displayList;
                        }
                    }
                };
        }

        void ShowAccounts(QueryTask<Account> task)
        {
            IList<Account> accountList = task.Entities;
            if (!this.cbForbidden.IsChecked ?? false)
            {
                int iMax = accountList.Count - 1;
                for (int i = iMax; i >= 0; i--)
                {
                    if ((accountList[i].Attributes & 0x0008) == 0x0008)
                        accountList.RemoveAt(i);
                }
            }

            foreach (Account item in accountList)
            {
                this.displayList.Add(new SelectedPrivileger { Name = item.LoginID, Type = PrivilegerType.Account, Permissions = (int)Privileges.Execute, Tag = item });
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.selectList = (this.dataList.ItemsSource as IList<SelectedPrivileger>).Where(p => p.Checked).ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var item in this.selectList)
            {
                if (sb.Length > 0)
                    sb.Append(",");
                sb.Append(item.Name);
            }

            this.tbSelected.Text = sb.ToString();
        }

        /// <summary>
        /// 选择器的类型。
        /// </summary>
        internal int SelectorType
        {
            get
            {
                return this.pst;
            }
            set
            {
                this.pst = value;
                this.SetType();
            }
        }        

        /// <summary>
        /// 已经选中的用户或者角色信息。
        /// </summary>
        public IList<SelectedPrivileger> SelectedPrivilegers
        {
            get
            {
                return this.selectList;
            }
        }        
	}
}
