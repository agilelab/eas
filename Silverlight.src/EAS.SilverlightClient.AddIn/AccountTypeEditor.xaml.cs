using System;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;

using System.Linq;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EAS.Controls;
using EAS.Explorer.Entities;
using EAS.Data.Linq;
using EAS.Data;
using EAS.Explorer.BLL;
using EAS.Services;
using EAS.Security;

namespace EAS.SilverlightClient.AddIn
{
    partial class AccountTypeEditor : DataWindow
    {
        bool iclose = false;

        public AccountTypeEditor()
        {
            InitializeComponent();
            this.cbxForm.Items.Add("无原型信息");
            this.cbxForm.SelectedIndex = 0;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.Apply();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        /// <summary>
        /// 上级机构。
        /// </summary>
        public Organization ParentOrgan
        {
            get;
            set;
        }

        /// <summary>
        /// Category。
        /// </summary>
        public Organization Organization
        {
            get
            {
                return this.DataEntity as Organization;
            }
            set
            {
                this.DataEntity = value;
            }
        }

        protected override void OnDataEntityChanged(EventArgs e)
        {
            base.OnDataEntityChanged(e);

            if (this.DataEntity != null)
            {
                if (!iclose)
                {
                    this.tbName.Text = this.Organization.Name;
                    this.tbExplain.Text = this.Organization.Explain;

                    Guid moduleID = Guid.Empty;
                    Guid.TryParse(this.Organization.Module, out moduleID);
                    if (moduleID != Guid.Empty)
                    {
                        //Module module = new Module { Guid = moduleID.ToString() };
                        //module.Read();
                        //this.cbxForm.Tag = module.Guid;
                    }

                    this.tbAddress.Text = this.Organization.Address;
                    this.tbContact.Text = this.Organization.Contact;
                    this.tbTel.Text = this.Organization.Tel;
                    this.tbFax.Text = this.Organization.Fax;
                    this.tbEMail.Text = this.Organization.Email;
                    this.tbHomePage.Text = this.Organization.Homepage;
                    this.tbRemarks.Text = this.Organization.Remarks;
                }
            }
        }

        private void Apply()
        {
            if (this.tbName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("机构名称必须输入", "提示", MessageBoxButton.OK);
                this.tcMain.SelectedIndex = 0;
                this.tbName.Focus();
                return;
            }

            DataPortal<Organization> dp = new DataPortal<Explorer.Entities.Organization>();
            IQueryable v = null;
            Organization organ = this.Organization;
            if (organ == null)
            {
                v  = new DataEntityQuery<Organization>().Where(p => p.Name == this.tbName.Text.Trim());                
            }
            else
            {
                if (this.tbName.Text.Trim() != organ.Name)
                {
                    v = new DataEntityQuery<Organization>().Where(p => p.Name == this.tbName.Text.Trim());
                }
            }

            if (v != null)
            {
                dp.BeginExecuteCountQuery(v).Completed += (s, e2) =>
                    {
                        QueryTask<Organization> task = s as QueryTask<Organization>;
                        int count = task.Count;
                        if (count > 0)
                        {
                            this.tbName.Focus();
                            MessageBox.Show(string.Format("已经存在名称为{0}的组织机构!", this.tbName.Text), "操作提示", MessageBoxButton.OK);
                            return;
                        }
                        else
                        {
                            this.Save();
                        }
                    };
            }
            else
            {
                this.Save();
            }
        }

        private void Save()
        {
            Cursor c = this.Cursor;
            this.Cursor = Cursors.Wait;
            this.btnOK.IsEnabled = false;

            Organization vItem = this.Organization;
            if (this.Organization == null)
            {
                vItem = new Organization();
                vItem.Guid = Guid.NewGuid().ToString().ToUpper();
                vItem.ParentID = this.ParentOrgan.Guid;
                vItem.DataID = 0;
                vItem.Name = this.tbName.Text;
                vItem.Explain = this.tbExplain.Text;
                vItem.Module = Guid.Empty.ToString();
            }

            vItem.Name = this.tbName.Text;
            vItem.Explain = this.tbExplain.Text;
            //vItem.Module = this.tbModule.Tag.ToString();
            vItem.Address = this.tbAddress.Text;
            vItem.Contact = this.tbContact.Text;
            vItem.Tel = this.tbTel.Text;
            vItem.Fax = this.tbFax.Text;
            vItem.Email = this.tbEMail.Text;
            vItem.Homepage = this.tbHomePage.Text;
            vItem.Remarks = this.tbRemarks.Text;

            DataPortal<Organization> dp = new DataPortal<Organization>();
            InvokeTask task = null;
            if (this.Organization == null)
            {
                task = dp.BeginInsert(vItem);
            }
            else
            {
                task = dp.BeginUpdate(vItem);
            }

            task.Completed += (s, e) =>
            {
                this.Cursor = c;
                this.btnOK.IsEnabled = true;
                if (task.Error != null)
                {
                    MessageBox.Show("完成业务处理时发生错误：" + task.Error.Message, "错误", MessageBoxButton.OK);
                    return;
                }
                else
                {
                    this.DataEntity = vItem;
                    this.DialogResult = true;
                    //this.Close();
                }
            };
        }

        private void tbDescription_TextChanged(object sender, System.EventArgs e)
        {
            this.btnOK.IsEnabled = true;
        }

        private void tbInput_TextChanged(object sender, System.EventArgs e)
        {
            this.btnOK.IsEnabled = true;
        }        
    }
}
