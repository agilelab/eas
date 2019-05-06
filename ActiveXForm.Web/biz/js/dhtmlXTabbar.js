 
function dhtmlXTabBar(parentObject,mode,height)
{
 mode=mode||"top";
 this._mode=mode+"/";
 this._eczF=true;

 
 if(typeof(parentObject)!="object")
 this.entBox=document.getElementById(parentObject);
 else
 this.entBox=parentObject;

 this.width = this.entBox.getAttribute("width")|| this.entBox.style.width ||(window.getComputedStyle?window.getComputedStyle(this.entBox,null)["width"]:(this.entBox.currentStyle?this.entBox.currentStyle["width"]:0));
 if(this.width=="100%")
 this.enableAutoReSize(true);

 if((!this.width)||(this.width=="auto")||(this.width.indexOf("%")!=-1)||(parseInt(this.width)==0))
 this.width=this.entBox.offsetWidth+"px";

 this.height = this.entBox.getAttribute("height")|| this.entBox.style.height ||(window.getComputedStyle?window.getComputedStyle(this.entBox,null)["height"]:(this.entBox.currentStyle?this.entBox.currentStyle["height"]:0));
 if((!this.height)||(this.height.indexOf("%")!=-1)||(this.height=="auto"))this.height=this.entBox.offsetHeight+"px";

 this.activeTab = null;
 this.tabsId = new Object();

 this._inRange = true;          // 左右的标签是否在可视范围内
 this._align="left";
 this._offset=5;
 this._scrl_line_offset = 36;   // 滚动条按钮下线条的偏移量
 this._margin=1;
 this._height=parseInt(height||20);
 this._bMode=(mode=="right"||mode=="bottom");
 this._tabSize='150';
 this._content=new Array();
 this._tbst="win_text";
 this._styles={
 winDflt:["p_left.gif","p_middle.gif","p_right.gif","a_left.gif","a_middle.gif","a_right.gif","a_middle.gif",3,3,6,"#F4F3EE","#F0F8FF",false],
 winScarf:["with_bg/p_left.gif","with_bg/p_middle.gif","with_bg/p_right_skos.gif","with_bg/a_left.gif","with_bg/a_middle.gif","with_bg/a_right_skos.gif","with_bg/p_middle_over.gif",3,18,6,false,false,false],
 winBiScarf:["with_bg/p_left_skos.gif","with_bg/p_middle.gif","with_bg/p_right_skos.gif","with_bg/a_left_skos.gif","with_bg/a_middle.gif","with_bg/a_right_skos.gif","with_bg/p_middle_over.gif",18,18,6,false,false,false],
 winRound:["circuses/p_left.gif","circuses/p_middle.gif","circuses/p_right.gif","circuses/a_left.gif","circuses/a_middle.gif","circuses/a_right.gif","circuses/p_middle_over.gif",10,10,6,false,false,false]
 };
 
 this._createSelf(mode=="right"||mode=="left");
 this.setStyle("winDflt");
 return this;
}

 
dhtmlXTabBar.prototype.setOffset = function(offset)
{
 this._offset=offset;
}
 
dhtmlXTabBar.prototype.setAlign = function(align)
{
 if(align=="top")align="left";
 if(align=="bottom")align="right";
 this._align=align;
}
 
dhtmlXTabBar.prototype.setMargin = function(margin)
{
 this._margin=margin;
}

dhtmlXTabBar.prototype._createSelf = function(vMode)
{
 this._tabAll=document.createElement("DIV");
 this._tabZone=document.createElement("DIV");
 this._conZone=document.createElement("DIV");

 // 左滚动按钮
 this.scrl_l = document.createElement("DIV");
 this.scrl_l.style.width = "17px";
 this.scrl_l.style.height = "17px";
 this.scrl_l.style.left = this.entBox.style.pixelWidth - 34 + "px";
 this.scrl_l.style.top = "5px";
 this.scrl_l.style.position = "absolute";
 this.scrl_l.style.cursor = "hand";
 this.scrl_l.style.zIndex = 10000;
 this.scrl_l.style.backgroundImage = 'url("images/tab/scrl_l01.gif")';
 this.scrl_l.style.backgroundRepeat = "no-repeat";
 this.scrl_l.style.visibility = "hidden";
 this.scrl_l.onclick = this.scrollLeftClick;
 this.scrl_l.onmousedown = this.scrollLeftDown;
 this.scrl_l.onmouseup = this.scrollLeftUp;
 this.scrl_l.onmouseout = this.scrollLeftUp;
 
 // 右滚动按钮
 this.scrl_r = document.createElement("DIV");
 this.scrl_r.style.width = "17px";
 this.scrl_r.style.height = "17px";
 this.scrl_r.style.left = this.entBox.style.pixelWidth - 17 + "px";
 this.scrl_r.style.top = "5px";
 this.scrl_r.style.position = "absolute";
 this.scrl_r.style.cursor = "hand";
 this.scrl_r.style.zIndex = 10000;
 this.scrl_r.style.backgroundImage = 'url("images/tab/scrl_r01.gif")';
 this.scrl_r.style.backgroundRepeat = "no-repeat";
 this.scrl_r.style.visibility = "hidden";
 this.scrl_r.onclick = this.scrollRightClick;
 this.scrl_r.onmousedown = this.scrollRightDown;
 this.scrl_r.onmouseup = this.scrollRightUp;
 this.scrl_r.onmouseout = this.scrollRightUp;
 
 // 滚动按钮下的边框下
 this.scrl_line = document.createElement("DIV");
 this.scrl_line.style.width = "38px";
 this.scrl_line.style.height = "1px";
 this.scrl_line.style.left = this.entBox.style.pixelWidth - this._scrl_line_offset + "px";
 this.scrl_line.style.top = "22px";
 this.scrl_line.style.position = "absolute";
 this.scrl_line.className="dhx_tablist_line";
 
 this.entBox.appendChild(this.scrl_l);
 this.entBox.appendChild(this.scrl_r);
 this.entBox.appendChild(this.scrl_line);
 

 this.entBox.appendChild(this._tabAll);
 
if(this._bMode)
{
 this._tabAll.appendChild(this._conZone);
 this._tabAll.appendChild(this._tabZone);
}
else
{
 this._tabAll.appendChild(this._tabZone);
 this._tabAll.appendChild(this._conZone);
}


 this._vMode=vMode;
 
 if(vMode)
 {
    this._tabAll.className='dhx_tabbar_zoneV';
    this._setSizes=this._setSizesV;
    this._redrawRow=this._redrawRowV;
 }
 else
 {
    this._tabAll.className='dhx_tabbar_zone';
 }

 
 if(this._bMode)
 {
    this._tabAll.className+='B';
 }
 
 this._tabZone.className='dhx_tablist_zone';
 this._conZone.className='dhx_tabcontent_zone';
 
 this._tabZone.onselectstart = function()
 {
    return false;
 };
 
 this._tabAll.onclick = this._onClickHandler;
// this._tabAll.ondblclick = this._onDblClickHandler;
 this._tabAll.onmouseover = this._onMouseOverHandler;

 if(_isFF)
 {
    this._tabZone.onmouseout = this._onMouseOutHandler;
 }
 else
 {
    this._tabZone.onmouseleave = this._onMouseOutHandler;
 }
 this._tabAll.tabbar=this;

 this._lineA=document.createElement("div");
 this._lineA.className="dhx_tablist_line";

 this._lineA.style[vMode?"left":"top"]=(this._bMode?0:(this._height+2))+"px";
 this._lineA.style[vMode?"height":"width"]=this[vMode?"height":"width"];
 
 if(vMode)
 {
    this._conZone.style.height=this.height;
 }
 else
 {
    this._conZone.style.width=parseInt(this.width)-(_isFF?2:0)+"px";
 }

 this.rows=new Array();
 this.rowscount=1;
 this._createRow();
 this._setSizes();
}

