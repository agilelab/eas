using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EAS.Explorer
{
    /// <summary>
    /// 构件类型。
    /// </summary>
    public enum GoComType
    {
        /// <summary>
        /// Win UI构件。
        /// </summary>
        WinUI = 0x0001,

        /// <summary>
        /// Web UI构件。
        /// </summary>
        WebUI = 0x0002,

        /// <summary>
        /// Silverlight UI构件。
        /// </summary>
        SilverUI = 0x0004,        

        /// <summary>
        /// 业务逻辑构件。 
        /// </summary>
        Business = 0x0010,

        /// <summary>
        /// 功能/函数构件。 
        /// </summary>
        Function = 0x0020,

        /// <summary>
        /// 报表构件。 
        /// </summary>
        Report = 0x0100
    }

    /// <summary>
    /// 导航属性。
    /// </summary>
    public enum GroupType
    {
        /// <summary>
        /// Windows导航。
        /// </summary>
        Windows = 0x0004,

        /// <summary>
        /// Web导航。
        /// </summary>
        Web = 0x0008,

        /// <summary>
        /// Silverlight导航。
        /// </summary>
        Silverlight = 0x0010,

        /// <summary>
        /// 展开。 
        /// </summary>
        Expend = 0x0002,
    }
}
