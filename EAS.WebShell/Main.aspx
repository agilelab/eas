<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="EAS.WebShell.Main" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>首页</title>
    <link href="res/css/main.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="regionPanel" runat="server" />
    <f:RegionPanel ID="regionPanel" ShowBorder="false" runat="server">
        <Regions>
            <f:Region ID="regionTop" CssClass="region-top" ShowBorder="false" ShowHeader="false"
                Position="Top" Layout="Fit" runat="server">
                <Content>
                    <div id="header">
                        <%--<img src="./res/images/login/logo.png" class="logo" alt="Logo" />--%>
                        <asp:HyperLink ID="linkSystemTitle" CssClass="title" runat="server" NavigateUrl="~/Main.aspx"></asp:HyperLink>
                    </div>
                </Content>
                <Toolbars>
                    <f:Toolbar ID="topRegionToolbar" Position="Bottom" CssClass="topbar" runat="server">
                        <Items>
                            
                            <f:ToolbarText ID="ToolbarText1" runat="server" Text="当前时间：">
                            </f:ToolbarText>
                            <f:ToolbarText ID="txtCurrentTime" runat="server">
                            </f:ToolbarText>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server" />
                            <f:Button ID="btnRefresh" runat="server" Icon="Reload" ToolTip="刷新主区域内容" EnablePostBack="false">
                            </f:Button>
                            <f:ToolbarSeparator ID="ToolbarSeparator3" runat="server" />
                            <f:ToolbarText ID="ToolbarText3" Text="&nbsp;&nbsp;主题：" runat="server">
                            </f:ToolbarText>
                            <f:Button ID="btnTheme" EnablePostBack="false" Icon="Theme" Text="主题" runat="server">
                            </f:Button>
                            <f:Button ID="btnHelp" EnablePostBack="false" Icon="Help" Text="帮助" runat="server">
                            </f:Button>
                            <f:ToolbarSeparator ID="ToolbarSeparator4" runat="server" />
                            <f:Button ID="btnExit" runat="server" Icon="UserRed" Text="安全退出" ConfirmText="确定退出系统？"
                                OnClick="btnExit_Click">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
            </f:Region>
            <f:Region ID="regionLeft" Split="true" EnableCollapse="true" Width="200px" ShowHeader="true"
                Title="系统菜单" Layout="Fit" Position="Left" runat="server">
            </f:Region>
            <f:Region ID="mainRegion" ShowHeader="false" Layout="Fit" Position="Center" runat="server">
                <Items>
                    <f:TabStrip ID="mainTabStrip" EnableTabCloseMenu="true" ShowBorder="false" runat="server">
                        <Tabs>
                            <f:Tab ID="Tab1" Title="首页" Layout="Fit" Icon="House" runat="server">
                                <Items>
                                    <f:ContentPanel ID="ContentPanel2" ShowBorder="false" BodyPadding="10px" ShowHeader="false"
                                        AutoScroll="true" runat="server">
                                        <p style="text-align: left;">
                                            <span style="line-height: 21px; font-family: verdana, 'courier new'; font-size: 14px;">
                                                &nbsp;</span></p>
                                        <p style="text-align: center; text-indent: 0px; margin: 7.05pt auto;">
                                            <span style="font-family: 微软雅黑; font-size: 22pt; font-weight: bold;">AgileEAS.NET SOA中间件平台简介</span></p>
                                        <p style="line-height: 18.75pt; text-indent: 24pt; margin: 9pt auto;">
                                            &nbsp;<span style="font-family: 'Times New Roman'; font-size: 10.5pt;">AgileEAS.NET
                                                SOA中间件平台</span><span style="font-family: 宋体; font-size: 10.5pt;">（简称EAS.NET</span><span
                                                    style="font-family: 'Times New Roman'; font-size: 10.5pt;">平台</span><span style="font-family: 宋体;
                                                        font-size: 10.5pt;">）</span><span style="font-family: 'Times New Roman'; font-size: 10.5pt;">是</span><span
                                                            style="font-family: 宋体; font-size: 10.5pt;">以&ldquo;</span><span style="font-family: 'Times New Roman';
                                                                font-size: 10.5pt;">敏捷并行开发</span><span style="font-family: 宋体; font-size: 10.5pt;">方法&rdquo;为其过程指导思想、基于</span><span
                                                                    style="font-family: 'Times New Roman'; font-size: 10.5pt;">Microsoft .Net</span><span
                                                                        style="font-family: 宋体; font-size: 10.5pt;">构件</span><span style="font-family: 'Times New Roman';
                                                                            font-size: 10.5pt;">技术</span><span style="font-family: 宋体; font-size: 10.5pt;">和模型驱动架构</span><span
                                                                                style="font-family: 'Times New Roman'; font-size: 10.5pt;">的</span><span style="font-family: 宋体;
                                                                                    font-size: 10.5pt;">企业级</span><span style="font-family: 'Times New Roman'; font-size: 10.5pt;">快速开发平台，</span><span
                                                                                        style="font-family: 宋体; font-size: 10.5pt;"><span style="font-family: 'Times New Roman';
                                                                                            font-size: 10.5pt;">AgileEAS.NET SOA中间件平台</span>使的构建企业级分布式应用系统变得简单，它提供了可灵活扩展应用架构，并且革命性的改变了软件的生产方式，</span><span
                                                                                                style="font-family: 'Times New Roman'; font-size: 10.5pt;">用于帮助中小型软件企业建立一条适合快速变化的开发团队，以达到节省开发成本、缩短开发时间，快速适应市场变化的目的</span><span
                                                                                                    style="font-family: 宋体; font-size: 10.5pt;">。</span></p>
                                        <p style="text-align: left; text-indent: 0px; margin: 0pt auto;">
                                            <span style="font-family: 微软雅黑; font-size: 16pt; font-weight: bold;">AgileEAS.NET</span><span
                                                style="font-family: 微软雅黑; font-size: 16pt; font-weight: bold;">介绍</span></p>
                                        <p style="line-height: 18.75pt; text-indent: 21pt; margin: 9pt auto;">
                                            <span style="font-family: 'Times New Roman'; font-size: 10.5pt;">AgileEAS.NET SOA中间件平台</span><span
                                                style="font-family: 宋体; font-size: 10.5pt;">是基于</span><span style="font-family: 'Times New Roman';
                                                    font-size: 10.5pt;">Microsoft .Net</span><span style="font-family: 宋体; font-size: 10.5pt;">构件、以&ldquo;敏捷并行开发方法&rdquo;为其过程指导思想、采用面向对象、构件复用技术实现企业级分布式</span><span
                                                        style="font-family: 'Times New Roman'; font-size: 10.5pt;">应用</span><span style="font-family: 宋体;
                                                            font-size: 10.5pt;">开发、运行、管理、监控、维护的中间件平台。这是企业应用软件开发的变革，一方面基于</span><span style="font-family: 'Times New Roman';
                                                                font-size: 10.5pt;">Microsoft .Net</span><span style="font-family: 宋体; font-size: 10.5pt;">构件平台，另一方面以更直观的API向最终应用提供开发、运维支撑。</span></p>
                                        <p style="line-height: 18.75pt; text-indent: 21pt; margin: 9pt auto;">
                                            <span style="font-family: 'Times New Roman'; font-size: 10.5pt;">AgileEAS.NET SOA中间件平台</span><span
                                                style="font-family: 宋体; font-size: 10.5pt;">将</span><span style="font-family: 'Times New Roman';
                                                    font-size: 10.5pt;">Microsoft .Net</span><span style="font-family: 宋体; font-size: 10.5pt;">构件技术、XML技术、分布式通信技术和可视化开发技术完美结合起来，为基于</span><span
                                                        style="font-family: 'Times New Roman'; font-size: 10.5pt;">Microsoft .Net</span><span
                                                            style="font-family: 宋体; font-size: 10.5pt;">平台之上的应用提供面向构件的分布式应用架构。使的企业应用系统的开发简化为应用构件单元的开发，业务构件单元做为应用系统的基本组织成元素，通过快速开发技术及构件组装技术使的应用系统可以快速高质量地搭建，建成的应用系统具有较强的可管理及可维护能力，同时拥有最强的需求变化响应能力，并通过构件积累为组织持续积累软件知财富。</span></p>
                                        <p style="line-height: 18.75pt; text-indent: 21pt; margin: 9pt auto;">
                                            <span style="font-family: 'Times New Roman'; font-size: 10.5pt;">基于<span style="font-family: 'Times New Roman';
                                                font-size: 10.5pt;">AgileEAS.NET SOA中间件平台</span></span><span style="font-family: 宋体;
                                                    font-size: 10.5pt;">开发的</span><span style="font-family: 'Times New Roman'; font-size: 10.5pt;">应用系统的各个业务功能子系统，在系统体系结构设计的过程中被设计成各个原子功能模块，各个子功能模块按照业务功能组织成单独的程序集文件，各子系统开发完成后，由<span
                                                        style="font-family: 'Times New Roman'; font-size: 10.5pt;">AgileEAS.NET SOA中间件平台</span>资源管理平台进行统一的集成部署。</span></p>
                                        <p style="line-height: 18.75pt; text-indent: 21pt; margin: 9pt auto;">
                                            <span style="font-family: 'Times New Roman'; font-size: 10.5pt;"><span style="font-family: 'Times New Roman';
                                                font-size: 10.5pt;">AgileEAS.NET SOA中间件平台</span>也是为应用系统开发而提供的一组低层功能集合及开发支撑平台，应用信息系统的开发建立在此平台之上，采用构件式、可复用开发，节省开发成本，</span><span
                                                    style="font-family: 宋体; font-size: 10.5pt;">快</span><span style="font-family: 'Times New Roman';
                                                        font-size: 10.5pt;">速</span><span style="font-family: 宋体; font-size: 10.5pt;">响应业务</span><span
                                                            style="font-family: 'Times New Roman'; font-size: 10.5pt;">。</span></p>
                                        <p style="line-height: 18.75pt; text-indent: 21pt; margin: 9pt auto;">
                                            <span style="font-family: 'Times New Roman'; font-size: 10.5pt;"><span style="font-family: 'Times New Roman';
                                                font-size: 10.5pt;">AgileEAS.NET SOA中间件平台</span>的核心思想是包含两点，一是基于Microsoft .Net构件技术的插件式开发，二是基于敏捷并行开发方法以的构件并行，即应用系统的构件同步并行开发，由平台进行总装集成。</span></p>
                                        <p style="line-height: 18.75pt; text-indent: 21pt; margin: 9pt auto;">
                                            <span style="font-family: 宋体; font-size: 10.5pt;"><span style="font-family: 'Times New Roman';
                                                font-size: 10.5pt;">AgileEAS.NET SOA中间件平台</span>同时也提供了一个中小软件企业的的开发管理解决方案，以敏捷并行开发方法为其过程理论依据、以<span
                                                    style="font-family: 'Times New Roman'; font-size: 10.5pt;">AgileEAS.NET SOA中间件平台</span>台为过程实践与指导、以AgilePM.NET为其项目管理工具，在开发技术、软件工程、技术架构、管理工具等方面帮助中小软件提供走向卓越。</span></p>
                                        <p style="text-align: center; text-indent: 0px; margin: 5px auto;">
                                            <img src="res/images/home/agile-dev.png" alt="" width="341"
                                                height="341" />&nbsp;</p>
                                        <p style="text-align: center; line-height: 18.75pt; text-indent: 0px; margin: 9pt auto;">
                                            &nbsp;</p>
                                        <p style="text-align: left; text-indent: 0px; margin: 0pt auto;">
                                            <span style="font-family: 微软雅黑; font-size: 16pt; font-weight: bold;">AgileEAS.NET SOA
                                                中间件平台</span><span style="font-family: 微软雅黑; font-size: 16pt; font-weight: bold;">结构</span></p>
                                        <p style="line-height: 18.75pt; text-indent: 21pt; margin: 9pt auto;">
                                            <span style="font-family: 'Times New Roman'; font-size: 10.5pt;">AgileEAS.NET SOA中间件平台</span><span
                                                style="font-family: 宋体; font-size: 10.5pt;">作为企业级分布式应用系统的中间件产品，提供了完整的</span><span
                                                    style="font-family: 'Times New Roman'; font-size: 10.5pt;">Microsoft .Ne</span><span
                                                        style="font-family: 宋体; font-size: 10.5pt;">t企业应用支撑，从应用的开发到运行、管理、监控的工具和环境支持，同时也为应用开发提供丰富的基础构件库，</span><span
                                                            style="font-family: 'Times New Roman'; font-size: 10.5pt;">AgileEAS.NET SOA中间件平台</span><span
                                                                style="font-family: 宋体; font-size: 10.5pt;">产品构成如下图所示：</span></p>
                                        <p style="text-align: center; line-height: 18.75pt; text-indent: 21pt; margin: 9pt auto;">
                                            <img src="res/images/home/agileeas.net.png" alt="" /></p>
                                        <p style="line-height: 18.75pt; text-indent: 21pt; margin: 9pt auto;">
                                            <span style="font-family: 宋体; font-size: 10.5pt;">从功能上看到，</span><span style="font-family: 'Times New Roman';
                                                font-size: 10.5pt;">AgileEAS.NET SOA中间件平台</span><span style="font-family: 宋体; font-size: 10.5pt;">主要包括8大部分，分别如下：</span></p>
                                        <p style="text-indent: 0px; margin: 0pt 2.5pt 0pt 21pt;">
                                            <span style="font-family: wingdings; font-size: 10.5pt; font-weight: bold;">n </span>
                                            <span style="font-family: 宋体; font-size: 10.5pt; font-weight: bold;">敏捷并发开发方法</span></p>
                                        <p style="line-height: 18.75pt; text-indent: 21pt; margin: 9pt auto;">
                                            <span style="font-family: 'Times New Roman'; font-size: 10.5pt;">敏捷并行开发方法</span><span
                                                style="font-family: 宋体; font-size: 10.5pt;">是<span style="font-family: 'Times New Roman';
                                                    font-size: 10.5pt;">AgileEAS.NET SOA中间件平台</span>的过程指导思想，其旨在提出一套符合国情的中小软件企业开发管理方法论：中国式的敏捷方法-</span><span
                                                        style="font-family: 'Times New Roman'; font-size: 10.5pt;">敏捷并行开发方法</span><span style="font-family: 宋体;
                                                            font-size: 10.5pt;">。</span></p>
                                        <p style="line-height: 18.75pt; text-indent: 21pt; margin: 9pt auto;">
                                            <span style="font-family: 'Times New Roman'; font-size: 10.5pt;">敏捷并行开发方法是</span><span
                                                style="font-family: 宋体; font-size: 10.5pt;">以</span><span style="font-family: 'Times New Roman';
                                                    font-size: 10.5pt;">软件构件技术等技术</span><span style="font-family: 宋体; font-size: 10.5pt;">为基础，</span><span
                                                        style="font-family: 'Times New Roman'; font-size: 10.5pt;">以平台＋插件化开发技术整合而出的一种快速开发模式；并行是指产品在生命周期内，项目管理过程、项目研发过程和机构支撑过程&ldquo;并行&rdquo;开展，项目研发过程中各个阶段有限度&ldquo;并行&rdquo;开展。</span></p>
                                        <p style="line-height: 18.75pt; text-indent: 21pt; margin: 9pt auto;">
                                            <span style="font-family: 'Times New Roman'; font-size: 10.5pt;">敏捷并行开发方法的基础是基于构件(插件)技术支持的并行，涉及软件开发的分析、设计、实现和测试等过程，
                                                一个完善的开发方法不单单是一个简单的理论基础，还需要相应的基础平台、项目管理工具、开发辅助工具才能构成一个完整的方法体系。</span></p>
                                        <p style="line-height: 18.75pt; text-indent: 21pt; margin: 9pt auto;">
                                            <span style="font-family: 'Times New Roman'; font-size: 10.5pt;">敏捷并行开发方法以<span style="font-family: 'Times New Roman';
                                                font-size: 10.5pt;">AgileEAS.NET SOA中间件平台</span>做为构件技术运行、管理平台，应用开发人员根据应用需要及<span
                                                    style="font-family: 'Times New Roman'; font-size: 10.5pt;">AgileEAS.NET SOA中间件平台</span>构件契约进行分析需要、设计开发应用构件，使用EAS.NET构件管理工具对所开发的构件进行总装集成和管理。</span></p>
                                        <p style="text-indent: 0px; margin: 0pt 2.5pt 0pt 21pt;">
                                            <span style="font-family: wingdings; font-size: 10.5pt; font-weight: bold;">n </span>
                                            <span style="font-family: 宋体; font-size: 10.5pt; font-weight: bold;">项目管理工具(AgilePM.NET)</span></p>
                                        <p style="line-height: 18.75pt; text-indent: 21pt; margin: 9pt auto;">
                                            <span style="font-family: 宋体; font-size: 10.5pt;"><span style="font-family: 'Times New Roman';
                                                font-size: 10.5pt;">AgileEAS.NET SOA中间件平台</span>提供了一套可选的项目管理软件-AgilePM.NET，AgilePM.NET软件基于<span
                                                    style="font-family: 'Times New Roman'; font-size: 10.5pt;">AgileEAS.NET SOA中间件平台</span>开发，采用先进的Silverlight技术做为其UI呈现，遵守<span
                                                        style="font-family: 'Times New Roman'; font-size: 10.5pt;">AgileEAS.NET SOA中间件平台</span>的构件标准。AgilePM.NET为中小软件企业提供组织级项目管理能力，提供诸如任务、计划、缺陷、需求等开发日常任务的管理</span><span
                                                            style="font-family: 'Times New Roman'; font-size: 10.5pt;">。</span></p>
                                        <p style="text-indent: 0px; margin: 0pt 2.5pt 0pt 21pt;">
                                            <span style="font-family: wingdings; font-size: 10.5pt; font-weight: bold;">n </span>
                                            <span style="font-family: 宋体; font-size: 10.5pt; font-weight: bold;">服务构件运行环境(</span><span
                                                style="font-family: 宋体; color: #000000; font-size: 12pt; font-weight: bold;">Server
                                            </span><span style="font-family: arial; color: #000000; font-size: 12pt; font-weight: bold;">
                                                &amp; Runtime</span><span style="font-family: 宋体; font-size: 10.5pt; font-weight: bold;">)</span></p>
                                        <p style="line-height: 18.75pt; text-indent: 21pt; margin: 9pt auto;">
                                            <span style="font-family: 宋体; font-size: 10.5pt;"><span style="font-family: 'Times New Roman';
                                                font-size: 10.5pt;">AgileEAS.NET SOA中间件平台</span>的服务构件运行环境运行于微软的IIS服务或者独立的业务服务实例之中，通过构建运行环境的构件引擎解析服务，对AgileEAS.NET
                                                SOA开发的应用系统之中的各种业务构件进行解析和运行，并对于业务构件的生命周期进行管理</span><span style="font-family: 'Times New Roman';
                                                    font-size: 10.5pt;">。</span></p>
                                        <p style="line-height: 18.75pt; text-indent: 21pt; margin: 9pt auto;">
                                            <span style="font-family: 宋体; font-size: 10.5pt;">在服务构件运行环境之中，还提供了SOA架构支撑以及企业服务总线，为基于SOA的分布式应用提供支持，以及基于SOA技术的SAAS引擎，为分布式应用托管及多租户分布式系统提供支持。</span></p>
                                        <p style="text-indent: 0px; margin: 0pt 2.5pt 0pt 21pt;">
                                            <span style="font-family: wingdings; font-size: 10.5pt; font-weight: bold;">n </span>
                                            <span style="font-family: 宋体; font-size: 10.5pt; font-weight: bold;">集成管理工具</span></p>
                                        <p style="line-height: 18.75pt; text-indent: 21pt; margin: 9pt auto;">
                                            <span style="font-family: 宋体; font-size: 10.5pt;"><span style="font-family: 'Times New Roman';
                                                font-size: 10.5pt;">AgileEAS.NET SOA中间件平台</span>集成管理工具是应用开发、运行、管理、监控、维护过程中需要的构件管理工具，其包含构件管理、应用装配、应用配置、安全审批、监控统计等各种服务。</span></p>
                                        <p style="text-indent: 0px; margin: 0pt 2.5pt 0pt 21pt;">
                                            <span style="font-family: wingdings; font-size: 10.5pt; font-weight: bold;">n </span>
                                            <span style="font-family: 宋体; font-size: 10.5pt; font-weight: bold;">业务构件仓库(</span><span
                                                style="font-family: arial; color: #000000; font-size: 12pt; font-weight: bold;">Component
                                                Library</span><span style="font-family: 宋体; font-size: 10.5pt; font-weight: bold;">)</span></p>
                                        <p style="line-height: 18.75pt; text-indent: 21pt; margin: 9pt auto;">
                                            <span style="font-family: 宋体; font-size: 10.5pt;">业务构件库是为了支撑应用系统快速开发和部署而提供的，具有高度复用的一组预制构件的集合，利用业务构件库中大量构件可以快速搭建应用系统，大大提高软件的可复用度，提高开发效率，同时通过对构件的管理可以建立一套针对构件的生产、改进、管理、沉淀和发展的完整的软件管理机制，使的软件企业组织级的软件知识沉淀可以通过构建库的形式展示和发展。</span></p>
                                        <p style="line-height: 18.75pt; text-indent: 21pt; margin: 9pt auto;">
                                            <span style="font-family: 宋体; font-size: 10.5pt;">在<span style="font-family: 'Times New Roman';
                                                font-size: 10.5pt;">AgileEAS.NET SOA中间件平台</span>应用开发过程之中，应用软件的开发即变成了业务构件的开发，最终的业务系统是由若干业务构件组织配置起来的一个整体，在业务构建仓库之中可以包含自己的业务构件，也可以包含第三方机构的符合<span
                                                    style="font-family: 'Times New Roman'; font-size: 10.5pt;">AgileEAS.NET SOA中间件平台</span>规范的业务构件。</span></p>
                                        <p style="line-height: 18.75pt; text-indent: 21pt; margin: 9pt auto;">
                                            <span style="font-family: 宋体; font-size: 10.5pt;"><span style="font-family: 'Times New Roman';
                                                font-size: 10.5pt;">AgileEAS.NET SOA中间件平台</span>自身之中，提供了一组企业应用常见的支撑和业务构件，比如ORM、IOC、AOP、Distributed、SOA、ESB、Workflow等支撑构件和比如报表管理、工作流管理、账户角色、组织机构、权限管理等基础业务构件。</span></p>
                                        <p style="line-height: 18.75pt; text-indent: 21pt; margin: 9pt auto;">
                                            <span style="font-family: 宋体; font-size: 10.5pt;">AgileEAS.NET SOA 中间件平台支撑多种主流数据库系统，目前<span
                                                style="font-family: 宋体; font-size: 10.5pt;">AgileEAS.NET SOA 中间件平台直接支持的有SqlServer、ORACLE、Mysql、Sqlite四种数据库，当然客户也可以实现参考<span
                                                    style="font-family: 宋体; font-size: 10.5pt;">AgileEAS.NET SOA实现更多的数据库接口</span></span>。</span></p>
                                        <p style="text-indent: 0px; margin: 0pt 2.5pt 0pt 21pt;">
                                            <span style="font-family: wingdings; font-size: 10.5pt; font-weight: bold;">n </span>
                                            <span style="font-family: 宋体; font-size: 10.5pt; font-weight: bold;">快速开发工具(</span><span
                                                style="font-family: arial; color: #000000; font-size: 12pt; font-weight: bold;">RD
                                                Tool</span><span style="font-family: 宋体; font-size: 10.5pt; font-weight: bold;">)</span></p>
                                        <p style="line-height: 18.75pt; text-indent: 21pt; margin: 9pt auto;">
                                            <span style="font-family: 宋体; font-size: 10.5pt;"><span style="font-family: 'Times New Roman';
                                                font-size: 10.5pt;">AgileEAS.NET SOA中间件平台</span>是一套企业级</span><span style="font-family: 'Times New Roman';
                                                    font-size: 10.5pt;">快速开发平台</span><span style="font-family: 宋体; font-size: 10.5pt;">，提供了对业务构件的可视化开发、调试、组装、发布和管理，另外基于模型驱动开发的思想，<span
                                                        style="font-family: 'Times New Roman'; font-size: 10.5pt;">AgileEAS.NET SOA中间件平台</span>提供了大量的图形化设计工具、生成器及快速开发向导功能，大大提高应用的开发效率，配合项目管理工具支持团队开发，以满足企业级应用软件的开发需求。</span></p>
                                        <p style="text-indent: 0px; margin: 0pt 2.5pt 0pt 21pt;">
                                            <span style="font-family: wingdings; font-size: 10.5pt; font-weight: bold;">n </span>
                                            <span style="font-family: 宋体; font-size: 10.5pt; font-weight: bold;">业务支撑工具(</span><span
                                                style="font-family: arial; color: #000000; font-size: 12pt; font-weight: bold;">Business
                                                Tool</span><span style="font-family: 宋体; font-size: 10.5pt; font-weight: bold;">)</span></p>
                                        <p style="line-height: 18.75pt; text-indent: 21pt; margin: 9pt auto;">
                                            <span style="font-family: 宋体; font-size: 10.5pt;">在企业级应用开发之中，<span style="font-family: 'Times New Roman';
                                                font-size: 10.5pt;">AgileEAS.NET SOA中间件平台</span>提供了诸如工作流、报表等业务基础支持工具，帮助企业快速构建基于工作流驱动以及BI应用系统。</span></p>
                                        <p style="line-height: 18.75pt; text-indent: 21pt; margin: 9pt auto;">
                                            <span style="font-family: 宋体; font-size: 10.5pt;"><span style="font-family: 'Times New Roman';
                                                font-size: 10.5pt;">AgileEAS.NET SOA</span>工作流包含业务流程定义工具、工作流引擎、工作流业务构件、工作流管理监控、工作流客户端等内容，工作流引擎基于</span><span
                                                    style="font-family: 'Times New Roman'; font-size: 10.5pt;">Microsoft </span>
                                            <span style="font-family: 宋体; font-size: 10.5pt;">WF，在遵守工作流标准的同时加入中国管理的特色，是适合国内中小企业的强大高效的的工作流产品。</span></p>
                                        <p style="line-height: 18.75pt; text-indent: 21pt; margin: 9pt auto;">
                                            <span style="font-family: 宋体; font-size: 10.5pt;"><span style="font-family: 'Times New Roman';
                                                font-size: 10.5pt;">AgileEAS.NET SOA 中间件平台整合了遵守Microsoft RDL语言标准的开源报表系统fyireporting，在原有英文版本的基础上进行了中文化和做了一些深度的整合，是非常适合的企业经量级报表系统。</span></span></p>
                                        <p style="line-height: 18.75pt; text-indent: 21pt; margin: 9pt auto;">
                                            <span style="font-family: 宋体; font-size: 10.5pt;"><span style="font-family: 'Times New Roman';
                                                font-size: 10.5pt;">AgileEAS.NET SOA 中间件平台最新版本5.1之中集成新优秀的中国式报表工具Grid++5.8，</span>并在其与AgileEAS.NET
                                                SOA中间件进行了深度的整合和扩展</span><span style="font-family: 宋体; font-size: 10.5pt;">，提供了更加强大和即便的企业轻量级报表系统。</span></p>
                                        <p style="text-indent: 0px; margin: 0pt 2.5pt 0pt 21pt;">
                                            <span style="font-family: wingdings; font-size: 10.5pt; font-weight: bold;">n </span>
                                            <span style="font-family: 宋体; font-size: 10.5pt; font-weight: bold;">客户构件运行容器(</span><span
                                                style="font-family: arial; color: #000000; font-size: 12pt; font-weight: bold;">Client
                                                &amp; </span><span style="font-family: 宋体; color: #000000; font-size: 12pt; font-weight: bold;">
                                                    Runtime</span><span style="font-family: 宋体; font-size: 10.5pt; font-weight: bold;">)</span></p>
                                        <p style="line-height: 18.75pt; text-indent: 21pt; margin: 9pt auto;">
                                            <span style="font-family: 宋体; font-size: 10.5pt;"><span style="font-family: 'Times New Roman';
                                                font-size: 10.5pt;">AgileEAS.NET SOA中间件平台</span>的客户构件运行环境用于承载业务展示构件，为UI构件提供一个运行容器，Win
                                                Container用于承载WinForm/WPF形式的业务展示构件，运行在独立的客户端实例之中，Web Container用于承载WebForm形式的业务展示构件，运行于IIS/Browser之中，Silverlight
                                                Container用于承载Silverlight形式的业务展示构件，运行于Silverlight ActiveX宿主。</span></p>
                                        <p style="line-height: 18.75pt; text-indent: 21pt; margin: 9pt auto;">
                                            <span style="font-family: 宋体; font-size: 10.5pt;">客户构件运行环境根据客户环境上下文中的身份认证信息及系统配置信息初始化客户构建运行环境，根据身份认证信息初始化业务构件并动态加载、运行业务构件。管理客户构件运行环境中的业务构件生存周期。</span></p>
                                        <p style="text-align: left; text-indent: 0px; margin: 0pt auto;">
                                            <span style="font-family: 微软雅黑; font-size: 16pt; font-weight: bold;">适用范围</span></p>
                                        <p style="line-height: 18.75pt; text-indent: 21pt; margin: 9pt auto;">
                                            <span style="font-family: 'Times New Roman'; font-size: 10.5pt;">AgileEAS.NET SOA中间件平台</span><span
                                                style="font-family: 宋体; font-size: 10.5pt;">适用于所有</span><span style="font-family: 宋体;
                                                    font-size: 10.5pt;">基于</span><span style="font-family: 'Times New Roman'; font-size: 10.5pt;">Microsoft
                                                        .Net</span><span style="font-family: 宋体; font-size: 10.5pt;">技术构架的分布式应用，它为企业级应用提供了基于完成的分布式多层模式的灵活可扩展的软件架构，并提供基于模型驱动的快速开发和部署、运维工具，以及基于软件工程思想支撑的</span><span
                                                            style="font-family: 宋体; font-size: 10.5pt;">AgilePM.NET项目管理体系，极大的提高了软件的有效生产效率和软件质量。</span></p>
                                        <p style="text-align: left; text-indent: 0px; margin: 0pt auto;">
                                            <span style="font-family: 微软雅黑; font-size: 16pt; font-weight: bold;">联系我们</span></p>
                                        <p style="line-height: 21px; text-indent: 21pt; margin: 0pt auto;">
                                            <span style="font-family: 'Times New Roman'; font-size: 10.5pt;">AgileEAS.NET SOA中间件平台</span><span
                                                style="font-family: 宋体; font-size: 10.5pt;">公开免费发行，无论是个人开发者还是企业开发者，都可以通过</span><span
                                                    style="font-family: verdana; font-size: 10.5pt;">AgileEAS.NET</span><span style="font-family: 宋体;
                                                        font-size: 10.5pt;">产品官方网站或敏捷软件工程实验网站下载获得，我们承诺</span><span style="font-family: 宋体;
                                                            font-size: 10.5pt;">针对产品本身</span><span style="font-family: 宋体; font-size: 10.5pt;">不收任何费用，并且持续的对产品进行升级。</span></p>
                                        <p style="line-height: 21px; text-indent: 21pt; margin: 0pt auto;">
                                            <span style="font-family: 宋体; font-size: 10.5pt;">敏捷软件工程实验室为企业提供专业的开发技术、开发管理咨询服务，我们为使用AgileEAS.NET平台的软件企业提供有偿的产品培训、方案设计、开发咨询服务；当然软件企业或者个人也可以公开的技术资料自行解决所遇到的相关问题，我们提供内部技术论坛、QQ群等多种技术交流环境。</span></p>
                                        <p style="line-height: 21px; text-indent: 21pt; margin: 0pt auto;">
                                            <span style="font-family: 宋体; font-size: 10.5pt;">联系人：魏琼东</span><span style="font-family: 宋体;
                                                font-size: 10.5pt;"> 电话：18629261335 QQ:47920381，邮件：</span><a href="mailto:mail.james@qq.com"><span
                                                    style="font-family: 宋体; color: #0000ff; font-size: 10pt; text-decoration: underline;">mail.james@qq.com</span></a></p>
                                        <p style="line-height: 21px; text-indent: 21pt; margin: 0pt auto;">
                                            <span style="font-family: 宋体; font-size: 10.5pt;">产品官</span><span style="font-family: 宋体;
                                                font-size: 10.5pt;">网</span><span style="font-family: verdana; font-size: 10.5pt;">：</span><a
                                                    href="http://www.smarteas.net/"><span style="font-family: verdana; color: #0000ff;
                                                        font-size: 10pt; text-decoration: underline;">http://www.smarteas.net</span></a></p>
                                        <p style="line-height: 21px; text-indent: 21pt; margin: 0pt auto;">
                                            <span style="font-family: 宋体; font-size: 10.5pt;">团队网站</span><span style="font-family: verdana;
                                                font-size: 10.5pt;">：</span><a href="http://www.agilelab.cn/"><span style="font-family: 宋体;
                                                    color: #0000ff; font-size: 10pt; text-decoration: underline;">http://www.agilelab.cn</span></a></p>
                                        <p style="line-height: 21px; text-indent: 21pt; margin: 0pt auto;">
                                            <span style="font-family: 宋体; font-size: 10.5pt;">官方博客:</span><a href="http://eastjade.cnblogs.com/"><span
                                                style="font-family: 宋体; color: #0000ff; font-size: 10pt; text-decoration: underline;">http://eastjade.cnblogs.com</span></a><span
                                                    style="font-family: 宋体; font-size: 10.5pt;">。</span></p>
                                        <p style="text-indent: 0px; margin: 5px auto;">
                                            &nbsp;</p>
                                        <p style="text-indent: 0px; margin: 5px auto;">
                                            PDF版本下载： <a href="http://www.smarteas.net/downloads/AgileEAS.NET%E6%95%8F%E6%8D%B7%E8%BD%AF%E4%BB%B6%E5%BC%80%E5%8F%91%E5%B9%B3%E5%8F%B0%E7%AE%80%E4%BB%8B.pdf">
                                                AgileEAS.NET敏捷软件开发平台简介.PDF</a></p>
                                    </f:ContentPanel>
                                </Items>
                            </f:Tab>
                        </Tabs>
                    </f:TabStrip>
                </Items>
            </f:Region>
            <f:Region ID="bottomPane" RegionPosition="Bottom" ShowBorder="false" ShowHeader="false"
                EnableCollapse="false" runat="server" Layout="Fit">
                <Items>
                    <f:ContentPanel ID="ContentPanel3" runat="server" ShowBorder="false" ShowHeader="false">
                        <table class="bottomtable" width="100%" cellpadding="5px" >
                            <tr>
                                <td><f:ToolbarText ID="txtUser" runat="server">
                            </f:ToolbarText>
                            <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server" />
                                    版本：<a target="_blank" href="http://www.agileeas.net/version">v5.1</a> &nbsp;&nbsp;
                                    <a target="_blank" href="http://jq.qq.com/?_wv=1027&k=XZ5bws">QQ公开群</a>
                                    在线人数:<a id="lbOnlineUserCount"></a>
                                </td>
                                <td style="text-align: center;">
                                    
                                </td>
                                <td style="width: 300px; text-align: right;">
                                    Copyright &copy; 2004-2015 敏捷软件工程实验室
                                </td>
                            </tr>
                        </table>
                    </f:ContentPanel>
                </Items>
            </f:Region>
        </Regions>
    </f:RegionPanel>
    <f:Window ID="Window1" runat="server" IsModal="true" Hidden="true" EnableIFrame="true"
        EnableResize="true" EnableMaximize="true" IFrameUrl="about:blank" Width="800px"
        Height="500px">
    </f:Window>
    <a id="toggleheader" href="javascript:;"></a>
    </form>
    <script src="res/js/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="res/js/main.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(
          function () {

              $.ajax({
                  url: "/Ajax/HelpHandler.ashx",
                  data: { cmd: "GetOnlineUserCount" },
                  cache: false,
                  async: false,
                  dataType: 'json',

                  success: function (data) {
                      if (data) {
                          var lbOnlineUserCount = document.getElementById("lbOnlineUserCount");
                          lbOnlineUserCount.innerHTML = data.toString();
                      }
                  },

                  error: function (msg) {
                      alert("取在线人数出错");
                  }
              });


          });
    
    </script>
</body>
</html>
