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
using System.IO;

namespace EAS.SilverlightClient
{
    public partial class UpdateLoader : UserControl
    {
        public UpdateLoader()
        {
            InitializeComponent();
            this.loaderControl.Text = "正在检查系统升级...";
            this.DownUpdateConfig();            
        }

        void DownUpdateConfig()
        {
            System.Threading.WaitCallback callBack = (state) =>
            {
                //Uri url = new Uri(System.Windows.Application.Current.Host.Source, "slupdate.xml");
                Uri url = new Uri("slupdate.xml",UriKind.Relative);
                WebClient client = new WebClient();
                System.Threading.Tasks.Task<Stream> task = client.OpenReadTaskAsync(url);
                task.Wait();

                if (task.Exception != null)
                {
                    Dispatcher.BeginInvoke(() =>
                    {
                        MessageBox.Show("读升级参数出错：" + task.Exception, "错误", MessageBoxButton.OK);
                        return;
                    });
                }

                using (var sw = new System.IO.StreamReader(task.Result))
                {
                    string xml = sw.ReadToEnd();
                    task.Result.Close();

                    try
                    {
                        SLContext.Instance.UpdateXml = SmartConfig.LoadXML(xml);
                    }
                    catch { }
                }

                Dispatcher.BeginInvoke(() =>
                    {
                        this.loaderControl.Initialize();
                    });
            };

            //X.线程同步。
            System.Threading.ThreadPool.QueueUserWorkItem(callBack);
        }
    }
}
