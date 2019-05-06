using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using System.ComponentModel.Design.Serialization;
using System.ComponentModel.Design;
using System.ComponentModel;

namespace EAS.Data.Design
{
    internal abstract class BaseCodeDomSerializer : CodeDomSerializer
    {
        CodeCommentStatementCollection _headercomment = new CodeCommentStatementCollection(
            new CodeCommentStatement[] { 
										   new CodeCommentStatement(String.Empty), 
										   new CodeCommentStatement("------------- AgileEAS.NET DataUIMapper Code -------------"), 
										   new CodeCommentStatement(String.Empty)
									   });

        protected CodeStatement[] GetCommentHeader(string sectionTitle)
        {
            CodeCommentStatementCollection comments = new CodeCommentStatementCollection(_headercomment);
            comments.Insert(2, new CodeCommentStatement(sectionTitle));
            CodeCommentStatement[] statements = new CodeCommentStatement[comments.Count];
            comments.CopyTo(statements, 0);
            return statements;
        }


        protected CodeDomSerializer GetConfiguredSerializer(IDesignerSerializationManager manager, object value)
        {
            object[] attrs = value.GetType().GetCustomAttributes(typeof(DesignerSerializerAttribute), true);
            if (attrs.Length == 0) return null;
            DesignerSerializerAttribute serializer = (DesignerSerializerAttribute)attrs[0];
            return (CodeDomSerializer)
                Activator.CreateInstance(((ITypeResolutionService)manager.GetService(typeof(ITypeResolutionService))).GetType(serializer.SerializerTypeName));
        }

        protected CodeDomSerializer GetBaseComponentSerializer(IDesignerSerializationManager manager)
        {
            return (CodeDomSerializer)manager.GetSerializer(typeof(Component), typeof(CodeDomSerializer));
        }

        protected CodeStatement AttachToEvent(IDesignerSerializationManager manager,
            string eventName, Type delegateType, object connectingComponent, string componentMethod)
        {
            return new CodeAttachEventStatement(
                new CodeThisReferenceExpression(), eventName,
                new CodeDelegateCreateExpression(
                new CodeTypeReference(delegateType),
                new CodeVariableReferenceExpression(manager.GetName(connectingComponent)),
                componentMethod));
        }

        public override object Deserialize(IDesignerSerializationManager manager, object codeObject)
        {
            return GetBaseComponentSerializer(manager).Deserialize(manager, codeObject);
        }
    }
}
