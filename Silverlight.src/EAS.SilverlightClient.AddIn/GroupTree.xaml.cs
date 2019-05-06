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
using EAS.Modularization;
using EAS.Controls;
using EAS.Data.Linq;
using EAS.Explorer.Entities;
using EAS.Data;
using EAS.Data.ORM;
using EAS.Services;
using EAS.Explorer.BLL;
using EAS.Explorer;

namespace EAS.SilverlightClient.AddIn
{
    [Module("EBBEF4E4-923F-4857-9F92-76067FDC762A", "导航管理", "用于AgileEAS.NET平台Silverlight应用的组成导航管理。")]
    public partial class GroupTree : UserControl
    {
        [ModuleStart]
        public void Start()
        {
            this.OnRefresh(this, null);
        }

        TreeItem rootItem = new TreeItem();
        IList<NavigateGroup> groupList = null;
        IList<Module> moduleList = null;

        NavigateGroup selectGroup = null;

        public GroupTree()
        {
            InitializeComponent();
            this.groupList = new List<NavigateGroup>();
            this.moduleList = new List<Module>();
        }

        void LoadGroupTree()
        {
            DataEntityQuery<NavigateGroup> query = new DataEntityQuery<NavigateGroup>();
            var v = from c in query
                    select c;

            EAS.Controls.Window.ShowLoading("请求数据...");
            DataPortal<NavigateGroup> dp = new DataPortal<NavigateGroup>();
            dp.BeginExecuteQuery(v).Completed +=
                (s, e2) =>
                {
                    EAS.Controls.Window.HideLoading();

                    QueryTask<NavigateGroup> task = s as QueryTask<NavigateGroup>;
                    if (task.Error != null)
                    {                        
                        MessageBox.Show("请求数据时发生错误：" + task.Error.Message, "错误", MessageBoxButton.OK);
                        return;
                    }
                    else
                    {
                        this.groupList = task.Entities;
                        this.InitializeNavigationTree();
                    }
                };
        }

        void InitializeNavigationTree()
        {
            //this.Tree.Items.Clear();

            this.rootItem = new TreeItem();
            this.rootItem.Name = "AgileEAS.NET";
            this.rootItem.Icon = "images2/desktop16.png";

            TreeItem subItem = new TreeItem();
            subItem.Name = "Windows";
            subItem.Icon = "images2/folder_documents16.png";
            subItem.Tag = 0x0004;
            this.InitializeNavigationPublic(subItem, null, 0x0004);
            this.rootItem.Items.Add(subItem);

            subItem = new TreeItem();
            subItem.Name = "Web";
            subItem.Icon = "images2/folder_groups16.png";
            subItem.Tag = 0x0008;
            this.InitializeNavigationPublic(subItem, null, 0x0008);
            this.rootItem.Items.Add(subItem);            

            subItem = new TreeItem();
            subItem.Name = "Silverlight";
            subItem.Icon = "images2/smart_folder16.png";
            subItem.Tag = 0x0010;
            this.InitializeNavigationPublic(subItem, null, 0x0010);
            this.rootItem.Items.Add(subItem);            

            List<TreeItem> items = new List<TreeItem>();
            items.Add(this.rootItem);
            this.Tree.ItemsSource = items;
        }

        void InitializeNavigationPublic(TreeItem rootItem, NavigateGroup iGroup,int propX)
        {
            string ID = iGroup ==null? Guid.Empty.ToString().ToUpper() :iGroup.ID;

            IList<NavigateGroup> List = this.groupList.Where(p => p.ParentID == ID).ToList();

            foreach (NavigateGroup var in List) //下级组
            {
                if ((var.Attributes & propX) == propX) //display and windows group
                {
                    TreeItem subItem = new TreeItem();
                    subItem.Icon = "images2/program_group.png";
                    subItem.Name = var.Name;
                    subItem.Tag = var;
                    subItem.Parent = rootItem;
                    this.InitializeNavigationPublic(subItem, var, propX);
                    rootItem.Items.Add(subItem);
                }
            }
        }

