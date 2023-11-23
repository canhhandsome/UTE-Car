using System.Runtime.InteropServices;

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
    public void ReturnCar() { }

    public void SortRentCost() { }
    public void SortNameCar() { }
    public void FindCar() { }
    public void FindOwner() { }
    public void FindRentCost(int min, int max) { }

    public void DisplayVehicle() { }
    public void DisplayCustomer() { }
    public void DisplayOwner() { }
    public void DisplayContract() { }
    public void DisplayTotal() { }

    public void ReadVehicle() { }
    public void ReadCustomer() { }
    public void ReadOwner() { }
    public void ReadContract() { }
    public void ReadTotal() { }

    public void UpdateVehicle() { }
    public void UpdateCustomer() { }
    public void UpdateOwner() { }
    public void UpdateContract() { }
    public void UpdateTotal() { }



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
            Console.WriteLine("1. Show All Vehicle");
            Console.WriteLine("2. Find Vehicle");
            Console.WriteLine("3. Sort Vehicle");
            Console.WriteLine("4. Show your contracts");
            Console.WriteLine("5. Rent Car");
            Console.WriteLine("6. Your Information");
            Console.WriteLine("7. Exit");
            Console.WriteLine("===================================");

            Console.Write("Enter your choice: ");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Show all car");
                    // Add your code for Option 1 here
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("Find Car Menu");
                    // Add your code for Option 2 here
                    break;

                case "3":
                    Console.Clear();
                    Console.WriteLine("Sorting Car Menu");
                    // Add your code for Option 3 here
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Show Contracts");
                    // Add your code for Option 3 here
                    break;
                case "5":
                    Console.Clear();
                    Console.WriteLine("Rent Car Menu");
                    // Add your code for Option 3 here
                    break;
                case "6":
                    Console.Clear();
                    customer.Display();
                    // Add your code for Option 3 here
                    break;
                case "7":
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
            Console.WriteLine("4. Exit");
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
            Console.WriteLine("3. Guest");
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
                    if(owner.LoginPage())
                    {
                        MenuOwner();
                    }
                    // Add your code for Option 2 here
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("You are Guest");
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
