AgileEAS.NET SOA中间件平台简介
=====
  AgileEAS.NET SOA中间件平台（简称EAS.NET平台）是以“敏捷并行开发方法”为其过程指导思想、基于Microsoft .Net构件技术和模型驱动架构的企业级快速开发平台，AgileEAS.NET SOA中间件平台使的构建企业级分布式应用系统变得简单，它提供了可灵活扩展应用架构，并且革命性的改变了软件的生产方式，用于帮助中小型软件企业建立一条适合快速变化的开发团队，以达到节省开发成本、缩短开发时间，快速适应市场变化的目的。

AgileEAS.NET SOA 中间件平台介绍
-----------------------------------
  AgileEAS.NET SOA中间件平台是基于Microsoft .Net构件、以“敏捷并行开发方法”为其过程指导思想、采用面向对象、构件复用技术实现企业级分布式应用开发、运行、管理、监控、维护的中间件平台。这是企业应用软件开发的变革，一方面基于Microsoft .Net构件平台，另一方面以更直观的API向最终应用提供开发、运维支撑。

  AgileEAS.NET SOA中间件平台将Microsoft .Net构件技术、XML技术、分布式通信技术和可视化开发技术完美结合起来，为基于Microsoft .Net平台之上的应用提供面向构件的分布式应用架构。使的企业应用系统的开发简化为应用构件单元的开发，业务构件单元做为应用系统的基本组织成元素，通过快速开发技术及构件组装技术使的应用系统可以快速高质量地搭建，建成的应用系统具有较强的可管理及可维护能力，同时拥有最强的需求变化响应能力，并通过构件积累为组织持续积累软件知财富。

  基于AgileEAS.NET SOA中间件平台开发的应用系统的各个业务功能子系统，在系统体系结构设计的过程中被设计成各个原子功能模块，各个子功能模块按照业务功能组织成单独的程序集文件，各子系统开发完成后，由AgileEAS.NET SOA中间件平台资源管理平台进行统一的集成部署。
  
  AgileEAS.NET SOA中间件平台也是为应用系统开发而提供的一组低层功能集合及开发支撑平台，应用信息系统的开发建立在此平台之上，采用构件式、可复用开发，节省开发成本，快速响应业务。
  
  AgileEAS.NET SOA中间件平台的核心思想是包含两点，一是基于Microsoft .Net构件技术的插件式开发，二是基于敏捷并行开发方法以的构件并行，即应用系统的构件同步并行开发，由平台进行总装集成。
  
  AgileEAS.NET SOA中间件平台同时也提供了一个中小软件企业的的开发管理解决方案，以敏捷并行开发方法为其过程理论依据、以AgileEAS.NET SOA中间件平台台为过程实践与指导、以AgilePM.NET为其项目管理工具，在开发技术、软件工程、技术架构、管理工具等方面帮助中小软件提供走向卓越。


AgileEAS.NET SOA 中间件平台结构
-----------------------------------
  AgileEAS.NET SOA中间件平台作为企业级分布式应用系统的中间件产品，提供了完整的Microsoft .Net企业应用支撑，从应用的开发到运行、管理、监控的工具和环境支持，同时也为应用开发提供丰富的基础构件库，AgileEAS.NET SOA中间件平台产品构成如下图所示：