dhtmlXTabBar.prototype.scrollLeftDown = function()
{
    this.style.backgroundImage = "url(\'images/tab/scrl_l02.gif\')";
}

dhtmlXTabBar.prototype.scrollLeftUp = function()
{
    this.style.backgroundImage = "url(\'images/tab/scrl_l01.gif\')";
}

dhtmlXTabBar.prototype.scrollRightDown = function()
{
    this.style.backgroundImage = "url(\'images/tab/scrl_r02.gif\')";
}

dhtmlXTabBar.prototype.scrollRightUp = function()
{
    this.style.backgroundImage = "url(\'images/tab/scrl_r01.gif\')";
}


var _scrlloffset = 0;
dhtmlXTabBar.prototype.scrollLeftClick = function()
{
   if(tabbar._checkScrollB())
        return;
        
    _scrlloffset += 100;
    
    if(tabbar == null)
        return;
        
    var row = tabbar.rows[0];
    var count=parseInt(tabbar._offset + _scrlloffset);
    for(var i=0;i<row.tabCount;i++)
    {
        if(row.childNodes[i].style.display=="none")
            continue;
        row.childNodes[i]._cInd=i;
        row.childNodes[i].style[tabbar._align]=count + "px";
        count+=row.childNodes[i]._offsetSize+parseInt(tabbar._margin);
        
    }
    
    tabbar._checkScrollA();
    return true;
}

dhtmlXTabBar.prototype.scrollRightClick = function()
{
    if(tabbar._checkScrollC())
        return;
        
    _scrlloffset -= 100;
    
    if(tabbar == null)
        return;
        
    var row = tabbar.rows[0];
    var count=parseInt(tabbar._offset + _scrlloffset);
    for(var i=0;i<row.tabCount;i++)
    {
        if(row.childNodes[i].style.display=="none")
            continue;
        row.childNodes[i]._cInd=i;
        row.childNodes[i].style[tabbar._align]=count + "px";
        count+=row.childNodes[i]._offsetSize+parseInt(tabbar._margin);
        
    }
    
    return true;
}


 
dhtmlXTabBar.prototype._createRow = function()
{
    var z=document.createElement("DIV");
    z.className='dhx_tabbar_row';
    this._tabZone.appendChild(z);
    z._rowScroller=document.createElement('DIV');
    z._rowScroller.style.display="none";
    z.appendChild(z._rowScroller);
    this.rows[this.rows.length]=z;
 
    if(this._vMode)
    {
        z.style.width=this._height+3+"px";
        z.style.height=parseInt(this.height)+"px";
        if(!this._bMode)
            this.setRowSizesA();
        else
            this.setRowSizesB();
    }
    else
    {
        
        z.style.height=this._height+3+"px";
        z.style.width = parseInt(this.width) - this._scrl_line_offset +"px";
    }

    z.appendChild(this._lineA);
}


dhtmlXTabBar.prototype._removeRow=function(row){
 row.parentNode.removeChild(row);
 var z=new Array();
 for(var i=0;i<this.rows.length;i++)
 if(this.rows[i]!=row)z[z.length]=this.rows[i];

 this.rows=z;
}

 
dhtmlXTabBar.prototype._setSizes = function()
{
    
    this._tabAll.height = this.height;
    this._tabAll.width = this.width;

    if(this._tabZone.childNodes.length)
        var z=this._tabZone.lastChild.offsetTop-this._tabZone.firstChild.offsetTop+this._height;
    else
        var z=this._height+(_isIE?5:0);

    var a=z-2;
    this._tabZone.style.height=(a>0?a:0)+"px";
    a=parseInt(this.height)-z-4;
    this._conZone.style.height=(a>0?a:0)+"px";
}
 
 
dhtmlXTabBar.prototype._setSizesV = function()
{
    this._tabAll.height=this.height;
    this._tabAll.width=this.width;

    var z=this._height*this.rows.length;

    if(!this._bMode)
    {
        this._tabZone.style.width=z+3+"px";
        this._conZone.style.width=parseInt(this.width)-z+"px";
        this._conZone.style.left= z+3+"px";
    }
    else
    {
        this._tabZone.style.width=z+3+"px";
        this._conZone.style.width=parseInt(this.width)-z+"px";
        this._tabZone.style.left=parseInt(this.width)-z+"px";
    }

    this._conZone.style.height=this.height;
    this._tabZone.style.height=this.height;
}


 
dhtmlXTabBar.prototype._redrawRowV=function(row){
 var talign=this._align=="left"?"top":"bottom";
 var count=parseInt(this._offset);
 for(var i=0;i<row.tabCount;i++){
 if(row.childNodes[i].style.display=="none")continue;
 row.childNodes[i]._cInd=i;
 row.childNodes[i].style[talign]=count+"px";
 count+=row.childNodes[i]._offsetSize+parseInt(this._margin);
}


};



 
dhtmlXTabBar.prototype.setRowSizesA=function(){
 for(var i=0;i<this.rows.length;i++){
 this.rows[i].style.left=i*this._height+"px";
 this.rows[i].style.zIndex=5+i;
}
}
 
dhtmlXTabBar.prototype.setRowSizesB=function(){
 for(var i=this.rows.length-1;i>=0;i--){
 this.rows[i].style.left=i*this._height+"px";
 this.rows[i].style.zIndex=15-i;
}
}
 
