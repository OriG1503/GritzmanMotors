using ViewModel;
using Model;

// See https://aka.ms/new-console-template 


internal class Program
{
    public static async void Print(dynamic personDB)
    {
        foreach (var d in await personDB.SelectAll())
        {
            Console.WriteLine(d.Id + ", ");
        }
    }

    private static void Main(String[] args)
    {
        PersonDB personDB = new PersonDB();

        //Person checkAddPerson = new Person();
        //checkAddPerson.FirstName = "נסיון1פרטי";
        //checkAddPerson.LastName = "נסיון1משפחה";
        //checkAddPerson.DateOfBirth = new DateOnly(2021, 1, 31);

        //personDB.Insert(checkAddPerson);
        //personDB.SaveChanges();

        //Print(personDB);

        //OrderDB orderDB = new OrderDB();
        //EmployeeDB employeeDB = new EmployeeDB();
        //CustomerDB customerDB = new CustomerDB();
        //PricingDB priceDB = new PricingDB();



        //Order order = new Order();
        
        //order.PriceCode = PricingDB.SelectById(1);
        //order.CustomerCode = 5;
        //order.EmployeeCode = 3;
        //order.DateOfTreatment = ;
        //order.CarReady;
        //order.DateOfOrder;


    //     LeagueDB check ADD
    //  T.LeagueName = "NewLeagueName";
    //  T.SportID = SportDB.SelectByID(4);
    //  LeagueDB.Insert(T);


    //  //      LeagueDB check UPDATE
    //  League Update = LeagueDB.SelectByID(5);
    //  Update.LeagueName = "newH";
    //  LeagueDB.Update(Update);

    //  //      LeagueDB check DELETE 
    //  League delete = LeagueDB.SelectByID(4);
    //  LeagueDB.Delete(delete);


    //  LeagueDB.SaveChanges();

}

}