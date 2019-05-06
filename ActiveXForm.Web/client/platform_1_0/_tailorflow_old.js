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

function getDaysOfMonth(mon)
{
	if(mon == 2)
		return (isLeapYear(new Date().getYear())) ? 29 : 28;
	return (isBigMonth(mon)) ? 31 : 30;
}

function getDaysOfYear(y)
{
	return isLeapYear(y) ? : 366 : 365;
}

function addDaysToDate(date,days)
{
	var day = (date.getDate() + days) % (getDaysOfMonth(date.getMonth() + 1));
	var dn = (date.getDate() + days) / (getDaysOfMonth(date.getMonth() + 1));
	var mon = (date.getMonth + 1 + n) % 12;
	var mn = (date.getMonth + 1 + n) / 12;
	var year = date.getFullYear() + mn;
	return new Date(year,mon - 1,day);
}

function subDates(date1,date2)
{
	var y1 = date1.getFullYear();
	var y2 = date2.getFullYear();
	var m1 = date1.getMonth() + 1;
	var m2 = date2.getMonth() + 1;
	var d1 = date1.getDate();
	var d2 = date2.getDate();
	if(y1 == y2)
	{
		if(m1 == m2)
		{
			return d2 - d1;
		}
		else
		{
			var n = (getDaysOfMonth(m1) - d1 + d2);
			for(var i = m1 + 1;i < m2;i ++)
				n += getDaysOfMonth(i);
			return n;
		}
	}
	else
	{
		var n = (subDates(date1,new Date(y1,12,31)) + subDates(new Date(y2,1,1),date2));
		for(var i = y1 + 1;i < y2;i ++)
			n += getDaysOfYear(i);
		return n;
	}
}


//
// Common select html-control operations.
//
function _se_createOptionFor(oList, sText, sValue)
{
	var oOption = document.createElement("OPTION");
	oList.options.add(oOption);
	oOption.innerText = sText;
	oOption.value = sValue;
	return oList.options.length - 1;
}

function _se_clearList(oList)
{
	for(var i = oList.options.length - 1;i >= 0;i --)
	{
		oList.options.remove(i);
	}
}

function _se_optionIndexByValue(oList, sValue)
{
	for(var i = oList.options.length - 1;i >= 0;i --)
	{
		if(oList.options(i).value.split(';')[0] == sValue)
		{
			return i;
		}
	}
	return -1;
}

//
// Operand work item,key-value.
//

function OWItem(sKey,val)
{
	this.key = sKey;
	this.value = val;

	this.toString = _se_owitemtostring;
	this.fromString = _se_owitemfromstring;
}

function _se_owitemtostring()
{
	return this.key + "=" + this.value;
}

function _se_getItemType(sKey)
{
	switch(sKey.toLower())
	{
		case "id":return "int";
		case "pfolderid":return "int";
		case "operandid":return "int";
		case "handlenodeid":return "int";
		case "actorder":return "int";
		case "timeoutactiontype":return "int";
		case "startdate":return "date";
		case "timeout":return "int";
		case "finisheddate":return "date";
		case "canreadflow":return "int";
		case "canwriteflow":return "int";
		case "canwritecontent":return "int";
		case "priorow":return "int";
		case "nextow":return "int";
		case "issync":return "int";
		case "backcenter":return "int";
		case "iscenter":return "int";
		case "response":return "int";
		case "carrier":return "int";
		case "status":return "int";
		case "templateid":return "int";
		case "empid":return "int";
		default:return "string";
	}
}

function _se_owitemfromstring(sv)
{
	var np = sv.indexOf('=');
	this.key = sv.substring(0,np);
	var type = _se_getItemType(this.key);
	if(type == "date")
	{
		this.key = new Date(parseInt(sv.substring(0,4),parseInt(sv.substring(5,7)),parseInt(sv.substring(8,10))));
	}
	else if(type == "int")
	{
		this.value = parseInt(sv.substring(np+1,sv.length));
	}
	else
	{
		this.value = sv.substring(np+1,sv.length);
	}
}

