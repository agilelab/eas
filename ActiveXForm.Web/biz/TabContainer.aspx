<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TabContainer.aspx.cs" Inherits="EAS.ActiveXForm.Web.Biz.TabContainer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <style>
        body
        {
            font-size: 12px;
        }
        .
        {
            font-family: arial;
            font-size: 12px;
        }
        h1
        {
            cursor: hand;
            font-size: 16px;
            margin-left: 10px;
            line-height: 10px;
        }
        xmp
        {
            color: green;
            font-size: 12px;
            margin: 0px;
            font-family: courier;
            background-color: #e6e6fa;
            padding: 2px;
        }
        div.hdr
        {
            background-color: lightgrey;
            margin-bottom: 10px;
            padding-left: 10px;
        }
    </style>
    <link rel="STYLESHEET" type="text/css" href="css/dhtmlXTabBar.css">
    <title>无标题页</title>
    <script type="text/javascript" src="js/dhtmlXCommon.js"></script>
    <script type="text/javascript" src="js/dhtmlXTabbar.js"></script>
    <script type="text/javascript" language="javascript">

        //    function ResetTabbarSize()
        //    {
        //         tabbar.setSize(content.clientWidth,content.clientHeight,false);
        //         tabbar.enableAutoSize(true,true);
        //    }
        function AddPage(frameName, caption, nodekey, appcode, module, type) {
            //PageManager.js脚本的AddPage方法

            try {
                //               var tab=this._getTabById(nodekey);
                bln_link = false;
                if (tabbar.setTabActive(nodekey)) {
                }
                else {
                    var len = caption.replace(/[^\x00-\xff]/g, "**").length;
                    len = len * 7 + 35;
                    tabbar.addTab(nodekey, caption, len + 'px', null, 0);
                    if (type == "Web") {
                        tabbar.setContentHref(nodekey, module);
                    }
                    else if (type == "WinForm") {

                        tabbar.setContentHref(nodekey, encodeURI("../MainContainer.aspx?login=" + nodekey + "," + caption + " ,xx,xx," + module + "," + appcode));
                    }
                    tabbar.setTabActive(nodekey);
                    ind++;

                }
            }
            catch (e) {
                alert("增加标签页时错误：" + e);
            }
        }

        function removeTab(id) {
            try {
                var s = tabbar.getContentById(id);
                var r = s.all[0].contentWindow.closeContainer();

                return r;
            }
            catch (e) {
                return true;
            }

        }

        function removeCurrentTab() {
            try {
                var s = tabbar.getActiveTab();
                tabbar.removeTab(s);
                //		        document.parentWindow.top.document.getElementById('main').document.parentWindow.CollapseV(false)
                return true;
            }
            catch (e) {
                return false;
            }

        }

        var temp;
        var bln_link = false;
        function addPage(frameName, caption, nodekey, str_tabLoadType, module, type, b) {
            try {
                bln_link = true;
                temp = b;
                //               if(tabbar.setTabActive(nodekey))
                var id = tabbar.getTabIdLast(nodekey);
                if (id) {

                    if (str_tabLoadType == "New" || str_tabLoadType == "NewAndCloseCurrent") {
                        if (str_tabLoadType == "NewAndCloseCurrent") {
                            tabbar.removeTab(tabbar.getActiveTab(), false);
                        }

                        nodekey = nodekey + "[" + Math.random() + "]";

                        var len = caption.replace(/[^\x00-\xff]/g, "**").length;
                        len = len * 7 + 35;
                        tabbar.addTab(nodekey, caption, len + 'px', null, 0);

                        if (type == "Web") {
                            tabbar.setContentHref(nodekey, module);
                        }
                        else if (type == "WinForm") {

                            tabbar.setContentHref(nodekey, "../MainContainer.aspx?login=x,x,x,x," + module + ",x,Link");
                        }

                        tabbar.setTabActive(nodekey);
                    }
                    else if (str_tabLoadType == "LayOver" || str_tabLoadType == "LayOverAndCloseCurrent") {
                        if (str_tabLoadType == "LayOverAndCloseCurrent") {
                            tabbar.removeTab(tabbar.getActiveTab(), false);
                        }
                        //                        var s = tabbar.getContentById(nodekey);
                        var s = tabbar.getContentById(id);
                        if (type == "Web") {
                            s.all[0].location.reload(module);
                        }
                        else if (type == "WinForm") {
                            s.all[0].contentWindow.location.reload();
                        }
                        tabbar.setTabActive(id);
                    }
                }
                else {
                    if (str_tabLoadType == "NewAndCloseCurrent" || str_tabLoadType == "LayOverAndCloseCurrent") {
                        tabbar.removeTab(tabbar.getActiveTab(), false);
                    }

                    var len = caption.replace(/[^\x00-\xff]/g, "**").length;
                    len = len * 7 + 35;
                    tabbar.addTab(nodekey, caption, len + 'px', null, 0);

                    if (type == "Web") {
                        tabbar.setContentHref(nodekey, module);
                    }
                    else if (type == "WinForm") {
                        tabbar.setContentHref(nodekey, "../MainContainer.aspx?login=x,x,x,x," + module + ",x,Link");

                    }
                    tabbar.setTabActive(nodekey);
                    ind++;

                }
            }
            catch (e) {
                alert("增加标签页时错误：" + e);
            }
        }

        function TabContentLoad(tabID) {
            try {
                var s = tabbar.getContentById(tabID);
                s.all[0].contentWindow.setMCSize(s.all[0].offsetWidth * 0.99, s.all[0].offsetHeight * 0.98);
            }
            catch (e) {

            }
        }

        function removeAllTab() {
            try {
                tabbar.removeAllTab(true);
            }
            catch (e) {
            }
        }

        function resizeTab(width, height) {
            try {
                tabbar.setSize(width, height, true);
                var s = tabbar.getContentById(tabbar.getActiveTab());
                s.all[0].contentWindow.setMCSize(width - 6, height - 15);
            }
            catch (e) {
            }
        }

        function resizeTab2(idn, ido) {
            try {
                var s = tabbar.getContentById(tabbar.tabsId[idn].id);
                s.all[0].contentWindow.setMCSize(s.all[0].offsetWidth * 0.99, s.all[0].offsetHeight * 0.98);
                return true;
            }
            catch (e) 
            {
                return true;
            }

        }
    </script>
</head>
<body bgcolor="white" id="content" style="width: 100%; height: 100%">
    <form id="form1" runat="server" style="width: auto; height: auto;">
    <div id="a_tabbar" style="width: 99.5%; height: 99.5%;" />
    <script type="text/javascript">
        var ind = 0;
        tabbar = new dhtmlXTabBar("a_tabbar", "top");
        tabbar.setImagePath("images/tab/");
        tabbar.setHrefMode("iframe");
        tabbar.setSkinColors("#FCFBFC", "#F4F3EE", "#FCFBFC");
        tabbar.setOnRemoveHandler(removeTab);
        tabbar.setOnSelectHandler(resizeTab2);
    </script>
    </form>
</body>
</html>
