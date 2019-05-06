using System;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;

using System.Linq;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EAS.Controls;
using EAS.Explorer.Entities;
using EAS.Data.Linq;
using EAS.Data;
using EAS.Explorer.BLL;
using EAS.Services;
using EAS.Security;

namespace EAS.SilverlightClient.AddIn
{
    partial class AccountEditor : DataWindow
    {
        ACL selectACL = null;
        IList<PermissionValue> pvList = null;
        IList<ACLEx> permissionList = null;
        IList<AccountGrouping> memberList = null;
        bool iclose = false;

        int m_Attributes;

        public AccountEditor()
        {
            InitializeComponent();
            pvList = PermissionValue.GetPermissionList();
            this.dataPVs.ItemsSource = pvList;
            this.permissionList = new List<ACLEx>();
            this.dataPermissions.ItemsSource = this.permissionList;
            this.memberList = new List<AccountGrouping>();
            this.dataMembers.ItemsSource = this.memberList;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.tbLoginID.Text.Trim().Length == 0)
            {
                MessageBox.Show("账户登录ID不能为空，请输入账户登录ID。", "提示",MessageBoxButton.OK);
                this.tcMain.SelectedIndex = 0;
                this.tbLoginID.Focus();
                return;
            }

            if (this.tbPassword.Text != this.tbCPassword.Text)
            {
                MessageBox.Show("您两次输入的密码不一致，请重新确定密码。", "提示",MessageBoxButton.OK);
                this.tcMain.SelectedIndex = 0;
                this.tbPassword.Focus();
                return;
            }

            this.Apply();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        /// <summary>
        /// 帐号。
        /// </summary>
        public Account Account
        {
            get
            {
                return this.DataEntity as Account;
            }
            set
            {
                this.DataEntity = value;
            }
        }

        internal int Attributes
        {
            get
            {
                if (this.cbCannotChPswd.IsChecked ?? false)
                    this.m_Attributes |= 0x00000002;
                else
                    this.m_Attributes &= 0x7ffffffd;

                if (this.cbLongPassword.IsChecked ?? false)
                    this.m_Attributes |= 0x00000004;
                else
                    this.m_Attributes &= 0x7ffffffb;

                if (this.cbDisable.IsChecked??false)
                    this.m_Attributes |= 0x00000008;
                else
                    this.m_Attributes &= 0x7ffffff7;

                return this.m_Attributes;
            }
            set
            {
                this.cbCannotChPswd.IsChecked = (value & 0x0002) == 0x0002;
                this.cbLongPassword.IsChecked = (value & 0x0004) == 0x0004;
                this.cbDisable.IsChecked = (value & 0x0008) == 0x0008;
                this.m_Attributes = value;
            }
        }

        internal int LoginCount
        {
            set
            {
                this.lbLoginCount.Content = "登录记数："+value.ToString();
            }
        }

        public Organization Organ
        {
            set
            {
                this.tbCategory.Tag = value.Guid;
                if (value != null)
                {
                    this.tbCategory.Text = value.Name;
                }
            }            
        }

        protected override void OnDataEntityChanged(EventArgs e)
        {
            base.OnDataEntityChanged(e);

            if (this.DataEntity != null)
            {
                if (!iclose)
                {
                    this.tbLoginID.IsEnabled = false;

                    this.tbLoginID.Text = this.Account.LoginID;
                    this.tbName.Text = this.Account.Name;
                    this.tbDescription.Text = this.Account.Description;
                    this.Attributes = this.Account.Attributes;
                    this.tbCategory.Text = this.Account.OrganName;
                    this.tbRowID.Text = this.Account.OriginalID;

                    this.cbLeader.IsChecked = this.Account.Leader > 0;

                    this.LoadMemberList();
                    this.LoadPermissionList();
                }
            }
        }

        void LoadMemberList()
        {
            DataEntityQuery<AccountGrouping> query = new DataEntityQuery<AccountGrouping>();
            var v = from c in query
                    where c.LoginID == this.Account.LoginID
                    select c;

            DataPortal<AccountGrouping> dp = new DataPortal<AccountGrouping>();
            dp.BeginExecuteQuery(v).Completed +=
                (s, e2) =>
                {
                    QueryTask<AccountGrouping> task = s as QueryTask<AccountGrouping>;
                    if (task.Error != null)
                    {
                        MessageBox.Show("读取成员数据时出错：" + task.Error.Message, "错误", MessageBoxButton.OK);
                        return;
                    }
                    else
                    {
                        this.memberList = task.Entities;
                        this.dataMembers.ItemsSource = this.memberList;
                    }
                };
        }