        void LoadModuleList(NavigateGroup GM)
        {
            this.selectGroup = GM;

            EAS.Controls.Window.ShowLoading("请求成员数据...");
            QueryTask<Module> task = new QueryTask<Module>();
            IModuleService service = ServiceContainer.GetService<IModuleService>(task);
            service.GetModules(new Guid(GM.ID));
            task.Completed +=
                (s, e2) =>
                {
                    EAS.Controls.Window.HideLoading();
                    if (task.Error != null)
                    {
                        MessageBox.Show("读取成员模块时发生错误：" + task.Error.Message, "错误", MessageBoxButton.OK);
                        return;
                    }
                    else
                    {
                        this.moduleList = task.Entities;
                        this.dataList.ItemsSource = null;
                        this.dataList.ItemsSource = this.moduleList;
                    }
                };
        }

        private void OnRefresh(object sender, MouseButtonEventArgs e)
        {
            this.LoadGroupTree();
        }
        
        private void OnNew(object sender, MouseButtonEventArgs e)
        {
            if (this.Tree.SelectedItem != null)
            {
                TreeItem item = this.Tree.SelectedItem as TreeItem;
                GroupEditor editor = new GroupEditor();
                if (item.Tag is NavigateGroup)
                {
                    NavigateGroup gItem = item.Tag as NavigateGroup;
                    editor.ParentID = gItem.ID;
                    editor.Props = gItem.Attributes;
                    editor.Show();
                    editor.Closed +=
                        (s, e2) =>
                        {
                            bool? dr = editor.DialogResult;
                            if (dr.HasValue && (dr == true))
                            {
                                int count = item.Items.Where(p => p.Name == editor.Group.Name).Count();
                                if (count < 1)
                                {
                                    TreeItem subItem = new TreeItem();
                                    subItem.Parent = item;
                                    subItem.Name = editor.Group.Name;
                                    subItem.Icon = "images2/program_group.png";
                                    subItem.Tag = editor.Group;
                                    item.Items.Add(subItem);

                                    List<TreeItem> items = new List<TreeItem>();
                                    items.Add(this.rootItem);
                                    this.Tree.ItemsSource = null;
                                    this.Tree.ItemsSource = items;
                                }
                            }
                        };
                }
                else if (item.Tag is int)
                {
                    editor.Props = (int)item.Tag;
                    editor.Show();
                    editor.Closed +=
                        (s, e2) =>
                        {
                            bool? dr = editor.DialogResult;
                            if (dr.HasValue && (dr == true))
                            {
                                int count = item.Items.Where(p => p.Name == editor.Group.Name).Count();
                                if (count < 1)
                                {
                                    TreeItem subItem = new TreeItem();
                                    subItem.Parent = item;
                                    subItem.Name = editor.Group.Name;
                                    subItem.Icon = "images2/program_group.png";
                                    subItem.Tag = editor.Group;
                                    item.Items.Add(subItem);

                                    List<TreeItem> items = new List<TreeItem>();
                                    items.Add(this.rootItem);
                                    this.Tree.ItemsSource = null;
                                    this.Tree.ItemsSource = items;
                                }
                            }
                        };
                }                
            }
        }

        private void OnProperty(object sender, MouseButtonEventArgs e)
        {
            if (this.Tree.SelectedItem != null)
            {
                TreeItem item = this.Tree.SelectedItem as TreeItem;
                if (item.Tag is NavigateGroup)
                {
                    GroupEditor editor = new GroupEditor();
                    editor.DataEntity = item.Tag as NavigateGroup;
                    editor.Show();
                    editor.Closed +=
                        (s, e2) =>
                        {
                            bool? dr = editor.DialogResult;
                            if (dr.HasValue && (dr == true))
                            {
                                this.LoadModuleList(item.Tag as NavigateGroup);
                            }
                        };
                }
            }
        }

