    // JScript 文件

//    function document.oncontextmenu()
//    {
//    	var s = event.srcElement.tagName;

//    	if (s && s != "INPUT" && s != "TEXTAREA" || event.srcElement.disabled || document.selection.createRange().text.length == 0)
//    	{
//    		event.returnValue = false;
//    	}
//    }
//    function document.onselectstart()
//    {
//    	var s = event.srcElement.tagName;
//    	if (s != "INPUT" && s != "TEXTAREA") event.returnValue = false;
//    }
//    function document.ondragstart()
//    {
//    	event.returnValue = false;
//    }

    
	function OpenInfo(url,w,h) {
       var chasm = screen.availWidth;
	   var mount = screen.availHeight;
	   putItThere = window.open(''+url+'','','alwaysRaised=yes,scrollbars=yes,resizable=yes,width=' + w + ',height=' + h + ',left=' + ((chasm - w - 10) * .5) + ',top=' + ((mount - h - 30) * .5));
	   return putItThere;
	}
	
	
	function OpenInfo_NoResize (url,w,h) {
       var chasm = screen.availWidth;
	   var mount = screen.availHeight;
	   putItThere = window.open(''+url+'','','alwaysRaised=yes,scrollbars=yes,resizable=no,width=' + w + ',height=' + h + ',left=' + ((chasm - w - 10) * .5) + ',top=' + ((mount - h - 30) * .5));
	   return putItThere;
	}
	
	function OpenInfo_window(url,w,h,win) {
       var chasm = screen.availWidth;
	   var mount = screen.availHeight;
	   putItThere = window.open(''+url+'',win,'alwaysRaised=yes,scrollbars=yes,resizable=yes,width=' + w + ',height=' + h + ',left=' + ((chasm - w - 10) * .5) + ',top=' + ((mount - h - 30) * .5));
	   return putItThere;
	}
	
	function OpenInfo_AllScreen(url,win) {
       var w = screen.availWidth;
	   var h = screen.availHeight;
	   putItThere = window.open(''+url+'',win,'alwaysRaised=yes,scrollbars=yes,resizable=yes,left=0,top=0,width=' + w + ',height=' + h );
	   return putItThere;
	}
	
	function OpenModel(url,w,h)
	{
	    return window.showModelessDialog(url,window,'dialogWidth='+ w +'px;dialogHeight='+ h +'px;status:0;help:0;resizable:0;');
	}
	
	function OpenModalDialog(url,w,h)
	{
	    return window.showModalDialog(url,window,'dialogWidth='+ w +'px;dialogHeight='+ h +'px;status:0;help:0;resizable:0;');
	}
	
	function OpenModalDialogAndValue(url,value,w,h)
	{
	    return window.showModalDialog(url,value,'dialogWidth='+ w +'px;dialogHeight='+ h +'px;status:0;help:0;resizable:0;');
	}
	
	function InManage()
	{
	
	     var cw;
	    
	     if(window.name=="biz")
	     {
	        window.name="";
	        cw=window.open('biz/default.aspx','biz','top=0, left=0, toolbar=no, menubar=no, scrollbars=yes, resizable=no,location=no, status=no');
	        window.close();
	     }
	     else
	     {
	        cw=window.open('biz/default.aspx','_blank','top=0, left=0, toolbar=no, menubar=no, scrollbars=yes, resizable=no,location=no, status=no');
	     }
	     
		 cw.resizeTo(screen.availWidth,screen.availHeight);
         cw.focus();
         
	}
	
	function OpenSelectPersonel()
	{
	    OpenModalDialog('SelectPersonal.aspx',770,450);
	    //OpenInfo('SelectPersonal.aspx',760,400);
	    return false;
	}
	
	// Web方式创建新标签页
	function CreateTabPage(id, name, tabLoadType, url)
	{
	    document.parentWindow.top.document.getElementById("main").contentWindow.addPage(id, name, id, tabLoadType, url, "Web", "");
	}
	
	// 修改当前标签页名称
	function ChangeCurrentTabName(name_new)
	{
	    
	}
	
	function RemoveCurrentTab()
	{
	
	    document.parentWindow.top.document.getElementById("main").contentWindow.removeCurrentTab();
	}
	
	function RemoveTab(id)
	{
	    var tabbar = document.parentWindow.top.document.getElementById("main").contentWindow.tabbar;
	    tabbar.removeTab(id,true);
	}
	
	function GetCurrentTabID()
	{
	    try
	    {
	        var tabbar = document.parentWindow.top.document.getElementById("main").contentWindow.tabbar;
	        return tabbar.getActiveTab();
	    }
	    catch(e)
	    {
	        return "";
	    }
	}