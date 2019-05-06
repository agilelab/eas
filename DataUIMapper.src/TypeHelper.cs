using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace EAS.Data
{
    class TypeHelper
    {
        /// <summary>
        /// 取得属性所在类型。
        /// </summary>
        /// <param name="instance">对象实例。</param>
        /// <param name="propertyName">属性名称。</param>
        /// <returns>类型。</returns>
        public static System.Type GetTypeOfProperty(object instance, string propertyName)
        {
            return GetTypeOfProperty(instance.GetType(), propertyName);
        }

        /// <summary>
        /// 取得属性所在类型。
        /// </summary>
        /// <param name="type">对象类型。</param>
        /// <param name="propertyName">属性名称。</param>
        /// <returns>类型。</returns>
        public static System.Type GetTypeOfProperty(System.Type type, string propertyName)
        {
            System.Reflection.PropertyInfo prop = type.GetProperty(propertyName);

            if (prop != null)
            {
                return prop.DeclaringType;
            }
            else if (type.BaseType != typeof(System.Object))
            {
                return GetTypeOfProperty(type.BaseType, propertyName);
            }
            else
            {
                return null;
            }
        }
    }
}
