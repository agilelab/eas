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

namespace EAS.SilverlightClient.AddIn
{	
    /// <summary>
    /// 模块管理器。
    /// </summary>
    class ModuleManager
    {
        public ModuleManager()
        {

        }

        internal static object LoadModule(System.Guid Guid, ISession session)
        {
            EAS.Explorer.Entities.Module mv = new EAS.Explorer.Entities.Module();
            mv.Guid = Guid.ToString();
            //mv.Refresh();

            if (!mv.Exists)
                return null;

            object module = ClassProvider.GetObjectInstance(mv.Assembly, mv.Type);

            if (module == null)
            {
                throw new System.Exception("无法加载模块“" + mv.Name + "”，请通知您的系统管理人员。");
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
            
        }

        /// <summary>
        /// 关闭当前模块。
        /// </summary>
        /// <param name="shell"></param>
        internal static void CloseModule()
        {
            
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
            Guid guid = GetModuleGuid(module);
            string name = GetModuleName(module);

            //if (!WinContext.Instance.Adminstrators)
            //{
            //    IACLService service = ServiceContainer.GetService<IACLService>();
            //    if (!service.Demand(guid, WinContext.Account.LoginID, (int)Privileges.Execute))
            //    {
            //        MessageBox.Show("对不起，你没有运行功能模块 \"" + name + "\"的权限！", "系统消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return false;
            //    }
            //}

            return true;
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
