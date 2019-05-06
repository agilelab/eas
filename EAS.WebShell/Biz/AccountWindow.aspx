<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountWindow.aspx.cs"
    Inherits="EAS.WebShell.Biz.AccountWindow" %>

<!doctype html>
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
    <script>
        function DoSelectPrivilegers(parm) {
            __doPostBack('', 'DoAddRoles$' + parm);
        }
        function DoSelectModules(parm) {
            __doPostBack('', 'DoAddModules$' + parm);
        }
    </script>
    <f:PageManager ID="PageManager1" AutoSizePanelID="form2" runat="server" />
    <f:Form ID="form2" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
        runat="server" LabelWidth="65px">
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
                    <f:TabStrip ID="TabStrip1" ShowBorder="true" TabPosition="Top" EnableTabCloseMenu="false"
                        runat="server">
                        <Tabs>
                            <f:Tab ID="Tab1" Title="基本信息" BodyPadding="5px" Layout="Fit" runat="server">
                                <Items>
                                    <f:SimpleForm ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="SimpleForm1"
                                        LabelWidth="70px" runat="server">
                                        <Items>
                                            <f:TextBox ID="tbLoinID" Label="登录ID" runat="server" Required="True" ShowRedStar="true">
                                            </f:TextBox>
                                            <f:TextBox ID="tbName" runat="server" Label="账户名称" Required="true" ShowRedStar="true">
                                            </f:TextBox>
                                            <f:DropDownList Label="组织机构" AutoPostBack="false" Required="true" EnableSimulateTree="true"
                                                ShowRedStar="true" runat="server" ID="ddlOrgan">
                                            </f:DropDownList>
                                            <f:TextBox ID="tbPass" runat="server" Label="账号密码" Text="" TextMode="Password">
                                            </f:TextBox>
                                            <f:TextBox ID="tbPass2" runat="server" Label="密码确认" Text="" TextMode="Password">
                                            </f:TextBox>
                                            <f:TextBox ID="tbRowID" Label="原型信息" runat="server">
                                            </f:TextBox>
                                            <f:Label ID="lbLoginCount" Label="登录记数" runat="server" Text="0">
                                            </f:Label>
                                            <f:TextArea ID="tbDescription" Height="60px" Label="说明" runat="server" Required="false" />
                                        </Items>
                                    </f:SimpleForm>
                                    <f:Panel ID="Panel2" runat="server" Height="86px" Width="360px" ShowBorder="false"
                                        EnableCollapse="false" CssClass="mytable" Layout="Table" TableConfigColumns="2"
                                        ShowHeader="false">
                                        <Items>
                                            <f:CheckBox TableRowspan="1" TableColspan="2" ID="cbLeader" runat="server" ShowLabel="False"
                                                Text="机构主管">
                                            </f:CheckBox>
                                            <f:CheckBox TableRowspan="1" TableColspan="1" ID="cbLock" runat="server" ShowLabel="False"
                                                Text="帐户已经禁用">
                                            </f:CheckBox>
                                            <f:CheckBox TableRowspan="1" TableColspan="1" ID="cbLockPass" runat="server" ShowLabel="False"
                                                Text="用户不能修改密码">
                                            </f:CheckBox>
                                            <f:CheckBox TableRowspan="1" TableColspan="2" ID="cbLongPass" runat="server" ShowLabel="False"
                                                Text="密码永远不过期">
                                            </f:CheckBox>
                                        </Items>
                                    </f:Panel>
                                </Items>
                            </f:Tab>
                            <f:Tab ID="Tab2" Title="角色" BodyPadding="5px" runat="server">
                                <Items>
                                    <f:Grid ID="Grid2" Title="Grid2" ShowBorder="true" BoxFlex="1" AllowPaging="false"
                                        AllowSorting="true" ShowHeader="false" runat="server" EnableCheckBoxSelect="True"
                                        Height="270px" DataKeyNames="Name" IsDatabasePaging="true" SortField="Name" SortDirection="ASC"
                                        OnSort="Grid2_Sort" OnRowCommand="Grid2_RowCommand">
                                        <Toolbars>
                                            <f:Toolbar ID="Toolbar2" runat="server">
                                                <Items>
                                                    <f:Button ID="btnAddRole" Text="添加角色" runat="server" Icon="New">
                                                    </f:Button>
                                                </Items>
                                            </f:Toolbar>
                                        </Toolbars>
                                        <Columns>
                                            <f:RowNumberField />
                                            <f:BoundField Width="140px" SortField="Name" DataField="Name" HeaderText="名称" />
                                            <f:BoundField Width="220px" DataField="Description" HeaderText="说明" />
                                            <f:LinkButtonField TextAlign="Center" Width="35px" Icon="Delete" ToolTip="删除" ConfirmText="确定删除此较色？"
                                                ConfirmTarget="Top" CommandName="Delete" />
                                        </Columns>
                                    </f:Grid>
                                </Items>
                            </f:Tab>
                            <f:Tab ID="Tab3" Title="模块访问" BodyPadding="5px" runat="server">
                                <Items>
                                    <f:Grid ID="Grid1" Title="Grid2" ShowBorder="true" BoxFlex="1" AllowPaging="false"
                                        AllowSorting="true" ShowHeader="false" runat="server" EnableCheckBoxSelect="True"
                                        Height="270px" DataKeyNames="Guid" IsDatabasePaging="true" SortField="Name" SortDirection="ASC"
                                        OnSort="Grid1_Sort" OnRowCommand="Grid1_RowCommand">
                                        <Toolbars>
                                            <f:Toolbar ID="Toolbar3" runat="server">
                                                <Items>
                                                    <f:Button ID="btnAddModule" Text="添加模块" runat="server" Icon="New">
                                                    </f:Button>
                                                </Items>
                                            </f:Toolbar>
                                        </Toolbars>
                                        <Columns>
                                            <f:RowNumberField ID="RowNumberField1" />
                                            <f:BoundField Width="140px" SortField="Name" DataField="Name" HeaderText="名称" ID="Grid1_ctl00" />
                                            <f:BoundField Width="220px" SortField="Description" DataField="Description" HeaderText="说明"
                                                ID="Grid1_ctl01" />
                                            <f:LinkButtonField TextAlign="Center" Width="35px" Icon="Delete" ToolTip="删除" ConfirmText="确定删除此模块访问权？"
                                                ConfirmTarget="Top" CommandName="Delete" ID="Grid1_ctl02" />
                                        </Columns>
                                    </f:Grid>
                                </Items>
                            </f:Tab>
                        </Tabs>
                    </f:TabStrip>
                </Items>
            </f:FormRow>
        </Rows>
    </f:Form>
    <f:Window ID="Window1" Title="选择窗口" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" IsModal="true" Width="600px"
        Height="420px">
    </f:Window>
    <f:Window ID="Window2" Title="选择模块" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" IsModal="true" Width="610px"
        Height="443px">
    </f:Window>
    <asp:HiddenField ID="hfLoginID" runat="server" />
    </form>
</body>
