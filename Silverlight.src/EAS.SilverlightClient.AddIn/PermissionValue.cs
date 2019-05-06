using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.Generic;
using EAS.Security;

namespace EAS.SilverlightClient.AddIn
{
    public class PermissionValue : INotifyPropertyChanged
    {
        bool m_Checked;
        string m_Name;
        int m_Value;

        public static IList<PermissionValue> GetPermissionList()
        {
            List<PermissionValue> pvs = new List<PermissionValue>();
            pvs.Add(new PermissionValue{Name="执行权限",Value = (int)Privileges.Execute});
            pvs.Add(new PermissionValue{Name="执行(扩展)权限",Value = (int)Privileges.ExecuteEx});
            pvs.Add(new PermissionValue{Name="配置权限",Value = (int)Privileges.Config});
            pvs.Add(new PermissionValue{Name="卸载权限",Value = (int)Privileges.Delete});

            return pvs;
        }

        /// <summary>
        /// 当前对象是否选中。
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
        /// 中文/名称。
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
        /// 权限值。
        /// </summary>
        public int Value
        {
            get
            {
                return this.m_Value;
            }
            set
            {
                this.m_Value = value;
                this.NotifyPropertyChanged("Value");
            }
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
