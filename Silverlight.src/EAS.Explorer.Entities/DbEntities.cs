using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EAS.Data;
using EAS.Data.Access;
using EAS.Data.ORM;
using EAS.Data.Linq;
using EAS.BPM.Entities;

namespace EAS.Explorer.Entities
{
    /// <summary>
    /// 数据上下文。
    /// </summary>
    public class DbEntities : DataContext
    {
        #region 字段定义

        private DataEntityQuery<Account> m_Accounts;
        private DataEntityQuery<Role> m_Roles;
        private DataEntityQuery<Module> m_Modules;
        private DataEntityQuery<NavigateGroup> m_NavigateGroups;
        private DataEntityQuery<NavigateModule> m_NavigateModules;
        private DataEntityQuery<ModuleGroup> m_ModuleGroups;
        private DataEntityQuery<Organization> m_Organizations;
        private DataEntityQuery<AccountGrouping> m_AccountGroupings;
        private DataEntityQuery<AppSetting> m_AppSettings;
        private DataEntityQuery<Certificate> m_Certificates;
        private DataEntityQuery<Bug> m_Bugs;
        private DataEntityQuery<Report> m_Reports;
        private DataEntityQuery<Package> m_Packages;
        private DataEntityQuery<Log> m_Logs;
        private DataEntityQuery<ErrorLog> m_ErrorLogs;
        private DataEntityQuery<Message> m_Messages;
        private DataEntityQuery<WFDefine> m_WFDefines;
        private DataEntityQuery<WFInstance> m_WFInstances;
        private DataEntityQuery<WFInstanceState> m_WFInstanceState;
        private DataEntityQuery<WFState> m_WFStates;
        private DataEntityQuery<WFExecute> m_WFExecutes;
        private DataEntityQuery<DesktopItem> m_DesktopItems;
        private DataEntityQuery<ACL> m_ACLs;
        private DataEntityQuery<InputDict> m_InputDicts;
        private DataEntityQuery<Variable> m_Variables;

        #endregion

        #region 构造函数

        /// <summary>
        /// 初始化DbEntities对象实例。
        /// </summary>
        public DbEntities()
        {

        }

        /// <summary>
        /// 初始化DbEntities对象实例。
        /// </summary>
        /// <param name="dbProvider">数据库访问程序提供者。</param>
        public DbEntities(IDbProvider dbProvider)
            : base(dbProvider)
        {

        }

        /// <summary>
        /// 初始化DbEntities对象实例。
        /// </summary>
        /// <param name="dataAccessor">数据访问器。</param>
        public DbEntities(IDataAccessor dataAccessor)
            : base(dataAccessor)
        {

        }

        /// <summary>
        /// 初始化DbEntities对象实例。
        /// </summary>
        /// <param name="dataAccessor">数据访问器。</param>
        /// <param name="ormAccessor">Orm访问器。</param>
        public DbEntities(IDataAccessor dataAccessor, IOrmAccessor ormAccessor)
            : base(dataAccessor, ormAccessor)
        {

        }

        #endregion

        #region 查询定义

        /// <summary>
        /// 组织机构。
        /// </summary>
        public DataEntityQuery<Organization> Organizations
        {
            get
            {
                if ((this.m_Organizations == null))
                {
                    this.m_Organizations = base.CreateQuery<Organization>();
                }
                return this.m_Organizations;
            }
        }

        /// <summary>
        /// 系统账户。
        /// </summary>
        public DataEntityQuery<Account> Accounts
        {
            get
            {
                if ((this.m_Accounts == null))
                {
                    this.m_Accounts = base.CreateQuery<Account>();
                }
                return this.m_Accounts;
            }
        }

        /// <summary>
        /// 角色信息。
        /// </summary>
        public DataEntityQuery<Role> Roles
        {
            get
            {
                if ((this.m_Roles == null))
                {
                    this.m_Roles = base.CreateQuery<Role>();
                }
                return this.m_Roles;
            }
        }

        /// <summary>
        /// 账号=>分组。
        /// </summary>
        public DataEntityQuery<AccountGrouping> AccountGroupings
        {
            get
            {
                if ((this.m_AccountGroupings == null))
                {
                    this.m_AccountGroupings = base.CreateQuery<AccountGrouping>();
                }
                return this.m_AccountGroupings;
            }
        }

        /// <summary>
        /// 程序包。
        /// </summary>
        public DataEntityQuery<Package> Packages
        {
            get
            {
                if ((this.m_Packages == null))
                {
                    this.m_Packages = base.CreateQuery<Package>();
                }
                return this.m_Packages;
            }
        }

        /// <summary>
        /// 模块信息。
        /// </summary>
        public DataEntityQuery<Module> Modules
        {
            get
            {
                if ((this.m_Modules == null))
                {
                    this.m_Modules = base.CreateQuery<Module>();
                }
                return this.m_Modules;
            }
        }

        /// <summary>
        /// 导航分组。
        /// </summary>
        public DataEntityQuery<NavigateGroup> Groups
        {
            get
            {
                if ((this.m_NavigateGroups == null))
                {
                    this.m_NavigateGroups = base.CreateQuery<NavigateGroup>();
                }
                return this.m_NavigateGroups;
            }
        }

        /// <summary>
        /// 导航模块。
        /// </summary>
        public DataEntityQuery<NavigateModule> NavigateModules
        {
            get
            {
                if ((this.m_NavigateModules == null))
                {
                    this.m_NavigateModules = base.CreateQuery<NavigateModule>();
                }
                return this.m_NavigateModules;
            }
        }

