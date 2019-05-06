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
using System.Collections.Generic;
using EAS.Explorer.Entities;
using System.IO;
using System.Windows.Resources;
using System.Xml.Linq;
using EAS.Modularization;

namespace EAS.SilverlightClient.AddIn
{
    public partial class ModuleLoader : ChildWindow
    {
        #region Class/ModuleInfo

        public class ModuleInfo : INotifyPropertyChanged
        {
            bool m_Checked;
            string m_Name;
            Type m_Type;

            /// <summary>
            /// 是否已经选择。
            /// </summary>
            public bool Checked
            {
                get
                {
                    return this.m_Checked;
                }
                set
                {
                    this.m_Checked = value;
                    this.NotifyPropertyChanged("Checked");
                }
            }

            /// <summary>
            /// 登录ID/角色名称。
            /// </summary>
            public string Name
            {
                get
                {
                    return this.m_Name;
                }
                set
                {
                    this.m_Name = value;
                    this.NotifyPropertyChanged("Name");
                }
            }

            /// <summary>
            /// 类型。
            /// </summary>
            public Type Type
            {
                get
                {
                    return this.m_Type;
                }
                set
                {
                    this.m_Type = value;
                    this.NotifyPropertyChanged("Type");
                }
            }

            public override string ToString()
            {
                return this.Name;
            }

            /// <summary>
            /// 写成PropertyChanged事件通知。
            /// </summary>
            /// <param name="propertyName">属性名称。</param>
            protected void NotifyPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            #region INotifyPropertyChanged 成员

            /// <summary>
            /// 在更改属性值时发生。
            /// </summary>
            public event PropertyChangedEventHandler PropertyChanged;

            #endregion
        }

        #endregion

        IList<ModuleInfo> modules = null;

        public ModuleLoader()
        {
            InitializeComponent();
        }

        public string AssemblyName
        {
            get
            {
                return this.tbAssembly.Text;
            }
        }

        public string ModuleClassName
        {
            get
            {
                if (this.cbxModules.SelectedIndex < 0)
                    return string.Empty;

                return this.cbxModules.SelectedItem.ToString();
            }
        }

        void LoadXap(FileInfo File)
        {
            using (FileStream stream = File.OpenRead())
            {
                System.Windows.Resources.StreamResourceInfo Info = new System.Windows.Resources.StreamResourceInfo(stream, "application/binary");
                StreamResourceInfo resource = System.Windows.Application.GetResourceStream(Info, new Uri("AppManifest.xaml", UriKind.Relative));
                string resourceManifest = new StreamReader(resource.Stream).ReadToEnd();
                IEnumerable<XElement> assemblyParts = XDocument.Parse(resourceManifest).Root.Element("Deployment").Element("Deployment.Parts").Elements("AssemblyPart");

                foreach (XElement element in assemblyParts)
                {
                    // 取出 AssemblyPart 的 Source 指定的 dll
                    string source = element.Attribute("Source").Value;
                    string xName = source.Substring(0, source.Length - 4);

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
                System.Type[] types = assembly.GetTypes();

                foreach (System.Type type in types)
                {
                    ModuleAttribute ma = Attribute.GetCustomAttribute(type, typeof(ModuleAttribute)) as ModuleAttribute;
                    if (!Object.Equals(null, ma))
                    {
                        ModuleInfo module = new ModuleInfo();
                        module.Type = type;
                        module.Name = ma.Name;
                        this.modules.Add(module);
                    }
                }
            }
        }

        private void btnBrowser_Click(object sender, RoutedEventArgs e)
        {
            this.cbxModules.ItemsSource = null;
            this.modules = new List<ModuleInfo>();

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

            this.cbxModules.ItemsSource = modules;
            //this.btnOK.IsEnabled = modules.Count > 0;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.cbxModules.SelectedIndex > -1)
            {
                ModuleInfo mi = this.cbxModules.SelectedItem as ModuleInfo;
                object module = System.Activator.CreateInstance(mi.Type);
                if (module != null)
                {
                    EAS.Application.Instance.StartModule(module);
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void tbAssembly_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.btnOK.IsEnabled = this.tbAssembly.Text.Trim().Length > 0 && this.cbxModules.SelectedIndex >= 0;
        }

        private void cbxModules_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.btnOK.IsEnabled = this.tbAssembly.Text.Trim().Length > 0 && this.cbxModules.SelectedIndex >= 0;
        }

        //    private void buttonOK_Click(object sender, System.EventArgs e)
        //    {
        //        Cursor cur = this.Cursor;
        //        try
        //        {
        //            try
        //            {
        //                this.Cursor = Cursors.WaitCursor;

        //                object module = System.Reflection.Assembly.LoadFrom(this.AssemblyName).CreateInstance(this.ModuleClassName);
        //                if (module != null)
        //                {
        //                    EAS.Windows.Application.Instance.StartModule(module);
        //                }
        //            }
        //            finally
        //            {
        //                this.Cursor = cur;
        //            }
        //        }
        //        catch(Exception exc)
        //        {
        //            System.Windows.Forms.MessageBox.Show(this, "�޷����س��򼯡�" + this.AssemblyName + "���е�ģ�顰" + this.ModuleClassName + "������ϸ���쳣��Ϣ���£�\n\n" + exc, "ģ����ش���", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }
        //        this.Close();
        //    }
    }
}