dhtmlXTabBar.prototype.setRowSizesC=function(){
 for(var i=this.rows.length-1;i>=0;i--){
 this.rows[i].style.zIndex=15-i;
}
}

//移动鼠标,引起频繁的刷新 魏琼东 2012-06-02
dhtmlXTabBar.prototype._onMouseOverHandler=function(e)
{
     if(_isIE)
     var target = event.srcElement;
     else
     var target = e.target;

     target=this.tabbar._getTabTarget(target);
//     if(!target)
//     {
//         this.tabbar._hideHover(target);return;
//     }

//     this.tabbar._showHover(target);
//     (e||event).cancelBubble=true;
}
 
dhtmlXTabBar.prototype._onMouseOutHandler=function(e)
{
//    this.parentNode.tabbar._hideHover(null);return;
}
 
dhtmlXTabBar.prototype._onClickHandler=function(e)
{

    if(_isIE)
    var target = event.srcElement;
    else
    var target = e.target;

    if(document.body.onclick)document.body.onclick(e);
    (e||event).cancelBubble=true;


    target=this.tabbar._getTabTarget(target);
    if(!target)return;

    this.tabbar._setTabActive(target);
    return false;
}

dhtmlXTabBar.prototype._onDblClickHandler=function(e)
{
    if(_isIE)
    var target = event.srcElement;
    else
    var target = e.target;

    if(document.body.ondblclick)document.body.ondblclick(e);
    (e||event).cancelBubble=true;


    target=this.tabbar._getTabTarget(target);
    if(!target)return;

    this.tabbar.removeTab(target.id, true);
    return false;
}

 
dhtmlXTabBar.prototype._getTabTarget=function(t){
 while((!t.className)||(t.className.indexOf("dhx_tab_element")==-1)){
 if((t.className)&&(t.className.indexOf("dhx_tabbar_zone")!=-1))return null;
 t=t.parentNode;
 if(!t)return null;
}
 return t;
}

 
dhtmlXTabBar.prototype._redrawRow=function(row)
{
    
    var count=parseInt(this._offset) + _scrlloffset;
    for(var i=0;i<row.tabCount;i++)
    {
        if(row.childNodes[i].style.display=="none")
            continue;
        row.childNodes[i]._cInd=i;
        row.childNodes[i].style[this._align]=count+"px";
        count+=row.childNodes[i]._offsetSize+parseInt(this._margin);
    }
}


 
dhtmlXTabBar.prototype.removeTab = function(tabId,mode)
{
    var tab;
    for(var n = 0; n < maxnum; n++)
    {	
	     if(this.tabsId[n] && (this.tabsId[n].id == tabId))
	     tab = this.tabsId[n];
	}
	
 //var tab=this.tabsId[tab];
    if(!tab)return;
 
    if((this._onremove)&&(!this._onremove(tab.id)))
    {
        return false;
    }

    if(this._content[tab.idd])
    {
        this._content[tab.idd].parentNode.removeChild(this._content[tab.idd]);
        this._content[tab.idd]=null;
    }

    if(this._contents[tab.idd])
    {
        // 关闭标签页前检查是否包含ClientBud容器，如果有先执行其Quit方法
              try
              {
                  var yyt = this._contents[tab.idd].children[0].contentWindow.document.getElementById("yyt");
                  if(yyt != null)
                  {
                      this._contents[tab.idd].children[0].contentWindow.form1.yyt.Quit();
                      yyt.parentNode.removeChild(yyt);
                  }
              }
              catch(e){}
              
        this._contents[tab.idd].parentNode.removeChild(this._contents[tab.idd]);
        this._contents[tab.idd]=null;
    }

    this._goToAny(tab,mode);

    var row=tab.parentNode;
    row.removeChild(tab);
    row.tabCount--;
    if((row.tabCount==0)&&(this.rows.length>1))
    this._removeRow(row);
    this.tabsId[tab.idd]=null;
    this._redrawRow(row)
    this._setSizes();
    this._checkScrollA();
 
    num --;
    
//    document.parentWindow.top.document.getElementById('main').document.parentWindow.CollapseV(false)
}

dhtmlXTabBar.prototype.removeAllTab = function(mode)
{
    var tab;
    for(var n = 0; n < maxnum; n++)
    {	
	     tab = this.tabsId[n];
	
         if(!tab)continue;
 
          if((this._onremove)&&(!this._onremove(tab.id)))
          {
               continue;
          }

          if(this._content[tab.idd])
          {
                this._content[tab.idd].parentNode.removeChild(this._content[tab.idd]);
                this._content[tab.idd]=null;
          }

          if(this._contents[tab.idd])
          {
              // 关闭标签页前检查是否包含ClientBud容器，如果有先执行其Quit方法
              try
              {
                  var yyt = this._contents[tab.idd].children[0].contentWindow.document.getElementById("yyt");
                  if(yyt != null)
                  {
                      this._contents[tab.idd].children[0].contentWindow.form1.yyt.Quit();
                      yyt.parentNode.removeChild(yyt);
                  }
              }
              catch(e){}
              
              this._contents[tab.idd].parentNode.removeChild(this._contents[tab.idd]);
              this._contents[tab.idd]=null;
          }

          this._goToAny(tab,mode);

          var row=tab.parentNode;
          row.removeChild(tab);
          row.tabCount--;
          if((row.tabCount==0)&&(this.rows.length>1))
          this._removeRow(row);
          this.tabsId[tab.idd]=null;
          this._redrawRow(row)
          this._setSizes();
 
          num --;
     }
}

dhtmlXTabBar.prototype._goToAny=function(tab,mode){
 if((this._lastActive)==tab)
 if(convertStringToBoolean(mode)){if(null===this.goToPrevTab())if(null===this.goToNextTab())this._lastActive=null;}
 else this._lastActive=null;
}

 var num = 0;
 var maxnum = 0;
