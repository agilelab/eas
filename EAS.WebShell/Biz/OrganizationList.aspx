<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrganizationList.aspx.cs"
    Inherits="EAS.WebShell.Biz.OrganizationList" %>

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
            <f:Grid ID="Grid2" Title="Grid2" PageSize="100" ShowBorder="true" BoxFlex="1" AllowPaging="false"
                AllowSorting="true" ShowHeader="false" runat="server" EnableCheckBoxSelect="True"
                DataKeyNames="Guid" OnRowCommand="Grid2_RowCommand">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" runat="server">
                        <Items>
                            <f:Button ID="btnNew" Text="新建机构" runat="server" Icon="New">
                            </f:Button>
                            <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                            </f:ToolbarSeparator>
                            <f:Button ID="btnQuery" Text="刷新" runat="server" Icon="Reload" OnClick="btnQuery_Click">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:BoundField DataField="Name" HeaderText="机构名称" DataSimulateTreeLevelField="TreeLevel"
                        Width="200px" />
                    <f:BoundField DataField="Address" HeaderText="地址" Width="180px" />
                    <f:BoundField DataField="Contact" HeaderText="联系人" Width="80px" />
                    <f:BoundField DataField="Tel" HeaderText="电话" Width="95px" />
                    <f:BoundField DataField="Fax" HeaderText="传真" Width="95px" />
                    <f:BoundField DataField="Email" HeaderText="邮件" Width="120px" />
                    <f:BoundField DataField="Homepage" HeaderText="主页" Width="150px" />
                    <f:BoundField DataField="OrganCode" HeaderText="机构代码" Width="120px" />
                    <f:WindowField TextAlign="Center" Width="35px" WindowID="Window1" Icon="Pencil" ToolTip="编辑组织机构"
                        DataIFrameUrlFields="Name" DataIFrameUrlFormatString="~/Biz/OrganizationWindow.aspx?name={0}"
                        Title="编辑角色" IFrameUrl="~/alert.aspx" />
                    <f:LinkButtonField TextAlign="Center" Width="35px" Icon="Delete" ToolTip="删除" ConfirmText="确定删除此组织机构？"
                        ConfirmTarget="Top" CommandName="Delete" />
                    <f:BoundField DataField="Remarks" HeaderText="说明" Width="180px" />
                    <f:BoundField DataField="Guid" HeaderText="ID" Width="180px" TextAlign="Center" />
                </Columns>
            </f:Grid>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="520px" Height="435px">
    </f:Window>
    </form>
</body>
</html>
