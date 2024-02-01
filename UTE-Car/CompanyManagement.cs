using System.Data.SqlClient;
using System.Net;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Linq;
using System.Collections.Specialized;
using System.Globalization;
using MoreLinq;

class CompanyManagement
{
    const string TableCus = "Customer";
    const string TableOwn = "Owner";
    private List<Vehicle> vehiclelist = new List<Vehicle>();
    private List<Customer> customerlist = new List<Customer>();
    private List<Owner> ownerlist = new List<Owner>();
    private List<Contract> contractlist = new List<Contract>();

    private Account account = new Account();
    private Contract contract = new Contract();

    public CompanyManagement()
    {
        ReadOwner();
        ReadCustomer();
        ReadVehicle();
        ReadContract(); 
    }

    public void RentCar(DateTime daterent)
    {
        Console.Write("Enter the ID Vehicle you want to rent: ");
        string idrent = Console.ReadLine();
        Console.Clear();
        Vehicle vehicle = new Vehicle();
        vehicle = vehiclelist.Where(v => v.Idvehicle == idrent).First();
        if (vehicle == null)
        {
            Console.WriteLine("You enter wrong ID");
            return;
        }

        vehicle.DisplayRating();

        string choice = "";
        do
        {
            Console.Title = "Login";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Choose what you want to find!");
            Console.WriteLine("1. Rent");
            Console.WriteLine("2. Return");
            Console.Write("Enter your choice: ");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateContract(vehicle, daterent);
                    Console.WriteLine("Rent Success!");
                    break;
                case "2":
                    Console.WriteLine("Returning to Menu...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            if (choice != "2")
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        } while (choice != "2");
    }

    public void CreateContract(Vehicle vehicle, DateTime daterent)
    {
        Console.Write("How many days you want to Rent: ");
        int numday = int.Parse(Console.ReadLine());
        contract = new Contract(account.Id, vehicle, daterent, numday);
        vehicle.setIsRent(true);
        contractlist.Add(contract);
        contract.AddContract();
    }

    public void RatingVehicle(Vehicle vehicle)
    {
        Rating rating = new Rating(); 
        rating.EnterYourRating(account.Id);
        rating.AddComment(vehicle);
    }

    public void ReturnCar() 
    {
        Vehicle vehicle = new Vehicle();
        Console.Write("Enter ID Vehicle you want to Return: ");
        string idreturn = Console.ReadLine();
        vehicle = vehiclelist.Where(v => v.Idvehicle == idreturn).First();
        if(vehicle == null)
        {
            Console.WriteLine("You are not rent any Vehicle have this ID!\nPlease Enter again!");
            return;
        }
        Console.Write("Would you like to leave a rate and comment of this vehicle? (Y/N)");
        string choice = Console.ReadLine().ToUpper();
        if(choice == "Y")
        {
            RatingVehicle(vehicle);
        }
        else if (choice == "N")
        {

        }
        Console.WriteLine("Thank you for using our service!!");
        vehicle.setIsRent(false);
        var clist = contractlist.OrderByDescending(c => int.Parse(c.Idcontract.Substring(2))).ToList();
        Contract contract = contractlist.Where(c => c.Vehicle.Idvehicle == vehicle.Idvehicle).First();
        contract.SetDateReturn();
        ReadContract();
    }

    public List<Vehicle> FindBrand(List<Vehicle> originalList)
    {
        Console.Write("Enter Brand: ");
        string search = Console.ReadLine();
        List<Vehicle> filteredList = originalList.Where(v => v.Brand.Contains(search)).ToList();
        return filteredList;
    }

    public List<Vehicle> FindYearBuy(List<Vehicle> originalList)
    {
        Console.Write("Enter Year Buy: ");
        string search = Console.ReadLine();
        List<Vehicle> filteredList = originalList.Where(v => v.DayBuy.Year.ToString() == search).ToList();
        return filteredList;
    }

    public List<Vehicle> FindInsurance(List<Vehicle> originalList)
    {
        bool ins = true;
        Console.Write("Insurance(YES/NO) Y/N: ");
        string search = Console.ReadLine().ToUpper();
        if (search == "Y") ins = true;
        else if (search == "N") ins = false;
        else Console.WriteLine("Wrong input!\nPlease Enter Again");
        List<Vehicle> filteredList = originalList.Where(v => v.Insurance == ins).ToList();
        return filteredList;
    }

    public List<Vehicle> FindDayReturn(List<Vehicle> originalList, DateTime rent)
    {
        List<Contract> expiredContracts = contractlist.Where(contract => rent > contract.DateExpire() && contract.DateReturn == DateTime.MinValue).ToList();
        List<Contract> distinctExpiredContracts = expiredContracts.DistinctBy(contract => contract.Vehicle.Idvehicle).ToList(); 
        List<Vehicle> filteredList = new List<Vehicle>();
        foreach(Vehicle vehicle in originalList)
        {
            foreach(Contract contract in distinctExpiredContracts)
            {
                if (vehicle.Idvehicle == contract.Vehicle.Idvehicle && vehicle.IsRented)
                    filteredList.Add(vehicle);
            }
            if(!vehicle.IsRented)
                filteredList.Add(vehicle);
        }
        return filteredList;
    }

    public List<Vehicle> FindRentCost(List<Vehicle> originalist)
    {
        List<Vehicle> result = new List<Vehicle>();
        Console.Write("Min price: ");
        double min = double.Parse(Console.ReadLine());
        Console.Write("Max price: ");
        double max = double.Parse(Console.ReadLine());
        foreach (Vehicle vehicle in originalist)
        {
            if (vehicle.RentCost() >= min && vehicle.RentCost() <= max)
            {
                result.Add(vehicle);
            }
        }
        return result;
    }

    public void MenuRentCar()
    {
        string choice = "";
    InputDayRent:
        ReadVehicle();
        DateTime result;
        Console.Write("Enter a date (format: dd/MM/yyyy): ");
        string userInput = Console.ReadLine();

        if (DateTime.TryParseExact(userInput, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
        {
            vehiclelist = FindDayReturn(vehiclelist, result);
        }
        List<Vehicle> tmp = vehiclelist;
        do
        {
            Console.Title = "Login";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Clear();
            DisplayVehicle();
            Console.WriteLine("Choose what you want to find!");
            Console.WriteLine("1. Brand");
            Console.WriteLine("2. Year Buy");
            Console.WriteLine("3. Insurance");
            Console.WriteLine("4. Input your date you want to rent again");
            Console.WriteLine("5. Range of Price");
            Console.WriteLine("6. Remove all filter");
            Console.WriteLine("7. Choose vehicle");
            Console.WriteLine("8. Return ");
            Console.Write("Enter your choice: ");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    vehiclelist = FindBrand(vehiclelist);
                    break;
                case "2":
                    vehiclelist = FindYearBuy(vehiclelist);
                    break;
                case "3":
                    vehiclelist = FindInsurance(vehiclelist);
                    break;
                case "4":
                    Console.Clear();
                    goto InputDayRent;
                    break;
                case "5":
                    vehiclelist = FindRentCost(vehiclelist);
                    break;
                case "6":
                    vehiclelist = tmp;
                    Console.WriteLine("Remove all filter success!");
                    break;
                case "7":
                    RentCar(result);
                    break;
                case "8":
                    Console.WriteLine("Returning to Menu...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            if(choice != "8")
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }

        } while (choice != "8");
    }

    public void MenuReturnCar()
    {
        List<Contract> contracts = contractlist.Where(c => c.Idcustomer == account.Id).ToList();
        account.DisplayContract(contracts);
        contracts = contracts.Where(c => c.DateReturn != DateTime.MinValue).ToList();
        string choice = "";
        do
        {
            Console.Title = "Login";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Choose what you want to find!");
            Console.WriteLine("1. Return Vehicle");
            Console.WriteLine("2. Return");
            Console.Write("Enter your choice: ");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ReturnCar();
                    Console.WriteLine("Return Success!");
                    choice = "2";
                    break;
                case "2":
                    Console.WriteLine("Returning to Menu...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            if (choice != "2")
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        } while (choice != "2");
    }

    public void SortVehicle()
    {
        var result = vehiclelist.OrderBy(v => int.Parse(v.Idvehicle.Substring(1))).ToList();
        vehiclelist = result;
    }

    public void DisplayVehicle() 
    {
        Console.Clear();
        Console.WriteLine($"{"ID",-5} | {"Brand",-22} | {"TravelDistance",-15} | {"DayBuy",-12} | {"Insurance",-12} | {"RentCost",-12} | {"IsRent"} |");
        foreach (Vehicle vehicle in vehiclelist)
        {
            Console.WriteLine("------------------------------------------------------------------------------------------------------");
            vehicle.display();
        }
        Console.WriteLine("------------------------------------------------------------------------------------------------------");
    }

    public void DisplayCustomer() 
    {
        Console.Clear();

        foreach (Customer customer in customerlist)
        {
            customer.Display();
            Console.WriteLine("---------------------------");
        }
    }
    
    public void DisplayOwner() 
    {
        Console.Clear();
        Console.WriteLine("All Owners:");

        foreach (Owner owner in ownerlist)
        {
            owner.Display();
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------");
        }
    }

    public void DisplayContract() 
    {
        Console.Clear();
        foreach (Contract contract in contractlist)
        {
            contract.Display();
            Console.WriteLine("------------------------------------------------------------------------------------------------");
        }
    }

    public void ReadVehicle() 
    {
        vehiclelist.Clear();
        foreach (Owner owner in ownerlist)
        {
            owner.VehicleList.Clear();
            Vehicle.GetInfor(owner);
            foreach(Vehicle vehicle in owner.VehicleList)
            {
                vehiclelist.Add(vehicle);
            }
        }
        SortVehicle();
    }

    public void ReadCustomer() 
    {
        string connectionString = "Server=LAPTOP-Q3MNC1CJ;Database=UTE_Car;Trusted_Connection=true";
        string table = TableCus;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string idcolumn = "id" + table;
            using (SqlCommand command = new SqlCommand($"SELECT {idcolumn}, fullname, address, phone, username, password FROM dbo.{table} ", connection))
            {

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Customer customer = new Customer();
                        customer.Id = reader[idcolumn].ToString().Trim();
                        customer.Fullname = reader["fullname"].ToString().Trim();
                        customer.Address = reader["address"].ToString().Trim();
                        customer.Phone = reader["phone"].ToString().Trim();
                        customer.Username = reader["username"].ToString().Trim();
                        customer.Password = reader["password"].ToString().Trim();
                        customerlist.Add(customer);
                    }
                }
            }
        }

    }
    
    public void ReadOwner() 
    {
        string connectionString = "Server=LAPTOP-Q3MNC1CJ;Database=UTE_Car;Trusted_Connection=true";
        string table = TableOwn;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string idcolumn = "id" + table;
            using (SqlCommand command = new SqlCommand($"SELECT {idcolumn}, fullname, address, phone, username, password FROM dbo.{table} ", connection))
            {

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Owner owner = new Owner();
                        owner.Id = reader[idcolumn].ToString().Trim();
                        owner.Fullname = reader["fullname"].ToString().Trim();
                        owner.Address = reader["address"].ToString().Trim();
                        owner.Phone = reader["phone"].ToString().Trim();
                        owner.Username = reader["username"].ToString().Trim();
                        owner.Password = reader["password"].ToString().Trim();
                        ownerlist.Add(owner);
                    }
                }
            }
        }
    }
    
    public void ReadContract() 
    {
        contract.GetInfor(contractlist);
    }


    public void MenuCustomer()
    {
        while (true)
        {
            Console.Title = "Customer";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Clear();
            Console.WriteLine($"Hello, {account.Fullname}");
            Console.WriteLine("===================================");
            Console.WriteLine("Choose what you want to do: ");
            Console.WriteLine("1. Show your contracts");
            Console.WriteLine("2. Rent Car");
            Console.WriteLine("3. Your Information");
            Console.WriteLine("4. Exit");
            Console.WriteLine("===================================");


            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    MenuReturnCar();
                    break;
                case "2":
                    MenuRentCar();
                    break;
                case "3":
                    account.Display();
                    break;
                case "4":
                    Console.WriteLine("Thank you for visiting\nGoodbye!");
                    Console.ResetColor(); 
                    return; 
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    public void MenuOwner()
    {
        Console.Title = "Owner";
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Magenta;
        string choice = "";

        while (true)
        {
            Console.Title = "Owner";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Clear();
            Console.WriteLine($"Hello, {account.Fullname}");
            Console.WriteLine("===================================");
            Console.WriteLine("Who are you?");
            Console.WriteLine("1. Show your contracts");
            Console.WriteLine("2. Find Contract");
            Console.WriteLine("3. Your Information");
            Console.WriteLine("4. Return");
            Console.WriteLine("===================================");

            Console.Write("Enter your choice: ");

            choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    account.DisplayContract(contractlist);
                    break;
                case "2":
                    Console.WriteLine("Find Contract");
                    break;
                case "3":
                    account.Display();
                    break;
                case "4":
                    Console.WriteLine("Thank you for visiting\nGoodbye!");
                    Console.ResetColor(); 
                    return; 
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    public void MenuAdmin()
    {
        Console.Title = "Admin";
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Magenta;

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Hello, Admin");
            Console.WriteLine("===================================");
            Console.WriteLine("1. Show all Owners");
            Console.WriteLine("2. Show all Customers");
            Console.WriteLine("3. Show all Vehicle");
            Console.WriteLine("4. Show all Contract");
            Console.WriteLine("5. Return");
            Console.WriteLine("===================================");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayOwner();
                    break;
                case "2":
                    DisplayCustomer();
                    break;
                case "3":
                    DisplayVehicle();
                    break;
                case "4":
                    DisplayContract();
                    break;
                case "5":
                    Console.WriteLine("Thank you for visiting. Goodbye!");
                    Console.ResetColor();
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    public bool CheckAccount()
    {
        List<Account> accounts = new List<Account>();
        if(account is Owner)
        {
            accounts = ownerlist.Cast<Account>().ToList();
        }
        else if(account is Customer)
        {
            accounts = customerlist.Cast<Account>().ToList();
        }
        
        foreach(var acc in accounts)
        {
            if(acc.Username == account.Username && acc.Password == account.Password)
            {
                account = acc;
                return true;
            }
        }
        return false;
    }

    public bool LoginPage()
    {
        string choice = "";
        do
        {
            Console.Title = "Login";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.Clear();
            Console.WriteLine("Hello!");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("3. Return ");
            Console.Write("Enter your choice: ");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    account.Login();
                    if (CheckAccount())
                    {
                        Console.WriteLine("Login Success!!!");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Login Failed. Please try again.");
                        Console.ReadKey();
                    }
                    break;
                case "2":
                    account.AddAccount();
                    break;
                case "3":
                    Console.WriteLine("Returning to Menu Login...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
            if (choice != "3")
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }

        } while (choice != "3");
        return false;
    }

    public void MenuLogin()
    {
        while (true)
        {

            Console.Title = "UTE-CAR";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Clear();
            Console.WriteLine("\tWelcome to UTE-Car");
            Console.WriteLine("===================================");
            Console.WriteLine("Who are you?");
            Console.WriteLine("1. Customer");
            Console.WriteLine("2. Owner");
            Console.WriteLine("3. Admin");
            Console.WriteLine("4. Exit");
            Console.WriteLine("===================================");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("You are Customer");
                    account = new Customer();
                    if(LoginPage())
                    {
                        MenuCustomer();
                    }
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("You are Owner");
                    account = new Owner();
                    if (LoginPage())
                    {
                        MenuOwner();
                    }
                    break;
                case "3":
                    Console.Clear();
                    string username, password;
                    Console.Write("Enter Username: ");
                    username = Console.ReadLine();
                    Console.Write("Enter Password: ");
                    password = Console.ReadLine();
                    if(username == password && username ==  "admin")
                    {
                        MenuAdmin();
                    }
                    else
                    {
                        Console.WriteLine("Wrong Username or Password!!");
                        Console.ReadKey();
                    }
                    break;
                case "4":
                    Console.WriteLine("Thank you for visiting\nGoodbye!");
                    Console.ResetColor(); 
                    return; 
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
