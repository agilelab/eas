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
using System.IO.IsolatedStorage;
using EAS.Explorer;
using System.IO;

namespace EAS.Demo.Res
{
    public partial class LoginPage : UserControl
    {
        public LoginPage()
        {
            InitializeComponent();
            
            if ((System.Windows.Application.Current.InstallState == InstallState.Installed) && System.Windows.Application.Current.IsRunningOutOfBrowser)
            {
                System.Windows.Application.Current.CheckAndDownloadUpdateCompleted += new CheckAndDownloadUpdateCompletedEventHandler(this.Current_CheckAndDownloadUpdateCompleted);
                System.Windows.Application.Current.CheckAndDownloadUpdateAsync();
            }

            this.LoadCookie();
        }

        void Current_CheckAndDownloadUpdateCompleted(object sender, CheckAndDownloadUpdateCompletedEventArgs e)
        {
            if (e.UpdateAvailable)
            {
                this.btnLogin.IsEnabled = false;
                MessageBox.Show("AgileEAS.NET SOA平台已经下载并更新了最新版本，请您退出后重新运行AgileEAS.NET SOA平台。");
            }
            else if ((e.Error != null) && (e.Error is PlatformNotSupportedException))
            {
                this.btnLogin.IsEnabled = false;
                MessageBox.Show("AgileEAS.NET SOA平台已经发布了新版本，但运行新版本需要更新您的Silverlight插件。\r\n请您登录 http://smarteas.net 以安装最新的运行环境。");
            }
        }

        void LoadCookie()
        {
            using (IsolatedStorageFile storeFile = IsolatedStorageFile.GetUserStoreForApplication())
            {
                string fileName = "LoginCookie.tx";
                if (storeFile.FileExists(fileName))
                {
                    IsolatedStorageFileStream fileStream = storeFile.OpenFile(fileName, FileMode.Open, FileAccess.Read);
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        string loginID = reader.ReadToEnd();
                        this.tbLoginID.Text = loginID;
                    }
                }
            }
        }

        void SetCookie()
        {
            using (IsolatedStorageFile storeFile = IsolatedStorageFile.GetUserStoreForApplication())
            {
                string fileName = "LoginCookie.tx";
                using (StreamWriter writeFile = new StreamWriter(new IsolatedStorageFileStream(fileName, FileMode.OpenOrCreate, storeFile)))
                {
                    writeFile.Write(this.tbLoginID.Text);
                }
            }
        }        

        private void tbLoginID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.tbPassword.Focus();
            }
        }

        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.btnLogin.Focus();
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            this.SetCookie();

            using (IsolatedStorageFile storeFile = IsolatedStorageFile.GetUserStoreForApplication())
            {
                long oldSize = storeFile.Quota;
                long newSize = 512 * 1024 * 1024;
                if ((oldSize+(1024*1024)) < newSize)
                {
                    if (!storeFile.IncreaseQuotaTo(newSize))
                    {
                        MessageBox.Show("申请独立存储空间不成功，程序无法继续运行。");
                        return;
                    }
                }
            }

            string LoginID = this.tbLoginID.Text.Trim();
            if (LoginID.Length == 0)
            {
                this.tbLoginID.Focus();
                MessageBox.Show("请输入要登录的账户名称或者账户ID。");
                return;
            }

            if (string.Compare(LoginID,"Guest",StringComparison.CurrentCultureIgnoreCase) == 0)
            {
                this.tbLoginID.Focus();
                this.tbLoginID.SelectAll();
                MessageBox.Show("Guest用户不充许登录系统,请重新输入账户名称或者账户ID。");
                return;
            }

            (Application.Instance as IApplication).Login(this.tbLoginID.Text.Trim(), this.tbPassword.Password);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            (EAS.Application.Instance as IApplication).AppEnd();
        }        
    }
}
