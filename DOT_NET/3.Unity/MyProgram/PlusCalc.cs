using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProgram
{
    public class PlusCalc : ICalculator
    {
        public string Eval(string a, string b)
        {
            int aa;
            int bb;
            try
            {
                aa = int.Parse(a); 
                bb = int.Parse(b);
            }
            catch
            {
                Console.WriteLine("Error parsing numbers try again");
                return "";
            }
            return (aa + bb) + "";
        }
    }
}
