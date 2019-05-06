var conWeekend = 3;  // 周末颜色显示: 1=黑色, 2=绿色, 3=红色, 4=隔周休


/*****************************************************************************
                                   日期资料
*****************************************************************************/

var lunarInfo=new Array(
0x4bd8,0x4ae0,0xa570,0x54d5,0xd260,0xd950,0x5554,0x56af,0x9ad0,0x55d2,
0x4ae0,0xa5b6,0xa4d0,0xd250,0xd295,0xb54f,0xd6a0,0xada2,0x95b0,0x4977,
0x497f,0xa4b0,0xb4b5,0x6a50,0x6d40,0xab54,0x2b6f,0x9570,0x52f2,0x4970,
0x6566,0xd4a0,0xea50,0x6a95,0x5adf,0x2b60,0x86e3,0x92ef,0xc8d7,0xc95f,
0xd4a0,0xd8a6,0xb55f,0x56a0,0xa5b4,0x25df,0x92d0,0xd2b2,0xa950,0xb557,
0x6ca0,0xb550,0x5355,0x4daf,0xa5b0,0x4573,0x52bf,0xa9a8,0xe950,0x6aa0,
0xaea6,0xab50,0x4b60,0xaae4,0xa570,0x5260,0xf263,0xd950,0x5b57,0x56a0,
0x96d0,0x4dd5,0x4ad0,0xa4d0,0xd4d4,0xd250,0xd558,0xb540,0xb6a0,0x95a6,
0x95bf,0x49b0,0xa974,0xa4b0,0xb27a,0x6a50,0x6d40,0xaf46,0xab60,0x9570,
0x4af5,0x4970,0x64b0,0x74a3,0xea50,0x6b58,0x5ac0,0xab60,0x96d5,0x92e0,
0xc960,0xd954,0xd4a0,0xda50,0x7552,0x56a0,0xabb7,0x25d0,0x92d0,0xcab5,
0xa950,0xb4a0,0xbaa4,0xad50,0x55d9,0x4ba0,0xa5b0,0x5176,0x52bf,0xa930,
0x7954,0x6aa0,0xad50,0x5b52,0x4b60,0xa6e6,0xa4e0,0xd260,0xea65,0xd530,
0x5aa0,0x76a3,0x96d0,0x4afb,0x4ad0,0xa4d0,0xd0b6,0xd25f,0xd520,0xdd45,
0xb5a0,0x56d0,0x55b2,0x49b0,0xa577,0xa4b0,0xaa50,0xb255,0x6d2f,0xada0,
0x4b63,0x937f,0x49f8,0x4970,0x64b0,0x68a6,0xea5f,0x6b20,0xa6c4,0xaaef,
0x92e0,0xd2e3,0xc960,0xd557,0xd4a0,0xda50,0x5d55,0x56a0,0xa6d0,0x55d4,
0x52d0,0xa9b8,0xa950,0xb4a0,0xb6a6,0xad50,0x55a0,0xaba4,0xa5b0,0x52b0,
0xb273,0x6930,0x7337,0x6aa0,0xad50,0x4b55,0x4b6f,0xa570,0x54e4,0xd260,
0xe968,0xd520,0xdaa0,0x6aa6,0x56df,0x4ae0,0xa9d4,0xa4d0,0xd150,0xf252,
0xd520);

var solarMonth=new Array(31,28,31,30,31,30,31,31,30,31,30,31);
var Gan=new Array("甲","乙","丙","丁","戊","己","庚","辛","壬","癸");
var Zhi=new Array("子","丑","寅","卯","辰","巳","午","未","申","酉","戌","亥");
var Animals=new Array("鼠","牛","虎","兔","龙","蛇","马","羊","猴","鸡","狗","猪");
var solarTerm = new Array("小寒","大寒","立春","雨水","惊蛰","春分","清明","谷雨","立夏","小满","芒种","夏至","小暑","大暑","立秋","处暑","白露","秋分","寒露","霜降","立冬","小雪","大雪","冬至");
var sTermInfo = new Array(0,21208,42467,63836,85337,107014,128867,150921,173149,195551,218072,240693,263343,285989,308563,331033,353350,375494,397447,419210,440795,462224,483532,504758);
var nStr1 = new Array('日','一','二','三','四','五','六','七','八','九','十');
var nStr2 = new Array('初','十','廿','卅','□');
var monthName = new Array("JAN","FEB","MAR","APR","MAY","JUN","JUL","AUG","SEP","OCT","NOV","DEC");

//公历节日 *表示放假日
var sFtv = new Array(
"0101*新年元旦",
"0202 世界湿地日",
"0207 国际声援南非日",
"0210 国际气象节",
"0214 情人节",
"0301 国际海豹日",
"0303 全国爱耳日",
"0308 国际妇女节",
"0312 植树节 孙中山逝世纪念日",
"0314 国际警察日",
"0315 国际消费者权益日",
"0317 中国国医节 国际航海日",
"0321 世界森林日 消除种族歧视国际日",
"0321 世界儿歌日",
"0322 世界水日",
"0323 世界气象日",
"0324 世界防治结核病日",
"0325 全国中小学生安全教育日",
"0330 巴勒斯坦国土日",
"0401 愚人节 全国爱国卫生运动月(四月) 税收宣传月(四月)",
"0407 世界卫生日",
"0422 世界地球日",
"0423 世界图书和版权日",
"0424 亚非新闻工作者日",
"0501 国际劳动节",
"0504 中国五四青年节",
"0505 碘缺乏病防治日",
"0508 世界红十字日",
"0512 国际护士节",
"0515 国际家庭日",
"0517 世界电信日",
"0518 国际博物馆日",
"0520 全国学生营养日",
"0523 国际牛奶日",
"0531 世界无烟日", 
"0601 国际儿童节",
"0605 世界环境日",
"0606 全国爱眼日",
"0617 防治荒漠化和干旱日",
"0623 国际奥林匹克日",
"0625 全国土地日",
"0626 国际反毒品日",
"0701 中国共产党建党日 世界建筑日",
"0702 国际体育记者日 精品推介站(http://www.21softs.com/)正式对外开放纪念日",
"0707 中国人民抗日战争纪念日",
"0711 世界人口日",
"0730 非洲妇女日",
"0801 中国建军节",
"0808 中国男子节(爸爸节)",
"0815 日本正式宣布无条件投降日",
"0908 国际扫盲日 国际新闻工作者日",
"0910 教师节",
"0914 世界清洁地球日",
"0916 国际臭氧层保护日",
"0918 九·一八事变纪念日",
"0920 国际爱牙日",
"0927 世界旅游日",
"1001*国庆节 世界音乐日 国际老人节",
"1001 国际音乐日",
"1002 国际和平与民主自由斗争日",
"1004 世界动物日",
"1008 全国高血压日",
"1008 世界视觉日",
"1009 世界邮政日 万国邮联日",
"1010 辛亥革命纪念日 世界精神卫生日",
"1013 世界保健日 国际教师节",
"1014 世界标准日",
"1015 国际盲人节(白手杖节)",
"1016 世界粮食日",
"1017 世界消除贫困日",
"1022 世界传统医药日",
"1024 联合国日 世界发展信息日",
"1031 世界勤俭日",
"1107 十月社会主义革命纪念日",
"1108 中国记者日",
"1109 全国消防安全宣传教育日",
"1110 世界青年节",
"1111 国际科学与和平周(本日所属的一周)",
"1112 孙中山诞辰纪念日",
"1114 世界糖尿病日",
"1117 国际大学生节 世界学生节",
"1121 世界问候日 世界电视日",
"1129 国际声援巴勒斯坦人民国际日",
"1201 世界艾滋病日",
"1203 世界残疾人日",
"1205 国际经济和社会发展志愿人员日",
"1208 国际儿童电视日",
"1209 世界足球日",
"1210 世界人权日",
"1212 西安事变纪念日",
"1213 南京大屠杀(1937年)纪念日！紧记血泪史！",
"1221 国际篮球日",
"1224 平安夜",
"1225 圣诞节",
"1229 国际生物多样性日");

