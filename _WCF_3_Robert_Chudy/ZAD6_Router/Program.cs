using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Routing;
using System.Text;
using System.Threading.Tasks;

namespace ZAD6_Router
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var routePath1 = "net.pipe://localhost/zadanie6_Service_Main";
            var routePath2 = "net.pipe://localhost/zadanie6_Service_BackUp";
            var routeAdres = "net.pipe://localhost/router";

            var host = new ServiceHost(typeof(RoutingService));
            host.AddServiceEndpoint(
                typeof(IRequestReplyRouter),
                new NetNamedPipeBinding(),
                routeAdres);

            var routeConfig = new RoutingConfiguration();
            var contract = ContractDescription.GetContract(typeof(IRequestReplyRouter));
            var k1 = new ServiceEndpoint(
                contract,
                new NetNamedPipeBinding(),
                new EndpointAddress(routePath1));
            var k2 = new ServiceEndpoint(
                contract,
                new NetNamedPipeBinding(),
                new EndpointAddress(routePath2));

            var list = new List<ServiceEndpoint>();
            list.Add(k1);
            list.Add(k2);

            routeConfig.FilterTable.Add(new MatchAllMessageFilter(), list);
            host.Description.Behaviors.Add(new RoutingBehavior(routeConfig));

            host.Open();
            Console.ReadKey();
            host.Close();
        }
    }
}
