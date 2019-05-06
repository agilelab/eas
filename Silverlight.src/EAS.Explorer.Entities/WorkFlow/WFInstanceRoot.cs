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
    /// 流程实例聚合根。
    /// </summary>
    [Serializable]
    public class WFInstanceRoot : WFInstance
    {
        List<WFExecute> m_Executes = new List<WFExecute>();

        public WFInstanceRoot()
            :this(null)
        {

        }

        public WFInstanceRoot(WFInstance xInstance)
        {
            if (xInstance != null)
            {
                ColumnCollection columns = this.GetColumns();
                foreach (Column column in columns)
                {
                    this[column] = xInstance[column];
                }
            }
        }

        protected WFInstanceRoot(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        /// <summary>
        /// 执行记录。
        /// </summary>
        [XmlArray(ElementName = "Executes")]
        public List<WFExecute> Executes
        {
            get
            {
                return this.m_Executes;
            }
            set
            {
                this.m_Executes = value;
            }
        }
    }
}
