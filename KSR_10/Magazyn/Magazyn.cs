using Komunikaty;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazyn
{

    internal class Magazyn
    {
        class MagazynStan
        {
            public int wolne = 0;
            public int zarezerwowane = 0;
        }

        class HandlerAkceptacja : IConsumer<Komunikaty.AkceptacjaZamówienia>
        {
            public MagazynStan magStan;
            public HandlerAkceptacja(MagazynStan magStanRef)
            {
                this.magStan = magStanRef;
            }
            public Task Consume(ConsumeContext<AkceptacjaZamówienia> ctx)
            {
                this.magStan.zarezerwowane -= ctx.Message.ilosc;
                return Console.Out.WriteLineAsync("akceptacja zamowienia o ilosci: " + ctx.Message.ilosc);
            }
        }
        class HandlerOdrzucenie : IConsumer<Komunikaty.OdrzucenieZamówienia>
        {
            public MagazynStan magStan;
            public HandlerOdrzucenie(MagazynStan magStanRef)
            {
                this.magStan = magStanRef;
            }
            public Task Consume(ConsumeContext<OdrzucenieZamówienia> ctx)
            {
                if (ctx.Message.czyDoZwrotu)
                {
                    this.magStan.wolne += ctx.Message.ilosc;
                    this.magStan.zarezerwowane -= ctx.Message.ilosc;
                }
                return Console.Out.WriteLineAsync("odrzucenie zamowienia o ilosci: " + ctx.Message.ilosc);
            }
        }
        class HandlerClassStats : IConsumer<Komunikaty.PytanieoWolne>
        {
            public MagazynStan magStan;
            public HandlerClassStats(MagazynStan magStanRef)
            {
                this.magStan = magStanRef;
            }
            
            public Task Consume(ConsumeContext<Komunikaty.PytanieoWolne> ctx)
            {
                Console.Out.WriteLineAsync("wolne: " + (this.magStan.wolne) + " zapotrzebowane: " + ctx.Message.ilosc);

                if (this.magStan.wolne >= ctx.Message.ilosc)
                {
                    Console.Out.WriteLineAsync("Wystarczająco");
                    this.magStan.wolne -= ctx.Message.ilosc;
                    this.magStan.zarezerwowane += ctx.Message.ilosc;
                    ctx.Publish(new Komunikaty.OdpowiedzWolne() { CorrelationId = ctx.Message.CorrelationId });

                }
                else
                {
                    Console.Out.WriteLineAsync("Nie Wystarczajaco");
                    ctx.Publish(new Komunikaty.OdpowiedzWolneNegatywna() { CorrelationId = ctx.Message.CorrelationId });

                }
                return Console.Out.WriteLineAsync("");
            }
        }
        static void Main(string[] args)
        {
            var magazyn = new MagazynStan();
            var hanldAkceptacja = new HandlerAkceptacja(magazyn);
            var hanldOdrzucenie = new HandlerOdrzucenie(magazyn);
            var hanlderClass = new HandlerClassStats(magazyn);
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                sbc.ReceiveEndpoint(host, "MainQueueM", ep =>
                {
                    ep.Instance(hanlderClass);
                    ep.Instance(hanldOdrzucenie);
                    ep.Instance(hanldAkceptacja);
                });
            });
            bus.Start();
            Console.WriteLine("Stan Magazynu Wolne: 0");
            Console.WriteLine("Stan Magazynu Zarezerwowane: 0");
            ConsoleKeyInfo cki;
            do
            {
                Console.WriteLine("type a to add items");
                cki = Console.ReadKey();
                Console.WriteLine();
                switch (cki.Key)
                {
                    case ConsoleKey.A:
                        try
                        {
                            Console.WriteLine("Type number to add");
                            int intTemp = Convert.ToInt32(Console.ReadLine());
                            magazyn.wolne += intTemp;
                            Console.Clear();
                            Console.WriteLine("Stan Magazynu Wolnego: " + magazyn.wolne);
                            Console.WriteLine("Stan Magazynu Zarezerwowane: "+magazyn.zarezerwowane);

                        }
                        catch { }
                        break;
                    case ConsoleKey.C:
                        Console.Clear();
                        Console.WriteLine("Stan Magazynu Wolnego: " + magazyn.wolne);
                        Console.WriteLine("Stan Magazynu Zarezerwowane: " + magazyn.zarezerwowane);
                        break;

                }

            } while (cki.Key != ConsoleKey.Escape);
            bus.Stop();

        }
    }
}