//某月的第几个星期几。 5,6,7,8 表示到数第 1,2,3,4 个星期几
var wFtv = new Array(
"0110 黑人日",
"0150 世界麻风日", //一月的最后一个星期日（月倒数第一个星期日）
"0520 国际母亲节",
"0530 全国助残日",
"0630 父亲节",
"0911 劳动节",
"0932 国际和平日",
"0940 国际聋人节 世界儿童日",
"0950 世界海事日",
"1011 国际住房日",
"1013 国际减轻自然灾害日(减灾日)",
"1144 感恩节");

//农历节日
var lFtv = new Array(
"0101*春节",
"0115 元宵节",
"0202 龙抬头节",
"0323 妈祖生辰 (天上圣母诞辰)",
"0505 端午节",
"0707 七七中国情人节",
"0815 中秋节",
"0909 重阳节",
"1208 腊八节",
"1223 灶君(祭灶)节",
"0100*除夕");

//世界时间资料
var timeData = {
"Asia               亚洲": {   //----------------------------------------------
"Brunei             文莱    ":["+0800","","斯里巴加湾市"],
"Burma              缅甸    ":["+0630","","仰光"],
"Cambodia           柬埔寨  ":["+0700","","金边"],
"China              中国    ":["+0800","","北京、重庆、上海、天津"],
"China(HK,Macau)    中国    ":["+0800","","香港、澳门特区"],
"China(TaiWan)      中国    ":["+0800","","台北、高雄"],
"China(Urumchi)     中国    ":["+0700","","乌鲁木齐"],
"Indonesia          印尼    ":["+0700","","雅加达"],
"Japan              日本    ":["+0900","","东京、大阪、札幌"],
"Korea              韩国    ":["+0900","","汉城"],
"Laos               老挝    ":["+0700","","万象"],
"Malaysia           马来西亚":["+0800","","吉隆坡"],
"Mongolia           蒙古    ":["+0800","03L03|09L03","乌兰巴托、库伦"],
"Philippines        菲律宾  ":["+0800","04F53|10F53","马尼拉"],
"Russia(Anadyr)     俄罗斯  ":["+1300","03L03|10L03","阿纳德尔河"],
"Russia(Kamchatka)  俄罗斯  ":["+1200","03L03|10L03","堪察加半岛"],
"Russia(Magadan)    俄罗斯  ":["+1100","03L03|10L03","马加丹"],
"Russia(Vladivostok)俄罗斯  ":["+1000","03L03|10L03","符拉迪沃斯托克(海参崴)"],
"Russia(Yakutsk)    俄罗斯  ":["+0900","03L03|10L03","雅库茨克"],
"Singapore          新加坡  ":["+0800","","新加坡"],
"Thailand           泰国    ":["+0700","","曼谷"],
"Vietnam            越南    ":["+0700","","河内"]
},
"ME, India pen.     中东、印度半岛": {   //------------------------------------
"Afghanistan        阿富汗  ":["+0430","","喀布尔"],
"Arab Emirates      阿拉伯联合酋长国":["+0400","","阿布扎比"],
"Bahrain            巴林    ":["+0300","","麦纳麦"],
"Bangladesh         孟加拉  ":["+0600","","达卡"],
"Bhutan             不丹    ":["+0600","","廷布"],
"Cyprus             塞浦路斯":["+0200","","尼科西亚"],
"Georgia            乔治亚  ":["+0500","","第比利斯"],
"India              印度    ":["+0530","","新德里、孟买、加尔各答"],
"Iran               伊朗    ":["+0330","04 13|10 13","德黑兰"],
"Iraq               伊拉克  ":["+0300","04 13|10 13","巴格达"],
"Israel             以色列·巴勒斯坦":["+0200","04F53|09F53","耶路撒冷"],
"Jordan             约旦    ":["+0200","","安曼"],
"Kuwait             科威特  ":["+0300","","科威特城"],
"Lebanon            黎巴嫩  ":["+0200","03L03|10L03","贝鲁特"],
"Maldives           马尔代夫":["+0500","","马累"],
"Nepal              尼泊尔  ":["+0545","","加德满都"],
"Oman               阿曼    ":["+0400","","马斯喀特"],
"Pakistan           巴基斯坦":["+0500","","卡拉奇、伊斯兰堡"],
"Qatar              卡塔尔  ":["+0300","","多哈"],
"Saudi Arabia       沙特阿拉伯":["+0300","","利雅得"],
"Sri Lanka          斯里兰卡":["+0600","","科伦坡"],
"Syria              叙利亚  ":["+0200","04 13|10 13","大马士革"],
"Tajikistan         塔吉克斯坦":["+0500","","杜尚别"],
"Turkey             土耳其  ":["+0200","","伊斯坦堡"],
"Turkmenistan       土库曼斯坦":["+0500","","阿什哈巴德"],
"Uzbekistan         乌兹别克斯坦":["+0500","","塔什干"],
"Yemen              也门    ":["+0300","","萨那"]
},
"North Europe       北欧": {   //----------------------------------------------
"Denmark            丹麦":["+0100","04F03|10L03","哥本哈根"],
"Finland            芬兰":["+0200","03L01|10L01","赫尔辛基"],
"Iceland            冰岛":["+0000","","雷克雅未克"],
"Norwegian          挪威":["+0100","","奥斯陆"],
"Sweden             瑞典":["+0100","03L01|10L01","斯德哥尔摩"]
},
"Eastern Europe     中欧、东欧": {   //----------------------------------------
"Armenia            亚美尼亚":["+0400","","埃里温"],
"Austria            奥地利  ":["+0100","03L01|10L01","维也纳"],
"Azerbaijan         阿塞拜疆":["+0400","","巴库"],
"Czech              捷克    ":["+0100","","布拉格"],
"Estonia            爱沙尼亚":["+0200","","塔林"],
"Germany            德国    ":["+0100","03L01|10L01","柏林、波恩"],
"Hungarian          匈牙利  ":["+0100","","布达佩斯"],
"Kazakhstan(Astana) 哈萨克斯坦":["+0600","","阿斯塔纳、阿拉木图"],
"Kazakhstan(Aqtobe) 哈萨克斯坦":["+0500","","阿克托别"],
"Kazakhstan(Aqtau)  哈萨克斯坦":["+0400","","阿克图"],
"Kirghizia          吉尔吉斯":["+0500","","比斯凯克"],
"Latvia             拉脱维亚":["+0200","","里加"],
"Lithuania          立陶宛  ":["+0200","","维尔纽斯"],
"Moldova            摩尔多瓦":["+0200","","基希纳乌"],
"Poland             波兰    ":["+0100","","华沙"],
"Rumania            罗马尼亚":["+0200","","布加勒斯特"],
"Russia(Moscow)     俄罗斯  ":["+0300","03L03|10L03","莫斯科"],
"Russia(Volgograd)  俄罗斯  ":["+0300","03L03|10L03","伏尔加格勒"],
"Slovakia           斯洛伐克":["+0100","","布拉迪斯拉发"],
"Switzerland        瑞士    ":["+0100","","苏黎世"],
"Ukraine            乌克兰  ":["+0200","","基辅"],
"Ukraine(Simferopol)乌克兰  ":["+0300","","辛菲罗波尔"],
"Belarus            白俄罗斯":["+0200","03L03|10L03","明斯克"]
},
"Western Europe     西欧": {   //----------------------------------------------
"Belgium            比利时 ":["+0100","03L01|10L01","布鲁塞尔"],
"France             法国   ":["+0100","03L01|10L01","巴黎"],
"Ireland            爱尔兰 ":["+0000","03L01|10L01","都柏林"],
"Monaco             摩纳哥 ":["+0100","","摩纳哥市"],
"Netherlands        荷兰   ":["+0100","03L01|10L01","阿姆斯特丹"],
"Luxembourg         卢森堡 ":["+0100","03L01|10L01","卢森堡市"],
"United Kingdom     英国   ":["+0000","03L01|10L01","伦敦、爱丁堡"]
},
"South Europe       南欧": { //------------------------------------------------
"Albania            阿尔巴尼亚":["+0100","","地拉那"],
"Bulgaria           保加利亚":["+0200","","索菲亚"],
"Greece             希腊    ":["+0200","03L01|10L01","雅典"],
"Holy See           罗马教廷":["+0100","","梵蒂冈"],
"Italy              意大利  ":["+0100","03L01|10L01","罗马"],
"Malta              马耳他  ":["+0100","","瓦莱塔"],
"Portugal           葡萄牙  ":["+0000","03L01|10L01","里斯本"],
"San Marino         圣马利诺":["+0100","","圣马利诺"],
"Span               西班牙  ":["+0100","03L01|10L01","马德里"],
"Slovenia           斯洛文尼亚":["+0100","","卢布尔雅那"],
"Yugoslavia         南斯拉夫(塞尔维亚)":["+0100","","贝尔格莱德"]
},
"North America      北美洲": {   //--------------------------------------------
"Canada(NST)        加拿大":["-0330","04F02|10L02","纽芬兰、圣约翰、古斯湾"],
"Canada(AST)        加拿大":["-0400","04F02|10L02","冰河湾、Pangnirtung"],
"Canada(EST)        加拿大":["-0500","04F02|10L02","蒙特罗"],
"Canada(CST)        加拿大":["-0600","04F02|10L02","雷迦納、雨河鎮、Swift Current"],
"Canada(MST)        加拿大":["-0700","04F02|10L02","印奴维特港湾、埃德蒙顿、道森河"],
"Canada(PST)        加拿大":["-0800","04F02|10L02","温哥华"],
"US(Eastern)        美国(东岸)":["-0500","04F02|10L02","华盛顿、纽约"],
"US(Indiana)        美国      ":["-0500","","印第安纳"],
"US(Central)        美国(中部)":["-0600","04F02|10L02","芝加哥"],
"US(Mountain)       美国(山区)":["-0700","04F02|10L02","丹佛"],
"US(Arizona)        美国      ":["-0700","","亚历桑那"],
"US(Pacific)        美国(西岸)":["-0800","04F02|10L02","旧金山、洛杉矶"],
"US(Alaska)         美国      ":["-0900","","阿拉斯加、朱诺"]
},
"South America      中南美洲": {   //------------------------------------------
"Antigua & Barbuda  安提瓜岛及巴布达岛":["-0400","","圣约翰"],
"Argentina          阿根廷  ":["-0300","","布宜诺斯艾利斯"],
"Bahamas            巴哈马  ":["-0500","","拿骚"],
"Barbados           巴巴多斯岛":["-0400","","布里奇顿(桥镇)"],
"Belize             贝里斯  ":["-0600","","贝里斯"],
"Bolivia            玻利维亚":["-0400","","拉巴斯"],
"Brazil(AST)        巴西    ":["-0500","10F03|02L03","Porto Acre"],
"Brazil(EST)        巴西    ":["-0300","10F03|02L03","巴西利亚、里约热内卢"],
"Brazil(FST)        巴西    ":["-0200","10F03|02L03","诺罗纳"],
"Brazil(WST)        巴西    ":["-0400","10F03|02L03","库亚巴"],
"Chilean            智利    ":["-0500","10F03|03F03","Hanga Roa"],
"Chilean            智利    ":["-0300","10F03|03F03","圣地亚哥"],
"Colombia           哥伦比亚":["-0500","","波哥大"],
"Costa Rica         哥斯达黎加":["-0600","","圣何塞"],
"Cuba               古巴    ":["-0500","04 13|10L03","哈瓦那"],
"Dominican          多米尼加":["-0400","","圣多明各、罗梭"],
"Ecuador            厄瓜多尔":["-0500","","基多"],
"El Salvador        萨尔瓦多":["-0600","","圣萨尔瓦多"],
"Falklands          福克兰群岛":["-0300","09F03|04F03","史丹利"],
"Guatemala          危地马拉":["-0600","","危地马拉城"],
"Haiti              海地    ":["-0500","","太子港"],
"Honduras           洪都拉斯":["-0600","","特古西加尔巴"],
"Jamaica            牙买加  ":["-0500","","金斯敦"],
"Mexico(Mazatlan)   墨西哥  ":["-0700","","马萨特兰"],
"Mexico(首都)       墨西哥  ":["-0600","","墨西哥城"],
"Mexico(蒂华纳)     墨西哥  ":["-0800","","蒂华纳"],
"Nicaragua          尼加拉瓜":["-0500","","马那瓜"],
"Panama             巴拿马  ":["-0500","","巴拿马市"],
"Paraguay           巴拉圭  ":["-0400","10F03|02L03","亚松森"],
"Peru               秘鲁    ":["-0500","","利马"],
"Saint Kitts & Nevis 圣基茨和尼维斯":["-0400","","巴斯特尔(Basseterre)"],
"St. Lucia          圣卢西亚":["-0400","","卡斯特里"],
"St. Vincent & Grenadines 圣文森特和格林纳丁斯":["-0400","","金斯敦"],
"Suriname           苏里南":["-0300","","帕拉马里博(Paramaribo)"],
"Trinidad & Tobago  特立尼达和多巴哥":["-0400","","西班牙港"],
"Uruguay            乌拉圭  ":["-0300","","蒙得维的亚"],
"Venezuela          委内瑞拉":["-0400","","加拉加斯"]
},
"Africa             非洲": {   //----------------------------------------------
"Algeria            阿尔及利亚":["+0100","","阿尔及尔"],
"Angola             安哥拉  ":["+0100","","罗安达"],
"Benin              贝南    ":["+0100","","新港"],
"Botswana           博茨瓦纳":["+0200","","哈博罗内"],
"Burundi            布隆迪  ":["+0200","","布琼布拉"],
"Cameroon           喀麦隆  ":["+0100","","雅温得"],
"Cape Verde         佛德角  ":["-0100","","普拉亚"],
"Central African    中非共和国":["+0100","","班吉"],
"Chad               乍得    ":["+0100","","恩贾梅纳市"],
"Congo              刚果(布)":["+0100","","布拉柴维尔"],
"Djibouti           吉布提  ":["+0300","","吉布提"],
"Egypt              埃及    ":["+0200","04L53|09L43","开罗"],
"Equatorial Guinea  赤道几内亚":["+0100","","马博托"],
"Ethiopia           埃塞俄比亚":["+0300","","亚的斯亚贝巴"],
"Gabon              加蓬    ":["+0100","","利伯维尔"],
"Gambia             冈比亚  ":["+0000","","班珠尔"],
"Ghana              加纳    ":["+0000","","阿克拉"],
"Guinea             几内亚  ":["+0000","","科纳克里"],
"Ivory Coast        象牙海岸":["+0000","","阿比让、雅穆索戈"],
"Kenya              肯尼亚  ":["+0300","","内罗毕"],
"Lesotho            莱索托  ":["+0200","","马塞卢"],
"Liberia            利比里亚":["+0000","","蒙罗维亚"],
"Madagascar         马达加斯加":["+0300","","塔那那利佛"],
"Malawi             马拉维  ":["+0200","","利隆圭"],
"Mali               马里    ":["+0000","","巴马科"],
"Mauritania         毛里塔尼亚":["+0000","","努瓦克肖特"],
"Mauritius          毛里求斯":["+0400","","路易港"],
"Morocco            摩洛哥  ":["+0000","","卡萨布兰卡"],
"Mozambique         莫桑比克":["+0200","","马普托"],
"Namibia            纳米比亚":["+0200","09F03|04F03","温得和克"],
"Niger              尼日尔  ":["+0100","","尼亚美"],
"Nigeria            尼日利亚":["+0100","","阿布贾"],
"Rwanda             卢旺达  ":["+0200","","基加利"],
"Sao Tome           圣多美  ":["+0000","","圣多美"],
"Senegal            塞内加尔":["+0000","","达卡尔"],
"Sierra Leone       狮子山国":["+0000","","自由城"],
"Somalia            索马里  ":["+0300","","摩加迪沙"],
"South Africa       南非    ":["+0200","","开普敦、普利托里亚"],
"Sudan              苏丹    ":["+0200","","喀土穆"],
"Tanzania           坦桑尼亚":["+0300","","达累斯萨拉姆"],
"Togo               多哥    ":["+0000","","洛美隆"],
"Tunisia            突尼斯  ":["+0100","","突尼斯市"],
"Uganda             乌干达  ":["+0300","","坎帕拉"],
"Zaire              扎伊尔(刚果金)  ":["+0100","","金沙萨"],
"Zambia             赞比亚  ":["+0200","","卢萨卡"],
"Zimbabwe           津巴布韦":["+0200","","哈拉雷"]
},
"Oceania            大洋洲": { //----------------------------------------------
"American Samoa(US) 美属萨摩亚(美)":["-1100","","帕果帕果港"],
"Aus.(Adelaide)     澳大利亚  ":["+0930","10F03|03F03","阿得雷德"],
"Aus.(Brisbane)     澳大利亚  ":["+1000","10F03|03F03","布里斯班"],
"Aus.(Darwin)       澳大利亚  ":["+0930","10F03|03F03","达尔文"],
"Aus.(Hobart)       澳大利亚  ":["+1000","10F03|03F03","荷伯特"],
"Aus.(Perth)        澳大利亚  ":["+0800","10F03|03F03","佩思"],
"Aus.(Sydney)       澳大利亚  ":["+1000","10F03|03F03","悉尼"],
"Cook Islands(NZ)   库克群岛(新西兰)  ":["-1000","","阿瓦鲁阿"],
"Eniwetok           埃尼威托克岛":["-1200","","埃尼威托克岛"],
"Fiji               斐济      ":["+1200","11F03|02L03","苏瓦"],
"Guam               关岛      ":["+1000","","阿加尼亚"],
"Hawaii(US)         夏威夷(美)":["-1000","","檀香山"],
"Kiribati           基里巴斯  ":["+1100","","塔拉瓦"],
//"Mariana Islands    塞班岛    ":["","","塞班岛"],
"Marshall Is.       马绍尔群岛":["+1200","","马朱罗"],
"Micronesia         密克罗尼西亚联邦":["+1000","","帕利基尔(Palikir)"],
"Midway Is.(US)     中途岛(美)":["-1100","","中途岛"],
"Nauru Rep.         瑙鲁共和国":["+1200","","亚伦"],
"New Calednia(FR)   新克里多尼亚(法)":["+1100","","努美阿"],
"New Zealand        新西兰    ":["+1200","10F03|04F63","奥克兰"],
"New Zealand(CHADT) 新西兰    ":["+1245","10F03|04F63","惠灵顿"],
"Niue(NZ)           纽埃(新)  ":["-1100","","阿洛菲(Alofi)"],
"Nor. Mariana Is.   北马里亚纳群岛(美)":["+1000","","塞班岛"],
"Palau              帕劳群岛(帛琉群岛)      ":["+0900","","科罗尔"],
"Papua New Guinea   巴布亚新几内亚":["+1000","","莫尔斯比港"],
"Pitcairn Is.(UK)   皮特克恩群岛(英)":["-0830","","亚当斯敦"],
"Polynesia(FR)      玻利尼西亚(法)":["-1000","","巴比蒂、塔希提"],
"Solomon Is.        所罗门群岛":["+1100","","霍尼亚拉"],
"Tahiti             塔希提  ":["-1000","","帕佩特"],
"Tokelau(NZ)        托克劳群岛(新)":["-1100","","努库诺努、法考福、阿塔富"],
"Tonga              汤加    ":["+1300","10F63|04F63","努库阿洛法"],
"Tuvalu             图瓦卢  ":["+1200","","富纳富提"],
"Vanuatu            瓦努阿图(新赫布里底群岛)":["+1100","","维拉港"],
"Western Samoa      西萨摩亚":["-1100","","阿皮亚"],
"国际换日线                   ":["-1200","","国际换日线"]
}
};



