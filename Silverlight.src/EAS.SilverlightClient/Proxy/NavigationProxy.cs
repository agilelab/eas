using System;
using System.Collections.Generic;
using System.Text;
using EAS.Explorer.Entities;
using EAS.Explorer.BLL;
using EAS.Services;

namespace EAS.SilverlightClient
{
    class NavigationProxy
    {
        #region 单例模式

        static NavigationProxy instance = new NavigationProxy();
        static readonly object _lock = new object();        

        /// <summary>
        /// 单例。
        /// </summary>
        public static NavigationProxy Instance
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new NavigationProxy();
                    }
                }

                return instance;
            }
        }

        NavigationProxy()
        {

        }

        #endregion       

        internal IList<NavigateGroup> GroupList { get; set; }
        internal IList<NavigateModule> ModuleList  { get; set; }

        public FuncTask<NavigationResult> BeginGetNavigation(string loginID)
        {
            FuncTask<NavigationResult> task = new FuncTask<NavigationResult>();
            INavigationService service = ServiceContainer.GetService<INavigationService>(task);
            service.GetNavigation(loginID);
            return task;
        }

        public IList<NavigateGroup> GetGroupList(string loginID)
        {
            Dictionary<string, int> gms = new Dictionary<string, int>();
            foreach (NavigateGroup var in this.GroupList)
                gms.Add(var.ID, 0);

            foreach (NavigateModule var in this.ModuleList)
            {
                gms[var.GroupID] += 1;
            }

            IList<NavigateGroup> List = new List<NavigateGroup>();
            foreach (NavigateGroup var in this.GroupList)
            {
                if (gms[var.ID] > 0)
                    List.Add(var);
            }

            return List;
        }

        public IList<NavigateGroup> GetGroupList(string loginID,string  parentID)
        {
            IList<NavigateGroup> List = new List<NavigateGroup>();
            foreach (NavigateGroup var in this.GroupList)
            {
                if (var.ParentID == parentID)
                    List.Add(var);
            }

            return List;
        }

        public IList<NavigateModule> GeModuleList(string groupID)
        {
            IList<NavigateModule> List = new List<NavigateModule>();
            foreach (NavigateModule var in this.ModuleList)
            {
                if (var.GroupID == groupID)
                    List.Add(var);
            }

            return List;
        }
    }
}
