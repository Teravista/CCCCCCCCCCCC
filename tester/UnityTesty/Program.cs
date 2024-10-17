using Microsoft.Practices.Unity.Configuration;
using System.Configuration;
using Unity;

HelloWorld hw = new();
hw.WriteHelloWorldLine();
IUnityContainer cont = new UnityContainer();
//cont.RegisterType<ILogger, Logger>();
UnityConfigurationSection section =(UnityConfigurationSection)ConfigurationManager.GetSection("unity");
section.Configure(cont);
var w1 = cont.Resolve<Worker>();
w1.Work();

public class HelloWorld
{
    public void WriteHelloWorldLine()
    {
        Console.WriteLine("Hello, World!");
    }
}
interface ILogger
{
    void Log(string s);
}
class Logger : ILogger
{
    public void Log(string s) { Console.WriteLine("logger: " + s); }
}
class Worker // wstrzykiwanie przez konstruktor
{
    public Worker(ILogger log) { m_log = log; }
    public void Work()
    {
        m_log.Log("begin");
        m_log.Log("end");
    }
    private ILogger m_log;
}

