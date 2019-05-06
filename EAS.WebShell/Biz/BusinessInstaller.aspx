<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BusinessInstaller.aspx.cs"
    Inherits="EAS.WebShell.Biz.BusinessInstaller" %>

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
    <form id="form2" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="form2" runat="server" />
    <f:Form ID="Form5" runat="server" Height="36px" BodyPadding="5px" ShowHeader="false"
        ShowBorder="false" LabelAlign="Right" Title="查询条件">
        <Rows>
            <f:FormRow ID="FormRow1" runat="server" ColumnWidths="70% 30%">
                <Items>
                    <f:DropDownList ID="cbxFiles" runat="server" Label="程序集" LabelWidth="65px">
                    </f:DropDownList>
                    <f:Button ID="btnRload" Text="加载" runat="server" Icon="Reload" OnClick="btnRload_Click">
                    </f:Button>
                </Items>
            </f:FormRow>
        </Rows>
    </f:Form>
    <f:Grid ID="Grid2" Title="Grid2" AllowSorting="true" AllowPaging="false" BoxFlex="1"
        ShowHeader="False" runat="server" Height="325px" EnableCheckBoxSelect="True"
        DataKeyNames="Type,Assembly,Method" SortField="Name" EnableRowLines="True" OnSort="Grid2_Sort">
        <Toolbars>
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
        </Toolbars>
        <Columns>
            <f:RowNumberField ID="ctl48" ColumnID="Grid2_ctl48" />
            <f:BoundField Width="100px" SortField="Name" DataField="Name" HeaderText="名称" ID="ctl49"
                ColumnID="Grid2_ctl49" />
            <f:BoundField Width="150px" SortField="Assembly" DataField="Assembly" HeaderText="程序集"
                ID="ctl50" ColumnID="Grid2_ctl50" />
            <f:BoundField Width="240px" DataField="Description" HeaderText="说明" ID="ctl51" ColumnID="Grid2_ctl51" />
        </Columns>
    </f:Grid>
    </form>
</body>
</html>
