<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppSttingWindow.aspx.cs"
    Inherits="EAS.WebShell.Biz.AppSttingWindow" %>

<!doctype html>
<html>
<head id="head1" runat="server">
    <title></title>
    <link href="~/res/css/common.css" rel="stylesheet" type="text/css" />
    <style>
        body.f-body
        {
            padding: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="form2" runat="server" />
    <f:Form ID="form2" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
        runat="server" LabelWidth="70px">
        <toolbars>
            <f:Toolbar ID="Toolbar1" runat="server" Position="Bottom">
                <Items>
                    <f:Button ID="btnSave" Text="保存|继续" runat="server" Icon="SystemSave" OnClick="btnSave_Click">
                    </f:Button>
                    <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                    </f:ToolbarSeparator>
                    <f:Button ID="btnSaveClose" Text="保存|关闭" runat="server" Icon="SystemSave" OnClick="btnSaveClose_Click">
                    </f:Button>
                    <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                    </f:ToolbarSeparator>
                    <f:Button ID="btnClose" EnablePostBack="false" Text="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </toolbars>
        <rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="tbCategory" runat="server" Label="参数目录" Required="true" ShowRedStar="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="tbKey" runat="server" Label="名称" Required="true" ShowRedStar="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="tbValue" runat="server" Height="80px" Label="参数值" Required="true"
                        ShowRedStar="true" />
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="tbDescription" Height="80px" Label="参数说明" runat="server" Required="false" />
                </Items>
            </f:FormRow>
        </rows>
    </f:Form>
    </form>
</body>
