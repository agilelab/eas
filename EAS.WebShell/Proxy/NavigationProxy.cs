using System;
using System.Collections.Generic;
using System.Text;
using EAS.Explorer.Entities;
using EAS.Explorer.BLL;
using EAS.Services;
using EAS.Modularization;
using System.Linq;
using EAS.Explorer;

namespace EAS.WebShell
{
    class NavigationProxy
    {
        #region 单例模型

        private static NavigationProxy instance;
        private static readonly object _lock = new object();

        NavigationProxy()
        {

        }

        public static NavigationProxy Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                            instance = new NavigationProxy();
                    }
                }

                return instance;
            }
        }

        #endregion

        /// <summary>
        /// 查询导航。
        /// </summary>
        /// <param name="loginID"></param>
        public void GetNavigation(string loginID)
        {
            INavigationService service = ServiceContainer.GetService<INavigationService>();
            NavigationResult result = service.GetNavigation(loginID);
            this.GroupList = new List<INavigateGroup>();
            foreach (NavigateGroup item in result.Groups)
            {
                this.GroupList.Add(item);
            }

            this.ModuleList = new List<INavigateModule>();
            foreach (var item in result.Modules)
            {
                this.ModuleList.Add(item);
            }
        }        

        /// <summary>
        /// 导航信息。
        /// </summary>
        public List<INavigateGroup> GroupList
        {
            get;
            set;
        }

        /// <summary>
        /// 模块信息。
        /// </summary>
        public List<INavigateModule> ModuleList
        {
            get;
            set;
        }

        /// <summary>
        /// 查下载分组。
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        public List<NavigateGroup> GetGroupList(string  parentID)
        {
            List<NavigateGroup> List = new List<NavigateGroup>();
            foreach (NavigateGroup var in this.GroupList)
            {
                if (parentID == null)
                {
                    if (var.ParentID == null)
                    {
                        List.Add(var);
                    }
                    else if ((var.ParentID == string.Empty) || (var.ParentID == Guid.Empty.ToString()))
                    {
                        List.Add(var);
                    }
                }
                else if ((parentID == string.Empty) || (parentID == Guid.Empty.ToString()))
                {
                    if (var.ParentID == null)
                    {
                        List.Add(var);
                    }
                    else if ((var.ParentID == string.Empty) || (var.ParentID == Guid.Empty.ToString()))
                    {
                        List.Add(var);
                    }
                }
                else if (var.ParentID == parentID)
                {
                    List.Add(var);
                }
            }

            return List;
        }

        /// <summary>
        /// 查询导航模块。
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public List<NavigateModule> GeModuleList(string groupID)
        {
            var List = new List<NavigateModule>();
            foreach (NavigateModule var in this.ModuleList)
            {
                if (var.GroupID == groupID)
                    List.Add(var);
            }

            return List;
        }
    }
}