![github](https://github.com/agilelab/eas/blob/master/documents/AgileEAS.NET.png "agileeas.net")  

从功能上看到，AgileEAS.NET SOA中间件平台主要包括8大部分，分别如下：

n 敏捷并发开发方法

  敏捷并行开发方法是AgileEAS.NET SOA中间件平台的过程指导思想，其旨在提出一套符合国情的中小软件企业开发管理方法论：中国式的敏捷方法-敏捷并行开发方法。

  敏捷并行开发方法是以软件构件技术等技术为基础，以平台＋插件化开发技术整合而出的一种快速开发模式；并行是指产品在生命周期内，项目管理过程、项目研发过程和机构支撑过程“并行”开展，项目研发过程中各个阶段有限度“并行”开展。

  敏捷并行开发方法的基础是基于构件(插件)技术支持的并行，涉及软件开发的分析、设计、实现和测试等过程， 一个完善的开发方法不单单是一个简单的理论基础，还需要相应的基础平台、项目管理工具、开发辅助工具才能构成一个完整的方法体系。

  敏捷并行开发方法以AgileEAS.NET SOA中间件平台做为构件技术运行、管理平台，应用开发人员根据应用需要及AgileEAS.NET SOA中间件平台构件契约进行分析需要、设计开发应用构件，使用EAS.NET构件管理工具对所开发的构件进行总装集成和管理。

n 项目管理工具(AgilePM.NET)

  AgileEAS.NET SOA中间件平台提供了一套可选的项目管理软件-AgilePM.NET，AgilePM.NET软件基于AgileEAS.NET SOA中间件平台开发，采用先进的Silverlight技术做为其UI呈现，遵守AgileEAS.NET SOA中间件平台的构件标准。AgilePM.NET为中小软件企业提供组织级项目管理能力，提供诸如任务、计划、缺陷、需求等开发日常任务的管理。

n 服务构件运行环境(Server & Runtime)

  AgileEAS.NET SOA中间件平台的服务构件运行环境运行于微软的IIS服务或者独立的业务服务实例之中，通过构建运行环境的构件引擎解析服务，对AgileEAS.NET SOA开发的应用系统之中的各种业务构件进行解析和运行，并对于业务构件的生命周期进行管理。

  在服务构件运行环境之中，还提供了SOA架构支撑以及企业服务总线，为基于SOA的分布式应用提供支持，以及基于SOA技术的SAAS引擎，为分布式应用托管及多租户分布式系统提供支持。

n 集成管理工具

  AgileEAS.NET SOA中间件平台集成管理工具是应用开发、运行、管理、监控、维护过程中需要的构件管理工具，其包含构件管理、应用装配、应用配置、安全审批、监控统计等各种服务。

n 业务构件仓库(Component Library)

  业务构件库是为了支撑应用系统快速开发和部署而提供的，具有高度复用的一组预制构件的集合，利用业务构件库中大量构件可以快速搭建应用系统，大大提高软件的可复用度，提高开发效率，同时通过对构件的管理可以建立一套针对构件的生产、改进、管理、沉淀和发展的完整的软件管理机制，使的软件企业组织级的软件知识沉淀可以通过构建库的形式展示和发展。
  在AgileEAS.NET SOA中间件平台应用开发过程之中，应用软件的开发即变成了业务构件的开发，最终的业务系统是由若干业务构件组织配置起来的一个整体，在业务构建仓库之中可以包含自己的业务构件，也可以包含第三方机构的符合AgileEAS.NET SOA中间件平台规范的业务构件。
AgileEAS.NET SOA中间件平台自身之中，提供了一组企业应用常见的支撑和业务构件，比如ORM、IOC、AOP、Distributed、SOA、ESB、Workflow等支撑构件和比如报表管理、工作流管理、账户角色、组织机构、权限管理等基础业务构件。
  AgileEAS.NET SOA 中间件平台支撑多种主流数据库系统，目前AgileEAS.NET SOA 中间件平台直接支持的有SqlServer、ORACLE、Mysql、Sqlite四种数据库，当然客户也可以实现参考AgileEAS.NET SOA实现更多的数据库接口。

n 快速开发工具(RD Tool)

  AgileEAS.NET SOA中间件平台是一套企业级快速开发平台，提供了对业务构件的可视化开发、调试、组装、发布和管理，另外基于模型驱动开发的思想，AgileEAS.NET SOA中间件平台提供了大量的图形化设计工具、生成器及快速开发向导功能，大大提高应用的开发效率，配合项目管理工具支持团队开发，以满足企业级应用软件的开发需求。

n 业务支撑工具(Business Tool)

  在企业级应用开发之中，AgileEAS.NET SOA中间件平台提供了诸如工作流、报表等业务基础支持工具，帮助企业快速构建基于工作流驱动以及BI应用系统。

  AgileEAS.NET SOA工作流包含业务流程定义工具、工作流引擎、工作流业务构件、工作流管理监控、工作流客户端等内容，工作流引擎基于Microsoft WF，在遵守工作流标准的同时加入中国管理的特色，是适合国内中小企业的强大高效的的工作流产品。

  AgileEAS.NET SOA 中间件平台整合了遵守Microsoft RDL语言标准的开源报表系统fyireporting，在原有英文版本的基础上进行了中文化和做了一些深度的整合，是非常适合的企业经量级报表系统。

  AgileEAS.NET SOA 中间件平台最新版本5.1之中集成新优秀的中国式报表工具Grid++5.8，并在其与AgileEAS.NET SOA中间件进行了深度的整合和扩展，提供了更加强大和即便的企业轻量级报表系统。

n 客户构件运行容器(Client & Runtime)

  AgileEAS.NET SOA中间件平台的客户构件运行环境用于承载业务展示构件，为UI构件提供一个运行容器，Win Container用于承载WinForm/WPF形式的业务展示构件，运行在独立的客户端实例之中，Web Container用于承载WebForm形式的业务展示构件，运行于IIS/Browser之中，Silverlight Container用于承载Silverlight形式的业务展示构件，运行于Silverlight ActiveX宿主。

  客户构件运行环境根据客户环境上下文中的身份认证信息及系统配置信息初始化客户构建运行环境，根据身份认证信息初始化业务构件并动态加载、运行业务构件。管理客户构件运行环境中的业务构件生存周期。

AgileEAS.NET SOA 中间件适用范围
-----------------------------------

  AgileEAS.NET SOA中间件平台适用于所有基于Microsoft .Net技术构架的分布式应用，它为企业级应用提供了基于完成的分布式多层模式的灵活可扩展的软件架构，并提供基于模型驱动的快速开发和部署、运维工具，以及基于软件工程思想支撑的AgilePM.NET项目管理体系，极大的提高了软件的有效生产效率和软件质量。

AgileEAS.NET SOA 中间件开发之旅
-----------------------------------

   “接触AgileEAS.NET SOA 中间件平台（以下简称AgileEAS.NET SOA中间件平台）有4个多月时间，经过试用认为可以把它作为一个开发的基础平台，开发团队可以把开发的重点放在需求的把控和项目的交付上，从而节省大量的时间，提高项目的开发、交付效率，降低对项目团队的深层技术要求，更重要的一点是AgileEAS.NET SOA中间件平台的开发团队持续不断地维护和改进平台以及对反馈问题的快速反应，使我对平台的持续发展充满信心。
   由于AgileEAS.NET SOA中间件平台的资料比较多，需要花费较多的时间才能够初步了解平台并能够使用平台开始开发，所以，在此，我把学习AgileEAS.NET SOA中间件平台的过程总结了一下，形成本篇短文，希望能够让初次接触AgileEAS.NET SOA中间件平台的朋友能够用30分钟时间，跟着本文案例实际操作一遍，对AgileEAS.NET SOA中间件平台有一个真实的体验，节省大家的时间，也算对AgileEAS.NET SOA中间件平台的一点回报，希望有更多的人了解它的优势并真正用好它，为AgileEAS.NET SOA中间件平台的使用者带来价值，也为AgileEAS.NET SOA中间件平台的开发者带来效益，最终实现合作共赢的美好结局。”
    
  以上内容摘自一位应用AgileEAS.NET SOA 中间件平台进行管理应用系统开发的朋友所基于应用开发者的角度出发写写的零基础30分钟开启你的[AgileEAS.NET SOA 中间件平台开发之旅.pdf](https://github.com/agilelab/eas/blob/master/documents/%E9%9B%B6%E5%9F%BA%E7%A1%8030%E5%88%86%E9%92%9F%E5%BC%80%E5%90%AF%E4%BD%A0%E7%9A%84AgileEAS.NET%20SOA%20%E4%B8%AD%E9%97%B4%E4%BB%B6%E5%B9%B3%E5%8F%B0%E5%BC%80%E5%8F%91%E4%B9%8B%E6%97%85.pdf)<br />

  AgileEAS.NET SOA 中间件平台为应用开发者提供了教程、案例、视频等直观简介的方式为应用开发提供相应的支持，应用开发者可能通过以下的“AgileEAS.NET SOA 中间件最新下载”获得相应的内容。

AgileEAS.NET SOA 中间件最新下载
-----------------------------------

1、AgileEAS.NET SOA 5.2最新版本下载
      包含AgileEAS.NET SOA 平台的最新程序集，SQLServer\Oracle\MySQL\SQLite数据库的初始化工上，平台最新文档。

      直接下载：AgileEAS.NET SOA 5.2 下载，http://118.24.209.136/downloads/eas/agileeas.net.5.rar。

      SVN更新：https://118.24.209.136/svn/eas/5.0，登录用户:eas，密码eas.

      AgileEAS.NET SOA 中间件平台管理员：Administrator，登录密码sa。

2、案例源代码-药店管理系统
      基于AgileEAS.NET平台开发的一个药店管理系统，包含所有源代码、文档、数据库，可直接运行，有关这个案例的文章请参考AgileEAS.NET平台开发Step By Step系列-药店系统-索引。

      直接下载：AgileEAS.NET SOA  案例（药店管理系统）源码下载，http://118.24.209.136/downloads/eas/drugshop.rar。

      SVN更新：https://118.24.209.136/svn/drugshop，登录用户:eas，密码eas.

      源代码下载,最好请通过SVN进行下载。

      管理员：Administrator，登录密码sa。

3、案例源代码-小型分销EPR系统
      基于AgileEAS.NET平台开发的一个小型分销ERP系统，包含所有源代码、文档、数据库，可直接运行，有关这个案例的文章请参考AgileEAS.NET平台开发Step By Step系列-药店系统-索引。

      直接下载：AgileEAS.NET SOA 5.0 案例（分诊ERP系统）源码下载，http://118.24.209.136/downloads/eas/smarterp.rar。

      SVN更新：https://118.24.209.136/svn/smarterp，登录用户:eas，密码eas.
      
      github:https://github.com/agilelab/eas

      源代码下载,最好请通过SVN进行下载。

      管理员：Administrator，登录密码sa。

      ERP操作员：erp-admin，登录密码为空。

4、案例源代码-Northwind
        基于AgileEAS.NET平台开发的一个简单的MIS项目，Northwind案例（源自于SQLServer数据库系统并且做了一些改动），包含所有源代码、文档、数据库，可直接运行，有关这个案例的文章请参考3小时搞定一个简单的MIS系统案例Northwind，有视频、有源代码下载、有真相。

     直接下载：AgileEAS.NET SOA  案例（Northwind）源码下载，http://118.24.209.136/downloads/eas/Northwind.rar。

     SVN更新：https://118.24.209.136/svn/Northwind，登录用户:eas，密码eas.
     
     github:https://github.com/agilelab/northwind

     源代码下载,最好请通过SVN进行下载。

     Administrator,james,demo1用户登录密码均为：sa

5、案例源代码-在线聊天室系统
        基于AgileEAS.NET SOA 中间件平台Socket通信框架的一个多人在线聊天室系统，包括所有的源代码、文档、数据库，可直接运行，有关于本案例请参考AgileEAS.NET SOA 中间件平台.Net Socket通信框架-完整应用例子-在线聊天室系统-下载配置、AgileEAS.NET SOA 中间件平台.Net Socket通信框架-完整应用例子-在线聊天室系统-代码解析。

      直接下载：AgileEAS.NET SOA 中间件平台Socket通信框架 案例（在线聊天室系统）源码下载，http://118.24.209.136/downloads/eas/Chat.src.rar。

      SVN更新：https://118.24.209.136/svn/chat，登录用户:eas，密码eas.

      源代码下载,请通过进行下载。

      同时，我们在服务器上部署好了一套可运行的案例，客户端下载：http://118.24.209.136/downloads/eas/examples/chat.client.rar。

6、Grid++报表
      最新版本的AgileEAS.NET SOA 平台中集成了Grid++报表，如需要使用Grid++报表进行开发，请先下载Grid++报表，下载地址：http://www.rubylong.cn/Download/Grid++Report5.zip。

7、AgileEAS.NET SOA 5 培训视频
      2014-04-13（星期天）下午2：30-6：30进行的AgileEAS.NET SOA中间件平台配置 及 Northwind案例培训开发所录制的视频，共有2.1G的高清视屏，视频时长4小时，请大家耐性观看。

     视频下载地址，百度云链接：https://pan.baidu.com/s/1V0akBerwzdPNm6ug2nffzw 提取码：4w9f

     1.AgileEASNET SOA 5平台配置.avi

     2.AgileEAS.NET SOA 5元数据设计器.avi

     3.Northwind.WinForm案例.第1段.avi

     4.Northwind.WinForm案例.第2段.avi

     5.Northwind.WinForm案例.第3段.avi

     6.Northwind.WinForm案例.第4段.avi

8、视频会议培训下载
      包含历次的视频会议培训过程所录制的学习视频，请大家及时下载。

      第一辑-AgileEAS.NET平台介绍及药品系统的SAAS搭建演练：下载地址：http://118.24.209.136/downloads/video/AgileEAS.NET 4.0 Video1.rar

      第二辑-简单插件开发应用演练：下载地址：http://118.24.209.136/downloads/video/AgileEAS.NET 4.0 Video2.rar

      第三辑 插件的安装、配置以及账户权限系统演练.wmv http://118.24.209.136/downloads/video/AgileEAS.NET 4.0 Video3.rar

联系我们
-----------------------------------

AgileEAS.NET SOA中间件平台公开免费发行，无论是个人开发者还是企业开发者，都可以通过AgileEAS.NET产品官方网站或敏捷软件工程实验网站下载获得，我们承诺针对产品本身不收任何费用，并且持续的对产品进行升级。

敏捷软件工程实验室为企业提供专业的开发技术、开发管理咨询服务，我们为使用AgileEAS.NET平台的软件企业提供有偿的产品培训、方案设计、开发咨询服务；当然软件企业或者个人也可以公开的技术资料自行解决所遇到的相关问题，我们提供内部技术论坛、QQ群等多种技术交流环境。

联系人：魏琼东 电话：18629261335 QQ:47920381，邮件：mail.james@qq.com

产品官网：http://www.agileeas.net

团队网站：http://www.agilelab.cn

官方博客:http://eastjade.cnblogs.com。


PDF版本：[AgileEAS.NET SOA中间件平台简介.PDF](https://github.com/agilelab/eas/blob/master/documents/AgileEAS.NET%20SOA%E4%B8%AD%E9%97%B4%E4%BB%B6%E5%B9%B3%E5%8F%B0%E7%AE%80%E4%BB%8B.pdf)<br />   
