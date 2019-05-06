using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace EAS.Data.Design
{
    internal partial class MappingsEditorForm : Form
    {
        IDesigner _designer;
        IReferenceService _reference;
        ITypeResolutionService _resolution;
        IDesignerHost host;

        DataUIMapper dm = null;
        bool DataList = false;

        /// <summary>
        /// DM。
        /// </summary>
        public DataUIMapper DataUIMapper
        {
            get
            {
                return this.dm;
            }
            set
            {
                this.dm = value;
            }
        }

        /// <summary>
        /// 设计时环境。
        /// </summary>
        public IDesignerHost Host
        {
            get
            {
                return this.host;
            }
            set
            {
                this.host =value;
            }
        }

        #region 读写属性

        public void SetMappings(MapperInfoList mappings)
        {
            this.lstMappings.Items.Clear();

            foreach (MapperInfo mi in mappings)
            {
                ListViewItem item = new ListViewItem(mi.ToString());
                item.Tag = mi;
                lstMappings.Items.Add(item);
            }

            this.lstMappings.SelectedIndexChanged+=new EventHandler(lstMappings_SelectedIndexChanged);

            if(lstMappings.Items.Count>0)
                lstMappings.Items[0].Selected =lstMappings.Items[0].Focused = true;
        }

        public MapperInfoList GetMappings()
        {
            MapperInfoList miList = new MapperInfoList();

            foreach (ListViewItem item in this.lstMappings.Items)
            {
                MapperInfo mi = item.Tag as MapperInfo;
                miList.Add(mi);
            }

            return miList;
        }

        #endregion

        public MappingsEditorForm()
        {
            InitializeComponent();
        }

        public MappingsEditorForm(IDesigner designer)
		{
			InitializeComponent();
			_designer = designer;
		}

        protected override void OnLoad(EventArgs e)
        {
            this._reference = (IReferenceService)_designer.Component.Site.GetService(typeof(IReferenceService));
            _resolution = (ITypeResolutionService)
            _designer.Component.Site.GetService(typeof(ITypeResolutionService));

            this.InitControls();

            this.SetMappings(this.dm.Mappings);
        }

        /// <summary>
        /// Loads all controls in the page.
        /// </summary>
        void InitControls()
        {
            try
            {
                stStatus.Text = "加载控件... ";

                List<IComponent> controls = this.GetControls();
                foreach (IComponent control in controls)
                {
                    if(MemberHelper.CheckCommonControl(control,this.rbAll.Checked))
                        cbControl.Items.Add(new ControlEntry(control, _reference.GetName(control)));
                }

                if (cbControl.Items.Count > 0)
                    cbControl.SelectedIndex = 0;

                if (this.DataUIMapper.Type == null)
                {
                    this.tbModelProperty.Visible = true;
                    this.cbModelProperty.Visible = false;
                }
                else
                {
                    this.tbModelProperty.Visible = false;
                    this.cbModelProperty.Visible = true;

                    this.cbModelProperty.DropDownStyle = ComboBoxStyle.DropDownList;
                    List<string> propertys = MemberHelper.GetDataPropertys(this.DataUIMapper.Type);
                    foreach (string text in propertys)
                    {
                        cbModelProperty.Items.Add(text);
                    }

                    if (cbModelProperty.Items.Count > 0)
                        cbModelProperty.SelectedIndex = 0;
                }

                stStatus.Text = " 就绪";
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    "初始化出错: " + ex.ToString());
            }
        }        

        #region 读属性

        /// <summary>
        /// 取控件。
        /// </summary>
        /// <returns></returns>
        List<IComponent> GetControls()
        {
            if (host.RootComponent is System.Windows.Forms.Form)
            {
                List<IComponent> controls = new List<IComponent>(host.Container.Components.Count);
                foreach (IComponent component in host.Container.Components)
                {
                    if (component != host.RootComponent && component is Control)
                    {
                        controls.Add(component);
                    }
                }
                return controls;
            }
            else if (host.RootComponent is System.Web.UI.Page)
            {
                List<IComponent> controls = new List<IComponent>(host.Container.Components.Count);
                foreach (IComponent component in host.Container.Components)
                {
                    if (component != host.RootComponent && component is Control)
                        controls.Add(component);
                }
                return controls;
            }
            else
            {
                return null;
            }
        }        

        #endregion

        #region 联动属性

        public ListViewItem SelectedItem
        {
            get
            {
                if (lstMappings.SelectedItems.Count > 0)
                    return lstMappings.SelectedItems[0];
                else
                    return null;
            }
        }

        private void cbControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((this.SelectedItem != null) && (this.cbControl.SelectedIndex > -1))
                {
                    MapperInfo mi = this.SelectedItem.Tag as MapperInfo;
                    mi.ControlID = ((ControlEntry)cbControl.SelectedItem).Name;
                    this.SelectedItem.Text = mi.ToString();
                }

                cbControlProperty_DropDown(sender, e);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    "错误: " + ex.ToString());
            }
        }        

        private void cbControlProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((this.SelectedItem != null) && (this.cbControlProperty.SelectedIndex > -1))
                {
                    MapperInfo mi = this.SelectedItem.Tag as MapperInfo;
                    mi.ControlProperty = cbControlProperty.Text;
                    this.SelectedItem.Text = mi.ToString();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    "出错: " + ex.ToString());
            }
        }

        private void cbModelProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((this.SelectedItem != null) && (this.cbModelProperty.SelectedIndex > -1))
                {
                    MapperInfo mi = this.SelectedItem.Tag as MapperInfo;
                    mi.DataProperty = cbModelProperty.Text;
                    this.SelectedItem.Text = mi.ToString();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    "错误: " + ex.ToString());
            }
        }

        void tbModelProperty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.SelectedItem != null)
                {
                    MapperInfo mi = this.SelectedItem.Tag as MapperInfo;
                    mi.DataProperty = tbModelProperty.Text;
                    this.SelectedItem.Text = mi.ToString();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    "错误: " + ex.ToString());
            }
        }

        private void cbFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.SelectedItem != null)
                {
                    switch (cbFormat.Text)
                    {
                        case "Date":
                            ((MapperInfo)this.SelectedItem.Tag).Format = Format.Date;
                            break;
                        case "Time":
                            ((MapperInfo)this.SelectedItem.Tag).Format = Format.Time;
                            break;
                        case "DateAndTime":
                            ((MapperInfo)this.SelectedItem.Tag).Format = Format.DateAndTime;
                            break;
                        case "F2":
                            ((MapperInfo)this.SelectedItem.Tag).Format = Format.F2;
                            break;
                        case "F4":
                            ((MapperInfo)this.SelectedItem.Tag).Format = Format.F4;
                            break;
                        case "F6":
                            ((MapperInfo)this.SelectedItem.Tag).Format = Format.F6;
                            break;

                        case "MF2":
                            ((MapperInfo)this.SelectedItem.Tag).Format = Format.MF2;
                            break;
                        case "MF4":
                            ((MapperInfo)this.SelectedItem.Tag).Format = Format.MF4;
                            break;
                        case "MF6":
                            ((MapperInfo)this.SelectedItem.Tag).Format = Format.MF6;
                            break;
                        default:
                            ((MapperInfo)this.SelectedItem.Tag).Format = Format.None;
                            break;
                    }             
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    "错误: " + ex.ToString());
            }
        }

        private void lstMappings_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SelectedItem == null)
                return;

            try
            {
                MapperInfo mi = this.SelectedItem.Tag as MapperInfo;

                if (mi == null)
                {
                    pnlGeneral.Enabled = false;
                    cbControl.SelectedIndex = -1;
                    cbControlProperty.SelectedIndex = -1;
                    cbFormat.SelectedIndex = 0;
                    tbModelProperty.Text = string.Empty;
                    return;
                }

                pnlGeneral.Enabled = true;
                if (mi.ControlID != String.Empty)
                {
                    object ctl = _reference.GetReference(mi.ControlID);
                    if (ctl == null)
                    {
                        mi.ControlID = String.Empty;
                    }
                    else
                    {
                        cbControl.SelectedIndex = cbControl.FindStringExact(new ControlEntry(ctl, _reference.GetName(ctl)).ToString());
                    }
                }

                if (mi.ControlProperty != String.Empty)
                    cbControlProperty.SelectedIndex = cbControlProperty.FindStringExact(mi.ControlProperty);
                else
                    cbControlProperty.SelectedIndex = -1;

                if (mi.DataProperty != string.Empty)
                    cbModelProperty.SelectedIndex = cbModelProperty.FindStringExact(mi.DataProperty);
                else
                    cbModelProperty.SelectedIndex = cbModelProperty.FindStringExact(mi.DataProperty);

                tbModelProperty.Text = mi.DataProperty;
                cbFormat.SelectedIndex = (int)mi.Format;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    "出错: " + ex.ToString());
            }
        }

        private void cbControlProperty_DropDown(object sender, EventArgs e)
        {
            if (cbControl.SelectedItem == null)
                return;

            cbControlProperty.Items.Clear();

            try
            {
                object control = ((ControlEntry)cbControl.SelectedItem).Control;
                string controlID2 = ((ControlEntry)cbControl.SelectedItem).Name;

                cbControlProperty.Items.Clear();
                cbControlProperty.Text = String.Empty;
                cbControlProperty.Items.AddRange(MemberHelper.GetControlProperties(control));

                string controlID = string.Empty;

                if (this.SelectedItem != null)
                {
                    controlID = (this.SelectedItem.Tag as MapperInfo).ControlID;
                }

                if (controlID == controlID2)
                {
                    MapperInfo mi = this.SelectedItem.Tag as MapperInfo;
                    if (mi.ControlProperty != String.Empty)
                        cbControlProperty.SelectedIndex = cbControlProperty.FindStringExact(mi.ControlProperty);
                    else
                        this.SetDefaultControlProperty(control);
                }
                else 
                {
                    this.SetDefaultControlProperty(control);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    "错误: " + ex.ToString());
            }
        }

        void SetDefaultControlProperty(object control)
        {
            if (control is TextBox)  //文本框
            {
                cbControlProperty.SelectedIndex = cbControlProperty.FindStringExact("Text");
            }
            else if (control is RichTextBox)  //文本
            {
                cbControlProperty.SelectedIndex = cbControlProperty.FindStringExact("Text");
            }
            else if (control is Label)  //文本
            {
                cbControlProperty.SelectedIndex = cbControlProperty.FindStringExact("Text");
            }
            else if (control is LinkLabel)  //文本
            {
                cbControlProperty.SelectedIndex = cbControlProperty.FindStringExact("Text");
            }
            else if (control is DateTimePicker)  //时间选择
            {
                cbControlProperty.SelectedIndex = cbControlProperty.FindStringExact("Value");
            }
            else if (control is ComboBox)  //下拉列表
            {
                cbControlProperty.SelectedIndex = cbControlProperty.FindStringExact("Text");
                //ComboBox box = control as ComboBox;
                //if (box.DropDownStyle == ComboBoxStyle.DropDownList)
                //{
                //    cbControlProperty.SelectedIndex = cbControlProperty.FindStringExact("SelectedValue");
                //}
                //else
                //{
                //    cbControlProperty.SelectedIndex = cbControlProperty.FindStringExact("Text");
                //}
            }
            else if (control is CheckBox)  //复选
            {
                cbControlProperty.SelectedIndex = cbControlProperty.FindStringExact("Checked");
            }
            else if (control is RadioButton)  //单选
            {
                cbControlProperty.SelectedIndex = cbControlProperty.FindStringExact("Checked");
            }
            else if (control is ListBox)  //列表
            {
                cbControlProperty.SelectedIndex = cbControlProperty.FindStringExact("Text");
            }
            else if (control is NumericUpDown)  //数字下拉
            {
                cbControlProperty.SelectedIndex = cbControlProperty.FindStringExact("Value");
            }
        }

        #endregion

        private void btnAdd_Click(object sender, EventArgs e)
        {
            MapperInfo mi = new MapperInfo();
            ListViewItem item = new ListViewItem(mi.ToString());
            item.Tag = mi;
            lstMappings.Items.Add(item);
            item.Selected = true;
            item.Focused = true;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (this.SelectedItem != null)
            {
                this.lstMappings.SelectedIndexChanged -= new EventHandler(lstMappings_SelectedIndexChanged);
                lstMappings.Items.Remove(this.SelectedItem);
                this.lstMappings.SelectedIndexChanged += new EventHandler(lstMappings_SelectedIndexChanged);
                this.lstMappings_SelectedIndexChanged(this.lstMappings, e);
            }
        }

        private void rbSelect_CheckedChanged(object sender, EventArgs e)
        {
            this.InitControls();
        }

        private void rbAll_CheckedChanged(object sender, EventArgs e)
        {
            this.InitControls();
        }

        private void lstMappings_Enter(object sender, EventArgs e)
        {
            if (lstMappings.Items.Count > 0)
                lstMappings.Items[0].Selected = lstMappings.Items[0].Focused = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.cbControl.SelectedIndexChanged-=new EventHandler(cbControl_SelectedIndexChanged);
            this.cbControlProperty.SelectedIndexChanged-=new EventHandler(cbControlProperty_SelectedIndexChanged);
            this.cbModelProperty.SelectedIndexChanged -= new EventHandler(cbModelProperty_SelectedIndexChanged);
            this.tbModelProperty.TextChanged -= new EventHandler(tbModelProperty_TextChanged);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }

    struct ControlEntry
    {
        public object Control;
        public string Name;

        public ControlEntry(object control, string name)
        {
            this.Control = control;
            this.Name = name;
        }
        public override string ToString()
        {
            string id = this.Name.PadRight(20, ' ');
            return id + " [" + Control.GetType().Name + "]";
        }
    }
}