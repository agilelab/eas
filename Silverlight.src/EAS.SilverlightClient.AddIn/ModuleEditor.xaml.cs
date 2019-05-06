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

namespace EAS.SilverlightClient.AddIn
{
    partial class ModuleEditor : DataWindow
    {
        ACL selectACL = null;
        IList<PermissionValue> pvList = null;
        IList<ACL> permissionList = null;

        public ModuleEditor()
        {
            InitializeComponent();
            pvList = PermissionValue.GetPermissionList();
            this.dataPVs.ItemsSource = pvList;
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
        /// 模块。
        /// </summary>
        public Module Module
        {
            get
            {
                return this.DataEntity as Module;
            }
            set
            {
                this.DataEntity = value;
            }
        }

        protected override void OnDataEntityChanged(EventArgs e)
        {
            base.OnDataEntityChanged(e);

            if (this.Module != null)
            {
                this.tbName.Text = this.Module.Name;
                this.tbDescription.Text = this.Module.Description;
                this.tbType.Text = this.Module.Type;
                this.tbGuid.Text = this.Module.Guid.ToUpper();
                this.tbAssembly.Text = this.Module.Assembly;
                this.tbVersion.Text = this.Module.Version;
                this.tbDeveloper.Text = this.Module.Developer;

                this.tbUrl.Text = this.Module.Url;
                this.nudSortCode.Value = this.Module.SortCode;

                this.GetPermissionList();
            }
        }

        void GetPermissionList()
        {
            DataEntityQuery<ACL> query = new DataEntityQuery<ACL>();
            var v = from c in query
                    where c.PObject == this.Module.Guid
                    select c;

            DataPortal<ACL> dp = new DataPortal<ACL>();
            dp.BeginExecuteQuery(v).Completed +=
                (s, e2) =>
                {
                    QueryTask<ACL> task = s as QueryTask<ACL>;
                    if (task.Error != null)
                    {
                        MessageBox.Show("读取模块权限时出错：" + task.Error.Message, "错误", MessageBoxButton.OK);
                        return;
                    }
                    else
                    {
                        this.permissionList = task.Entities;
                        this.dataPermissions.ItemsSource = this.permissionList;
                        if (this.permissionList.Count > 0)
                        {
                            this.dataPermissions.SelectedItem = this.permissionList[0];
                        }
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
            Cursor c = this.Cursor;
            this.Cursor = Cursors.Wait;
            this.btnOK.IsEnabled = false;

            if (this.selectACL != null)
                this.selectACL.PValue = this.GetPermissionValue();

            if (this.tbName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("模块名称不能为空", "提示",MessageBoxButton.OK);
                this.tcMain.SelectedIndex = 0;
                this.tbName.Focus();
                return;
            }

            this.Module.Name = this.tbName.Text;
            this.Module.Description = this.tbDescription.Text;
            this.Module.Url = this.tbUrl.Text;

            InvokeTask task = new InvokeTask();
            IModuleService service = ServiceContainer.GetService<IModuleService>(task);
            service.UpdateModule(this.Module,(List<ACL>)this.permissionList);
            
                task.Completed +=
                    (s, e2) =>
                    {
                        if (task.Error != null)
                        {
                            this.Cursor = c;
                            this.btnOK.IsEnabled = true;
                            MessageBox.Show("请求数据时发生错误：" + task.Error.Message, "错误", MessageBoxButton.OK);
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
            PrivilegersSelector ps = new PrivilegersSelector();
            ps.SelectorType = 0x0003;
            ps.Closed += new EventHandler(ps_Closed);
            ps.Show();            
        }

        void ps_Closed(object sender, EventArgs e)
        {
            PrivilegersSelector ps = sender  as PrivilegersSelector;
            if (ps.DialogResult ??false)
            {
                IList<SelectedPrivileger> selectList = ps.SelectedPrivilegers;
                foreach (var item in selectList)
                {
                    int count = this.permissionList.Where(p => (p.Privileger == item.Name)).Count();
                    if (count < 1)
                    {
                        this.permissionList.Add(new ACL { PObject = this.Module.Guid.ToUpper(),Privileger= item.Name,PType = (int)item.Type,PValue = item.Permissions });
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
