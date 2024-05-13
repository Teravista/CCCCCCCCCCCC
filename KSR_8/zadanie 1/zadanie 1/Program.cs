using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace zadanie_1
{
    internal class Program
    {
        class HandlerClass : IConsumer<Komunikaty.komunikat0>
        {
            private int mescount = 0;
            public Task Consume(ConsumeContext<Komunikaty.komunikat0> ctx)
            {
                foreach (var hdr in ctx.Headers.GetAll())
                {
                    Console.Out.WriteLineAsync($"{hdr.Key}: {hdr.Value}");
                }
                    Console.Out.WriteLineAsync($"messcount :: : {mescount++}");
                return Console.Out.WriteLineAsync($"received: {ctx.Message.tekst} ;; {ctx.Message.number}");

            }
        }
        
        static void Main(string[] args)
        {
            var inst = new HandlerClass();
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc => {
                var host = sbc.Host(new Uri("rabbitmq://localhost"),
                h => { h.Username("guest"); h.Password("guest"); });
                sbc.ReceiveEndpoint(host, "recvqueueb", ep => {
                   ep.Instance(inst);
                });
            });
            bus.Start();
            Console.WriteLine("odbiorca BE wystartował");
            Console.ReadKey();
            bus.Stop();
        }

    }
}
