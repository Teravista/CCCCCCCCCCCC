using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using KSR_WCF2;

namespace ZAD3
{
    public class Zadanie3 : IZadanie3
    {
        public void TestujZwrotny()
        {
            var zwr = OperationContext.Current.GetCallbackChannel<IZadanie3Zwrotny>();
            for (int i =0;i<=30;i++)
            {
                zwr.WolanieZwrotne(i,i*i*(i-1));
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var h = new ServiceHost(typeof(Zadanie3));
            h.AddServiceEndpoint(typeof(IZadanie3),
new NetNamedPipeBinding(),
"net.pipe://localhost/ksr-wcf2-zad3");
            h.Open();
            Console.ReadKey();
            h.Close();
            return;

        }
    }
}
