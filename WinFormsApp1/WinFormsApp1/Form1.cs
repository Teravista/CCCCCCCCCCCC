using System.ComponentModel;
using System.DirectoryServices;
using System.Reflection.Metadata.Ecma335;
using System.Security;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace WinFormsApp1
{
    public class Engine : IComparable
    {
        public double displacement { get; set; }
        public double horsePower { get; set; }
        public string model { get; set; }
        public Engine(double displacement, double horsePower, string model)
        {
            this.displacement = displacement;
            this.horsePower = horsePower;
            this.model = model;
        }
        public Engine()
        {

        }
        public override string ToString()
        {
            return model + " " + displacement + " (" + horsePower + " hp)";
        }
        public int CompareTo(object inc)
        {

            // Storing incoming object in temp variable of 
            // current class type
            Engine enging = inc as Engine;

            return this.horsePower.CompareTo(enging.horsePower);
        }

    }


    public class Car
    {

        public string model { get; set; }
        public Engine motor { get; set; }
        public int year { get; set; }
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
    public class MyBindingList : BindingList<Car>
    {
        public List<Car> originalList { get; set; }
        public List<Car> sortedList { get; set; }
        public MyBindingList(List<Car> myBidingCar)
        {
                originalList = myBidingCar;
        }
        protected override bool SupportsSortingCore
        {
            get
            {
                return true;
            }
        }
       
        protected override void ApplySortCore(PropertyDescriptor prop,
                            ListSortDirection direction)
        {
            if (prop != null)
            {
                if (prop.PropertyType.GetInterface("IComparable") != null)
                {
                    if (prop.ComponentType.Name == "Engine")
                    {
                        sortedList = new List<Car>(originalList);
                        sortedList.Sort((x, y) => prop.GetValue(x.motor).ToString()
                                .CompareTo(prop.GetValue(y.motor).ToString()));

                    }
                    else
                    {
                        sortedList = new List<Car>(originalList);
                        sortedList.Sort((x, y) => prop.GetValue(x).ToString()
                                .CompareTo(prop.GetValue(y).ToString()));
                    }
                    if(direction==ListSortDirection.Descending)
                    {
                        sortedList.Reverse();
                    }

                }
                else
                {
                    MessageBox.Show("Icomparable not implemented");
                }
            }
            else
            {
                MessageBox.Show("Error prop empty");
            }


        }
        public void Sorter(PropertyDescriptor prop, ListSortDirection direction)
        {
            ApplySortCore(prop, direction);
        }
        protected override bool SupportsSearchingCore
        {
            get { return true; }
        }
        protected override int FindCore(PropertyDescriptor prop, object key)
        {
            // by model or horsepower

               int index = originalList.FindIndex((x) => prop.GetValue(x).ToString()
                            ==key.ToString());
            return index;
        }

        public int Finder(PropertyDescriptor prop,object key) {
            return FindCore(prop, key);
        }
    }


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static int CompareHP(Car x, Car y)
        {
            return x.motor.horsePower.CompareTo(y.motor.horsePower);
        }
        static bool IsTDI(Car x)
        {
            return !(x.motor.model.Equals("TDI"));
        }
        static void Printer(Car x)
        {
            MessageBox.Show("model: " + x.model + "\nmoedl silnika:" + x.motor.model + "\nHP: " + x.motor.horsePower + "\nrok wykonania: " + x.year);
        }

        private void Form1_Load(object sender, EventArgs e)
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
            BindingList<Car> myCarsBindingList = new BindingList<Car>(myCars);
            BindingSource carBindingSource = new BindingSource(myCarsBindingList,null);
       
            //Drag a DataGridView control from the Toolbox to the form.
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = carBindingSource;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.Refresh();
            ///1 zapytanie
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
            ////drugi posob
            var results22 = from a in myCars
                            where a.model == "A6"
                            select new
                            {
                                hppl = a.motor.horsePower / a.motor.displacement,
                                engineType = ((a.motor.model == "TDI") ? "diesel" : "petrol"),

                            };
            var results23 = from a in results22
                            group a by new { engineType = a.engineType} into g
                            select new { g.Key.engineType, Average = g.Average(p=>p.hppl)}
                            ;
                            
            foreach (var ee in results23) MessageBox.Show(ee.engineType + ": " + ee.Average);


            Predicate<Car> arg2 = IsTDI;
            Func<Car,Car,int> arg1 = CompareHP;

            Action<Car> arg3 = Printer;

            myCars.Sort(new Comparison<Car>(arg1));
            myCars.FindAll(arg2).ForEach(arg3);
            var myBindingCars =  new MyBindingList(myCars);


            String sortBy = "horsePower";

            PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(typeof(Car));

            //Get property descriptor for current property
            PropertyDescriptor pd = pdc[sortBy];
            if(pd==null)
            {
                pdc = TypeDescriptor.GetProperties(typeof(Engine));
                pd = pdc[sortBy];
            }
            myBindingCars.Sorter(pd,ListSortDirection.Descending);
            var carlistSorted = myBindingCars.sortedList;
            var carlistOriginal = myBindingCars.originalList;
            String sortString = "";
            String originalString = "";
            foreach (var car in carlistOriginal)
            {
                originalString += car.model + car.motor.model + car.motor.horsePower + car.motor.displacement + car.year + "\n";
            }
            foreach (var car in carlistSorted)
            {
                sortString += car.model + car.motor.model + car.motor.horsePower + car.motor.displacement + car.year + "\n";
            }
           // MessageBox.Show(originalString, "original");
            //MessageBox.Show(sortString, "posortowane by model");
            dataGridView1.DataSource = carlistSorted;
            PropertyDescriptorCollection pdc2 = TypeDescriptor.GetProperties(typeof(Car));

            //Get property descriptor for current property
            PropertyDescriptor pd2 = pdc2["model"];
            MessageBox.Show(myBindingCars.Finder(pd2,"E350").ToString());
        }
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}