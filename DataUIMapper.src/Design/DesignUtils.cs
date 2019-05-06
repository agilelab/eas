using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace EAS.Data.Design
{
    internal class DesignUtils
    {
        internal static Type LoadPrivateType(string className)
        {
            string[] names = className.Split(',');
            if (names.Length < 2) throw new ReflectionTypeLoadException(null, null, "Invalid class name: " + className);

            AssemblyName[] asmNames = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
            Assembly asm = null;
            foreach (AssemblyName name in asmNames)
            {
                if (String.CompareOrdinal(name.Name, names[1]) == 0)
                {
                    asm = Assembly.Load(name);
                    break;
                }
            }
            if (asm == null) throw new ReflectionTypeLoadException(null, null, names[1] + " assembly couldn't be loaded.");

            //Locate the type. Iteration is required because it's not public.
            Type type = null;
            foreach (Type t in asm.GetTypes())
            {
                if (String.CompareOrdinal(t.FullName, names[0]) == 0)
                {
                    type = t;
                    break;
                }
            }

            if (type == null) throw new ReflectionTypeLoadException(null, null, className + " wasn't found.");

            return type;
        }

        /// <summary>
        /// 提供者属性。 
        /// </summary>
        internal static PropertyInfo ProviderProperty = DesignUtils.LoadPrivateType("System.ComponentModel.ExtendedPropertyDescriptor,System").GetProperty("Provider");

    }
}