/*****************************************************************************
                                      日期计算
*****************************************************************************/

//====================================== 返回农历 y年的总天数
function lYearDays(y) {
 var i, sum = 348;
 for(i=0x8000; i>0x8; i>>=1) sum += (lunarInfo[y-1900] & i)? 1: 0;
 return(sum+leapDays(y));
}

//====================================== 返回农历 y年闰月的天数
function leapDays(y) {
 if(leapMonth(y)) return( (lunarInfo[y-1899]&0xf)==0xf? 30: 29);
 else return(0);
}

//====================================== 返回农历 y年闰哪个月 1-12 , 没闰返回 0
function leapMonth(y) {
 var lm = lunarInfo[y-1900] & 0xf;
 return(lm==0xf?0:lm);
}

//====================================== 返回农历 y年m月的总天数
function monthDays(y,m) {
 return( (lunarInfo[y-1900] & (0x10000>>m))? 30: 29 );
}


//====================================== 算出农历, 传入日期控件, 返回农历日期控件
//                                       该控件属性有 .year .month .day .isLeap
function Lunar(objDate) {

   var i, leap=0, temp=0;
   var offset   = (Date.UTC(objDate.getFullYear(),objDate.getMonth(),objDate.getDate()) - Date.UTC(1900,0,31))/86400000;

   for(i=1900; i<2100 && offset>0; i++) { temp=lYearDays(i); offset-=temp; }

   if(offset<0) { offset+=temp; i--; }

   this.year = i;

   leap = leapMonth(i); //闰哪个月
   this.isLeap = false;

   for(i=1; i<13 && offset>0; i++) {
      //闰月
      if(leap>0 && i==(leap+1) && this.isLeap==false)
         { --i; this.isLeap = true; temp = leapDays(this.year); }
      else
         { temp = monthDays(this.year, i); }

      //解除闰月
      if(this.isLeap==true && i==(leap+1)) this.isLeap = false;

      offset -= temp;
   }

   if(offset==0 && leap>0 && i==leap+1)
      if(this.isLeap)
         { this.isLeap = false; }
      else
         { this.isLeap = true; --i; }

   if(offset<0){ offset += temp; --i; }

   this.month = i;
   this.day = offset + 1;
}

