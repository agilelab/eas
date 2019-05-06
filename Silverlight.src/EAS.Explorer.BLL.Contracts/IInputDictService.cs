using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EAS.Explorer.BLL
{
    /// <summary>
    /// 输入字典服务。
    /// </summary>
    public interface IInputDictService
    {
        /// <summary>
        /// 获取输入字典元数据。
        /// </summary>
        /// <param name="inputID">输入字典ID。</param>
        /// <returns>字典元数据。</returns>
        string GetInputMetadata(Guid inputID);

        /// <summary>
        /// 获取输入字典元数据。
        /// </summary>
        /// <param name="inputID">输入字典ID。</param>
        /// <param name="updateTime">更新时间。</param>
        /// <returns>字典元数据，无更新返回空字符串。</returns>
        string GetInputMetadata(Guid inputID, DateTime updateTime);
    }
}
