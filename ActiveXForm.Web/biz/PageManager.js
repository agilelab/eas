var MAX_PAGE_COUNT	= 8;
var TAB_HEIGHT = 20;
var WINDOW_OFFSET = 20;
var PAGE_WIDTH = 870;
var PAGE_HEIGHT = 500;
var TAB_WIDTH = 75;
var pageManager = new PageManager();

//---------------------------------------------------
function PageManager(){
	this.pageCount = 0;
	this.pages = new Array(MAX_PAGE_COUNT);
	this.pageVisible = null;
}

PageManager.prototype.AddPage=function(id, caption, nodeKey, pageKey, width, height)
{
	if(IndexOfPagekey(nodeKey) == false)		// 添加前先判断是否已有该类窗口，无则添加  // LiGongnan 2005-1-4
	{
		if(this.pageCount >= MAX_PAGE_COUNT)
		{
			alert("打开的窗口已经达到了最到数目，请关闭其他不用窗口。");
			return false;
		}
	
		//先将其他页置为非活动
		if (this.pageVisible != null) 
			this.UnactivatePage();
		//增加页签
		var tab = new TabWindow(id+"tab",caption, this.pageCount*TAB_WIDTH, 0);
		tab.ActivateTab();
		//增加页面//周按：已经更改

		PAGE_WIDTH = width;
		PAGE_HEIGHT = height - 20;
		
		var content = new ContentWindow(id+"frame", caption, pageKey, 0, TAB_HEIGHT);
		content.ShowWindow();
		//保存当前页

		var page = new Object();
		page.tab = tab;
		page.content = content;
		page.nodekey = nodeKey;									// 即为nodeID
		page.pagekey = pageKey;									// 即为pageID  LiGongnan 2005-1-4
		this.pages[this.pageCount] = page;
		this.pageVisible = page;
		this.pageCount++;
	}
}

// 循环各页面，若没有相应nodeKey的页则新建，若有个则打开	LiGongnan 2005-1-4
function IndexOfPagekey(nodeKey)
{
	for(var n=0; n<MAX_PAGE_COUNT; n++)
	{
		if(pageManager.pages[n] != null && pageManager.pages[n].nodekey == nodeKey)
		{
			pageManager.ActivatePage(pageManager.pages[n].tab.id);
			return true;
		}
	}
	return false;
}

PageManager.prototype.Dispose = function(){
	for(var n = 0; n < MAX_PAGE_COUNT; n++){
		if(pages[n] != null){
			pages[n].tab.DestroyTab();
			pages[n].content.DestroyWindow();
		}
	}
}

PageManager.prototype.GetActivePage = function(){
	return pageVisible;
}

//周按：已经更改

PageManager.prototype.ActivatePage = function(id){
	
	for(var n = 0; n < MAX_PAGE_COUNT; n++)
	{	
	  
		if(this.pages[n] != null && this.pages[n].tab.id != id)
		{
	          this.pages[n].tab.UnactivateTab();	
	          this.pages[n].content.HideWindow();
		}
		else if(this.pages[n] != null && this.pages[n].tab.id == id)
		{
		      this.pages[n].tab.ActivateTab();
			  this.pages[n].content.ActivateWindow();
		}
	}	
}
/*
PageManager.prototype.ActivatePage = function(){
	this.pageVisible.tab.ActivateTab();	// 否则将页签和窗口隐藏
	this.pageVisible.content.ActivateWindow();		
}
*/
PageManager.prototype.UnactivatePage=function(){
	this.pageVisible.tab.UnactivateTab();	// 否则将页签和窗口隐藏
	this.pageVisible.content.HideWindow();	
}

function PageClick()
{
	var id = event.srcElement.id;
	pageManager.ActivatePage(id);
//	
//	for(var n = 0; n < MAX_PAGE_COUNT; n++)
//	{
//		if(pageManager.pages[n] != null && pageManager.pages[n].tab.id == id)
//		{
//			BackTreeView("change", pageManager.pages[n].nodekey);
//		}
//	}
}

// 关闭窗口  LiGongnan
function RemoveClick()
{
	
	var id = event.srcElement.id;
	for(var n = 0; n < MAX_PAGE_COUNT; n++)
	{	
		if(pageManager.pages[n] != null && pageManager.pages[n].tab.id == id)
		{
			  ///////////////////////
				var d = document.getElementById(pageManager.pages[n].content.element.id);
//				alert(id);
//				alert(d.id);
//				alert(pageManager.pages[n].content.element.id);
//				alert(d.parentElement.id);
//				d.contentWindow.DisposeAllControl();return;
//				BackTreeView("close", pageManager.pages[n].nodekey);
			//	pageManager.pages[n].content.element.test();
			  ///////////////////////
	
			  document.body.removeChild(pageManager.pages[n].content.element);
			  document.body.removeChild(pageManager.pages[n].tab.element);
			  pageManager.pages.splice(n,1);			  
			  pageManager.pageCount--;
		}
	}	
	TabReRange();
}

