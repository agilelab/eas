function _se_dateToString(oDate)
{
	return oDate.getFullYear() + "-" + (oDate.getMonth() + 1) + "-" + oDate.getDate() + " " + oDate.getHours() + ":" + oDate.getMinutes() + ":" + oDate.getSeconds();
}

function OWItem(sKey,sVal)
{
	this.key = sKey;
	this.value = sVal;

	this.toString = _se_owitemtostring;
	this.fromString = _se_owitemfromstring;
}

function _se_owitemtostring()
{
	return this.key + "=" + this.value;
}

function _se_owitemfromstring(s)
{
	if(s == "")
		return;
	var np = s.indexOf('=');
	if(np <= 0)
		return;
	this.key = s.substring(0,np);
	this.value = s.substring(np+1,s.length);
}

function OperandWork()
{
	this.items = new Array(21);
	this.items[0] = new OWItem("ID","0");
	this.items[1] = new OWItem("PFolderID","1");
	this.items[2] = new OWItem("EmpID","0");
	this.items[3] = new OWItem("OperandID","0");
	this.items[4] = new OWItem("HandleNodeID","0");
	this.items[5] = new OWItem("Description","");
	this.items[6] = new OWItem("ActionResult","");
	this.items[7] = new OWItem("ResultDescription","");
	this.items[8] = new OWItem("TimeoutActionType","0");
	this.items[9] = new OWItem("TimeoutMessage","处理超时");
	this.items[10] = new OWItem("StartDate",_se_dateToString(new Date()));
	this.items[11] = new OWItem("Timeout","1");
	this.items[12] = new OWItem("FinishedDate",this.items[10].value);
	this.items[13] = new OWItem("CanReadFlow","1");
	this.items[14] = new OWItem("CanWriteFlow","0");
	this.items[15] = new OWItem("CanWriteContent","0");
	this.items[16] = new OWItem("BackCenter","1");
	this.items[17] = new OWItem("IsCenter","0");
	this.items[18] = new OWItem("Sender","未知发送人");
	this.items[19] = new OWItem("Status","0");
	this.items[20] = new OWItem("ActOrder","0");

	this.isValid = true;

	this.toString = _se_owtostring;
	this.fromString = _se_owfromstring;
	this.valueOf = _se_owvalueof;
	this.setValue = _se_owsetvalue;
	this.toControls = _se_owtoctrls;
	this.fromControls = _se_owfromctrls;
	this.print = _se_owprint;
}

function _se_owprint()
{
}

function _se_owtostring()
{
	if(!this.isValid)
		return "";
	var sRet = "";
	this.items[20].value = ""+(parseInt(this.items[20].value)+(parseInt(document.all.__index.value)*1000000));
	for(var i = 0;i < 20;i ++)
		sRet += this.items[i].toString() + ";";
	sRet += this.items[20].toString();
	return sRet;
}

function _se_owfromstring(s)
{
	this.isValid = false;
	if(s == "")
		return;
	var v = s.split(';');
	for(var i = 0;i < 21;i ++)
	{
		this.items[i].fromString(v[i]);
	}
	this.isValid = true;
	this.items[20].value = ""+(parseInt(this.items[20].value)-(parseInt(document.all.__index.value)*1000000));
}

function _se_owvalueof(sKey)
{
	if(!this.isValid)
		return null;
	switch(sKey.toLowerCase())
	{
		case "id":					return this.items[0].value;
		case "pfolderid":			return this.items[1].value;
		case "empid":				return this.items[2].value;
		case "operandid":			return this.items[3].value;
		case "handlenodeid":		return this.items[4].value;
		case "requiredaction":		return this.items[5].value;
		case "actionresult":		return this.items[6].value;
		case "resultdescription":	return this.items[7].value;
		case "timeoutactiontype":	return this.items[8].value;
		case "timeoutmessage":		return this.items[9].value;
		case "startdate":			return this.items[10].value;
		case "timeout":				return this.items[11].value;
		case "finisheddate":		return this.items[12].value;
		case "canreadflow":			return this.items[13].value;
		case "canwriteflow":		return this.items[14].value;
		case "canwritecontent":		return this.items[15].value;
		case "backcenter":			return this.items[16].value;
		case "iscenter":			return this.items[17].value;
		case "sender":				return this.items[18].value;
		case "status":				return this.items[19].value;
		case "actorder":			return this.items[20].value;
		default:					return null;
	}
}

function _se_owsetvalue(sKey,sVal)
{
	if(!this.isValid)
		return;
	switch(sKey.toLowerCase())
	{
		case "id":					this.items[0].value = sVal;break;
		case "pfolderid":			this.items[1].value = sVal;break;
		case "empid":				this.items[2].value = sVal;break;
		case "operandid":			this.items[3].value = sVal;break;
		case "handlenodeid":		this.items[4].value = sVal;break;
		case "requiredaction":		this.items[5].value = sVal;break;
		case "actionresult":		this.items[6].value = sVal;break;
		case "resultdescription":	this.items[7].value = sVal;break;
		case "timeoutactiontype":	this.items[8].value = sVal;break;
		case "timeoutmessage":		this.items[9].value = sVal;break;
		case "startdate":			this.items[10].value = sVal;break;
		case "timeout":				this.items[11].value = sVal;break;
		case "finisheddate":		this.items[12].value = sVal;break;
		case "canreadflow":			this.items[13].value = sVal;break;
		case "canwriteflow":		this.items[14].value = sVal;break;
		case "canwritecontent":		this.items[15].value = sVal;break;
		case "backcenter":			this.items[16].value = sVal;break;
		case "iscenter":			this.items[17].value = sVal;break;
		case "sender":				this.items[18].value = sVal;break;
		case "status":				this.items[19].value = sVal;break;
		case "actorder":			this.items[20].value = sVal;break;
	}
}

