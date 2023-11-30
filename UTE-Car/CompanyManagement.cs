using System.Data.SqlClient;
using System.Net;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Linq;
using System.Collections.Specialized;

class CompanyManagement
{
    private List<Vehicle> vehiclelist = new List<Vehicle>();
    private List<Customer> customerlist = new List<Customer>();
    private List<Owner> ownerlist = new List<Owner>();
    private List<Contract> contractlist = new List<Contract>();

    private Customer customer = new Customer();
    private Owner owner = new Owner();

    public CompanyManagement()
    {
        ReadOwner("Owner");
        ReadCustomer("Customer");
        ReadVehicle();
        ReadContract(); 
    }


    //Methods
    /*
    public void RentCar();
    public void ReturnCar();

    public void SortRentCost();
    public void SortNameCar();
    public void FindCar();
    public void FindOwner();
    public void FindRentCost(int min, int max);
    
    public void DisplayVehicle();
    public void DisplayCustomer();
    public void DisplayOwner();
    public void DisplayContract();
    public void DisplayTotal();

    public void ReadVehicle();
    public void ReadCustomer();
    public void ReadOwner();
    public void ReadContract();
    public void ReadTotal();
    
    public void UpdateVehicle();
    public void UpdateCustomer();
    public void UpdateOwner();
    public void UpdateContract();
    public void UpdateTotal();
    
    public void MenuLogin();
     */
    //Methods
    


    public void RentCar()
    {
        Console.Write("Enter the ID Vehicle you want to rent: ");
        string idrent = Console.ReadLine();
        bool isfind = false;
        foreach (var vehicle in vehiclelist)
        {
            if (vehicle.Idvehicle == idrent && vehicle.IsRented == false)
            {
                Console.WriteLine("This is your car you want to rent");
                vehicle.display();
                Console.WriteLine("Y/N");
                string choice = Console.ReadLine();
                if (choice == "Y")
                {
                    Console.Write("How many days you want to rent? ");
                    int numdayrent = int.Parse(Console.ReadLine());
                    Contract contract = new Contract(customer.Id, vehicle, DateTime.Now, numdayrent, -10);
                    contract.AddContract();
                    contract.Display();
                    isfind = true;
                }
                else
                {
                    Console.WriteLine("Let's find vehicle again");
                    return;
                }
            }

        }
        if(!isfind)
        {
            Console.WriteLine("Yor car finding is rented now or we don't have the car you want!");
            Console.WriteLine("Let's find vehicle again");
        }
    }

    public void ReturnCar() 
    {

    }

    public void SortRentCost() { }
    public void SortIDCar()
    {
        var result = vehiclelist.OrderBy(v => int.Parse(v.Idvehicle.Substring(1))).ToList();
        vehiclelist = result;
    }

    public void FindCar(string search)
    {
        List<Vehicle> originalList = new List<Vehicle>(vehiclelist);
        List<Vehicle> filteredList = originalList.Where(v => v.Brand.Contains(search)).ToList();
        vehiclelist = filteredList;
        // Display the filtered results
        DisplayVehicle();
        
        ReadVehicle();
    }

    public void FindVehicle()
    {
        Console.WriteLine("You choose find vehicle");
    }

