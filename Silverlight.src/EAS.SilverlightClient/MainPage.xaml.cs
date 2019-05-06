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
using EAS.Controls;
using System.Reflection;
using EAS.Modularization;
using EAS.Data.Linq;
using EAS.Data;
using EAS.Data.ORM;
using EAS.Services;
using EAS.Explorer;
using EAS.Explorer.BLL;

namespace EAS.SilverlightClient.UI
{
    public partial class MainPage : UserControl
    {
        string LastModule = string.Empty;
        TreeItem rootItem = new TreeItem();
        object m_StartModule = null;
        Control m_Menu = null;

        public MainPage()
        {
            InitializeComponent();

            this.collapse.MouseLeftButtonUp += new MouseButtonEventHandler(collapse_MouseLeftButtonUp);
            this.InitializeModule();

            (Application.Instance as Application).OnStarted2(new System.EventArgs());
        }

        void item_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        void collapse_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement fe = sender as FrameworkElement;
            if (fe.Opacity == 1)
            {
                this.hideMenu.Begin();
            }
            else
            {
                this.showMenu.Begin();
            }
        }

        #region 脚本调用

        /// <summary>
        /// 
        /// </summary>
        internal void SwitchNavigation(bool show)
        {
            if (show)  //显示
            {
                if (this.collapse.Opacity != 1)
                    this.showMenu.Begin();
            }
            else
            {
                if (this.collapse.Opacity == 1)
                    this.hideMenu.Begin();
            }
        }

        #endregion

        #region 容器方法

        /// <summary>
        /// 初始化模块
        /// </summary>
        internal void InitializeModule()
        {
            #region Menu/Banner/Footer

            Control banner = PlugContext.Banner as Control;
            if (banner != null)
            {
                this.vbBanner.Height = banner.Height;
                banner.Margin = new Thickness(0);
                this.vbBanner.Children.Add(banner);
            }

            Control footer = PlugContext.Footer as Control;
            if (footer != null)
            {
                this.vbFooter.Height = footer.Height;
                footer.Margin = new Thickness(0);
                this.vbFooter.Children.Add(footer);
            }

            m_Menu = PlugContext.Navigation as Control;
            if (m_Menu != null)
            {
                m_Menu.Margin = new Thickness(0);
                this.vbMenu.Children.Add(m_Menu);
            }

            #endregion

            this.tabMain.Items.Clear();

            if (SLContext.Instance.Debug)
            {
                this.InitializeNavigationDebug();
            }
            else
            {
                this.InitializeNavigation();
                //System.Type T = EAS.Objects.ClassProvider.GetType("EAS.SilverlightClient.AddIn", "EAS.SilverlightClient.AddIn.AppSettingList");
                //this.LoadModule(System.Activator.CreateInstance(T));
            }

            this.m_StartModule = PlugContext.StartModule;
            this.LoadModule(this.m_StartModule);
        }

        /// <summary>
        /// 加载模块。
        /// </summary>
        internal void LoadModule(object addIn)
        {
            if (addIn is ChildWindow)
            {
                this.LoadModule(addIn as ChildWindow);
            }
            else if (addIn is UserControl)
            {
                this.LoadModule(addIn as UserControl);
            }

            this.LastModule = ModuleManager.GetModuleName(addIn);
        }

        /// <summary>
        /// 关闭当前模块。
        /// </summary>
        internal void CloseModule()
        {
            if (this.tabMain.SelectedItem!=null)
            {
                TabItemEx fc = this.tabMain.SelectedItem as TabItemEx;
                this.fc_Closed(fc, new EventArgs());
            }        
        }

        /// <summary>
        /// 关闭指定模块。
        /// </summary>
        internal void CloseModule(object module)
        {
            TabItemEx fc = this.FindAddInContainer(module) as TabItemEx;
            if (fc != null)
            {
                this.fc_Closed(fc, new EventArgs());
            }
        }

        #endregion

        void LoadModule(ChildWindow addIn)
        {
            ModuleManager.StartModule(addIn);
            addIn.Show();
        }

        void LoadModule(UserControl addIn)
        {
            TabItem doc = this.FindAddInContainer(addIn);
            if (doc == null)
            {
                doc = CreateAddInContainer(addIn);
                doc.Tag = addIn;
                doc.Content = addIn;
                this.tabMain.Items.Add(doc);
                this.tabMain.SelectedIndex = this.tabMain.Items.Count - 1;
                ModuleManager.StartModule(addIn);
            }
            else
            {
                doc.IsSelected = true;
                this.tabMain.SelectedIndex = doc.TabIndex;
            }
        }

        #region Mdi Shell Document

        TabItem CreateAddInContainer(object addIn)
        {
            string name = ModuleManager.GetModuleName(addIn);
            TabItemEx fc = this.CreateAddInContainer(name) as TabItemEx;
            fc.ExplorerControl = addIn as UserControl;
            if (fc.ExplorerControl != this.m_StartModule)
            {
                fc.Closed += new EventHandler(fc_Closed);
            }
            return fc;
        }

        void fc_Closed(object sender, EventArgs e)
        {
            TabItemEx fc = sender as TabItemEx;
            if (fc != null)
            {
                if (fc.ExplorerControl.GetType() != PlugContext.TStartModule)
                {
                    this.tabMain.Items.Remove(sender);
                }
            }
        }

        TabItem CreateAddInContainer(string name)
        {
            TabItemEx fc = new TabItemEx();
            fc.Header = name;
            fc.Content = name;
            return fc;
        }

