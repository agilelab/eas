using System;
using System.Collections.Generic;
using System.Text;

namespace EAS.Data.Adapter
{
    /// <summary>
    /// 适配器。
    /// </summary>
    public interface IAdapter
    {
        /// <summary>
        /// 连接到控件容器。
        /// </summary>
        void Connect(DataUIMapper dataUIMapper, object controlsContainer);

        /// <summary>
        /// 查找指定控件。
        /// </summary>
        object FindControl(string controlId);

        /// <summary>
        /// 更新对象属性到UI显示。
        /// </summary>
        void UpdateUI(DataUIMapper dm);

        /// <summary>
        /// 更新UII显示到对象属性。
        /// </summary>
        void UpdateObject(DataUIMapper dm); 
    }

    public abstract class BaseAdapter : IAdapter
    {
        DataUIMapper _dm;

        /// <summary>
        /// DataUIMapper组件。
        /// </summary>
        public DataUIMapper DataUIMapper
        {
            get
            {
                return this._dm;
            }
        }

        #region IAdapter 成员

        public virtual void Connect(DataUIMapper dataUIMapper, object controlsContainer)
        {
            this._dm = dataUIMapper;
            this._dm.Adapter = this;
        }

        public abstract object FindControl(string controlId);

        public abstract void UpdateUI(DataUIMapper dm);

        public abstract void UpdateObject(DataUIMapper dm);

        #endregion
    }
}
