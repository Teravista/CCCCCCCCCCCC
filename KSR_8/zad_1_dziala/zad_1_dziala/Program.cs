using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Komunikaty;


namespace Komunikaty
{


    internal class Program
    {
        public static Task Handle(ConsumeContext<Komunikaty.Komunikat> ctx)
        {
            return Console.Out.WriteLineAsync($"received: {ctx.Message.tekst}");
        }
        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc => {
                var host = sbc.Host(new Uri("rabbitmq://localhost"),
                h => { h.Username("guest"); h.Password("guest"); });
                sbc.ReceiveEndpoint(host, "recvqueue", ep => {
                    ep.Handler<Komunikaty.Komunikat>(Handle);
                });
            });
            bus.Start();
            Console.WriteLine("odbiorca wystartował");
            Console.ReadKey();
            bus.Stop();

        }
    }
}
