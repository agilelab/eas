using System;
using System.Collections;
using System.ComponentModel;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Resources;
using System.Collections.Generic;
using System.Xml.Linq;
using EAS.Modularization;
using EAS.Explorer.Entities;
using System.Linq;
using EAS.Explorer.BLL;
using EAS.Services;
using EAS.Explorer;
using EAS.Workflow;

namespace EAS.SilverlightClient.AddIn
{
    partial class ModuleInstaller : ChildWindow
	{
        IList<Module> modules = null;

		public ModuleInstaller()
		{
			InitializeComponent();
		}

        void LoadXap(FileInfo File)
        {
            using (FileStream stream = File.OpenRead())
            {                
                System.Windows.Resources.StreamResourceInfo Info = new System.Windows.Resources.StreamResourceInfo(stream, "application/binary");
                StreamResourceInfo resource = System.Windows.Application.GetResourceStream(Info, new Uri("AppManifest.xaml", UriKind.Relative));
                string resourceManifest = new StreamReader(resource.Stream).ReadToEnd();

                IEnumerable<XElement> assemblyParts = XDocument.Parse(resourceManifest).Root.Elements().Elements();

                foreach (XElement element in assemblyParts)
                {
                    XAttribute xa = element.Attribute("Source");
                    if (xa == null) continue;

                    // 取出 AssemblyPart 的 Source 指定的 dll
                    string source = xa.Value;
                    //string source = xa.Value.Substring(0, xa.Value.Length - 4);

                    AssemblyPart assemblyPart = new AssemblyPart();
                    StreamResourceInfo streamInfo = System.Windows.Application.GetResourceStream(Info, new Uri(source, UriKind.Relative));

                    System.Reflection.Assembly assembly = null;
                    try
                    {
                        assembly = assemblyPart.Load(streamInfo.Stream);
                    }
                    catch { }

                    if (assembly != null)
                        LoadAssembly(assembly);
                }
            }
        }

        void LoadAssembly(System.Reflection.Assembly assembly)
        {
            if (assembly != null)
            {
                System.Type  [] types = assembly.GetTypes();

                foreach (System.Type T in types) 
                {
                    ModuleAttribute ma = Attribute.GetCustomAttribute(T, typeof(ModuleAttribute)) as ModuleAttribute;
                    if (!Object.Equals(null, ma))
                    {
                        Module module = new Module();
                        module.Assembly = MetaHelper.GetAssemblyString(T);
                        module.Type = MetaHelper.GetTypeString(T);
                        module.Developer = MetaHelper.GetDeveloperString(T);
                        module.Version = MetaHelper.GetVersionString(T);
                        module.Name = ma.Name;
                        module.Guid = ma.Guid.ToString().ToUpper();
                        module.Description = ma.Description;
                        module.Attributes = (int)GoComType.SilverUI;

                        WorkflowAddInAttribute wfa = Attribute.GetCustomAttribute(T, typeof(WorkflowAddInAttribute)) as WorkflowAddInAttribute;
                        if (!Object.Equals(null, wfa))
                        {
                            module.Attributes |= 0x1000;
                        }
                        this.modules.Add(module);
                    }
                }                
            }
        }

        private void btnBrowser_Click(object sender, RoutedEventArgs e)
        {
            this.moduleList.ItemsSource = null;
            this.modules = new List<Module>();

            OpenFileDialog openfile = new OpenFileDialog();
            if (openfile.ShowDialog() == true)
            {
                string fileName = openfile.File.Name;

                if (fileName.EndsWith(".dll", StringComparison.CurrentCultureIgnoreCase))
                {
                    using (FileStream stream = openfile.File.OpenRead())
                    {
                        AssemblyPart assemblyPart = new AssemblyPart();
                        System.Reflection.Assembly assembly = null;
                        try
                        {
                            assembly = assemblyPart.Load(stream);
                        }
                        catch { }

                        if (assembly != null)
                            LoadAssembly(assembly);
                    }
                }
                else if (fileName.EndsWith(".xap", StringComparison.CurrentCultureIgnoreCase))
                {
                    LoadXap(openfile.File);
                }
            }

            this.labelCount.Content = string.Format("已加载 {0} 个要安装的模块。", this.modules.Count);

            this.moduleList.ItemsSource = modules;
            this.btnOK.IsEnabled = modules.Count > 0;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Cursor c = this.Cursor;
            this.Cursor = Cursors.Wait;
            this.btnOK.IsEnabled = false;

            IList<Module> installers = this.modules.Where(p => p.Checked).ToList();

            if (installers.Count == 0)
            {
                installers = this.modules;
            }

            int index = 0;

            foreach (Module item in installers)
            {
                InvokeTask task = new InvokeTask();
                IModuleService service = ServiceContainer.GetService<IModuleService>(task);
                service.InstallModule(item);
                task.Completed +=
                    (s, e2) =>
                    {
                        if (task.Error != null)
                        {
                            this.Cursor = c;
                            this.btnOK.IsEnabled = true;
                            MessageBox.Show("请求数据时发生错误：" + task.Error.Message, "错误", MessageBoxButton.OK);
                            return;
                        }
                        else
                        {
                            index++;
                            if (index == installers.Count)
                            {
                                this.Cursor = c;
                                this.btnOK.IsEnabled = true;
                                this.DialogResult = true;
                                this.Close();
                            }
                        }
                    };
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void moduleList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
            if (e.AddedItems.Count > 0)
            {
                Module module = e.AddedItems[0] as Module;
                this.tbGuid.Text = module.Guid;
                this.tbName.Text = module.Name;
                this.tbVersion.Text = module.Version;
                this.tbClass.Text = module.Type;
                this.tbAssembly.Text = module.Assembly;
                this.tbDescription.Text = module.Description;
                this.tbDeveloper.Text = module.Developer;
            }
            else
            {
                this.tbGuid.Text = string.Empty;
                this.tbName.Text = string.Empty;
                this.tbVersion.Text = string.Empty;
                this.tbClass.Text = string.Empty;
                this.tbAssembly.Text = string.Empty;
                this.tbDescription.Text = string.Empty;
                this.tbDeveloper.Text = string.Empty;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            IList<Module> installers = this.modules.Where(p => p.Checked).ToList();
            int count = installers.Count > 0 ? installers.Count : this.modules.Count;
            this.labelCount.Content = string.Format("已加载 {0} 个要安装的模块。", count);
        }
	}
}
