function isDigit(ch)
{
	return (ch >= '0' && ch <= '9');
}

function isWhiteSpace(ch)
{
	return (ch == ' ' || ch == '\t' || ch == '\n');
}

function isUpper(ch)
{
	return (ch >= 'A' && ch <= 'Z');
}

function isLower(ch)
{
	return (ch >= 'a' && ch <= 'z');
}

function isNotLower(ch)
{
	return !(isLower(ch));
}

function isNotUpper(ch)
{
	return !(isUpper(ch));
}

function isNotDigit(ch)
{
	return !(isDigit(ch));
}

function isLetter(ch)
{
	return (isUpper(ch) || isLower(ch));
}

function isLetterOrDigit(ch)
{
	return (isLetter(ch) || isDigit(ch));
}

function isValidIDChar(ch)
{
	return ((ch == '_')||(isLetterOrDigit(ch)));
}

function isValidID(sID)
{
	if(sID == null || sID == "")
		return false;
	if(sID.charAt(0) != '_' && !isLetter(sID.charAt(0)))
		return false;
	for(var i = 1;i < sID.length;i ++)
	{
		if(!isValidIDChar(sID.charAt(i)))
			return false;
	}
	return true;
}

function isNumber(sNum)
{
	if(sNum == null || sNum == "")
		return false;
	for(var i = 0;i < sNum.length;i ++)
	{
		if(!isDigit(sNum.charAt(i)))
			return false;
	}
	return true;
}

function strTrim(sSrc)
{
	if(sSrc == null || sSrc.length == 0)
		return "";
	var sNew = "";
	for(var i = 0;i < sSrc.length; i ++)
	{
		if(!isWhiteSpace(sSrc.charAt(i)))
		{
			sNew += sSrc.charAt(i);
		}
	}
	return sNew;
}

function strTrimStart(sSrc)
{
	if(sSrc == null || sSrc.length == 0)
		return "";
	var i;
	for(i = 0;i < sSrc.length; i ++)
	{
		if(!isWhiteSpace(sSrc.charAt(i)))
			break;
	}
	return sSrc.substring(i,sSrc.length);
}

function strTrimEnd(sSrc)
{
	if(sSrc == null || sSrc.length == 0)
		return "";
	var i;
	for(i = sSrc.length - 1;i >= 0; i --)
	{
		if(!isWhiteSpace(sSrc.charAt(i)))
			break;
	}
	return sSrc.substring(0,i + 1);
}

function strTrimSE(sSrc)
{
	return strTrimEnd(strTrimStart(sSrc));
}

function trimSEFor(oCtrl)
{
	oCtrl.value = strTrimSE(oCtrl.value);
}

function strIsExists(sKey,sOr)
{
	var sv = sOr.split(';');
	for(var i = 0;i < sv.length;i ++)
	{
		if(sv[i] != "" && sKey == sv[i])
			return true;
	}
	return false;
}

function isFileExists(sFilePathName)
{
	try
	{
		return new ActiveXObject("Scripting.FileSystemObject").FileExists(sFilePathName);
	}
	catch(e)
	{
		alert("您的IE的安全设置不支持创建ActiveX控件。");
		return false;
	}
}

function createOptionFor(oList, sText, sValue)
{
	var oOption = document.createElement("OPTION");
	oList.options.add(oOption);
	oOption.innerText = sText;
	oOption.value = sValue;
}

function moveOptions(oFromList, oToList, btnAdd, btnDel)
{
	for(var i = oFromList.options.length - 1;i >= 0;i --)
	{
		if(oFromList.options(i).selected)
		{
			createOptionFor(oToList, oFromList.options(i).innerText, oFromList.options(i).value);
			oFromList.options.remove(i);
		}
	}
	if(btnAdd != null && oFromList.options.length == 0)
	{
		btnAdd.disabled = true;
	}
	if(btnDel != null && oToList.options.length > 0)
	{
		btnDel.disabled = false;
	}
	return (oToList.options.length > 0);
}

function clearList(oList)
{
	for(var i = oList.options.length - 1;i >= 0;i --)
	{
		oList.options.remove(i);
	}
}

