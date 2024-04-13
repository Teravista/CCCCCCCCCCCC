using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
namespace zad2
{
    internal class Program
    {
        class Handler : ServiceReference1.IZadanie2Callback
        {
            public void Zadanie([MessageParameter(Name = "zadanie")] string zadanie1, int pkt, bool zaliczone)
            {
                Console.WriteLine(zadanie1 + " " + pkt + " " + zaliczone);    
            }
        }


        static void Main(string[] args)
        {
            var c = new ServiceReference1.Zadanie2Client(new InstanceContext(new Handler()));
            c.PodajZadania();
            Console.ReadKey();
            ((IDisposable)c).Dispose();
        }
    }
}
