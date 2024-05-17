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
    
}
internal class Wydawca
{
    class rec : IReceiveObserver
    {
        public int attempts = 0;
        public int succsesfullAtt = 0;
        public Task ConsumeFault<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType, Exception exception) where T : class
        {
            return Console.Out.WriteLineAsync("");

        }

        public Task PostConsume<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType) where T : class
        {
            attempts++;
            return Console.Out.WriteLineAsync("");
        }

        public Task PostReceive(ReceiveContext context)
        {
            succsesfullAtt++;
            return Console.Out.WriteLineAsync("");
        }

        public Task PreReceive(ReceiveContext context)
        {
            //attempts++;
            return Console.Out.WriteLineAsync("");
        }

        public Task ReceiveFault(ReceiveContext context, Exception exception)
        {
            return Console.Out.WriteLineAsync("");
        }
    }
    class ReceiveObserver : IPublishObserver
    {
       
        public int published = 0;

        Task IPublishObserver.PrePublish<T>(PublishContext<T> context)
        {
            return Console.Out.WriteLineAsync("");
        }

        Task IPublishObserver.PostPublish<T>(PublishContext<T> context)
        {
            published++;
            return Console.Out.WriteLineAsync("");
        }

        Task IPublishObserver.PublishFault<T>(PublishContext<T> context, Exception exception)
        {
            return Console.Out.WriteLineAsync("");
        }
    }
    //static Random rnd = new Random();
    class HandlerClass : IConsumer<Komunikaty.Ustaw>
    {
        public bool active = false;
        public Task Consume(ConsumeContext<Komunikaty.Ustaw> context)
        {
            this.active = context.Message.dziala;
            return Console.Out.WriteLineAsync("aktivi Set to: " + this.active);
        }
    }

    class HandlerClassStats : IConsumer<Komunikaty.Stats>
    {
        public bool active = false;
        public Task Consume(ConsumeContext<Komunikaty.Stats> context)
        {
            active = context.Message.stat;
            return Console.Out.WriteLineAsync("");
        }
    }
    public static Task HandleA(ConsumeContext<Komunikaty.OdpA> ctx)
    {
        Random rnd = new Random();
        if (rnd.Next(0, 3) == 1)
        {
            throw new Exception();
        }
        return Console.Out.WriteLineAsync("res A from: " + ctx.Message.kto);
    }
    public static Task HandleB(ConsumeContext<Komunikaty.OdpB> ctx)
    {
        Random rnd = new Random();
        if (rnd.Next(0, 3) == 1)
        {
            throw new Exception();
        }
        return Console.Out.WriteLineAsync("res B from: " + ctx.Message.kto);
    }
    static void Main(string[] args)
    {
        var inst = new HandlerClass();
        var nowy = new ReceiveObserver();
        var nowy2 = new rec();
        var inst2 = new HandlerClassStats();
        var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
        {
            var host = sbc.Host(new Uri("rabbitmq://localhost"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
            sbc.ReceiveEndpoint(host, "recvqueueW", ep =>
            {
                ep.Instance(inst);
                ep.Instance(inst2);
            });
            sbc.ReceiveEndpoint(host, "recvqueueOA", ep =>
            {
                ep.Handler<Komunikaty.OdpA>(HandleA);
               // ep.UseRetry(r => r.Immediate(5));
            });
            sbc.ReceiveEndpoint(host, "recvqueueOB", ep =>
            {
                ep.Handler<Komunikaty.OdpB>(HandleB);
               // ep.UseRetry(r => r.Immediate(5));
            });

        });
        //class ReceiveObserver : IReceiveObserver { ... }

        bus.ConnectReceiveObserver(nowy2); 
        //bus.ConnectConsumeObserver(nowy);
        //bus.ConnectConsumeMessageObserver(nowy);
        //bus.ConnectSendObserver(nowy);
        bus.ConnectPublishObserver(nowy);
        bus.Start();
        Console.WriteLine("W wystartował");
        int index = 0;
        while (true)
        {
            if (inst.active)
            {
                bus.Publish(new Komunikaty.Komunikaty() { tekst = "mess number:: " + index, number = index });
                Console.WriteLine("send mess id: " + index);
                index++;
            }
            if(inst2.active)
            {
                Console.WriteLine("Stats");
                Console.WriteLine("published; " + nowy.published);
                Console.WriteLine("attempts; " + nowy2.attempts);
                Console.WriteLine("succattempts; " + nowy2.succsesfullAtt);
                inst2.active = false;
            }
            System.Threading.Thread.Sleep(1000);
            //Console.WriteLine("one sec passed");
        }
        bus.Stop();
    }
}

