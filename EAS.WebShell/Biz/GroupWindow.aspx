<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupWindow.aspx.cs" Inherits="EAS.WebShell.Biz.GroupWindow" %>

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
        runat="server" LabelWidth="75px">
        <Toolbars>
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
        </Toolbars>
        <Rows>
            <f:FormRow>
                <Items>
                    <f:Label ID="lbID" runat="server" Label="导航ID">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="tbName" runat="server" Label="导航名称" Required="true" ShowRedStar="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList Label="上级导航" AutoPostBack="false" Required="true" EnableSimulateTree="true"
                        ShowRedStar="true" runat="server" ID="ddlParent">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="nbSortCode" Label="排序码" Required="true" ShowRedStar="true" runat="server" />
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="tbDescription" Height="80px" Label="说明" runat="server" Required="false" />
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:CheckBox ID="cbExpend" runat="server" ShowLabel="False" Text="展开导航">
                    </f:CheckBox>
                </Items>
            </f:FormRow>
        </Rows>
    </f:Form>
    </form>
</body>