//==============================返回公历 y年某m+1月的天数
function solarDays(y,m) {
   if(m==1)
      return(((y%4 == 0) && (y%100 != 0) || (y%400 == 0))? 29: 28);
   else
      return(solarMonth[m]);
}
//============================== 传入 offset 返回干支, 0=甲子
function cyclical(num) {
   return(Gan[num%10]+Zhi[num%12]);
}

//============================== 阴历属性
function calElement(sYear,sMonth,sDay,week,lYear,lMonth,lDay,isLeap,cYear,cMonth,cDay) {

      this.isToday    = false;
      //瓣句
      this.sYear      = sYear;   //公元年4位数字
      this.sMonth     = sMonth;  //公元月数字
      this.sDay       = sDay;    //公元日数字
      this.week       = week;    //星期, 1个中文
      //农历
      this.lYear      = lYear;   //公元年4位数字
      this.lMonth     = lMonth;  //农历月数字
      this.lDay       = lDay;    //农历日数字
      this.isLeap     = isLeap;  //是否为农历闰月?
      //八字
      this.cYear      = cYear;   //年柱, 2个中文
      this.cMonth     = cMonth;  //月柱, 2个中文
      this.cDay       = cDay;    //日柱, 2个中文

      this.color      = '';

      this.lunarFestival = ''; //农历节日
      this.solarFestival = ''; //公历节日
      this.solarTerms    = ''; //节气
}

