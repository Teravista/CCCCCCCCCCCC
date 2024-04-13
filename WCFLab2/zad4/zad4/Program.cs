using KSR_WCF2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace zad4
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class Zadanie4 : IZadanie4
    {
        int liczba = 0;
        public int Dodaj(int v)
        {
            liczba += v;
            return liczba;
        }

        public void Ustaw(int v)
        {
            liczba = v;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var h = new ServiceHost(typeof(Zadanie4));
            h.AddServiceEndpoint(typeof(IZadanie4),
new NetNamedPipeBinding(),
"net.pipe://localhost/ksr-wcf2-zad4");
            h.Open();
            Console.ReadKey();
            h.Close();
            return;
                
        }
    }
}
