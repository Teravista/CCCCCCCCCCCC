using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abonament_B
{
    internal class AbonamentB
    {
        public static Task Handle(ConsumeContext<Komunikaty.Komunikaty> ctx)
        {
            if (ctx.Message.number % 3 == 0 && ctx.Message.number != 0)
            {
                ctx.RespondAsync<Komunikaty.OdpA>(new Komunikaty.OdpA() { kto = " AbonamentB" });
                Console.Out.WriteLineAsync("Wiadonosc /3");
            }
            return Console.Out.WriteLineAsync($"received: {ctx.Message.tekst}");

        }
        public static Task HndlFault(ConsumeContext<Fault<Komunikaty.OdpB>> ctx)
        {

            return Console.Out.WriteLineAsync(" caused Exception");
            // ctx.Message.Message = oryginalna wiadomość

        }
        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc => {
                var host = sbc.Host(new Uri("rabbitmq://localhost"),
                h => { h.Username("guest"); h.Password("guest"); });
                sbc.ReceiveEndpoint(host, "recvqueueB", ep => {
                    ep.Handler<Komunikaty.Komunikaty>(Handle);
                });
                sbc.ReceiveEndpoint(host, "recvqueueOB_error", ep => {
                    ep.Handler<Fault<Komunikaty.OdpB>>(HndlFault);
                });
            });
            bus.Start();
            Console.WriteLine("ab __BB__ wystartował");
            Console.ReadKey();
            bus.Stop();
        }
    }
}
