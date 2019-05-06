<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModuleList.aspx.cs" Inherits="EAS.WebShell.Biz.ModuleList" %>

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
                    <f:FormRow ID="FormRow1" runat="server" ColumnWidths="20% 20% 60%">
                        <Items>
                            <f:DropDownList ID="cbxType" runat="server" Label="模块类型" LabelAlign="Left" LabelWidth="75px"
                                Width="195px">
                            </f:DropDownList>
                            <f:TextBox ID="tbKey" runat="server" Label="检索" Width="195px" LabelWidth="40px" LabelAlign="Left">
                            </f:TextBox>
                            <f:Label ID="Label1" runat="server">
                            </f:Label>
                        </Items>
                    </f:FormRow>
                </Rows>
            </f:Form>
            <f:Grid ID="Grid2" Title="Grid2" PageSize="15" ShowBorder="true" BoxFlex="1" AllowPaging="true"
                AllowSorting="true" OnPageIndexChange="Grid2_PageIndexChange" ShowHeader="false"
                runat="server" EnableCheckBoxSelect="True" DataKeyNames="Guid" IsDatabasePaging="true"
                SortField="Assembly" SortDirection="ASC" OnSort="Grid2_Sort" OnRowCommand="Grid2_RowCommand">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" runat="server">
                        <Items>
                            <f:Button ID="btnInstall" Text="安装模块" runat="server" Icon="New">
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
                    <f:BoundField Width="100px" SortField="Name" DataField="Name" HeaderText="模块名称" />
                    <f:BoundField Width="200px" SortField="Assembly" DataField="Assembly" HeaderText="程序集" />
                    <f:BoundField Width="300px" SortField="Type" DataField="Type" HeaderText="类型" />
                    <f:BoundField Width="75px" SortField="Version" DataField="Version" HeaderText="版本" />
                    <f:BoundField Width="90px" SortField="Developer" DataField="Developer" HeaderText="作者" />
                    <f:BoundField Width="60px" SortField="SortCode" DataField="SortCode" HeaderText="排序码" />
                    <f:WindowField TextAlign="Center" Width="35px" WindowID="Window1" Icon="Pencil" ToolTip="模块编辑"
                        DataIFrameUrlFields="Guid" DataIFrameUrlFormatString="~/Biz/ModuleWindow.aspx?guid={0}"
                        Title="模块编辑" IFrameUrl="~/alert.aspx" />
                    <f:LinkButtonField TextAlign="Center" Width="35px" Icon="Delete" ToolTip="卸载" ConfirmText="确定卸载模块？"
                        ConfirmTarget="Top" CommandName="Delete" />
                    <f:BoundField Width="340px" DataField="Description" HeaderText="说明" />
                    <f:BoundField Width="100px" DataField="LMTime" DataFormatString="{0:yyyy-MM-dd}"
                        HeaderText="更新时间" />
                    <f:BoundField Width="240px" DataField="Assembly" HeaderText="模块ID" />                    
                </Columns>
            </f:Grid>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="模块属性" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="500px" Height="385px">
    </f:Window>
    </form>
</body>
</html>