dhtmlXTabBar.prototype.addTab = function(id,text,size,position,row)
{

    row=row||0;

    var z=this.rows[row].tabCount||0;
    if((!position)&&(position!==0))
    position=z;

    var tab=this._createTab(text,size, id);
    tab.idd = maxnum//num;
    tab.id = id;
    this.tabsId[maxnum] = tab;

    
    this.rows[row].insertBefore(tab,this.rows[row].childNodes[position]);

    this.rows[row].tabCount=z+1;
    this._redrawRow(this.rows[row]);
    this._setSizes();
    
    
    num++;
    maxnum++;
    
    this._checkScrollA();
    
//    this.scrl_l = document.createElement("IMAGE");
//    this.scrl_l.left = "100px";
//    this.scrl_l.top = "0px";
//    this._tabZone.appendChild(this.scrl_l);
    //this._tabAll.appendChild(this.scrl_l);
//    this.setOffset(100 * maxnum);
}

 
dhtmlXTabBar.prototype._showHover=function(tab){
 if(tab._disabled)return;
 this._hideHover(tab);
 if(tab==this._lastActive)return;
 switch(this._tbst){
 case "win_text":
 tab._lChild.style.backgroundImage='url('+this._imgPath+this._mode+this._styles[this._cstyle][6]+')';
 break;
}
 this._lastHower=tab;
}
 
dhtmlXTabBar.prototype._hideHover=function(tab){
 if((!this._lastHower)||(this._lastHower==tab)||(this._lastHower==this._lastActive))
 return;
 switch(this._tbst){
 case "win_text":
 this._lastHower._lChild.style.backgroundImage='url('+this._imgPath+this._mode+this._styles[this._cstyle][1]+')';
 break;
}
 this._lastHower=null;
}

 
dhtmlXTabBar.prototype._getTabById=function(tabId)
{
    for(var n = 0; n < maxnum; n++)
    {	
	     if(this.tabsId[n] && (this.tabsId[n].id == tabId))
	     return this.tabsId[n];
	}
////    return this.tabsId[tabId];
}

dhtmlXTabBar.prototype.getTabIdLast = function(tabId)
{
    var temp = "";
    for(var n = maxnum - 1; n >= 0; n--)
    {	
	     if(this.tabsId[n])
	     {
	        temp = this.tabsId[n].id;
	        var i = temp.indexOf("[");
            if(i > 0)
            {
                temp = temp.substring(0, i);
            }   
          
	        if(temp == tabId)
	        {
	            return this.tabsId[n].id;
	        }
	     }
	}
}

 
dhtmlXTabBar.prototype.setTabActive= function boolean (tabId)
{
    var tab=this._getTabById(tabId);
    if(tab)
    {
        this._setTabActive(tab);
        return true;
    }
    else
    {
        return false;
    }
}
 
dhtmlXTabBar.prototype._setTabActive=function(tab)
{

    if((tab._disabled)||(tab.style.display=="none"))
    {
        return false;
    }
    if((this._onsel)&&(!this._onsel(tab.idd,this._lastActive?this._lastActive.idd:null)))
    {
        return false;
    }
    if(this._lastActive)
    {
        this._lastActive.className=this._lastActive.className.replace(/dhx_tab_element_active/g,"dhx_tab_element_inactive");
    }
    tab.className=tab.className.replace(/dhx_tab_element_inactive/g,"dhx_tab_element_active");
    if((this._lastActive)&&(this._styles[this._cstyle][10]))
    this._lastActive.style.backgroundColor=this._styles[this._cstyle][10];
    if(this._styles[this._cstyle][11])
    tab.style.backgroundColor=this._styles[this._cstyle][11];

 
    if(this._vMode)
    {
        switch(this._tbst)
        {
            case "win_text":
            if(this._lastActive)
            {
                this._lastActive._lChild.style.backgroundImage='url('+this._imgPath+this._mode+this._styles[this._cstyle][1]+')';
                this._lastActive.childNodes[0].childNodes[0].src=this._imgPath+this._mode+this._styles[this._cstyle][0];
                this._lastActive.childNodes[1].childNodes[0].src=this._imgPath+this._mode+this._styles[this._cstyle][2];
                this._lastActive.style.height=parseInt(this._lastActive.style.height)-this._styles[this._cstyle][9]+"px";
                this._lastActive._lChild.style.height=parseInt(this._lastActive._lChild.style.height)-this._styles[this._cstyle][9]+"px";
                this._lastActive.style[this._align=="right"?"marginBottom":"marginTop"]="0px"
                this._lastActive.style.width=this._height+1+"px";
                if(this._bMode)
                this._lastActive._lChild.style.width=this._height+1+"px";
            }

            tab._lChild.style.backgroundImage='url('+this._imgPath+this._mode+this._styles[this._cstyle][4]+')';
            tab.childNodes[0].childNodes[0].src=this._imgPath+this._mode+this._styles[this._cstyle][3];
            tab.childNodes[1].childNodes[0].src=this._imgPath+this._mode+this._styles[this._cstyle][5];
            tab.style.height=parseInt(tab.style.height)+this._styles[this._cstyle][9]+"px";
            tab._lChild.style.height=parseInt(tab._lChild.style.height)+this._styles[this._cstyle][9]+"px";
            tab.style[this._align=="right"?"marginBottom":"marginTop"]="-3px"
            tab.style.width=this._height+3+"px";
            if(this._bMode)
            {
                tab._lChild.style.width=this._height+3+"px";
            }
            break;
       }
    }
    else
    {
        switch(this._tbst)
        {
            case "win_text":
            if(this._lastActive)
            {
                this._lastActive._lChild.style.backgroundImage='url('+this._imgPath+this._mode+this._styles[this._cstyle][1]+')';
                this._lastActive.childNodes[0].childNodes[0].src=this._imgPath+this._mode+this._styles[this._cstyle][0];
                this._lastActive.childNodes[1].childNodes[0].src=this._imgPath+this._mode+this._styles[this._cstyle][2];
                this._lastActive.style.width=parseInt(this._lastActive.style.width)-this._styles[this._cstyle][9]+"px";
                this._lastActive._lChild.style.width=parseInt(this._lastActive._lChild.style.width)-this._styles[this._cstyle][9]+"px";
                this._lastActive.style[this._align=="left"?"marginLeft":"marginRight"]="0px"
                this._lastActive.style.height=this._height+1+"px";
 
                if(this._bMode)
                    this._lastActive._lChild.style.height=this._height+1+"px";
 
                // 非活动标签隐藏关闭按钮
                this._lastActive.all[4].children[1].style.visibility = "hidden";
            }

            tab._lChild.style.backgroundImage='url('+this._imgPath+this._mode+this._styles[this._cstyle][4]+')';
            tab.childNodes[0].childNodes[0].src=this._imgPath+this._mode+this._styles[this._cstyle][3];
            tab.childNodes[1].childNodes[0].src=this._imgPath+this._mode+this._styles[this._cstyle][5];
            tab.style.width=parseInt(tab.style.width)+this._styles[this._cstyle][9]+"px";
            tab._lChild.style.width=parseInt(tab._lChild.style.width)+this._styles[this._cstyle][9]+"px";
            tab.style[this._align=="left"?"marginLeft":"marginRight"]="-3px"
            tab.style.height=this._height+3+"px";
 
            if(this._bMode)
                tab._lChild.style.height=this._height+3+"px";
            
            // 活动标签显示关闭按钮
            var img_close = tab.all[4].children[1];
            img_close.onclick = this.RemoveClick;
            img_close.style.visibility = "";
 
            break;
        }
    }
    this._setContent(tab);

    this._lastActive=tab;
    
    this._checkScrollD();
    return true;
}

