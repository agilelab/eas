using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

namespace EAS.Data
{
    class MemberHelper
    {
        /// <summary>
        /// 取属性。
        /// </summary>
        /// <returns></returns>
        public static List<string> GetDataPropertys(System.Type type)
        {
            System.Reflection.PropertyInfo[] pis = type.GetProperties();
            List<string> props = new List<string>(pis.Length);
            foreach (System.Reflection.PropertyInfo pi in pis)
            {
                props.Add(pi.Name);
            }

            if (type.BaseType == null)
                return props;

            if (type.BaseType == typeof(object))
                return props;

            //if ((type.BaseType == typeof(IEntity)) || (type.BaseType == typeof(Entity)) || (type.BaseType == typeof(ITable)) || (type.BaseType == typeof(Table)))
            //    return props;

            List<string> p = GetDataPropertys(type.BaseType);

            foreach (string text in p)
            {
                if (!props.Contains(text))
                    props.Add(text);
            }

            return props;
        }

        public static string[] GetControlProperties(object control)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(
                control, new Attribute[] { new BrowsableAttribute(true) });

            string[] names = new string[props.Count];
            for (int i = 0; i < props.Count; i++)
                names[i] = props[i].Name;
            Array.Sort(names);
            return names;
        }

        /// <summary>
        /// 检测常用控件
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public static bool CheckCommonControl(IComponent component,bool all)
        {
            if (all)
            {
                return true;
            }
            else
            {
                if (component is Control)
                    return WinHelper.CheckCommonControl(component);
                else
                    return WebHelper.CheckCommonControl(component);
            }
        }

        /// <summary>
        /// 显示数据。
        /// </summary>
        /// <param name="control"></param>
        /// <param name="mi"></param>
        /// <param name="value"></param>
        public static void SetControlDisplay(object control, MapperInfo mi, object value)
        {
            System.Type type = TypeHelper.GetTypeOfProperty(control, mi.ControlProperty);
            PropertyInfo pi = type.GetProperty(mi.ControlProperty);

            if (pi.PropertyType != typeof(string))
            {
                pi.SetValue(control, value, new object[] { });
                return;
            }

            //字符串属性

            if (value is string)
            {
                pi.SetValue(control, value, new object[] { });
            }
            else if (value is DateTime) //时间
            {
                if (mi.Format == Format.Date)
                {
                    pi.SetValue(control, ((DateTime)value).ToShortDateString(), new object[] { });
                }
                if (mi.Format == Format.Time)
                {
                    pi.SetValue(control, ((DateTime)value).ToShortTimeString(), new object[] { });
                }
                if (mi.Format == Format.DateAndTime)
                {
                    pi.SetValue(control, ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss"), new object[] { });
                }
                else
                {
                    pi.SetValue(control, ((DateTime)value).ToString(), new object[] { });
                }
            }
            else if (value is float)
            {
                if (mi.Format == Format.F2)
                {
                    pi.SetValue(control, ((float)value).ToString("F2"), new object[] { });
                }
                if (mi.Format == Format.MF2)
                {
                    pi.SetValue(control, ((float)value).ToString("C2"), new object[] { });
                }
                if (mi.Format == Format.F4)
                {
                    pi.SetValue(control, ((float)value).ToString("F4"), new object[] { });
                }
                if (mi.Format == Format.MF4)
                {
                    pi.SetValue(control, ((float)value).ToString("C4"), new object[] { });
                }
                if (mi.Format == Format.F6)
                {
                    pi.SetValue(control, ((float)value).ToString("F6"), new object[] { });
                }
                if (mi.Format == Format.MF6)
                {
                    pi.SetValue(control, ((float)value).ToString("C6"), new object[] { });
                }
                else
                {
                    pi.SetValue(control, ((float)value).ToString(), new object[] { });
                }
            }
            else if (value is double)
            {
                if (mi.Format == Format.F2)
                {
                    pi.SetValue(control, ((double)value).ToString("F2"), new object[] { });
                }
                if (mi.Format == Format.MF2)
                {
                    pi.SetValue(control, ((double)value).ToString("C2"), new object[] { });
                }
                if (mi.Format == Format.F4)
                {
                    pi.SetValue(control, ((double)value).ToString("F4"), new object[] { });
                }
                if (mi.Format == Format.MF4)
                {
                    pi.SetValue(control, ((double)value).ToString("C4"), new object[] { });
                }
                if (mi.Format == Format.F6)
                {
                    pi.SetValue(control, ((double)value).ToString("F6"), new object[] { });
                }
                if (mi.Format == Format.MF6)
                {
                    pi.SetValue(control, ((double)value).ToString("C6"), new object[] { });
                }
                else
                {
                    pi.SetValue(control, ((double)value).ToString(), new object[] { });
                }
            }
            else if (value is decimal)
            {
                if (mi.Format == Format.F2)
                {
                    pi.SetValue(control, ((decimal)value).ToString("F2"), new object[] { });
                }
                if (mi.Format == Format.MF2)
                {
                    pi.SetValue(control, ((decimal)value).ToString("C2"), new object[] { });
                }
                if (mi.Format == Format.F4)
                {
                    pi.SetValue(control, ((decimal)value).ToString("F4"), new object[] { });
                }
                if (mi.Format == Format.MF4)
                {
                    pi.SetValue(control, ((decimal)value).ToString("C4"), new object[] { });
                }
                if (mi.Format == Format.F6)
                {
                    pi.SetValue(control, ((decimal)value).ToString("F6"), new object[] { });
                }
                if (mi.Format == Format.MF6)
                {
                    pi.SetValue(control, ((decimal)value).ToString("C6"), new object[] { });
                }
                else
                {
                    pi.SetValue(control, ((decimal)value).ToString(), new object[] { });
                }
            }
            else
            {
                pi.SetValue(control, value.ToString(), new object[] { });
            }
        }
    }
}
