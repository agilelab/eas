<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EAS.WebShell.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>AgileEAS.NET SOA中间件 系统登录</title>
    <style>
    .bg-blue {
    background: url(res/images/login/bg.jpg) no-repeat fixed;
    background-size: 100% 100%;
    
}
a{color:#474747; text-decoration: none;}
a:hover,
a:focus {
  color: #333;
  text-decoration: none;
}
    
    </style>
</head>
<body class="bg-blue">    
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" />
    <f:Window ID="Window1" runat="server" Title="登录表单" IsModal="false" EnableClose="false"
        WindowPosition="GoldenSection" Width="350px">
        <Items>
            <f:SimpleForm ID="SimpleForm1" runat="server" ShowBorder="false" BodyPadding="10px"
                LabelWidth="60px" ShowHeader="false">
                <Items>
                    <f:TextBox ID="tbLogin" Label="用户名" Required="true" runat="server">
                    </f:TextBox>
                    <f:TextBox ID="tbPassword" Label="密码" TextMode="Password" Required="true" runat="server">
                    </f:TextBox>
                    <f:TextBox ID="tbCaptcha" Label="验证码" Required="true" runat="server">
                    </f:TextBox>
                    <f:Panel CssStyle="padding-left:65px;" ShowBorder="false" ShowHeader="false" runat="server">
                        <Items>
                            <f:Image ID="imgCaptcha" CssStyle="float:left;width:160px;" runat="server">
                            </f:Image>
                            <f:LinkButton CssStyle="float:left;margin-top:8px;" ID="btnRefresh" Text="看不清？" runat="server"
                                OnClick="btnRefresh_Click">
                            </f:LinkButton>
                        </Items>
                    </f:Panel>
                </Items>
            </f:SimpleForm>
        </Items>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" runat="server" Position="Bottom" ToolbarAlign="Right">
                <Items>
                    <f:Button ID="btnLogin" Text="登录" Type="Submit" ValidateForms="SimpleForm1" ValidateTarget="Top"
                        runat="server" OnClick="btnLogin_Click">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Window>
    </form>
</body>
</html>
