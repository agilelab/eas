using System;
using System.Collections;
using System.ComponentModel;

using System.Linq;
using System.Collections.Generic;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EAS.Explorer.Entities;

using EAS.Data.Linq;
using EAS.Data;
using System.Text;

namespace EAS.SilverlightClient.AddIn
{
    partial class ModuleSelector : ChildWindow
	{
        IList<Module> selectList = null;
        IList<Module> displayList = null;

		public ModuleSelector()
		{
			InitializeComponent();
            this.selectList = new List<Module>();
		}

        private void tbKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.btnSearch_Click(this.btnSeach, new RoutedEventArgs());
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            this.tbSelected.Text = string.Empty;
            DataEntityQuery<Module> query = new DataEntityQuery<Module>();
            var v2 = from c in query
                     where c.Assembly.StartsWith(this.tbKey.Text) || c.Type.StartsWith(this.tbKey.Text)
                     select c;

            DataPortal<Module> dp = new DataPortal<Module>();
            dp.BeginExecuteQuery(v2).Completed +=
                (s, e2) =>
                {
                    QueryTask<Module> task = s as QueryTask<Module>;

                    if (task.Error != null)
                    {
                        MessageBox.Show("查询角色时出错：" + task.Error.Message, "错误", MessageBoxButton.OK);
                        return;
                    }
                    else
                    {
                        this.displayList = task.Entities.Where(p=>(p.Attributes & this.SelectMask) == this.SelectMask).ToList();

                        for (int i = this.displayList.Count - 1; i >= 0; i--)
                        {
                            bool bFlag = (this.displayList[i].Attributes & this.SelectMask) == this.SelectMask;
                            if (!bFlag)
                            {
                                this.displayList.RemoveAt(i);
                            }
                        }

                        if (this.WfAddIn)
                            this.displayList = this.displayList.Where(p => p.WorkFlowAddIn).ToList();

                        this.dataList.ItemsSource = null;
                        this.dataList.ItemsSource = this.displayList;
                    }
                };
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.selectList = this.displayList.Where(p => p.Checked).ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var item in this.selectList)
            {
                if (sb.Length > 0)
                    sb.Append(",");
                sb.Append(item.Name);
            }

            this.tbSelected.Text = sb.ToString();
        }

        /// <summary>
        /// 选择掩码。
        /// </summary>
        public int SelectMask
        {
            get;
            set;
        }

        /// <summary>
        /// 只搜索工作流插件。
        /// </summary>
        public bool WfAddIn
        {
            get;
            set;
        }

        /// <summary>
        /// 已经选中的模块。
        /// </summary>
        public IList<Module> SelectedModules
        {
            get
            {
                return this.selectList;
            }
        }       
	}
}
