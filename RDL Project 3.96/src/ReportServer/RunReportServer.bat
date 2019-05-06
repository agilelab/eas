copy "C:\fyiReporting\RDL Project 4\src\RdlAsp\bin\Debug\*.*" "C:\fyiReporting\RDL Project 4\src\ReportServer\bin\*.*" 
copy "C:\fyiReporting\RDL Project 4\src\RdlEngine\bin\Debug\*.*" "C:\fyiReporting\RDL Project 4\src\ReportServer\bin\*.*" 
copy "C:\fyiReporting\RDL Project 4\src\RdlEngine\RdlEngineConfig.xml" "C:\fyiReporting\RDL Project 4\src\ReportServer\bin\*.*" 

"C:\Windows\Microsoft.NET\Framework\v2.0.50727\WebDev.WebServer.EXE" /port:8080 /path:"C:\fyiReporting\RDL Project 4\src\ReportServer"