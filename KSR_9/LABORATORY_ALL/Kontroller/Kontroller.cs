using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontroller
{
    internal class Kontroller
    {
        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc => {
                var host = sbc.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                //sbc.ReceiveEndpoint(host, "recvqueue", ep => {
                //    ep.Instance(inst);
                //});
                
            });
            bus.Start();
            Console.WriteLine("Kontroller wystartował"); 
            var tsk = bus.GetSendEndpoint(new Uri("rabbitmq://localhost/recvqueueW"));
            tsk.Wait(); var sendEp = tsk.Result;

            ConsoleKeyInfo cki;
            do 
            {
                cki = Console.ReadKey();
                switch (cki.Key)
                {
                    case ConsoleKey.S:
                        sendEp.Send<Komunikaty.Ustaw>(new Komunikaty.Ustaw() { dziala=true });
                        Console.WriteLine("Sent Ustaw = true");
                        break;
                    case ConsoleKey.T:
                        sendEp.Send<Komunikaty.Ustaw>(new Komunikaty.Ustaw() { dziala = false });
                        Console.WriteLine("Sent Ustaw = false");
                        break;
                    case ConsoleKey.I:
                        sendEp.Send<Komunikaty.Stats>(new Komunikaty.Stats() { stat = true });
                        Console.WriteLine("starts");

                        break;
                    

                }

            } while (cki.Key != ConsoleKey.Escape);

            bus.Stop();
        }
    }
}
