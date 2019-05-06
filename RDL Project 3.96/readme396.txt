The 3.9.6 development build includes early code for the following features and bug fixes: 

[list][b]Silverlight Viewer [/b]  This is an early proof of concept of a Silverlight report viewer.  It will largely be rewritten in the next few builds so don't bother reporting bugs.  However, suggestions on how it should work are very welcome. To try it, start an ASP web server using the ReportServer directory and bring up the SilverlightTest.aspx page.  [/list]
[list][b]Bug Fix [/b] RdlViewer problem when trying to pass '&' character in a parameter value.  The fix requires you to substitute &amp; in place of any '&' you want.   Thanks to khgamester of forum for reporting this.
[/list]
[list][b]Bug Fix [/b] New report dialog.  Tables not always sorted; problems when table not qualified by table schema with SQL Server.[/list]
[list][b]Bug Fix [/b] Null object exception occasionally when changing text color property in dialog (thanks to kobruleht for reporting)[/list]
[list][b]Bug Fix [/b] Excel formatting problem.  Reported and fixed by adamminc![/list]
[list][b]Bug Fix [/b] Thanks to kobruleht for reporting the following bugs:  Edit Expression dialog doesn't handle Esc key.  Exception saving report to HTML when Rectangle embedded in TableCell.  Change to designer to do Application.Exit instead of Environment.Exit.[/list]
	
	
Special thankgs to Peter Rohner, Adam Minchenton and Gil Little for the following significant changes and improvements.  	

[list][b]Chart [/b] Two vertical axis on charts (including auto scaling ranges so they match)[/list]
[list][b]Chart [/b] Omitting null values in a series (i.e. broken lines where no data exists)[/list]
[list][b]Chart [/b] Label grouping by month on x axis while including points for each day of month[/list]
[list][b]Chart [/b] Limit lines on XY scatter chart.[/list]
[list][b]Chart [/b] Additional palettes for rendering charts so they are legible when printed on a BW printer.[/list]
[list][b]ASP support [/b] NoShow property allows report parameters to be shown without actually running the report.[/list]	
[list][b]ASP support [/b] Better parameter support: numeric fields are checked by javascript to ensure valid number; datetime parameters now have a datepicker.   These changes require three additional files: SmallCalendar.gif, datetimepicker.js and limitinput.js[/list]
[list][b]RdlCmd [/b] Added support for xlsx, tiff, … file types.[/list]
[list][b]RdlViewer [/b] Wait dialog when rendering report.   Property ShowWaitDialog controls whether this is shown or not.  Default is true.
[/list]
