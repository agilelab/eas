The 3.9.3 development build includes early code for the following features and bug fixes: 

[list][b]HTML Format [/b]<expr>=Fields!col1.Value</expr> support in html formatted expressions;  One potential user is as a simple way to create formatted letters while embedding expressions (mail merge).  For example: <b>Company:</b> <expr>=Fields!CompanyName.Value</expr><br>
 Contact: <expr>=Fields!ContactName.Value</expr><hr/> and what is the total
<expr>=Count(Fields!ContactName.Value)</expr> <br>the end[/list]
[list][b]Background images [/b]Support repeat for background images.  See   http://www.fyireporting.com/forum/viewtopic.php?p=1640#1640  Thanks to Duc Phan for the PDF version of this code which I adapted to work with the RdlViewer and designer.[/list]
[list][b]RdlViewer selection [/b]A new selection tool for the viewer is added along with copy support.  In the designer when previewing a report clicking on Selection Tool (+)  puts viewer into selection mode.  The user can then use a selection rectangle to select report items and use Edit/Copy to place contents into clipboard.  If a single report item is an image or chart then an Image is placed on the clipboard; otherwise the text of the report items is placed on the clipboard. Copied text is created as tab and newline delimited.  This is determined by the following simple logic: if a report item has same y coordinate then place tab before it other wise use newline.   HTML formatted text is stripped of all formatting when copied onto clipboard.  Note: the entire text of a report item is selected: this is NOT textual selection.
[/list]
[list][b]Bug [/b]Charts don't show in Firefox browser from ASP server[/list]

Thanks to Peter Rohner, Adam Minchenton, Gil Little of Hyne for the following changes:
[list][b]Charts [/b]chart static series support as well as dynamic determination of horizontal or vertical text depending on text size when no Style attributes are deted.[/list]
[list][b] [/b]High Definition JPEG encoding[/list]
