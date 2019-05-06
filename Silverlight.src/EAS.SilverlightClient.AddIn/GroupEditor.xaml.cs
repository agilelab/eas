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
using EAS.Explorer;

namespace EAS.SilverlightClient.AddIn
{
    partial class GroupEditor : DataWindow
    {
        IList<Module> memberList = null;
        bool iclose = false;

        public GroupEditor()
        {
            InitializeComponent();
            this.memberList = new List<Module>();
            this.dataMembers.ItemsSource = this.memberList;
            this.ParentID = Guid.Empty.ToString();
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
        /// 上级组。
        /// </summary>
        public string ParentID
        {
            get;
            set;
        }

        /// <summary>
        /// 导航。
        /// </summary>
        public NavigateGroup Group
        {
            get
            {
                return this.DataEntity as NavigateGroup;
            }
            set
            {
                this.DataEntity = value;
            }
        }

        /// <summary>
        /// 分组属性。
        /// </summary>
        public int Props
        {
            get
            {
                int prop = this.rbWindows.IsChecked ?? false ? 0x0004 : (this.rbWindows.IsChecked ?? false?0x0008:0x0010);

                if (this.cbExpand.IsChecked??false)
                    prop |= 0x0002;

                return prop;
            }
            set
            {
                int prop = value;

                if ((prop & 0x0004) == 0x0004)
                    this.rbWindows.IsChecked = true;
                else if ((prop & 0x0008) == 0x0008)
                    this.rbWeb.IsChecked = true;
                else
                    this.rbSilverlight.IsChecked = true;

                this.rbWindows.IsEnabled = this.rbWeb.IsEnabled = this.rbSilverlight.IsEnabled = false;

                this.cbExpand.IsChecked = (prop & 0x0002) == 0x0002;
            }
        }

        protected override void OnDataEntityChanged(EventArgs e)
        {
            base.OnDataEntityChanged(e);

            if (this.Group != null)
            {
                if (!iclose)
                {
                    this.tbName.Text = this.Group.Name;
                    this.tbDescription.Text = this.Group.Description;
                    this.Props = this.Group.Attributes;
                    this.LoadMemberList();
                    this.tbName.IsEnabled = false;
                }
            }
        }

        void LoadMemberList()
        {
            QueryTask<Module> task = new QueryTask<Module>();
            IModuleService service = ServiceContainer.GetService<IModuleService>(task);
            service.GetModules(new Guid(this.Group.ID));
            task.Completed +=
                (s, e2) =>
                {
                    EAS.Controls.Window.HideLoading();
                    if (task.Error != null)
                    {
                        MessageBox.Show("读取成员模块时发生错误：" + task.Error.Message, "错误", MessageBoxButton.OK);
                        return;
                    }
                    else
                    {
                        this.memberList = task.Entities;
                        this.dataMembers.ItemsSource = null;
                        this.dataMembers.ItemsSource = this.memberList;
                    }
                };
        }

        private void Apply()
        {
            if (this.tbName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("导航名称不能为空", "提示", MessageBoxButton.OK);
                this.tcMain.SelectedIndex = 0;
                this.tbName.Focus();
                return;
            }

            NavigateGroup vItem = this.Group;
            if (this.Group != null)
            {
                this.UpdateGroup();
            }
            else
            {
                this.NewGrpup();
            }
        }

        void NewGrpup()
        {
            Cursor c = this.Cursor;
            this.Cursor = Cursors.Wait;
            this.btnOK.IsEnabled = false;

            NavigateGroup vItem = new NavigateGroup();
            vItem.ID = Guid.NewGuid().ToString().ToUpper();
            vItem.Name = this.tbName.Text;
            vItem.Description = this.tbDescription.Text;
            vItem.Attributes = this.Props;
            vItem.ParentID = this.ParentID.ToUpper();

            DataPortal<NavigateGroup> dp = new DataPortal<NavigateGroup>();
            dp.BeginExistsInDb(vItem).Completed +=
                (s, e) =>
                {
                    FuncTask funcTask = s as FuncTask;
                    if (funcTask.Error != null)
                    {
                        this.Cursor = c;
                        this.btnOK.IsEnabled = true;
                        MessageBox.Show("检查导航信息时发生错误：" + funcTask.Error.Message, "错误", MessageBoxButton.OK);
                        return;
                    }
                    else
                    {
                        List<string> members = new List<string>(this.memberList.Count);
                        foreach (var item in this.memberList)
	                    {
		                    members.Add(item.Guid.ToUpper());
	                    }

                        FuncTask task = new FuncTask();
                        IGroupService service = ServiceContainer.GetService<IGroupService>(task);
                        service.UpdateGroup(vItem, members);

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
                                    this.DataEntity = task.Result;
                                    this.DialogResult = true;
                                    this.Close();
                                }
                            };
                    }
                };            
        }

        void UpdateGroup()
        {
            Cursor c = this.Cursor;
            this.Cursor = Cursors.Wait;
            this.btnOK.IsEnabled = false;

            this.Group.Name = this.tbName.Text;
            this.Group.Description = this.tbDescription.Text;

            List<string> members = new List<string>(this.memberList.Count);
            foreach (var item in this.memberList)
            {
                members.Add(item.Guid);
            }

            InvokeTask task = new InvokeTask();
            IGroupService service = ServiceContainer.GetService<IGroupService>(task);
            service.UpdateGroup(this.Group, members);

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

        private void cbMS_Checked(object sender, RoutedEventArgs e)
        {
            this.btnOK.IsEnabled = true;
            int count = this.memberList.Where(p => p.Checked).Count();
            this.btnMDelete.IsEnabled = count > 0;
        }

        private void btnMAdd_Click(object sender, RoutedEventArgs e)
        {
            ModuleSelector Selector = new ModuleSelector();
            
            int mask = 0x0004;
            int mask2 = 0x0008;
            //int mask3 = (int)GoComType.SilverlightUI;
            if ((this.Group.Attributes & mask) == mask)
                Selector.SelectMask = (int)GoComType.WinUI;
            else if ((this.Group.Attributes & mask2) == mask2)
                Selector.SelectMask = (int)GoComType.WebUI;
            else
                Selector.SelectMask = (int)GoComType.SilverUI;

            Selector.Closed += new EventHandler(Selector_Closed);
            Selector.Show();
        }

        void Selector_Closed(object sender, EventArgs e)
        {
            ModuleSelector Selector = sender as ModuleSelector;
            if (Selector.DialogResult ?? false)
            {
                IList<Module> selectList = Selector.SelectedModules;
                foreach (var item in selectList)
                {
                    int count = this.memberList.Where(p => (p.Guid == item.Guid)).Count();
                    if (count < 1)
                    {
                        this.memberList.Add(item);
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

        private void tbInput_TextChanged(object sender, System.EventArgs e)
        {
            this.btnOK.IsEnabled = true;
        }
    }
}
