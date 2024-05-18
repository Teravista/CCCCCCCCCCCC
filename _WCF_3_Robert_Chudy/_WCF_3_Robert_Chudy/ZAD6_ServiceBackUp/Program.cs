using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ZAD6_ServiceBackUp
{
    [ServiceContract]
    public interface IZadanie6
    {
        [OperationContract]
        int Dodaj(int a, int b);
    }
    public class Zadanie6 : IZadanie6
    {
        public int Dodaj(int a, int b)
        {
            Console.WriteLine("Korzysta z Servicu Backup. wartość liczona: "+(a+b));
            return a + b;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = new ServiceHost(typeof(Zadanie6));
            host.AddServiceEndpoint(
                typeof(IZadanie6),
                new NetNamedPipeBinding(),
                "net.pipe://localhost/zadanie6_Service_BackUp");

            host.Open();
            Console.ReadKey();
            host.Close();
        }
    }
}
