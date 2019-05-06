using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;

namespace EAS.SilverlightClient
{
    /// <summary>
    /// 下载完成参数。
    /// </summary>
    public class FileDownloadCompleteEventArgs : EventArgs
    {
        // Methods
        public FileDownloadCompleteEventArgs(Stream downloadStream)
        {
            this.MemoryStream = downloadStream;
        }

        // Properties
        public Stream MemoryStream { get; set; }
    }

    /// <summary>
    /// 下载错误。
    /// </summary>
    public class FileDownloadErrorAccured : EventArgs
    {
        public Exception Exception
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 下载进度参数。
    /// </summary>
    public class FileDownloadProgressEventArgs : EventArgs
    {
        // Methods
        public FileDownloadProgressEventArgs(int progressPercent)
        {
            this.ProgressPercent = progressPercent;
        }

        // Properties
        public int ProgressPercent
        {
            get;
            set;
        }
    }
}
