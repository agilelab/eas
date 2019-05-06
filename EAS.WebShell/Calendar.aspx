<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Calendar.aspx.cs" Inherits="EAS.WebShell.Calendar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=GBK">
    <title>万年历</title>
    <meta content="农历; 阳历; 月历; 节日; 时区; 节气; 八字; 干支; 生肖; gregorian solar; chinese lunar; calendar;"
        name="keywords">
    <meta content="All" name="robots">
    <meta content="gregorian solar calendar and chinese lunar calendar" name="description">
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
        }
    </style>
</head>
<body>
    <center>
        <br>
        <table cellpadding="0" cellspacing="0" id="1">
            <tbody>
                <tr>
                    <td>
                        <style>
                            #cal
                            {
                                width: 434px;
                                border: 1px solid #c3d9ff;
                                font-size: 12px;
                                margin: 8px 0 0 15px;
                            }
                            #cal #top
                            {
                                height: 29px;
                                line-height: 29px;
                                background: #e7eef8;
                                color: #003784;
                                padding-left: 30px;
                            }
                            #cal #top select
                            {
                                font-size: 12px;
                            }
                            #cal #top input
                            {
                                padding: 0;
                            }
                            #cal ul#wk
                            {
                                margin: 0;
                                padding: 0;
                                height: 25px;
                            }
                            #cal ul#wk li
                            {
                                float: left;
                                width: 60px;
                                text-align: center;
                                line-height: 25px;
                                list-style: none;
                            }
                            #cal ul#wk li b
                            {
                                font-weight: normal;
                                color: #c60b02;
                            }
                            #cal #cm
                            {
                                clear: left;
                                border-top: 1px solid #ddd;
                                border-bottom: 1px dotted #ddd;
                                position: relative;
                            }
                            #cal #cm .cell
                            {
                                position: absolute;
                                width: 42px;
                                height: 36px;
                                text-align: center;
                                margin: 0 0 0 9px;
                            }
                            #cal #cm .cell .so
                            {
                                font: bold 16px arial;
                            }
                            #cal #bm
                            {
                                text-align: right;
                                height: 24px;
                                line-height: 24px;
                                padding: 0 13px 0 0;
                            }
                            #cal #bm a
                            {
                                color: 7977ce;
                            }
                            #cal #fd
                            {
                                display: none;
                                position: absolute;
                                border: 1px solid #dddddf;
                                background: #feffcd;
                                padding: 10px;
                                line-height: 21px;
                                width: 150px;
                            }
                            #cal #fd b
                            {
                                font-weight: normal;
                                color: #c60a00;
                            }
                        </style>
                        <!--[if IE]>