function selectedCount(oList)
{
	var nCount = 0;
	for(var i = oList.options.length - 1;i >= 0;i --)
	{
		if(oList.options(i).selected)
			nCount ++;
	}
	return nCount;
}

function isLeapYear(nYear)
{
	return (nYear % 400 == 0 || ((nYear % 4 == 0) && (nYear % 100 != 0)));
}

function isBigMonth(nMonth)
{
	return (nMonth == 1 ||
		    nMonth == 3 ||
		    nMonth == 5 ||
		    nMonth == 7 ||
		    nMonth == 8 ||
		    nMonth == 10 ||
		    nMonth == 12);
}

function loadMonthDaies(nYear, nMonth, oList)
{
	clearList(oList);
	var nLen = (nMonth == 2) ? ((isLeapYear(nYear)) ? (29) : (28)) : ((isBigMonth(nMonth)) ? (31) : (30));
	for(var i = 0;i < nLen;i ++)
	{
		createOptionFor(oList, i, i);
	}
}

function isZIP(sZIP)
{
	if(sZIP.length != 6) return false;
	for(var i = 0;i < 6;i ++)
	{
		if(!isDigit(sZIP.charAt(i))) return false;
	}
	return true;
}

function isTel(sTel)
{
	return true;
}

function isFax(sFax)
{
	return true;
}

function isBeeper(sBP)
{
	return true;
}

function isEmail(sEmail)
{
	return true;
}

function parseOptionValue(srcName, vv, oDesList)
{
	if(vv.length == 1)
		return;
	for(var i = 1;i < vv.length - 1;i ++)
	{
		createOptionFor(oDesList, vv[i + 1] + "@" + srcName, vv[i]);
	}
}

function findByTextAndValue(oList, sText, sVal)
{
	for(var i = oList.options.length - 1;i >= 0;i --)
	{
		if(oList.options(i).innerText == sText && oList.options(i).value == sVal)
		{
			return i;
		}
	}
	return -1;
}

function findByText(oList, sText)
{
	for(var i = oList.options.length - 1;i >= 0;i --)
	{
		if(oList.options(i).innerText == sText)
		{
			return i;
		}
	}
	return -1;
}

function findByValue(oList, sValue)
{
	for(var i = oList.options.length - 1;i >= 0;i --)
	{
		if(oList.options(i).value == sValue)
		{
			return i;
		}
	}
	return -1;
}

function parseSrcList(oSrcList, oDesList, oDesList0, bClearOld)
{
	if(bClearOld)
		clearList(oDesList);
	for(var i = 0;i < oSrcList.options.length;i ++)
	{
		parseOptionValue(oSrcList.options(i).innerText, oSrcList.options(i).value.split(';'), oDesList);
	}
	for(var j = oDesList0.options.length - 1;j >= 0;j --)
	{
		if(findByTextAndValue(oDesList, oDesList0.options(i).innerText, oDesList0.options(i).value) < 0)
			oDesList0.options.remove(j);
	}
}

/*----------------------Select System code start--------------------------------*/
function selSysCode(sCatalog,oTextBox)
{
	var sRet = window.showModalDialog("/OA.NET/SystemCodes/SystemCode.aspx?CatalogID="+sCatalog,null,"dialogHeight:280px;dialogWidth:387px;help:no;status:no");
	if(sRet == "")
		return;
	oTextBox.value = sRet;
}

/*----------------------Select System code end--------------------------------*/

function toggleContainerItem(send, recv, caption)
{
	if(document.all[recv].style.display == 'none')
	{
		document.all[recv].style.display = '';
		document.all[send].innerHTML = '隐藏<font face=\"marlett\" size=\"2\">t</font>';
		document.all[send].title = '隐藏“' + caption + '”下的所有元素。';
	}
	else
	{
		document.all[recv].style.display = 'none';
		document.all[sender].innerHTML = '显示<font face=\"marlett\" size=\"2\">u</font>';
		document.all[sender].title = '显示“' + caption + '”下的所有元素。';
	}
}

