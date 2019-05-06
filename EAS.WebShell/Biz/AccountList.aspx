<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountList.aspx.cs" Inherits="EAS.WebShell.Biz.AccountList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style>
        .x-grid-tpl .others input
        {
            vertical-align: middle;
        }
        .x-grid-tpl .others label
        {
            margin-left: 5px;
            margin-right: 15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="RegionPanel1" runat="server" />
    <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server">
        <Regions>
            <f:Region ID="Region1" Split="true" Width="200px" ShowHeader="true" Title="组织机构" EnableCollapse="true"
                Layout="Fit" Position="Left" runat="server">
                <Items>
                    <f:Tree runat="server" ShowBorder="false" ShowHeader="false" AutoScroll="true" ID="treeOrgan"
                        OnNodeCommand="treeOrgan_Selected">
                    </f:Tree>
                </Items>
            </f:Region>
            <f:Region ID="Region2" ShowBorder="false" ShowHeader="false" Position="Center" Layout="Fit"
                BodyPadding="0px 5px 5px 0" runat="server">
                <Items>
                    <f:Panel ID="Panel7" runat="server" BodyPadding="5px" Title="Panel" ShowBorder="false"
                        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
                        <Items>
                            <f:Form ID="Form5" runat="server" Height="72px" BodyPadding="5px" ShowHeader="true"
                                ShowBorder="false" LabelAlign="Right" Title="查询条件">
                                <Rows>
                                    <f:FormRow ID="FormRow1" runat="server" ColumnWidths="25% 10% 10% 55%">
                                        <Items>
                                            <f:TextBox ID="tbKey" runat="server" Label="检索" LabelWidth="40px">
                                            </f:TextBox>
                                            <f:RadioButton ID="rbALL" runat="server" Checked="True" Label="Label" ShowLabel="False"
                                                AutoPostBack="true" Text="搜索全部" GroupName="1" OnCheckedChanged="rbALL_CheckedChanged">
                                            </f:RadioButton>
                                            <f:RadioButton ID="rbSlef" runat="server" Label="Label" ShowLabel="False" Text="仅本机构" AutoPostBack="true"
                                                GroupName="1" OnCheckedChanged="rbSlef_CheckedChanged">
                                            </f:RadioButton>
                                            <f:Label ID="Label1" runat="server">
                                            </f:Label>
                                        </Items>
                                    </f:FormRow>
                                </Rows>
                            </f:Form>
                            <f:Grid ID="Grid2" Title="Grid2" PageSize="15" ShowBorder="true" BoxFlex="1" AllowPaging="true"
                                AllowSorting="true" OnPageIndexChange="Grid2_PageIndexChange" ShowHeader="false"
                                runat="server" EnableCheckBoxSelect="True" DataKeyNames="LoginID" IsDatabasePaging="true"
                                SortField="LoginID" SortDirection="ASC" OnSort="Grid2_Sort" OnRowCommand="Grid2_RowCommand">
                                <Toolbars>
                                    <f:Toolbar ID="Toolbar2" runat="server">
                                        <Items>
                                            <f:Button ID="btnNew" Text="新建账号" runat="server" Icon="New">
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
                                    <f:BoundField Width="100px" SortField="LoginID" DataField="LoginID" HeaderText="登录ID" />
                                    <f:BoundField Width="120px" SortField="Name" DataField="Name" HeaderText="账号名称" />
                                    <f:BoundField Width="75px" SortField="LoginCount" DataField="LoginCount" HeaderText="登录记数" />
                                    <f:BoundField Width="75px" SortField="OriginalID" DataField="OriginalID" HeaderText="账号原型" />
                                    <f:BoundField Width="95px" SortField="Birthday" DataField="Birthday" HeaderText="生日"
                                        DataFormatString="{0:yyyy-MM-dd}" />
                                    <f:BoundField Width="90px" SortField="Developer" DataField="Phone" HeaderText="电话" />
                                    <f:BoundField Width="130px" SortField="SortCode" DataField="Mail" HeaderText="邮件" />
                                    <f:BoundField Width="75px" SortField="LineState" DataField="LineText" HeaderText="在线？" />
                                    <f:WindowField TextAlign="Center" Width="35px" WindowID="Window1" Icon="Pencil" ToolTip="账号编辑"
                                        DataIFrameUrlFields="LoginID" DataIFrameUrlFormatString="~/Biz/AccountWindow.aspx?loginid={0}"
                                        Title="账号编辑" IFrameUrl="~/alert.aspx" />
                                    <f:LinkButtonField TextAlign="Center" Width="35px" Icon="Delete" ToolTip="删除" ConfirmText="确定是否删除账号？"
                                        ConfirmTarget="Top" CommandName="Delete" />
                                    <f:BoundField Width="340px" DataField="Description" HeaderText="说明" />
                                </Columns>
                            </f:Grid>
                        </Items>
                    </f:Panel>
                </Items>
            </f:Region>
        </Regions>
    </f:RegionPanel>
    <f:Window ID="Window1" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="500px" Height="420px">
    </f:Window>
    </form>
</body>
</html>