dhtmlXTabBar.prototype.RemoveClick = function()
{
    tabbar.removeTab(event.srcElement.id,true);
    return true;
}


 
dhtmlXTabBar.prototype._createTab = function(text,size, id)
{
    
    var tab=document.createElement("DIV");
    tab.className='dhx_tab_element dhx_tab_element_inactive';
    var thml="";

    if(this._vMode)
    {
        tab.style.height=parseInt(size||this._tabSize)+"px";
        tab.style.width=this._height+1+"px";
    }
    else
    {
        tab.style.width=parseInt(size||this._tabSize)+"px";
        tab.style.height=this._height+1+"px";
    }
    
    switch(this._tbst)
    {
        case 'text':
            thml=text;
            break;
        case 'win_text':
 
            if(this._vMode)
            {
                thml='<div style="position:absolute;'+(this._bMode?"right":"left")+':0px;top:0px;height:'+this._styles[this._cstyle][7]+'px;width:'+(this._height+3)+'px;"><img src="'+this._imgPath+this._mode+this._styles[this._cstyle][0]+((this._bMode&&(_isFF||_isOpera))?'" style="position:absolute;right:1px;"':'"')+'></div>';
                thml+='<div style="position:absolute;'+(this._bMode?"right":"left")+':0px;bottom:0px;height:'+this._styles[this._cstyle][8]+'px;width:'+(this._height+3)+'px;"><img src="'+this._imgPath+this._mode+this._styles[this._cstyle][2]+((this._bMode&&(_isFF||_isOpera))?'" style="position:absolute;right:1px;"':'"')+'></div>';
                thml+='<div style="position:absolute;background-repeat: repeat-y;background-image:url('+this._imgPath+this._mode+this._styles[this._cstyle][1]+');width:'+(this._height)+'px;left:0px;top:'+this._styles[this._cstyle][7]+'px;height:'+(parseInt(size||this._tabSize)-this._styles[this._cstyle][8]-this._styles[this._cstyle][7]+"px")+'">'+text+'</div>';
            }
            else
            {
                thml='<div style="position:absolute;'+(this._bMode?"bottom":"top")+':0px;left:0px;width:'+this._styles[this._cstyle][7]+'px;height:'+(this._height+3)+'px;"><img src="'+this._imgPath+this._mode+this._styles[this._cstyle][0]+((this._bMode&&_isFF)?'" style="position:absolute;bottom:0px;"':'"')+'></div>';
                thml+='<div style="position:absolute;'+(this._bMode?"bottom":"top")+':0px;right:0px;width:'+this._styles[this._cstyle][8]+'px;height:'+(this._height+3)+'px;"><img src="'+this._imgPath+this._mode+this._styles[this._cstyle][2]+((this._bMode&&_isFF)?'" style="position:absolute;bottom:0px;left:0px;"':'"')+'></div>';
                thml+='<div style="position:absolute;background-repeat: repeat-x;background-image:url('+this._imgPath+this._mode+this._styles[this._cstyle][1]+');height:'+(this._height+(this._bMode?1:3))+'px;top:0px;left:'+this._styles[this._cstyle][7]+'px;width:'+(parseInt(size||this._tabSize)-this._styles[this._cstyle][8]-this._styles[this._cstyle][7]+"px")+';"><div style="padding-top:3px; width:'+(parseInt(size) - 30)+'px">'+text+'</div>';
//                thml+='<img id="'+id+'" src="images/tab/close.gif" onmouseover="this.src = \'images/tab/close_hover.gif\';" onmouseout="this.src = \'images/tab/close.gif\';" style=" position:absolute; width:10px; height:10px; left: '+(tab.style.pixelWidth - 13)+'px; top: 5px;" /></div>';
                thml+='<img id="'+id+'" src="images/tab/close.gif" style=" position:absolute; width:10px; height:10px; left: '+(tab.style.pixelWidth - 13)+'px; top: 5px;" /></div>';
//                thml+='<div style="position:absolute;background-repeat: repeat-x;background-image:url(images/tab/close.gif);height:'+(this._height+(this._bMode?1:3))+'px;top:0px;left:'+this._styles[this._cstyle][7]+'px;width:'+(parseInt(size||this._tabSize)-this._styles[this._cstyle][8]-this._styles[this._cstyle][7]+"px")+';" onclick="RemoveClick()"></div>';
            }
        
            if(!this._styles[this._cstyle][10])
            {
                tab.style.backgroundColor='transparent';
            }
            else 
            {
                tab.style.backgroundColor=this._styles[this._cstyle][10];
            }
            break;
    }
    tab.innerHTML=thml;
    tab._lChild=tab.childNodes[tab.childNodes.length-1];
    tab._offsetSize=parseInt(size||this._tabSize);
    
    
	
    return tab;
}

 
dhtmlXTabBar.prototype.clearAll = function(){
 var z=this._conZone.style.backgroundColor;
 this._content=new Array();
 this.tabsId=new Array();
 this.rows=new Array();
 this._lastActive=null;
 this._lastHower=null;
 this.entBox.innerHTML="";

 this._createSelf(this._vMode);
 this.setStyle(this._cstyle);
 if(z)this._conZone.style.backgroundColor=z;

 this.enableContentZone(this._eczF);
}



 
dhtmlXTabBar.prototype.setImagePath = function(path){
 this._imgPath=path;
}




 
dhtmlXTabBar.prototype.loadXMLString=function(xmlString,afterCall){
 this.XMLLoader=new dtmlXMLLoaderObject(this._parseXML,this,true,this.no_cashe);
 this.waitCall=afterCall||0;
 this.XMLLoader.loadXMLString(xmlString);
};
 
 dhtmlXTabBar.prototype.loadXML=function(file,afterCall){
 this.XMLLoader=new dtmlXMLLoaderObject(this._parseXML,this,true,this.no_cashe);
 this.waitCall=afterCall||0;
 this.XMLLoader.loadXML(file);
}


 dhtmlXTabBar.prototype._getXMLContent=function(node){
 var text="";
 for(var i=0;i<node.childNodes.length;i++)
{
 var z=node.childNodes[i];
 text+=(z.nodeValue===null?"":z.nodeValue);
}
 return text;
}
 
 dhtmlXTabBar.prototype._parseXML=function(that,a,b,c,obj){
 that.clearAll();
 var selected="";
 if(!obj)obj=that.XMLLoader;

 var atop=obj.getXMLTopNode("tabbar");
 var arows = obj.doXPath("//row",atop);
 
 that._hrfmode=atop.getAttribute("hrefmode")||that._hrfmode;
 
 
 that._margin =atop.getAttribute("margin")||that._margin;
 that._align =atop.getAttribute("align")||that._align;
 that._offset =atop.getAttribute("offset")||that._offset;

 var acs=atop.getAttribute("tabstyle");
 if(acs)that.setStyle(acs);

 acs=atop.getAttribute("skinColors");
 if(acs)that.setSkinColors(acs.split(",")[0],acs.split(",")[1]);
 
 for(var i=0;i<arows.length;i++){
 var atabs = obj.doXPath("./tab",arows[i]);
 for(var j=0;j<atabs.length;j++){
 var width=atabs[j].getAttribute("width");
 var name=that._getXMLContent(atabs[j]);
 var id=atabs[j].getAttribute("id");
 that.addTab(id,name,width,"",i);
 if(atabs[j].getAttribute("selected"))selected=id;

 
 if(that._hrfmode)
 that.setContentHref(id,atabs[j].getAttribute("href"));
 else
 
 
 for(var k=0;k<atabs[j].childNodes.length;k++)
 if(atabs[j].childNodes[k].tagName=="content")
 that.setContentHTML(id,that._getXMLContent(atabs[j].childNodes[k]));
 


}
}
 if(selected)that.setTabActive(selected);
 if(that.dhx_xml_end)that.dhx_xml_end(that);
}

 
 dhtmlXTabBar.prototype.setOnLoadingEnd=function(func){
 if(typeof(func)=="function")
 this.dhx_xml_end=func;
 else
 this.dhx_xml_end=eval(func);
};

 
 
 dhtmlXTabBar.prototype.forceLoad=function(tabId,href){
 var tab=this.tabsId[tabId];
 if(href)this._hrefs[tabId]=href;
 this._content[tab.idd]._loaded=false;

 this._setContent(tab,this._lastActive.idd!=tabId);
}

 
 dhtmlXTabBar.prototype.setHrefMode=function(mode){
 this._hrfmode=mode;
}
 
 dhtmlXTabBar.prototype.setContentHref=function(id,href)
 {

    if(!this._hrefs)
        this._hrefs=new Array();
    if(!this._contents)
        this._contents = new Array();
    
    this._hrefs[id]=href;
    switch(this._hrfmode)
    {
        case "iframe":
//            if(!this._glframe)
//            {
                var z=document.createElement("DIV");
                z.style.width='100%';
                z.style.height='100%';
//                z.innerHTML="<iframe frameborder='0' width='100%' height='100%' src='"+this._imgPath+"blank.html'></iframe>";
                z.innerHTML="<iframe id='aaa' frameborder='0' width='100%' height='100%' src='"+href+"' onload=\"TabContentLoad('"+id+"')\"></iframe>";
//                z.src = href;
                z.style.position = "absolute";
//                var _glframe=z.childNodes[0];
//                this._conZone.appendChild(_glframe);
                this._conZone.appendChild(z);
                var num = 0;
                for(var n = 0; n < maxnum; n++)
	            {	
	                if(this.tabsId[n] && (this.tabsId[n].id == id))
		                num = this.tabsId[n].idd;
	            }
                this._contents[num] = z;
//            }
            break;
        case "iframes":
            case "iframes-on-demand":
            var z=document.createElement("DIV");
            z.style.width='100%';
            z.style.height='100%';
            z.style.display='none';
            z.innerHTML="<iframe frameborder='0' width='100%' height='100%' src='"+this._imgPath+"blank.html'></iframe>";
            if(this._hrfmode=="iframes")
            z.childNodes[0].src=href;
            this.setContent(id,z);
            break;
        case "ajax":
        case "ajax-html":
             var z=document.createElement("DIV");
             z.style.width='100%';
             z.style.height='100%';
             this.setContent(id,z);
           break;
    }
}

 
 dhtmlXTabBar.prototype.tabWindow=function(tab_id)
 {
    
    if(this._hrfmode.indexOf("iframe")==0)
    {
        return(this._content[tab_id]?this._content[tab_id].childNodes[0].contentWindow:null);
    }
}

