using System;

using EAS.Data.Access;
using EAS.Data.ORM;
using System.Xml.Serialization;

namespace EAS.Explorer.Entities
{
    /// <summary>
    /// 实体对象 Account(系统账户)。
    /// </summary>
    public partial class Account : DataEntity<Account>, IAccount
    {
        #region IAccount 成员

        Guid IAccount.Certificate
        {
            get
            {
                return new Guid(this.Certificate);
            }
            set
            {
                this.Certificate = value.ToString();
            }
        }

        public bool EnableCertificate
        {
            get
            {
                if (this.Certificate == string.Empty)
                    return false;
                else
                {
                    Guid guid = new Guid(this.Certificate);
                    return guid != Guid.Empty;
                }
            }
        }

        public bool IsOnline
        {
            get
            {
                return (this.Attributes & 0x0100) == 0x0100;
            }
            set
            {
                if (value)
                {
                    this.Attributes = this.Attributes | 0x0100;
                }
                else
                {
                    this.Attributes = this.Attributes & 0xffffeff;
                }
            }
        }

        public bool IsLocked
        {
            get
            {
                return (this.Attributes & 0x0010) == 0x0010;
            }
        }

        public bool IsDisabled
        {
            get
            {
                return (this.Attributes & 0x0008) == 0x0008;
            }
        }

        #endregion

        #region IClient 成员

        public string ID
        {
            get
            {
                return this.LoginID;
            }
        }

        #endregion

        /// <summary>
        /// 组织机构。
        /// </summary>
        [Column("Organization", "虚拟"), Virtual]
        [XmlIgnore]
        public IOrganization Organization
        {
            get
            {
                return this.GetValue<IOrganization>("Organization");
            }
            set
            {
                this["Organization"] = value;
            }
        }

        /// <summary>
        /// 组织机构。
        /// </summary>
        [Column("OnlineState", "在线状态"), Virtual]
        //[XmlIgnore]
        public int  OnlineState
        {
            get
            {
                return this.GetValue<int>("OnlineState");
            }
            set
            {
                this["OnlineState"] = value;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}({1})", this.Name, this.LoginID);
        }
    }
}
