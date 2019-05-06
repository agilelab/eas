<%@ Page Language="C#" AutoEventWireup="true" Codebehind="MenuContainer.aspx.cs"
    Inherits="EAS.ActiveXForm.Web.Biz.MenuContainer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>导航</title>
    <link href="css/css.css" rel="stylesheet" type="text/css">

    <script language="JavaScript" type="text/JavaScript">
function LoadMenu()
{
    try
    {
        
        form1.menuBar.StartEx();
    }
    catch(e)
    {
        alert("ActiveXForm出错:"+e.message);
    }
}

    </script>

</head>
<body>
    <form id="form1" runat="server">

        <script type="text/javascript" src="MenuContainer.js"></script>
        <script type="text/javascript" language="javascript">
    
    var n = 0;
    
    function form1.ClientBud_Menu::AfterMenuSelectE(str_menuCode, str_menuName, str_type, str_url, str_appCode)
    {
        var str_url = str_url;
   var id = str_menuCode;
   if(str_url!=undefined && str_url.length > 0)
   {
       if(str_url.indexOf("@") >= 0)
       {            
            str_url = str_url.substring(str_url.indexOf("@") + 1, str_url.length);
            var arr_js = str_url.split(",");
            var url=arr_js[0];
            var protocol = document.parentWindow.top.location.protocol;
            var host = document.parentWindow.top.location.host;
            var port = document.parentWindow.top.location.port;
            url = protocol + "//" + host + port + url;
            document.parentWindow.top.document.getElementById('main').contentWindow.AddPage(n, str_menuName, str_menuCode, str_appCode, url,'Web');
       }
       else if(str_url.toLowerCase().indexOf(".dll") >= 0)
       {
            var arr_js = str_url.split(",");
            var url=arr_js[0];
            url = url.replace('#','%');
            
            document.parentWindow.top.document.getElementById('main').contentWindow.AddPage(n, str_menuName, str_menuCode, str_appCode, url,'WinForm');
       }
       else
       {
            var id = str_menuCode;
            var index = str_menuName.indexOf("&nbsp");
            if(index > 0)
            {
                caption = str_menuName.substring(0, str_menuName.indexOf("&nbsp"));
            }
            else
            {
                caption = str_menuName;
            }
    
            document.parentWindow.top.document.getElementById('main').contentWindow.AddPage(n,  caption, str_menuCode, str_appCode, str_url,'Web');
            n++;
       }
       n++;
   }
    }
        </script>

    </form>
</body>
</html>
