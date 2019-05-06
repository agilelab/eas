using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace EAS.WebShell.Utils
{
    class MetaHelper
    {
        public static string GetTypeString(Type type)
        {
            return type.ToString();
        }

        public static string GetAssemblyString(Type type)
        {
            return Assembly.GetAssembly(type).GetName().Name;
        }

        public static string GetVersionString(Type type)
        {
            return Assembly.GetAssembly(type).GetName().Version.ToString();
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
    }
}
