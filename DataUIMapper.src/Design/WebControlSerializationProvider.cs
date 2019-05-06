using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.Design.Serialization;
using System.Web.UI;
using System.CodeDom;
using System.ComponentModel;

namespace EAS.Data.Design
{
    /// <summary>
    /// Web控件的序列化提供者。
    /// </summary>
    internal class WebControlSerializationProvider : IDesignerSerializationProvider
    {
        /// <summary>
        /// 序列化提供者。
        /// </summary>
        public object GetSerializer(IDesignerSerializationManager manager, object currentSerializer, Type objectType, Type serializerType)
        {
            if (typeof(IAttributeAccessor).IsAssignableFrom(objectType) && serializerType == typeof(CodeDomSerializer))
                return new WebControlSerializer();

            return null;
        }
    }

    /// <summary>
    /// 生成序列化映射记录。
    /// </summary>
    internal class WebControlSerializer : BaseCodeDomSerializer
    {
        public override object Serialize(IDesignerSerializationManager manager, object value)
        {
            CodeDomSerializer serial = GetConfiguredSerializer(manager, value);
            if (serial == null)
                return null;

            CodeStatementCollection statements = (CodeStatementCollection)serial.Serialize(manager, value);

            PropertyDescriptor prop = TypeDescriptor.GetProperties(value)["WebMapping"];
            MapperInfo info = (MapperInfo)prop.GetValue(value);

            DataUIMapper dm = (DataUIMapper)
                DesignUtils.ProviderProperty.GetValue(prop, new object[0]);

            //Attach the view mappings to the control attributes.
            if (info.ControlProperty != String.Empty &&  info.DataProperty != String.Empty)
            {
                CodeExpression ctlref = SerializeToExpression(manager, value);
                CodeCastExpression cast = new CodeCastExpression(typeof(IAttributeAccessor), ctlref);

                statements.Add(new CodeMethodInvokeExpression(
                    cast, "SetAttribute", new CodeExpression[] {
																   new CodePrimitiveExpression("DIM_Mapper"), 
																   new CodePrimitiveExpression(manager.GetName(dm)) 
															   }));
                statements.Add(new CodeMethodInvokeExpression(
                    cast, "SetAttribute", new CodeExpression[] {
																   new CodePrimitiveExpression("DIM_Format"), 
																   new CodePrimitiveExpression(info.Format) 
															   }));
                statements.Add(new CodeMethodInvokeExpression(
                    cast, "SetAttribute", new CodeExpression[] {
																   new CodePrimitiveExpression("DIM_DataProperty"), 
																   new CodePrimitiveExpression(info.DataProperty) 
															   }));
                statements.Add(new CodeMethodInvokeExpression(
                    cast, "SetAttribute", new CodeExpression[] {
																   new CodePrimitiveExpression("DIM_ControlProperty"), 
																   new CodePrimitiveExpression(info.ControlProperty) 
															   }));
            }
            return statements;
        }
    }
}