        /// <summary>
        /// 模块分组。
        /// </summary>
        public DataEntityQuery<ModuleGroup> ModuleGroups
        {
            get
            {
                if ((this.m_ModuleGroups == null))
                {
                    this.m_ModuleGroups = base.CreateQuery<ModuleGroup>();
                }
                return this.m_ModuleGroups;
            }
        }

        /// <summary>
        /// ACL。
        /// </summary>
        public DataEntityQuery<ACL> ACLs
        {
            get
            {
                if ((this.m_ACLs == null))
                {
                    this.m_ACLs = base.CreateQuery<ACL>();
                }
                return this.m_ACLs;
            }
        }

        /// <summary>
        /// 系统参数。
        /// </summary>
        public DataEntityQuery<AppSetting> AppSettings
        {
            get
            {
                if ((this.m_AppSettings == null))
                {
                    this.m_AppSettings = base.CreateQuery<AppSetting>();
                }
                return this.m_AppSettings;
            }
        }

        /// <summary>
        /// 证书记录。
        /// </summary>
        public DataEntityQuery<Certificate> Certificates
        {
            get
            {
                if ((this.m_Certificates == null))
                {
                    this.m_Certificates = base.CreateQuery<Certificate>();
                }
                return this.m_Certificates;
            }
        }

        /// <summary>
        /// Bug记录。
        /// </summary>
        public DataEntityQuery<Bug> Bugs
        {
            get
            {
                if ((this.m_Bugs == null))
                {
                    this.m_Bugs = base.CreateQuery<Bug>();
                }
                return this.m_Bugs;
            }
        }

        /// <summary>
        /// 报表对象。
        /// </summary>
        public DataEntityQuery<Report> Reports
        {
            get
            {
                if ((this.m_Reports == null))
                {
                    this.m_Reports = base.CreateQuery<Report>();
                }
                return this.m_Reports;
            }
        }

        /// <summary>
        /// 流程定义。
        /// </summary>
        public DataEntityQuery<WFDefine> WFDefines
        {
            get
            {
                if ((this.m_WFDefines == null))
                {
                    this.m_WFDefines = base.CreateQuery<WFDefine>();
                }
                return this.m_WFDefines;
            }
        }

        /// <summary>
        /// 流程实状态。
        /// </summary>
        public DataEntityQuery<WFState> WFStates
        {
            get
            {
                if ((this.m_WFStates == null))
                {
                    this.m_WFStates = base.CreateQuery<WFState>();
                }
                return this.m_WFStates;
            }
        }

        /// <summary>
        /// 流程实例。
        /// </summary>
        public DataEntityQuery<WFInstance> WFInstances
        {
            get
            {
                if ((this.m_WFInstances == null))
                {
                    this.m_WFInstances = base.CreateQuery<WFInstance>();
                }
                return this.m_WFInstances;
            }
        }

        /// <summary>
        /// 流程执行信息。
        /// </summary>
        public DataEntityQuery<WFExecute> WFExecutes
        {
            get
            {
                if ((this.m_WFExecutes == null))
                {
                    this.m_WFExecutes = base.CreateQuery<WFExecute>();
                }
                return this.m_WFExecutes;
            }
        }

        /// <summary>
        /// 流程实例状态/持久化。
        /// </summary>
        public DataEntityQuery<WFInstanceState> WFInstanceStates
        {
            get
            {
                if ((this.m_WFInstanceState == null))
                {
                    this.m_WFInstanceState = base.CreateQuery<WFInstanceState>();
                }
                return this.m_WFInstanceState;
            }
        }

        /// <summary>
        /// 离线消息。
        /// </summary>
        public DataEntityQuery<Message> Messages
        {
            get
            {
                if ((this.m_Messages == null))
                {
                    this.m_Messages = base.CreateQuery<Message>();
                }
                return this.m_Messages;
            }
        }

        /// <summary>
        /// 操作日志。
        /// </summary>
        public DataEntityQuery<Log> Logs
        {
            get
            {
                if ((this.m_Logs == null))
                {
                    this.m_Logs = base.CreateQuery<Log>();
                }
                return this.m_Logs;
            }
        }

        /// <summary>
        /// 系统日志。
        /// </summary>
        public DataEntityQuery<ErrorLog> ErrorLogs
        {
            get
            {
                if ((this.m_ErrorLogs == null))
                {
                    this.m_ErrorLogs = base.CreateQuery<ErrorLog>();
                }
                return this.m_ErrorLogs;
            }
        }

        /// <summary>
        /// 桌面定义。
        /// </summary>
        public DataEntityQuery<DesktopItem> DesktopItems
        {
            get
            {
                if ((this.m_DesktopItems == null))
                {
                    this.m_DesktopItems = base.CreateQuery<DesktopItem>();
                }
                return this.m_DesktopItems;
            }
        }

        /// <summary>
        /// 桌面定义。
        /// </summary>
        public DataEntityQuery<InputDict> InputDicts
        {
            get
            {
                if ((this.m_InputDicts == null))
                {
                    this.m_InputDicts = base.CreateQuery<InputDict>();
                }
                return this.m_InputDicts;
            }
        }

        /// <summary>
        /// 内部变量定义。
        /// </summary>
        public DataEntityQuery<Variable> Variables
        {
            get
            {
                if (this.m_Variables == null)
                {
                    this.m_Variables = base.CreateQuery<Variable>();
                }
                return this.m_Variables;
            }
        }

        #endregion
    }
}
