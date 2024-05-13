using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace Zadanie_4
{
    internal class Program
    {
        public static Task Handle(ConsumeContext<Komunikaty.Komunikat1> ctx)
        {
            foreach (var hdr in ctx.Headers.GetAll())
            {
                Console.Out.WriteLineAsync($"{hdr.Key}: {hdr.Value}");
            }

            return Console.Out.WriteLineAsync($"received: {ctx.Message.tekst}");

        }
        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc => {
                var host = sbc.Host(new Uri("rabbitmq://localhost"),
                h => { h.Username("guest"); h.Password("guest"); });
                sbc.ReceiveEndpoint(host, "recvqueuea", ep => {
                    ep.Handler<Komunikaty.Komunikat1>(Handle);
                });
            });
            bus.Start();
            Console.WriteLine("odbiorca AA wystartował");
            Console.ReadKey();
            bus.Stop();
        }

    }
}
