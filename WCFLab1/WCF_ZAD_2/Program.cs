using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSR_WCF1;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace WCF_ZAD_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = new ServiceHost(typeof(Zadanie2));

            var b = host.Description.Behaviors.Find<ServiceMetadataBehavior>();
            if (b == null) b = new ServiceMetadataBehavior();
            host.Description.Behaviors.Add(b);

            host.AddServiceEndpoint(typeof(IZadanie2), new NetNamedPipeBinding(), "net.pipe://localhost/ksr-wcf1-zad2");
            host.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, 
                MetadataExchangeBindings.CreateMexNamedPipeBinding(), "net.pipe://localhost/metadane");
            host.AddServiceEndpoint(typeof(IZadanie2), new NetTcpBinding(), "net.tcp://127.0.0.1:55765");


            var host2 = new ServiceHost(typeof(Zadanie7));
            host2.AddServiceEndpoint(typeof(IZadanie7), new NetNamedPipeBinding(), "net.pipe://localhost/ksr-wcf1-zad7");
            var b2 = host2.Description.Behaviors.Find<ServiceMetadataBehavior>();
            if (b2 == null) b2 = new ServiceMetadataBehavior();
            host2.Description.Behaviors.Add(b2);
            host2.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName,
    MetadataExchangeBindings.CreateMexNamedPipeBinding(), "net.pipe://localhost/metadane2");


            host.Open();
            host2.Open();
            Console.ReadKey();
            host.Close();
            host2.Close();
        }
    }
}