function _se_owtoctrls()
{
	document.all.txtActOrder.value = this.valueOf("actorder");
	document.all.ddlTO.selectedIndex = parseInt(this.valueOf("timeoutactiontype"));
	document.all.txtMsg.value = this.valueOf("timeoutmessage");
	document.all.txtTime.value = this.valueOf("timeout");
	document.all.rbCanSeeFlow.checked = this.valueOf("canreadflow") == "1";
	document.all.rbCannotSeeFlow.checked = !document.all.rbCanSeeFlow.checked;
	document.all.rbCanModFlow.checked = this.valueOf("canwriteflow") == "1";
	document.all.rbCannotModFlow.checked = !document.all.rbCanModFlow.checked;
	document.all.rbCanModContent.checked = this.valueOf("canwritecontent") == "1";
	document.all.rbCannotModContent.checked = !document.all.rbCanModContent.checked;
	document.all.cbBackCenter.checked = this.valueOf("backcenter") == "1";
	document.all.txtSender.value = this.valueOf("sender");
}

function _se_owfromctrls()
{
	var tmp = parseInt(document.all.txtActOrder.value);
	if(tmp < 0)
		document.all.txtActOrder.value = "0";
	this.setValue("actorder",document.all.txtActOrder.value);
	this.setValue("timeoutactiontype",""+document.all.ddlTO.selectedIndex);
	this.setValue("timeoutmessage",document.all.txtMsg.value == "" ? "处理超时" : document.all.txtMsg.value);
	this.setValue("timeout",document.all.txtTime.value == "" ? "1" : document.all.txtTime.value);
	this.setValue("canreadflow",document.all.rbCanSeeFlow.checked ? "1" : "0");
	this.setValue("canwriteflow",document.all.rbCanModFlow.checked ? "1" : "0");
	this.setValue("canwritecontent",document.all.rbCanModContent.checked ? "1" : "0");
	this.setValue("backcenter",document.all.cbBackCenter.checked ? "1" : "0");
	this.setValue("sender",document.all.txtSender.value == "" ? "未知发送人" : document.all.txtSender.value);
}

function OWArrayList()
{
	this.ows = new Array();
	this.count = 0;

	this.add = _se_owaadd;
	this.remove = _se_owaremove;
	this.clear = _se_owaclear;
	this.moveForward = _se_owamovef;
	this.moveBackward = _se_owamoveb;
	this.toString = _se_owatostring;
	this.fromString = _se_owafromstring;
}

function _se_owaadd(ow)
{
	this.ows[this.count] = ow;
	this.count ++;
}

function _se_owaremove(index)
{
	if(this.count == 0 || index < 0 || index >= this.count)
		return;
	for(var i = index;i < this.count - 1;i ++)
	{
		this.ows[i] = this.ows[i + 1];
	}
	this.ows[this.count - 1] = null;
	this.count --;
}

function _se_owaclear()
{
	if(this.count == 0)
		return;
	for(var i = 0;i < this.count; i ++)
		this.ows[i] = null;
	this.ows.length = 0;
	this.count = 0;
}

function _se_owamovef(index)
{
	if(this.count < 2 || index < 1 || index >= this.count)
		return;
	var po = this.ows[index - 1].valueOf("actorder");
	var no = this.ows[index].valueOf("actorder");
	this.ows[index].setValue("actorder",po);
	this.ows[index - 1].setValue("actorder",no);
	var temp = this.ows[index];
	this.ows[index] = this.ows[index - 1];
	this.ows[index - 1] = temp;
}

function _se_owamoveb(index)
{
	if(this.count < 2 || index < 0 || index >= this.count - 1)
		return;
	var po = this.ows[index].valueOf("actorder");
	var no = this.ows[index + 1].valueOf("actorder");
	this.ows[index].setValue("actorder",no);
	this.ows[index + 1].setValue("actorder",po);
	var temp = this.ows[index];
	this.ows[index] = this.ows[index + 1];
	this.ows[index + 1] = this.ows[index];
}

function _se_owatostring()
{
	var sRet = "";
	for(var i = 0;i < this.count;i ++)
	{
		sRet += this.ows[i].toString() + ",";
	}
	if(sRet != "")
		sRet = sRet.substring(0,sRet.length - 1);
	return sRet;
}

function _se_owafromstring(s)
{
	if(s == "")
		return;
	var v = s.split(',');
	for(var i = 0;i < v.length;i ++)
	{
		var ow = new OperandWork();
		ow.fromString(v[i]);
		this.add(ow);
	}
}