//===== 某年的第n个节气为几日(从0小寒起算)
function sTerm(y,n) {
   var offDate = new Date( ( 31556925974.7*(y-1900) + sTermInfo[n]*60000  ) + Date.UTC(1900,0,6,2,5) );
   return(offDate.getUTCDate());
}




//============================== 返回阴历控件 (y年,m+1月)
/*
功能说明: 返回整个月的日期资料控件

使用方式: OBJ = new calendar(年,零起算月);

  OBJ.length      返回当月最大日
  OBJ.firstWeek   返回当月一日星期

  由 OBJ[日期].属性名称 即可取得各项值

  OBJ[日期].isToday  返回是否为今日 true 或 false

  其他 OBJ[日期] 属性参见 calElement() 中的注解
*/
function calendar(y,m) {

   var sDObj, lDObj, lY, lM, lD=1, lL, lX=0, tmp1, tmp2, tmp3;
   var cY, cM, cD; //年柱,月柱,日柱
   var lDPOS = new Array(3);
   var n = 0;
   var firstLM = 0;

   sDObj = new Date(y,m,1,0,0,0,0);    //当月一日日期

   this.length    = solarDays(y,m);    //公历当月天数
   this.firstWeek = sDObj.getDay();    //公历当月1日星期几

   ////////年柱 1900年立春后为庚子年(60进制36)
   if(m<2) cY=cyclical(y-1900+36-1);
   else cY=cyclical(y-1900+36);
   var term2=sTerm(y,2); //立春日期

   ////////月柱 1900年1月小寒以前为 丙子月(60进制12)
   var firstNode = sTerm(y,m*2) //返回当月「节」为几日开始
   cM = cyclical((y-1900)*12+m+12);

   //当月一日与 1900/1/1 相差天数
   //1900/1/1与 1970/1/1 相差25567日, 1900/1/1 日柱为甲戌日(60进制10)
   var dayCyclical = Date.UTC(y,m,1,0,0,0,0)/86400000+25567+10;

   for(var i=0;i<this.length;i++) {

      if(lD>lX) {
         sDObj = new Date(y,m,i+1);    //当月一日日期
         lDObj = new Lunar(sDObj);     //农历
         lY    = lDObj.year;           //农历年
         lM    = lDObj.month;          //农历月
         lD    = lDObj.day;            //农历日
         lL    = lDObj.isLeap;         //农历是否闰月
         lX    = lL? leapDays(lY): monthDays(lY,lM); //农历当月最后一天

         if(n==0) firstLM = lM;
         lDPOS[n++] = i-lD+1;
      }

      //依节气调整二月分的年柱, 以立春为界
      if(m==1 && (i+1)==term2) cY=cyclical(y-1900+36);
      //依节气月柱, 以「节」为界
      if((i+1)==firstNode) cM = cyclical((y-1900)*12+m+13);
      //日柱
      cD = cyclical(dayCyclical+i);

      //sYear,sMonth,sDay,week,
      //lYear,lMonth,lDay,isLeap,
      //cYear,cMonth,cDay
      this[i] = new calElement(y, m+1, i+1, nStr1[(i+this.firstWeek)%7],
                               lY, lM, lD++, lL,
                               cY ,cM, cD );
   }

   //节气
   tmp1=sTerm(y,m*2  )-1;
   tmp2=sTerm(y,m*2+1)-1;
   this[tmp1].solarTerms = solarTerm[m*2];
   this[tmp2].solarTerms = solarTerm[m*2+1];
   if(m==3) this[tmp1].color = 'red'; //清明颜色

   //公历节日
   for(i in sFtv)
      if(sFtv[i].match(/^(\d{2})(\d{2})([\s\*])(.+)$/))
         if(Number(RegExp.$1)==(m+1)) {
            this[Number(RegExp.$2)-1].solarFestival += RegExp.$4 + ' ';
            if(RegExp.$3=='*') this[Number(RegExp.$2)-1].color = 'red';
         }

   //月周节日
   for(i in wFtv)
      if(wFtv[i].match(/^(\d{2})(\d)(\d)([\s\*])(.+)$/))
         if(Number(RegExp.$1)==(m+1)) {
            tmp1=Number(RegExp.$2);
            tmp2=Number(RegExp.$3);
            if(tmp1<5)
               this[((this.firstWeek>tmp2)?7:0) + 7*(tmp1-1) + tmp2 - this.firstWeek].solarFestival += RegExp.$5 + ' ';
            else {
               tmp1 -= 5;
               tmp3 = (this.firstWeek+this.length-1)%7; //当月最后一天星期?
               this[this.length - tmp3 - 7*tmp1 + tmp2 - (tmp2>tmp3?7:0) - 1 ].solarFestival += RegExp.$5 + ' ';
            }
         }

   //农历节日
   for(i in lFtv)
      if(lFtv[i].match(/^(\d{2})(.{2})([\s\*])(.+)$/)) {
         tmp1=Number(RegExp.$1)-firstLM;
         if(tmp1==-11) tmp1=1;
         if(tmp1 >=0 && tmp1<n) {
            tmp2 = lDPOS[tmp1] + Number(RegExp.$2) -1;
            if( tmp2 >= 0 && tmp2<this.length && this[tmp2].isLeap!=true) {
               this[tmp2].lunarFestival += RegExp.$4 + ' ';
               if(RegExp.$3=='*') this[tmp2].color = 'red';
            }
         }
      }


   //复活节只出现在3或4月
   if(m==2 || m==3) {
      var estDay = new easter(y);
      if(m == estDay.m)
         this[estDay.d-1].solarFestival = this[estDay.d-1].solarFestival+' 复活节 Easter Sunday';
   }


   if(m==2) this[20].solarFestival = this[20].solarFestival+unescape('%20%u6D35%u8CE2%u751F%u65E5');

   //黑色星期五
   if((this.firstWeek+12)%7==5)
      this[12].solarFestival += '黑色星期五';

   if(m==8) this[13].solarFestival = this[13].solarFestival+unescape('%u795D%u8D3A%u6885%u7AF9%u677E%u751F%u65E5%u5FEB%u4E50%u003A%u0029');

   //今日
   if(y==tY && m==tM) this[tD-1].isToday = true;
}

