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
using System.Windows.Media.Imaging;

namespace EAS.BPM.SilverlightUI
{
    partial class MsgBoxWindow : ChildWindow
    {
        #region enum
        public enum MsgBtns
        {
            OK = 0,
            Cancel = 1,
            OKCancel = 2,
            YesNo = 3,
            YesNoCancel = 4,
            OKCancelApply = 5,
            RetryCancel = 6,
            AbortRetryIgnore = 7
        }

        public enum MsgIcon
        {
            Information = 0,
            StopSign = 1,
            Exclamation = 2,
            Question = 3,
            None = 4
        }

        public enum MsgResult
        {
            OK = 0,
            Cancel = 1,
            Yes = 2,
            No = 3,
            Apply = 4,
            Retry = 5,
            Abort = 6,
            Ignore = 7,
            Close = 8
        }
        #endregion

        #region delegate MsgBoxCloseCallBack
        public delegate void MsgBoxCloseCallBack(object sender, MsgBoxCloseCallBackArgs e);

        public class MsgBoxCloseCallBackArgs : EventArgs
        {
            public MsgBoxCloseCallBackArgs()
                : base()
            {
            }

            public MsgResult Result { get; set; }
        }
        #endregion

        #region event
        void MsgBoxWindow_Closed(object sender, EventArgs e)
        {
            if (_CallBack != null)
            {
                _CallBack(this, new MsgBoxCloseCallBackArgs() { Result = this._Result });
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            switch (btn.Name)
            {
                case "OK":
                    this._Result = MsgResult.OK;
                    break;
                case "Cancel":
                    this._Result = MsgResult.Cancel;
                    break;
                case "Yes":
                    this._Result = MsgResult.Yes;
                    break;
                case "No":
                    this._Result = MsgResult.No;
                    break;
                case "Apply":
                    this._Result = MsgResult.Apply;
                    break;
                case "Retry":
                    this._Result = MsgResult.Apply;
                    break;
                case "Abort":
                    this._Result = MsgResult.Apply;
                    break;
                case "Ignore":
                    this._Result = MsgResult.Apply;
                    break;
                default:
                    this._Result = MsgResult.Close;
                    break;
            }
            base.Close();
        }
        #endregion

        #region private property
        private MsgResult _Result = MsgResult.Close;
        private MsgBoxCloseCallBack _CallBack;
        #endregion

        public MsgBoxWindow()
        {
            InitializeComponent();
            this.Closed += new EventHandler(MsgBoxWindow_Closed);
        }

        #region show

        public void Show(string title, string msg, MsgBoxCloseCallBack callBack)
        {
            Show(title, msg, MsgIcon.Information, MsgBtns.OK, callBack);
        }

        public void Show(string title, string msg, MsgIcon icon, MsgBoxCloseCallBack callBack)
        {
            Show(title, msg, icon, MsgBtns.OK, callBack);
        }

        public void Show(string title, string msg, MsgIcon icon, MsgBtns btns, MsgBoxCloseCallBack callBack)
        {
            if (callBack != null)
            {
                _CallBack = callBack;
            }

            this.msgBlock.Text = msg;
            this.Title = title;

            switch (icon)
            {
                case MsgIcon.Information:
                    this.imgIcon.Source = LoadImage("Images/Message.png");
                    break;
                case MsgIcon.StopSign:
                    this.imgIcon.Source = LoadImage("Images/StopSign.png");
                    break;
                case MsgIcon.Exclamation:
                    this.imgIcon.Source = LoadImage("Images/Exclamation.png");
                    break;
                case MsgIcon.Question:
                    this.imgIcon.Source = LoadImage("Images/Question.png");
                    break;
                case MsgIcon.None:
                    break;
                default:
                    break;
            }

            switch (btns)
            {
                case MsgBtns.OK:
                    CreateButtons(new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("OK", "确定") });
                    break;
                case MsgBtns.Cancel:
                    CreateButtons(new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("Cancel", "取消") });
                    break;
                case MsgBtns.OKCancel:
                    CreateButtons(new KeyValuePair<string, string>[] 
                    { new KeyValuePair<string, string>("OK", "确定"),
                    new KeyValuePair<string, string>("Cancel", "取消")});
                    break;
                case MsgBtns.YesNo:
                    CreateButtons(new KeyValuePair<string, string>[] 
                    { new KeyValuePair<string, string>("Yes", "是"),
                    new KeyValuePair<string, string>("No", "否")});
                    break;
                case MsgBtns.YesNoCancel:
                    CreateButtons(new KeyValuePair<string, string>[] 
                    { new KeyValuePair<string, string>("Yes", "是"),
                    new KeyValuePair<string, string>("No", "否"),
                    new KeyValuePair<string, string>("Cancel", "取消")});
                    break;
                case MsgBtns.OKCancelApply:
                    CreateButtons(new KeyValuePair<string, string>[] 
                    { new KeyValuePair<string, string>("OK", "确定"),
                    new KeyValuePair<string, string>("Cancel", "取消"),
                    new KeyValuePair<string, string>("Apply", "应用")});
                    break;
                case MsgBtns.RetryCancel:
                    CreateButtons(new KeyValuePair<string, string>[] 
                    { new KeyValuePair<string, string>("Retry", "重试"),
                    new KeyValuePair<string, string>("Cancel", "取消")});
                    break;
                case MsgBtns.AbortRetryIgnore:
                    CreateButtons(new KeyValuePair<string, string>[] 
                    { new KeyValuePair<string, string>("Abort", "取消"),
                    new KeyValuePair<string, string>("Retry", "重试"),
                    new KeyValuePair<string, string>("Ignore", "忽略")});
                    break;
            }
            base.Show();
        } 
        #endregion

        #region private hepler
        
        private void CreateButtons(KeyValuePair<string, string>[] btns)
        {
            for (int i = 0; i < btns.Count(); i++)
            {
                KeyValuePair<string, string> item = btns[i];
                Button btn = new Button()
                {
                    Name = item.Key,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Margin = new Thickness(0, 12, 79 * (btns.Count() - i - 1), 0),
                    Content = item.Value,
                    Width = 75,
                    Height = 25
                };
                btn.Click += new RoutedEventHandler(Button_Click);
                LayoutRoot.Children.Add(btn);
                Grid.SetRow(btn, 1);
                Grid.SetColumnSpan(btn, 2);
            }
        }

        private BitmapImage LoadImage(string url)
        {
            BitmapImage image = null;
            UriKind kind = UriKind.Relative;
            if (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
            {
                kind = UriKind.Absolute;
            }
            var streamInfo = System.Windows.Application.GetResourceStream(new Uri(string.Format("/EAS.BPM.SilverlightUI;component/{0}", url), kind));
            if (streamInfo != null)
            {
                image = new BitmapImage();
                image.SetSource(streamInfo.Stream);
            }
            else
            {
                image = new BitmapImage(new Uri(url, kind));
            }
            return image;
        }

        #endregion
    }
}

