<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Calculator.aspx.cs" Inherits="EAS.WebShell.Calculator" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>科学计算器</title>
    <meta content="text/html; charset=gb2312" http-equiv="Content-Type">
    <!--written by GoldHuman li hai-->
    <!--2000.8-->
    <style>
        body
        {
            background-color: #edf0e1;
            background-attachment: fixed;
            font-family: "宋体" , "Arial" , "Times New Roman";
            color: #0001fc;
            font-size: 9pt;
        }
        td
        {
            font-family: "宋体" , "Arial Narrow" , "Times New Roman";
            font-size: 9pt;
            font-color: #000000;
        }
        .buttons table input
        {
            width: 50px;
        }
    </style>
    <script language="javascript">
<!--
        var endNumber = true
        var mem = 0
        var carry = 10
        var hexnum = "0123456789abcdef"
        var angle = "d"
        var stack = ""
        var level = "0"
        var layer = 0


        //数字键

        function inputkey(key) {
            var index = key.charCodeAt(0);
            if ((carry == 2 && (index == 48 || index == 49))
	 || (carry == 8 && index >= 48 && index <= 55)
	 || (carry == 10 && (index >= 48 && index <= 57 || index == 46))
	 || (carry == 16 && ((index >= 48 && index <= 57) || (index >= 97 && index <= 102))))
                if (endNumber) {
                    endNumber = false
                    document.calc.display.value = key
                }
                else if (document.calc.display.value == null || document.calc.display.value == "0")
                    document.calc.display.value = key
                else
                    document.calc.display.value += key
            }

            function changeSign() {
                if (document.calc.display.value != "0")
                    if (document.calc.display.value.substr(0, 1) == "-")
                        document.calc.display.value = document.calc.display.value.substr(1)
                    else
                        document.calc.display.value = "-" + document.calc.display.value
                }

                //函数键

                function inputfunction(fun, shiftfun) {
                    endNumber = true
                    if (document.calc.shiftf.checked)
                        document.calc.display.value = decto(funcalc(shiftfun, (todec(document.calc.display.value, carry))), carry)
                    else
                        document.calc.display.value = decto(funcalc(fun, (todec(document.calc.display.value, carry))), carry)
                    document.calc.shiftf.checked = false
                    document.calc.hypf.checked = false
                    inputshift()
                }

                function inputtrig(trig, arctrig, hyp, archyp) {
                    if (document.calc.hypf.checked)
                        inputfunction(hyp, archyp)
                    else
                        inputfunction(trig, arctrig)
                }


                //运算符

                function operation(join, newlevel) {
                    endNumber = true
                    var temp = stack.substr(stack.lastIndexOf("(") + 1) + document.calc.display.value
                    while (newlevel != 0 && (newlevel <= (level.charAt(level.length - 1)))) {
                        temp = parse(temp)
                        level = level.slice(0, -1)
                    }
                    if (temp.match(/^(.*\d[\+\-\*\/\%\^\&\|x])?([+-]?[0-9a-f\.]+)$/))
                        document.calc.display.value = RegExp.$2
                    stack = stack.substr(0, stack.lastIndexOf("(") + 1) + temp + join
                    document.calc.operator.value = " " + join + " "
                    level = level + newlevel

                }

                //括号

                function addbracket() {
                    endNumber = true
                    document.calc.display.value = 0
                    stack = stack + "("
                    document.calc.operator.value = "   "
                    level = level + 0

                    layer += 1
                    document.calc.bracket.value = "(=" + layer
                }

                function disbracket() {
                    endNumber = true
                    var temp = stack.substr(stack.lastIndexOf("(") + 1) + document.calc.display.value
                    while ((level.charAt(level.length - 1)) > 0) {
                        temp = parse(temp)
                        level = level.slice(0, -1)
                    }

                    document.calc.display.value = temp
                    stack = stack.substr(0, stack.lastIndexOf("("))
                    document.calc.operator.value = "   "
                    level = level.slice(0, -1)

                    layer -= 1
                    if (layer > 0)
                        document.calc.bracket.value = "(=" + layer
                    else
                        document.calc.bracket.value = ""
                }

                //等号

                function result() {
                    endNumber = true
                    while (layer > 0)
                        disbracket()
                    var temp = stack + document.calc.display.value
                    while ((level.charAt(level.length - 1)) > 0) {
                        temp = parse(temp)
                        level = level.slice(0, -1)
                    }

                    document.calc.display.value = temp
                    document.calc.bracket.value = ""
                    document.calc.operator.value = ""
                    stack = ""
                    level = "0"
                }


                //修改键

                function backspace() {
                    if (!endNumber) {
                        if (document.calc.display.value.length > 1)
                            document.calc.display.value = document.calc.display.value.substring(0, document.calc.display.value.length - 1)
                        else
                            document.calc.display.value = 0
                    }
                }

                function clearall() {
                    document.calc.display.value = 0
                    endNumber = true
                    stack = ""
                    level = "0"
                    layer = ""
                    document.calc.operator.value = ""
                    document.calc.bracket.value = ""
                }


                //转换键

                function inputChangCarry(newcarry) {
                    endNumber = true
                    document.calc.display.value = (decto(todec(document.calc.display.value, carry), newcarry))
                    carry = newcarry

                    document.calc.sin.disabled = (carry != 10)
                    document.calc.cos.disabled = (carry != 10)
                    document.calc.tan.disabled = (carry != 10)
                    document.calc.bt.disabled = (carry != 10)
                    document.calc.pi.disabled = (carry != 10)
                    document.calc.e.disabled = (carry != 10)
                    document.calc.kp.disabled = (carry != 10)

                    document.calc.k2.disabled = (carry <= 2)
                    document.calc.k3.disabled = (carry <= 2)
                    document.calc.k4.disabled = (carry <= 2)
                    document.calc.k5.disabled = (carry <= 2)
                    document.calc.k6.disabled = (carry <= 2)
                    document.calc.k7.disabled = (carry <= 2)
                    document.calc.k8.disabled = (carry <= 8)
                    document.calc.k9.disabled = (carry <= 8)
                    document.calc.ka.disabled = (carry <= 10)
                    document.calc.kb.disabled = (carry <= 10)
                    document.calc.kc.disabled = (carry <= 10)
                    document.calc.kd.disabled = (carry <= 10)
                    document.calc.ke.disabled = (carry <= 10)
                    document.calc.kf.disabled = (carry <= 10)



                }

                function inputChangAngle(angletype) {
                    endNumber = true
                    angle = angletype
                    if (angle == "d")
                        document.calc.display.value = radiansToDegress(document.calc.display.value)
                    else
                        document.calc.display.value = degressToRadians(document.calc.display.value)
                    endNumber = true
                }

                function inputshift() {
                    if (document.calc.shiftf.checked) {
                        document.calc.bt.value = "deg "
                        document.calc.ln.value = "exp "
                        document.calc.log.value = "expd"

                        if (document.calc.hypf.checked) {
                            document.calc.sin.value = "ahs "
                            document.calc.cos.value = "ahc "
                            document.calc.tan.value = "aht "
                        }
                        else {
                            document.calc.sin.value = "asin"
                            document.calc.cos.value = "acos"
                            document.calc.tan.value = "atan"
                        }

                        document.calc.sqr.value = "x^.5"
                        document.calc.cube.value = "x^.3"

                        document.calc.floor.value = "小数"
                    }
                    else {
                        document.calc.bt.value = "d.ms"
                        document.calc.ln.value = " ln "
                        document.calc.log.value = "log "

                        if (document.calc.hypf.checked) {
                            document.calc.sin.value = "hsin"
                            document.calc.cos.value = "hcos"
                            document.calc.tan.value = "htan"
                        }
                        else {
                            document.calc.sin.value = "sin "
                            document.calc.cos.value = "cos "
                            document.calc.tan.value = "tan "
                        }

                        document.calc.sqr.value = "x^2 "
                        document.calc.cube.value = "x^3 "

                        document.calc.floor.value = "取整"
                    }

                }
                //存储器部分

                function clearmemory() {
                    mem = 0
                    document.calc.memory.value = "   "
                }

                function getmemory() {
                    endNumber = true
                    document.calc.display.value = decto(mem, carry)
                }

                function putmemory() {
                    endNumber = true
                    if (document.calc.display.value != 0) {
                        mem = todec(document.calc.display.value, carry)
                        document.calc.memory.value = " M "
                    }
                    else
                        document.calc.memory.value = "   "
                }

                function addmemory() {
                    endNumber = true
                    mem = parseFloat(mem) + parseFloat(todec(document.calc.display.value, carry))
                    if (mem == 0)
                        document.calc.memory.value = "   "
                    else
                        document.calc.memory.value = " M "
                }

                function multimemory() {
                    endNumber = true
                    mem = parseFloat(mem) * parseFloat(todec(document.calc.display.value, carry))
                    if (mem == 0)
                        document.calc.memory.value = "   "
                    else
                        document.calc.memory.value = " M "
                }

                //十进制转换

                function todec(num, oldcarry) {
                    if (oldcarry == 10 || num == 0) return (num)
                    var neg = (num.charAt(0) == "-")
                    if (neg) num = num.substr(1)
                    var newnum = 0
                    for (var index = 1; index <= num.length; index++)
                        newnum = newnum * oldcarry + hexnum.indexOf(num.charAt(index - 1))
                    if (neg)
                        newnum = -newnum
                    return (newnum)
                }

                function decto(num, newcarry) {
                    var neg = (num < 0)
                    if (newcarry == 10 || num == 0) return (num)
                    num = "" + Math.abs(num)
                    var newnum = ""
                    while (num != 0) {
                        newnum = hexnum.charAt(num % newcarry) + newnum
                        num = Math.floor(num / newcarry)
                    }
                    if (neg)
                        newnum = "-" + newnum
                    return (newnum)
                }

                //表达式解析

                function parse(string) {
                    if (string.match(/^(.*\d[\+\-\*\/\%\^\&\|x\<])?([+-]?[0-9a-f\.]+)([\+\-\*\/\%\^\&\|x\<])([+-]?[0-9a-f\.]+)$/))
                        return (RegExp.$1 + cypher(RegExp.$2, RegExp.$3, RegExp.$4))
                    else
                        return (string)
                }

                //数学运算和位运算

                function cypher(left, join, right) {
                    left = todec(left, carry)
                    right = todec(right, carry)
                    if (join == "+")
                        return (decto(parseFloat(left) + parseFloat(right), carry))
                    if (join == "-")
                        return (decto(left - right, carry))
                    if (join == "*")
                        return (decto(left * right, carry))
                    if (join == "/" && right != 0)
                        return (decto(left / right, carry))
                    if (join == "%")
                        return (decto(left % right, carry))
                    if (join == "&")
                        return (decto(left & right, carry))
                    if (join == "|")
                        return (decto(left | right, carry))
                    if (join == "^")
                        return (decto(Math.pow(left, right), carry))
                    if (join == "x")
                        return (decto(left ^ right, carry))
                    if (join == "<")
                        return (decto(left << right, carry))
                    alert("除数不能为零")
                    return (left)
                }

                //函数计算

                function funcalc(fun, num) {
                    with (Math) {
                        if (fun == "pi")
                            return (PI)
                        if (fun == "e")
                            return (E)

                        if (fun == "abs")
                            return (abs(num))
                        if (fun == "ceil")
                            return (ceil(num))
                        if (fun == "round")
                            return (round(num))

                        if (fun == "floor")
                            return (floor(num))
                        if (fun == "deci")
                            return (num - floor(num))


                        if (fun == "ln" && num > 0)
                            return (log(num))
                        if (fun == "exp")
                            return (exp(num))
                        if (fun == "log" && num > 0)
                            return (log(num) * LOG10E)
                        if (fun == "expdec")
                            return (pow(10, num))


                        if (fun == "cube")
                            return (num * num * num)
                        if (fun == "cubt")
                            return (pow(num, 1 / 3))
                        if (fun == "sqr")
                            return (num * num)
                        if (fun == "sqrt" && num >= 0)
                            return (sqrt(num))

                        if (fun == "!")
                            return (factorial(num))

                        if (fun == "recip" && num != 0)
                            return (1 / num)

                        if (fun == "dms")
                            return (dms(num))
                        if (fun == "deg")
                            return (deg(num))

                        if (fun == "~")
                            return (~num)

                        if (angle == "d") {
                            if (fun == "sin")
                                return (sin(degressToRadians(num)))
                            if (fun == "cos")
                                return (cos(degressToRadians(num)))
                            if (fun == "tan")
                                return (tan(degressToRadians(num)))

                            if (fun == "arcsin" && abs(num) <= 1)
                                return (radiansToDegress(asin(num)))
                            if (fun == "arccos" && abs(num) <= 1)
                                return (radiansToDegress(acos(num)))
                            if (fun == "arctan")
                                return (radiansToDegress(atan(num)))
                        }
                        else {
                            if (fun == "sin")
                                return (sin(num))
                            if (fun == "cos")
                                return (cos(num))
                            if (fun == "tan")
                                return (tan(num))

                            if (fun == "arcsin" && abs(num) <= 1)
                                return (asin(num))
                            if (fun == "arccos" && abs(num) <= 1)
                                return (acos(num))
                            if (fun == "arctan")
                                return (atan(num))
                        }

                        if (fun == "hypsin")
                            return ((exp(num) - exp(0 - num)) * 0.5)
                        if (fun == "hypcos")
                            return ((exp(num) + exp(-num)) * 0.5)
                        if (fun == "hyptan")
                            return ((exp(num) - exp(-num)) / (exp(num) + exp(-num)))

                        if (fun == "ahypsin" | fun == "hypcos" | fun == "hyptan") {
                            alert("对不起,公式还没有查到!")
                            return (num)
                        }

                        alert("超出函数定义范围")
                        return (num)
                    }
                }

                function factorial(n) {
                    n = Math.abs(parseInt(n))
                    var fac = 1
                    for (; n > 0; n -= 1)
                        fac *= n
                    return (fac)
                }

                function dms(n) {
                    var neg = (n < 0)
                    with (Math) {
                        n = abs(n)
                        var d = floor(n)
                        var m = floor(60 * (n - d))
                        var s = (n - d) * 60 - m
                    }
                    var dms = d + m / 100 + s * 0.006
                    if (neg)
                        dms = -dms
                    return (dms)
                }

                function deg(n) {
                    var neg = (n < 0)
                    with (Math) {
                        n = abs(n)
                        var d = floor(n)
                        var m = floor((n - d) * 100)
                        var s = (n - d) * 100 - m
                    }
                    var deg = d + m / 60 + s / 36
                    if (neg)
                        deg = -deg
                    return (deg)
                }

                function degressToRadians(degress) {
                    return (degress * Math.PI / 180)
                }

                function radiansToDegress(radians) {
                    return (radians * 180 / Math.PI)
                }

                //界面

