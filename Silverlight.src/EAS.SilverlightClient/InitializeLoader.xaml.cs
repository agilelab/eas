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
using EAS.Services;
using System.IO.IsolatedStorage;

namespace EAS.SilverlightClient
{
    public partial class InitializeLoader : UserControl
    {
        public InitializeLoader()
        {
            InitializeComponent();

            this.loaderControl.Text = "正在检查系统配置...";
            this.DownSlConfig();            
        }

        /// <summary>
        /// 下载配置文件。
        /// </summary>
        void DownSlConfig()
        {
            //A.初始化系统
            System.Threading.WaitCallback callBack = (state) =>
            {
                try
                {
                    EAS.ConfigManager.Current.Load();
                }
                catch (System.Exception exc)
                {
                    Dispatcher.BeginInvoke(() =>
                    {
                        MessageBox.Show("读系统配置信息出错：" + exc.Message, "错误", MessageBoxButton.OK);
                    });
                    return;
                }

                Dispatcher.BeginInvoke(() =>
                {
                    this.loaderControl.Text = "系统配置检查完成...";
                    this.loaderControl.Text = "正在加载系统资源...";
                });                

                //1.读系统配置。
                string resourceInfo = string.Empty;
                try
                {
                    resourceInfo = EAS.Configuration.Config.GetValue("EAS.Explorer.Resource");
                }
                catch { }

                //2.加载资源。
                try
                {
                    ResourceLoader loader = new ResourceLoader();
                    loader.Load(resourceInfo);
                }
                catch //(System.Exception exc)
                {
                    //Dispatcher.BeginInvoke(() =>
                    //{
                    //    MessageBox.Show("加载系统资源出错：" + exc.Message, "错误", MessageBoxButton.OK);
                    //});
                    //return;
                }

                //3.读系统时间/藏在读资源之中。
                DateTimeClient.Initialize();

                //4.检查独立存储，转向。
                Dispatcher.BeginInvoke(() =>
                {
                    this.loaderControl.Text = "系统资源加载完成...";

                    EAS.SilverlightClient.Application instance = new EAS.SilverlightClient.Application();
                    new ApplicationBridge().SetInstance2(instance);
                    (EAS.SilverlightClient.Application.Instance as EAS.SilverlightClient.Application).AppStart();

                    using (IsolatedStorageFile storeFile = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        long oldSize = storeFile.Quota;
                        long newSize = 128 * 1024 * 1024;
                        if ((oldSize + (1024 * 1024)) < newSize)
                        {
                            LoginContext.Singleton.IsolatedStorage = true;
                            App.Navigation(PlugContext.LoginForm as UserControl);
                        }
                        else
                        {
                            App.Navigation(new UpdateLoader());
                        }
                    }
                });
            };

            //A.线程程同步。
            System.Threading.ThreadPool.QueueUserWorkItem(callBack);
        }
    }
}
