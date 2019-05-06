using System;
using System.Collections.Generic;
using System.Text;

namespace fyiReporting.RdlDesign
{
    class RDLHelper
    {
        static public readonly string[] ZoomListString = new string[] { "Actual Size", "Fit Page", "Fit Width", "800%", "400%", "200%", "150%", "125%", "100%", "75%", "50%", "25%" };

        static public readonly float[] ZoomList = new float[] { 8f, 4f, 2f, 1.5f, 1.0f, 0.75f, 0.5f, 0.25f };
    }
}
