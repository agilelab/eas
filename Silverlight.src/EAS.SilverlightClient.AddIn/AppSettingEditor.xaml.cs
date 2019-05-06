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
    partial class AppSettingEditor : DataWindow
    {
        bool iclose = false;

        public AppSettingEditor()
        {
            InitializeComponent();
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

        public AppSetting AppSetting
        {
            get
            {
                return this.DataEntity as AppSetting;
            }
            set
            {
                this.DataEntity = value;
            }
        }

        protected override void OnDataEntityChanged(EventArgs e)
        {
            base.OnDataEntityChanged(e);

            if (this.AppSetting != null)
            {
                if (!iclose)
                {
                    this.tbName.Text = this.AppSetting.Key;
                    this.tbCategory.Text = this.AppSetting.Category;
                    this.tbDescription.Text = this.AppSetting.Description;
                    this.tbValue.Text = this.AppSetting.Value;
                    this.tbCategory.IsEnabled = false;
                    this.tbName.IsEnabled = false;
                }
            }
        }

        private void Apply()
        {
            if (this.tbCategory.Text.Trim() == string.Empty)
            {
                MessageBox.Show("参数目录不能为空", "提示", MessageBoxButton.OK);
                this.tcMain.SelectedIndex = 0;
                this.tbCategory.Focus();
                return;
            }

            if (this.tbName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("参数名称不能为空", "提示", MessageBoxButton.OK);
                this.tcMain.SelectedIndex = 0;
                this.tbName.Focus();
                return;
            }

            AppSetting vItem = this.AppSetting;
            if (this.AppSetting != null)
            {
                this.UpdateItem();
            }
            else
            {
                this.NewItem();
            }
        }

        void NewItem()
        {
            Cursor c = this.Cursor;
            this.Cursor = Cursors.Wait;
            this.btnOK.IsEnabled = false;

            AppSetting vItem = new AppSetting();

            vItem.Category = this.tbCategory.Text;
            vItem.Key = this.tbName.Text;
            vItem.Description = this.tbDescription.Text;
            vItem.Value = this.tbValue.Text;

            DataPortal<AppSetting> dp = new DataPortal<AppSetting>();
            dp.BeginExistsInDb(vItem).Completed +=
                (s, e) =>
                {
                    FuncTask funcTask = s as FuncTask;
                    if (funcTask.Error != null)
                    {
                        this.Cursor = c;
                        this.btnOK.IsEnabled = true;
                        MessageBox.Show("检查参数信息时发生错误：" + funcTask.Error.Message, "错误", MessageBoxButton.OK);
                        return;
                    }
                    else
                    {
                        bool exists = (bool)funcTask.Result;
                        if (exists)
                        {
                            this.Cursor = c;
                            this.btnOK.IsEnabled = true;
                            this.tbName.Focus();
                            MessageBox.Show("已存在同名的账户类型，请重新定义账户类型信息!" + funcTask.Error.Message, "错误", MessageBoxButton.OK);
                            return;
                        }

                        dp.BeginInsert(vItem).Completed +=
                            (s2, e2) =>
                            {
                                InvokeTask task = s2 as InvokeTask;
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

        void UpdateItem()
        {
            Cursor c = this.Cursor;
            this.Cursor = Cursors.Wait;
            this.btnOK.IsEnabled = false;

            this.AppSetting.Key = this.tbName.Text;
            this.AppSetting.Category = this.tbCategory.Text;
            this.AppSetting.Value = this.tbValue.Text;
            this.AppSetting.Description = this.tbDescription.Text;

            DataPortal<AppSetting> dp = new DataPortal<AppSetting>();
            dp.BeginUpdate(this.AppSetting).Completed +=
                (s, e2) =>
                {
                    InvokeTask task = s as InvokeTask;
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

        private void tbInput_TextChanged(object sender, System.EventArgs e)
        {
            this.btnOK.IsEnabled = true;
        }
    }
}
