using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace fyiReporting.RdlDesign
{
    static class AppStart
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.DoEvents();

            Application.Run(new MDIDesigner());
        }
    }
}