    public void SortVehicle()
    {
        Console.WriteLine("You choose sort vehicle");
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
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------");
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
                //vehicle.display();
            }
        }
        SortIDCar();
    }

    public void ReadCustomer(string table) 
    {
        string connectionString = "Server=LAPTOP-Q3MNC1CJ;Database=UTE_Car;Trusted_Connection=true";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string idcolumn = "id" + table;
            using (SqlCommand command = new SqlCommand($"SELECT {idcolumn}, fullname, address, phone FROM dbo.{table} ", connection))
            {

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Customer customer = new Customer();
                        customer.Id = reader[idcolumn].ToString();
                        customer.Fullname = reader["fullname"].ToString();
                        customer.Address = reader["address"].ToString();
                        customer.Phone = reader["phone"].ToString();
                        customerlist.Add(customer);
                    }
                }
            }
        }

    }
    
    public void ReadOwner(string table) 
    {
        string connectionString = "Server=LAPTOP-Q3MNC1CJ;Database=UTE_Car;Trusted_Connection=true";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string idcolumn = "id" + table;
            using (SqlCommand command = new SqlCommand($"SELECT {idcolumn}, fullname, address, phone FROM dbo.{table} ", connection))
            {

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Owner owner = new Owner();
                        owner.Id = reader[idcolumn].ToString();
                        owner.Fullname = reader["fullname"].ToString();
                        owner.Address = reader["address"].ToString();
                        owner.Phone = reader["phone"].ToString();
                        ownerlist.Add(owner);
                    }
                }
            }
        }
    }
    
    public void ReadContract() 
    {
        Contract.GetInfor("Contract", contractlist);
    }

    public void UpdateVehicle() { }
    public void UpdateCustomer() { }
    public void UpdateOwner() { }
    public void UpdateContract() { }
    public void UpdateTotal() { }

    public void RentCarMenu()
    {
        while (true)
        {
            Console.Title = "Rent Car";
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            DisplayVehicle();
            Console.WriteLine($"Hello, {customer.Fullname}");
            Console.WriteLine("===================================");
            Console.WriteLine("Choose what you want to do: ");
            Console.WriteLine("1. Find Car");
            Console.WriteLine("2. Sort Car");
            Console.WriteLine("3. Rent Car");
            Console.WriteLine("4. Exit");
            Console.WriteLine("===================================");


            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    FindVehicle();
                    // Add your code for Option 3 here
                    break;
                case "2":
                    Console.Clear();
                    SortVehicle(); 
                    // Add your code for Option 3 here
                    break;
                case "3":
                    RentCar();
                    // Add your code for Option 3 here
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Thank you for visiting\nGoodbye!");
                    Console.ResetColor(); // Reset console colors
                    return; // Exit the program
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    public void MenuCustomer()
    {
        while (true)
        {
            Console.Title = "Customer";
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.WriteLine($"Hello, {customer.Fullname}");
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
                    Console.Clear();
                    Console.WriteLine("Show Contracts");
                    // Add your code for Option 3 here
                    break;
                case "2":
                    Console.Clear();
                    RentCarMenu();
                    // Add your code for Option 3 here
                    break;
                case "3":
                    Console.Clear();
                    customer.Display();
                    // Add your code for Option 3 here
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Thank you for visiting\nGoodbye!");
                    Console.ResetColor(); // Reset console colors
                    return; // Exit the program
                default:
                    Console.Clear();
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
        Console.BackgroundColor = ConsoleColor.Cyan;
        Console.ForegroundColor = ConsoleColor.Black;
        string choice = "";

        while (true)
        {
            Console.Title = "Owner";
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.WriteLine($"Hello, {owner.Fullname}");
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
                    Console.Clear();
                    Console.WriteLine("Show your contracts");
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("Find Contract");
                    break;
                case "3":
                    Console.Clear();
                    owner.Display();
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Thank you for visiting\nGoodbye!");
                    Console.ResetColor(); // Reset console colors
                    return; // Exit the program
                default:
                    Console.Clear();
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
        Console.BackgroundColor = ConsoleColor.Cyan;
        Console.ForegroundColor = ConsoleColor.Black;

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
                    Console.Clear();
                    Console.WriteLine("Thank you for visiting. Goodbye!");
                    Console.ResetColor();
                    return;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }


    private void DisplayAdminInformation()
    {
        Console.Clear();
        Console.WriteLine("Your Information:");
        owner.Display(); // Assuming "owner" is the current admin
    }

    public void MenuLogin()
    {
        while (true)
        {

            Console.Title = "UTE-CAR";
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
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
                    if(customer.LoginPage())
                    {
                        MenuCustomer();
                    }
                    // Add your code for Option 1 here
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("You are Owner");
                    owner = new Owner();
                    if (owner.LoginPage())
                    {
                        MenuOwner();
                    }
                    else break;
                    // Add your code for Option 2 here
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
                    // Add your code for Option 3 here
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Thank you for visiting\nGoodbye!");
                    Console.ResetColor(); // Reset console colors
                    return; // Exit the program
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    
}
