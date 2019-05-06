using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using EAS.Explorer.Entities;
using EAS.Data.ORM;

namespace EAS.BPM.Entities
{
    /// <summary>
    /// 流程定义聚合根。
    /// </summary>
    [Serializable]
    public class WFDefineRoot : WFDefine
    {
        private List<WFState> m_States = new List<WFState>();
        private List<ACL> m_Permissions = new List<ACL>();

        public WFDefineRoot()
            :this(null)
        {

        }

        public WFDefineRoot(WFDefine xDefine)
        {
            if (xDefine != null)
            {
                ColumnCollection columns = this.GetColumns();
                foreach (Column column in columns)
                {
                    this[column] = xDefine[column];
                }
            }
        }


        protected WFDefineRoot(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        /// <summary>
        /// 状态定义。
        /// </summary>
        [XmlArray(ElementName = "States")]
        public List<WFState> States
        {
            get
            {
                return this.m_States;
            }
            set
            {
                this.m_States = value;
            }
        }

        /// <summary>
        /// 权限定义。
        /// </summary>
        [XmlArray(ElementName = "Permissions")]
        public List<ACL> Permissions
        {
            get
            {
                return this.m_Permissions;
            }
            set
            {
                this.m_Permissions = value;
            }
        }
    }
}