<style>#cal #top{padding-top:4px;}#cal #top input{width:65px;}#cal #fd{width:170px;}</style>
<![endif]-->
                        <div id="cal">
                            <div id="top">
                                公元&nbsp;<select><option value="1901">1901</option>
                                    <option value="1902">1902</option>
                                    <option value="1903">1903</option>
                                    <option value="1904">1904</option>
                                    <option value="1905">1905</option>
                                    <option value="1906">1906</option>
                                    <option value="1907">1907</option>
                                    <option value="1908">1908</option>
                                    <option value="1909">1909</option>
                                    <option value="1910">1910</option>
                                    <option value="1911">1911</option>
                                    <option value="1912">1912</option>
                                    <option value="1913">1913</option>
                                    <option value="1914">1914</option>
                                    <option value="1915">1915</option>
                                    <option value="1916">1916</option>
                                    <option value="1917">1917</option>
                                    <option value="1918">1918</option>
                                    <option value="1919">1919</option>
                                    <option value="1920">1920</option>
                                    <option value="1921">1921</option>
                                    <option value="1922">1922</option>
                                    <option value="1923">1923</option>
                                    <option value="1924">1924</option>
                                    <option value="1925">1925</option>
                                    <option value="1926">1926</option>
                                    <option value="1927">1927</option>
                                    <option value="1928">1928</option>
                                    <option value="1929">1929</option>
                                    <option value="1930">1930</option>
                                    <option value="1931">1931</option>
                                    <option value="1932">1932</option>
                                    <option value="1933">1933</option>
                                    <option value="1934">1934</option>
                                    <option value="1935">1935</option>
                                    <option value="1936">1936</option>
                                    <option value="1937">1937</option>
                                    <option value="1938">1938</option>
                                    <option value="1939">1939</option>
                                    <option value="1940">1940</option>
                                    <option value="1941">1941</option>
                                    <option value="1942">1942</option>
                                    <option value="1943">1943</option>
                                    <option value="1944">1944</option>
                                    <option value="1945">1945</option>
                                    <option value="1946">1946</option>
                                    <option value="1947">1947</option>
                                    <option value="1948">1948</option>
                                    <option value="1949">1949</option>
                                    <option value="1950">1950</option>
                                    <option value="1951">1951</option>
                                    <option value="1952">1952</option>
                                    <option value="1953">1953</option>
                                    <option value="1954">1954</option>
                                    <option value="1955">1955</option>
                                    <option value="1956">1956</option>
                                    <option value="1957">1957</option>
                                    <option value="1958">1958</option>
                                    <option value="1959">1959</option>
                                    <option value="1960">1960</option>
                                    <option value="1961">1961</option>
                                    <option value="1962">1962</option>
                                    <option value="1963">1963</option>
                                    <option value="1964">1964</option>
                                    <option value="1965">1965</option>
                                    <option value="1966">1966</option>
                                    <option value="1967">1967</option>
                                    <option value="1968">1968</option>
                                    <option value="1969">1969</option>
                                    <option value="1970">1970</option>
                                    <option value="1971">1971</option>
                                    <option value="1972">1972</option>
                                    <option value="1973">1973</option>
                                    <option value="1974">1974</option>
                                    <option value="1975">1975</option>
                                    <option value="1976">1976</option>
                                    <option value="1977">1977</option>
                                    <option value="1978">1978</option>
                                    <option value="1979">1979</option>
                                    <option value="1980">1980</option>
                                    <option value="1981">1981</option>
                                    <option value="1982">1982</option>
                                    <option value="1983">1983</option>
                                    <option value="1984">1984</option>
                                    <option value="1985">1985</option>
                                    <option value="1986">1986</option>
                                    <option value="1987">1987</option>
                                    <option value="1988">1988</option>
                                    <option value="1989">1989</option>
                                    <option value="1990">1990</option>
                                    <option value="1991">1991</option>
                                    <option value="1992">1992</option>
                                    <option value="1993">1993</option>
                                    <option value="1994">1994</option>
                                    <option value="1995">1995</option>
                                    <option value="1996">1996</option>
                                    <option value="1997">1997</option>
                                    <option value="1998">1998</option>
                                    <option value="1999">1999</option>
                                    <option value="2000">2000</option>
                                    <option value="2001">2001</option>
                                    <option value="2002">2002</option>
                                    <option value="2003">2003</option>
                                    <option value="2004">2004</option>
                                    <option value="2005">2005</option>
                                    <option value="2006">2006</option>
                                    <option value="2007">2007</option>
                                    <option value="2008">2008</option>
                                    <option value="2009">2009</option>
                                    <option value="2010">2010</option>
                                    <option value="2011">2011</option>
                                    <option value="2012">2012</option>
                                    <option value="2013">2013</option>
                                    <option value="2014">2014</option>
                                    <option value="2015">2015</option>
                                    <option value="2016">2016</option>
                                    <option value="2017">2017</option>
                                    <option value="2018">2018</option>
                                    <option value="2019">2019</option>
                                    <option value="2020">2020</option>
                                    <option value="2021">2021</option>
                                    <option value="2022">2022</option>
                                    <option value="2023">2023</option>
                                    <option value="2024">2024</option>
                                    <option value="2025">2025</option>
                                    <option value="2026">2026</option>
                                    <option value="2027">2027</option>
                                    <option value="2028">2028</option>
                                    <option value="2029">2029</option>
                                    <option value="2030">2030</option>
                                    <option value="2031">2031</option>
                                    <option value="2032">2032</option>
                                    <option value="2033">2033</option>
                                    <option value="2034">2034</option>
                                    <option value="2035">2035</option>
                                    <option value="2036">2036</option>
                                    <option value="2037">2037</option>
                                    <option value="2038">2038</option>
                                    <option value="2039">2039</option>
                                    <option value="2040">2040</option>
                                    <option value="2041">2041</option>
                                    <option value="2042">2042</option>
                                    <option value="2043">2043</option>
                                    <option value="2044">2044</option>
                                    <option value="2045">2045</option>
                                    <option value="2046">2046</option>
                                    <option value="2047">2047</option>
                                    <option value="2048">2048</option>
                                    <option value="2049">2049</option>
                                </select>&nbsp;年&nbsp;<select><option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5">5</option>
                                    <option value="6">6</option>
                                    <option value="7">7</option>
                                    <option value="8">8</option>
                                    <option value="9">9</option>
                                    <option value="10">10</option>
                                    <option value="11">11</option>
                                    <option value="12">12</option>
                                </select>&nbsp;月&nbsp;&nbsp;&nbsp;&nbsp;农历<span>庚寅</span>年&nbsp;[&nbsp;<span>虎</span>年&nbsp;]&nbsp;&nbsp;&nbsp;&nbsp;<input
                                    type="button" value="回到今天" title="点击后跳转回今天" style="padding-top: 0px; padding-right: 0px;
                                    padding-bottom: 0px; padding-left: 0px; visibility: hidden;"></div>
                            <ul id="wk">
                                <li>一</li><li>二</li><li>三</li><li>四</li><li>五</li><li><b>六</b></li><li><b>日</b></li></ul>
                            <div id="cm" style="height: 192px;">
                                <div class="cell" style="left: 180px; top: 2px;">
                                    <div class="so" style="color: rgb(49, 49, 49);">
                                        1</div>
                                    <div style="color: rgb(102, 102, 102);">
                                        中国...</div>
                                </div>
                                <div class="cell" style="left: 240px; top: 2px;">
                                    <div class="so" style="color: rgb(49, 49, 49);">
                                        2</div>
                                    <div style="color: rgb(102, 102, 102);">
                                        廿一</div>
                                </div>
                                <div class="cell" style="left: 300px; top: 2px;">
                                    <div class="so" style="color: rgb(198, 11, 2);">
                                        3</div>
                                    <div style="color: rgb(102, 102, 102);">
                                        廿二</div>
                                </div>
                                <div class="cell" style="left: 360px; top: 2px;">
                                    <div class="so" style="color: rgb(198, 11, 2);">
                                        4</div>
                                    <div style="color: rgb(102, 102, 102);">
                                        廿三</div>
                                </div>
                                <div class="cell" style="left: 0px; top: 40px;">
                                    <div class="so" style="color: rgb(49, 49, 49);">
                                        5</div>
                                    <div style="color: rgb(102, 102, 102);">
                                        廿四</div>
                                </div>
                                <div class="cell" style="left: 60px; top: 40px;">
                                    <div class="so" style="color: rgb(49, 49, 49);">
                                        6</div>
                                    <div style="color: rgb(102, 102, 102);">
                                        廿五</div>
                                </div>
                                <div class="cell" style="left: 120px; top: 40px;">
                                    <div class="so" style="color: rgb(49, 49, 49);">
                                        7</div>
                                    <div style="color: rgb(102, 102, 102);">
                                        小暑</div>
                                </div>
                                <div class="cell" style="left: 180px; top: 40px;">
                                    <div class="so" style="color: rgb(49, 49, 49);">
                                        8</div>
                                    <div style="color: rgb(102, 102, 102);">
                                        廿七</div>
                                </div>
                                <div class="cell" style="left: 240px; top: 40px;">
                                    <div class="so" style="color: rgb(49, 49, 49);">
                                        9</div>
                                    <div style="color: rgb(102, 102, 102);">
                                        廿八</div>
                                </div>
                                <div class="cell" style="left: 300px; top: 40px;">
                                    <div class="so" style="color: rgb(198, 11, 2);">
                                        10</div>
                                    <div style="color: rgb(102, 102, 102);">
                                        廿九</div>
                                </div>
                                <div class="cell" style="left: 360px; top: 40px;">
                                    <div class="so" style="color: rgb(198, 11, 2);">
                                        11</div>
                                    <div style="color: rgb(102, 102, 102);">
                                        三十</div>
                                </div>
                                <div class="cell" style="left: 0px; top: 78px;">
                                    <div class="so" style="color: rgb(49, 49, 49);">
                                        12</div>
                                    <div style="color: rgb(102, 102, 102);">
                                        六月</div>
                                </div>
                                <div class="cell" style="left: 60px; top: 78px;">
                                    <div class="so" style="color: rgb(49, 49, 49);">
                                        13</div>
                                    <div style="color: rgb(102, 102, 102);">
                                        初二</div>
                                </div>
                                <div class="cell" style="left: 120px; top: 78px;">
                                    <div class="so" style="color: rgb(49, 49, 49);">
                                        14</div>
                                    <div style="color: rgb(102, 102, 102);">
                                        初三</div>
                                </div>
                                <div class="cell" style="left: 180px; top: 78px;">
                                    <div class="so" style="color: rgb(49, 49, 49);">
                                        15</div>
                                    <div style="color: rgb(102, 102, 102);">
                                        初四</div>
                                </div>
                                <div class="cell" style="left: 240px; top: 78px;">
                                    <div class="so" style="color: rgb(49, 49, 49);">
                                        16</div>
                                    <div style="color: rgb(102, 102, 102);">
                                        初五</div>
                                </div>
                                <div class="cell" style="left: 300px; top: 78px;">
                                    <div class="so" style="color: rgb(198, 11, 2);">
                                        17</div>
                                    <div style="color: rgb(102, 102, 102);">
                                        初六</div>
                                </div>
                                <div style="border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px;
                                    border-left-width: 1px; border-top-style: solid; border-right-style: solid; border-bottom-style: solid;
                                    border-left-style: solid; border-top-color: rgb(165, 185, 218); border-right-color: rgb(165, 185, 218);
                                    border-bottom-color: rgb(165, 185, 218); border-left-color: rgb(165, 185, 218);
                                    background-image: initial; background-attachment: initial; background-origin: initial;
                                    background-clip: initial; background-color: rgb(193, 217, 255); left: 360px;
                                    top: 78px; background-position: initial initial; background-repeat: initial initial;"
                                    class="cell">
                                    <div class="so" style="color: rgb(198, 11, 2);">
                                        18</div>
                                    <div style="color: rgb(102, 102, 102);">
                                        初七</div>
                                </div>
                                <div class="cell" style="left: 0px; top: 116px;">
                                    <div class="so" style="color: rgb(49, 49, 49);">
                                        19</div>
                                    <div style="color: rgb(102, 102, 102);">
                                        初八</div>
                                </div>
                                <div class="cell" style="left: 60px; top: 116px;">
                                    <div class="so" style="color: rgb(49, 49, 49);">
                                        20</div>
                                    <div style="color: rgb(102, 102, 102);">
                                        初九</div>
                                </div>
                                <div class="cell" style="left: 120px; top: 116px;">
                                    <div class="so" style="color: rgb(49, 49, 49);">
                                        21</div>
                                    <div style="color: rgb(102, 102, 102);">
                                        初十</div>
                                </div>
                                <div class="cell" style="left: 180px; top: 116px;">
                                    <div class="so" style="color: rgb(49, 49, 49);">
                                        22</div>
                                    <div style="color: rgb(102, 102, 102);">
                                        十一</div>
                                </div>
                                <div class="cell" style="left: 240px; top: 116px;">
                                    <div class="so" style="color: rgb(49, 49, 49);">
                                        23</div>
                                    <div style="color: rgb(102, 102, 102);">
                                        大暑</div>
                                </div>
                                <div class="cell" style="left: 300px; top: 116px;">
                                    <div class="so" style="color: rgb(198, 11, 2);">
                                        24</div>
                                    <div style="color: rgb(102, 102, 102);">
                                        十三</div>
                                </div>
                                <div class="cell" style="left: 360px; top: 116px;">
                                    <div class="so" style="color: rgb(198, 11, 2);">
                                        25</div>
                                    <div style="color: rgb(102, 102, 102);">
                                        十四</div>
                                </div>
                                <div class="cell" style="left: 0px; top: 154px;">
                                    <div class="so" style="color: rgb(49, 49, 49);">
                                        26</div>
                                    <div style="color: rgb(102, 102, 102);">
                                        十五</div>
                                </div>
                                <div class="cell" style="left: 60px; top: 154px;">
                                    <div class="so" style="color: rgb(49, 49, 49);">
                                        27</div>
                                    <div style="color: rgb(102, 102, 102);">
                                        十六</div>
                                </div>
                                <div class="cell" style="left: 120px; top: 154px;">
                                    <div class="so" style="color: rgb(49, 49, 49);">
                                        28</div>
                                    <div style="color: rgb(102, 102, 102);">
                                        十七</div>
                                </div>
                                <div class="cell" style="left: 180px; top: 154px;">
                                    <div class="so" style="color: rgb(49, 49, 49);">
                                        29</div>
                                    <div style="color: rgb(102, 102, 102);">
                                        十八</div>
                                </div>
                                <div class="cell" style="left: 240px; top: 154px;">
                                    <div class="so" style="color: rgb(49, 49, 49);">
                                        30</div>
                                    <div style="color: rgb(102, 102, 102);">
                                        十九</div>
                                </div>
                                <div class="cell" style="left: 300px; top: 154px;">
                                    <div class="so" style="color: rgb(198, 11, 2);">
                                        31</div>
                                    <div style="color: rgb(102, 102, 102);">
                                        二十</div>
                                </div>
                                <div id="fd" style="top: 33px; left: 286px; display: none;">
                                    2010&nbsp;年&nbsp;7&nbsp;月&nbsp;2&nbsp;日&nbsp;星期五<br>
                                    <b>农历&nbsp;五月廿一</b><br>
                                    庚寅年&nbsp;壬午月&nbsp;癸丑日</div>
                            </div>
                            <div id="bm">
                                <a target="_blank" onmousedown="return c({&#39;fm&#39;:&#39;alop&#39;,&#39;title&#39;:this.innerHTML,&#39;url&#39;:this.href,&#39;p1&#39;:al_c(this),&#39;p2&#39;:1})"
                                    href="http://zh.wikipedia.org/zh-cn/7%E6%9C%8818%E6%97%A5">历史上的今天</a></div>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </center>
    <script language="JavaScript"> 
