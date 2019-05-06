
//标准饼图：数据，Div标签，数据主题，标题，子标题。
function DrawPie(data, id, name, text, subtext) {
    var option = ECharts.ChartOptionTemplates.StandardPie(data, name, text, subtext);
    var container = document.getElementById(id);
    opt = ECharts.ChartConfig(container, option);
    ECharts.Charts.RenderChart(opt);
}

//标准柱状图：数据，Div标签，标题，子标题。
function DrawBars(data, id, text, subtext) {

    var option = ECharts.ChartOptionTemplates.StandardBars(data, text, subtext);
    var container = document.getElementById(id);
    opt = ECharts.ChartConfig(container, option);
    ECharts.Charts.RenderChart(opt);
}

//标准折线图：数据，Div标签，标题，子标题。
function DrawLines(data, id, text, subtext) {

    var option = ECharts.ChartOptionTemplates.StandardLines(data, text, subtext);
    var container = document.getElementById(id);
    opt = ECharts.ChartConfig(container, option);
    ECharts.Charts.RenderChart(opt);
}