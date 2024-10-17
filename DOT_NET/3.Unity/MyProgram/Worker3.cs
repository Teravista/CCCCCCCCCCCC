using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProgram
{
    public class Worker3
    {
        private ICalculator iCalculator;

        public void CalculatorSet(ICalculator iCalculatornew)
        {
            this.iCalculator = iCalculatornew;
        }
        public void Work(string a, string b)
        {
            if (this.iCalculator != null)
            {
                Console.WriteLine(this.iCalculator.Eval(a, b));
            }
            else
            {
                Console.WriteLine("iCalculator is null try again");
            }
        }

    }
}
