using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProgram
{
    public class CatCalc : ICalculator
    {
        public string Eval(string a, string b)
        {
            return string.Concat(a, b);
        }
    }
}
