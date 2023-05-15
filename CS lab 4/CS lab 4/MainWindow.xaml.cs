using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace CS_lab_4
{
    public class Engine
    {
        public double displacement;
        public double horsePower;
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

    public class Car
    {

        public string model = "";
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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
            BindingSource carBindingSource = new s();
            carBindingSource.DataSource = myCarsBindingList;
            //Drag a DataGridView control from the Toolbox to the form.
            dataGridView1.DataSource = carBindingSource;
        }
    }
}
