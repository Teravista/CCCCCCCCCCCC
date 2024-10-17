using OfficeOpenXml;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var ep = new ExcelPackage(new FileInfo(@"C:\Users\Robert\source\repos\tester\ConsoleApp1\lab3.xlsx"));
            Console.WriteLine("Hello, World!");
        }
    }
}
