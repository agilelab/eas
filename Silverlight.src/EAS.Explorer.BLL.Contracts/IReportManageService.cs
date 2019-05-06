using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAS.Explorer.Entities;
using EAS.Services;

namespace EAS.Explorer.BLL
{
    /// <summary>
    /// 报表管理服务。
    /// </summary>
    public interface IReportManageService
    {
        /// <summary>
        /// 更新报表
        /// </summary>
        /// <param name="m_Report">报表对象。</param>
        [ServiceMethod("2F3CF5EC-7E84-4EC9-8BA2-4DA0606F618D", "更新报表", "完成AgileEAS.NET SOA中间件平台RDL报表的配置与发布")]
        //[DemoLog]
        void UpdateReport(Report m_Report);

        /// <summary>
        /// 删除报表
        /// </summary>
        /// <param name="m_Report">报表对象。</param>
        [ServiceMethod("45889363-921B-4FC7-965E-88B41BE1BF41", "删除报表", "完成AgileEAS.NET SOA中间件平台RDL报表的删除操作")]
        void DeleteReport(Report m_Report);
    }
}