//
// Operand work object,contains one OWItem array;
//

function OperandWork()
{
	this.items = new Array(28);
	this.items[0]	= new OWItem("ID",					0);
	this.items[1]	= new OWItem("PFolderID",			0);
	this.items[2]	= new OWItem("OperandID",			0);
	this.items[3]	= new OWItem("HandleNodeID",		0);
	this.items[4]	= new OWItem("RequiredAction",		"");
	this.items[5]	= new OWItem("ActOrder",			0);
	this.items[6]	= new OWItem("ActionResult",		"");
	this.items[7]	= new OWItem("ResultDescription",	"");
	this.items[8]	= new OWItem("TimeoutActionType",	0);
	this.items[9]	= new OWItem("TimeoutMessage",		"");
	this.items[10]	= new OWItem("StartDate",			new Date(2001,1,1));
	this.items[11]	= new OWItem("Timeout",				1);
	this.items[12]	= new OWItem("FinishedDate",		new Date(2001,1,1));
	this.items[13]	= new OWItem("CanReadFlow",			1);
	this.items[14]	= new OWItem("CanWriteFlow",		0);
	this.items[15]	= new OWItem("CanWriteContent",		0);
	this.items[16]	= new OWItem("PriorOW",				0);
	this.items[17]	= new OWItem("NextOW",				0);
	this.items[18]	= new OWItem("IsSync",				0);
	this.items[19]	= new OWItem("BackCenter",			1);
	this.items[20]	= new OWItem("IsCenter",			0);
	this.items[21]	= new OWItem("Response",			1);
	this.items[22]	= new OWItem("Sender",				"");
	this.items[23]	= new OWItem("Carrier",				0);
	this.items[24]	= new OWItem("Status",				0);
	this.items[25]	= new OWItem("TemplateID",			"");
	this.items[26]	= new OWItem("EmpName",				"");
	this.items[27]	= new OWItem("EmpID",				0);

	this.toString = _se_owtostring;
	this.toOption = _se_owtooption;
	this.valueOf = _se_owvalueof;
	this.setValue = _se_owsetvalue;
	this.createFrom = _se_owcreatefrom;
	this.fromString = _se_owfromstring;
}

function _se_owfromstring(s)
{
	var sv = s.split(';');
	for(var i = 0;i < sv.length;i ++)
	{
		var item = new OWItem();
		item.fromString(sv[i]);
		this.setValue(item.key,item.value);
	}
}

function _se_owcreatefrom(ow)
{
	this.items[0]	= new OWItem("ID",					ow.items[0].value);
	this.items[1]	= new OWItem("PFolderID",			ow.items[1].value);
	this.items[2]	= new OWItem("OperandID",			ow.items[2].value);
	this.items[3]	= new OWItem("HandleNodeID",		ow.items[3].value);
	this.items[4]	= new OWItem("RequiredAction",		ow.items[4].value);
	this.items[5]	= new OWItem("ActOrder",			ow.items[5].value);
	this.items[6]	= new OWItem("ActionResult",		ow.items[6].value);
	this.items[7]	= new OWItem("ResultDescription",	ow.items[7].value);
	this.items[8]	= new OWItem("TimeoutActionType",	ow.items[8].value);
	this.items[9]	= new OWItem("TimeoutMessage",		ow.items[9].value);
	this.items[10]	= new OWItem("StartDate",			ow.items[10].value);
	this.items[11]	= new OWItem("Timeout",				ow.items[11].value);
	this.items[12]	= new OWItem("FinishedDate",		ow.items[12].value);
	this.items[13]	= new OWItem("CanReadFlow",			ow.items[13].value);
	this.items[14]	= new OWItem("CanWriteFlow",		ow.items[14].value);
	this.items[15]	= new OWItem("CanWriteContent",		ow.items[15].value);
	this.items[16]	= new OWItem("PriorOW",				ow.items[16].value);
	this.items[17]	= new OWItem("NextOW",				ow.items[17].value);
	this.items[18]	= new OWItem("IsSync",				ow.items[18].value);
	this.items[19]	= new OWItem("BackCenter",			ow.items[19].value);
	this.items[20]	= new OWItem("IsCenter",			ow.items[20].value);
	this.items[21]	= new OWItem("Response",			ow.items[21].value);
	this.items[22]	= new OWItem("Sender",				ow.items[22].value);
	this.items[23]	= new OWItem("Carrier",				ow.items[23].value);
	this.items[24]	= new OWItem("Status",				ow.items[24].value);
	this.items[25]	= new OWItem("TemplateName",		ow.items[25].value);
	this.items[26]	= new OWItem("EmpName",				ow.items[26].value);
	this.items[27]	= new OWItem("EmpID",				ow.items[27].value);
}