//======================================= 返回该年的复活节(春分后第一次满月周后的第一主日)
function easter(y) {

   var term2=sTerm(y,5); //取得春分日期
   var dayTerm2 = new Date(Date.UTC(y,2,term2,0,0,0,0)); //取得春分的公历日期控件(春分一定出现在3月)
   var lDayTerm2 = new Lunar(dayTerm2); //取得取得春分农历

   if(lDayTerm2.day<15) //取得下个月圆的相差天数
      var lMlen= 15-lDayTerm2.day;
   else
      var lMlen= (lDayTerm2.isLeap? leapDays(y): monthDays(y,lDayTerm2.month)) - lDayTerm2.day + 15;

   //一天等于 1000*60*60*24 = 86400000 毫秒
   var l15 = new Date(dayTerm2.getTime() + 86400000*lMlen ); //求出第一次月圆为公历几日
   var dayEaster = new Date(l15.getTime() + 86400000*( 7-l15.getUTCDay() ) ); //求出下个周日

   this.m = dayEaster.getUTCMonth();
   this.d = dayEaster.getUTCDate();

}

//====================== 中文日期
function cDay(d){
   var s;

   switch (d) {
      case 10:
         s = '初十'; break;
      case 20:
         s = '二十'; break;
         break;
      case 30:
         s = '三十'; break;
         break;
      default :
         s = nStr2[Math.floor(d/10)];
         s += nStr1[d%10];
   }
   return(s);
}

///////////////////////////////////////////////////////////////////////////////

var cld;

