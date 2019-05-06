using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.Design;
using System.ComponentModel;

namespace EAS.Data.Design
{
    internal class ControllerDesigner : ComponentDesigner
    {
        DataUIMapper CurrentMapper
        {
            get { return (DataUIMapper)this.Component; }
        }

        #region Verbs

        public override DesignerVerbCollection Verbs
        {
            get
            {
                DesignerVerb[] verbs = new DesignerVerb[] { 
															  new DesignerVerb(String.Empty, null), 
															  new DesignerVerb("验证UI绑定 ...", new EventHandler(OnVerifyOne)), 
															  new DesignerVerb("编辑UI绑定 ...", new EventHandler(OnEditMappings)) 
														  };
                return new DesignerVerbCollection(verbs);
            }
        }

        void OnEditMappings(object sender, EventArgs e)
        {
            try
            {
                MappingsEditorForm form = new MappingsEditorForm(this);
                form.SetMappings(CurrentMapper.Mappings);

                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    MapperInfoList mappings = form.GetMappings();
                    PropertyDescriptor prop = TypeDescriptor.GetProperties(Component)["Mappings"];
                    prop.SetValue(Component, mappings);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    "错误: " + ex.ToString());
            }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Sets up the design features and hooks to events.
        /// </summary>
        /// <param name="component"></param>
        public override void Initialize(IComponent component)
        {
            //DONE: always call base.Initialize!
            base.Initialize(component);
            IComponentChangeService ccs = (IComponentChangeService)
                GetService(typeof(IComponentChangeService));
            if (ccs != null)
            {
                ccs.ComponentRemoving += new ComponentEventHandler(OnComponentRemoving);
                ccs.ComponentRename += new ComponentRenameEventHandler(OnComponentRename);
            }
        }

        #endregion

        #region Command-related

        /// <summary>
        /// Passes the verification task to the <see cref="IControllerService"/>.
        /// </summary>
        void OnVerifyOne(object sender, EventArgs e)
        {
            //IControllerService svc = (IControllerService)GetService(typeof(IControllerService));
            //svc.VerifyMappings(CurrentMapper);
        }

        #endregion

        #region Component design-time changes tracking

        /// <summary>
        /// Removes associated view mappings whenever a component is removed from the page.
        /// </summary>
        void OnComponentRemoving(object sender, ComponentEventArgs e)
        {
            IReferenceService svc = (IReferenceService)GetService(typeof(IReferenceService));
            string id = svc.GetName(sender);
            if (id != null)
            {
                foreach (MapperInfo mi in CurrentMapper.Mappings)
                {
                    if(mi.ControlID == id)
                    {
                        CurrentMapper.Mappings.Remove(mi);
                        RaiseComponentChanging(
                        TypeDescriptor.GetProperties(CurrentMapper)["System.ComponentModel.Design"]);
                        break;
                    }
                }                
            }
        }

        void OnComponentRename(object sender, ComponentRenameEventArgs e)
        {
            foreach (MapperInfo mi in CurrentMapper.Mappings)
            {
                if (mi.ControlID == e.OldName)
                {
                    MapperInfo info = mi;
                    info.ControlID = e.NewName;
                    //CurrentMapper.Mappings.Remove(e.OldName);
                    //CurrentMapper.Mappings.Add(e.NewName, info);
                    RaiseComponentChanging(TypeDescriptor.GetProperties(CurrentMapper)["Mappings"]);
                    break;
                }
            }
        }

        #endregion
    }
}