        void LoadPermissionList()
        {
            this.permissionList = new List<ACLEx>();
            DataEntityQuery<ACL> query = new DataEntityQuery<ACL>();
            var v = from c in query
                    where c.Privileger == this.Account.LoginID
                    select c;

            DataPortal<ACL> dp = new DataPortal<ACL>();
            dp.BeginExecuteQuery(v).Completed +=
                (s, e2) =>
                {
                    QueryTask<ACL> task = s as QueryTask<ACL>;
                    if (task.Error != null)
                    {
                        MessageBox.Show("读取权限数据时出错：" + task.Error.Message, "错误", MessageBoxButton.OK);
                        return;
                    }
                    else
                    {
                        ModuleConverter.LoadAllPermission(this.dataPermissions, task.Entities, this.permissionList);
                    }
                };
        }

        int GetPermissionValue()
        {
            int PValue = 0;
            foreach (PermissionValue pv in this.pvList)
            {
                if (pv.Checked)
                {
                    PValue += pv.Value;
                }
            }

            return PValue;
        }

        private void Apply()
        {
            if (this.selectACL != null)
                this.selectACL.PValue = this.GetPermissionValue();

            if (this.tbName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("账号名称不能为空", "提示", MessageBoxButton.OK);
                this.tcMain.SelectedIndex = 0;
                this.tbName.Focus();
                return;
            }

            Account vItem = this.Account;
            if (this.Account != null)
            {
                this.UpdateAccount();
            }
            else
            {
                this.NewAccount();
            }
        }

        void NewAccount()
        {
            Cursor c = this.Cursor;
            this.Cursor = Cursors.Wait;
            this.btnOK.IsEnabled = false;

            Account vItem = new Account();
            vItem.LoginID = this.tbLoginID.Text;
            vItem.OrganID = this.tbCategory.Tag.ToString();
            vItem.OrganName = this.tbCategory.Text;
            vItem.Name = this.tbName.Text;
            vItem.Description = this.tbDescription.Text;
            vItem.Leader = this.cbLeader.IsChecked.HasValue ? (this.cbLeader.IsChecked.Value ? 1:0) : 0;

            DataPortal<Account> dp = new DataPortal<Explorer.Entities.Account>();
            dp.BeginExistsInDb(vItem).Completed+=
                (s, e) =>
                {
                    FuncTask funcTask = s as FuncTask;
                    if (funcTask.Error != null)
                    {
                        this.Cursor = c;
                        this.btnOK.IsEnabled = true;
                        MessageBox.Show("检查账号信息时发生错误：" + funcTask.Error.Message, "错误", MessageBoxButton.OK);
                        return;
                    }
                    else
                    {
                        List<string> members = new List<string>(this.memberList.Count);
                        foreach (var item in this.memberList)
	                    {
		                    members.Add(item.RoleName);
	                    }

                        List<ACL> pList = new List<ACL>();
                        foreach (var item in this.permissionList)
                        {
                            pList.Add(new ACL{PObject = item.PObject,PType = item.PType,Privileger = item.Privileger,PValue = item.PValue});
                        }

                        InvokeTask task = new InvokeTask();
                        IAccountService service = ServiceContainer.GetService<IAccountService>(task);
                        service.UpdateAccount(vItem,this.tbPassword.Text, members, pList);

                        task.Completed +=
                            (s2, e2) =>
                            {
                                if (task.Error != null)
                                {
                                    this.Cursor = c;
                                    this.btnOK.IsEnabled = true;
                                    MessageBox.Show("完成业务处理时发生错误：" + task.Error.Message, "错误", MessageBoxButton.OK);
                                    return;
                                }
                                else
                                {
                                    this.Cursor = c;
                                    this.btnOK.IsEnabled = true;
                                    this.iclose = true;
                                    this.DataEntity = vItem;
                                    this.DialogResult = true;
                                    this.Close();
                                }
                            };
                    }
                };            
        }

