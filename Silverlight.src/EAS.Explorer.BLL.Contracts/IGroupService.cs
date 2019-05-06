using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAS.Explorer.Entities;

namespace EAS.Explorer.BLL
{
    /// <summary>
    /// 分组服务。
    /// </summary>
    public interface IGroupService
    {
        /// <summary>
        /// 添加成员。
        /// </summary>
        /// <param name="group"></param>
        /// <param name="modules"></param>
        void AddMember(NavigateGroup group, List<string> modules);

        /// <summary>
        /// 移除成员。
        /// </summary>
        /// <param name="group"></param>
        /// <param name="modules"></param>
        void RemoveMember(NavigateGroup group, List<string> modules);

        /// <summary>
        /// 保存分组。
        /// </summary>
        /// <param name="group"></param>
        /// <param name="modules"></param>
        NavigateGroup UpdateGroup(NavigateGroup group, List<string> modules);

        /// <summary>
        /// 删除分组。
        /// </summary>
        /// <param name="group"></param>
        void DeleteGroup(NavigateGroup group);

        /// <summary>
        /// 读取分组。
        /// </summary>
        List<NavigateGroup> GetSubGroups(Guid ID);

        /// <summary>
        /// 读取分组。
        /// </summary>
        List<NavigateGroup> GetSubGroups(Guid ID, int attributes);
    }
}