function _se_owtostring()
{
	var sRet = "";
	for(var i = 0;i < 29;i ++)
		sRet += this.items[i].toString() + ";";
	sRet += this.items[29].toString();
	return sRet;
}

function _se_owtooption(oList)
{
	var oOption = document.createElement("OPTION");
	oList.options.add(oOption);
	oOPtion.value = this.toString();
	oOption.innerText = this.items[6].value + ":" + this.items[28].value;
}

function _se_owvalueof(sKey)
{
	switch(sKey.toLower())
	{
		case "id":					return this.items[0].value;
		case "pfolderid":			return this.items[1].value;
		case "operandid":			return this.items[2].value;
		case "handlenodeid":		return this.items[3].value;
		case "requiredaction":		return this.items[4].value;
		case "actorder":			return this.items[5].value;
		case "actionresult":		return this.items[6].value;
		case "resultdescription":	return this.items[7].value;
		case "timeoutactiontype":	return this.items[8].value;
		case "timemessage":			return this.items[9].value;
		case "startdate":			return this.items[10].value;
		case "timeout":				return this.items[11].value;
		case "finisheddate":		return this.items[12].value;
		case "canreadflow":			return this.items[13].value;
		case "canwriteflow":		return this.items[14].value;
		case "canwritecontent":		return this.items[15].value;
		case "priorow":				return this.items[16].value;
		case "nextow":				return this.items[17].value;
		case "issync":				return this.items[18].value;
		case "backcenter":			return this.items[19].value;
		case "iscenter":			return this.items[20].value;
		case "response":			return this.items[21].value;
		case "sender":				return this.items[22].value;
		case "carrier":				return this.items[23].value;
		case "status":				return this.items[24].value;
		case "templateid":			return this.items[25].value;
		case "empname":				return this.items[26].value;
		case "empid":				return this.items[27].value;
		default:					return null;
	}
}

function _se_owsetvalue(sKey,val)
{
	switch(sKey.toLower())
	{
		case "id"						:this.items[0].value = val;break;
		case "pfolderid"				:this.items[1].value = val;break;
		case "operandid"				:this.items[2].value = val;break;
		case "handlenodeid"				:this.items[3].value = val;break;
		case "requiredaction"			:this.items[4].value = val;break;
		case "actorder"					:this.items[5].value = val;break;
		case "actionresult"				:this.items[6].value = val;break;
		case "resultdescription"		:this.items[7].value = val;break;
		case "timeoutactiontype"		:this.items[8].value = val;break;
		case "timemessage"				:this.items[9].value = val;break;
		case "startdate"				:this.items[10].value = val;break;
		case "timeout"					:this.items[11].value = val;break;
		case "finisheddate"				:this.items[12].value = val;break;
		case "canreadflow"				:this.items[13].value = val;break;
		case "canwriteflow"				:this.items[14].value = val;break;
		case "canwritecontent"			:this.items[15].value = val;break;
		case "priorow"					:this.items[16].value = val;break;
		case "nextow"					:this.items[17].value = val;break;
		case "issync"					:this.items[18].value = val;break;
		case "backcenter"				:this.items[19].value = val;break;
		case "iscenter"					:this.items[20].value = val;break;
		case "response"					:this.items[21].value = val;break;
		case "sender"					:this.items[22].value = val;break;
		case "carrier"					:this.items[23].value = val;break;
		case "status"					:this.items[24].value = val;break;
		case "templateid"				:this.items[25].value = val;break;
		case "empname"					:this.items[26].value = val;break;
		case "empid"					:this.items[27].value = val;break;
		default							:return false;
	}
	return true;
}

