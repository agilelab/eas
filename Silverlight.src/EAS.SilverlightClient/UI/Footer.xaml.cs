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
    partial class Footer : UserControl
    {
        public Footer()
        {
            InitializeComponent();

            try
            {
                (EAS.Application.Instance as IApplication).Started += new EventHandler(Footer_Started);
                (EAS.Application.Instance as IApplication).Notify += new EventHandler<NotifyEventArgs>(Footer_Notify);
            }
            catch { }
        }

        void Footer_Notify(object sender, NotifyEventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                this.ShowMessage(e.Topic, e.Message.ToString());
            });
        }

        void Footer_Started(object sender, EventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(delegate
                { 
                    this.tbLoginID.Text = "当前登录:" + SLContext.Account.Name + "(" + SLContext.Account.LoginID + ")"; 
                });
        }

        /// <summary>
        /// 显示消息。
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="message"></param>
        void ShowMessage(string topic, string message)
        {
            //登录消息。
            if (string.Compare(topic,"login",StringComparison.CurrentCultureIgnoreCase) ==0)
            {
                this.tbLoginID.Text = message;
            }
            else if (string.Compare(topic, "登录", StringComparison.CurrentCultureIgnoreCase) == 0)
            {
                this.tbLoginID.Text = message;
            }
            else if (string.Compare(topic, "notify", StringComparison.CurrentCultureIgnoreCase) == 0)
            {
                this.tbMessage.Text = message;
            }
            else if (string.Compare(topic, "通知", StringComparison.CurrentCultureIgnoreCase) == 0)
            {
                this.tbMessage.Text = message;
            }
            else
            {
                this.tbMessage.Text = message;
            }
        }

    }
}
