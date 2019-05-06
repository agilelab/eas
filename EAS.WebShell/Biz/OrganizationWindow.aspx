<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrganizationWindow.aspx.cs"
    Inherits="EAS.WebShell.Biz.OrganizationWindow" %>

<!doctype html>
<html>
<head id="head2" runat="server">
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
    <form id="form3" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="form2" runat="server" />
    <f:Form ID="form2" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
        runat="server" LabelWidth="70px">
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
                    <f:Label ID="lbID" runat="server" Label="机构ID" Text="" />
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="tbName" runat="server" Label="机构名称" Required="true" ShowRedStar="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList Label="上级机构" AutoPostBack="false" Required="true" EnableSimulateTree="true"
                        ShowRedStar="true" runat="server" ID="ddlParent">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="tbAddress" runat="server" Label="地址" />
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="tbContact" runat="server" Label="联系人" />
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="50% 50%">
                <Items>
                    <f:TextBox ID="tbTel" runat="server" Label="电话" />
                    <f:TextBox ID="tbFax" runat="server" Label="传真" LabelWidth="60px" />
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="tbMail" runat="server" Label="电子邮件" />
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="tbHomepage" runat="server" Label="机构主页" />
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="tbOrganCode" runat="server" Label="机构代码" />
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="tbRemarks" Height="66px" Label="备注" runat="server" Required="false" />
                </Items>
            </f:FormRow>
        </Rows>
    </f:Form>
    <asp:HiddenField ID="hfGuid" runat="server" />
    </form>
</body>
