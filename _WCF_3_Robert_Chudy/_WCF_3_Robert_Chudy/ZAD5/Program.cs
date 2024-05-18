using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ZAD5
{
    [ServiceContract]
    public interface IZadanie3
    {
        [OperationContract]
        [WebGet(UriTemplate = "index.html")]
        [XmlSerializerFormat]
        XmlDocument GetHtml();

        [OperationContract]
        [WebInvoke(UriTemplate = "Dodaj/{a}/{b}")]
        int Dodaj(string a, string b);

        [OperationContract]
        [WebGet(UriTemplate = "scripts.js")]
        Stream GetScript();
    }
    class Program
    {
        static void Main(string[] args)
        {
            var f = new ChannelFactory<IZadanie3>(
                new WebHttpBinding(),
                new EndpointAddress("http://localhost:52754/Service1.svc/ZAD3"));
            f.Endpoint.Behaviors.Add(new WebHttpBehavior());
            var c = f.CreateChannel();

            Console.WriteLine("Wynik dodawania 55 i 16: "+c.Dodaj("55", "16"));
            ((IDisposable)c).Dispose();
            f.Close();
            Console.ReadKey();
        }
    }
}
