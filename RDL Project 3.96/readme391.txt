The 3.9.1 development build includes early code for the following features and bug fixes: 

[list][b]Designer Property sheets[/b]Added ReportItem name drop down list to property sheet[/list]
[list][b]Designer SQL editting[/b]SQL editor supports resizing table/view list; both in New Report and DataSet SQL editing[/list]
[list][b]Designer bug fix[/b]Globals!ExecutionTime.Value pasted in not Globals!ExecutionTime[/list]
[list][b]Designer bug fix[/b]Pasting in new reportitem sometimes generates duplicate name;  names not case insensitive[/list]
[list][b]Runtime bug fix[/b]nullable datetime parsing: see http://www.fyireporting.com/forum/viewtopic.php?p=1454#1454[/list]
[list][b]Runtime VB compatibility[/b]Added IsNumeric() function-- thanks to Gareth Marshall[/list]
[list][b]Runtime PDF generation[/b]text with many blank spaces may misformat during PDF generation[/list]
[list][b]Designer[/b]Some minor interprocess communication (ipc) capabilities:  current use is to open files in the existing running designer instead of starting up a new instance of the program (as was previously done).  IPC functionality could be expanded.[/list]
[list][b]Designer bug[/b]Missing Dispose when using dialogs (thanks to Lionel Cuir of Aulofee for pointing this out)[/list]
[list][b]Runtime parsing[/b]=1=2-- doesn't parse but should return a boolean[/list]
[list][b]Runtime parsing[/b]Exception when Code element contains braces {}[/list]
	

