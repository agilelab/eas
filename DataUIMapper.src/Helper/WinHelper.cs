using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace EAS.Data
{
    class WinHelper
    {
        public static void SetDefaultControlProperty(Control control, ref MapperInfo mi)
        {
            if (control is TextBox)  //文本框
            {
                mi.ControlProperty = "Text";
            }
            else if (control is RichTextBox)  //文本
            {
                mi.ControlProperty = "Text";
            }
            else if (control is Label)  //文本
            {
                mi.ControlProperty = "Text";
            }
            else if (control is LinkLabel)  //文本
            {
                mi.ControlProperty = "Text";
            }
            else if (control is DateTimePicker)  //时间选择
            {
                mi.ControlProperty = "Value";
            }
            else if (control is ComboBox)  //下拉列表
            {
                mi.ControlProperty = "Text";
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
            else if (control is NumericUpDown)  //数字下拉
            {
                mi.ControlProperty = "Value";
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
            //else if (component is Label)  //Label
            //{
            //    return true;
            //}
            //else if (component is LinkLabel)  //LinkLabel
            //{
            //    return true;
            //}
            else if (component is ListBox)  //ListBox
            {
                return true;
            }
            else if (component is ComboBox)  //ComboBox
            {
                return true;
            }
            else if (component is RichTextBox)  //RichTextBox
            {
                return true;
            }
            else if (component is DateTimePicker)  //DateTimePicker
            {
                return true;
            }
            else if (component is NumericUpDown)  //NumericUpDown
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
