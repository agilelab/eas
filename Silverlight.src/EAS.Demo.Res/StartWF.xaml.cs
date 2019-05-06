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

namespace EAS.Demo.Res
{
    [Module("621B669F-5568-4BFB-925D-F8199EAF0CBF", "启始页", "演示制作一个启始页模块")]
    public partial class StartWF : UserControl
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
