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
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.IO;
using System.Linq;
using EAS.Configuration;

namespace EAS.SilverlightClient
{
    class DownloadHelper
    {
        #region 单例模式

        static DownloadHelper instance = new DownloadHelper();
        static readonly object _lock = new object();

        /// <summary>
        /// 单例。
        /// </summary>
        public static DownloadHelper Instance
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new DownloadHelper();
                    }
                }

                return instance;
            }
        }

        DownloadHelper()
        {

        }

        #endregion

        List<SmartFile> files = new List<SmartFile>();

        /// <summary>
        /// 下载清单。
        /// </summary>
        /// <returns></returns>
        public IList<SmartFile> GetDownList()
        {
            //如果为测试程序，下载所有文件
            string debug = Config.GetValue("Debug");
            SLContext.Instance.Debug = string.Compare(debug, "true", StringComparison.CurrentCultureIgnoreCase) == 0;
            if (SLContext.Instance.Debug)
            {
                SLContext.Instance.Assembly = Config.GetValue("Assembly");
            }

            string m_urlHost = string.Empty;

            try
            {
                m_urlHost = System.Windows.Application.Current.Host.Source.ToString();
            }
            catch
            {
                System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    m_urlHost = System.Windows.Application.Current.Host.Source.ToString();
                });

                while (m_urlHost == string.Empty)
                {
                    System.Threading.Thread.Sleep(50);
                }
            }

            //如果不是第一次执行。2012-07-11
            if (this.files.Count >0 )
                return this.files;

            this.files.Clear();

            if (SLContext.Instance.UpdateXml == null)
            {
                return files;
            }

            SmartConfig sc1 = SLContext.Instance.UpdateXml;
            if (sc1.Files.Count == 0)
                return files;

            if (m_urlHost.ToLower().StartsWith("http://localhost") || m_urlHost.ToLower().StartsWith("http://127.0.0.1"))
            {
                return sc1.Files;
            }

            SmartConfig sc2 = null;
            try
            {
                sc2 = this.GetLocalUpdateXml();
            }
            catch { }

            if (sc2 == null)
            {
                return sc1.Files;
            }

            //Debug状态。
            if (SLContext.Instance.Debug)
                return sc1.Files;

            //
            foreach (SmartFile item in sc1.Files)
            {
                var v = from c in sc2.Files
                        where c.FileName == item.FileName
                        select c;

                SmartFile item2 = v.FirstOrDefault();

                if (item2 == null)
                {
                    files.Add(item);
                }
                else
                {
                    if (item.Time > item2.Time)
                    {
                        files.Add(item);
                    }
                }
            }

            return files;
        }

        /// <summary>
        /// 保存本地配置文件。
        /// </summary>
        public void WriteLocalUpdateXml()
        {
            if (SLContext.Instance.UpdateXml != null)
            {
                using (IsolatedStorageFile storeFile = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    string fileName = "slupdate.xml";
                    using (StreamWriter writeFile = new StreamWriter(new IsolatedStorageFileStream(fileName, FileMode.OpenOrCreate, storeFile)))
                    {
                        writeFile.Write(SLContext.Instance.UpdateXml.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// 读本地配置。
        /// </summary>
        /// <returns></returns>
        SmartConfig GetLocalUpdateXml()
        {
            using (IsolatedStorageFile storeFile = IsolatedStorageFile.GetUserStoreForApplication())
            {
                //string fileName = "config\\slupdate.xml";
                string fileName = "slupdate.xml";

                if (storeFile.FileExists(fileName))
                {
                    IsolatedStorageFileStream fileStream = storeFile.OpenFile(fileName, FileMode.Open, FileAccess.Read);
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        string xml = reader.ReadToEnd();
                        return SmartConfig.LoadXML(xml);
                    }
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// 写入本地文件。
        /// </summary>
        /// <param name="fileName"></param>
        public void WriteSmartFile(SmartFile sf, byte[] buffer)
        {
            using (IsolatedStorageFile storeFile = IsolatedStorageFile.GetUserStoreForApplication())
            {
                string fileName = sf.FileName;
                using (IsolatedStorageFileStream fileStream = new IsolatedStorageFileStream(fileName, FileMode.OpenOrCreate, storeFile))
                {
                    using (BinaryWriter writer = new BinaryWriter(fileStream))
                    {
                        writer.Write(buffer, 0, buffer.Length);
                    }
                }
            }
        }
    }
}
