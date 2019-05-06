using System;

using EAS.Data.Access;
using EAS.Data.ORM;
using System.Xml.Serialization;

namespace EAS.Explorer.Entities
{
    /// <summary>
    /// 实体对象 Organization(机构部门)。
    /// </summary>
    public partial class Organization : DataEntity<Organization>, IOrganization
    {
        [Column("Parent", "上级机构"), Virtual]
        [XmlIgnore]
        public IOrganization Parent
        {
            get
            {
                return this.GetValue<IOrganization>("Parent");
            }
            set
            {
                this["Parent"] = value;
            }
        }

        Guid IOrganization.Guid
        {
            get
            {
                return new Guid(this.Guid);
            }
            set
            {
                this.Guid = value.ToString();
            }
        }

        /// <summary>
        /// 总人数。
        /// </summary>
        [Column("Totals", "总人数"), Virtual]
        //[XmlIgnore]
        public int Totals
        {
            get
            {
                return this.GetValue<int>("Totals");
            }
            set
            {
                this["Totals"] = value;
            }
        }

        /// <summary>
        /// 在线人数。
        /// </summary>
        [Column("Onlines", "在线人数"), Virtual]
        //[XmlIgnore]
        public int Onlines
        {
            get
            {
                return this.GetValue<int>("Onlines");
            }
            set
            {
                this["Onlines"] = value;
            }
        }
    }
}
