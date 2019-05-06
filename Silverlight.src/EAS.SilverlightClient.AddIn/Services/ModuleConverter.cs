using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using EAS.Explorer.Entities;
using System.Collections;
using EAS.Data.Linq;

using System.Linq;
using EAS.Data;

namespace EAS.SilverlightClient.AddIn
{
    public class ModuleConverter
    {
        public static void LoadAllPermission(DataGrid dataGrid, IList<ACL> permissionList, IList<ACLEx> allPermissionList)
        {
            IList<string> sList = new List<string>(permissionList.Count);

            foreach (var item in permissionList)
            {
                sList.Add(item.PObject.ToUpper());
            }

#if DEBUG
            //MessageBox.Show("N:" + sList.Count.ToString());
#endif
            DataEntityQuery<Module> query = new DataEntityQuery<Module>();
            var v = from c in query
                    where sList.Contains(c.Guid)
                    select new EAS.Explorer.Entities.Module
                    {
                        Guid = c.Guid,
                        Name = c.Name,
                        Description = c.Description
                    };

#if DEBUG
            //MessageBox.Show("2");
#endif

            DataPortal<Module> dp = new DataPortal<Module>();
            dp.BeginExecuteQuery(v).Completed +=
                (s, e2) =>
                {
                    QueryTask<Module> task = s as QueryTask<Module>;
                    if (task.Error != null)
                    {
                        MessageBox.Show("读取模块数据时出错：" + task.Error.Message, "错误", MessageBoxButton.OK);
                        return;
                    }
                    else
                    {
#if DEBUG
                        //MessageBox.Show("3:");
#endif

                        IList<Module> modules = task.Entities;

#if DEBUG
                        //MessageBox.Show("X2:" + modules.Count.ToString());
#endif

                        foreach (var item in permissionList)
                        {
                            ACLEx permission = new ACLEx();
                            permission.Changed = item.Changed;
                            permission.PObject = item.PObject;
                            permission.Privileger = item.Privileger;
                            permission.PType = item.PType;
                            permission.PValue = item.PValue;
                            permission.MInfo = modules.Where(p => p.Guid.ToUpper() == item.PObject.ToUpper()).FirstOrDefault();
                            allPermissionList.Add(permission);
                        }

                        dataGrid.ItemsSource = allPermissionList;

#if DEBUG
                        //MessageBox.Show("4:");
#endif
                        if (allPermissionList.Count > 0)
                        {
                            dataGrid.SelectedItem = allPermissionList[0];
                        }
                    }
                };
        }
    }
}
