using System.Runtime.InteropServices;

[Guid("82C2867A-8E32-4BB7-922A-18398DDA7F2C"), ComVisible(true), InterfaceType(ComInterfaceType.InterfaceIsDual)] 
public interface IKlasa { uint Test(string s); }



class TestClass
{
    static void Main(string[] argss)
    {
        Type t = Type.GetTypeFromProgID("KSR20.COM3Klasa.1");
        object k = Activator.CreateInstance(t);
        t.InvokeMember("Test", System.Reflection.BindingFlags.InvokeMethod,
        null, k, new object[] { "KochamAKO" });
        //t.InvokeMember("Pop", System.Reflection.BindingFlags.InvokeMethod, null, k, args);
        //Console.WriteLine("Pop = {0}", (int)args[0]);
    }
}

