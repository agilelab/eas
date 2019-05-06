<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EAS.ActiveXForm.Web.Biz.Default" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>AgileEAS.NET SOA 中间件平台ActiveXForm</title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/JavaScript">

        if (window.screen.width > '800') {
            document.writeln("<style type=\"text\/css\">");
            document.writeln("<!--");
            document.writeln("body {");
            document.writeln("	overflow-x:hidden;");
            document.writeln("	overflow-y:visible;");
            document.writeln("}");
            document.writeln("-->");
            document.writeln("<\/style>")
        }

        function switchSysBar() {
            if (switchPoint.innerText == 3) {
                switchPoint.innerText = 4
                document.all("frmTitle").style.display = "none"
                document.all("frmTop").style.display = "none"
            }
            else {
                switchPoint.innerText = 3
                document.all("frmTitle").style.display = ""
                document.all("frmTop").style.display = ""
            }
            document.all("main").contentWindow.resizeTab(document.all("main").offsetWidth - 2, document.all("main").offsetHeight - 25);
        }

        function CollapseV(bln) {
            if (bln && switchPoint.innerText == 3) {
                switchSysBar();
            }
            else if (!bln && switchPoint.innerText == 4) {
                switchSysBar();
            }
        }

        function OnLoad() {
            try {
                var o = document.all("main");
                o.contentWindow.resizeTab(o.offsetWidth - 2, o.offsetHeight - 25);

                applicationURLRoot = "<%=ApplicationURLRoot%>";  //地址
                sessionId = "<%=SessionId%>";   //会话
                loginID = "<%=LoginID %>";      //登录ID
                dataSet = "<%=DataSet %>";      //帐套
                organization = "<%=DataSet %>";    //机构

                param = applicationURLRoot + "|" + sessionId + "|" + loginID + "|" + dataSet + "|" + organization;

                var clientClasp = document.getElementById("clientClasp");
                clientClasp.StartEx(param);
            }
            catch (e) 
            {
                alert("ActiveXForm出错:" + e.message);
            }
        }

        function window.onresize() {
            var o = document.all("main");
            o.contentWindow.resizeTab(o.offsetWidth - 2, o.offsetHeight - 25);
        }

        //关闭窗口
        function window.onbeforeunload() {
            try {
                var clientClasp = this.document.getElementById("clientClasp");
                //clientClasp.Quit();
                clientClasp.parentNode.removeChild(clientClasp);
            }
            catch (e) 
            {
            }
        } 
  
    </script>
    <style type="text/css">
        .navPoint
        {
            cursor: hand;
            font-family: Webdings;
            font-size: 9pt;
        }
        .copyright
        {
            font-size: 12px;
            font-weight: bold;
        }
    </style>
</head>
<body onload="OnLoad();">
    <form id="form1" runat="server">
    <script type="text/javascript" src="ClientClasp.js"></script>
    <script type="text/javascript" language="javascript">
    function form1.clientClasp::OpenIEWindowE(url, target, windowOptions, replaceEntry)
    {
		window.open(url,target,windowOptions,replaceEntry);
	}
		
	var n = 0;
    function form1.clientClasp::AfterMenuSelectE(name, assembly, type, url, guid)
    {
//        alert("事件已激活。");
         
        var str_url = url;
        var id = guid;
        if(str_url!=undefined && str_url.length > 0)
        {
                str_url = str_url.substring(str_url.indexOf("@") + 1, str_url.length);
                var arr_js = str_url.split(",");
                var url=arr_js[0];
                var protocol = document.parentWindow.top.location.protocol; 
                var host = document.parentWindow.top.location.host;
                var port = document.parentWindow.top.location.port;
                var url = protocol + "//" + host + port + url;
//                document.parentWindow.top.document.getElementById('main').contentWindow.AddPage(n, name, guid, guid, url,'Web');
                document.parentWindow.top.document.getElementById('main').contentWindow.AddPage(guid, name, guid, guid, url,'Web');
        }
        else
        {
                var url2=assembly + "|" + type;
//                document.parentWindow.top.document.getElementById('main').contentWindow.AddPage(n, name, guid, guid, url2,'WinForm');
                document.parentWindow.top.document.getElementById('main').contentWindow.AddPage(guid, name, guid, guid, url2,'WinForm');
        }            
        n++;
    }
    </script>
    <div>
        <table width="100%" height="101%" border="0" cellpadding="0" cellspacing="0">
            <tr id="frmTop">
                <td style="border-bottom-style: solid; border-bottom-color: LightGrey; border-bottom-width: thin">
                    <script type="text/javascript" src="TopContainer.js"></script>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" height="100%" width="100%">
                        <tr>
                            <td align="middle" nowrap id="frmTitle" width="206px">
                                <script type="text/javascript" src="MenuContainer.js"></script>
                            </td>
                            <td style="width: 9pt">
                                <table border="0" cellpadding="0" cellspacing="0" height="100%" width="100%">
                                    <tr>
                                        <td style="height: 100%; width: 10px;" align="right" onclick="switchSysBar()" bgcolor="Gainsboro">
                                            <!-- background="images/narbar.gif">-->
                                            <font style="font-size: 9pt; cursor: default;"><span class="navPoint" id="switchPoint"
                                                title="折叠/打开菜单">3</span><br>
                                                <br>
                                            </font>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 100%; height: 100%;">
                                <iframe src="TabContainer.aspx" name="main" width="100%" marginwidth="0" height="100%"
                                    marginheight="0" align="middle" scrolling="no" frameborder="0" id="main"></iframe>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="height: 32px;">
                <td style=" padding: 0px 0px 1px 0px;  margin: 0px 0px 1px 0px; border-top-style: solid; border-top-color: #6699FF; border-top-width:thin; ">
                    <script type="text/javascript" src="BottomContainer.js"></script>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
