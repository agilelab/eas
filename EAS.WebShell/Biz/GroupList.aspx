<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupList.aspx.cs" Inherits="EAS.WebShell.Biz.GroupList" %>

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
    <script>
        function DoSelectModules(parm) {
            __doPostBack('', 'DoAddModules$' + parm);
        }        
    </script>
    <f:PageManager ID="PageManager1" AutoSizePanelID="RegionPanel1" runat="server" />
    <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server">
        <Regions>
            <f:Region ID="Region1" Split="true" Width="380px" ShowHeader="true" Title="导航分组"
                EnableCollapse="true" Layout="Fit" Position="Left" runat="server">
                <Items>
                    <f:Tree runat="server" ShowBorder="false" ShowHeader="false" AutoScroll="true" ID="treeGroup"
                        OnNodeCommand="treeGroup_Selected">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar1" runat="server">
                                <Items>
                                    <f:Button ID="btnNewGroup" Text="新建导航" runat="server" Icon="New">
                                    </f:Button>
                                    <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                                    </f:ToolbarSeparator>
                                    <f:Button ID="btnModify" Text="编辑导航" runat="server" Icon="Pencil" OnClick="btnModify_Click">
                                    </f:Button>
                                    <f:ToolbarSeparator ID="ToolbarSeparator5" runat="server">
                                    </f:ToolbarSeparator>
                                    <f:Button ID="btnDelete" Text="删除导航" runat="server" Icon="Delete" OnClick="btnDelte_Click">
                                    </f:Button>
                                    <f:ToolbarSeparator ID="ToolbarSeparator4" runat="server">
                                    </f:ToolbarSeparator>
                                    <f:Button ID="btnReload" Text="刷新" runat="server" Icon="Reload" OnClick="btnReload_Click">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                    </f:Tree>
                </Items>
            </f:Region>
            <f:Region ID="Region2" ShowBorder="false" ShowHeader="false" Position="Center" Layout="Fit"
                BodyPadding="0px 5px 5px 0" runat="server">
                <Items>
                    <f:Grid ID="Grid2" Title="Grid2" PageSize="15" ShowBorder="true" BoxFlex="1" AllowPaging="true"
                        AllowSorting="true" OnPageIndexChange="Grid2_PageIndexChange" ShowHeader="false"
                        runat="server" EnableCheckBoxSelect="True" DataKeyNames="Guid" IsDatabasePaging="true"
                        SortField="Name" SortDirection="ASC" OnSort="Grid2_Sort" OnRowCommand="Grid2_RowCommand">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar2" runat="server">
                                <Items>
                                    <f:Button ID="btnAddModules" Text="添加模块" runat="server" Icon="New">
                                    </f:Button>
                                    <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                                    </f:ToolbarSeparator>
                                    <f:Button ID="btnRemoves" Text="移除模块" runat="server" Icon="Delete" OnClick="btnRemoves_Click">
                                    </f:Button>
                                    <f:ToolbarSeparator ID="ToolbarSeparator3" runat="server">
                                    </f:ToolbarSeparator>
                                    <f:Button ID="btnQuery" Text="刷新" runat="server" Icon="Reload" OnClick="btnQuery_Click">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:RowNumberField />
                            <f:BoundField Width="100px" SortField="Name" DataField="Name" HeaderText="模块名称" />
                            <f:BoundField Width="150px" SortField="Assembly" DataField="Assembly" HeaderText="程序集" />
                            <f:BoundField Width="260px" SortField="Type" DataField="Type" HeaderText="类型" />
                            <f:BoundField Width="95px" SortField="Version" DataField="Version" HeaderText="版本" />
                            <f:BoundField Width="90px" SortField="Developer" DataField="Developer" HeaderText="作者" />
                            <f:BoundField Width="320px" DataField="Description" HeaderText="说明" />
                        </Columns>
                    </f:Grid>
                </Items>
            </f:Region>
        </Regions>
    </f:RegionPanel>
    <f:Window ID="Window1" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" IsModal="true" OnClose="Window1_Close"
        Width="470px" Height="330px">
    </f:Window>
    <f:Window ID="Window2" Title="选择窗口" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" IsModal="true" Width="610px"
        Height="443px">
    </f:Window>
    </form>
</body>
</html>
