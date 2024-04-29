using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using System.Text;
using System.Threading.Tasks;

namespace ZAD2
{
    [ServiceContract]
    public interface IZadanie1
    {
        [OperationContract]
        string ScalNapisy(string a, string b);
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var discoveryClient = new DiscoveryClient(
                new UdpDiscoveryEndpoint("soap.udp://localhost:30703"));
            System.Collections.ObjectModel.Collection<EndpointDiscoveryMetadata> lst = discoveryClient.Find(new FindCriteria(typeof(IZadanie1))).Endpoints;
            discoveryClient.Close();

            if (lst.Count > 0)
            {
                var addr = lst[0].Address; // łączymy się z pierwszym znalezionym
                var proxy = ChannelFactory<IZadanie1>
                    .CreateChannel(new NetNamedPipeBinding(), addr);
                Console.WriteLine(proxy.ScalNapisy("Klient zad2", " dziala"));
                Console.ReadKey();
                ((IDisposable)proxy).Dispose();
            }
        }
    }
}
