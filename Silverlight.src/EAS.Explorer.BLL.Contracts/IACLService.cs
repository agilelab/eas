using System;

namespace EAS.Explorer.BLL
{
    public interface IACLService
    {
        /// <summary>
        /// 清除权限。
        /// </summary>
        /// <param name="guid"></param>
        void Clear(Guid guid);

        /// <summary>
        /// 清除权限。
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="privileger"></param>
        void Clear(Guid guid, string privileger);

        /// <summary>
        /// 清除全新。
        /// </summary>
        /// <param name="privileger"></param>
        void Clear(string privileger);

        /// <summary>
        /// 验证权限。
        /// </summary>
        /// <param name="module"></param>
        /// <param name="loginID"></param>
        /// <returns></returns>
        int Demand(Guid module, string loginID);

        /// <summary>
        /// 验证权限。
        /// </summary>
        /// <param name="module"></param>
        /// <param name="loginID"></param>
        /// <param name="privileges"></param>
        /// <returns></returns>
        bool Demand(Guid module, string loginID, int privileges);
    }
}
