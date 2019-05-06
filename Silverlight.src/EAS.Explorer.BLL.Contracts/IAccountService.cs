using System;
using EAS.Explorer.Entities;
using System.Collections.Generic;
namespace EAS.Explorer.BLL
{
    /// <summary>
    /// 账户服务。
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// 删除账户。
        /// </summary>
        /// <param name="account"></param>
        void Delete(Account account);

        /// <summary>
        /// 删除账户目录/组织机构。
        /// </summary>
        /// <param name="organID"></param>
        void DeleteOrganization(string organID);

        /// <summary>
        /// 更新账户。
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <param name="roleList"></param>
        /// <param name="aclList"></param>
        void UpdateAccount(Account account, string password, List<string> roleList, List<ACL> aclList);

        /// <summary>
        /// 修改密码。
        /// </summary>
        /// <param name="loginid"></param>
        /// <param name="password"></param>
        void UpdatePassword(string loginid, string password);

        /// <summary>
        /// 密码验证。
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        bool VerifyPassword(string loginid, string password);

        /// <summary>
        /// 查询指定角色的子账户。
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        List<Account> GetAccounts(string roleName);
    }
}
