using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace EAS.Demo.Res
{
    public class Resources : EAS.Explorer.IResource
    {
        public string GetApplicationName()
        {
            return "AgileEAS.NET";
        }

        public string GetApplicationTitle()
        {
            return "AgileEAS.NET应用开发平台";
        }

        public object GetLoginForm()
        {
            return new LoginPage();
        }

        public object GetStartModule()
        {
            return new StartWF();
        }

        public object GetNavigationControl()
        {
            throw new NotImplementedException();
        }

        public object GetBannerControl()
        {
            throw new NotImplementedException();
        }

        public object GetBottomControl()
        {
            throw new NotImplementedException();
        }

        public object GetAboutForm()
        {
            throw new NotImplementedException();
        }
    }
}

