using System;
using System.IO;
using System.Xml;
using EAS.Modularization;
using EAS.Services;
using System.Configuration;
using EAS.Explorer.BLL;
using EAS.Sessions;
using System.Collections.Generic;
using System.Windows;
using EAS.SilverlightClient.UI;
using EAS.Explorer;
using System.Windows.Controls;
using EAS.Workflow;

namespace EAS.SilverlightClient
{
    /// <summary>
    /// AgileEAS.NET平台Silverlight应用程序。
    /// </summary>
    class Application : EAS.Application
    {
        SessionCollection sessions;
        IWorkflowRuntime m_WorkflowRuntime = null;

        public Application()
        {
            this.sessions = new SessionCollection();
        }

        #region 公共属性

        public  IDictionary<string,ISession> Sessions
        {
            get
            {
                return this.sessions;
            }
        }

        public override ISession Session
        {
            get
            {
                return this.sessions[0];
            }
        }

        /// <summary>
        /// 客户端帐户。
        /// </summary>
        public EAS.Explorer.IAccount Account
        {
            get
            {
                return this.Session.Client as EAS.Explorer.IAccount;
            }
        }        

        /// <summary>
        /// 应用程序的名称。
        /// </summary>
        public override string Name
        {
            get
            {
                return "AgileEAS.NET应用开发平台";
            }
        }

        #endregion

        public override DateTime Time
        {
            get
            {
                return DateTimeClient.CurrentTime;
            }
        }

        public override IWorkflowRuntime WorkflowRuntime
        {
            get
            {
                if (this.m_WorkflowRuntime == null)
                {
                    lock (this)
                    {
                        this.LoadWorkflowRuntime();
                    }
                }

                return this.m_WorkflowRuntime;
            }
        }

        public override void AppStart()
        {
            this.sessions.Clear();
            this.sessions.Add(string.Empty,new Session());
        }

        public override void AppEnd()
        {
            
        }
        
        public override void Login(string organization,string loginID, string password)        
        {
            Session session = this.Session as Session;
            session.Organization = organization;

            EAS.Controls.Window.ShowLoading("正在登录...");

            LoginProxy proxy = new LoginProxy();
            proxy.GetAccountLoginInfo(loginID, password).Completed +=
                (s, e1) =>
                {
                    EAS.Controls.Window.HideLoading();

                    FuncTask task = s as FuncTask;
                    if (task.Error != null)
                    {
                        if (task.Error.InnerException != null)
                            MessageBox.Show(task.Error.InnerException.Message, "未能通过验证", MessageBoxButton.OK);
                        else
                            MessageBox.Show("用户验证过程中出现未知错误，请记录下面的异常信息，并通知您的系统管理员。\n\n" + task.Error.Message, "错误", MessageBoxButton.OK);
                    }
                    else
                    {
                        LoginResult lr = task.Result as LoginResult;

                        if ((!lr.Passed) & (string.Compare(loginID, "Guest", StringComparison.CurrentCultureIgnoreCase) != 0))
                            MessageBox.Show("登录验证时发现未知错误", "未能通过验证", MessageBoxButton.OK);

                        string ip = string.Empty;
                        try
                        {
                            ip = System.Windows.Application.Current.Host.InitParams["ClientIP"];
                        }catch{}

                        InvokeTask task2 = new InvokeTask();
                        ILogService logService = ServiceContainer.GetService<ILogService>(task2);
                        logService.Log(loginID,"登录系统",string.Empty,ip);
                        task2.Completed+=(s2,e2)=>
                            {

                            };

                        session.Account = lr.Account;

                        if (LoginContext.Singleton.IsolatedStorage)
                        {
                            LoginContext.Singleton.Account = lr.Account;
                            App.Navigation(new UpdateLoader());
                        }
                        else
                        {
                            App.StartMain();  //开始主页
                        }
                    }
                };

            int x = 2;
        }

