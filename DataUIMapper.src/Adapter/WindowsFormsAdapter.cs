using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

namespace EAS.Data.Adapter
{
    public class WindowsFormsAdapter : BaseAdapter
    {
        Control host;

        #region IAdapter ≥…‘±

        public override void Connect(DataUIMapper dataUIMapper, object controlsContainer)
        {
            base.Connect(dataUIMapper, controlsContainer);
            host = controlsContainer as Control;
        }

        public override object FindControl(string controlId)
        {
            object ret = null;
            FindControl(controlId, host, ref ret);
            return ret;
        }

        public override void UpdateUI(DataUIMapper dm)
        {
            if (dm.DataSource == null)
                return;

            foreach (MapperInfo mi in dm.Mappings)
            {
                if ((mi.DataProperty != null) && (mi.ControlProperty != null) && (mi.ControlID != null))
                {
                    object control = this.FindControl(mi.ControlID);
                    if (control == null)
                        continue;
                    try
                    {
                        System.Type dataType = TypeHelper.GetTypeOfProperty(dm.DataSource, mi.DataProperty);
                        PropertyInfo dataPI = dataType.GetProperty(mi.DataProperty);
                        object value = dataPI.GetValue(dm.DataSource, new object[0]);
                        MemberHelper.SetControlDisplay(control, mi, value);
                    }
                    catch { }
                }
            }
        }

        public override void UpdateObject(DataUIMapper dm)
        {
            if (dm.DataSource == null)
                return;

            foreach (MapperInfo mi in dm.Mappings)
            {
                if ((mi.DataProperty != null) && (mi.ControlProperty != null) && (mi.ControlID != null))
                {
                    object control = this.FindControl(mi.ControlID);
                    if (control == null)
                        continue;

                    try
                    {
                        System.Type ctrlType = TypeHelper.GetTypeOfProperty(control, mi.ControlProperty);
                        PropertyInfo ctrlPI = ctrlType.GetProperty(mi.ControlProperty);
                        System.Type dataType = TypeHelper.GetTypeOfProperty(dm.DataSource, mi.DataProperty);
                        PropertyInfo dataPI = dataType.GetProperty(mi.DataProperty);
                        TypeConverter converter = TypeDescriptor.GetConverter(dataPI.PropertyType);
                        object value = ctrlPI.GetValue(control, new object[0]);
                        dataPI.SetValue(dm.DataSource, converter.ConvertFrom(value), new object[0]);
                    }
                    catch { }
                }
            }
        }

        #endregion

        private void FindControl(string controlToFind, Control currentControl,ref object ret)
        {
            if (currentControl.Name == controlToFind)
            {
                ret = currentControl;
                return;
            }

            foreach (Control ctl in currentControl.Controls)
            {
                FindControl(controlToFind, ctl, ref ret);
            }
        }
    }
}
