using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProgram
{
    public class StateCalc : ICalculator
    {
        int number;
        public StateCalc(int num) {
            this.number = num;
        }
        public string Eval(string a, string b)
        {
            string str = string.Concat(a, b);
            string str2 = string.Concat(str, this.number);
            this.number++;
            return str2;
        }
    }
}
