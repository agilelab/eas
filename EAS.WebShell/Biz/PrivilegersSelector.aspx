<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrivilegersSelector.aspx.cs"
    Inherits="EAS.WebShell.Biz.PrivilegersSelector" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
    <f:Form ID="Form5" runat="server" Height="72px" BodyPadding="5px" ShowHeader="true"
        ShowBorder="false" LabelAlign="Right" Title="查询条件">
        <Rows>
            <f:FormRow ID="FormRow1" runat="server" ColumnWidths="40% 20% 20% 20%">
                <Items>
                    <f:TextBox ID="tbKey" runat="server" Label="检索" Width="200px" LabelWidth="40px" LabelAlign="Left">
                    </f:TextBox>
                    <f:CheckBox ID="cbAccount" runat="server" Label="Label" ShowLabel="False" 
                        Text="搜索账号">
                    </f:CheckBox>
                    <f:CheckBox ID="cbRole" runat="server" Label="Label" ShowLabel="False" 
                        Text="搜索角色">
                    </f:CheckBox>
                    <f:Button ID="btnQuery" Text="搜索" runat="server" Icon="Reload" OnClick="btnQuery_Click">
                    </f:Button>
                </Items>
            </f:FormRow>
            <f:FormRow ID="FormRow2" runat="server" ColumnWidths="40% 60%">
                <Items>
                    <f:Label ID="Label1" runat="server" Text="搜索结果:">
                    </f:Label>
                    <f:Label ID="Label2" runat="server">
                    </f:Label>
                </Items>
            </f:FormRow>
        </Rows>
    </f:Form>
    <f:Grid ID="Grid2" Title="Grid2" PageSize="15" BoxFlex="1" ShowHeader="False" runat="server" Height="285px"
        EnableCheckBoxSelect="True" DataKeyNames="PType,Code" SortField="Name" EnableRowLines="True">
        <Toolbars>
            <f:Toolbar ID="Toolbar1" runat="server" Position="Bottom">
                <Items>
                    <f:Button ID="btnOK" Text="确定" runat="server" Icon="Accept" OnClick="btnOK_Click">
                    </f:Button>
                    <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                    </f:ToolbarSeparator>
                    <f:Button ID="btnClose" EnablePostBack="false" Text="取消" runat="server" 
                        Icon="SystemClose" OnClick="btnClose_Click">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Columns>
            <f:RowNumberField ID="ctl12" ColumnID="Grid2_ctl12" />
            <f:BoundField Width="120px" SortField="Name" DataField="Name" HeaderText="名称" />
            <f:TemplateField Width="80px" HeaderText="类型" >
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# GetPTypeText(Eval("PType")) %>'></asp:Label>
                </ItemTemplate>
            </f:TemplateField>
        </Columns>
    </f:Grid>
    </form>
</body>
