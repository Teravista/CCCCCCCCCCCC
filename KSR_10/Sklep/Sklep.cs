using Automatonymous;
using Komunikaty;
using MassTransit;
using MassTransit.Saga;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using static MassTransit.MessageHeaders;

namespace Sklep
{
    public class RejestracjaDane : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; } // wymagane
        public string CurrentState { get; set; } // wymagane
        public string login { get; set; }
        public int ilosc { get; set; }
        public Guid? timeoutId
        {
            get;
            set;
        }
    }
    public class RejestracjaSaga : MassTransitStateMachine<RejestracjaDane>
    {

        // stany sagi poza początkowym
        public State Niepotwierdzone { get; private set; }

        public State PoteirdzonyKlient { get; private set; }
        public State PoteirdzonyMagazyn { get; private set; }
        // zdarzenia
        public Event<Komunikaty.StartZamówienia> Rej { get; private set; }
        public Event<Komunikaty.Potwierdzenie> Potw { get; private set; }
        public Event<Komunikaty.BrakPotwierdzenia> BrakPotw { get; private set; }
        public Event<Komunikaty.OdpowiedzWolne> Wolne { get; private set; }
        public Event<Komunikaty.ENDORADO> odpENDER { get; private set; }
        public Event<Komunikaty.OdpowiedzWolneNegatywna> WolneNega { get; private set; }

        public Event<Komunikaty.Timeout> TimeoutEvt { get; private set; }
        public Schedule<RejestracjaDane, Komunikaty.Timeout> TO
        {
            get; private set;
        }

        public RejestracjaSaga()
        {
            //timeouter
            Schedule(() => TO, x => x.timeoutId, x => { x.Delay = TimeSpan.FromSeconds(1); });
            DuringAny(When(TimeoutEvt).Then(ctx =>
            {
                //Console.WriteLine("TIMEOUTTTTTTTTTTTTTTTTTTT");
                ctx.Publish(new Komunikaty.ENDORADO() { CorrelationId = ctx.Instance.CorrelationId });
            }));
            InstanceState(x => x.CurrentState);
            //2
            Event(() => Rej, x => x.CorrelateBy(s => s.login, ctx => ctx.Message.login).SelectId(context => Guid.NewGuid()));
            //3
            Initially(
                When(Rej).Then(context => { }).ThenAsync(ctx =>
                {
                    ctx.Publish(new Komunikaty.PytanieOPotwierdzenie() { ilosc = ctx.Data.ilosc, CorrelationId = ctx.Instance.CorrelationId });
                    ctx.Publish(new Komunikaty.PytanieoWolne() { ilosc = ctx.Data.ilosc, CorrelationId = ctx.Instance.CorrelationId });
                    ctx.Instance.ilosc = ctx.Data.ilosc;
                    return Console.Out.WriteLineAsync(
                        $"ilosc={ctx.Data.ilosc}   " +
                        $"id={ctx.Instance.CorrelationId}");
                }).Schedule(TO, ctx => new Komunikaty.Timeout() { CorrelationId = ctx.Instance.CorrelationId })
                // .Respond(ctx =>
                //{
                // return// new Komunikaty.PytanieOPotwierdzenie()
                //  { id = ctx.Instance.CorrelationId };
                // })
                .TransitionTo(Niepotwierdzone));
            //4
            During(Niepotwierdzone, When(Potw).Then(ctx => { }).ThenAsync(ctx => { return Console.Out.WriteLineAsync("Akecptacjaużytkownika"); }).TransitionTo(PoteirdzonyKlient));
            During(Niepotwierdzone, When(Wolne).Then(ctx => { }).ThenAsync(ctx => { return Console.Out.WriteLineAsync("wystarczajaco rzeczy w magazynie"); }).TransitionTo(PoteirdzonyMagazyn));

            //
            During(PoteirdzonyKlient, When(Wolne).Then(ctx => { }).ThenAsync(ctx =>
            {
                Console.Out.WriteLineAsync("warunki spelnione Zamówienie spełnione");
                ctx.Publish(new Komunikaty.AkceptacjaZamówienia() { ilosc = ctx.Instance.ilosc });
                return Console.Out.WriteLineAsync("");
            }).Finalize());

            During(PoteirdzonyMagazyn, When(Potw).Then(ctx => { }).ThenAsync(ctx =>
            {
                Console.Out.WriteLineAsync("warunki spelnione Zamówienie spełnione");
                ctx.Publish(new Komunikaty.AkceptacjaZamówienia() { ilosc = ctx.Instance.ilosc });
                return Console.Out.WriteLineAsync("");
            }).Finalize());



            //
            DuringAny(When(WolneNega).Then(ctx =>
            {
                Console.WriteLine("koniec Brak wolnega magazynu");
                ctx.Publish(new Komunikaty.OdrzucenieZamówienia() { ilosc = ctx.Instance.ilosc, czyDoZwrotu = false });
            }).Finalize());
            ///ednordao
            DuringAny(When(odpENDER).Then(ctx =>
            {
                Console.WriteLine("TIMED OUT");
                ctx.Publish(new Komunikaty.OdrzucenieZamówienia() { ilosc = ctx.Instance.ilosc, czyDoZwrotu = false });
            }).Finalize());

            During(PoteirdzonyMagazyn, When(BrakPotw).Then(ctx =>
            {
                Console.WriteLine("odmowa klienta + zwracenie magazynu");
                ctx.Publish(new Komunikaty.OdrzucenieZamówienia() { ilosc = ctx.Instance.ilosc, czyDoZwrotu = true });
            }).Finalize());
            During(Niepotwierdzone, When(BrakPotw).Then(ctx =>
            {
                Console.WriteLine("odmowa klienta");
                ctx.Publish(new Komunikaty.OdrzucenieZamówienia() { ilosc = ctx.Instance.ilosc, czyDoZwrotu = false });
            }).Finalize());
            //6
            DuringAny(When(Rej).Then(ctx => { }));
            //5
            SetCompletedWhenFinalized();
        }

    }

    internal class Sklep
    {
        static void Main(string[] args)
        {
            var repo = new InMemorySagaRepository<RejestracjaDane>();
            var machine = new RejestracjaSaga();
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                sbc.ReceiveEndpoint(host, "MainQueueS", ep =>
                {
                    ep.StateMachineSaga(machine, repo);
                });
                sbc.UseInMemoryScheduler();
            });
            bus.Start();
            Console.WriteLine("SKLEP START!!");
            Console.ReadKey();
            bus.Stop();
        }
    }
}
