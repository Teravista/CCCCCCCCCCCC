using GreenPipes;
using Komunikaty;
using MassTransit;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABORATORY_ALL
{
    internal class Wydawca
    {
        static Random rnd = new Random();
        class HandlerClass : IConsumer<Komunikaty.Ustaw>
        {
            public bool active = false;
            public Task Consume(ConsumeContext<Komunikaty.Ustaw> context)
            {
                this.active = context.Message.dziala;
                return Console.Out.WriteLineAsync("Activion Set to: "+this.active);
            }
        }
        public static Task HandleA(ConsumeContext<Komunikaty.OdpA> ctx)
        {
            //Random rnd = new Random();
            if(rnd.Next(0,3)==0)
            {
                throw new Exception();
            }
            return Console.Out.WriteLineAsync("response A from: " + ctx.Message.kto);
        }
        public static Task HandleB(ConsumeContext<Komunikaty.OdpB> ctx)
        {
            //Random rnd = new Random();
            if (rnd.Next(0, 3) == 0)
            {
                throw new Exception();
            }
            return Console.Out.WriteLineAsync("response B from: " + ctx.Message.kto);
        }
        static void Main(string[] args)
        {
            var inst = new HandlerClass();
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>{
                    var host = sbc.Host(new Uri("rabbitmq://localhost"), h =>
                            {
                                h.Username("guest");
                                h.Password("guest");
                            });
                    sbc.ReceiveEndpoint(host, "recvqueueW", ep =>{
                        ep.Instance(inst);
                    });
                    sbc.ReceiveEndpoint(host, "recvqueueOA", ep => {
                        ep.Handler<Komunikaty.OdpA>(HandleA);
                        ep.UseRetry(r => r.Immediate(5));
                    });
                sbc.ReceiveEndpoint(host, "recvqueueOB", ep => {
                    ep.Handler<Komunikaty.OdpB>(HandleB);
                    ep.UseRetry(r => r.Immediate(5));
                });

            });
            bus.Start();
            Console.WriteLine("Wydawca wystartował");
            int index = 0;
            while(true)
            {
                if (inst.active)
                {
                    bus.Publish(new Komunikaty.Komunikaty() { tekst = "message with number: " + index, number=index });
                    Console.WriteLine("sent messege with id: " + index);
                    index++;
                }
                System.Threading.Thread.Sleep(1000);
                //Console.WriteLine("one sec passed");
            }
            bus.Stop();
        }
    }
}