        public override void Logout()
        {
            if (EAS.Application.Instance != null)
                if (EAS.Application.Instance.Session.Client != null)
                {
                    IAccount account = EAS.Application.Instance.Session.Client as IAccount;
                    if (account != null)
                    {
                        account.LoginID = "Guest";
                        account.Name = "访客";
                    }
                }

            UserControl loginPage = PlugContext.LoginForm as UserControl;
            App.Navigation(loginPage);
            this.OnLogouting(); 
        }

        /// <summary>
        /// 修改密码。
        /// </summary>
        public override void ChangePassword()
        {
            PasswordWindow window = new PasswordWindow();
            window.LoginID = SLContext.Account.LoginID;
            window.Show();
        }

        #region 生命周期


        /// <summary>
        /// 登录系统。
        /// </summary>
        public void OnLogined()
        {
           
        }

        /// <summary>
        /// 注销程序。
        /// </summary>
        public void OnLogouting()
        {

        }

        #endregion

        #region 异常处理

        static void Application_Error(object sender, ApplicationUnhandledExceptionEventArgs e)
        {

        }

        #endregion

        #region 打开/关闭模板

        public override void StartModule(Type module)
        {
            object module2 = System.Activator.CreateInstance(module);
            this.StartModule(module2);
        }

        public override void StartModule(Guid module)
        {
            object module2 = ModuleManager.LoadModule(module);
            if (!ModuleManager.DemandPrivileges(module2))
                return;
            this.StartModule(module2);
        }

        public override void StartModule(object module)
        {
            if (!ModuleManager.DemandPrivileges(module))
                return;

            SLContext.Instance.Shell.LoadModule(module);

            //记录日志
            string loginID = this.Account.LoginID;
            string ip = string.Empty;
            string mName = ModuleManager.GetModuleName(module);
            try
            {
                ip = System.Windows.Application.Current.Host.InitParams["ClientIP"];
            }
            catch { }

            InvokeTask task2 = new InvokeTask();
            ILogService logService = ServiceContainer.GetService<ILogService>(task2);
            logService.Log(loginID, string.Format("{0}-打开模块{1}",loginID,mName), string.Empty, ip);
            task2.Completed += (s2, e2) =>
            {

            };
        }

        public override void CloseModule()
        {
            SLContext.Instance.Shell.CloseModule();
        }

        public override void CloseModule(object module)
        {
            if (module != null)
            {
                if (module is System.Windows.Window)
                {
                    (module as System.Windows.Window).Close();
                }
                else if (module is System.Windows.Controls.Control)
                {
                    SLContext.Instance.Shell.CloseModule(module);
                }
            }
        }

        #endregion

        public override void CallScript(string script, IDictionary<string, object> args)
        {
            string x_Script = script.ToLower();

            switch (x_Script)
            {
                case "shownavigation":  //显示菜单
                case "showmenu":
                case "显示菜单":
                case "展开导航":
                    Deployment.Current.Dispatcher.BeginInvoke(delegate
                    {
                        SLContext.Instance.Shell.SwitchNavigation(true);
                    });
                    break;
                case "hidenavigation":  //隐藏菜单
                case "hidemenu":
                case "隐藏菜单":
                case "收起导航":
                    Deployment.Current.Dispatcher.BeginInvoke(delegate
                    {
                        SLContext.Instance.Shell.SwitchNavigation(false);
                    });
                    break;
                case "message":
                case "消息":
                    {
                        foreach (var KV in args)
                        {
                            this.OnNotify(new NotifyEventArgs(KV.Key,KV.Value));
                        } 
                    }
                    break;
                default:
                    break;
            }
        }

        internal void OnStarted2(EventArgs e)
        {
            base.OnStarted(e);
        }

        void LoadWorkflowRuntime()
        {
            this.m_WorkflowRuntime = EAS.Objects.ClassProvider.GetObjectInstance("EAS.BPM.SilverlightUI", "EAS.BPM.SilverlightUI.WorkflowRuntime") as IWorkflowRuntime;
        }
    }
}
