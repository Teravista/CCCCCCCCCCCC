using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zad7.ServiceReference1;
using System.ServiceModel;
namespace zad7
{
    public class Handler7 : IZadanie6Callback
    {
        public void Wynik(int wyn)
        {
            Console.WriteLine("wynik:+ " + wyn);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var client5 = new Zadanie5Client();
            Console.WriteLine(
                client5.ScalNapisy(
                    client5.ScalNapisy(
                        client5.ScalNapisy("Prosze ", "niech "), "to "), "działa"));
            ((IDisposable)client5).Dispose();

            var client6 = new Zadanie6Client(new InstanceContext(new Handler7()));
            client6.Dodaj(1, 11111);
            Console.ReadKey();
            ((IDisposable)client6).Dispose();
            return;

        }
    }
}
