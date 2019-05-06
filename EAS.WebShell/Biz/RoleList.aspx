<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleList.aspx.cs" Inherits="EAS.WebShell.Biz.RoleList" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title></title>
    <meta name="sourcefiles" content="~/Biz/AppSttingWindow.aspx" />
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
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel7" runat="server" />
    <f:Panel ID="Panel7" runat="server" BodyPadding="5px" Title="Panel" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Form ID="Form5" runat="server" Height="72px" BodyPadding="5px" ShowHeader="true"
                ShowBorder="false" LabelAlign="Right" Title="查询条件">
                <Rows>
                    <f:FormRow ID="FormRow1" runat="server" ColumnWidths="30% 70%">
                        <Items>
                            <f:TextBox ID="tbKey" runat="server" Label="检索" Width="150px" LabelWidth="40px">
                            </f:TextBox>
                            <f:Label ID="Label1" runat="server">
                            </f:Label>
                        </Items>
                    </f:FormRow>
                </Rows>
            </f:Form>
            <f:Grid ID="Grid2" Title="Grid2" PageSize="15" ShowBorder="true" BoxFlex="1" AllowPaging="true"
                AllowSorting="true" OnPageIndexChange="Grid2_PageIndexChange" ShowHeader="false"
                runat="server" EnableCheckBoxSelect="True" DataKeyNames="Name" IsDatabasePaging="true"
                SortField="Name" SortDirection="ASC" OnSort="Grid2_Sort" OnRowCommand="Grid2_RowCommand">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" runat="server">
                        <Items>
                            <f:Button ID="btnNew" Text="新建角色" runat="server" Icon="New">
                            </f:Button>
                            <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                            </f:ToolbarSeparator>
                            <f:Button ID="btnQuery" Text="刷新" runat="server" Icon="Reload" OnClick="btnQuery_Click">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:RowNumberField />
                    <f:BoundField Width="150px" SortField="Name" DataField="Name" HeaderText="名称" />
                    <f:BoundField Width="480px" DataField="Description" HeaderText="描述" />
                    <f:WindowField TextAlign="Center" Width="35px" WindowID="Window1" Icon="Pencil" ToolTip="编辑角色"
                        DataIFrameUrlFields="Name" DataIFrameUrlFormatString="~/Biz/RoleWindow.aspx?name={0}"
                        Title="编辑角色" IFrameUrl="~/alert.aspx" />
                    <f:LinkButtonField TextAlign="Center" Width="35px" Icon="Delete" ToolTip="删除" ConfirmText="确定删除此角色？"
                        ConfirmTarget="Top" CommandName="Delete" />
                </Columns>
            </f:Grid>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="520px" Height="400px">
    </f:Window>
    </form>
</body>
</html>
