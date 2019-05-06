The 3.9.4 development build includes early code for the following features and bug fixes: 

[list][b]TIFF render [/b]New renderer for TIF files. Thanks to samuelchoi on the forum for this code.  The renderer is capable of producing either black and white TIFs or full color.  A type of "tif" results in color; "tifbw" results in black and white.   There is a significant size difference between the two.  In a small 3 page report the color tif was 20 times larger than the black and white tif.[/list]
[list][b]Excel render [/b]New renderer for Excel 2007 file format (extension xlsx).  (Sorry, no support for earlier versions.)   Renderer support only table, matrix and Textbox report items at this time.  A new workbook sheet is created for each Table and Matrix in the report.  Lists are converted to tabular format when rendered (ie Textbox positioning is lost).[/list]
[list][b]Viewer [/b]Selection tool: when ctrl key is pressed deselect previously selected items on reselection[/list]
[list][b]Designer [/b]Add highlighting when mouse enters the fx botton on the toolbar (clicking brings up expression editor).[/list]
[list][b]Bug fix [/b]HTML generation of CSS sizes should remove blanks.   Thanks to dbeaugez on the forum for this fix.[/list]
[list][b]BUg fix [/b]Textbox formatted with "html" containing character codes > 128 don't display properly.  Thanks to Hugo Ferreira for pointing this problem out to me.[/list]


