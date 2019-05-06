<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EAS.ActiveXForm.Web.Default" %>

<html>
<head>
    <title>AgileEAS.NET SOA 中间件平台ActiveXForm</title>
    <style type="text/css">
<!--
body {
	margin-left: 0px; margin-top: 0px;margin-right: 0px;margin-bottom: 0px;background-color: #1D3647;
}
-->
</style>
    <script language="JavaScript">
        function correctPNG() {
            var arVersion = navigator.appVersion.split("MSIE")
            var version = parseFloat(arVersion[1])
            if ((version >= 5.5) && (document.body.filters)) {
                for (var j = 0; j < document.images.length; j++) {
                    var img = document.images[j]
                    var imgName = img.src.toUpperCase()
                    if (imgName.substring(imgName.length - 3, imgName.length) == "PNG") {
                        var imgID = (img.id) ? "id='" + img.id + "' " : ""
                        var imgClass = (img.className) ? "class='" + img.className + "' " : ""
                        var imgTitle = (img.title) ? "title='" + img.title + "' " : "title='" + img.alt + "' "
                        var imgStyle = "display:inline-block;" + img.style.cssText
                        if (img.align == "left") imgStyle = "float:left;" + imgStyle
                        if (img.align == "right") imgStyle = "float:right;" + imgStyle
                        if (img.parentElement.href) imgStyle = "cursor:hand;" + imgStyle
                        var strNewHTML = "<span " + imgID + imgClass + imgTitle
             + " style=\"" + "width:" + img.width + "px; height:" + img.height + "px;" + imgStyle + ";"
             + "filter:progid:DXImageTransform.Microsoft.AlphaImageLoader"
             + "(src=\'" + img.src + "\', sizingMethod='scale');\"></span>"
                        img.outerHTML = strNewHTML
                        j = j - 1
                    }
                }
            }
        }
        window.attachEvent("onload", correctPNG);

        function OnLoad() {
            var activeX_OK = true;
            try {
                param = "<%=ApplicationURLRoot%>";
                var clientClasp = document.getElementById("clientClasp");
                clientClasp.Prepare(param);
            }
            catch (e) {
                activeX_OK = false;
                alert("ActiveXForm出错:" + e.message);
            }

            if (activeX_OK == true) {
                var clientClasp = document.getElementById("clientClasp");
                if (clientClasp.CheckUpdate() == true) {
                    clientClasp.Upgrade();
                }
            }
        }

        //关闭窗口
        function window.onbeforeunload() {
            try {
                var clientClasp = this.document.getElementById("clientClasp");
                clientClasp.Quit();
                clientClasp.parentNode.removeChild(clientClasp);
            }
            catch (e) {
            }
        }
    </script>
    <link href="images/skin.css" rel="stylesheet" type="text/css">
</head>
<body onload="OnLoad();">
    <form id="form1" runat="server">
    <script type="text/javascript" src="biz/ClientClasp.js"></script>
    <table width="100%" height="166" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td height="42" valign="top">
                <table width="100%" height="42" border="0" cellpadding="0" cellspacing="0" class="login_top_bg">
                    <tr>
                        <td width="1%" height="21">
                            &nbsp;
                        </td>
                        <td height="42">
                            &nbsp;
                        </td>
                        <td width="17%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <table width="100%" height="532" border="0" cellpadding="0" cellspacing="0" class="login_bg">
                    <tr>
                        <td width="49%" align="right">
                            <table width="91%" height="532" border="0" cellpadding="0" cellspacing="0" class="login_bg2">
                                <tr>
                                    <td height="138" valign="top">
                                        <table width="89%" height="427" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td height="149">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="80" align="right" valign="top">
                                                    <img src="images/logo.png" width="354" height="68">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="198" align="right" valign="top">
                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td width="35%">
                                                                &nbsp;
                                                            </td>
                                                            <td height="25" colspan="2" class="left_txt">
                                                                <p>
                                                                    1- AgileEAS.NET SOA平台是一套快速应用开发平台</p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td height="25" colspan="2" class="left_txt">
                                                                <p>
                                                                    2- AgileEAS.NET SOA平台是一套消息中间件</p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td height="25" colspan="2" class="left_txt">
                                                                <p>
                                                                    3- AgileEAS.NET SOA平台是一套应用集成平台</p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td width="30%" height="40">
                                                                <img src="images/icon-demo.gif" width="16" height="16"><a href="" target="_blank"
                                                                    class="left_txt3"> 使用说明</a>
                                                            </td>
                                                            <td width="35%">
                                                                <img src="images/icon-login-seaver.gif" width="16" height="16"><a href="" class="left_txt3">
                                                                    在线客服</a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="1%">
                            &nbsp;
                        </td>
                        <td width="50%" valign="bottom">
                            <table width="100%" height="59" border="0" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="4%">
                                        &nbsp;
                                    </td>
                                    <td width="96%" height="38">
                                        <span class="login_txt_bt">登陆AgileEAS.NET SOA中间件平台</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td height="21">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0" id="table211" height="328">
                                            <tr>
                                                <td height="164" colspan="2" align="middle">
                                                    <form name="myform" action="" method="post">
                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0" height="143" id="table212">
                                                        <tr>
                                                            <td width="13%" height="38" class="top_hui_text">
                                                                <span class="login_txt">用户名：&nbsp;&nbsp; </span>
                                                            </td>
                                                            <td height="38" colspan="2" class="top_hui_text">
                                                                <asp:TextBox ID="tbLogin" runat="server" ToolTip="输入您的系统用户名（登录ID）。" CssClass="editbox4"
                                                                    TabIndex="1" size="20"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="13%" height="35" class="top_hui_text">
                                                                <span class="login_txt">密 码： &nbsp;&nbsp; </span>
                                                            </td>
                                                            <td height="35" colspan="2" class="top_hui_text">
                                                                <asp:TextBox ID="tbPass" runat="server" ToolTip="输入您的系统密码（区分大小写）。" CssClass="editbox4"
                                                                    TextMode="Password" TabIndex="2" size="20"></asp:TextBox>
                                                                <img src="images/luck.gif" width="19" height="18">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35">
                                                                &nbsp;
                                                            </td>
                                                            <td height="35" colspan="2">
                                                                <asp:ImageButton ID="buttonOK" runat="server" ImageUrl="images/logo.gif" OnClick="buttonOK_Click"
                                                                    Width="120" Height="30" class="button" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br>
                                                    </form>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="433" height="164" align="right" valign="bottom">
                                                    <img src="images/login-wel.gif" width="242" height="138">
                                                </td>
                                                <td width="57" align="right" valign="bottom">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="20">
                <table>
                    <tr>
                        <td width="40px" />
                        <td>
                            <span class="login-buttom-txt">说明：第一次使用ActiveXForm运行容器请<a href="xClient/downloads/eRoot.cer"
                                style="color: #ffffff">安装根证书</a>,若不能正常运行，请下载<a href="xClient/downloads/EAS.ActiveXForm.msi"
                                style="color: #ffffff">未经签名的安装包</a>手工安装、配置IE安全性设置，有关这方面的资料请参考:<a href="http://www.cnblogs.com/eastjade/archive/2010/06/26/1765792.html"
                                style="color: #ffffff">安全设置帮助</a></span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="20">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="login-buttom-bg">
                    <tr>
                        <td align="center">
                            <span class="login-buttom-txt">版权所有&copy; 2004-2013 Copyright 敏捷软件工程实验室 备案号: <a href="http://www.miibeian.gov.cn/"
                                style="color: #ffffff">陇ICP备09001014号</span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
