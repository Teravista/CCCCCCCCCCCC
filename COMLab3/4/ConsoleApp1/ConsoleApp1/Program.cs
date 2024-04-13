// See https://aka.ms/new-console-template for more information
using klasacNET;

namespace Console1
{
    class Program
    {
        static void Main(string[] args)
        {
            IKlasa klasa = new Klasa();
            klasa.Test("ladzia w c#");
        }
    }
}