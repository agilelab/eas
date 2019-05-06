using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAS.Explorer.Entities;
using EAS.Services;

namespace EAS.Explorer.BLL
{
    /// <summary>
    /// 报表服务。
    /// </summary>
    public interface IReportService
    {
        /// <summary>
        /// 读取报表元数据定义。
        /// </summary>
        /// <param name="name">报表名称。</param>
        /// <returns>报表元数据定义信息。</returns>
        string GetMetadata(string name);

        /// <summary>
        /// 读取报表元数据定义。
        /// </summary>
        /// <param name="name">报表名称。</param>
        /// <param name="updateTime">更新时间。</param>
        /// <returns>报表元数据定义信息。</returns>
        string GetMetadata(string name, DateTime updateTime);

        /// <summary>
        /// 读取报表元数据定义。
        /// </summary>
        /// <param name="reportID">报表ID。</param>
        /// <returns>报表元数据定义信息。</returns>
        string GetMetadata(Guid reportID);

        /// <summary>
        /// 读取报表元数据定义。
        /// </summary>
        /// <param name="reportID">报表ID。</param>
        /// <param name="updateTime">更新时间。</param>
        /// <returns>报表元数据定义信息。</returns>
        string GetMetadata(Guid reportID, DateTime updateTime);
    }
}
