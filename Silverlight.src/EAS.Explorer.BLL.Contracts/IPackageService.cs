using System;
using EAS.Explorer.Entities;
namespace EAS.Explorer.BLL
{
    /// <summary>
    /// 模块包管理服务。
    /// </summary>
    public interface IPackageService
    {
        /// <summary>
        /// 删除程序包。
        /// </summary>
        /// <param name="package"></param>
        void DeletePackage(Package package);

        /// <summary>
        /// 读取程序包。
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        Package GetPackage(Guid guid);
    }
}
