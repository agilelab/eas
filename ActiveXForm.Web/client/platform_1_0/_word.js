//
// iAlignment - WdParagraphAlignment : 
//					wdAlignParagraphCenter		- 1,
//					wdAlignParagraphDistribute	- 4,
//					wdAlignParagraphJustify		- 3,
//					wdAlignParagraphJustifyHi	- 7,
//					wdAlignParagraphJustifyLow	- 8,
//					wdAlignParagraphJustifyMed	- 5,
//					wdAlignParagraphLeft		- 0,
//					wdAlignParagraphRight		- 2.
//

function validateAlignment(iAlignment)
{
	return (iAlignment < 0 || iAlignment == 6 || iAlignment > 8) ? 0 : iAlignment;
}

function createWordDocument(sHeader,sSeal)
{
	try
	{
		var WordApp = new ActiveXObject("Word.Application");
		WordApp.Visible = true;
		WordApp.Activate();
		var WordDoc = WordApp.Documents.Add();
		var pageWidth = WordDoc.PageSetup.PageWidth;
		var pageHeight = WordDoc.PageSetup.PageHeight;
		var header = WordDoc.Shapes.AddPicture(sHeader);
		var seal = WordDoc.Shapes.AddPicture(sSeal);
		seal.Left = (pageWidth - seal.Width) / 2;
		seal.Top = (pageHeight - seal.Height) / 2;
	}
	catch(e)
	{
		alert("无法创建ActiveX控件或者不能启动Word，您的IE安全设置不支持创建ActiveX控件或者没有正确地安装Microsoft Word。");
	}
}

function insertSH(sWord,sHeader,sSeal)
{
	try
	{
		var WordApp = new ActiveXObject("Word.Application");
		WordApp.Visible = true;
		WordApp.Activate();
		var WordDoc = WordApp.Documents.Open(sWord);
		var pageWidth = WordDoc.PageSetup.PageWidth;
		var pageHeight = WordDoc.PageSetup.PageHeight;
		var header = WordDoc.Shapes.AddPicture(sHeader);
		var seal = WordDoc.Shapes.AddPicture(sSeal);
		seal.Left = (pageWidth - seal.Width) / 2;
		seal.Top = (pageHeight - seal.Height) / 2;
	}
	catch(e)
	{
		alert("无法创建ActiveX控件或者不能启动Word，您的IE安全设置不支持创建ActiveX控件或者没有正确地安装Microsoft Word。");
	}
}

function changeSelect()
{
	var ddl = document.all.ddlDepartments;
	if(ddl.selectedIndex < 0)
		return;
	var v = ddl.options(ddl.selectedIndex).value.split(';');
	document.all.HyperLinkDocHeader.href = v[0];
	document.all.HyperLinkDocSeal.href = v[1];
}

function isFileExists(sFilePathName)
{
	try
	{
		return new ActiveXObject("Scripting.FileSystemObject").FileExists(sFilePathName);
	}
	catch(e)
	{
		alert("无法创建ActiveX控件，您的IE安全设置不支持创建ActiveX控件。");
		return false;
	}
}

function isWordDoc(sFile)
{
	if(sFile == null || sFile == "" && sFile.length < 4)
			return false;
	var len = sFile.length;
	var _sFile = sFile.toLowerCase();
	if(_sFile.charAt(len - 1) == 'c' && _sFile.charAt(len - 2) == 'o' && _sFile.charAt(len - 3) == 'd' && _sFile.charAt(len - 4) == '.')
		return true;
	return false;
}