dhtmlXTabBar.prototype.getContentById = function(tab_id)
{
    
    var tab=this._getTabById(tab_id);
    if(tab)
    {
        return this._contents[tab.idd];
    }
}

 dhtmlXTabBar.prototype._ajaxOnLoad=function(obj,a,b,c,loader){
 if(this._hrfmode=="ajax"){
 var z=loader.getXMLTopNode("content");
 var val=obj[0]._getXMLContent(z);
}
 else var val=loader.xmlDoc.responseText;

 obj[0]._resolveContent(obj[1],val);
 
 obj[0].adjustSize();
 
}
 dhtmlXTabBar.prototype._resolveContent=function(id,val){ 
 var z=val.match(/<script[^>]*>([^<]+)<\/script>/g);
 if(this._content[id]){
 this._content[id].innerHTML=val;
 if(z)
 for(var i=0;i<z.length;i++)
 eval(z[i].replace(/<([/]{0,1})script[^>]*>/g,""));
}
}
 


 
 dhtmlXTabBar.prototype.setOnSelectHandler=function(func)
 {
    if(typeof(func)=="function")
        this._onsel=func;
    else
        this._onsel=eval(func);
 }
 
 dhtmlXTabBar.prototype.setOnRemoveHandler = function(func)
 {
    if(typeof(func) == "function")
    {
        this._onremove = func;
    }
    else
    {
        this._onremove = eval(func);
    }
 }
 
 dhtmlXTabBar.prototype.setContent=function(id,nodeId){
 if(typeof(nodeId)=="string")
 nodeId=document.getElementById(nodeId);


 if(this._content[id])
 this._content[id].parentNode.removeChild(this._content[id]);

 this._content[id]=nodeId;
 if(nodeId.parentNode)nodeId.parentNode.removeChild(nodeId);
 nodeId.style.position="absolute";
 if(this._dspN){
 nodeId.style.display="none";
 nodeId.style.visibility="visible";
}
 else{
 nodeId.style.visibility="hidden";
 nodeId.style.display="block";
}
 nodeId.style.top="0px";nodeId.style.top="0px";
 this._conZone.appendChild(nodeId);

 if((this._lastActive)&&(this._lastActive.idd==id))this._setContent(this._lastActive);
}


 
 dhtmlXTabBar.prototype._setContent=function(tab,stelth)
 {
 
    if(this._hrfmode)
    switch(this._hrfmode)
    {
        case "iframe":
            //this._glframe.src=this._hrefs[tab.idd];
            for(var n = 0; n < /*this._contents.length*/maxnum; n++)
	        {	
	            if(this._contents[n]!=null)
		            this._contents[n].style.zIndex = 0;
	        }
	        if(this._contents[tab.idd])
	        {
	            this._contents[tab.idd].style.zIndex = 9990;	
	        }
            return;
            break;
        case "iframes":
        case "iframes-on-demand":
            if((this._hrfmode=="iframes-on-demand")&&(!this._content[tab.idd]._loaded))
            {
                this._content[tab.idd].childNodes[0].src=this._hrefs[tab.idd];
                this._content[tab.idd]._loaded="true";
            }
            break;
        case "ajax":
        case "ajax-html":
            var z=this._content[tab.idd];
            if(!z._loaded)
            {
                z.innerHTML="<div class='dhx_ajax_loader'><img src='"+this._imgPath+"loading.gif' />&nbsp;Loading...</div>";
                (new dtmlXMLLoaderObject(this._ajaxOnLoad,[this,tab.idd],true,this.no_cashe)).loadXML(this._hrefs[tab.idd]);
                z._loaded=true;
            }
            break;
    }
 

    if(!stelth){
    if((this._lastActive)&&(this._content[this._lastActive.idd]))
    if(this._dspN)
        this._content[this._lastActive.idd].style.display='none';
    else
        this._content[this._lastActive.idd].style.visibility='hidden';

    if(this._content[tab.idd])
    if(this._dspN)
        this._content[tab.idd].style.display='block';
    else
        this._content[tab.idd].style.visibility='';
    }
 
    this.adjustSize();
 
}
 
 dhtmlXTabBar.prototype.setContentHTML=function(id,html){
 var z=document.createElement("DIV");
 z.style.width="100%";
 z.style.height="100%";
 z.style.overflow="auto";
 z.innerHTML=html;
 this.setContent(id,z);
}

 
 dhtmlXTabBar.prototype.setStyle=function(name){
 if(this._styles[name]){
 this._cstyle=name;
 if(this._styles[this._cstyle][12])
 this._conZone.style.backgroundColor=this._styles[this._cstyle][12];
}
}


 
 dhtmlXTabBar.prototype.enableContentZone=function(mode){
 this._eczF=convertStringToBoolean(mode);
 this._conZone.style.display=this._eczF?"":'none';
}

 
 dhtmlXTabBar.prototype.enableForceHiding=function(mode){
 this._dspN=convertStringToBoolean(mode);
}

 
 dhtmlXTabBar.prototype.setSkinColors=function(a_tab,p_tab,c_zone){
 this._styles[this._cstyle][10]=p_tab;
 this._styles[this._cstyle][11]=a_tab;
 this._conZone.style.backgroundColor=c_zone||a_tab;
}

 
dhtmlXTabBar.prototype.getActiveTab=function(){
 if(this._lastActive)return this._lastActive.id;
 return null;
}
 
