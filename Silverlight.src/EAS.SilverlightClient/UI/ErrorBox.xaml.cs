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
    partial class ErrorBox : ChildWindow
    {
        bool details;
        System.Exception error;

        public ErrorBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取或者设置当前正在处理的 EAS.Windows.Error 对象。
        /// </summary>
        internal System.Exception Error
        {
            get
            {
                return this.error;
            }
            set
            {
                this.error = value;
                if (value != null)
                {
                    while (true)
                    {
                        if (this.error.InnerException == null)
                            break;

                        this.error = this.error.InnerException;
                    }

                    this.tbError.Text = this.error.Message;
                }
            }
        }		

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            if (this.error == null)
                return;

            if (this.details)
            {
                this.tbError.Text = this.error.Message;
                this.btnShow.Content = "显示详细信息(&D)";
            }
            else
            {
                this.tbError.Text = ToDetail(this.error);
                this.btnShow.Content = "隐藏详细信息(&D)";
            }
            this.details = !this.details;
        }

        public string ToDetail(System.Exception exc)
        {
            return "发生时间：" + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒") + "\n" +
                "异常信息：" + exc.Message + "\n" +
                "堆栈跟踪信息：" + exc.StackTrace + "\n";
        }
    }
}