        void UpdateAccount()
        {
            Cursor c = this.Cursor;
            this.Cursor = Cursors.Wait;
            this.btnOK.IsEnabled = false;

            this.Account.Name = this.tbName.Text;
            this.Account.Description = this.tbDescription.Text;
            this.Account.Leader = this.cbLeader.IsChecked.HasValue ? (this.cbLeader.IsChecked.Value ? 1 : 0) : 0;

            List<string> members = new List<string>(this.memberList.Count);
            foreach (var item in this.memberList)
            {
                members.Add(item.RoleName);
            }

            List<ACL> pList = new List<ACL>();
            foreach (var item in this.permissionList)
            {
                pList.Add(new ACL { PObject = item.PObject, PType = item.PType, Privileger = item.Privileger, PValue = item.PValue });
            }

            InvokeTask task = new InvokeTask();
            IAccountService service = ServiceContainer.GetService<IAccountService>(task);
            service.UpdateAccount(this.Account, this.tbPassword.Text, members, pList);

            task.Completed +=
                (s, e2) =>
                {
                    if (task.Error != null)
                    {
                        this.Cursor = c;
                        this.btnOK.IsEnabled = true;
                        MessageBox.Show("完成业务处理时发生错误：" + task.Error.Message, "错误", MessageBoxButton.OK);
                        return;
                    }
                    else
                    {
                        this.Cursor = c;
                        this.btnOK.IsEnabled = true;
                        this.DialogResult = true;
                        this.Close();
                    }
                };
        }

        private void tbDescription_TextChanged(object sender, System.EventArgs e)
        {
            this.btnOK.IsEnabled = true;
        }

        private void dataPermissions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.selectACL != null)
                this.selectACL.PValue = this.GetPermissionValue();

            if (e.AddedItems.Count > 0)
            {
                this.selectACL = e.AddedItems[0] as ACL;
                foreach (PermissionValue pv in this.pvList)
                {
                    if ((pv.Value & this.selectACL.PValue) == pv.Value)
                        pv.Checked = true;
                    else
                        pv.Checked = false;
                }
            }
        }

        private void cbPermissions_Checked(object sender, RoutedEventArgs e)
        {
            int count = this.permissionList.Where(p => p.Checked).Count();
            this.btnDelete.IsEnabled = count > 0;
        }

        private void cbPVS_Checked(object sender, RoutedEventArgs e)
        {
            this.btnOK.IsEnabled = true;
        }

        private void cbMS_Checked(object sender, RoutedEventArgs e)
        {
            this.btnOK.IsEnabled = true;
            int count = this.memberList.Where(p => p.Checked).Count();
            this.btnMDelete.IsEnabled = count > 0;
        }

        private void btnMAdd_Click(object sender, RoutedEventArgs e)
        {
            PrivilegersSelector pSelector = new PrivilegersSelector();
            pSelector.SelectorType = (int)PrivilegerSelectorType.RoleSelector;
            pSelector.Closed += new EventHandler(pSelector_Closed);
            pSelector.Show();
        }

        void pSelector_Closed(object sender, EventArgs e)
        {
            PrivilegersSelector ps = sender as PrivilegersSelector;
            if (ps.DialogResult ?? false)
            {
                IList<SelectedPrivileger> selectList = ps.SelectedPrivilegers;
                foreach (var item in selectList)
                {
                    int count = this.memberList.Where(p => (p.RoleName == item.Name)).Count();
                    if (count < 1)
                    {
                        this.memberList.Add(new AccountGrouping { LoginID = this.Account.LoginID, RoleName = item.Name });
                    }
                }

                this.dataMembers.ItemsSource = null;
                this.dataMembers.ItemsSource = this.memberList;
                this.btnMAdd.IsEnabled = true;
            }
        }

        private void btnMDelete_Click(object sender, RoutedEventArgs e)
        {
            var v = this.memberList.Where(p => p.Checked).ToList();
            if (v.Count > 0)
            {
                foreach (var item in v)
                {
                    this.memberList.Remove(item);
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var v = this.permissionList.Where(p => p.Checked).ToList();
            if (v.Count > 0)
            {
                foreach (var item in v)
                {
                    this.permissionList.Remove(item);
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ModuleSelector selector = new ModuleSelector();
            selector.Closed += new EventHandler(selector_Closed);
            selector.Show();
        }

        void selector_Closed(object sender, EventArgs e)
        {
            ModuleSelector ms = sender as ModuleSelector;
            if (ms.DialogResult ?? false)
            {
                IList<Module> selectList = ms.SelectedModules;
                foreach (var item in selectList)
                {
                    int count = this.permissionList.Where(p => (p.PObject == item.Guid)).Count();
                    if (count < 1)
                    {
                        this.permissionList.Add(new ACLEx { MInfo = item, PObject = item.Guid, Privileger = this.Account.LoginID, PType = (int)PrivilegerType.Account, PValue = (int)Privileges.Execute });
                    }
                }

                this.dataPermissions.ItemsSource = null;
                this.dataPermissions.ItemsSource = this.permissionList;
                this.btnAdd.IsEnabled = true;
            }
        }

        private void tbInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.btnOK.IsEnabled = true;
        }
    }
}
