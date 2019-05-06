using System;
using System.IO;
using EAS.Objects.Lifecycle;
using EAS.Explorer.Entities;
using System.Reflection;
using EAS.Objects;
using EAS.Modularization;
using EAS.Explorer.BLL;
using EAS.Services;
using EAS.Sessions;
using EAS.Security;
using System.Windows;

namespace EAS.SilverlightClient
{	
    /// <summary>
    /// 模块管理器。
    /// </summary>
    class ModuleManager
    {
        /// <summary>
        /// 加载模块。
        /// </summary>
        /// <param name="Guid"></param>
        /// <returns></returns>
        internal static object LoadModule(System.Guid Guid)
        {
            NavigateModule GModule = null;

            if (!SLContext.Instance.Adminstrators)
            {
                foreach (var item in NavigationProxy.Instance.ModuleList)
                {
                    Guid Guid2 = new Guid(item.Guid);
                    if (Guid2 == Guid)
                    {
                        GModule = item;
                        break;
                    }
                }
            }

            if (GModule == null)
                return null;

            object module = ClassProvider.GetObjectInstance(GModule.Assembly, GModule.Type);

            if (module == null)
            {
                throw new System.Exception("无法加载模块“" + GModule.Name + "”，请通知您的系统管理人员。");
            }

            return module;
        }

        internal static object LoadModule(string name, string assemmby, string type)
        {
            object module = ClassProvider.GetObjectInstance(assemmby, type);

            if (module == null)
            {
                throw new System.Exception("无法加载“" + name + "”，请通知您的系统管理人员。");
            }

            return module;
        }

        internal static void RunModule(object module)
        {
            SLContext.Instance.Shell.LoadModule(module);
        }

        /// <summary>
        /// 关闭当前模块。
        /// </summary>
        /// <param name="shell"></param>
        internal static void CloseModule()
        {
            SLContext.Instance.Shell.CloseModule();
        }

        public static string GetModuleName(object module)
        {
            AddInAttribute ma = Attribute.GetCustomAttribute(module.GetType(), typeof(AddInAttribute)) as AddInAttribute;
            if (!Object.Equals(null, ma))
            {
                return ma.Name;
            }
            else
                return string.Empty;
        }

        public static string GetModuleDescription(object module)
        {
            AddInAttribute ma = Attribute.GetCustomAttribute(module.GetType(), typeof(AddInAttribute)) as AddInAttribute;
            if (!Object.Equals(null, ma))
            {
                return ma.Description;
            }
            else
                return string.Empty;
        }

        public static Guid GetModuleGuid(object module)
        {
            AddInAttribute ma = Attribute.GetCustomAttribute(module.GetType(), typeof(AddInAttribute)) as AddInAttribute;
            if (!Object.Equals(null, ma))
            {
                return new Guid(ma.Guid);
            }
            else
                return Guid.Empty;
        }

        internal static MethodInfo GetRunMethod(object module)
        {
            MethodInfo[] mis = module.GetType().GetMethods();

            foreach (MethodInfo mi in mis)
            {
                ModuleStartAttribute ms = Attribute.GetCustomAttribute(mi, typeof(ModuleStartAttribute)) as ModuleStartAttribute;
                if (!Object.Equals(null, ms))
                {
                    return mi;
                }

                AddInStartAttribute mr = Attribute.GetCustomAttribute(mi, typeof(AddInStartAttribute)) as AddInStartAttribute;
                if (!Object.Equals(null, mr))
                {
                    return mi;
                }
            }

            return null;
        }

        internal static bool DemandPrivileges(object module)
        {
            if (SLContext.Instance.Debug) return true;

            Guid Guid1 = GetModuleGuid(module);
            string name = GetModuleName(module);

            if(Guid1== Guid.Empty) return true;

            if (!SLContext.Instance.Adminstrators)
            {
                foreach (var item in NavigationProxy.Instance.ModuleList)
                {
                    Guid Guid2 = new Guid(item.Guid);
                    if (Guid2 == Guid1) return true;
                }
            }

            MessageBox.Show("对不起，你没有运行功能模块 \"" + name + "\"的权限！", "系统消息", MessageBoxButton.OK);
            return false;
        }

        public static void StartModule(object AddIn)
        {
            MethodInfo mi = ModuleManager.GetRunMethod(AddIn);
            if (mi != null)
            {
                mi.Invoke(AddIn, new object[] { });
            }
        }
    }
}