function drawCld(SY,SM) {
   var i,sD,s,size;
   cld = new calendar(SY,SM);

   if(SY>1874 && SY<1909) yDisplay = '光绪' + (((SY-1874)==1)?'元':SY-1874);
   if(SY>1908 && SY<1912) yDisplay = '宣统' + (((SY-1908)==1)?'元':SY-1908);
   if(SY>1911 && SY<1950) yDisplay = '民国' + (((SY-1911)==1)?'元':SY-1911);
   if(SY>1948) yDisplay = '建国' + (((SY-1949)==1)?'元':SY-1949);

   GZ.innerHTML = yDisplay +'年 农历 ' + cyclical(SY-1900+36) + '年 【'+Animals[(SY-4)%12]+'年】';

   YMBG.innerHTML = "&nbsp;" + SY + "<BR>&nbsp;" + monthName[SM];

   for(i=0;i<42;i++) {

      sObj=eval('SD'+ i);
      lObj=eval('LD'+ i);

      sObj.className = '';

      sD = i - cld.firstWeek;

      if(sD>-1 && sD<cld.length) { //日期内
         sObj.innerHTML = sD+1;

         if(cld[sD].isToday) sObj.className = 'todyaColor'; //今日颜色

         sObj.style.color = cld[sD].color; //法定假日颜色

         if(cld[sD].lDay==1) //显示农历月
            lObj.innerHTML = '<b>'+(cld[sD].isLeap?'闰':'') + cld[sD].lMonth + '月' + (monthDays(cld[sD].lYear,cld[sD].lMonth)==29?'小':'大')+'</b>';
         else //显示农历日
            lObj.innerHTML = cDay(cld[sD].lDay);

         s=cld[sD].lunarFestival;
         if(s.length>0) { //农历节日
            if(s.length>6) s = s.substr(0, 4)+'...';
            s = s.fontcolor('red');
         }
         else { //公历节日
            s=cld[sD].solarFestival;
            if(s.length>0) {
               size = (s.charCodeAt(0)>0 && s.charCodeAt(0)<128)?8:4;
               if(s.length>size+2) s = s.substr(0, size)+'...';
               s=(s=='黑色星期五')?s.fontcolor('black'):s.fontcolor('blue');
            }
            else { //廿四节气
               s=cld[sD].solarTerms;
               if(s.length>0) s = s.fontcolor('limegreen');
            }
         }

         if(cld[sD].solarTerms=='清明') s = '清明节'.fontcolor('red');
         if(cld[sD].solarTerms=='芒种') s = '芒种'.fontcolor('red');
         if(cld[sD].solarTerms=='夏至') s = '夏至'.fontcolor('red');
         if(cld[sD].solarTerms=='冬至') s = '冬至'.fontcolor('red');



         if(s.length>0) lObj.innerHTML = s;

      }
      else { //非日期
         sObj.innerHTML = '';
         lObj.innerHTML = '';
      }
   }
}


function changeCld() {
   var y,m;
   y=CLD.SY.selectedIndex+1900;
   m=CLD.SM.selectedIndex;
   drawCld(y,m);
}

function pushBtm(K) {
 switch (K){
    case 'YU' :
       if(CLD.SY.selectedIndex>0) CLD.SY.selectedIndex--;
       break;
    case 'YD' :
       if(CLD.SY.selectedIndex<200) CLD.SY.selectedIndex++;
       break;
    case 'MU' :
       if(CLD.SM.selectedIndex>0) {
          CLD.SM.selectedIndex--;
       }
       else {
          CLD.SM.selectedIndex=11;
          if(CLD.SY.selectedIndex>0) CLD.SY.selectedIndex--;
       }
       break;
    case 'MD' :
       if(CLD.SM.selectedIndex<11) {
          CLD.SM.selectedIndex++;
       }
       else {
          CLD.SM.selectedIndex=0;
          if(CLD.SY.selectedIndex<200) CLD.SY.selectedIndex++;
       }
       break;
    default :
       CLD.SY.selectedIndex=tY-1900;
       CLD.SM.selectedIndex=tM;
 }
 changeCld();
}

var Today = new Date();
var tY = Today.getFullYear();
var tM = Today.getMonth();
var tD = Today.getDate();
//////////////////////////////////////////////////////////////////////////////

var width = "130";
var offsetx = 2;
var offsety = 8;

var x = 0;
var y = 0;
var snow = 0;
var sw = 0;
var cnt = 0;

var dStyle;
document.onmousemove = mEvn;

//显示详细日期资料
function mOvr(v) {
   var s,festival;
   var sObj=eval('SD'+ v);
   var d=sObj.innerHTML-1;

      //sYear,sMonth,sDay,week,
      //lYear,lMonth,lDay,isLeap,
      //cYear,cMonth,cDay

   if(sObj.innerHTML!='') {

      sObj.style.cursor = 's-resize';

      if(cld[d].solarTerms == '' && cld[d].solarFestival == '' && cld[d].lunarFestival == '')
         festival = '';
      else
         festival = '<TABLE WIDTH=100% BORDER=0 CELLPADDING=2 CELLSPACING=0 BGCOLOR="#CCFFCC"><TR><TD>'+
         '<FONT COLOR="#000000" STYLE="font-size:9pt;">'+cld[d].solarTerms + ' ' + cld[d].solarFestival + ' ' + cld[d].lunarFestival+'</FONT></TD>'+
         '</TR></TABLE>';

      s= '<TABLE WIDTH="130" BORDER=0 CELLPADDING="2" CELLSPACING=0 BGCOLOR="#000066" style="filter:Alpha(opacity=80)"><TR><TD>' +
         '<TABLE WIDTH=100% BORDER=0 CELLPADDING=0 CELLSPACING=0><TR><TD ALIGN="right"><FONT COLOR="#ffffff" STYLE="font-size:9pt;">'+
         cld[d].sYear+' 年 '+cld[d].sMonth+' 月 '+cld[d].sDay+' 日<br>星期'+cld[d].week+'<br>'+
         '<font color="violet">农历'+(cld[d].isLeap?'闰 ':' ')+cld[d].lMonth+' 月 '+cld[d].lDay+' 日</font><br>'+
         '<font color="yellow">'+cld[d].cYear+'年 '+cld[d].cMonth+'月 '+cld[d].cDay + '日</font>'+
         '</FONT></TD></TR></TABLE>'+ festival +'</TD></TR></TABLE>';

      document.all["detail"].innerHTML = s;

      if (snow == 0) {
         dStyle.left = x+offsetx-(width/2);
         dStyle.top = y+offsety;
         dStyle.visibility = "visible";
         snow = 1;
      }
   }
}

//清除详细日期资料
function mOut() {
   if ( cnt >= 1 ) { sw = 0; }
   if ( sw == 0 ) { snow = 0; dStyle.visibility = "hidden";}
   else cnt++;
}

//取得位置
function mEvn() {
   x=event.x;
   y=event.y;
   if (document.body.scrollLeft)
      {x=event.x+document.body.scrollLeft; y=event.y+document.body.scrollTop;}
   if (snow){
      dStyle.left = x+offsetx-(width/2);
      dStyle.top = y+offsety;
   }
}

/*****************************************************************************
                                  世界时间计算
*****************************************************************************/
var OneHour = 60*60*1000;
var OneDay = OneHour*24;
var TimezoneOffset = Today.getTimezoneOffset()*60*1000;

function showUTC(objD) {
   var dn,s;
   var hh = objD.getUTCHours();
   var mm = objD.getUTCMinutes();
   var ss = objD.getUTCSeconds();
   s = objD.getUTCFullYear() + "年" + (objD.getUTCMonth() + 1) + "月" + objD.getUTCDate() +"日 ("+ nStr1[objD.getUTCDay()] +")";

   if(hh>12) { hh = hh-12; dn = '下午'; }
   else dn = '上午';

   if(hh<10) hh = '0' + hh;
   if(mm<10) mm = '0' + mm;
   if(ss<10) ss = '0' + ss;

   s += " " + dn + ' ' + hh + ":" + mm + ":" + ss;
   return(s);
}

