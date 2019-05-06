The 3.9.5 development build includes early code for the following features and bug fixes: 

[list][b]ASP [/b]  PassPhrase property added to RdlReport.  Used for decrypting shared datasources.  Set this property prior to setting the report.[/list]
[list][b]RdlEngine Code [/b] Donated by solidstore from the forum.  Enhancement that allows Code block to have functions like Public Function GetLang() as String
   Return Report.User.Language
End Function
This change can cause a VB compilation error if you have CodeModules defined that can't be located.[/list]
[list][b]MemoryStreamGen [/b] Change by jonh of forum to allow class to be used with CSV[/list]
[list][b]Designer [/b] right mouse support on current designer tab with "Save", "Close", "Close All But This" menu items[/list]
[list][b]Viewer [/b] New event triggered when subreport needs to retrieve data.  RdlTests has example.            this.rdlViewer.SubreportDataRetrieval +=new EventHandler<SubreportDataRetrievalEventArgs>(rdlViewer_SubreportDataRetrieval);        void rdlViewer_SubreportDataRetrieval(object sender, SubreportDataRetrievalEventArgs e)
        {
            int ids = 0;
            foreach (fyiReporting.RDL.DataSet ds in e.Report.DataSets)
                ids++;
            MessageBox.Show(string.Format(""Subreport Data Retrieval: {0} datasets"", ids));
        }

[/list]
[list][b]Bug Fix [/b] RTF export allow non ansis chars for normal textbox but not for html formated textboxs.  Thanks to hugo on the forum for this fix.[/list]
[list][b]Bug Fix [/b] Parser problem with handling '.' notation after a Fields array reference.  E.g. =IIf(Parameters!pSortOrder.Value = "0", lds(Parameters!pSortOn.Value).Value, 0)    Thanks to solidstore on the forum for reporting this problem.[/list]
[list][b]Bug Fix [/b] drilldown failing;  thanks to dbeaugez on the forum for finding this problem[/list]
[list][b]Bug Fix [/b] DialogNewDatabase: Globals!PageNumber.Value changed to Globals!PageNumber (thanks to geezzeer on the forum for reporting a number of incompatibilities with designer RDL syntax generation and MS reporting services usage)[/list]
[list][b]Bug Fix [/b] HTML formatting:  Code donated by jonh of forum.    HTML Comments won't be rendered to the report with this fix.   Also fix for multiple &nbsp; support.[/list]

