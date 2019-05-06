using System;
using System.Collections.Generic;
using System.Text;

namespace fyiReporting.RdlDesign
{
    /// <summary>
    /// 打印纸张结构，以公制计算。
    /// </summary>
    struct PrintPage
    {
        public PrintPage(string name,decimal width,decimal height)
        {
            this.Name = name;
            this.Width = width;
            this.Height = height;
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name;

        /// <summary>
        /// 宽度
        /// </summary>
        public decimal Width;

        /// <summary>
        /// 高度
        /// </summary>
        public decimal Height;

        /// <summary>
        /// 宽度
        /// </summary>
        public string WidthString
        {
            get
            {
                return this.Width.ToString() + "mm";
            }
        }

        /// <summary>
        /// 高度
        /// </summary>
        public string HeightString
        {
            get
            {
                return this.Height.ToString() + "mm";
            }
        }
    }
}
