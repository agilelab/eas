using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EAS.Explorer.BLL;
using EAS.Services;
using System.Linq;
using FineUI;
using System.Web.Security;
using Newtonsoft.Json.Linq;

namespace EAS.WebShell
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //工具栏上的帮助菜单
            JArray ja = JArray.Parse(XContext.Instance.HelpMenus);
            foreach (JObject jo in ja)
            {
                MenuButton menuItem = new MenuButton();
                menuItem.EnablePostBack = false;
                menuItem.Text = jo.Value<string>("Text");
                menuItem.Icon = IconHelper.String2Icon(jo.Value<string>("Icon"), true);
                menuItem.OnClientClick = String.Format("addExampleTab('{0}','{1}','{2}')", jo.Value<string>("ID"), ResolveUrl(jo.Value<string>("URL")), jo.Value<string>("Text"));
                this.btnHelp.Menu.Items.Add(menuItem);
            }

            #region 样式主题

            MenuButton themeItem = new MenuButton();
            themeItem.EnablePostBack = true;
                themeItem.Text = "Neptune";
                //themeItem.Icon = IconHelper.String2Icon(jo.Value<string>("Icon"), true);
            themeItem.Click+=(s,e2)=>
                {
                    this.ChangeTheme("neptune");
                };
            this.btnTheme.Menu.Items.Add(themeItem);

            themeItem = new MenuButton();
            themeItem.EnablePostBack = true;
                themeItem.Text = "Blue";
                //themeItem.Icon = IconHelper.String2Icon(jo.Value<string>("Icon"), true);
            themeItem.Click+=(s,e2)=>
                {
                    this.ChangeTheme("blue");
                };
            this.btnTheme.Menu.Items.Add(themeItem);

            themeItem = new MenuButton();
            themeItem.EnablePostBack = true;
                themeItem.Text = "Gray";
                //themeItem.Icon = IconHelper.String2Icon(jo.Value<string>("Icon"), true);
            themeItem.Click+=(s,e2)=>
                {
                    this.ChangeTheme("gray");
                };
            this.btnTheme.Menu.Items.Add(themeItem);

            themeItem = new MenuButton();
            themeItem.EnablePostBack = true;
                themeItem.Text = "Access";
                //themeItem.Icon = IconHelper.String2Icon(jo.Value<string>("Icon"), true);
            themeItem.Click+=(s,e2)=>
                {
                    this.ChangeTheme("access");
                };
            this.btnTheme.Menu.Items.Add(themeItem);

            #endregion
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.Compare(XContext.Account.LoginID, "Guest", true) == 0)
                {
                    FormsAuthentication.SignOut();
                    FormsAuthentication.RedirectToLoginPage();
                }

                //标题。
                System.Web.UI.WebControls.HyperLink link = regionTop.FindControl("linkSystemTitle") as System.Web.UI.WebControls.HyperLink;
                if (link != null)
                    link.Text = XContext.Instance.ProductName;

                #region 处理导航

                //注册客户端脚本，服务器端控件ID和客户端ID的映射关系
                JObject ids = GetClientIDS(this.regionPanel, regionTop, mainTabStrip, txtUser,
                    txtCurrentTime, btnRefresh);
                ids.Add("userName", XContext.Account.Name);
                ids.Add("userIP", GetUserIP());
                ids.Add("onlineUserCount", XContext.Instance.GetOnlineCount());

                if (XContext.Instance.MenuType == "accordion")
                {
                    Accordion accordionMenu = this.InitAccordionMenu();
                    ids.Add("treeMenu", accordionMenu.ClientID);
                    ids.Add("menuType", "accordion");
                }
                else
                {
                    Tree treeMenu = InitTreeMenu();
                    ids.Add("treeMenu", treeMenu.ClientID);
                    ids.Add("menuType", "menu");
                }

                string idsScriptStr = String.Format("window.DATA={0};", ids.ToString(Newtonsoft.Json.Formatting.None));
                PageContext.RegisterStartupScript(idsScriptStr);

                #endregion
            }
        }

        private JObject GetClientIDS(params ControlBase[] ctrls)
        {
            JObject jo = new JObject();
            foreach (ControlBase ctrl in ctrls)
            {
                jo.Add(ctrl.ID, ctrl.ClientID);
            }

            return jo;
        }

        string GetUserIP()
        {
            HttpRequest request = HttpContext.Current.Request;
            string result = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result))
            {
                result = request.ServerVariables["REMOTE_ADDR"];
            }
            if (string.IsNullOrEmpty(result))
            {
                result = request.UserHostAddress;
            }
            if (string.IsNullOrEmpty(result))
            {
                result = "0.0.0.0";
            }

            return result;
        }

        /// <summary>
        /// 样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ChangeTheme(string theme)
        {
            HttpCookie themeCookie = new HttpCookie("EAS_Theme_v5", theme);
            themeCookie.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(themeCookie);
            PageContext.Refresh();
        }

        #region InitMenu。

        #region InitTreeMenu

        /// <summary>
        /// 创建树菜单
        /// </summary>
        /// <param name="menus"></param>
        /// <returns></returns>
        private Tree InitTreeMenu()
        {
            //取导航。
            NavigationProxy.Instance
                .GetNavigation(XContext.Account.LoginID);

            //生成树。
            Tree treeMenu = new Tree();
            treeMenu.ID = "treeMenu";
            treeMenu.ShowBorder = false;
            treeMenu.ShowHeader = false;
            treeMenu.EnableIcons = true;
            treeMenu.EnableArrows = true;
            treeMenu.AutoScroll = true;            

            regionLeft.Items.Add(treeMenu);

            // 生成树
            this.ResolveMenuTree(string.Empty, treeMenu.Nodes);

            // 展开第一个树节点
            if (treeMenu.Nodes.Count > 0)
            {                
                treeMenu.Nodes[0].Expanded = true;
            }

            return treeMenu;
        }

        #endregion

        #region InitAccordionMenu

        /// <summary>
        /// 创建手风琴菜单
        /// </summary>
        /// <param name="menus"></param>
        /// <returns></returns>
        private Accordion InitAccordionMenu()
        {
            //取导航。
            NavigationProxy.Instance
                .GetNavigation(XContext.Account.LoginID);

            //生成手风琴。
            Accordion accordionMenu = new Accordion();
            accordionMenu.ID = "accordionMenu";
            accordionMenu.EnableFill = true;
            accordionMenu.ShowBorder = false;
            accordionMenu.ShowHeader = false;
            regionLeft.Items.Add(accordionMenu);

            //生成顶级菜单。
            var vRoots = NavigationProxy.Instance
                .GetGroupList(string.Empty)
                .Where(p => (p.Attributes & 0x0008) == 0x0008).ToList();

            foreach (var vGroup in vRoots)
            {
                AccordionPane accordionPane = new AccordionPane();
                accordionPane.Title = vGroup.Name;
                accordionPane.Layout = Layout.Fit;
                accordionPane.ShowBorder = false;
                accordionPane.BodyPadding = "2px 0 0 0";

                //子树。
                Tree innerTree = new Tree();
                innerTree.EnableArrows = true;
                innerTree.ShowBorder = false;
                innerTree.ShowHeader = false;
                innerTree.EnableIcons = true;
                innerTree.AutoScroll = true;

                // 生成树
                this.ResolveMenuTree(vGroup.ID, innerTree.Nodes);

                //
                accordionPane.Items.Add(innerTree);
                accordionMenu.Items.Add(accordionPane);

            }

            return accordionMenu;
        }

        #endregion

        /// <summary>
        /// 生成菜单树。
        /// </summary>
        /// <param name="dtMenus"></param>
        /// <param name="parentID"></param>
        /// <param name="nodes"></param>
        void ResolveMenuTree(string parentID, FineUI.TreeNodeCollection nodes)
        {
            //分组。
            var groupList = NavigationProxy.Instance
                .GetGroupList(parentID)
                .Where(p => (p.Attributes & 0x0008) == 0x0008).ToList();
            foreach (var vGroup in groupList)
            {
                FineUI.TreeNode node = new FineUI.TreeNode();
                nodes.Add(node);
                node.Expanded = true;
                node.ToolTip = vGroup.Description;
                node.Text = vGroup.Name;
                node.EnableClickEvent = true;
                //node.IconUrl = "~/res/icon/folder.png";
                this.ResolveMenuTree(vGroup.ID, node.Nodes);
            }
            
            //功能
            var moduleList = NavigationProxy.Instance.GeModuleList(parentID);
            foreach (var vModule in moduleList)
            {
                FineUI.TreeNode node = new FineUI.TreeNode();
                nodes.Add(node);
                node.Expanded = true;
                node.ToolTip = vModule.Description;
                node.Text = vModule.Name;
                node.IconUrl = "~/res/icon/tag_blue.png";
                if (!String.IsNullOrEmpty(vModule.Url))
                {
                    node.EnableClickEvent = false;
                    node.NavigateUrl = ResolveUrl(vModule.Url);
                }
            }
        }

        #endregion 

        protected void btnExit_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}