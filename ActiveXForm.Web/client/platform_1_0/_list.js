//
//target,"dialog args","dialog features"
//
function __getTarget(sTarget)
{
	var nPos = sTarget.indexOf(',');
	if(nPos < 0)
		return sTarget;
	if(nPos == 0)
		return "";
	return sTarget.substring(0,nPos);
}

function __getDialogArguments(sTarget)
{
	var ns = sTarget.indexOf('"');
	if(ns <= 0)
		return "";
	var ne = sTarget.indexOf('"',ns + 1);
	if(ne <= 0)
		return "";
	return sTarget.substring(ns + 1, ne);
}

function __getDialogFeatures(sTarget)
{
	var ns = sTarget.indexOf('","');
	if(ns <= 0)
		return "";
	ns += 3;
	var ne = sTarget.indexOf('"',ns);
	if(ne <= 0)
		return "";
	return sTarget.substring(ns, ne);
}
//
function __goto(url, sTarget, sMsg, nMsgType)
{
	if(sMsg != "")
	{
		if(nMsgType == 0)
		{
			alert(sMsg);
		}
		else
		{
			if(!confirm(sMsg))
				return;
		}
	}
	var sT = __getTarget(sTarget);
	sT = sT.toLowerCase();
	if(sT == "modaldialog")
	{
		var obj = new Object();
		obj.win  =this;
		obj.args = __getDialogArguments(sTarget);
		window.showModalDialog(url,obj,__getDialogFeatures(sTarget));
	}
	else if(sT == "modelessdialog")
	{
		var obj = new Object();
		obj.win  =this;
		obj.args = __getDialogArguments(sTarget);
		window.showModelessDialog(url,obj,__getDialogFeatures(sTarget));
	}
	else
	{
		if(sT == "")
			sT = "_self";
		window.open(url,sT);
	}
}