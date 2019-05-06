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
    partial class WfInstanceWindow : ChildWindow
    {
        object m_WfAddIn = null;
        Guid m_InstanceID = Guid.Empty;

        public WfInstanceWindow()
        {
            InitializeComponent();
        }

        public WfInstanceWindow(object wfAddIn, bool isEnabled, Guid instanceID)
        {
            this.InitializeComponent();

            this.m_InstanceID = instanceID;
            this.m_WfAddIn = wfAddIn;
            this.Title = string.Format("工作流实例:{0}-{1}", (instanceID != Guid.Empty ? instanceID.ToString() : "新实例"), ContextHelper.Account.LoginID);

            if (wfAddIn is Control)
            {
                Control wfControl = this.m_WfAddIn as Control;
                this.Viewer.Children.Add(wfControl);
            }            
        }

        /// <summary>
        /// 插件。
        /// </summary>
        public object WfAddIn
        {
            get
            {
                return this.m_WfAddIn;
            }
        }

        protected override void OnOpened()
        {
            if (this.m_InstanceID == Guid.Empty)
            {
                WfAddInHelper.Start(this.WfAddIn);
            }
            else
            {
                WfAddInHelper.LoadWorkflowInstance(this.WfAddIn, true, this.m_InstanceID);
            }

            if (EAS.Application.Instance != null)
            {
                EAS.Application.Instance.AddInClosing += new AddInEventHandler(OnAddInClosing);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            if (EAS.Application.Instance != null)
            {
                EAS.Application.Instance.AddInClosing -= new AddInEventHandler(OnAddInClosing);
            }
        }

        void OnAddInClosing(object sender, AddInEventArgs e)
        {
            if (e.AddIn == this.WfAddIn)
            {
                this.Close();
            }
        }        
    }
}

