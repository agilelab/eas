<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BusinessWindow.aspx.cs" Inherits="EAS.WebShell.Biz.BusinessWindow" %>

<!DOCTYPE html>
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
            __doPostBack('', 'DoGrant$' + parm);
        }
    </script>
    <f:PageManager ID="PageManager1" AutoSizePanelID="form2" runat="server" />
    <f:Form ID="form2" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
        runat="server" LabelWidth="70px">
        <Toolbars>
            <f:Toolbar ID="Toolbar1" runat="server" Position="Bottom">
                <Items>
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
                        runat="server" ActiveTabIndex="2">
                        <Tabs>
                            <f:Tab ID="Tab1" Title="常规" BodyPadding="5px" Layout="Fit" runat="server">
                                <Items>
                                    <f:SimpleForm ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="SimpleForm1"
                                        LabelWidth="70px" runat="server">
                                        <Items>
                                            <f:TextBox ID="tbName" Label="模块名称" runat="server" Required="True" ShowRedStar="true">
                                            </f:TextBox>
                                            <f:Label ID="tbID" runat="server" Label="全局标识" Text="">
                                            </f:Label>
                                            <f:Label ID="tbType" runat="server" Label="对象类型" Text="">
                                            </f:Label>
                                            <f:Label ID="tbMethod" runat="server" Label="构件方法" Text="">
                                            </f:Label>
                                            <f:Label ID="tbAssembly" runat="server" Label="程序集" Text="">
                                            </f:Label>
                                            <f:Label ID="tbVersion" runat="server" Label="版本号" Text="">
                                            </f:Label>
                                            <f:Label ID="tbDeveloper" runat="server" Label="开发商" Text="">
                                            </f:Label>
                                            <f:TextBox ID="tbUrl" Label="Url路径" runat="server" Required="false">
                                            </f:TextBox>
                                            <f:NumberBox ID="nbSortCode" Label="排序码" Required="true" ShowRedStar="true" runat="server" />
                                        </Items>
                                    </f:SimpleForm>
                                </Items>
                            </f:Tab>
                            <f:Tab ID="Tab2" Title="安全" BodyPadding="5px" runat="server">
                                <Items>
                                    <f:Grid ID="Grid2" Title="Grid2" ShowBorder="true" BoxFlex="1" AllowPaging="false"
                                        AllowSorting="true" ShowHeader="false" runat="server" EnableCheckBoxSelect="True"
                                        Height="260px" DataKeyNames="PObject,Privileger,PType" IsDatabasePaging="true"
                                        SortField="Privileger" SortDirection="ASC" OnRowCommand="Grid2_RowCommand">
                                        <Toolbars>
                                            <f:Toolbar ID="Toolbar2" runat="server">
                                                <Items>
                                                    <f:Button ID="btnAdd" Text="添加" runat="server" Icon="New">
                                                    </f:Button>
                                                </Items>
                                            </f:Toolbar>
                                        </Toolbars>
                                        <Columns>
                                            <f:RowNumberField ID="ctl04" ColumnID="form2_ctl01_TabStrip1_Tab2_Grid2_ctl04" />
                                            <f:BoundField Width="150px" SortField="Privileger" DataField="Privileger" DataFormatString="{0}"
                                                HeaderText="名称" ID="ctl05" ColumnID="form2_ctl01_TabStrip1_Tab2_Grid2_ctl05" />
                                            <f:TemplateField Width="80px" HeaderText="类型" ID="ctl06" ColumnID="form2_ctl01_TabStrip1_Tab2_Grid2_ctl06">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# GetPTypeText(Eval("PType")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </f:TemplateField>
                                            <f:LinkButtonField TextAlign="Center" Width="35px" Icon="Delete" ToolTip="删除" ConfirmText="确定删除此权限？"
                                                ConfirmTarget="Top" CommandName="Delete" ID="ctl07" ColumnID="form2_ctl01_TabStrip1_Tab2_Grid2_ctl07" />
                                        </Columns>
                                    </f:Grid>
                                </Items>
                            </f:Tab>
                            <f:Tab ID="Tab3" Title="摘要" BodyPadding="5px" runat="server">
                                <Items>
                                    <f:TextArea ID="tbDescription" Height="225px" Width="460px" runat="server" Required="false"
                                        ShowLabel="False" />
                                </Items>
                            </f:Tab>
                        </Tabs>
                    </f:TabStrip>
                </Items>
            </f:FormRow>
        </Rows>
    </f:Form>
    <f:Window ID="Window1" Title="选择账户&角极" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" IsModal="true"
        Width="600px" Height="400px">
    </f:Window>
    </form>
</body>
