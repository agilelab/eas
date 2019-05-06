function createOptionFor(oList, sText, sValue)
{
	var oOption = document.createElement("OPTION");
	oList.options.add(oOption);
	oOption.innerText = sText;
	oOption.value = sValue;
}

function clearList(oList)
{
	for(var i = oList.options.length - 1;i >= 0;i --)
	{
		oList.options.remove(i);
	}
}

function findByValue(oList,sVal)
{
	for(var i = 0;i < oList.options.length;i ++)
	{
		if(oList.options(i).value == sVal)
			return i;
	}
	return -1;
}

function isExists(sValue)
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

/*----------------------Select Employees start--------------------------------*/
function selEmployees(vOld)
{
	return window.showModalDialog("/OA.NET/Osu/SelectEmployees.aspx",vOld,"dialogHeight:265px;dialogWidth:499px;help:no;status:no");
}

function btnSel_Click()
{
	var theEmps = document.all.lbEmployees;
	if(theEmps.options.length == 0 || theEmps.selectedIndex < 0)
		return;
	if(isExists(theEmps.options(theEmps.selectedIndex).value))
		return;
	createOptionFor(document.all.lbSelectedEmps, theEmps.options(theEmps.selectedIndex).innerText.substring(3,theEmps.options(theEmps.selectedIndex).innerText.length), theEmps.options(theEmps.selectedIndex).value);
}

function btnDel_Click()
{
	var theSelectedEmps = document.all.lbSelectedEmps;
	if(theSelectedEmps.options.length == 0 || theSelectedEmps.selectedIndex < 0)
		return;
	//createOptionFor(document.all.lbEmployees, theSelectedEmps.options(theSelectedEmps.selectedIndex).innerText, theSelectedEmps.options(theSelectedEmps.selectedIndex).value);
	var i = theSelectedEmps.selectedIndex;
	theSelectedEmps.options.remove(i);
	if(theSelectedEmps.options.length == 0)
		return;
	if(i > 0)
		theSelectedEmps.selectedIndex = i - 1;
	else
		theSelectedEmps.selectedIndex = 0;
}

function btnSelAll_Click()
{
	var theEmps = document.all.lbEmployees;
	var theSelectedEmps = document.all.lbSelectedEmps;
	for(var i = 0;i < theEmps.options.length;i ++)
	{
		if(isExists(theEmps.options(i).value))
			continue;
		createOptionFor(theSelectedEmps, theEmps.options(i).innerText.substring(3,theEmps.options(i).innerText.length), theEmps.options(i).value);
	}
}

function btnDelAll_Click()
{
	var theSelectedEmps = document.all.lbSelectedEmps;
	clearList(theSelectedEmps);
}

function btnOK_Click()
{
	var theSelectedEmps = document.all.lbSelectedEmps;
	if(theSelectedEmps.options.length == 0)
	{
		var vRet = new Array(2);
		vRet[0] = "";
		vRet[1] = "";
		window.returnValue = vRet;
	}
	else
	{
		var vRet = new Array(2);
		vRet[0] = "";
		vRet[1] = "";
		for(var i = 0;i < theSelectedEmps.options.length;i ++)
		{
			vRet[0] += theSelectedEmps.options(i).innerText + ";";
			vRet[1] += theSelectedEmps.options(i).value + ";";
		}
		if(vRet[0] != "")
		{
			vRet[0] = vRet[0].substring(0,vRet[0].length - 1);
			vRet[1] = vRet[1].substring(0,vRet[1].length - 1);
		}
		window.returnValue = vRet;
	}
	window.close();
}

function btnCancel_Click()
{
	window.returnValue = null;
	window.close();
}

function _initEmps()
{
	var old = window.dialogArguments;
	if(old == null || old[0] == "" || old[1] == "")
		return;
	var theSel = document.all.lbSelectedEmps;
	var theAll = document.all.lbEmployees;
	var vv = old[1].split(';');
	var vi = old[0].split(';');
	for(var i = 0;i < vv.length;i ++)
	{
		createOptionFor(theSel,vi[i],vv[i]);
		theAll.options.remove(findByValue(theAll,vv[i]));
	}
}

function _selectEmps(oNames,oIDs)
{
	var arg = new Array(2);
	arg[0] = oNames.value;
	arg[1] = oIDs.value;
	var ov = selEmployees(arg);
	if(ov != null)
	{
		oNames.value = ov[0];
		oIDs.value = ov[1];
	}
}

/*----------------------Select Employees end--------------------------------*/

