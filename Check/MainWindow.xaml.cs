using System;
using System.Collections.Generic;
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
using ViewModel;
using Model;

namespace Check
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Activate1();
        }

        public async void Activate1()
        {
            OrderDB xDB = new OrderDB();

            Pricing p = await PricingDB.SelectById(2);
            Customer c = await CustomerDB.SelectById(6);
            Employee e = await EmployeeDB.SelectById(3);
            //CarModel m = await CarModelDB.SelectById(5);

            //Order x = new Order();
            //x.PriceCode = p;
            //x.CustomerCode = c;
            //x.EmployeeCode = e;
            //x.DateOfTreatment = new DateOnly(2021, 05, 15);
            //x.CarReady = false;
            //x.DateOfOrder = DateTime.Now;
            //xDB.Insert(x);

            //Order x = await OrderDB.SelectById(23);
            //x.PriceCode = p;
            //x.CustomerCode = c;
            //x.EmployeeCode = e;
            //x.DateOfTreatment = new DateOnly(2020, 05, 15);
            //x.CarReady = true;
            //x.DateOfOrder = DateTime.Now;

            //xDB.Update(x);
            //xDB.Delete(x);
            await xDB.SaveChanges();

            var t = await new OrderDB().SelectAll();
            dataListView.ItemsSource = t;



















            //CustomerDB xDB = new CustomerDB();

            //Person y = await PersonDB.SelectById(4);

            ////Customer x = new Customer();
            ////x.FirstName = "נסיון 1.1";
            ////x.LastName = "נסיון 1.2";
            ////x.DateOfBirth = new DateOnly(2000, 2, 25);
            ////x.PhoneNumber = "05480";
            ////xDB.Insert(x);

            // Customer x = await CustomerDB.SelectById(33);
            ////CarCompany y = await CarCompanyDB.SelectById(4);
            //x.FirstName = "נסיון 2.1";
            //x.LastName = "נסיון 2.2";
            //x.DateOfBirth = new DateOnly(2009, 5, 25);
            //x.PhoneNumber = "000000";


            ////xDB.Update(x);
            //xDB.Delete(x);
            //await xDB.SaveChanges();

            //dataListView.ItemsSource =await new CustomerDB().SelectAll();
        }

    }
}