        TabItem FindAddInContainer(object addIn)
        {
            foreach (TabItemEx tabPage in this.tabMain.Items)
            {
                if (tabPage.ExplorerControl != null)
                {
                    if (tabPage.ExplorerControl.GetType() == addIn.GetType())
                    {
                        return tabPage;
                    }
                }
            }

            return null;
        }

        #endregion

        #region 初始化导航/控件

        void InitializeNavigation()
        {
            NavigationProxy.Instance.BeginGetNavigation(SLContext.Account.LoginID).Completed +=
                (s1, e1) =>
                {
                    FuncTask<NavigationResult> task = s1 as FuncTask<NavigationResult>;
                    if (task.Error != null)
                    {
                        if (task.Error.InnerException != null)
                            MessageBox.Show(task.Error.InnerException.Message, "未能通过验证", MessageBoxButton.OK);
                        else
                            MessageBox.Show("获取系统导航信息时发生错误。\n\n" + task.Error.Message, "错误", MessageBoxButton.OK);
                    }
                    else
                    {
                        NavigationProxy.Instance.GroupList = task.TResult.Groups;
                        NavigationProxy.Instance.ModuleList = task.TResult.Modules;
                        (this.m_Menu as INavigation).Initialize(NavigationProxy.Instance.GroupList, NavigationProxy.Instance.ModuleList);
                    }
                };
        }

        #endregion        

        #region 导航/调试

        void InitializeNavigationDebug()
        {
            //=======================================================

            Assembly assembly = EAS.Objects.ClassProvider.GetAssembly(SLContext.Instance.Assembly);
            System.Type[] types = assembly.GetTypes();

            List<NavigateModule> moduleList = new List<NavigateModule>();
            List<NavigateGroup> groupList = new List<NavigateGroup>();

            #region 提取模块

            foreach (System.Type type in types)
            {
                AddInAttribute ma = Attribute.GetCustomAttribute(type, typeof(AddInAttribute)) as AddInAttribute;
                if (!Object.Equals(null, ma))
                {
                    NavigateModule dataEntity = new NavigateModule();
                    dataEntity.Guid = ma.Guid;
                    dataEntity.Name = ma.Name;
                    dataEntity.Description = ma.Description;
                    dataEntity.Assembly = MetaHelper.GetAssemblyString(type);
                    dataEntity.Type = MetaHelper.GetTypeString(type);
                    dataEntity.Version = MetaHelper.GetVersionString(type);
                    dataEntity.Developer = MetaHelper.GetDeveloperString(type);
                    moduleList.Add(dataEntity);
                }
            }

            foreach (NavigateModule item in moduleList)
            {
                if (item.GroupName == null)
                    item.GroupName = string.Empty;

                if (item.GroupName.Length == 0)
                    item.GroupName = "调试模块";

                NavigateGroup GX = groupList.Where(p => p.Name == item.GroupName).FirstOrDefault();

                if (GX != null)
                {
                    item.GroupID = GX.ID;
                }
                else
                {
                    GX = new NavigateGroup();
                    GX.ParentID = Guid.Empty.ToString().ToUpper();
                    GX.ID = Guid.NewGuid().ToString().ToUpper();
                    GX.Name = item.GroupName;
                    GX.Attributes = 0xffff;
                    groupList.Add(GX);
                    item.GroupID = GX.ID;
                }
            }

            #endregion

            (this.m_Menu as INavigation).Initialize(groupList, moduleList);
        }

        #endregion

        private void TbItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            if (tb.Tag is NavigateGroup)
            {
                NavigateGroup group = tb.Tag as NavigateGroup;

                DataEntityQuery<EAS.Explorer.Entities.Module> query = new DataEntityQuery<Explorer.Entities.Module>();
                var v = query.Where(p => p.Guid == group.WFModule);
                DataPortal<EAS.Explorer.Entities.Module> dp = new DataPortal<Explorer.Entities.Module>();
                dp.BeginExecuteSingleQuery(v).Completed +=
                    (s, e2) =>
                    {
                        QueryTask<EAS.Explorer.Entities.Module> task = s as QueryTask<EAS.Explorer.Entities.Module>;
                        if (task.DataEntity != null)
                        {
                            try
                            {
                                System.Type T3 = EAS.Objects.ClassProvider.GetType(task.DataEntity.Assembly, task.DataEntity.Type);
                                object addIn = System.Activator.CreateInstance(T3);
                                this.LoadModule(addIn);
                            }
                            catch (System.Exception exc)
                            {
                                MessageBox.Show("打开模块出错：" + exc.Message, "错误", MessageBoxButton.OK);
                            }
                        }
                    };                
            }
            else if (tb.Tag is NavigateModule)
            {
                NavigateModule module = tb.Tag as NavigateModule;

                try
                {
                    System.Type T2 = EAS.Objects.ClassProvider.GetType(module.Assembly, module.Type);
                    object addIn = System.Activator.CreateInstance(T2);
                    this.LoadModule(addIn);
                }
                catch (System.Exception exc)
                {
                    MessageBox.Show("打开模块出错：" + exc.Message, "错误", MessageBoxButton.OK);
                }
            }
            else if (tb.Tag is Type)
            {
                System.Type T = tb.Tag as Type;
                object addIn = System.Activator.CreateInstance(T);
                this.LoadModule(addIn);
            }
        }
    }
}
