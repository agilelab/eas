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
	this.items = new Array(24);

	this.items[0] = new OWItem("ID","0");
	this.items[1] = new OWItem("PFolderID","0");
	this.items[2] = new OWItem("EmpID","0");
	this.items[3] = new OWItem("OperandID","0");
	this.items[4] = new OWItem("HandleNodeID","0");
	this.items[5] = new OWItem("RequiredAction","");
	this.items[6] = new OWItem("ActOrder","0");
	this.items[7] = new OWItem("ActionResult","");
	this.items[8] = new OWItem("ResultDescription","");
	this.items[9] = new OWItem("TimeoutActionType","0");
	this.items[10] = new OWItem("TimeoutMessage","处理超时");
	this.items[11] = new OWItem("StartDate","2001-01-01");
	this.items[12] = new OWItem("Timeout","1");
	this.items[13] = new OWItem("FinishedDate","2001-01-02");
	this.items[14] = new OWItem("CanReadFlow","1");
	this.items[15] = new OWItem("CanWriteFlow","0");
	this.items[16] = new OWItem("CanWriteContent","0");
	this.items[17] = new OWItem("PriorOW","0");
	this.items[18] = new OWItem("NextOW","0");
	this.items[19] = new OWItem("BackCenter","1");
	this.items[20] = new OWItem("IsCenter","0");
	this.items[21] = new OWItem("Response","1");
	this.items[22] = new OWItem("Sender","未知");
	this.items[23] = new OWItem("Status","0");

	this.toString = _se_owtostring;
	this.fromString = _se_owfromstring;
	this.valueOf = _se_owvalueof;
	this.setValue = _se_owsetvalue;
	this.toControls = _se_owtoctrls;
	this.fromControls = _se_owfromctrls;
}

function _se_owtostring()
{
	var sRet = "";
	for(var i = 0;i < 23;i ++)
		sRet += this.items[i].toString() + ";";
	sRet += this.items[23].toString();
	return sRet;
}

function _se_owfromstring(s)
{
	if(s == "")
		return;
	var v = s.split(';');
	for(var i = 0;i < 24;i ++)
		this.items[i].fromString(v[i]);
}

function _se_owvalueof(sKey)
{
	switch(sKey.toLower())
	{
		case "id":					return this.items[0].value;
		case "pfolderid":			return this.items[1].value;
		case "empid":				return this.items[2].value;
		case "operandid":			return this.items[3].value;
		case "handlenodeid":		return this.items[4].value;
		case "requiredaction":		return this.items[5].value;
		case "actorder":			return this.items[6].value;
		case "actionresult":		return this.items[7].value;
		case "resultdescription":	return this.items[8].value;
		case "timeoutactiontype":	return this.items[9].value;
		case "timeoutmessage":		return this.items[10].value;
		case "startdate":			return this.items[11].value;
		case "timeout":				return this.items[12].value;
		case "finisheddate":		return this.items[13].value;
		case "canreadflow":			return this.items[14].value;
		case "canwriteflow":		return this.items[15].value;
		case "canwritecontent":		return this.items[16].value;
		case "priorow":				return this.items[17].value;
		case "nextow":				return this.items[18].value;
		case "backcenter":			return this.items[19].value;
		case "iscenter":			return this.items[20].value;
		case "response":			return this.items[21].value;
		case "sender":				return this.items[22].value;
		case "status":				return this.items[23].value;
		default:					return null;
	}
}

