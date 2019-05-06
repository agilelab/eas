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
using EAS.SilverlightClient.UI;
using System.Xml.Linq;
using EAS.Services;
using System.IO.IsolatedStorage;
using System.Threading;

namespace EAS.SilverlightClient
{
    public partial class App : System.Windows.Application
    {
        Grid rootGrid = new Grid();

        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;

            System.Threading.Thread.Sleep(10);
            InitializeComponent(); 
        }

        /// <summary>
        /// 打开项目主页
        /// </summary>
        public static void StartMain()
        {
            SLContext.Instance.Shell = new UI.MainPage();
            App.Navigation(SLContext.Instance.Shell);
        }

        /// <summary>
        /// 导航到指定页面
        /// </summary>
        /// <param name="newPage"></param>
        public static void Navigation(UserControl newPage)
        {
            App currentApp = (App)System.Windows.Application.Current;
            currentApp.rootGrid.Children.Clear();
            currentApp.rootGrid.Children.Add(newPage);
            EAS.Controls.Window.ChangeRootGrid();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            this.RootVisual = rootGrid;
            rootGrid.Children.Add(new InitializeLoader());
            EAS.Controls.Window.ChangeRootGrid();
        }

        private void Application_Exit(object sender, EventArgs e)
        {

        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // 如果应用程序是在调试器外运行的，则使用浏览器的
            // 异常机制报告该异常。在 IE 上，将在状态栏中用一个 
            // 黄色警报图标来显示该异常，而 Firefox 则会显示一个脚本错误。
            if (!System.Diagnostics.Debugger.IsAttached)
            {

                // 注意: 这使应用程序可以在已引发异常但尚未处理该异常的情况下
                // 继续运行。 
                // 对于生产应用程序，此错误处理应替换为向网站报告错误
                // 并停止应用程序。
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e); });
            }
            else
            {
                Deployment.Current.Dispatcher.BeginInvoke(
                    delegate
                    {
                        ErrorBox errorBox = new ErrorBox();
                        errorBox.Error = e.ExceptionObject;
                        e.Handled = true;
                        errorBox.Show();
                    }
                    );
            }
        }

        private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }
    }
}
