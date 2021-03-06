﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EAS.Data.Linq;
using EAS.Explorer.Entities;
using FineUI;
using System.Text;
using System.IO;
using System.Reflection;
using EAS.Modularization;
using EAS.WebShell.Utils;
using EAS.Explorer.BLL;
using EAS.Services;

namespace EAS.WebShell.Biz
{
    public partial class BusinessInstaller : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnOK.OnClientClick = this.Grid2.GetNoSelectionAlertInTopReference("请至少选中一个模块！");
                this.LoadAssemblys();
                if (this.cbxFiles.Items.Count > 0)
                {
                    this.LoadModules();
                }
            }
        }

        void LoadAssemblys()
        {
            string[] files = Directory.GetFiles(Server.MapPath("~/bin/"), "*.dll", SearchOption.TopDirectoryOnly);
            var vFiles = files.Where(p => this.CanLoadAddIn(p)).Select(p => Path.GetFileName(p)).ToList();

            this.cbxFiles.DataSource = vFiles;
            this.cbxFiles.DataBind();
        }

        bool CanLoadAddIn(string file)
        {
            try
            {
                var assemblys = System.AppDomain.CurrentDomain.GetAssemblies();
                var assembly = Assembly.LoadFile(file);
                var types = assembly.GetTypes();
                var count = types.Where(p => p.IsPublic && this.GetBizCount(p)>0).Count();
                return count > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 求一个类里面的Biz构件数量.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        int GetBizCount(System.Type type)
        {
            return type.GetMethods()
                .Where(x => Attribute.GetCustomAttribute(x, typeof(ServiceMethodAttribute)) != null)
                .Count();
        }

        private void LoadModules()
        {
            string file = this.cbxFiles.SelectedValue;
            file = Path.Combine(Server.MapPath("~/bin/"), file);

            var assembly = Assembly.LoadFile(file);
            var types = assembly.GetTypes().Where(p => p.IsPublic && this.GetBizCount(p) > 0).ToList();

            var vList = new List<EAS.Explorer.Entities.Module>();
            foreach (var item in types)
            {
                var xList = item.GetMethods().Where(x => Attribute.GetCustomAttribute(x, typeof(ServiceMethodAttribute)) != null)
                    .Select(p => new { Type = item,Method =p, M = Attribute.GetCustomAttribute(p, typeof(ServiceMethodAttribute)) as ServiceMethodAttribute })
                    .Select(p => new EAS.Explorer.Entities.Module
                {
                    Guid = p.M.Guid.ToUpper(),
                    Name = p.M.Name,
                    Description = p.M.Description,
                    Type = MetaHelper.GetTypeString(p.Type),
                    Assembly = MetaHelper.GetAssemblyString(p.Type),
                    Method = p.Method.Name,
                    Developer = MetaHelper.GetDeveloperString(p.Type),
                    Version = MetaHelper.GetVersionString(p.Type),
                    SortCode = 0,
                    LMTime = DateTime.Now
                })
                .ToList();

                vList.AddRange(xList);
            }

            string sortField = this.Grid2.SortField;
            string sortDirection = this.Grid2.SortDirection;
            if (!string.IsNullOrEmpty(sortField))
                vList = vList.AsQueryable().DynamicSorting(sortField, sortDirection).ToList();

            this.Grid2.DataSource = vList;
            this.Grid2.DataBind();
        }
        
        private void SaveData()
        {
            IModuleService service = ServiceContainer.GetService<IModuleService>();
            int[] selections = this.Grid2.SelectedRowIndexArray;
            try
            {
                foreach (int rowIndex in selections)
                {
                    var vType = System.Type.GetType(string.Format("{0},{1}", this.Grid2.DataKeys[rowIndex][0].ToString(), this.Grid2.DataKeys[rowIndex][1].ToString()));
                    var vMethod = vType.GetMethod(this.Grid2.DataKeys[rowIndex][2].ToString());
                    var vM = Attribute.GetCustomAttribute(vMethod, typeof(ServiceMethodAttribute)) as ServiceMethodAttribute;
                    var vModule = new EAS.Explorer.Entities.Module
                    {
                        Guid = vM.Guid.ToUpper(),
                        Name = vM.Name,
                        Description = vM.Description,
                        Type = MetaHelper.GetTypeString(vType),
                        Assembly = MetaHelper.GetAssemblyString(vType),
                        Method = vMethod.Name,
                        Developer = MetaHelper.GetDeveloperString(vType),
                        Version = MetaHelper.GetVersionString(vType),
                        SortCode = 0,
                        Attributes = (int)EAS.Explorer.GoComType.Business,
                        LMTime = DateTime.Now
                    };
                    service.InstallModule(vModule);
                }
            }
            catch(System.Exception exc)
            {
                Alert.Show(exc.Message, MessageBoxIcon.Error);
            }
        }

        protected void Grid2_Sort(object sender, FineUI.GridSortEventArgs e)
        {
            this.Grid2.SortDirection = e.SortDirection;
            this.Grid2.SortField = e.SortField;
            this.LoadModules();
        }

        protected void btnRload_Click(object sender, EventArgs e)
        {
            this.LoadModules();
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            this.SaveData();
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }        

        protected void btnClose_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(ActiveWindow.GetHideReference());
        }
    }
}