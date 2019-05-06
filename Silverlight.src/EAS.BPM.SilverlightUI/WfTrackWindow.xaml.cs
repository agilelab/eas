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
using EAS.BPM.Entities;
using System.IO;
using System.Windows.Media.Imaging;
using EAS.Data;

namespace EAS.BPM.SilverlightUI
{
    partial class WfTrackWindow : ChildWindow
    {
        WFDefine m_WFDefine = new WFDefine();
        WFInstance m_WFInstance = new WFInstance();

        public WfTrackWindow()
        {
            InitializeComponent();
        }

        string InstanceID
        {
            get
            {
                return this.wfExecuteItem.InstanceID;
            }
            set
            {
                this.wfExecuteItem.InstanceID = value;
                this.LoadWFDefine();
            }
        }

        public WFInstance WFInstance
        {
            get
            {
                return this.m_WFInstance;
            }
            set
            {
                this.m_WFInstance = value;
                this.wfExecuteItem.InstanceID = this.m_WFInstance.ID;
                this.LoadWFDefine();
            }
        }

        void LoadWFDefine()
        {
            //this.m_WFInstance.ID = this.InstanceID;
            //this.m_WFInstance.Read();

            this.m_WFDefine.FlowID = this.m_WFInstance.FlowID;

            DataPortal<WFDefine> dataPortal = new DataPortal<WFDefine>();
            dataPortal.BeginRead(this.m_WFDefine).Completed += (s, e) =>
                {
                    QueryTask<WFDefine> task = s as QueryTask<WFDefine>;
                    if (task.Error != null)
                    {
                        MessageBox.Show("请求数据时发生错误：" + task.Error.Message, "错误", MessageBoxButton.OK);
                    }
                    else
                    {
                        this.m_WFDefine = task.DataEntity;

                        using (Stream stream = new MemoryStream(this.m_WFDefine.Photo))
                        {
                            BitmapImage bitmapImage = new BitmapImage();
                            try
                            {
                                bitmapImage.SetSource(stream);
                                this.Wf.Source = bitmapImage;
                            }
                            catch { }
                        }
                    }
                };
        }
    }
}

