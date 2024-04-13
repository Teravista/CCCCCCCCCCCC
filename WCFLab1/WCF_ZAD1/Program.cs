using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSR_WCF1;
using System.ServiceModel;
using System.Linq.Expressions;

namespace WCF_ZAD1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var client7 = new ServiceReference3.Zadanie7Client();
            try
            {
                client7.RzucWyjatek7("Bebe", 188);
            }
            catch (FaultException<ServiceReference3.Wyjatek7> ex)
            {
                Console.WriteLine("Detail: "+ex.Detail);
                Console.WriteLine("opsi + a + b: " + ex.Detail.opis + " " + ex.Detail.a + " " + ex.Detail.b);

            }
            ((IDisposable)client7).Dispose();



            var fact = new ChannelFactory<IZadanie1>(new NetNamedPipeBinding(),
                 new EndpointAddress("net.pipe://localhost/ksr-wcf1-test"));
            var client = fact.CreateChannel();
            Console.WriteLine(client.Test("BBEEEBE"));
            try
            {
                client.RzucWyjatek(true);
            }
            catch (FaultException<Wyjatek> c)
            {
                Console.WriteLine(client.OtoMagia(c.Detail.magia));
                Console.WriteLine(client.Test(c.Message.ToString()));
            }
            ((IDisposable)client).Dispose();
            fact.Close();
            Console.ReadKey();
        }
    }
}
