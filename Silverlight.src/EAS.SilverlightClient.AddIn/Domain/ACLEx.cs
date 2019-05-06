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
using EAS.Explorer.Entities;

namespace EAS.SilverlightClient.AddIn
{
    public class ACLEx:ACL
    {
        Module m_MInfo = null;

        public Module MInfo
        {
            get
            {
                return this.m_MInfo;
            }
            set
            {
                this.m_MInfo = value;
                this.NotifyPropertyChanged("MInfo");
            }
        }
    }
}