dhtmlXTabBar.prototype.goToNextTab=function(tab){
 var z=tab||this._lastActive;
 if(z){
 if(z.nextSibling.idd){
 if(!this._setTabActive(z.nextSibling))
 return this.goToNextTab(z.nextSibling);
 return z.nextSibling.idd;
}
 else
 if(z.parentNode.nextSibling){
 var arow=z.parentNode.nextSibling.childNodes[0];
 if(!this._setTabActive(arow))
 return this.goToNextTab(arow);
 return arow.idd;
}
}
 return null;
}
 
dhtmlXTabBar.prototype.goToPrevTab=function(tab){
 var z=tab||this._lastActive;
 if(z){
 if(z.previousSibling){
 if(!this._setTabActive(z.previousSibling))
 return this.goToPrevTab(z.previousSibling);
 return this._lastActive.idd;
}
 else
 if(z.parentNode.previousSibling){
 var arow=z.parentNode.previousSibling.childNodes[arow.tabCount-1];
 if(!this._setTabActive(arow))
 return this.goToPrevTab(arow);
 return arow.idd;
}
}
 return null;
}

 
 
dhtmlXTabBar.prototype.enableAutoSize=function(autoWidth,autoHeight){
 this._ahdj=convertStringToBoolean(autoHeight);
 this._awdj=convertStringToBoolean(autoWidth);
}


 
dhtmlXTabBar.prototype.enableAutoReSize=function(mode)
{
    if(convertStringToBoolean(mode))
    {
        var self=this;
        if(this.entBox.addEventListener)
        {
            if((_isFF)&&(_FFrv<1.8))
                window.addEventListener("resize",function() {window.setTimeout(function(){self.adjustOuterSize();},10);},false);
            else
                window.addEventListener("resize",function(){if(self.adjustOuterSize)self.adjustOuterSize();},false);
        }
        else if(window.attachEvent)
            window.attachEvent("onresize",function(){ if(self._resize_timer)window.clearTimeout(self._resize_timer); if(self.adjustOuterSize) self._resize_timer=window.setTimeout(function(){self.adjustOuterSize();},500);});

    }
}

 
dhtmlXTabBar.prototype.setSize=function(width,height,contentZone)
{
    
    this.scrl_r.style.left = width - 17 + "px";
    this.scrl_l.style.left = width - 34 + "px";
    this.scrl_line.style.left = width - this._scrl_line_offset + "px";
    
    
 height=parseInt(height);
 width=parseInt(width);
 if(contentZone){
 
 if(!this._vMode)
 height+=20;
 else
 
 width+=20;
}

 this.height=height+"px";
 this.width=width+"px";

 this._lineA.style[this._vMode?"left":"top"]=(this._bMode?0:(this._height+2))+"px";
 this._lineA.style[this._vMode?"height":"width"]=this[this._vMode?"height":"width"];

 
 if(this._vMode){
 for(var i=0;i<this.rows.length;i++)
 this.rows[i].style.height=parseInt(this.height)+"px";

 this._conZone.style.height=height;
 }
 else
 {
    this._conZone.style.width=parseInt(this.width)-(_isFF?2:0)+"px";
    for(var i=0;i<this.rows.length;i++)
        this.rows[i].style.width=parseInt(this.width) - this._scrl_line_offset +"px";
 }
 for(var i=0;i<this.rows.length;i++)
 this._redrawRow(this.rows[i]);
 this._setSizes();
 this._checkScrollA();
 this._checkScrollD();
}