// 重排tab  LiGongnan 2005-1-13
function TabReRange()
{
	var newArray = new Array(MAX_PAGE_COUNT);
	var i = 0;
	for(var n=0; n<MAX_PAGE_COUNT; n++)
	{
		if(pageManager.pages[n] != null)
		{
			newArray[i] = pageManager.pages[n];
			i++;
		}
	}
	pageManager.pages = newArray;
	
	for(var m=0; m<MAX_PAGE_COUNT; m++)
	{
		if(pageManager.pages[m] != null)
		{
			pageManager.pages[m].tab.element.style.left = m*TAB_WIDTH;
		}
	}
}

function OnResize()
{
	for(var m=0; m<MAX_PAGE_COUNT; m++)
	{
		if(pageManager.pages[m] != null)
		{
			pageManager.pages[m].content.element.style.width = pageManager.pages[m].tab.element.parentElement.clientWidth;
			pageManager.pages[m].content.element.style.height = pageManager.pages[m].tab.element.parentElement.clientHeight-20;
		}
	}
}

//---------------------------------------------------
//定义页处理类
//构造函数


function ContentWindow(id, caption, pageKey, left, top){
	this.miscellData = new Object;	
	this.id = id;
	this.caption = caption;
	this.UIPObject = null;
	this.element = document.createElement("IFRAME");
	this.element.name = id;
	this.element.id = id;
	this.element.style.left = left;
	this.element.style.top = top;
	this.element.border = 1;
	this.element.style.position = "absolute";
	this.element.style.width = PAGE_WIDTH;
	this.element.style.height = PAGE_HEIGHT;
	//引用PageHandler.aspx
	this.element.src = pageKey;
	this.element.attachEvent("onclick", PageClick);
//	this.element.attachEvent("onmouseover",OnMouseOver);
	document.body.appendChild(this.element);

	//////////////////
//	this.ee = document.createElement("IFRAME");
//	document.body.appendChild(this.ee);
	//////////////////
}

ContentWindow.prototype.ShowWindow = function(){
	//this.element.style.visiblity = "visible";
	//this.element.style.display = "block";
	this.element.style.zIndex = 9999;
}

ContentWindow.prototype.HideWindow = function(){
	//this.element.style.display = "none";
	//this.element.style.visiblity = "hidden";
	this.element.style.zIndex = 0;
}

ContentWindow.prototype.ActivateWindow = function(){
	this.ShowWindow();
	this.element.style.zIndex = MAX_PAGE_COUNT;
}

ContentWindow.prototype.DestroyWindow = function() {
	this.element.deatchEvent("onclick", PageClick);
}

//---------------------------------------------------------
//定义页签管理类
function TabWindow(id, caption, left, top)
{
//debugger;
	this.id = id;
	this.caption = caption;
	this.element = document.createElement("DIV");
	this.element.id = id;
	this.element.innerHTML = caption;
//	this.element.style.backgroundImage = "url(images/tab/01-01.gif)";
    this.element.className = 'dhx_tab_element dhx_tab_element_inactive';
	//this.element.style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(enabled=true,src='images/tab/01-01.gif',sizingMethod='scale')";
	this.element.style.border = "none";
	this.element.style.position = "absolute";
	this.element.style.fontSize = 12;
	this.element.style.left = left;
	this.element.style.top = top;
	var length = caption.length * 15 + 20;
	if(length > TAB_WIDTH)
	{
	    this.element.style.width = length;
	    TAB_WIDTH = length;
	}
	else
	{
	    this.element.style.width = TAB_WIDTH;
	}
	this.element.style.height = TAB_HEIGHT;
	this.element.style.cursor = 'hand';
	this.element.attachEvent("onclick", PageClick);
	this.element.zIndex = 99;
	this.state = 1;
	document.body.appendChild(this.element);
	
	this.el = document.createElement("DIV");
	this.el.id = id;
	this.el.style.width = 7;
	this.el.style.height = 7;
	this.el.style.left = TAB_WIDTH-14;
	this.el.style.top = top+1;
	this.el.style.position = "absolute";
	this.el.zIndex = 1;
//	this.el.attachEvent("onclick", RemoveClick);
	this.element.appendChild(this.el);
	
	this.img = document.createElement("IMAGE");
	this.img.id = id;
	this.img.zIndex = 0;
	this.img.src = "images/tab/close.gif";
	this.img.style.width = 10;
	this.img.style.height = 10;
	this.img.attachEvent("onclick", RemoveClick);
	this.el.appendChild(this.img);
	
	TAB_WIDTH = 75;
}

TabWindow.prototype.ActivateTab = function(){
	this.element.style.border= "2 groove gray";
	this.state = 1;
}

TabWindow.prototype.UnactivateTab = function(){
	this.element.style.border="none";//"1 groove gray";
	this.state = 0;
}

TabWindow.prototype.DestroyTab = function(){
	this.element.detachEvent("onclick",PageClick);
}

TabWindow.prototype.MoveTab = function(offset){
	this.element.left -= offset;
}
