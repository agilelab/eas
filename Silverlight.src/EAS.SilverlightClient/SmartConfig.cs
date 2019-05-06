using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using EAS.Serialization;
using System.IO.IsolatedStorage;

namespace EAS.SilverlightClient
{
    /// <summary>
    /// 升级配置信息。
    /// </summary>
    [Serializable]
    [DataContract]
    public class SmartConfig
    {
        string url = "http://www.smarteas.net/";
        string name = "AgileEAS.NET升级配置文件";
        string description = "用于AgileEAS.NET平台SmartClient/ActiveX/Silverlight运行容器模块升级之用";
        string startEx = string.Empty;

        DateTime time = DateTime.Now;
        List<SmartFile> files;

        string fileName = "slupdate.xml";

        /// <summary>
        /// 初始化SmartConfig对象实例。
        /// </summary>
        public SmartConfig()
        {
            this.files = new List<SmartFile>();
        }

        /// <summary>
        /// URL地址。
        /// </summary>
        [DataMember]
        public string URI { get; set; }

        /// <summary>
        /// 名称。
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 说明。
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// 最后更新时间。
        /// </summary>
        [DataMember]
        public DateTime Time { get; set; }

        /// <summary>
        /// 系统运行必须的一些文件。
        /// </summary>
        [XmlArray()]
        [XmlArrayItem(typeof(SmartFile))]
        [DataMember]
        public List<SmartFile> Files { get; set; }

        /// <summary>
        /// 装载智能升级配置。
        /// </summary>
        /// <param name="xml">xml。</param>
        /// <returns>智能升级配置。</returns>
        public static SmartConfig LoadXML(string xml)
        {
            return SerializeHelper.DeserializeXml<SmartConfig>(xml);
        }

        /// <summary>
        /// 转换为XML字符串。
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return SerializeHelper.SerializeXml<SmartConfig>(this);
        }

        /// <summary>
        /// 系统默认配置文件。
        /// </summary>
        public static string DefaultConfgiFile
        {
            get
            {
                return "slUpdate.xml";
            }
        }
    }
}
