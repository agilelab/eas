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
using EAS.Explorer.BLL;
using EAS.Services;

namespace EAS.SilverlightClient.UI
{
    [Module("0983598B-2C3B-4CB9-BA8C-7E5F1F7EC63F", "密码修改", "AgileEAS.NET平台WinForm/Wpf容器密码修改模块")]
    partial class PasswordWindow : ChildWindow
    {
        [ModuleStart]
        public void Start()
        {
            this.LoginID = this.LoginID == null || this.LoginID == string.Empty ? SLContext.Account.LoginID : this.LoginID;
            this.Show();
        }

        string loginid = string.Empty;

        public PasswordWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取或者设置要设置密码的帐户的登录ID。
        /// </summary>
        internal string LoginID
        {
            get
            {
                return this.loginid;
            }
            set
            {
                this.loginid = value;
            }
        }

        private void tbPass1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.tbPass2.Focus();
            }
        }

        private void tbPass2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.btnOK.Focus();
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.tbPass1.Password != this.tbPass1.Password)
            {
                MessageBox.Show("您两次输入的密码不一致，请重新确定密码。");
                this.tbPass1.Focus();
                return;
            }

            if (this.tbPass1.Password == string.Empty)
            {
                if (MessageBox.Show("您要将密码设置为空密码，这样会使您的工作和数据很容易被窃取。\n系统强烈建议您不要将密码设置为空密码，是否要设定密码为空密码？", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                {
                    this.tbPass1.Focus();
                    return;
                }
            }

            Cursor c = this.Cursor;
            this.Cursor = Cursors.Wait;
            InvokeTask task = new InvokeTask();
            IAccountService service = ServiceContainer.GetService<IAccountService>(task);
            service.UpdatePassword(this.loginid, this.tbPass1.Password);
            task.Completed +=
                (s, e2) =>
                {
                    this.Cursor = c;
                    if (task.Error != null)
                    {
                        if (task.Error.InnerException != null)
                            MessageBox.Show(task.Error.InnerException.Message, "密码修改过程中出现未知错误，请记录下面的异常信息，并通知您的系统管理员", MessageBoxButton.OK);
                        else
                            MessageBox.Show("密码修改过程中出现未知错误，请记录下面的异常信息，并通知您的系统管理员。\n\n" + task.Error.Message, "错误", MessageBoxButton.OK);
                    }
                    else
                    {
                        MessageBox.Show("您已经成功的修改了“" + this.loginid + "”的登录密码，请牢记您刚刚设置的密码！", "提示", MessageBoxButton.OK);
                        this.DialogResult = true;
                    }
                };
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}

