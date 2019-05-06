var ECharts = {

    //配置。
    ChartConfig: function (container, option) {

        var chart_path = "/res/js/echarts/echarts"; //配置图表请求路径 
        var map_path = "/res/js/echarts/echarts-map"; //配置地图的请求路径 

        require.config({//引入常用的图表类型的配置
            paths: {
                echarts: chart_path,
                'echarts/chart/bar': chart_path,
                'echarts/chart/pie': chart_path,
                'echarts/chart/line': chart_path,
                'echarts/chart/k': chart_path,
                'echarts/chart/scatter': chart_path,
                'echarts/chart/radar': chart_path,
                'echarts/chart/chord': chart_path,
                'echarts/chart/force': chart_path,
                'echarts/chart/map': map_path
            }
        });

        this.option = { chart: {}, option: option, container: container };
        return this.option;

    },

    //数据模板。
    ChartDataFormate: {
        //生成无分组|单一序列数据。
        FormateNOGroupData: function (data) {
            //data的格式如上的Result1，这种格式的数据，多用于饼图、单一的柱形图的数据源
            var categories = [];
            var datas = [];
            for (var i = 0; i < data.length; i++) {
                categories.push(data[i].Name || "");
                datas.push({ name: data[i].Name, value: data[i].Value || 0 });
            }
            return { category: categories, data: datas };
        },

        //生成有分组数据|多序列数据。
        FormateGroupData: function (data, type, is_stack) {
            //data的格式如上的Result2，type为要渲染的图表类型：可以为line，bar，is_stack表示为是否是堆积图，这种格式的数据多用于展示多条折线图、分组的柱图
            var chart_type = 'line';
            if (type)
                chart_type = type || 'line';

            var xAxis = [];
            var group = [];
            var series = [];

            for (var i = 0; i < data.length; i++) {
                for (var j = 0; j < xAxis.length && xAxis[j] != data[i].Name; j++);
                if (j == xAxis.length)
                    xAxis.push(data[i].Name);

                for (var k = 0; k < group.length && group[k] != data[i].Group; k++);
                if (k == group.length)
                    group.push(data[i].Group);
            }


            for (var i = 0; i < group.length; i++) {
                var temp = [];
                for (var j = 0; j < data.length; j++) {
                    if (group[i] == data[j].Group) {
                        if (type == "map") {
                            temp.push({ name: data[j].Name, value: data[i].Value });
                        } else {
                            temp.push(data[j].Value);
                        }
                    }

                }


                switch (type) {
                    case 'bar':
                        var series_temp = { name: group[i], data: temp, type: chart_type };
                        if (is_stack)
                            series_temp = $.extend({}, { stack: 'stack' }, series_temp);
                        break;

                    case 'map':
                        var series_temp = {
                            name: group[i], type: chart_type, mapType: 'china', selectedMode: 'single',
                            itemStyle: {
                                normal: { label: { show: true} },
                                emphasis: { label: { show: true} }
                            },
                            data: temp
                        };
                        break;

                    case 'line':
                        var series_temp = { name: group[i], data: temp, type: chart_type };
                        if (is_stack)
                            series_temp = $.extend({}, { stack: 'stack' }, series_temp);
                        break;

                    default:
                        var series_temp = { name: group[i], data: temp, type: chart_type };
                }
                series.push(series_temp);
            }
            return { category: group, xAxis: xAxis, series: series };
        }
    }
    ,


    ChartOptionTemplates: {
        CommonOption: {
            //通用的图表基本配置 
            tooltip: {
                trigger: 'axis'//tooltip触发方式:axis以X轴线触发,item以每一个数据项触发 
            },
            toolbox: {
                show: true, //是否显示工具栏 
                feature: {
                    mark: true,
                    dataView: { readOnly: false }, //数据预览 
                    restore: true, //复原 
                    saveAsImage: true //是否保存图片 
                }
            }
        },

        CommonLineOption: {//通用的折线图表的基本配置 
            tooltip: {
                trigger: 'axis'
            },
            calculable: true,
            toolbox: {
                show: true,
                feature: {
                    dataView: { readOnly: false }, //数据预览
                    restore: true, //复原
                    saveAsImage: true, //是否保存图片
                    magicType: ['line', 'bar']//支持柱形图和折线图的切换 
                }
            }
        },

        //标准饼图：数据，数据主题，标题，子标题，data:数据格式：{Name：xxx,Value:xxx}
        StandardPie: function (data, name, text, subtext) {
            var pie_datas = ECharts.ChartDataFormate.FormateNOGroupData(data);

            var option = {
                title: {
                    text: text,
                    subtext: subtext,
                    x: 'center'
                },
                tooltip: {
                    trigger: 'item',
                    formatter: "{a} <br/>{b} : {c} ({d}%)"
                },
                legend: {
                    orient: 'vertical',
                    x: 'left',
                    data: pie_datas.category
                },
                toolbox: {
                    show: true,
                    feature: {
                        mark: { show: true },
                        dataView: { show: true, readOnly: false },
                        magicType: {
                            show: true,
                            type: ['pie', 'funnel'],
                            option: {
                                funnel: {
                                    x: '25%',
                                    width: '50%',
                                    funnelAlign: 'left',
                                    max: 1548
                                }
                            }
                        },
                        restore: { show: true },
                        saveAsImage: { show: true }
                    }
                },
                calculable: true,
                series: [
                    {
                        name: name,
                        type: 'pie',
                        radius: '55%',
                        center: ['50%', '60%'],
                        data: pie_datas.data
                    }
                ]
            };
            return $.extend({}, ECharts.ChartOptionTemplates.CommonOption, option);
        },


        //标准折线图：数据，数据主题，标题，子标题，data:数据格式：{Name：xxx,Group:xxx,Value:xxx}
        StandardLines: function (data, name,text, subtext, is_stack) {
            var stackline_datas = ECharts.ChartDataFormate.FormateGroupData(data, 'line', is_stack);
            var option = {
                title: {
                    text: text,
                    subtext: subtext,
                },
                tooltip: {
                    trigger: 'axis'
                },                
                toolbox: {
                    show: true,
                    feature: {
                        mark: { show: true },
                        dataView: { show: true, readOnly: false },
                        magicType: { show: true, type: ['line', 'bar'] },
                        restore: { show: true },
                        saveAsImage: { show: true }
                    }
                },
                calculable: true,
                legend: {
                    data: stackline_datas.category
                },
                xAxis: [{
                    type: 'category', //X轴均为category，Y轴均为value 
                    data: stackline_datas.xAxis,
                    boundaryGap: false//数值轴两端的空白策略 
                }],
                yAxis: [{
                    type: 'value'
                }],
                series: stackline_datas.series
            };
            return $.extend({}, ECharts.ChartOptionTemplates.CommonLineOption, option);
        },


        //标准柱状图：数据，标题，子标题，data:数据格式：{Name：xxx,Group:xxx,Value:xxx}
        StandardBars: function (data, text,subtext, is_stack) {
            var bars_datas = ECharts.ChartDataFormate.FormateGroupData(data, 'bar', is_stack);
            var option = {
                title: {
                    text: text,
                    subtext: subtext,
                },
                tooltip: {
                    trigger: 'axis'
                },
                toolbox: {
                    show: true,
                    feature: {
                        mark: { show: true },
                        dataView: { show: true, readOnly: false },
                        magicType: { show: true, type: ['line', 'bar'] },
                        restore: { show: true },
                        saveAsImage: { show: true }
                    }
                },
                calculable: true,
                legend: {
                    data: bars_datas.category
                },
                xAxis: [{
                    type: 'category',
                    data: bars_datas.xAxis,
                }],
                yAxis: [{
                    type: 'value',
                }],
                series: bars_datas.series
            };
            return $.extend({}, ECharts.ChartOptionTemplates.CommonLineOption, option);
        }
    }
 ,

    //图表渲染。
    Charts: {
        RenderChart: function (option) {
            require([

                'echarts',
                'echarts/chart/line',
                'echarts/chart/bar',
                'echarts/chart/pie',
                'echarts/chart/k',
                'echarts/chart/scatter',
                'echarts/chart/radar',
                'echarts/chart/chord',
                'echarts/chart/force',
                'echarts/chart/map'
                ],

              function (ec) {
                  echarts = ec;
                  if (option.chart && option.chart.dispose)
                      option.chart.dispose();

                  option.chart = echarts.init(option.container);
                  window.onresize = option.chart.resize;
                  option.chart.setOption(option.option, true);
              });
        }
    }
  ,
    RenderMap: function (option) { }

};
  