function _se_owsetvalue(sKey,sVal)
{
	switch(sKey.toLower())
	{
		case "id":					this.items[0].value = sVal;
		case "pfolderid":			this.items[1].value = sVal;
		case "empid":				this.items[2].value = sVal;
		case "operandid":			this.items[3].value = sVal;
		case "handlenodeid":		this.items[4].value = sVal;
		case "requiredaction":		this.items[5].value = sVal;
		case "actorder":			this.items[6].value = sVal;
		case "actionresult":		this.items[7].value = sVal;
		case "resultdescription":	this.items[8].value = sVal;
		case "timeoutactiontype":	this.items[9].value = sVal;
		case "timeoutmessage":		this.items[10].value = sVal;
		case "startdate":			this.items[11].value = sVal;
		case "timeout":				this.items[12].value = sVal;
		case "finisheddate":		this.items[13].value = sVal;
		case "canreadflow":			this.items[14].value = sVal;
		case "canwriteflow":		this.items[15].value = sVal;
		case "canwritecontent":		this.items[16].value = sVal;
		case "priorow":				this.items[17].value = sVal;
		case "nextow":				this.items[18].value = sVal;
		case "backcenter":			this.items[19].value = sVal;
		case "iscenter":			this.items[20].value = sVal;
		case "response":			this.items[21].value = sVal;
		case "sender":				this.items[22].value = sVal;
		case "status":				this.items[23].value = sVal;
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
	document.all.cbCenter.checked = this.valueOf("iscenter") == "1";
	document.all.cbNoResponse.checked = this.valueOf("response") == "0";
	document.all.txtSender.value = this.valueOf("sender");
}

function _se_owfromctrls()
{
	this.setValue("actorder",document.all.txtActOrder.value);
	this.setValue("timeoutactiontype",""+document.all.ddlTO.selectedIndex);
	this.setValue("timeoutmessage",document.all.txtMsg.value == "" ? "处理超时" : document.all.txtMsg.value);
	this.setValue("timeout",document.all.txtTime.value == "" ? "1" : document.all.txtTime.value);
	this.setValue("canreadflow",document.all.rbCanSeeFlow.checked ? "1" : "0");
	this.setValue("canwriteflow",document.all.rbCanModFlow.checked ? "1" : "0");
	this.setValue("canwritecontent",document.all.rbCanModContent.checked ? "1" : "0");
	this.setValue("backcenter",document.all.cbBackCenter.checked ? "1" : "0");
	this.setValue("iscenter",document.all.cbCenter.checked ? "1" : "0");
	this.setValue("response",document.all.cbNoResponse.checked ? "0" : "1");
	this.setValue("sender",document.all.txtSender.value == "" ? "未知" : document.all.txtSender.value);
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
	this.findByEmpID = _se_owafindbyempid;
}

function _se_owaadd(ow)
{
	ow.setValue("actorder",""+this.count);
	this.ows[this.count] = ow;
	this.count ++;
}

function remove(index)
{
	if(this.count == 0 || index < 0 || index >= this.count)
		return;
	for(var i = index;i < this.count - 1;i ++)
	{
		this.ows[i] = this.ows[i + 1];
		this.ows[i].setValue("actorder",""+i);
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
	this.count = 0;
}

function _se_owamovef(index)
{
	if(this.count < 2 || index < 1 || index >= this.count)
		return;
	var temp = this.ows[index];
	this.ows[index] = this.ows[index - 1];
	this.ows[index - 1] = temp;
	this.ows[index - 1].setValue("actorder",""+(index - 1));
	this.ows[index].setValue("actorder",""+index);
}

function _se_owamoveb(index)
{
	if(this.count < 2 || index < 0 || index >= this.count - 1)
		return;
	var temp = this.ows[index];
	this.ows[index] = this.ows[index + 1];
	this.ows[index + 1] = temp;
	this.ows[index + 1].setValue("actorder",""+(index + 1));
	this.ows[index].setValue("actorder",""+index);
}

function _se_owatostring()
{
	var sRet = "";
	for(var i = 0;i < this.count;i ++)
		sRet += this.ows[i].toString()+",";
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

function _se_owafindbyempid(sempid)
{
	for(var i = 0;i < this.count;i ++)
	{
		if(this.ows[i].valueOf("empid") == sempid)
			return this.ows[i];
	}
	return null;
}

function _se_show(sNodeID,sOperandID)
{
	return window.showModalDialog("SelEmps.aspx?NodeID="+sNodeID+"&OperandID="+sOperandID,null,"dialogHeight:434px;dialogWidth:595px;help:no;status:no");
}