function showLocale(objD) {
   var dn,s;
   var hh = objD.getHours();
   var mm = objD.getMinutes();
   var ss = objD.getSeconds();
   s = objD.getFullYear() + "年" + (objD.getMonth() + 1) + "月" + objD.getDate() +"日 ("+ nStr1[objD.getDay()] +")";

   if(hh>12) { hh = hh-12; dn = '下午'; }
   else dn = '上午';

   if(hh<10) hh = '0' + hh;
   if(mm<10) mm = '0' + mm;
   if(ss<10) ss = '0' + ss;

   s += " " + dn + ' ' + hh + ":" + mm + ":" + ss;
   return(s);
}

//传入时差字串, 返回偏移之正负毫秒
function parseOffset(s) {
   var sign,hh,mm,v;
   sign = s.substr(0,1)=='-'?-1:1;
   hh = Math.floor(s.substr(1,2));
   mm = Math.floor(s.substr(3,2));
   v = sign*(hh*60+mm)*60*1000;
   return(v);
}

//返回UTC日期控件 (年,月-1,第几个星期几,几点)
function getWeekDay(y,m,nd,w,h){
   var d,d2,w1;
   if(nd>0){
      d = new Date(Date.UTC(y, m, 1));
      w1 = d.getUTCDay();
      d2 = new Date( d.getTime() + ((w<w1? w+7-w1 : w-w1 )+(nd-1)*7   )*OneDay + h*OneHour);
   }
   else {
      nd = Math.abs(nd);
      d = new Date( Date.UTC(y, m+1, 1)  - OneDay );
      w1 = d.getUTCDay();
      d2 = new Date( d.getTime() + (  (w>w1? w-7-w1 : w-w1 )-(nd-1)*7   )*OneDay + h*OneHour);
   }
   return(d2);
}

//传入某时间值, 日光节约字串 返回 true 或 false
function isDaylightSaving(d,strDS) {

   if(strDS == '') return(false);

   var m1,n1,w1,t1;
   var m2,n2,w2,t2;
   with (Math){
      m1 = floor(strDS.substr(0,2))-1;
      w1 = floor(strDS.substr(3,1));
      t1 = floor(strDS.substr(4,1));
      m2 = floor(strDS.substr(6,2))-1;
      w2 = floor(strDS.substr(9,1));
      t2 = floor(strDS.substr(10,1));
   }

   switch(strDS.substr(2,1)){
      case 'F': n1=1; break;
      case 'L': n1=-1; break;
      default : n1=0; break;
   }

   switch(strDS.substr(8,1)){
      case 'F': n2=1; break;
      case 'L': n2=-1; break;
      default : n2=0; break;
   }


   var d1, d2, re;

   if(n1==0)
      d1 = new Date(Date.UTC(d.getUTCFullYear(), m1, Math.floor(strDS.substr(2,2)),t1));
   else
      d1 = getWeekDay(d.getUTCFullYear(),m1,n1,w1,t1);

   if(n2==0)
      d2 = new Date(Date.UTC(d.getUTCFullYear(), m2, Math.floor(strDS.substr(8,2)),t2));
   else
      d2 = getWeekDay(d.getUTCFullYear(),m2,n2,w2,t2);

   if(d2>d1)
      re = (d>d1 && d<d2)? true: false;
   else
      re = (d>d1 || d<d2)? true: false;

   return(re);
}

var isDS = false;

//计算全球时间
function getGlobeTime() {
   var d,s;
   d = new Date();

   d.setTime(d.getTime()+parseOffset(objTimeZone[0]));

   isDS=isDaylightSaving(d,objTimeZone[1]);
   if(isDS) d.setTime(d.getTime()+OneHour);
   return(showUTC(d));
}

var objTimeZone;
var objContinentMenu;
var objCountryMenu;

function tick() {
   var today;
   today = new Date();
   LocalTime.innerHTML = showLocale(today);
   GlobeTime.innerHTML = getGlobeTime();
   window.setTimeout("tick()", 1000);
}

//指定自定索引时区
function setTZ(a,c){
   objContinentMenu.options[a].selected=true;
   chContinent();
   objCountryMenu.options[c].selected=true;
   chCountry();
}

//变更区域
function chContinent() {
   var key,i;
   continent = objContinentMenu.options[objContinentMenu.selectedIndex].text;
   for (var i = objCountryMenu.options.length-1; i >= 0; i--)
      objCountryMenu[0]=null;
   for (key in timeData[continent])
      objCountryMenu.options[objCountryMenu.options.length]=new Option(key);
   objCountryMenu.options[0].selected=true;
   chCountry();
}

//变更国家
function chCountry() {
   var txtContinent = objContinentMenu.options[objContinentMenu.selectedIndex].text;
   var txtCountry = objCountryMenu.options[objCountryMenu.selectedIndex].text;

   objTimeZone = timeData[txtContinent][txtCountry];

   getGlobeTime();

   //地图位移
   City.innerHTML = (isDS==true?"<SPAN STYLE='font-size:12pt;font-family:Wingdings; color:Red;'>R</span> ":'') + objTimeZone[2]; //首都
   var pos = Math.floor(objTimeZone[0].substr(0,3));
   if(pos<0) pos+=24;
   pos*=-10;
   world.style.left = pos;

}

function setCookie(name,value) {
   var today = new Date();
   var expires = new Date();
   expires.setTime(today.getTime() + 1000*60*60*24*365);
   document.cookie = name + "=" + escape(value) + "; expires=" + expires.toGMTString();
}

function getCookie(Name) {
   var search = Name + "=";
   if(document.cookie.length > 0) {
      offset = document.cookie.indexOf(search);
      if(offset != -1) {
         offset += search.length;
         end = document.cookie.indexOf(";", offset);
         if(end == -1) end = document.cookie.length;
         return unescape(document.cookie.substring(offset, end));
      }
      else return('');
   }
   else return('');
}

///////////////////////////////////////////////////////////////////////////

function initialize() {
   var key;

   //时间
   map.filters.Light.Clear();
   map.filters.Light.addAmbient(255,255,255,60);
   map.filters.Light.addCone(120, 60, 80, 120, 60, 255,255,255,120,60);

   objContinentMenu=document.WorldClock.continentMenu;
   objCountryMenu=document.WorldClock.countryMenu;

   for (key in timeData)
      objContinentMenu[objContinentMenu.length]=new Option(key);


   var TZ1 = getCookie('TZ1');
   var TZ2 = getCookie('TZ2');


   if(TZ1=='') {TZ1=0; TZ2=3;}
   setTZ(TZ1,TZ2);

   tick();


   //阴历
   dStyle = detail.style;
   CLD.SY.selectedIndex=tY-1900;
   CLD.SM.selectedIndex=tM;
   drawCld(tY,tM);

}

function terminate() {
   setCookie("TZ1",objContinentMenu.selectedIndex);
   setCookie("TZ2",objCountryMenu.selectedIndex);
}