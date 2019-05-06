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
    [Module("BB8D2CAC-75FB-41B4-98C6-1839A4E84BFE", "关于", "AgileEAS.NET平台Silverlight容器关于对话框")]
    partial class AboutWindow : ChildWindow
    {
        [ModuleStart]
        public void Start()
        {
            this.Show();
        }

        public AboutWindow()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void hyperlinkButton1_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void hyperlinkButton2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

