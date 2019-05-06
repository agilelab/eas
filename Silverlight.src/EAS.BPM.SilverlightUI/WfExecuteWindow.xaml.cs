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
    public partial class WfExecuteWindow : ChildWindow
    {        
        public WfExecuteWindow()
        {
            InitializeComponent();
        }

        public string InstanceID
        {
            get
            {
                return this.wfExecuteItem.InstanceID;
            }
            set
            {
                this.wfExecuteItem.InstanceID = value;
            }
        }
    }
}

