using System;
using EAS.Explorer.Entities;
using System.Collections.Generic;
namespace EAS.Explorer.BLL
{
    /// <summary>
    /// 角色管理服务。
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// 删除角色。
        /// </summary>
        /// <param name="role"></param>
        void DeleteRole(Role role);

        /// <summary>
        /// 更新角色。
        /// </summary>
        /// <param name="role"></param>
        /// <param name="accounts"></param>
        /// <param name="acls"></param>
        void UpdateRole(Role role, List<string> accounts, List<ACL> acls);

        /// <summary>
        /// 查询指定账号的所属角色。
        /// </summary>
        /// <param name="LoginID"></param>
        /// <returns></returns>
        List<Role> GetRoles(string LoginID);
    }
}
