using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using EAS.Modularization;
using EAS.BPM.Entities;
using System.Windows;
using System.Windows.Controls;
using EAS.Workflow;
using EAS.Data;

namespace EAS.BPM.SilverlightUI
{
    class WfAddInHelper
    {
        /// <summary>
        /// 工作流实例回调。
        /// </summary>
        public delegate void WorkflowInstanceCallBack();

        /// <summary>
        /// 发起流程。
        /// </summary>
        /// <param name="wfDefine"></param>
        public static void StartWorkflowInstance(WFDefine wfDefine, WorkflowInstanceCallBack callBack)
        {
            System.Type T = EAS.Objects.ClassProvider.GetType(wfDefine.SilverMType);

            if (T == null)
            {
                MessageBox.Show(string.Format("没有找到名称为\"{0}\"的功能模块", wfDefine.Module), "提示", MessageBoxButton.OK);
                return;
            }

            if (EAS.Application.Instance != null)
            {
                if (typeof(Control).IsAssignableFrom(T))
                {
                    object wfAddIn = System.Activator.CreateInstance(T);
                    WfInstanceWindow wfInstanceForm = new WfInstanceWindow(wfAddIn, true, Guid.Empty);
                    wfInstanceForm.Closed += (s, e) => { callBack(); };
                    wfInstanceForm.Show();
                }
                else if (typeof(ChildWindow).IsAssignableFrom(T))
                {
                    ChildWindow wfAddIn = System.Activator.CreateInstance(T) as ChildWindow;
                    wfAddIn.Closed += (s, e) => { callBack(); };
                    WfAddInHelper.Start(wfAddIn);
                }
                else
                {
                    WfAddInHelper.Start(System.Activator.CreateInstance(T));
                }
            }
        }

        /// <summary>
        /// 加载流程。
        /// </summary>
        /// <param name="wfDefine"></param>
        public static void LoadWorkflowInstance(WFInstance wfInstance, bool isEnabled, WorkflowInstanceCallBack callBack)
        {
            DataPortal<WFDefine> dataPortal = new DataPortal<WFDefine>();

            WFDefine wfDefine = new WFDefine();
            wfDefine.FlowID = wfInstance.FlowID;
            
            dataPortal.BeginRead(wfDefine).Completed += (s, e)
                =>
                {
                    QueryTask<WFDefine> task = s as QueryTask<WFDefine>;
                    if (task.Error != null)
                    {
                        MessageBox.Show(string.Format("读取流程定义错误:" + task.Error.Message, wfDefine.Module), "提示", MessageBoxButton.OK);
                    }
                    else  //
                    {
                        wfDefine = task.DataEntity;

                        System.Type T = EAS.Objects.ClassProvider.GetType(wfDefine.SilverMType);

                        if (T == null)
                        {
                            MessageBox.Show(string.Format("没有找到名称为\"{0}\"的功能模块", wfDefine.Module), "提示", MessageBoxButton.OK);
                            return;
                        }

                        if (EAS.Application.Instance != null)
                        {
                            if (typeof(Control).IsAssignableFrom(T))
                            {
                                object wfAddIn = System.Activator.CreateInstance(T);
                                WfInstanceWindow wfInstanceForm = new WfInstanceWindow(wfAddIn, isEnabled, new Guid(wfInstance.ID));
                                wfInstanceForm.Closed += (s2, e2) => { callBack(); };
                                wfInstanceForm.Show();
                            }
                            else if (typeof(ChildWindow).IsAssignableFrom(T))
                            {
                                ChildWindow wfAddIn = System.Activator.CreateInstance(T) as ChildWindow;
                                wfAddIn.Closed += (s2, e2) => { callBack(); };
                                WfAddInHelper.LoadWorkflowInstance(wfAddIn, isEnabled, new Guid(wfInstance.ID));
                            }
                            else
                            {
                                WfAddInHelper.LoadWorkflowInstance(System.Activator.CreateInstance(T), isEnabled, new Guid(wfInstance.ID));
                            }
                        }
                    }
                };
        }

        /// <summary>
        /// 运行模块。
        /// </summary>
        /// <param name="addIn"></param>
        public static void Start(object addIn)
        {
            MethodInfo mi = GetRunMethod(addIn);
            if (mi != null)
            {
                mi.Invoke(addIn, new object[] { });
            }
        }

        /// <summary>
        /// 加载模块实例。
        /// </summary>
        /// <param name="wfAddIn"></param>
        /// <param name="instanceID"></param>
        internal static void LoadWorkflowInstance(object wfAddIn, bool isEnabled,Guid instanceID)
        {
            System.Type T = wfAddIn.GetType();
            System.Reflection.PropertyInfo propertyInfo = null;
            foreach (System.Reflection.PropertyInfo item in T.GetProperties())
            {
                WorkflowInstanceIdAttribute wfa = Attribute.GetCustomAttribute(item, typeof(WorkflowInstanceIdAttribute)) as WorkflowInstanceIdAttribute;
                if (!Object.Equals(null, wfa))
                {
                    propertyInfo = item;
                    break;
                }
            }

            if (propertyInfo != null)
                propertyInfo.SetValue(wfAddIn, instanceID, new object[] { });
        }

        #region 内部代码

        internal static string GetModuleName(object module)
        {
            AddInAttribute ma = Attribute.GetCustomAttribute(module.GetType(), typeof(AddInAttribute)) as AddInAttribute;
            if (!Object.Equals(null, ma))
            {
                return ma.Name;
            }
            else
                return string.Empty;
        }

        internal static string GetModuleDescription(object module)
        {
            AddInAttribute ma = Attribute.GetCustomAttribute(module.GetType(), typeof(AddInAttribute)) as AddInAttribute;
            if (!Object.Equals(null, ma))
            {
                return ma.Description;
            }
            else
                return string.Empty;
        }

        internal static MethodInfo GetRunMethod(object module)
        {
            MethodInfo[] mis = module.GetType().GetMethods();

            foreach (MethodInfo mi in mis)
            {
                ModuleStartAttribute ms = Attribute.GetCustomAttribute(mi, typeof(ModuleStartAttribute)) as ModuleStartAttribute;
                if (!Object.Equals(null, ms))
                {
                    return mi;
                }

                AddInStartAttribute mr = Attribute.GetCustomAttribute(mi, typeof(AddInStartAttribute)) as AddInStartAttribute;
                if (!Object.Equals(null, mr))
                {
                    return mi;
                }
            }

            return null;
        }

        #endregion
    }
}
