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
using System.ComponentModel;
using EAS.Data.Linq;
using EAS.BPM.Entities;
using EAS.Data;

namespace EAS.BPM.SilverlightUI
{
    partial class WfExecuteItem : UserControl
    {
        string m_InstanceID = string.Empty;

        public WfExecuteItem()
        {
            InitializeComponent();
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string InstanceID
        {
            get
            {
                return m_InstanceID;
            }
            set
            {
                this.m_InstanceID = value;
                this.LoadWfExecuteList();
            }
        }

        void LoadWfExecuteList()
        {
            DataEntityQuery<WFExecute> query = new DataEntityQuery<WFExecute>();
            var v = from c in query
                    where c.InstanceID == this.m_InstanceID
                    orderby c.ExecuteTime
                    select c;

            DataPortal<WFExecute> dataPortal = new DataPortal<WFExecute>();
            dataPortal.BeginExecuteQuery(v).Completed += (s, e) =>
            {
                QueryTask<WFExecute> task = s as QueryTask<WFExecute>;
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
    }
}
