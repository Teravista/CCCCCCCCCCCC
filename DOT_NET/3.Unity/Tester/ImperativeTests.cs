using Microsoft.Practices.Unity.Configuration;
using MyProgram;
using System.Configuration;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace Tester
{
    [TestClass]
    public class ImperativeTests
    {

        StringWriter sw;


        public string RemoveSpecialChars(string input)
        {
            return Regex.Replace(input, @"[^0-9a-zA-Z\._]", string.Empty);
        }

        public static IUnityContainer ImperatieStartup()
        {
            IUnityContainer cont = new UnityContainer();
            cont.RegisterType<ICalculator, CatCalc>("CatCalcName");
            cont.RegisterType<ICalculator, PlusCalc>("PlusCalcName");
            cont.RegisterType<ICalculator, StateCalc>("StateCalcName", new ContainerControlledLifetimeManager(), new InjectionConstructor(1));

            cont.RegisterType<Worker>("Worker1CatCalc", new InjectionConstructor(cont.Resolve<ICalculator>("CatCalcName")));
            cont.RegisterType<Worker2>("Worker2PlusCalc", new InjectionProperty("iCalculator", cont.Resolve<ICalculator>("PlusCalcName")));
            cont.RegisterType<Worker3>("Worker3CatCalc", new InjectionMethod("CalculatorSet", cont.Resolve<ICalculator>("CatCalcName")));

            cont.RegisterType<Worker>("state", new InjectionConstructor(cont.Resolve<ICalculator>("StateCalcName")));
            cont.RegisterType<Worker2>("state", new InjectionProperty("iCalculator", cont.Resolve<ICalculator>("StateCalcName")));
            cont.RegisterType<Worker3>("state", new InjectionMethod("CalculatorSet", cont.Resolve<ICalculator>("StateCalcName")));

            return cont;
        }

        public static IUnityContainer DeclarativeStuff()
        {
            IUnityContainer cont = new UnityContainer();
            var configFile = new ExeConfigurationFileMap { ExeConfigFilename = "C:\\Users\\Robert\\source\\repos\\DOT_NET\\3.Unity\\MyProgram\\App.config" };
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFile,ConfigurationUserLevel.None);
            UnityConfigurationSection section = (UnityConfigurationSection)config.GetSection("unity");
            section.Configure(cont);
            return cont;
        }

        [TestInitialize]
        public void TestInitialize()
        {
            
        }

        [TestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void TestWorker1CatCalc(bool isImperative)
        {
            IUnityContainer cont;
            if (isImperative)
            {
                cont = ImperatieStartup();
            }
            else
            {
                cont = DeclarativeStuff();
            }
            var worker = cont.Resolve<Worker>("Worker1CatCalc");
            
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);
            worker.Work("15", "15");
            string finalString = RemoveSpecialChars(sw.ToString());
            Assert.AreEqual("1515",finalString );


            sw = new StringWriter();
            Console.SetOut(sw);
            worker.Work("222", "111");
            finalString = RemoveSpecialChars(sw.ToString());
            Assert.AreEqual("222111",finalString );

        }

        [TestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void TestWorker2PlusCalc(bool isImperative)
        {
            IUnityContainer cont;
            if (isImperative)
            {
                cont = ImperatieStartup();
            }
            else
            {
                cont = DeclarativeStuff();
            }
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);
            var work2PlusCalc = cont.Resolve<Worker2>("Worker2PlusCalc");
            work2PlusCalc.Work("15", "15");
            string finalString = RemoveSpecialChars(sw.ToString());
            Assert.AreEqual("30",finalString);

            sw = new StringWriter();
            Console.SetOut(sw);
            work2PlusCalc.Work("77", "33");
            finalString = RemoveSpecialChars(sw.ToString());
            Assert.AreEqual("110",finalString );

        }


        [TestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void TestWorker3CatCalc(bool isImperative)
        {
            IUnityContainer cont;
            if (isImperative)
            {
                cont = ImperatieStartup();
            }
            else
            {
                cont = DeclarativeStuff();
            }
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);
            var work2PlusCalc = cont.Resolve<Worker3>("Worker3CatCalc");
            work2PlusCalc.Work("15", "15");
            string finalString = RemoveSpecialChars(sw.ToString());
            Assert.AreEqual("1515",finalString );

            sw = new StringWriter();
            Console.SetOut(sw);
            work2PlusCalc.Work("77", "33");
            finalString = RemoveSpecialChars(sw.ToString());
            Assert.AreEqual("7733",finalString );

        }

        [TestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void TestSingletonStateCalc(bool isImperative)
        {
            IUnityContainer cont;
            if (isImperative)
            {
                cont = ImperatieStartup();
            }
            else
            {
                cont = DeclarativeStuff();
            }
            var calculator1 = cont.Resolve<ICalculator>("StateCalcName");
            var calculator2 = cont.Resolve<ICalculator>("StateCalcName");

            calculator1.Eval("1", "2");
            calculator1.Eval("1", "2");
            calculator1.Eval("1", "2");
            calculator1.Eval("1", "2");
            string finalString = calculator2.Eval("1", "2");
            Assert.AreEqual("125",finalString );
            Assert.AreEqual(calculator1, calculator2);
        }

        [TestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void TestAllWorkersStateCalc(bool isImperative)
        {
            IUnityContainer cont;
            if (isImperative)
            {
                cont = ImperatieStartup();
            }
            else
            {
                cont = DeclarativeStuff();
            }
            StringWriter sw;
            string finalString;
            var workerStateCalc1 = cont.Resolve<Worker>("state");
            var workerStateCalc2 = cont.Resolve<Worker2>("state");
            var workerStateCalc3 = cont.Resolve<Worker3>("state");


            workerStateCalc1.Work("15", "15");
            workerStateCalc2.Work("15", "15");
            workerStateCalc3.Work("15", "15");
            workerStateCalc1.Work("15", "15");
            workerStateCalc1.Work("15", "15");
            workerStateCalc3.Work("15", "15");
            workerStateCalc3.Work("15", "15");


            sw = new StringWriter();
            Console.SetOut(sw);
            workerStateCalc2.Work("77", "13");
            finalString = RemoveSpecialChars(sw.ToString());
            Assert.AreEqual("77138", finalString);

            sw = new StringWriter();
            Console.SetOut(sw);
            workerStateCalc3.Work("11", "99999");
            finalString = RemoveSpecialChars(sw.ToString());
            Assert.AreEqual("11999999", finalString);






        }
    }
}