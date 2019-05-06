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

namespace EAS.BPM.SilverlightUI
{
    public partial class WfSubmitWindow : ChildWindow
    {
        bool m_Result = false;

        public WfSubmitWindow()
        {
            InitializeComponent();
        }

        public bool Result
        {
            get
            {
                return this.m_Result;
            }
        }

        public string Comment
        {
            get
            {
                return this.tbComment.Text;
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.m_Result = true;
            this.DialogResult = true;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.m_Result = false;
            this.DialogResult = false;
        }
    }
}

