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
using EAS.Explorer.Entities;

namespace EAS.SilverlightClient.UI
{
    partial class LoginPage : UserControl
    {
        public LoginPage()
        {
            InitializeComponent();

            if ((System.Windows.Application.Current.InstallState == InstallState.Installed) && System.Windows.Application.Current.IsRunningOutOfBrowser)
            {
                System.Windows.Application.Current.CheckAndDownloadUpdateCompleted += new CheckAndDownloadUpdateCompletedEventHandler(this.Current_CheckAndDownloadUpdateCompleted);
                System.Windows.Application.Current.CheckAndDownloadUpdateAsync();
            }

            this.Loaded += new RoutedEventHandler(LoginPage_Loaded);
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

                if(storeFile.FileExists(fileName))
                    storeFile.DeleteFile(fileName);

                using (StreamWriter writeFile = new StreamWriter(new IsolatedStorageFileStream(fileName, FileMode.OpenOrCreate, storeFile)))
                {
                    writeFile.Write(this.tbLoginID.Text);
                }
            }
        }

        void LoginPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (EAS.Application.Instance != null)
                if (EAS.Application.Instance.Session.Client != null)
                {
                    IAccount account = EAS.Application.Instance.Session.Client as IAccount;
                    if (account != null)
                    {
                        if (string.Compare(account.LoginID, "Guest", StringComparison.CurrentCultureIgnoreCase) != 0)
                        {
                            App.Navigation(SLContext.Instance.Shell);
                        }
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
            this.btnLogin.IsEnabled = false;
            this.SetCookie();

            using (IsolatedStorageFile storeFile = IsolatedStorageFile.GetUserStoreForApplication())
            {
                long oldSize = storeFile.Quota;
                long newSize = 128 * 1024 * 1024;
                if ((oldSize + (1024 * 1024)) < newSize)
                {
                    if (!storeFile.IncreaseQuotaTo(newSize))
                    {
                        MessageBox.Show("申请独立存储空间不成功，程序无法继续运行。");
                        this.btnLogin.IsEnabled = true;
                        return;
                    }

                    //转到模块下载界面。
                    App.Navigation(new UpdateLoader());
                    return;
                }
            }

            string LoginID = this.tbLoginID.Text.Trim();
            if (LoginID.Length == 0)
            {
                this.tbLoginID.Focus();
                MessageBox.Show("请输入要登录的账户名称或者账户ID。");
                this.btnLogin.IsEnabled = true;
                return;
            }

            if (string.Compare(LoginID, "Guest", StringComparison.CurrentCultureIgnoreCase) == 0)
            {
                this.tbLoginID.Focus();
                this.tbLoginID.SelectAll();
                MessageBox.Show("Guest用户不充许登录系统,请重新输入账户名称或者账户ID。");
                this.btnLogin.IsEnabled = true;
                return;
            }

            //System.Threading.WaitCallback callBack = (state) =>
            //{
            //    string key = "e";
            //    using (DbEntities db = new DbEntities())
            //    {
            //        var v = from c in db.Modules
            //                where c.Assembly.StartsWith(key) || c.Type.StartsWith(key) || c.Developer.StartsWith(key)
            //                select c;
            //        //this.vList = v as IQueryable<Module>;

            //        try
            //        {
            //            int iCount = v.Count();
            //        }
            //        catch (System.Exception exc)
            //        {
            //            Dispatcher.BeginInvoke(() =>
            //            {
            //                MessageBox.Show("请求数据时发生错误：" + exc.Message, "错误", MessageBoxButton.OK);
            //            });
            //            return;
            //        }

            //        var v2 = v.ToList();

            //        Dispatcher.BeginInvoke(() =>
            //        {
            //            //int mod = iCount % PageSize;
            //            //int pages = mod == 0 ? iCount / PageSize : (iCount / PageSize) + 1;
            //            //this.pager.PageCount = pages;
            //            //this.pager.ReBind(pages);
            //            //this.GetPageList(1);
            //        });
            //    }
            //};

            //System.Threading.ThreadPool.QueueUserWorkItem(callBack);

            (Application.Instance as IApplication).Login(this.tbLoginID.Text.Trim(), this.tbPassword.Password);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            (Application.Instance as IApplication).AppEnd();
        }        
    }
}
