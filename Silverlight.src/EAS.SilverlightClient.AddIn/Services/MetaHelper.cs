using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using EAS.Modularization;

namespace EAS.SilverlightClient.AddIn
{
    /// <summary>
    /// ÔªÊý¾Ý¡£
    /// </summary>
    class MetaHelper
    {
        public static string GetTypeString(Type type)
        {
            return type.ToString();
        }

        public static string GetAssemblyString(Type type)
        {
            return type.Assembly.FullName.Split(',')[0];
        }

        public static string GetVersionString(Type type)
        {
            string vx = type.Assembly.FullName.Split(',')[1];
            return vx.Split('=')[1];
        }

        public static string GetDeveloperString(Type type)
        {
            try
            {
                AssemblyCompanyAttribute aca = type.Assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), true)[0] as AssemblyCompanyAttribute;

                if (aca != null)
                {
                    return aca.Company;
                }
                else
                {
                    return "AgileLab.cn";
                }
            }
            catch
            {
                return "AgileLab.cn";
            }
        }

        public static string GetModuleGuid(Type type)
        {
            AddInAttribute ma = Attribute.GetCustomAttribute(type, typeof(AddInAttribute)) as AddInAttribute;
            if (!Object.Equals(null, ma))
            {
                return ma.Guid;
            }
            else
                return string.Empty;
        }        


        public static string GetModuleName(Type type)
        {
            AddInAttribute ma = Attribute.GetCustomAttribute(type, typeof(AddInAttribute)) as AddInAttribute;
            if (!Object.Equals(null, ma))
            {
                return ma.Name;
            }
            else
                return string.Empty;
        }

        public static string GetModuleDescription(Type type)
        {
            AddInAttribute ma = Attribute.GetCustomAttribute(type, typeof(AddInAttribute)) as AddInAttribute;
            if (!Object.Equals(null, ma))
            {
                return ma.Description;
            }
            else
                return string.Empty;
        }        

    }
}
