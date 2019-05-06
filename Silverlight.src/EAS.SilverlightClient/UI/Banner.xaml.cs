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

namespace EAS.SilverlightClient.UI
{
    partial class Banner : UserControl
    {
        public Banner()
        {
            InitializeComponent();
        }

        private void btnCPassword_Click(object sender, RoutedEventArgs e)
        {
            (Application.Instance as IApplication).ChangePassword();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            (Application.Instance as IApplication).Logout();
        }
    }
}