<!--
        (function () { var S = navigator.userAgent.indexOf("MSIE") != -1 && !window.opera; function M(C) { return document.getElementById(C) } function R(C) { return document.createElement(C) } var P = [0x04bd8, 0x04ae0, 0x0a570, 0x054d5, 0x0d260, 0x0d950, 0x16554, 0x056a0, 0x09ad0, 0x055d2, 0x04ae0, 0x0a5b6, 0x0a4d0, 0x0d250, 0x1d255, 0x0b540, 0x0d6a0, 0x0ada2, 0x095b0, 0x14977, 0x04970, 0x0a4b0, 0x0b4b5, 0x06a50, 0x06d40, 0x1ab54, 0x02b60, 0x09570, 0x052f2, 0x04970, 0x06566, 0x0d4a0, 0x0ea50, 0x06e95, 0x05ad0, 0x02b60, 0x186e3, 0x092e0, 0x1c8d7, 0x0c950, 0x0d4a0, 0x1d8a6, 0x0b550, 0x056a0, 0x1a5b4, 0x025d0, 0x092d0, 0x0d2b2, 0x0a950, 0x0b557, 0x06ca0, 0x0b550, 0x15355, 0x04da0, 0x0a5b0, 0x14573, 0x052b0, 0x0a9a8, 0x0e950, 0x06aa0, 0x0aea6, 0x0ab50, 0x04b60, 0x0aae4, 0x0a570, 0x05260, 0x0f263, 0x0d950, 0x05b57, 0x056a0, 0x096d0, 0x04dd5, 0x04ad0, 0x0a4d0, 0x0d4d4, 0x0d250, 0x0d558, 0x0b540, 0x0b6a0, 0x195a6, 0x095b0, 0x049b0, 0x0a974, 0x0a4b0, 0x0b27a, 0x06a50, 0x06d40, 0x0af46, 0x0ab60, 0x09570, 0x04af5, 0x04970, 0x064b0, 0x074a3, 0x0ea50, 0x06b58, 0x055c0, 0x0ab60, 0x096d5, 0x092e0, 0x0c960, 0x0d954, 0x0d4a0, 0x0da50, 0x07552, 0x056a0, 0x0abb7, 0x025d0, 0x092d0, 0x0cab5, 0x0a950, 0x0b4a0, 0x0baa4, 0x0ad50, 0x055d9, 0x04ba0, 0x0a5b0, 0x15176, 0x052b0, 0x0a930, 0x07954, 0x06aa0, 0x0ad50, 0x05b52, 0x04b60, 0x0a6e6, 0x0a4e0, 0x0d260, 0x0ea65, 0x0d530, 0x05aa0, 0x076a3, 0x096d0, 0x04bd7, 0x04ad0, 0x0a4d0, 0x1d0b6, 0x0d250, 0x0d520, 0x0dd45, 0x0b5a0, 0x056d0, 0x055b2, 0x049b0, 0x0a577, 0x0a4b0, 0x0aa50, 0x1b255, 0x06d20, 0x0ada0, 0x14b63]; var K = "甲乙丙丁戊己庚辛壬癸"; var J = "子丑寅卯辰巳午未申酉戌亥"; var O = "鼠牛虎兔龙蛇马羊猴鸡狗猪"; var L = ["小寒", "大寒", "立春", "雨水", "惊蛰", "春分", "清明", "谷雨", "立夏", "小满", "芒种", "夏至", "小暑", "大暑", "立秋", "处暑", "白露", "秋分", "寒露", "霜降", "立冬", "小雪", "大雪", "冬至"]; var D = [0, 21208, 43467, 63836, 85337, 107014, 128867, 150921, 173149, 195551, 218072, 240693, 263343, 285989, 308563, 331033, 353350, 375494, 397447, 419210, 440795, 462224, 483532, 504758]; var B = "日一二三四五六七八九十"; var H = ["正", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "腊"]; var E = "初十廿卅"; var V = { "0101": "*1元旦节", "0214": "情人节", "0305": "学雷锋纪念日", "0308": "妇女节", "0312": "植树节", "0315": "消费者权益日", "0401": "愚人节", "0501": "*1劳动节", "0504": "青年节", "0601": "国际儿童节", "0701": "中国共产党诞辰", "0801": "建军节", "0910": "中国教师节", "1001": "*3国庆节", "1224": "平安夜", "1225": "圣诞节" }; var T = { "0101": "*2春节", "0115": "元宵节", "0505": "*1端午节", "0815": "*1中秋节", "0909": "重阳节", "1208": "腊八节", "0100": "除夕" }; function U(Y) { function c(j, i) { var h = new Date((31556925974.7 * (j - 1900) + D[i] * 60000) + Date.UTC(1900, 0, 6, 2, 5)); return (h.getUTCDate()) } function d(k) { var h, j = 348; for (h = 32768; h > 8; h >>= 1) { j += (P[k - 1900] & h) ? 1 : 0 } return (j + b(k)) } function a(h) { return (K.charAt(h % 10) + J.charAt(h % 12)) } function b(h) { if (g(h)) { return ((P[h - 1900] & 65536) ? 30 : 29) } else { return (0) } } function g(h) { return (P[h - 1900] & 15) } function e(i, h) { return ((P[i - 1900] & (65536 >> h)) ? 30 : 29) } function C(m) { var k, j = 0, h = 0; var l = new Date(1900, 0, 31); var n = (m - l) / 86400000; this.dayCyl = n + 40; this.monCyl = 14; for (k = 1900; k < 2050 && n > 0; k++) { h = d(k); n -= h; this.monCyl += 12 } if (n < 0) { n += h; k--; this.monCyl -= 12 } this.year = k; this.yearCyl = k - 1864; j = g(k); this.isLeap = false; for (k = 1; k < 13 && n > 0; k++) { if (j > 0 && k == (j + 1) && this.isLeap == false) { --k; this.isLeap = true; h = b(this.year) } else { h = e(this.year, k) } if (this.isLeap == true && k == (j + 1)) { this.isLeap = false } n -= h; if (this.isLeap == false) { this.monCyl++ } } if (n == 0 && j > 0 && k == j + 1) { if (this.isLeap) { this.isLeap = false } else { this.isLeap = true; --k; --this.monCyl } } if (n < 0) { n += h; --k; --this.monCyl } this.month = k; this.day = n + 1 } function G(h) { return h < 10 ? "0" + h : h } function f(i, j) { var h = i; return j.replace(/dd?d?d?|MM?M?M?|yy?y?y?/g, function (k) { switch (k) { case "yyyy": var l = "000" + h.getFullYear(); return l.substring(l.length - 4); case "dd": return G(h.getDate()); case "d": return h.getDate().toString(); case "MM": return G((h.getMonth() + 1)); case "M": return h.getMonth() + 1 } }) } function Z(i, h) { var j; switch (i, h) { case 10: j = "初十"; break; case 20: j = "二十"; break; case 30: j = "三十"; break; default: j = E.charAt(Math.floor(h / 10)); j += B.charAt(h % 10) } return (j) } this.date = Y; this.isToday = false; this.isRestDay = false; this.solarYear = f(Y, "yyyy"); this.solarMonth = f(Y, "M"); this.solarDate = f(Y, "d"); this.solarWeekDay = Y.getDay(); this.solarWeekDayInChinese = "星期" + B.charAt(this.solarWeekDay); var X = new C(Y); this.lunarYear = X.year; this.shengxiao = O.charAt((this.lunarYear - 4) % 12); this.lunarMonth = X.month; this.lunarIsLeapMonth = X.isLeap; this.lunarMonthInChinese = this.lunarIsLeapMonth ? "闰" + H[X.month - 1] : H[X.month - 1]; this.lunarDate = X.day; this.showInLunar = this.lunarDateInChinese = Z(this.lunarMonth, this.lunarDate); if (this.lunarDate == 1) { this.showInLunar = this.lunarMonthInChinese + "月" } this.ganzhiYear = a(X.yearCyl); this.ganzhiMonth = a(X.monCyl); this.ganzhiDate = a(X.dayCyl++); this.jieqi = ""; this.restDays = 0; if (c(this.solarYear, (this.solarMonth - 1) * 2) == f(Y, "d")) { this.showInLunar = this.jieqi = L[(this.solarMonth - 1) * 2] } if (c(this.solarYear, (this.solarMonth - 1) * 2 + 1) == f(Y, "d")) { this.showInLunar = this.jieqi = L[(this.solarMonth - 1) * 2 + 1] } if (this.showInLunar == "清明") { this.showInLunar = "清明节"; this.restDays = 1 } this.solarFestival = V[f(Y, "MM") + f(Y, "dd")]; if (typeof this.solarFestival == "undefined") { this.solarFestival = "" } else { if (/\*(\d)/.test(this.solarFestival)) { this.restDays = parseInt(RegExp.$1); this.solarFestival = this.solarFestival.replace(/\*\d/, "") } } this.showInLunar = (this.solarFestival == "") ? this.showInLunar : this.solarFestival; this.lunarFestival = T[this.lunarIsLeapMonth ? "00" : G(this.lunarMonth) + G(this.lunarDate)]; if (typeof this.lunarFestival == "undefined") { this.lunarFestival = "" } else { if (/\*(\d)/.test(this.lunarFestival)) { this.restDays = (this.restDays > parseInt(RegExp.$1)) ? this.restDays : parseInt(RegExp.$1); this.lunarFestival = this.lunarFestival.replace(/\*\d/, "") } } if (this.lunarMonth == 12 && this.lunarDate == e(this.lunarYear, 12)) { this.lunarFestival = T["0100"]; this.restDays = 1 } this.showInLunar = (this.lunarFestival == "") ? this.showInLunar : this.lunarFestival; this.showInLunar = (this.showInLunar.length > 4) ? this.showInLunar.substr(0, 2) + "..." : this.showInLunar } var Q = (function () { var X = {}; X.lines = 0; X.dateArray = new Array(42); function Y(a) { return (((a % 4 === 0) && (a % 100 !== 0)) || (a % 400 === 0)) } function G(a, b) { return [31, (Y(a) ? 29 : 28), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31][b] } function C(a, b) { a.setDate(a.getDate() + b); return a } function Z(a) { var f = 0; var c = new U(new Date(a.solarYear, a.solarMonth - 1, 1)); var d = (c.solarWeekDay - 1 == -1) ? 6 : c.solarWeekDay - 1; X.lines = Math.ceil((d + G(a.solarYear, a.solarMonth - 1)) / 7); for (var e = 0; e < X.dateArray.length; e++) { if (c.restDays != 0) { f = c.restDays } if (f > 0) { c.isRest = true } if (d-- > 0 || c.solarMonth != a.solarMonth) { X.dateArray[e] = null; continue } var b = new U(new Date()); if (c.solarYear == b.solarYear && c.solarMonth == b.solarMonth && c.solarDate == b.solarDate) { c.isToday = true } X.dateArray[e] = c; c = new U(C(c.date, 1)); f-- } } return { init: function (a) { Z(a) }, getJson: function () { return X } } })(); var W = (function () { var C = M("top").getElementsByTagName("SELECT")[0]; var X = M("top").getElementsByTagName("SELECT")[1]; var G = M("top").getElementsByTagName("SPAN")[0]; var c = M("top").getElementsByTagName("SPAN")[1]; var Y = M("top").getElementsByTagName("INPUT")[0]; function a(g) { G.innerHTML = g.ganzhiYear; c.innerHTML = g.shengxiao } function b(g) { C[g.solarYear - 1901].selected = true; X[g.solarMonth - 1].selected = true } function f() { var j = C.value; var g = X.value; var i = new U(new Date(j, g - 1, 1)); Q.init(i); N.draw(); if (this == C) { i = new U(new Date(j, 3, 1)); G.innerHTML = i.ganzhiYear; c.innerHTML = i.shengxiao } var h = new U(new Date()); Y.style.visibility = (j == h.solarYear && g == h.solarMonth) ? "hidden" : "visible" } function Z() { var g = new U(new Date()); a(g); b(g); Q.init(g); N.draw(); Y.style.visibility = "hidden" } function d(k, g) { for (var j = 1901; j < 2050; j++) { var h = R("OPTION"); h.value = j; h.innerHTML = j; if (j == k) { h.selected = "selected" } C.appendChild(h) } for (var j = 1; j < 13; j++) { var h = R("OPTION"); h.value = j; h.innerHTML = j; if (j == g) { h.selected = "selected" } X.appendChild(h) } C.onchange = f; X.onchange = f } function e(g) { d(g.solarYear, g.solarMonth); G.innerHTML = g.ganzhiYear; c.innerHTML = g.shengxiao; Y.onclick = Z; Y.style.visibility = "hidden" } return { init: function (g) { e(g) }, reset: function (g) { b(g) } } })(); var N = (function () { function C() { var Z = Q.getJson(); var c = Z.dateArray; M("cm").style.height = Z.lines * 38 + 2 + "px"; M("cm").innerHTML = ""; for (var a = 0; a < c.length; a++) { if (c[a] == null) { continue } var X = R("DIV"); if (c[a].isToday) { X.style.border = "1px solid #a5b9da"; X.style.background = "#c1d9ff" } X.className = "cell"; X.style.left = (a % 7) * 60 + "px"; X.style.top = Math.floor(a / 7) * 38 + 2 + "px"; var b = R("DIV"); b.className = "so"; b.style.color = ((a % 7) > 4 || c[a].isRest) ? "#c60b02" : "#313131"; b.innerHTML = c[a].solarDate; X.appendChild(b); var Y = R("DIV"); Y.style.color = "#666"; Y.innerHTML = c[a].showInLunar; X.appendChild(Y); X.onmouseover = (function (d) { return function (f) { F.show({ dateIndex: d, cell: this }) } })(a); X.onmouseout = function () { F.hide() }; M("cm").appendChild(X) } var G = R("DIV"); G.id = "fd"; M("cm").appendChild(G); F.init(G) } return { draw: function (G) { C(G) } } })(); var F = (function () { var C; function Y(e, c) { if (arguments.length > 1) { var b = /([.*+?^=!:${}()|[\]\/\\])/g, Z = "{".replace(b, "\\$1"), d = "}".replace(b, "\\$1"); var a = new RegExp("#" + Z + "([^" + Z + d + "]+)" + d, "g"); if (typeof (c) == "object") { return e.replace(a, function (f, h) { var g = c[h]; return typeof (g) == "undefined" ? "" : g }) } } return e } function G(b) { var a = Q.getJson().dateArray[b.dateIndex]; var Z = b.cell; var c = "#{solarYear}&nbsp;年&nbsp;#{solarMonth}&nbsp;月&nbsp;#{solarDate}&nbsp;日&nbsp;#{solarWeekDayInChinese}"; c += "<br><b>农历&nbsp;#{lunarMonthInChinese}月#{lunarDateInChinese}</b>"; c += "<br>#{ganzhiYear}年&nbsp;#{ganzhiMonth}月&nbsp;#{ganzhiDate}日"; if (a.solarFestival != "" || a.lunarFestival != "" || a.jieqi != "") { c += "<br><b>#{lunarFestival} #{solarFestival} #{jieqi}</b>" } C.innerHTML = Y(c, a); C.style.top = Z.offsetTop + Z.offsetHeight - 5 + "px"; C.style.left = Z.offsetLeft + Z.offsetWidth - 5 + "px"; C.style.display = "block" } function X() { C.style.display = "none" } return { show: function (Z) { G(Z) }, hide: function () { X() }, init: function (Z) { C = Z } } })(); var I = (function () { var G = M("bm").getElementsByTagName("A")[0]; function C(X) { G.href = "http://zh.wikipedia.org/zh-cn/" + X.solarMonth + "%E6%9C%88" + X.solarDate + "%E6%97%A5" } return { setLink: function (X) { C(X) } } })(); var A = new U(new Date()); if (S) { window.attachEvent("onload", function () { W.reset(A) }) } W.init(A); Q.init(A); N.draw(); I.setLink(A) })();
 
//-->
</script>
</body>
</html>