//
// Operand work array list,contains one OperandWork array.
//

function OWArrayList()
{
	this.ows = new Array(16);
	this.count = 0;
	
	this.add = _se_owaadd;
	this.clear = _se_owaclear;
	this.insertAt = _se_owainsertat;
	this.removeAt = _se_owaremoveat;
	this.moveForward = _se_owamoveforward;
	this.moveBackward = _se_owamovebackward;
	this.findByEmpName = _se_owafindbyempname;
	this.findByID = _se_owafindbyid;
	this.toString = _se_owatostring;
	this.toList = _se_owatolist;
	this.createFrom = _se_owacreatefrom;
	this.fromString = _se_owafromstring;
}

function _se_owafromstring(s)
{
	var sv = s.split(',');
	for(var i = 0;i < sv.length;i ++)
	{
		var ow = new OperandWork();
		ow.fromString(sv[i]);
		this.add(ow);
	}
}

function _se_owacreatefrom(owa)
{
	this.clear();
	this.ows = new Array(owa.count);
	this.count = owa.count;
	for(var i = 0;i < owa.count;i ++)
	{
		this.ows[i] = new OperandWork();
		this.ows[i].createFrom(owa[i]);
	}
}

function _se_owaadd(ow)
{
	if(ow == null)
		return -1;
	this.ows[this.count] = ow;
	this.ows[this.count].items[0].value = this.count;
	this.ows[this.count].items[6].value = this.count;
	this.count ++;
	return this.count;
}

function _se_owaclear()
{
	if(this.count == 0)
		return;
	for(var i = 0;i < this.count;i ++)
		this.ows[i] = null;
	this.count = 0;
}

function _se_owainsertat(ow,index)
{
	if(ow == null)
		return;
	if(index < 0)
		index = 0;
	else if(index > this.count)
		index = this.count;
	for(var i = index;i < this.count;i ++)
	{
		this.ows[i + 1] = this.ows[i];
		this.ows[i + 1].items[0].value = i + 1;
		this.ows[i + 1].items[6].value = i + 1;
	}
	this.ows[index] = ow;
	this.ows[index].items[0].value = index;
	this.ows[index].items[6].value = index;
	this.count ++;
}

function _se_owaremoveat(index)
{
	if(this.count == 0 || index < 0 || index >= this.count)
		return;
	this.ows[index] = null;
	for(var i = index;i < this.count - 1;i ++)
	{
		this.ows[i] = this.ows[i + 1];
		this.ows[i].items[0].value = i;
		this.ows[i].items[6].value = i;
	}
	this.count --;
}

function _se_owamoveforward(index)
{
	if(this.count < 2 || index <= 0)
		return;
	var tmp = this.ows[index];
	this.ows[index] = this.ows[index - 1];
	this.ows[index].items[0].value = index;
	this.ows[index].items[6].value = index;
	this.ows[index - 1] = tmp;
	this.ows[index - 1].items[0].value = index - 1;
	this.ows[index - 1].items[6].value = index - 1;
}

function _se_owamovebackward(index)
{
	if(this.count < 2 || index >= this.count)
		return;
	var tmp = this.ows[index];
	this.ows[index] = this.ows[index + 1];
	this.ows[index].items[0].value = index;
	this.ows[index].items[6].value = index;
	this.ows[index + 1] = tmp;
	this.ows[index + 1].items[0].value = index + 1;
	this.ows[index + 1].items[6].value = index + 1;
}