//-->
    </script>
    <!--written by GoldHuman li hai-->
    <!--2000.8-->
    <meta name="GENERATOR" content="MSHTML 8.00.6001.18702">
</head>
<body>
    <div align="center">
        <form name="calc">
        <table border="1" width="500" height="250">
            <tbody>
                <tr>
                    <td height="50">
                        <table width="500">
                            <tbody>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <div align="center">
                                            <input value="0" readonly size="40" name="display">
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="500">
                            <tbody>
                                <tr>
                                    <td width="290">
                                        <input onclick="inputChangCarry(16)" type="radio" name="carry">
                                        十六进制
                                        <input onclick="inputChangCarry(10)" checked type="radio" name="carry">
                                        十进制
                                        <input onclick="inputChangCarry(8)" type="radio" name="carry">
                                        八进制
                                        <input onclick="inputChangCarry(2)" type="radio" name="carry">
                                        二进制
                                    </td>
                                    <td>
                                    </td>
                                    <td width="135">
                                        <input onclick="inputChangAngle('d')" value="d" checked type="radio" name="angle">
                                        角度制
                                        <input onclick="inputChangAngle('r')" value="r" type="radio" name="angle">
                                        弧度制
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table width="500">
                            <tbody>
                                <tr>
                                    <td width="170">
                                        <input onclick="inputshift()" type="checkbox" name="shiftf">上档功能
                                        <input onclick="inputshift()" type="checkbox" name="hypf">双曲函数
                                    </td>
                                    <td>
                                        <input style="background-color: lightgrey" readonly size="3" name="bracket">
                                        <input style="background-color: lightgrey" readonly size="3" name="memory">
                                        <input style="background-color: lightgrey" readonly size="3" name="operator">
                                    </td>
                                    <td width="183">
                                        <input style="color: red" onclick="backspace()" value=" 退格 " type="button">
                                        <input style="color: red" onclick="document.calc.display.value = 0 " value=" 清屏 "
                                            type="button">
                                        <input style="color: red" onclick="clearall()" value=" 全清" type="button">
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table class="buttons" width="500">
                            <tbody>
                                <tr>
                                    <td>
                                        <table>
                                            <tbody>
                                                <tr align="middle">
                                                    <td>
                                                        <input style="color: blue" onclick="inputfunction('pi','pi')" value=" PI " type="button"
                                                            name="pi">
                                                    </td>
                                                    <td>
                                                        <input style="color: blue" onclick="inputfunction('e','e')" value=" E  " type="button"
                                                            name="e">
                                                    </td>
                                                    <td>
                                                        <input style="color: #ff00ff" onclick="inputfunction('dms','deg')" value="d.ms" type="button"
                                                            name="bt">
                                                    </td>
                                                </tr>
                                                <tr align="middle">
                                                    <td>
                                                        <input style="color: #ff00ff" onclick="addbracket()" value=" (  " type="button">
                                                    </td>
                                                    <td>
                                                        <input style="color: #ff00ff" onclick="disbracket()" value=" )  " type="button">
                                                    </td>
                                                    <td>
                                                        <input style="color: #ff00ff" onclick="inputfunction('ln','exp')" value=" ln " type="button"
                                                            name="ln">
                                                    </td>
                                                </tr>
                                                <tr align="middle">
                                                    <td>
                                                        <input style="color: #ff00ff" onclick="inputtrig('sin','arcsin','hypsin','ahypsin')"
                                                            value="sin " type="button" name="sin">
                                                    </td>
                                                    <td>
                                                        <input style="color: #ff00ff" onclick="operation('^',7)" value="x^y " type="button">
                                                    </td>
                                                    <td>
                                                        <input style="color: #ff00ff" onclick="inputfunction('log','expdec')" value="log "
                                                            type="button" name="log">
                                                    </td>
                                                </tr>
                                                <tr align="middle">
                                                    <td>
                                                        <input style="color: #ff00ff" onclick="inputtrig('cos','arccos','hypcos','ahypcos')"
                                                            value="cos " type="button" name="cos">
                                                    </td>
                                                    <td>
                                                        <input style="color: #ff00ff" onclick="inputfunction('cube','cubt')" value="x^3 "
                                                            type="button" name="cube">
                                                    </td>
                                                    <td>
                                                        <input style="color: #ff00ff" onclick="inputfunction('!','!')" value=" n! " type="button">
                                                    </td>
                                                </tr>
                                                <tr align="middle">
                                                    <td>
                                                        <input style="color: #ff00ff" onclick="inputtrig('tan','arctan','hyptan','ahyptan')"
                                                            value="tan " type="button" name="tan">
                                                    </td>
                                                    <td>
                                                        <input style="color: #ff00ff" onclick="inputfunction('sqr','sqrt')" value="x^2 "
                                                            type="button" name="sqr">
                                                    </td>
                                                    <td>
                                                        <input style="color: #ff00ff" onclick="inputfunction('recip','recip')" value="1/x "
                                                            type="button">
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                    <td width="30">
                                    </td>
                                    <td>
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <input style="color: red" onclick="putmemory()" value=" 储存 " type="button">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <input style="color: red" onclick="getmemory()" value=" 取存 " type="button">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <input style="color: red" onclick="addmemory()" value=" 累存 " type="button">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <input style="color: red" onclick="multimemory()" value=" 积存 " type="button">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="33">
                                                        <input style="color: red" onclick="clearmemory()" value=" 清存 " type="button">
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                    <td width="30">
                                    </td>
                                    <td>
                                        <table>
                                            <tbody>
                                                <tr align="middle">
                                                    <td>
                                                        <input style="color: blue" onclick="inputkey('7')" value=" 7 " type="button" name="k7">
                                                    </td>
                                                    <td>
                                                        <input style="color: blue" onclick="inputkey('8')" value=" 8 " type="button" name="k8">
                                                    </td>
                                                    <td>
                                                        <input style="color: blue" onclick="inputkey('9')" value=" 9 " type="button" name="k9">
                                                    </td>
                                                    <td>
                                                        <input style="color: red" onclick="operation('/',6)" value=" / " type="button">
                                                    </td>
                                                    <td>
                                                        <input style="color: red" onclick="operation('%',6)" value="取余" type="button">
                                                    </td>
                                                    <td>
                                                        <input style="color: red" onclick="operation('&amp;',3)" value=" 与 " type="button">
                                                    </td>
                                                </tr>
                                                <tr align="middle">
                                                    <td>
                                                        <input style="color: blue" onclick="inputkey('4')" value=" 4 " type="button" name="k4">
                                                    </td>
                                                    <td>
                                                        <input style="color: blue" onclick="inputkey('5')" value=" 5 " type="button" name="k5">
                                                    </td>
                                                    <td>
                                                        <input style="color: blue" onclick="inputkey('6')" value=" 6 " type="button" name="k6">
                                                    </td>
                                                    <td>
                                                        <input style="color: red" onclick="operation('*',6)" value=" * " type="button">
                                                    </td>
                                                    <td>
                                                        <input style="color: red" onclick="inputfunction('floor','deci')" value="取整" type="button"
                                                            name="floor">
                                                    </td>
                                                    <td>
                                                        <input style="color: red" onclick="operation('|',1)" value=" 或 " type="button">
                                                    </td>
                                                </tr>
                                                <tr align="middle">
                                                    <td>
                                                        <input style="color: blue" onclick="inputkey('1')" value=" 1 " type="button">
                                                    </td>
                                                    <td>
                                                        <input style="color: blue" onclick="inputkey('2')" value=" 2 " type="button" name="k2">
                                                    </td>
                                                    <td>
                                                        <input style="color: blue" onclick="inputkey('3')" value=" 3 " type="button" name="k3">
                                                    </td>
                                                    <td>
                                                        <input style="color: red" onclick="operation('-',5)" value=" - " type="button">
                                                    </td>
                                                    <td>
                                                        <input style="color: red" onclick="operation('<',4)" value="左移" type="button">
                                                    </td>
                                                    <td>
                                                        <input style="color: red" onclick="inputfunction('~','~')" value=" 非 " type="button">
                                                    </td>
                                                </tr>
                                                <tr align="middle">
                                                    <td>
                                                        <input style="color: blue" onclick="inputkey('0')" value=" 0 " type="button">
                                                    </td>
                                                    <td>
                                                        <input style="color: blue" onclick="changeSign()" value="+/-" type="button">
                                                    </td>
                                                    <td>
                                                        <input style="color: blue" onclick="inputkey('.')" value=" . " type="button" name="kp">
                                                    </td>
                                                    <td>
                                                        <input style="color: red" onclick="operation('+',5)" value=" + " type="button">
                                                    </td>
                                                    <td>
                                                        <input style="color: red" onclick="result()" value=" ＝ " type="button">
                                                    </td>
                                                    <td>
                                                        <input style="color: red" onclick="operation('x',2)" value="异或" type="button">
                                                    </td>
                                                </tr>
                                                <tr align="middle">
                                                    <td>
                                                        <input style="color: blue" disabled onclick="inputkey('a')" value=" A " type="button"
                                                            name="ka">
                                                    </td>
                                                    <td>
                                                        <input style="color: blue" disabled onclick="inputkey('b')" value=" B " type="button"
                                                            name="kb">
                                                    </td>
                                                    <td>
                                                        <input style="color: blue" disabled onclick="inputkey('c')" value=" C " type="button"
                                                            name="kc">
                                                    </td>
                                                    <td>
                                                        <input style="color: blue" disabled onclick="inputkey('d')" value=" D " type="button"
                                                            name="kd">
                                                    </td>
                                                    <td>
                                                        <input style="color: blue" disabled onclick="inputkey('e')" value=" E　" type="button"
                                                            name="ke">
                                                    </td>
                                                    <td>
                                                        <input style="color: blue" disabled onclick="inputkey('f')" value=" F　" type="button"
                                                            name="kf">
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
        </form>
    </div>
</body>
</html>