dhtmlXTabBar.prototype.adjustOuterSize=function()
{
    this.setSize(this.entBox.offsetWidth,this.entBox.offsetHeight,false);
}

dhtmlXTabBar.prototype.adjustSize=function()
{
    var flag=false;var x=this._conZone.offsetWidth;var y=this._conZone.offsetHeight;
    if((this._ahdj)&&(this._conZone.scrollHeight>this._conZone.offsetHeight))
    {
        y=this._conZone.scrollHeight;
        flag=true;
    }

    if((this._awdj)&&(this._conZone.scrollWidth>this._conZone.offsetWidth))
    {
        x=this._conZone.scrollWidth;
        flag=true;
    }
    if(flag)
        this.setSize(x,y,true);
}
 

 
dhtmlXTabBar.prototype.preventIECashing=function(mode){
 this.no_cashe = convertStringToBoolean(mode);
 if(this.XMLLoader)this.XMLLoader.rSeed=this.no_cashe;
}


 
dhtmlXTabBar.prototype.hideTab = function(tab,mode){
 var tab=this.tabsId[tab];
 if(!tab)return;
 this._goToAny(tab,mode);
 tab.style.display='none';
 var row=tab.parentNode;
 this._redrawRow(row);
}

 
dhtmlXTabBar.prototype.showTab = function(tab){
 var tab=this.tabsId[tab];
 if(!tab)return;
 tab.style.display='block';
 var row=tab.parentNode;
 this._redrawRow(row)
}

 
dhtmlXTabBar.prototype.enableTab = function(tab){
 var tab=this.tabsId[tab];
 if(!tab)return;
 tab._disabled=false;
 tab.className=(tab.className||"").replace(/dhx_tab_element_disabled/g,"");
}

 
dhtmlXTabBar.prototype.disableTab = function(tab,mode){
 var tab=this.tabsId[tab];
 if(!tab)return;
 this._goToAny(tab,mode);
 tab._disabled=true;
 tab.className+=" dhx_tab_element_disabled";
}

 
dhtmlXTabBar.prototype.setLabel = function(tab,value){
 var tab=this.tabsId[tab];
 if(!tab)return;
 switch(this._tbst){
 case 'text':
 tab.innerHTML=value;
 break;
 case 'win_text':
 tab.childNodes[2].childNodes[0].innerHTML=value;
 break;
}
}

 
dhtmlXTabBar.prototype.getLabel = function(tab){
 var tab=this.tabsId[tab];
 if(!tab)return;
 switch(this._tbst){
 case 'text':
 return tab.innerHTML;
 break;
 case 'win_text':
 return tab.childNodes[2].childNodes[0].innerHTML;
 break;
}
}

dhtmlXTabBar.prototype.detachTab=function(id){
 var WindowName = this.getLabel(id);
 var tab=this.tabsId[id];
 if(!tab)return;
 var node = this._content[tab.idd];
 tab = this._getTabById(id);
 var tab_w = parseInt(tab.style.width);
 this.removeTab(id,true);
 node.style.position = '';
 var width = parseInt(this._conZone.style.width)+5;
 var height = parseInt(this._conZone.style.height)+25;
 var min_width = 100;
 var min_height = 50;
 width = width<min_width?min_width:width;
 height = height<min_height?min_height:height;

 var top = Math.ceil(window.offsetHeight/20-height/20);
 var left = Math.ceil(window.offsetWidth/20-width/20);

 var win = new dhtmlxWindow(420,300,width,height,WindowName,false);
 win._tab_w = tab_w;
 win.attachContent(node);
 return win;
}


// 判断所有标签页是否在可视范围内，若不在可视范围内则显示滚动按钮，若在可视范围内并且第一个标签页脱离负区域则隐藏滚动按钮
dhtmlXTabBar.prototype._checkScrollA = function()
{
    var length = this._offset;
    for(var n = 0; n < maxnum; n++)
    {
         if(this.tabsId[n])	
	        length += this.tabsId[n].style.pixelWidth;
	}
	if(length > this.rows[0].style.pixelWidth)
	{
	    this.scrl_l.style.visibility = "visible";
	    this.scrl_r.style.visibility = "visible";
	    this._inRange = false;
	}
	else
	{
	    this._inRange = true;
	    if(this._inRange && this._checkScrollB())
	    {
	        this.scrl_l.style.visibility = "hidden";
	        this.scrl_r.style.visibility = "hidden";
	    }
	}
}

// 判断第一个标签页是否脱离负区域，若脱离则左滚动按钮无效，并且若所有标签页都在可视范围内则隐藏滚动按钮
dhtmlXTabBar.prototype._checkScrollB = function()
{
    var min = 0;
    for(var n = 0; n < maxnum; n++)
    {
        if(this.tabsId[n])
        {
            min = this.tabsId[n].style.pixelLeft;
            break;
        }
    }
    
    if(min > 0)
    {
        if(this._inRange)
        {
            this.scrl_l.style.visibility = "hidden";
	        this.scrl_r.style.visibility = "hidden";
        }
        return true;
    }
    else
    {
        return false;
    }
}

// 判断第一个标签页是否脱离正区域，若脱离则右滚动按钮无效
dhtmlXTabBar.prototype._checkScrollC = function()
{
    var max = 9999;
    for(var n = maxnum - 1; n >= 0; n--)
    {
        if(this.tabsId[n])
        {
            max = this.tabsId[n].style.pixelLeft + this.tabsId[n].style.pixelWidth;
            break;
        }
    }
    
    if(max < this.rows[0].style.pixelLeft + this.rows[0].style.pixelWidth)
    {
//        if(this._inRange)
//        {
//            this.scrl_l.style.visibility = "hidden";
//	        this.scrl_r.style.visibility = "hidden";
//        }
        return true;
    }
    else
    {
        return false;
    }
}

// 检查当前激活标签是否在可视范围内，若在范围外则将其移动到可视范围内
dhtmlXTabBar.prototype._checkScrollD = function()
{
    var l = this._lastActive.style.pixelLeft;
    var r = l + this._lastActive.style.pixelWidth;
    var range_r = this.rows[0].style.pixelLeft + this.rows[0].style.pixelWidth;
      
    while(l < 0)
    {
        this.scrollLeftClick();
        l = this._lastActive.style.pixelLeft;
    }
    while(r > range_r)
    {
        this.scrollRightClick();
        r = this._lastActive.style.pixelLeft + this._lastActive.style.pixelWidth;
    }
}