function _se_owafindbyempname(empname)
{
	if(empname == null || empname == "")
		return null;
	for(var i = 0;i < this.count;i ++)
	{
		if(this.ows[i].items[28].value == empname)
			return this.ows[i];
	}
	return null;
}

function _se_owafindbyid(id)
{
	if(id == null || id <= 0)
		return null;
	for(var i = 0;i < this.count;i ++)
	{
		if(this.ows[i].items[0].value == id)
			return this.ows[i];
	}
	return null;
}

function _se_owatostring()
{
	var sRet = "";
	for(var i = 0;i < this.count - 1;i ++)
		sRet += this.ows[i].toString() + ",";
	sRet += this.ows[this.count - 1].toString();
	return sRet;
}

function _se_owatolist(oList,bClear)
{
	if(bClear)
	{
		_se_clearList(oList);
	}
	for(var i = 0;i < this.count;i ++)
		this.ows[i].toOption(oList);
}

//
// Global var of OWArrayList.
//

var _g_owlist = null;
var _g_se_nLastIndex = -1;
var _g_se_bModify = false;

//
// Methods or event methods of the flow-node-select-employees-dialog.
//

function _se_saveSettings(index)
{
	if(_g_owlist.ows[index] == null)
		_g_owlist.ows[index] = new OperandWork();
	var ow = _g_owlist.ows[index];
	var theSel = document.all.lbSelectedEmps;
	var sOptionVal = theSel.options(nIndex).value;
	ow.items[0].value = index;
	ow.items[1].value = parseInt(sOptionVal.split(',')[2]);
	ow.items[2].value = parseInt(document.all.__operandid.value);
	ow.items[4].value = parseInt(document.all.__handlenodeid.value);
	ow.items[5].value = document.all.__action.value;
	ow.items[6].value = index;
	ow.items[7].value = "";
	ow.items[8].value = document.all.__action.value+"½á¹û";
	ow.items[9].value = document.all.ddlTO.selectedIndex;
	ow.items[10].value = document.all.txtMsg.value;
	ow.items[11].value = new Date();
	ow.items[12].value = parseInt(document.all.txtTime.value);
	ow.items[13].value = new Date();
	ow.items[14].value = document.all.rbCanSeeFlow.checked ? 1 : 0;
	ow.items[15].value = (ow.items[14].value == 0) ? 0 : (document.all.rbCanModFlow.checked ? 1 : 0);
	ow.items[16].value = document.all.rbCanModContent.checked ? 1 : 0;
	ow.items[17].value = index - 1;
	ow.items[18].value = index + 1;
	ow.items[19].value = document.all.cbSynchronous.checked ? 1 : 0;
	ow.items[20].value = document.all.cbBackCenter.checked ? 1 : 0;
	ow.items[21].value = document.all.cbCenter.checked ? 1 : 0;
	ow.items[22].value = document.all.cbNoResponse.checked ? 1 : 0;
	ow.items[23].value = document.all.txtSender.value;
	ow.items[24].value = document.all.rbEPaper.checkde ? 0 : 1;
	ow.items[25].value = 0;
	ow.items[26].value = 0;
	ow.items[27].value = theSel.options(nIndex).innerText;
	ow.items[28].value = parseInt(sOptionVal.split(',')[0]);
}

