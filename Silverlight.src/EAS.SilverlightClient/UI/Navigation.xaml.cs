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
using EAS.Explorer.Entities;
using EAS.Data.Linq;
using EAS.Data;
using EAS.Explorer;

namespace EAS.SilverlightClient.UI
{
    partial class Navigation : UserControl,INavigation
    {
        TreeItem rootItem = new TreeItem();
        IList<NavigateGroup> m_Groups { get; set; }
        IList<NavigateModule> m_Modules { get; set; }

        public Navigation()
        {
            InitializeComponent();
        }

        public void Initialize(object groupList, object moduleList)
        {
            this.m_Groups = groupList as IList<NavigateGroup>;
            this.m_Modules = moduleList as IList<NavigateModule>;

            this.Tree.Items.Clear();

            this.rootItem = new TreeItem();
            this.rootItem.Name = PlugContext.ApplicationName;
            this.rootItem.Icon = "/EAS.SilverlightClient;component/images/desktop16.png";

            this.InitializeTree(rootItem, null);

            List<TreeItem> items = new List<TreeItem>();
            items.Add(this.rootItem);
            this.Tree.ItemsSource = items;
        }

        void InitializeTree(TreeItem rootItem, NavigateGroup iGroup)
        {
            string ID = iGroup == null ? Guid.Empty.ToString().ToUpper() : iGroup.ID;
            IList<NavigateGroup> groupList = this.m_Groups.Where(p => p.ParentID == ID).ToList();

            foreach (NavigateGroup var in groupList) //下级组
            {
                if ((var.Attributes & 0x0010) == 0x0010) //display and windows NavigateGroup
                {
                    TreeItem subItem = new TreeItem();
                    subItem.Icon = "/EAS.SilverlightClient;component/images/group16.png";
                    subItem.Name = var.Name;
                    subItem.Tag = var;
                    this.InitializeTree(subItem, var);
                    rootItem.Items.Add(subItem);
                }
            }

            if (iGroup != null)  //功能节点
            {
                IList<NavigateModule> moduleList =  this.m_Modules.Where(p=>p.GroupID == ID).ToList();
                foreach (NavigateModule mv in moduleList)
                {
                    TreeItem item = new TreeItem();
                    item.Icon = "/EAS.SilverlightClient;component/images/module16.png";
                    item.Name = mv.Name;
                    item.Tag = mv;
                    rootItem.Items.Add(item);
                }
            }
        }

        private void TbItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            if (tb.Tag is NavigateGroup)
            {
                NavigateGroup group = tb.Tag as NavigateGroup;

                Guid mGuid = Guid.Empty;
                Guid.TryParse(group.WFModule, out mGuid);
                if (mGuid == Guid.Empty) return;

                EAS.Application.Instance.StartModule(mGuid);
            }
            else if (tb.Tag is NavigateModule)
            {
                try
                {
                    NavigateModule module = tb.Tag as NavigateModule;
                    System.Type T2 = EAS.Objects.ClassProvider.GetType(module.Assembly, module.Type);
                    object addIn = System.Activator.CreateInstance(T2);
                    EAS.Application.Instance.StartModule(addIn);
                }
                catch (System.Exception exc)
                {
                    MessageBox.Show("打开模块出错：" + exc.Message, "错误", MessageBoxButton.OK);
                }
            }
            else if (tb.Tag is Type)
            {
                try
                {
                    System.Type T = tb.Tag as Type;
                    object addIn = System.Activator.CreateInstance(T);
                    EAS.Application.Instance.StartModule(addIn);
                }
                catch (System.Exception exc)
                {
                    MessageBox.Show("打开模块出错：" + exc.Message, "错误", MessageBoxButton.OK);
                }
            }
        }
    }
}
