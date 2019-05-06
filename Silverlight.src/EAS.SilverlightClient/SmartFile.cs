using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;

namespace EAS.SilverlightClient
{
    /// <summary>
    /// 升级文件定义。
    /// </summary>
    [Serializable]
    [DataContract]
    public class SmartFile
    {
        string fileName;
        string version;
        DateTime time;

        /// <summary>
        /// 文件名称。
        /// </summary>
        [DataMember]
        public string FileName
        {
            get
            {
                return this.fileName;
            }
            set
            {
                this.fileName = value;
            }
        }

        /// <summary>
        /// 版本。
        /// </summary>
        [DataMember]
        public string Version
        {
            get
            {
                return this.version;
            }
            set
            {
                this.version = value;
            }
        }        

        /// <summary>
        /// 时间。
        /// </summary>
        [DataMember]
        public DateTime Time
        {
            get
            {
                return this.time;
            }
            set
            {
                this.time = value;
            }
        }
    }
}
