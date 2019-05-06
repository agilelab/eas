<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TextBuilder.aspx.cs" Inherits="EAS.WebShell.Biz.TextBuilder" %>

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
        <Toolbars>
            <f:Toolbar ID="Toolbar1" runat="server" Position="Bottom">
                <Items>
                    <f:Button ID="btnCreate" Text="生成" runat="server" Icon="Accept" OnClick="btnCreate_Click">
                    </f:Button>
                    <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                    </f:ToolbarSeparator>
                    <f:Button ID="btnClear" Text="清空" runat="server" Icon="Delete">
                    </f:Button>
                    <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                    </f:ToolbarSeparator>
                    <f:Button ID="btnCopy" EnablePostBack="false" Text="复制" runat="server" Icon="Compass">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="tbText" runat="server" Height="265px" 
                        ShowLabel="False" />
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="tbText2" Height="265px" 
                        ShowLabel="False" />
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="tbName" runat="server" Label="变量名称">
                    </f:TextBox>
                </Items>
            </f:FormRow>
        </Rows>
    </f:Form>
    </form>
</body>
