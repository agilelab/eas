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
using EAS.Data.Linq;
using EAS.Data;

namespace EAS.BPM.SilverlightUI
{
    partial class WfDefineInput : ChildWindow
    {
        public WfDefineInput()
        {
            InitializeComponent();
        }

        public void Seach(string key = "")
        {
            this.btnOK.IsEnabled = false;
            
            DataEntityQuery<WFDefine> query = new DataEntityQuery<WFDefine>();
            var v  = from c in query
                     where (c.Name.StartsWith(key) || c.Module.StartsWith(key)) && c.IsPublish > 0 && c.IsEnable > 0
                     orderby c.FCTime descending
                     select new WFDefine
                    {
                        FlowID = c.FlowID,
                        Name = c.Name,
                        Version = c.Version,
                        Module =c.Module,
                        MType = c.MType,
                        SilverModule = c.SilverModule,
                        SilverMType = c.SilverMType,
                        Desctiption = c.Desctiption,
                        Creator = c.Creator,
                        FCTime = c.FCTime,
                        Modifier = c.Modifier,
                        LMTime = c.LMTime,
                        Publisher = c.Publisher,
                        PublishTime = c.PublishTime,
                        IsPublish = c.IsPublish,
                        StartTime = c.StartTime,
                        EndTime = c.EndTime,
                        InitialState = c.InitialState,
                        CompletedState = c.CompletedState
                    };

            DataPortal<WFDefine> dataPortal = new DataPortal<WFDefine>();
            dataPortal.BeginExecuteQuery(v).Completed += (s, e) =>
                {
                    QueryTask<WFDefine> task = s as QueryTask<WFDefine>;
                    if (task.Error != null)
                    {
                        MessageBox.Show("请求数据时发生错误：" + task.Error.Message, "错误", MessageBoxButton.OK);
                    }
                    else
                    {
                        this.dgList.ItemsSource = task.Entities;
                    }
                };
        }

        /// <summary>
        /// 选择对象。
        /// </summary>
        public WFDefine SelectedItem
        {
            get
            {
                return this.dgList.SelectedItem as WFDefine;
            }
        }

        private void tbKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.Seach(this.tbKey.Text);
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void dgList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.btnOK.IsEnabled = this.dgList.SelectedItem != null;
        }

        private void tbKey_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