function _se_fillCtrls(index)
{
	var ow = _g_owlist.ows[index];
	document.all.rbCanModFlow.checked = (ow.items[15].value == 1);
	document.all.rbCannotModFlow.checked = !document.all.rbCanModFlow.checked;
	document.all.rbCanSeeFlow.checked = (ow.items[14].value == 1);
	document.all.rbCannotSeeFlow.checked = !document.all.rbCanSeeFlow.checked;
	document.all.rbCanModContent.checked = (ow.items[16].value == 1);
	document.all.rbCannotModContent.checked = !document.all.rbCanModContent.checked;
	document.all.txtTime.value = ow.items[12].value;
	document.all.ddlTO.selectedIndex = ow.items[9].value;
	document.all.txtMsg.disabled = (document.all.ddlTO.selectedIndex == 1);
	document.all.txtMsg.value = ow.items[9].value;
	document.all.cbCenter.checked = (ow.items[21].value == 1);
	document.all.cbBackCenter.checked = (ow.items[20].value == 1);
	document.all.cbNoResponse.checked = (ow.items[22].value == 1);
	document.all.txtSender.value = ow.items[23].value;
	document.all.rbEPaper.checked = (ow.items[24].value == 0);
}

/*----------------------Select Employees start--------------------------------*/

function _se_show(sNodeID,sOperandID,oHiddenOW,oUndoneList)
{
	var arg = new Object();
	arg.list = oUndoneList;
	arg.hidden = oHiddenOW;
	window.showModalDialog("SelEmps.aspx?NodeID="+sNodeID+"&OperandID="+sOperandID,arg,"dialogHeight:434px;dialogWidth:595px;help:no;status:no");
}

function _se_init()
{
	var arg = window.dialogArguments;
	_g_owlist = new OWArrayList();
	_g_owlist.fromString(arg.hidden.value);
	for(var i = 0;i < _g_owlist.count;i ++)
	{
		_se_createOptionFor(document.all.lbSelectedEmps,_g_owlist.ows[i].items[28],_g_owlist.ows[i].items[29] + "," + _g_owlist.ows[i].items[28] + "," + _g_owlist.ows[i].items[1] + "," + _g_owlist.ows[i].items[25]);
	}
	if(_g_owlist.count > 0)
	{
		document.all.lbSelectedEmps.selectedIndex = 0;
		_se_fillCtrls(0);
	}
}

function _se_btnSel_Click()
{
	var theEmps = document.all.lbEmployees;
	if(theEmps.options.length == 0 || theEmps.selectedIndex < 0)
		return;
	_se_saveSettings(_se_createOptionFor(document.all.lbSelectedEmps, theEmps.options(theEmps.selectedIndex).innerText, theEmps.options(theEmps.selectedIndex).value));
	var i = theEmps.selectedIndex;
	theEmps.options.remove(i);
	if(theEmps.options.length == 0)
		return;
	if(i > 0)
		theEmps.selectedIndex = i - 1;
	else
		theEmps.selectedIndex = 0;
}

function _se_cbSynchronous_Click()
{
	var the = document.all.cbSynchronous;
	if(the.checked)
	{
		document.all.btnUp.disabled = true;
		document.all.btnDn.disabled = true;
	}
	else
	{
		document.all.btnUp.disabled = true;
		document.all.btnDn.disabled = true;
	}
}

function _se_btnDel_Click()
{
	var theSelectedEmps = document.all.lbSelectedEmps;
	if(theSelectedEmps.options.length == 0 || theSelectedEmps.selectedIndex < 0)
		return;
	var __sv = theSelectedEmps.options(theSelectedEmps.selectedIndex).value;
	if(__sv.indexOf("status=0"))
	{
		_se_createOptionFor(document.all.lbEmployees, theSelectedEmps.options(theSelectedEmps.selectedIndex).innerText, theSelectedEmps.options(theSelectedEmps.selectedIndex).value);
		var i = theSelectedEmps.selectedIndex;
		_g_owlist.removeAt(i);
		theSelectedEmps.options.remove(i);
		if(theSelectedEmps.options.length == 0)
		{
			_g_se_nLastIndex = -1;
			return;
		}
		if(i > 0)
			theSelectedEmps.selectedIndex = i - 1;
		else
			theSelectedEmps.selectedIndex = 0;
	}
}

