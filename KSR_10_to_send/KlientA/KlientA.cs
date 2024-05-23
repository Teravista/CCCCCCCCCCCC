using Komunikaty;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlientA
{
    internal class KlientA
    {
        class HandlerAkceptacja : IConsumer<Komunikaty.AkceptacjaZamówienia>
        {
            public Task Consume(ConsumeContext<AkceptacjaZamówienia> ctx)
            {
                return Console.Out.WriteLineAsync("akceptacja zamowienia o ilosci: "+ctx.Message.ilosc);
            }
        }
        class HandlerOdrzucenie : IConsumer<Komunikaty.OdrzucenieZamówienia>
        {
            public Task Consume(ConsumeContext<OdrzucenieZamówienia> ctx)
            {
                return Console.Out.WriteLineAsync("odrzucenie zamowienia o ilosci: " + ctx.Message.ilosc);
            }
        }
        class HandlerPotwierdzenie : IConsumer<Komunikaty.PytanieOPotwierdzenie>
        {
            public Task Consume(ConsumeContext<PytanieOPotwierdzenie> ctx)
            {
                Console.WriteLine("potwerdzasz zamowienie ? {N/Y}");
                while (true)
                {
                    ConsoleKeyInfo cki = Console.ReadKey();
                    Console.WriteLine();
                    switch (cki.Key)
                    {
                        case ConsoleKey.N:
                            ctx.Publish(new Komunikaty.BrakPotwierdzenia() { CorrelationId = ctx.Message.CorrelationId });
                            Console.WriteLine("Odrzucamy");
                            return Console.Out.WriteLineAsync("");
                        case ConsoleKey.Y:
                            ctx.Publish(new Komunikaty.Potwierdzenie() { CorrelationId = ctx.Message.CorrelationId });
                            Console.WriteLine("Potwierdzamy");
                            return Console.Out.WriteLineAsync("");

                    }
                }
            }
        }
        static void Main(string[] args)
        {
            var handlPotwierdzeniue = new HandlerPotwierdzenie();
            var handlerOdrzucenie = new HandlerOdrzucenie();
            var handlAkceptacja = new HandlerAkceptacja();
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                sbc.ReceiveEndpoint(host, "MainQueueA", ep => {
                    ep.Instance(handlPotwierdzeniue);
                    ep.Instance(handlerOdrzucenie);
                    ep.Instance(handlAkceptacja);
                });
            });
            bus.Start();
            Console.WriteLine("KLIENT AAAA START");
            ConsoleKeyInfo cki;
            do
            {
                Console.WriteLine("type s to craete new saga");
                cki = Console.ReadKey();
                Console.WriteLine();
                switch (cki.Key)
                {
                    case ConsoleKey.S:
                        try
                        {
                            Console.WriteLine("Type number to create request");
                            int intTemp = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("podaj Login zamowienia");
                            string log = Console.ReadLine();
                            bus.Publish(new Komunikaty.StartZamówienia() { ilosc = intTemp, login = log ,sender= "MainQueueA" }) ;
                        }
                        catch { }
                        break;
                    case ConsoleKey.T:
                       
                        break;

                }

            } while (cki.Key != ConsoleKey.Escape);
            bus.Stop();

        }
    }
}
