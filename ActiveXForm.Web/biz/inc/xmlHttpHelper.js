function XmlHttpHelper(){}

XmlHttpHelper.__getXmlHttpObj = function()
{
	try
	{
		return new ActiveXObject("MSXML2.XMLHTTP");
	}
	catch(e)
	{
		try
		{
			return new XMLHttpRequest();
		}
		catch(ee)
		{
			throw(new Error(-1, "无法创建XMLHTTP对象。"));
		}
	}
};

//
//  使用XMLHTTP和远程服务器通信。
//
//	async			是否为异步方式：true/false
//	httpMethod		http方法："post"/"get"
//	responseType	返回数据的类型："text"/"xml"/null
//	url				请求的URL地址
//	callback		异步操作完成时执行的回调函数
//	postData		post方式时发送的数据
//
XmlHttpHelper.transmit = function(async, httpMethod, responseType, url, callback, postData)
{
    var xmlhttp = this.__getXmlHttpObj();
	xmlhttp.open(httpMethod, url, async);
	
	if(!async && httpMethod.toLowerCase() == "post")
	{
		xmlhttp.setRequestHeader('Content-Length', postData.length);
		xmlhttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
	}

	if(async)
	{
		xmlhttp.onreadystatechange = function()
		{
			if(xmlhttp.readyState == 4)
			{
			    try
			    {
			        if(responseType != null)
			        {
				        if(responseType.toLowerCase() == "text")
					        callback(xmlhttp.responseText);
				        else if(responseType.toLowerCase() == "xml")
					        callback(xmlhttp.responseXML);
			        }
			        else
			        {
					    callback(null);
				    }
				}
				catch(e)
				{
				    xmlhttp = null;
				}
			}
		}
		xmlhttp.send(postData);
	}
	else
	{
		xmlhttp.send(postData);
		if(xmlhttp.status == 200)
		{
		    if(responseType != null)
		    {
			    if(responseType.toLowerCase() == "text")
				    return xmlhttp.responseText;
			    else if(responseType.toLowerCase() == "xml")
				    return xmlhttp.responseXML;
		    }
			else
			{
				return null;
		    }
		}
		return null;
	}
};