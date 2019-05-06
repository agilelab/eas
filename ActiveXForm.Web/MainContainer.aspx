<%@ Page Language="C#" AutoEventWireup="true" Codebehind="MainContainer.aspx.cs"
    Inherits="EAS.ActiveXForm.Web.MainContainer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body onload="onLoad()" bottommargin="1" leftmargin="1" rightmargin="1" topmargin="1">

    <script language="javascript">
    function onLoad()
    {
        try
        { 
            minfo = "<%=Guid%>" + "|" + "<%=Module%>" ;
            form1.mainContainer.StartEx(minfo);
        }
        catch(e)
        {
            alert("ActiveXForm出错:"+e.message);
        }
    }
    
    function closeContainer()
    {
        try
        {
            form1.mainContainer.CloseContainer();
            return true; 
        }
        catch(e)
        {
            return true;
        }
    }
    
    function setMCSize(width, height)
    {
        form1.mainContainer.style.width = width + "px";
        form1.mainContainer.style.height = height + "px";
    }

    </script>
    <form id="form1" runat="server">
        <script type="text/javascript" src="biz/mainContainer.js"></script>
        <script type="text/javascript" language="javascript">
    function form1.mainContainer::ContainerClose()
    {
        var tabbar = document.parentWindow.top.document.getElementById("main").contentWindow.tabbar;
        tabbar.removeTab(tabbar.getActiveTab(),true);
    }
   
    function form1.mainContainer::OpenＭoduleE(name, assembly, type, url, guid)
    {
        var url2=assembly + "|" + type; 
        document.parentWindow.top.document.getElementById('main').contentWindow.addPage(guid, name, guid, guid, url2 ,'WinForm')
    }       
    
    function form1.mainContainer::CollapseVE(bln)
    {
        document.parentWindow.top.document.getElementById('main').document.parentWindow.CollapseV(bln);
    }
    
    function form1.mainContainer::OpenWindowE(url, target, windowOptions, replaceEntry)
    {
		window.open(url,target,windowOptions,replaceEntry);
		return true;
	}
        </script>
    </form>
</body>
</html>