var _g_nLastSelectedIndex = -1;
var _g_owa = null;

function _se_isExists(sValue)
{
	if(sValue == "-1")
		return true;
	var sels = document.all.lbSelectedEmps;
	for(var i = 0;i < sels.options.length;i ++)
	{
		if(sels.options(i).value == sValue)
			return true;
	}
	return false;
}

function _se_load()
{
	_g_owa = new OWArrayList();
	_g_owa.fromString(document.all.__values.value);
}

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
		oList.options.remove(i);
}

function _se_btnSel_Click(sOperandID,sNodeID)
{
	var all = document.all.lbEmployees;
	var sels = document.all.lbSelectedEmps;
	if(all.options.length == 0 || all.selectedIndex < 0)
		return;
	if(_se_isExists(all.options(all.selectedIndex).value))
		return;
	sels.selectedIndex = _se_createOptionFor(sels,all.options(all.selectedIndex).innerText.substring(3,all.options(all.selectedIndex).innerText.length),all.options(all.selectedIndex).value);
	_g_nLastSelectedIndex = sels.selectedIndex;
	var ow = new OperandWork();
	ow.setValue("actorder",_g_nLastSelectedIndex);
	ow.setValue("empid",all.options(all.selectedIndex).value);
	ow.setValue("operandid",sOperandID);
	ow.setValue("handlenodeid",sNodeID);
	_g_owa.add(ow);
	ow.toControls();
	//all.options.remove(all.selectedIndex);
}

function _se_btnSelAll_Click(sOperandID,sNodeID)
{
	var all = document.all.lbEmployees;
	var sels = document.all.lbSelectedEmps;
	if(all.options.length == 0)
		return;
	for(var i = 0;i < all.options.length;i ++)
	{
		if(_se_isExists(all.options(i).value))
			continue;
		_se_createOptionFor(sels,all.options(i).innerText.substring(3,all.options(i).innerText.length),all.options(i).value);
		var ow = new OperandWork();
		ow.setValue("actorder",i);
		ow.setValue("empid",all.options(i).value);
		ow.setValue("operandid",sOperandID);
		ow.setValue("handlenodeid",sNodeID);
		_g_owa.add(ow);
	}
	//_se_clearList(all);
	sels.selectedIndex = 0;
	_g_nLastSelectedIndex = 0;
	_g_owa.ows[0].toControls();
}

function _se_btnDel_Click()
{
	var all = document.all.lbEmployees;
	var sels = document.all.lbSelectedEmps;
	if(sels.options.length == 0 || sels.selectedIndex < 0)
		return;
	//all.selectedIndex = _se_createOptionFor(all,sels.options(sels.selectedIndex).innerText,sels.options(sels.selectedIndex).value);
	var ns = sels.selectedIndex;
	_g_owa.remove(ns);
	sels.options.remove(ns);
	if(sels.options.length > 0)
	{
		sels.selectedIndex = (ns == 0) ? 0 : ns - 1;
		_g_nLastSelectedIndex = sels.selectedIndex;
		_g_owa.ows[_g_nLastSelectedIndex].toControls();
	}
	else
	{
		_g_nLastSelectedIndex = -1;
	}
}

function _se_btnDelAll_Click()
{
	var all = document.all.lbEmployees;
	var sels = document.all.lbSelectedEmps;
	if(sels.options.length == 0)
		return;
	//for(var i = 0;i < sels.options.length;i ++)
	//{
		//_se_createOptionFor(all,sels.options(i).innerText,sels.options(i).value);
	//}
	_se_clearList(sels);
	_g_owa.clear();
	_g_nLastSelectedIndex = -1;
}

function _se_lbSelectedEmps_Click()
{
	var sels = document.all.lbSelectedEmps;
	if(sels.options.length == 0 || sels.selectedIndex < 0)
		return;
	if(_g_nLastSelectedIndex >= 0)
		_g_owa.ows[_g_nLastSelectedIndex].fromControls();
	_g_nLastSelectedIndex = sels.selectedIndex;
	_g_owa.ows[_g_nLastSelectedIndex].toControls();
}

function _se_btnDn_Click()
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
		_g_nLastSelectedIndex = theSelectedEmps.selectedIndex;
		_g_owa.moveBackward(i);
	}
}

function _se_btnUp_Click()
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
		_g_nLastSelectedIndex = theSelectedEmps.selectedIndex;
		_g_owa.moveForward(i);
	}
}

function _se_btnOK_Click()
{
	var sels = document.all.lbSelectedEmps;
	if(sels.options.length == 0)
	{
		if(!confirm("您没有选择任何人员，这将清除在该节点上的原设定人员！\n是否确定？"))
			return false;
	}
	
	if(sels.selectedIndex >= 0)
	{
		_g_nLastSelectedIndex = sels.selectedIndex;
		_g_owa.ows[_g_nLastSelectedIndex].fromControls();
	}
	document.all.__values.value = _g_owa.toString();
	return true;
}

function _se_btnCancel_Click()
{
	history.back(-1);
}