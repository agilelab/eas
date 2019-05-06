using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using EAS.Modularization;

namespace EAS.SilverlightClient.UI
{
    [Module("704B3F05-0BBC-413A-B554-53733539FCC9", "启始页", "AgileEAS.NET平台WinForm/Wpf容器起始页模块")]
    partial class StartWF : UserControl
    {
        [ModuleStart]
        public void Start()
        {

        }

        public StartWF()
        {
            InitializeComponent();
        }
    }
}
