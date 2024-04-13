using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSR_WCF1;
using System.ServiceModel;

namespace WCF_ZAD_2
{
    [ServiceContract] public interface IZadanie2
    {
        [OperationContract] string Test(string arg);
    }

    public class Zadanie2 : IZadanie2
    {
        public string Test(string arg)
        {
            return $"testiong here: {arg}";
        }
    }
}
