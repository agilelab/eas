using System;
using EAS.Security;
using System.ComponentModel;

namespace EAS.SilverlightClient.AddIn
{
	/// <summary>
	/// 已经选择的帐户或者角色。
	/// </summary>
    public class SelectedPrivileger : INotifyPropertyChanged
	{
        bool m_Checked;
        string m_Name;
        int m_Value;
        PrivilegerType m_Type = PrivilegerType.Role;

        /// <summary>
        /// 是否已经选择。
        /// </summary>
        public bool Checked
        {
            get
            {
                return this.m_Checked;
            }
            set
            {
                this.m_Checked = value;
                this.NotifyPropertyChanged("Checked");
            }
        }

		/// <summary>
		/// 登录ID/角色名称。
		/// </summary>
        public string Name
        {
            get
            {
                return this.m_Name;
            }
            set
            {
                this.m_Name = value;
                this.NotifyPropertyChanged("Name");
            }
        }
		/// <summary>
		/// 帐户Or角色。
		/// </summary>
        public PrivilegerType Type
        {
            get
            {
                return this.m_Type;
            }
            set
            {
                this.m_Type = value;
                this.NotifyPropertyChanged("Type");
            }
        }

		/// <summary>
		/// 权限。
		/// </summary>
        public int Permissions
        {
            get
            {
                return this.m_Value;
            }
            set
            {
                this.m_Value = value;
                this.NotifyPropertyChanged("Permissions");
            }
        }

        /// <summary>
        /// 账号或者角色对象。
        /// </summary>
        public object Tag { get; set; }

		public void AppendPermissions(int permissions)
		{
            this.Permissions |= permissions;
		}

		public override string ToString()
		{
            return this.Name;
		}

        /// <summary>
        /// 写成PropertyChanged事件通知。
        /// </summary>
        /// <param name="propertyName">属性名称。</param>
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region INotifyPropertyChanged 成员

        /// <summary>
        /// 在更改属性值时发生。
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
	}
}
