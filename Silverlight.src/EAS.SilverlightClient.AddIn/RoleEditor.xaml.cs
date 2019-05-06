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
    partial class RoleEditor : DataWindow
    {
        ACL selectACL = null;
        IList<PermissionValue> pvList = null;
        IList<ACLEx> permissionList = null;
        IList<AccountGrouping> memberList = null;
        bool iclose = false;

        public RoleEditor()
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
            this.Apply();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        /// <summary>
        /// 角色。
        /// </summary>
        public Role Role
        {
            get
            {
                return this.DataEntity as Role;
            }
            set
            {
                this.DataEntity = value;
            }
        }

        protected override void OnDataEntityChanged(EventArgs e)
        {
            base.OnDataEntityChanged(e);

            if (this.Role != null)
            {
                if (!iclose)
                {
                    this.tbName.IsEnabled = false;

                    this.tbName.Text = this.Role.Name;
                    this.tbDescription.Text = this.Role.Description;

                    this.LoadMemberList();
                    this.LoadPermissionList();
                }
            }
        }

        void LoadMemberList()
        {
            DataEntityQuery<AccountGrouping> query = new DataEntityQuery<AccountGrouping>();
            var v = from c in query
                    where c.RoleName == this.Role.Name
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
                    where c.Privileger == this.Role.Name
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
                MessageBox.Show("角色名称不能为空", "提示", MessageBoxButton.OK);
                this.tcMain.SelectedIndex = 0;
                this.tbName.Focus();
                return;
            }

            Role vRole = this.Role;
            if (this.Role != null)
            {
                this.UpdateRole();
            }
            else
            {
                this.NewRole();
            }
        }

        void NewRole()
        {
            Cursor c = this.Cursor;
            this.Cursor = Cursors.Wait;
            this.btnOK.IsEnabled = false;

            Role vRole = new Role();
            
            vRole.Name = this.tbName.Text;
            vRole.Description = this.tbDescription.Text;

            DataPortal<Role> dp = new DataPortal<Explorer.Entities.Role>();
            dp.BeginExistsInDb(vRole).Completed+=
                (s, e) =>
                {
                    FuncTask funcTask = s as FuncTask;
                    if (funcTask.Error != null)
                    {
                        this.Cursor = c;
                        this.btnOK.IsEnabled = true;
                        MessageBox.Show("检查角色信息时发生错误：" + funcTask.Error.Message, "错误", MessageBoxButton.OK);
                        return;
                    }
                    else
                    {
                        List<string> members = new List<string>(this.memberList.Count);
                        foreach (var item in this.memberList)
	                    {
		                    members.Add(item.LoginID);
	                    }

                        List<ACL> pList = new List<ACL>();
                        foreach (var item in this.permissionList)
                        {
                            pList.Add(new ACL{PObject = item.PObject.ToUpper(),PType = item.PType,Privileger = item.Privileger,PValue = item.PValue});
                        }

                        InvokeTask task = new InvokeTask();
                        IRoleService service = ServiceContainer.GetService<IRoleService>(task);
                        service.UpdateRole(vRole, members, pList);

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
                                    this.DataEntity = vRole;
                                    this.DialogResult = true;
                                    this.Close();
                                }
                            };
                    }
                };            
        }

        void UpdateRole()
        {
            Cursor c = this.Cursor;
            this.Cursor = Cursors.Wait;
            this.btnOK.IsEnabled = false;

            this.Role.Name = this.tbName.Text;
            this.Role.Description = this.tbDescription.Text;

            List<string> members = new List<string>(this.memberList.Count);
                        foreach (var item in this.memberList)
	                    {
		                    members.Add(item.LoginID);
	                    }

                        List<ACL> pList = new List<ACL>();
                        foreach (var item in this.permissionList)
                        {
                            pList.Add(new ACL{PObject = item.PObject.ToUpper(),PType = item.PType,Privileger = item.Privileger,PValue = item.PValue});
                        }

            InvokeTask task = new InvokeTask();
            IRoleService service = ServiceContainer.GetService<IRoleService>(task);
            service.UpdateRole(this.Role, members, pList);

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
            pSelector.SelectorType = (int)PrivilegerSelectorType.AccountSelector;
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
                    int count = this.memberList.Where(p => (p.LoginID == item.Name)).Count();
                    if (count < 1)
                    {
                        this.memberList.Add(new AccountGrouping { LoginID = item.Name, RoleName = this.Role.Name });
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
                        this.permissionList.Add(new ACLEx { MInfo = item, PObject = item.Guid, Privileger = this.Role.Name, PType = (int)PrivilegerType.Role, PValue = (int)Privileges.Execute });
                    }
                }

                this.dataPermissions.ItemsSource = null;
                this.dataPermissions.ItemsSource = this.permissionList;
                this.btnAdd.IsEnabled = true;
            }
        }

        private void tbInput_TextChanged(object sender, System.EventArgs e)
        {
            this.btnOK.IsEnabled = true;
        }
    }
}
