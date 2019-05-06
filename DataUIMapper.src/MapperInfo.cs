using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using EAS.Data.Design;

namespace EAS.Data
{
    public class MapperInfoList : List<MapperInfo>
    {
        public new  void Add(MapperInfo item)
        {
            if (Exists(item.ControlID))
                throw new System.Exception("已经存在控件:" + item.ControlID + "的数据映射");

            base.Add(item);
        }

        public bool Exists(string controlID)
        {
            foreach (MapperInfo item in this)
            {
                if (item.ControlID == controlID)
                    return true;
            }

            return false;
        }

        public bool ContainsKey(string controlID)
        {
            return this.IndexOfKey(controlID) > -1;
        }

        public int IndexOfKey(string controlID)
        {
            int index = -1;

            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].ControlID == controlID)
                    return i;
            }

            return index;
        }

        public MapperInfo this[string Key]
        {
            get
            {
                int index = this.IndexOfKey(Key);

                if (index > -1)
                    return this[index];
                else
                    return null;
            }
            set
            {
                int index = this.IndexOfKey(Key);

                if (index > -1)
                    this[index] = value;
                else
                    this.Add(value);
            }
        }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MapperInfo
    {
        DataUIMapper mapper;

        string ctrlid = String.Empty;
        string dataproperty = String.Empty;
        string controlproperty = String.Empty;
        Format format = Format.None;

        public override string ToString()
        {
            return dataproperty + " -> " + ctrlid + "." + controlproperty;
        }

        public MapperInfo()
        {
        }

        public MapperInfo(DataUIMapper mapper, string controlId)
        {
            this.mapper = mapper;
            this.ctrlid = controlId;
        }

        public MapperInfo(string controlId, string controlProperty, string dataProperty)
        {
            this.ctrlid = controlId;
            this.controlproperty = controlProperty;
            this.dataproperty = dataProperty;
        }

        public MapperInfo(string controlId, string controlProperty, string dataProperty,int format)
        {
            this.ctrlid = controlId;
            this.controlproperty = controlProperty;
            this.dataproperty = dataProperty;
            this.format = (Format)format;
        }

        [Browsable(false)]
        internal DataUIMapper DataUIMapper
        {
            get
            {
                return mapper;
            }
            set
            {
                mapper = value;
            }
        }

        [DefaultValue("")]
        [Browsable(false)]
        [Description("控件名称/ID")]
        public string ControlID
        {
            get
            {
                return ctrlid;
            }
            set
            {
                ctrlid = value;
            }
        }
                
        [DefaultValue("")]
        [Description("控件属性")]
        [TypeConverter(typeof(ControlPropertyConverter))]
        public string ControlProperty
        {
            get
            {
                return controlproperty;
            }
            set
            {
                controlproperty = value;
            }
        }

        [DefaultValue("")]
        [Description("数据属性")]
        [TypeConverter(typeof(DataPropertyConverter))]
        public string DataProperty
        {
            get
            {
                return dataproperty;
            }
            set
            {
                dataproperty = value;
            }
        }

        [Description("显示格式")]
        [DefaultValue(Format.None)]
        public Format Format
        {
            get
            {
                return format;
            }
            set
            {
                format = value;
            }
        }
    }

    /// <summary>
    /// 数字格式。
    /// </summary>
    public enum Format
    {
        [Description("无格式")]
        None,

        [Description("日期")]
        Date,

        [Description("时间")]
        Time,

        [Description("日期和时间")]
        DateAndTime,

        [Description("2位小数")]
        F2,

        [Description("4位小数")]
        F4,

        [Description("6位小数")]
        F6,

        [Description("2位小数/货币")]
        MF2,

        [Description("4位小数/货币")]
        MF4,

        [Description("6位小数/货币")]
        MF6,
    }
}
