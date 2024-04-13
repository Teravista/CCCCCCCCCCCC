using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF_ZAD_2
{
    [ServiceContract]
    public interface IZadanie7 
    {
        [OperationContract][FaultContract(typeof(Wyjatek7))]
        void RzucWyjatek7(string a, int b);
    }
    [DataContract]
    public class Wyjatek7
    {
        [DataMember] public string opis{ get; set; }
        [DataMember]  public string a { get; set; }
        [DataMember] public int b { get; set; }
    }
    public class Zadanie7 : IZadanie7
    {
        public void RzucWyjatek7(string a, int b)
        {
            var exception = new  FaultException<Wyjatek7>(new Wyjatek7(), new FaultReason("my reason"));
            exception.Detail.a = a;
            exception.Detail.b = b;
            exception.Detail.opis = "my reason";
            throw exception;
        }
    }

}

