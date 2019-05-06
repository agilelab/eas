using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Windows.Forms.Design;
using System.Windows.Forms;

namespace EAS.Data.Design
{
    internal class MappingsEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            object retvalue = value;

            try
            {
				IDesignerHost host = (IDesignerHost) context.GetService(typeof(IDesignerHost));
				IWindowsFormsEditorService srv = (IWindowsFormsEditorService)	context.GetService(typeof(IWindowsFormsEditorService));
                
                if (srv != null && host != null)
                {
                    DataUIMapper dm = (DataUIMapper)context.Instance;
                    IDesigner designer = host.GetDesigner(dm);
                    
                    MappingsEditorForm form = new MappingsEditorForm(designer);
                    //form.SetMappings(dm.Mappings);

                    form.Host = host;
                    form.DataUIMapper = dm;

                    //Show form.
                    if (srv.ShowDialog(form) == DialogResult.OK)
                    {
                        MapperInfoList mappings = form.GetMappings();
                        context.PropertyDescriptor.SetValue(context.Instance, mappings);

                        //if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        //{
                        //    MapperInfoList mappings = form.GetMappings();
                        //    context.PropertyDescriptor.SetValue(context.Instance, mappings);
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    "属性设置时异常: " + ex.ToString());
            }

            return retvalue;
        }
    }
}
