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

namespace EAS.SilverlightClient.Controls
{
    partial class LoaderControl : UserControl
    {
        List<SmartFile> files = new List<SmartFile>();
        Downloader downloader;
        SmartFile m_File = null;
 
        public LoaderControl()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(LoaderControl_Loaded);
            this.HideLayoutRoot.Completed += new EventHandler(HideLayoutRoot_Completed);
        }

        public string Text
        {
            get
            {
                return this.PercentageCounter.Text;
            }
            set
            {
                this.loading.Visibility = Visibility.Collapsed;
                this.PercentageCounter.FontFamily = new FontFamily("SimSun");
                this.PercentageCounter.Text = value;
            }
        }

        void HideLayoutRoot_Completed(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            this.HideLayoutRoot.Stop();
            this.RotateLoading.Stop();
        }

        void LoaderControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.RotateLoading.Begin();
        }

        public void Initialize()
        {
            while (SLContext.Instance.UpdateXml == null)
            {
                System.Threading.Thread.Sleep(50);
            }

            this.Text = "正在准备下载系统运行所必须的程序及资源...";

            this.files = new List<SmartFile>();
            IList<SmartFile> files2 = DownloadHelper.Instance.GetDownList();
            foreach (SmartFile item in files2)
            {
                this.files.Add(item);
            }

            if (this.files.Count > 0)
            {
                this.downloader = new Downloader();
                this.downloader.DownloadCompleted += new EventHandler<FileDownloadCompleteEventArgs>(downloader_DownloadCompleted);
                this.downloader.DownloadProgressChanged += new EventHandler<FileDownloadProgressEventArgs>(downloader_DownloadProgressChanged);
                this.downloader.ErrorAccured += new EventHandler<FileDownloadErrorAccured>(downloader_ErrorAccured);
                m_File = this.files[0];
                this.Text = "准备下载文件" + this.m_File.FileName + "...";
                this.downloader.DownloadFile(m_File.FileName);
            }
            else
            {
                if (LoginContext.Singleton.Account != null)  //已经成功登录
                {
                    App.StartMain();
                }
                else  //未能成功登录
                {
                    App.Navigation(PlugContext.LoginForm as UserControl);
                }
            }
        }

        void downloader_ErrorAccured(object sender, FileDownloadErrorAccured e)
        {
            this.Text = "网络不通，请检查网络连接是否可用。";
            Downloader.Logger(this.Text);
            Downloader.Logger(e.Exception.Message);
        }

        void downloader_DownloadProgressChanged(object sender, FileDownloadProgressEventArgs e)
        {
            this.Text = "正在下载文件"+this.m_File.FileName+",下载进度"+e.ProgressPercent+"%";
            Downloader.Logger(this.Text);
        }

        void downloader_DownloadCompleted(object sender, FileDownloadCompleteEventArgs e)
        {
            this.Text = "文件" + this.m_File.FileName + "文件下载完成100%";
            Downloader.Logger(this.Text);

            byte[] buffer = new byte[e.MemoryStream.Length];
            e.MemoryStream.Read(buffer, 0, buffer.Length);
            DownloadHelper.Instance.WriteSmartFile(this.m_File, buffer);

            this.files.RemoveAt(0);

            if (this.files.Count > 0)
            {
                m_File = this.files[0];
                this.Text = "准备下载文件" + this.m_File.FileName + "...";
                this.downloader.DownloadFile(m_File.FileName);
            }
            else
            {
                DownloadHelper.Instance.WriteLocalUpdateXml();

                if (LoginContext.Singleton.Account != null)  //已经成功登录
                {
                    SLContext.Instance.Shell = new UI.MainPage();
                    App.Navigation(SLContext.Instance.Shell);
                }
                else  //未能成功登录
                {
                    App.Navigation(PlugContext.LoginForm as UserControl);
                }
            }
        }

        public void Hide()
        {
            this.RotateLoading.Pause();
            this.HideLayoutRoot.Begin();
        }
    }
}
