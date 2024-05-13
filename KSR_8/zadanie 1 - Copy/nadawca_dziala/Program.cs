using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace zadanie_1
{
    internal class Program
    {
        class Komunikat0 : Komunikaty.komunikat0
        {
            public string tekst { get; set; }
            public int number { set; get; }
            public static Task Handle(ConsumeContext<Komunikaty.komunikat0> ctx)
            {
                return Console.Out.WriteLineAsync($"received: {ctx.Message.tekst}");
            }
            static void Main(string[] args)
            {
                System.Threading.Thread.Sleep(1000);
                var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
                {
                    var host = sbc.Host(new Uri("rabbitmq://localhost"),
                    h => { h.Username("guest"); h.Password("guest"); });
                    // sbc.ReceiveEndpoint(host, "recvqueue", ep => {
                    //     ep.Handler<Komunikaty.Komunikat>(Handle);
                    //});
                });
                bus.Start();
                Console.WriteLine("NADAWCA wystartował");
                // tworzymy i startujemy szynę jak wyżej, podając inną nazwę kolejki
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("wysyławm wiadomośc o num: " + i);
                    bus.Publish(new Komunikat0()
                    {
                        tekst = "wiadomosc o numerze: " + i,
                        number = i+i+i*i
                    }
                    , ctx =>
                    {
                        ctx.Headers.Set("klucz_1", $"value 1");
                        ctx.Headers.Set("klucz_2", $"value 2 ");
                    }


                    );
                }

                Console.ReadKey();
                bus.Stop();
            }

        }
    }
}
