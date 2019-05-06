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

namespace EAS.SilverlightClient
{
    [ContentProperty("Items")]
    public class TreeItem: INotifyPropertyChanged
    {
        public TreeItem()
        {
            Items = new List<TreeItem>();
        }

        public string Name
        {
            get;
            set;
        }

        public string Icon
        {
            get;
            set;
        }

        public object Tag
        {
            get;
            set;
        }

        public List<TreeItem> Items
        {
            get;
            private set;
        }

        #region INotifyPropertyChanged 成员

        //事件委托
        public event PropertyChangedEventHandler PropertyChanged;

        //实现接口INotifyPropertyChanged定义函数
        private void OnPropertyChanged(string propertyName)
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
