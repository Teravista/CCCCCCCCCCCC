using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProgram
{

    public class Worker
    {
        private ICalculator iCalculator;
        public Worker(ICalculator calculator) 
        { 
            this.iCalculator = calculator;
        }
        public void Work(string a, string b)
        {
            Console.WriteLine(this.iCalculator.Eval(a, b));
        }

    }
}
