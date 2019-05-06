using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.Design.Serialization;
using System.CodeDom;
using System.ComponentModel.Design;

namespace EAS.Data.Design
{
    internal class ControllerCodeDomSerializer : BaseCodeDomSerializer
    {
        public override object Serialize(IDesignerSerializationManager manager, object value)
        {
            CodeDomSerializer serial = GetBaseComponentSerializer(manager);
            if (serial == null)
                return null;         

            CodeStatementCollection statements = (CodeStatementCollection)serial.Serialize(manager, value);

            //serializer for web controls.
            if (!(manager.GetSerializer(typeof(System.Web.UI.Control), typeof(CodeDomSerializer)) is WebControlSerializer))
            {
                manager.AddSerializationProvider(new WebControlSerializationProvider());
            }

            IDesignerHost host = (IDesignerHost)manager.GetService(typeof(IDesignerHost));
            if (host.RootComponent == value)
                return statements;

            statements.AddRange(GetCommentHeader("Mapper code"));
            DataUIMapper cn = (DataUIMapper)value;
            CodeExpression cnref = SerializeToExpression(manager, value);

            #region Mapping property serialization

            CodePropertyReferenceExpression propref = new CodePropertyReferenceExpression(cnref, "Mappings");
            foreach (MapperInfo mi in cn.Mappings)
            {
                MapperInfo info = mi;
                if (info.ControlID != String.Empty && info.ControlProperty != null &&
                     info.DataProperty != String.Empty)
                {
                    object ctl = manager.GetInstance(info.ControlID);
                    //if (ctl == null)
                    //{
                    //    manager.ReportError(String.Format("Control '{0}' associated with the view mapping in controller '{1}' doesn't exist in the page.", info.ControlID, manager.GetName(value)));
                    //    continue;
                    //}
                    if (ctl.GetType().GetProperty(info.ControlProperty) == null)
                    {
                        manager.ReportError(String.Format("Control property '{0}' in control '{1}' associated with the view mapping in datauimapper '{2}' doesn't exist.", info.ControlProperty, info.ControlID, manager.GetName(value)));
                        continue;
                    }

                    statements.Add(
                        new CodeMethodInvokeExpression(
                        propref, "Add",
                        new CodeExpression[] 
                            { 												 
												 new CodeObjectCreateExpression(
												 typeof(MapperInfo),
                                                 new CodeExpression[] { 
																		  new CodePrimitiveExpression(info.ControlID),
																		  new CodePrimitiveExpression(info.ControlProperty),  
																		  new CodePrimitiveExpression(info.DataProperty) ,new CodePrimitiveExpression((int)info.Format) 
                                                                                }
												 ) }
                        ));
                }
            }

            #endregion

            statements.Add(
                new CodeCommentStatement("Connect the host environment."));

            if (host.RootComponent as System.Windows.Forms.Form != null)
            {
                CodeObjectCreateExpression adapter = new CodeObjectCreateExpression(typeof(Adapter.WindowsFormsAdapter), new CodeExpression[0]);
                CodeExpression connect = new CodeMethodInvokeExpression(adapter, "Connect",
                    new CodeExpression[] {
											 cnref, 
											 new CodeThisReferenceExpression(), 
											  });
                statements.Add(connect);
            }
            else if (host.RootComponent as System.Web.UI.Page != null)
            {
                CodeObjectCreateExpression adapter = new CodeObjectCreateExpression(typeof(Adapter.WebFormsAdapter), new CodeExpression[0]);
                CodeExpression connect = new CodeMethodInvokeExpression(adapter, "Connect",
                    new CodeExpression[] {
											 cnref, 
											 new CodeThisReferenceExpression(),
											  });
                statements.Add(connect);
            }

            return statements;
        }
    }
}
