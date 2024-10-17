using Microsoft.Practices.Unity.Configuration;
using System.ComponentModel;
using System.Configuration;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace MyProgram
{

    internal class Program
    {
        static IUnityContainer DeclarativeStuff()
        {
            IUnityContainer cont = new UnityContainer();
            UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            section.Configure(cont);
            return cont;
        }
        static IUnityContainer ImperativeStuff()
        {
            IUnityContainer cont = new UnityContainer();
            //UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            //section.Configure(cont);
            //  var CarCalcVar = new CatCalc();
            //var PlusCalcVar = new PlusCalc();
            cont.RegisterType<ICalculator, CatCalc>("CatCalcName");
            cont.RegisterType<ICalculator, PlusCalc>("PlusCalcName");
            cont.RegisterType<ICalculator, StateCalc>("StateCalcName", new ContainerControlledLifetimeManager(),new InjectionConstructor(1));

            cont.RegisterType<Worker>("Worker1CatCalc", new InjectionConstructor(cont.Resolve<ICalculator>("CatCalcName")));
            cont.RegisterType<Worker2>("Worker2PlusCalc" ,new InjectionProperty("iCalculator", cont.Resolve<ICalculator>("PlusCalcName"))); 
            cont.RegisterType<Worker3>("Worker3CatCalc", new InjectionMethod("CalculatorSet", cont.Resolve<ICalculator>("CatCalcName")));

            cont.RegisterType<Worker>("state", new InjectionConstructor( cont.Resolve<ICalculator>("StateCalcName")));
            cont.RegisterType<Worker2>("state", new InjectionProperty("iCalculator", cont.Resolve<ICalculator>("StateCalcName")));
            cont.RegisterType<Worker3>("state", new InjectionMethod("CalculatorSet", cont.Resolve<ICalculator>("StateCalcName")));

            return cont;
        }
        static void tester(IUnityContainer cont)
        {
            var work1CatCalc = cont.Resolve<Worker>("Worker1CatCalc");
            var work2PlusCalc = cont.Resolve<Worker2>("Worker2PlusCalc");
            var work3CatCalc = cont.Resolve<Worker3>("Worker3CatCalc");

            var workerStateCalc1 = cont.Resolve<Worker>("state");
            var workerStateCalc2 = cont.Resolve<Worker2>("state");
            var workerStateCalc3 = cont.Resolve<Worker3>("state");

            work1CatCalc.Work("1", "2");
            work2PlusCalc.Work("3", "4");
            work3CatCalc.Work("5", "6");

            workerStateCalc1.Work("7", "8");
            workerStateCalc2.Work("9", "10");
            workerStateCalc3.Work("11", "12");
        }
        static void Main(string[] args)
        {


            Console.WriteLine("Imperative start");
            tester(ImperativeStuff());
            Console.WriteLine("Imperative fin");


            Console.WriteLine("Declarative start");
            tester(DeclarativeStuff());
            Console.WriteLine("Declarative fin");

        }
    }
}
