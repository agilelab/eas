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
using System.Windows.Resources;
using EAS.Explorer;
using System.IO;

namespace EAS.SilverlightClient
{
    class ResourceLoader
    {
        public string Xap {get;set;}
        public string Dll { get; set; }
        public string Type { get; set; }

        public void Load(string info)
        {
            if (info.Length == 0)
            {
                return;
            }

            string [] sv = info.Split(',');
            this.Xap = sv[0];
            this.Dll = sv[1];
            this.Type = sv[2];

            Uri url = new Uri(System.Windows.Application.Current.Host.Source, "slupdate.xml");            

            WebClient client = new WebClient();
            System.Threading.Tasks.Task<Stream> task = client.OpenReadTaskAsync(url);
            task.Wait();

            if (task.Exception != null)
            {
                throw task.Exception;
            }

            StreamResourceInfo xapStream = new StreamResourceInfo(task.Result, null);
            EAS.Objects.ClassProvider.LoadXap(xapStream);
            StreamResourceInfo dllStream = App.GetResourceStream(xapStream, new Uri(this.Dll, UriKind.Relative));
            System.Reflection.Assembly assembly = new AssemblyPart().Load(dllStream.Stream);
            SLContext.Instance.ShellResource = assembly.CreateInstance(this.Type) as IResource;
        }
    }
}
