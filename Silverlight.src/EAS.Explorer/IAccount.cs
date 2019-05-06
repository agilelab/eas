using System;
using EAS.Data.ORM;
using EAS.Sessions;

namespace EAS.Explorer
{
    /// <summary>
    /// 系统账号接口。
    /// </summary>
    public interface IAccount : IDataEntity, IClient
    {
        /// <summary>
        /// 获取帐户对应的原始对象的标识。比如员工的工作证号。
        /// </summary>
        string OriginalID
        {
            get;
            set;
        }

        /// <summary>
        /// 获取登录ID，最大长度为 64 个字符。
        /// </summary>
        string LoginID
        {
            get;
            set;
        }

        /// <summary>
        /// 获取帐户的属性信息。
        /// </summary>
        /// <remarks>该值由相应的信息系统确定并使用。</remarks>
        int Attributes
        {
            get;
            set;
        }

        /// <summary>
        /// 获取帐户已经登录应用程序的次数。
        /// </summary>
        long LoginCount
        {
            get;
            set;
        }

        /// <summary>
        /// 获取帐户的名称，最大长度为 64 个字符。
        /// </summary>
        string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 获取对帐户的描述信息。
        /// </summary>
        string Description
        {
            get;
            set;
        }

        /// <summary>
        /// 所在机构/部门。
        /// </summary>
        IOrganization Organization
        {
            get;
            set;
        }

        /// <summary>
        /// 获取系统账号的目录，即账号类型。
        /// </summary>
        Guid Certificate
        {
            get;
            set;
        }

        /// <summary>
        /// 启用数字证书。
        /// </summary>
        bool EnableCertificate { get; }

        /// <summary>
        /// 获取一个值，该值指示当前用户是否在线。
        /// </summary>
        bool IsOnline { get; }

        /// <summary>
        /// 获取一个值，该值指示当前用户是否已经被锁定。
        /// </summary>
        bool IsLocked { get; }

        /// <summary>
        /// 获取一个值，该值指示当前用户是否已经被禁用。
        /// </summary>
        bool IsDisabled { get; }
    }
}
