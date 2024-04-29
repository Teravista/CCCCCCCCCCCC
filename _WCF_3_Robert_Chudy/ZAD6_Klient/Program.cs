using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ZAD6_Klient
{
    [ServiceContract]
    public interface IZadanie6
    {
        [OperationContract]
        int Dodaj(int a, int b);
        
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var fabryka = new ChannelFactory<IZadanie6>(
                new NetNamedPipeBinding(),
                new EndpointAddress("net.pipe://localhost/router"));
            var klient = fabryka.CreateChannel();
            Console.WriteLine(klient.Dodaj(555, 666));
            Console.ReadKey();
            ((IDisposable)klient).Dispose();
            fabryka.Close();
        }
    }
}
