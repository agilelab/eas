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
using System.Collections.Generic;
using System.Windows.Markup;
using System.ComponentModel;

namespace EAS.SilverlightClient.AddIn
{
    [ContentProperty("Items")]
    public class TreeItem: INotifyPropertyChanged
    {
        string m_Name;
        string m_Icon;
        object m_Tag;
        List<TreeItem> m_Items;
        TreeItem m_Parent = null;

        public TreeItem()
        {
            m_Items = new List<TreeItem>();
        }

        public TreeItem Parent
        {
            get
            {
                return this.m_Parent;
            }
            set
            {
                this.m_Parent = value;
                OnPropertyChanged("Parent");
            }
        }

        public string Name
        {
            get
            {
                return this.m_Name;
            }
            set
            {
                this.m_Name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Icon
        {
            get
            {
                return this.m_Icon;
            }
            set
            {
                this.m_Icon = value;
                OnPropertyChanged("Icon");
            }
        }

        public object Tag
        {
            get
            {
                return this.m_Tag;
            }
            set
            {
                this.m_Tag = value;
                OnPropertyChanged("Tag");
            }
        }

        public List<TreeItem> Items
        {
            get
            {
                return this.m_Items;
            }
        }

        #region INotifyPropertyChanged 成员

        //事件委托
        public event PropertyChangedEventHandler PropertyChanged;

        //实现接口INotifyPropertyChanged定义函数
        void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