        private void OnDelete(object sender, MouseButtonEventArgs e)
        {
            if (this.Tree.SelectedItem != null)
            {
                TreeItem item = this.Tree.SelectedItem as TreeItem;
                if (item.Tag is NavigateGroup)
                {
                    if (MessageBox.Show("是否确定卸载所选择的导航?", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    {
                        NavigateGroup xItem = item.Tag as NavigateGroup;
                        EAS.Controls.Window.ShowLoading("正在删除导航...");

                        InvokeTask task = new InvokeTask();
                        IGroupService service = ServiceContainer.GetService<IGroupService>(task);
                        service.DeleteGroup(xItem);
                        task.Completed +=
                            (s, e2) =>
                            {
                                EAS.Controls.Window.HideLoading();
                                if (task.Error != null)
                                {
                                    MessageBox.Show("删除导航时发生错误：" + task.Error.Message, "错误", MessageBoxButton.OK);
                                }
                                else
                                {
                                    item.Parent.Items.Remove(item);
                                    List<TreeItem> items = new List<TreeItem>();
                                    items.Add(this.rootItem);
                                    this.Tree.ItemsSource = null;
                                    this.Tree.ItemsSource = items;
                                }
                            };
                    }
                }
            }
        }

        private void TbItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            if (tb.Tag is NavigateGroup)
            {
                this.LoadModuleList(tb.Tag as NavigateGroup);
            }
        }        

        private void OnClose(object sender, MouseButtonEventArgs e)
        {
            if (EAS.Application.Instance != null)
            {
                EAS.Application.Instance.CloseModule(this);
            }
        }

        private void miAdd_Click(object sender, RoutedEventArgs e)
        {
            if (this.Tree.SelectedItem != null)
            {
                TreeItem item = this.Tree.SelectedItem as TreeItem;
                GroupEditor editor = new GroupEditor();
                if (item.Tag is NavigateGroup)
                {
                    NavigateGroup iGroup = item.Tag as NavigateGroup;
                    int mask = 0x0004;
                    int mask2 = 0x0008;

                    ModuleSelector Selector = new ModuleSelector();

                    //int mask3 = (int)GoComType.SilverlightUI;
                    if ((iGroup.Attributes & mask) == mask)
                        Selector.SelectMask = (int)GoComType.WinUI;
                    else if ((iGroup.Attributes & mask2) == mask2)
                        Selector.SelectMask = (int)GoComType.WebUI;
                    else
                        Selector.SelectMask = (int)GoComType.SilverUI;
                    
                    Selector.Closed += new EventHandler(Selector_Closed);
                    Selector.Show();
                }
            }
        }

        void Selector_Closed(object sender, EventArgs e)
        {
            ModuleSelector Selector = sender as ModuleSelector;
            if (Selector.DialogResult ?? false)
            {
                IList<Module> selectList = Selector.SelectedModules;
                if (selectList.Count <= 0) return;

                List<string> sList = new List<string>();
                foreach (var item in selectList)
                {
                    int count = this.moduleList.Where(p => (p.Guid == item.Guid)).Count();
                    if (count < 1)
                    {
                        sList.Add(item.Guid.ToUpper());
                        this.moduleList.Add(item);
                    }
                }

                InvokeTask task = new InvokeTask();
                IGroupService service = ServiceContainer.GetService<IGroupService>(task);
                service.AddMember(this.selectGroup, sList);

                task.Completed +=
                    (s, e2) =>
                    {
                        if (task.Error != null)
                        {
                            MessageBox.Show("完成业务处理时发生错误：" + task.Error.Message, "错误", MessageBoxButton.OK);
                            return;
                        }                        
                    };

                this.dataList.ItemsSource = null;
                this.dataList.ItemsSource = this.moduleList;
            }
        }

        private void miDelete_Click(object sender, RoutedEventArgs e)
        {
            if (this.Tree.SelectedItem != null)
            {
                TreeItem xItem = this.Tree.SelectedItem as TreeItem;
                GroupEditor editor = new GroupEditor();
                if (xItem.Tag is NavigateGroup)
                {
                    IList<Module> selectList = this.moduleList.Where(p=>p.Checked).ToList();
                    if (selectList.Count <= 0) return;

                    List<string> sList = new List<string>();
                    foreach (var item in selectList)
                    {
                        sList.Add(item.Guid);
                    }

                    InvokeTask task = new InvokeTask();
                    IGroupService service = ServiceContainer.GetService<IGroupService>(task);
                    service.RemoveMember(this.selectGroup, sList);

                    task.Completed +=
                        (s, e2) =>
                        {
                            if (task.Error != null)
                            {
                                MessageBox.Show("完成业务处理时发生错误：" + task.Error.Message, "错误", MessageBoxButton.OK);
                                return;
                            }
                        };

                    foreach (var item in selectList)
                    {
                        this.moduleList.Remove(item);
                    }
                }
            }
        }
    }
}
