<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModuleSelector.aspx.cs"
    Inherits="EAS.WebShell.Biz.ModuleSelector" %>

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
    <f:Form ID="Form5" runat="server" Height="93px" BodyPadding="10px" ShowHeader="false"
        ShowBorder="false" LabelAlign="Right" Title="查询条件">
        <rows>
            <f:FormRow ID="FormRow1" runat="server" ColumnWidths="45% 30% 25%" 
                BoxConfigChildMargin="5">
                <Items>
                    <f:TextBox ID="tbKey" runat="server" Label="检索" LabelWidth="40px" Width="180px"
                        LabelAlign="Left" RegionPosition="Left" BoxConfigAlign="Center" 
                        Margin="15px 15px 5px 0px">
                    </f:TextBox>
                    <f:Panel ID="Panel2" runat="server" ShowBorder="false"
                        EnableCollapse="false" CssClass="mytable" Layout="Table" TableConfigColumns="2"
                        ShowHeader="false" BodyPadding="0">
                        <Items>
                            <f:CheckBox TableRowspan="1" TableColspan="1" ID="cbModule" runat="server" Label="Label"
                                ShowLabel="False" Text="搜索模块" BoxConfigPadding="3" OffsetRight="5px">
                            </f:CheckBox>
                            <f:CheckBox TableRowspan="1" TableColspan="1" ID="cbReport" runat="server" Label="Label"
                                ShowLabel="False" Text="搜索报表" BoxConfigPadding="3" OffsetRight="5px">
                            </f:CheckBox>
                            <f:CheckBox TableRowspan="1" TableColspan="2" ID="cbBiz" runat="server" Label="Label"
                                ShowLabel="False" Text="搜索函数、业务功能" BoxConfigPadding="3" OffsetRight="5px">
                            </f:CheckBox>
                        </Items>
                    </f:Panel>
                    <f:Button ID="btnQuery" Text="搜索" runat="server" Icon="Reload" 
                        OnClick="btnQuery_Click" Margin="15px 0px 5px 0px">
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
        </rows>
    </f:Form>
    <f:Grid ID="Grid2" Title="Grid2" AllowSorting="true" AllowPaging="false" BoxFlex="1"
        ShowHeader="False" runat="server" Height="298px" EnableCheckBoxSelect="True"
        DataKeyNames="Guid" SortField="Name" EnableRowLines="True" 
        OnSort="Grid2_Sort" Margin="5">
        <toolbars>
            <f:Toolbar ID="Toolbar1" runat="server" Position="Bottom">
                <Items>
                    <f:Button ID="btnOK" Text="确定" runat="server" Icon="Accept" OnClick="btnOK_Click">
                    </f:Button>
                    <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                    </f:ToolbarSeparator>
                    <f:Button ID="btnClose" EnablePostBack="false" Text="取消" runat="server" Icon="SystemClose"
                        OnClick="btnClose_Click">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </toolbars>
        <columns>
            <f:RowNumberField />
            <f:BoundField Width="100px" SortField="Name" DataField="Name" HeaderText="名称" />
            <f:BoundField Width="150px" SortField="Assembly" DataField="Assembly" HeaderText="程序集" />
            <f:BoundField Width="240px" DataField="Description" HeaderText="说明" />
        </columns>
    </f:Grid>
    <asp:HiddenField ID="hfType" runat="server" />
    </form>
</body>
</html>
