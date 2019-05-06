using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.Design;
using System.Collections;
using System.ComponentModel;

namespace EAS.Data.Design
{
    /// <summary>
    /// 控件属性转换器。
    /// </summary>
    internal class ControlPropertyConverter : StringListConverter
    {
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            MapperInfo info = (MapperInfo)context.Instance;

            //if (!info.DataUIMapper.DisplayMember)
            //{
            //    string[] sv = new string[] { string.Empty };
            //    return new StandardValuesCollection(sv);
            //}

            IDesignerHost host = (IDesignerHost)context.GetService(typeof(IDesignerHost));
            IReferenceService svc = (IReferenceService)host.GetService(typeof(IReferenceService));

            if (svc == null)
                return null;

            object ctl = svc.GetReference(info.ControlID);

            if (ctl == null) 
            {
                throw new ArgumentException("当前容器中未发现名称为 '" + info.ControlID + "' 的控件。");
            }
            else
            {
                return new StandardValuesCollection(MemberHelper.GetControlProperties(ctl));
            }
        }
    }

    /// <summary>
    /// 数据属性转换器。
    /// </summary>
    internal class DataPropertyConverter : StringListConverter
    {
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            try
            {
                MapperInfo info = (MapperInfo)context.Instance;
                return new StandardValuesCollection(MemberHelper.GetDataPropertys(info.DataUIMapper.Type));
            }
            catch
            {
                string[] sv = new string[] { string.Empty };
                return new StandardValuesCollection(sv);
            }
        }
    }
}
