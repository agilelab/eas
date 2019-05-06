using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace EAS.Data
{
    class WebHelper
    {
        public static void SetDefaultControlProperty(Control control, ref MapperInfo mi)
        {
            if (control is TextBox)  //文本框
            {
                mi.ControlProperty = "Text";
            }
            else if (control is Label)  //文本
            {
                mi.ControlProperty = "Text";
            }
            else if (control is Calendar)  //Calendar
            {
                mi.ControlProperty = "SelectedDate";
            }
            else if (control is CheckBox)  //复选
            {
                mi.ControlProperty = "Checked";
            }
            else if (control is RadioButton)  //单选
            {
                mi.ControlProperty = "Checked";
            }
            else if (control is ListBox)  //列表
            {
                mi.ControlProperty = "Text";
            }
        }

        /// <summary>
        /// 检测常用控件
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public static bool CheckCommonControl(IComponent component)
        {
            if (component is TextBox)  //TextBox
            {
                return true;
            }
            else if (component is ListBox)  //ListBox
            {
                return true;
            }
            else if (component is Calendar)  //Calendar
            {
                return true;
            }
            else if (component is CheckBox)  //CheckBox
            {
                return true;
            }
            else if (component is RadioButton)  //RadioButton
            {
                return true;
            }

            return false;
        }
    }
}
