using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace zadanie_1
{
    internal class Program
    {
        public static Task Handle(ConsumeContext<Komunikaty.Komunikat> ctx)
        {
            return Console.Out.WriteLineAsync($"received: {ctx.Message.tekst}");
        }
        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(50);
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc => {
                var host = sbc.Host(new Uri("rabbitmq://localhost"),
                h => { h.Username("guest"); h.Password("guest"); });
                //sbc.ReceiveEndpoint(host, "recvqueue", ep => {
                //    ep.Handler<Komunikaty.Komunikat>(Handle);
                //});
            });
            bus.Start();
            Console.WriteLine("nadawca wystartował");
            // tworzymy i startujemy szynę jak wyżej, podając inną nazwę kolejki
            bus.Publish(new Komunikaty.Komunikat() { tekst = "asd" });

            Console.ReadKey();
            bus.Stop();
        }

    }
}
