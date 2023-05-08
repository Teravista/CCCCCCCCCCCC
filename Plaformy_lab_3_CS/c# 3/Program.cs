using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;



[Serializable]
public class Engine
{
    public double displacement;
    public double horsePower;
    [XmlAttribute]
    public string model = "";
    public Engine(double displacement, double horsePower, string model)
    {
        this.displacement = displacement;
        this.horsePower = horsePower;
        this.model = model;
    }
    public Engine()
    {

    }
}


[XmlType("car")]
public class Car
{

    public string model = "";
    [XmlElement(ElementName = "engine")]
    public Engine motor;
    public int year;
    public Car(string model, Engine engine, int year)
    {
        this.model = model;
        this.motor = engine;
        this.year = year;
    }

    public Car()
    {

    }
}

class program
{
    private static void createXmlFromLinq(List<Car> myCars)
    {
        IEnumerable<XElement> nodes = from e in myCars
                                      select (
            new XElement("car",
                new XElement("model", e.model),
                    new XElement("engine",
                        new XAttribute("model", e.motor.model),
                        new XElement("displacement", e.motor.displacement),
                        new XElement("horsePower", e.motor.horsePower)
                        ),
                     new XElement("year", e.year)
                     ));//LINQ query expressions
        XElement rootNode = new XElement("cars", nodes); //create a root node to contain the query results
        rootNode.Save("CarsFromLinq.xml");
    }

    private static void createXmlToXhtml(List<Car> myCars)
    {
        IEnumerable<XElement> nodes = from e in myCars
                                      select (
                new XElement("tr",
                     new XElement("th", e.model),
                     new XElement("th", e.motor.model),
                     new XElement("th", e.motor.displacement),
                     new XElement("th", e.motor.horsePower),
                     new XElement("th", e.year)
                ));//LINQ query expressions
        XElement rootNode = new XElement("table", nodes, new XAttribute("border", "1px solid black"));
        ;//create a root node to contain the query results
        rootNode.Save("CarsHtml.html");
    }

    static void Main()
    {
        List<Car> myCars = new List<Car>(){
            new Car("E250", new Engine(1.8, 204, "CGI"), 2009),
            new Car("E350", new Engine(3.5, 292, "CGI"), 2009),
            new Car("A6", new Engine(2.5, 187, "FSI"), 2012),
            new Car("A6", new Engine(2.8, 220, "FSI"), 2012),
            new Car("A6", new Engine(3.0, 295, "TFSI"), 2012),
            new Car("A6", new Engine(2.0, 175, "TDI"), 2011),
            new Car("A6", new Engine(3.0, 309, "TDI"), 2011),
            new Car("S6", new Engine(4.0, 414, "TFSI"), 2012),
            new Car("S8", new Engine(4.0, 513, "TFSI"), 2012)
            };
        var results2 = myCars.Where(e => e.model == "A6")
            .Select(e => new
            {
                hppl = e.motor.horsePower / e.motor.displacement,
                engineType = ((e.motor.model == "TDI") ? "diesel" : "petrol"),

            });
        var results = results2.GroupBy(t => new { engineType = t.engineType })
   .Select(g => new
   {
       Average = g.Average(p => p.hppl),
       engineType = g.Key.engineType
   });
        foreach (var group in results)
        {
            Console.WriteLine("Group key: {0} {1}", group.engineType, group.Average);
        }





        FileStream fs = new FileStream("CarsCollection.xml", FileMode.Create);
        XmlSerializer serializer = new XmlSerializer(typeof(List<Car>), new XmlRootAttribute("cars"));

        serializer.Serialize(fs, myCars);
        fs.Flush();
        fs.Close();
        fs.Dispose();




        XElement rootNode = XElement.Load("CarsCollection.xml");
        double totalHP = (double)rootNode.XPathEvaluate("sum(//engine[@model!='TDI']/horsePower)");
        double countHP = (double)rootNode.XPathEvaluate("count(//engine[@model!='TDI']/horsePower)");
        Console.WriteLine(totalHP / countHP);

        IEnumerable<XElement> models = rootNode.XPathSelectElements("(//model[not(. = following::model/.)])");
        foreach (var model in models)
        {
            Console.WriteLine(model);
        }
        createXmlFromLinq(myCars);


        createXmlToXhtml(myCars);

        /*IEnumerable<XElement> carsDeserialized = from e in objDeserialized
                                                 select (
                       new XElement("car",
                           new XElement("model", e.model, new XAttribute("year", e.year)),
                               new XElement("engine",
                                   new XAttribute("model", e.motor.model),
                                   new XElement("displacement", e.motor.displacement),
                                   new XElement("hp", e.motor.horsePower)
                                   )
                                ));//LINQ query expressions*/

        XElement xelm = null;
        foreach (var car in rootNode.Elements())
        {
            foreach (var elem in car.Elements())
            {
                if(elem.Name=="model")
                {
                    xelm = elem;
                }
                if (elem.Name == "engine")
                {
                    
                    foreach (var subEngine in elem.Elements())
                    {
                        if (subEngine.Name == "horsePower")
                        {
                            subEngine.Name="hp";
                        }
                    }
                }
                if (elem.Name == "year")
                {
                    if (xelm != null)
                    {
                        xelm.SetAttributeValue("year", elem.Value);
                    }
                    else
                    {
                        xelm.SetAttributeValue("year", "error");
                    }
                    elem.Remove();
                }
            }
        }
        Console.WriteLine("");
    }
}
