using MassTransit;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komunikaty
{
    //public class Rejestracja { public string login;  }
    public class StartZamówienia { public int ilosc; public string login;public string sender; }
    public class PytanieOPotwierdzenie : CorrelatedBy<Guid> { public int ilosc; public Guid CorrelationId { get; set; } }
    public class Potwierdzenie : CorrelatedBy<Guid> { public string login; public Guid CorrelationId { get; set; } }
    public class BrakPotwierdzenia : CorrelatedBy<Guid> { public Guid CorrelationId { get; set; } }
    public class PytanieoWolne : CorrelatedBy<Guid> { public int ilosc; public Guid CorrelationId { get; set; } }
    public class OdpowiedzWolne : CorrelatedBy<Guid> { public Guid CorrelationId { get; set; } }
    public class ENDORADO : CorrelatedBy<Guid> { public Guid CorrelationId { get; set; } }
    public class OdpowiedzWolneNegatywna : CorrelatedBy<Guid> { public Guid CorrelationId { get; set; } }
    public class AkceptacjaZamówienia : CorrelatedBy<Guid> { public int ilosc; public Guid CorrelationId { get; set; } }
    public class OdrzucenieZamówienia : CorrelatedBy<Guid> { public int ilosc;public bool czyDoZwrotu; public Guid CorrelationId { get; set; } }
    public class Timeout : CorrelatedBy<Guid>
    {  public Guid CorrelationId { get; set; }
    }

}
