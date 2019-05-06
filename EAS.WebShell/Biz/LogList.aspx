<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogList.aspx.cs" Inherits="EAS.WebShell.Biz.LogList" %>

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
                    <f:FormRow ID="FormRow1" runat="server" ColumnWidths= "40px 120px 25px 120px 25px 200px 200px">
                        <Items>
                            <f:Label ID="Label2" runat="server" Text="时间：">
                            </f:Label>
                            <f:DatePicker runat="server" Required="true" ShowLabel="false" DateFormatString="yyyy-MM-dd"
                                EmptyText="请选择开始日期" ID="dtpStart" ShowRedStar="True" >
                            </f:DatePicker>
                            <f:Label ID="Label3" runat="server" Text="==>" >
                            </f:Label>
                            <f:DatePicker ID="dtpEnd" Required="true" Readonly="false" CompareControl="dtpStart"
                                DateFormatString="yyyy-MM-dd" CompareOperator="GreaterThan" CompareMessage="结束日期应该大于开始日期"
                                ShowLabel="false" runat="server" ShowRedStar="True" >
                            </f:DatePicker>
                            <f:Label ID="Label4" runat="server" Width="20px">
                            </f:Label>
                            <f:TextBox ID="tbKey" runat="server" Label="检索" LabelWidth="40px" LabelAlign="Left">
                            </f:TextBox>
                            <f:Label ID="Label1" runat="server">
                            </f:Label>
                        </Items>
                    </f:FormRow>
                </Rows>
            </f:Form>
            <f:Grid ID="Grid2" Title="Grid2" PageSize="15" ShowBorder="true" BoxFlex="1" AllowPaging="true"
                AllowSorting="true" OnPageIndexChange="Grid2_PageIndexChange" ShowHeader="false"
                runat="server" EnableCheckBoxSelect="True" DataKeyNames="ID" IsDatabasePaging="true"
                SortField="ID" SortDirection="Desc" OnSort="Grid2_Sort">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" runat="server">
                        <Items>
                            <f:Button ID="btnQuery" Text="刷新" runat="server" Icon="Reload" OnClick="btnQuery_Click">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:RowNumberField />
                    <f:BoundField Width="100px" SortField="LoginID" DataField="LoginID" HeaderText="账户" />
                    <f:BoundField Width="200px" SortField="Event" DataField="Event" HeaderText="事件" />
                    <f:BoundField Width="150px" SortField="EventTime" DataField="EventTime" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                        HeaderText="时间" />
                    <f:BoundField Width="150px" SortField="HostName" DataField="HostName" HeaderText="主机" />
                    <f:BoundField Width="150px" SortField="IpAddress" DataField="IpAddress" HeaderText="IP地址" />
                </Columns>
            </f:Grid>
        </Items>
    </f:Panel>
    </form>
</body>
</html>
