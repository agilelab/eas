using System;
using System.IO;
using System.Net;
using System.IO.IsolatedStorage;
using System.Text;
using System.Windows;

namespace EAS.SilverlightClient
{
    class Downloader
    {
        /// <summary>
        /// 写下载日志。
        /// </summary>
        /// <param name="Text"></param>
        public static void Logger(string Text)
        {
            string xText = string.Format("{0}:{1}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), Text);
            using (IsolatedStorageFile storeFile = IsolatedStorageFile.GetUserStoreForApplication())
            {
                string fileName = "Download.log";

                using (StreamWriter writeFile = new StreamWriter(new IsolatedStorageFileStream(fileName, FileMode.Append, storeFile), Encoding.UTF8))
                {
                    writeFile.WriteLine(xText);
                }
            }
        }

        ///// <summary>
        ///// 写下载日志。
        ///// </summary>
        ///// <param name="Text"></param>
        //public static void Logger2(string Text)
        //{
        //    string xText = string.Format("{0}:{1}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), Text);
        //    using (IsolatedStorageFile storeFile = IsolatedStorageFile.GetUserStoreForApplication())
        //    {
        //        string fileName = "Download2.log";

        //        using (StreamWriter writeFile = new StreamWriter(new IsolatedStorageFileStream(fileName, FileMode.Append, storeFile), Encoding.UTF8))
        //        {
        //            writeFile.WriteLine(xText);
        //        }
        //    }
        //}

        public event EventHandler<FileDownloadCompleteEventArgs> DownloadCompleted = null;
        public event EventHandler<FileDownloadProgressEventArgs> DownloadProgressChanged = null;
        public event EventHandler<FileDownloadErrorAccured> ErrorAccured = null;

        public void DownloadFile(string xapName)
        {
            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(OnDownloadProgressChanged);
            client.OpenReadCompleted += new OpenReadCompletedEventHandler(OnOpenReadCompleted);
            Uri url = new Uri(System.Windows.Application.Current.Host.Source, xapName);
            //Uri url = new Uri(string.Format("http://mzb.51gh.net/clientbin/{0}", xapName));
            //client.OpenReadAsync(new Uri(xapName, UriKind.Relative));
            Logger(url.ToString());
            //MessageBox.Show(url.ToString());
            client.OpenReadAsync(url);
        }

        void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (this.DownloadProgressChanged != null)
            {
                this.DownloadProgressChanged(this, new FileDownloadProgressEventArgs(e.ProgressPercentage));
            }
        }

        void OnOpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                if (this.ErrorAccured != null)
                {
                    FileDownloadErrorAccured accured = new FileDownloadErrorAccured();
                    accured.Exception = e.Error;
                    this.ErrorAccured(this, accured);
                }
            }
            else if (this.DownloadCompleted != null)
            {
                this.DownloadCompleted(this, new FileDownloadCompleteEventArgs(e.Result));
            }
        }
    }
}