function _se_btnSelAll_Click()
{
	var theEmps = document.all.lbEmployees;
	var theSelectedEmps = document.all.lbSelectedEmps;
	for(var i = 0;i < theEmps.options.length;i ++)
	{
		_se_saveSettings(_se_createOptionFor(theSelectedEmps, theEmps.options(i).innerText, theEmps.options(i).value));
	}
	_se_clearList(theEmps);

}

function _se_btnDelAll_Click()
{
	var theEmps = document.all.lbEmployees;
	var theSelectedEmps = document.all.lbSelectedEmps;
	for(var i = 0;i < theSelectedEmps.options.length;i ++)
	{
		var __sv = theSelectedEmps.options(theSelectedEmps.selectedIndex).value;
		if(__sv.indexOf("status=0"))
			_se_createOptionFor(theEmps, theSelectedEmps.options(i).innerText, theSelectedEmps.options(i).value);
	}
	_g_owlist.clear();
	_se_clearList(theSelectedEmps);
	_g_se_nLastIndex = -1;
}

function _se_btnDn_Click()
{
	var theSelectedEmps = document.all.lbSelectedEmps;
	if(theSelectedEmps.options.length < 2)
		return;
	var i = theSelectedEmps.selectedIndex;
	if(i < 1)
	{
		return;
	}
	else
	{
		theSelectedEmps.children(i).swapNode(theSelectedEmps.children(i - 1));
		_g_owlist.moveBackward(i);
	}
}

function _se_btnUp_Click()
{
	var theSelectedEmps = document.all.lbSelectedEmps;
	if(theSelectedEmps.options.length < 2)
		return;
	var i = theSelectedEmps.selectedIndex;
	if(i == theSelectedEmps.options.length - 1)
		return;
	else
	{
		theSelectedEmps.children(i).swapNode(theSelectedEmps.children(i + 1));
		_g_owlist.moveForward(i);
	}
}

function _se_btnOK_Click()
{
	_se_saveSettings(document.all.lbSelectedEmps.selectedIndex);
	var arg = window.dialogArguments;
	arg.hidden.value = _g_owlist.toString();
	var theSelectedEmps = document.all.lbSelectedEmps;
	_se_clearList(arg.list);
	for(var i = 0;i < theSelectedEmps.options.length;i ++)
	{
		_se_createOptionFor(arg.list,theSelectedEmps.options(i).innerText,theSelectedEmps.options(i).value);
	}
	return true;
}

function _se_btnCancel_Click()
{
	window.close();
}

function _se_lbDepts_Click()
{
	var theDepts = document.all.lbDepts;
	var theEmps = document.all.lbEmployees;
	var theSelectedEmps = document.all.lbSelectedEmps;
	_se_clearList(theEmps);
	for(var i = 0;i < theDepts.options.length;i ++)
	{
		if(theDepts.options(i).selected)
		{
			var vVal = theDepts.options(i).value.split(';');
			for(var j = 0;j < vVal.length;j ++)
			{
				var vv = vVal[j].split(',');
				if(_se_optionIndexByValue(theSelectedEmps, vv[0]) < 0)
					_se_createOptionFor(theEmps,vv[1], vv[0] + ";" + vv[2]);
			}
		}
	}
}

function _se_ddlTimeout_Click()
{
	var theTO = document.all.ddlTO;
	if(theTO.selectedIndex == 1)
	{
		document.all.txtMsg.disabled = true;
	}
	else
	{
		document.all.txtMsg.disabled = false;
	}
}

function _se_lbSelectedEmps_Click()
{
	var lbSel = document.all.lbSelectedEmps;
	if(lbSel.options.length == 0 || lbSel.selectedIndex < 0)
	{
		_g_se_nLastIndex = -1;
		return;
	}
	if(_g_se_nLastIndex == lbSel.selectedIndex)
		return;
	if(_g_se_nLastIndex >= 0)
		_se_saveSettings(_g_se_nLastIndex);
	_g_se_nLastIndex = lbSel.selectedIndex;
	_se_fillCtrls(_g_se_nLastIndex);
}

/*----------------------Select Employees end--------------------------------*/

