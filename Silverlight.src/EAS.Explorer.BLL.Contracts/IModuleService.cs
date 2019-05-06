using System;
using EAS.Explorer.Entities;
using System.Collections.Generic;
using EAS.Services;
namespace EAS.Explorer.BLL
{
    /// <summary>
    /// 模块管理服务。
    /// </summary>
    public interface IModuleService
    {
        /// <summary>
        /// 安装模块。
        /// </summary>
        /// <param name="module"></param>
        [ServiceMethod("6F9D0831-DB0F-4062-B326-C7D302F4D93C", "构件安装", "向AgileEAS.NET平台注册应用构件")]
        void InstallModule(Module module);

        /// <summary>
        /// 卸载模块。
        /// </summary>
        /// <param name="module"></param>
        [ServiceMethod("8611AFB7-8804-4ABD-8970-FB15EA17A39F", "构件卸载", "完成AgileEAS.NET平台中构件卸载")]
        void UnstallModule(Module module);

        /// <summary>
        /// 更新模块
        /// </summary>
        /// <param name="?"></param>
        /// <param name="acls"></param>
        [ServiceMethod("6B414806-5A3F-47A5-8B9E-A51B0433A031", "构件更新", "完成AgileEAS.NET平台中构件更新、权限定义等")]
        void UpdateModule(Module module, List<ACL> acls);

        /// <summary>
        /// 读取指定分组的模块。
        /// </summary>
        List<Module> GetModules(Guid groupID);
    }
}
