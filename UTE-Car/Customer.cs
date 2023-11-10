using System.Data.SqlClient;

class Customer : Account
{ 
    public Customer() { }

    public Customer(string id,  string fullname, string address, string phone) : base(id, fullname, address, phone) 
    {
        this.id = id;
    }

    public void RentCar()
    {
        
    }

    public override void Display()
    {
        base.Display();
    }

    public override void LoginPage()
    {
        Console.Title = "Login";
        Console.BackgroundColor = ConsoleColor.Cyan;
        Console.ForegroundColor = ConsoleColor.Black;
        string choice = "";
    Login:
        Customer c = new Customer();
        Console.WriteLine("Hello!");
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Register");
        Console.Write("Enter your choice: ");
        choice = Console.ReadLine();

        if (choice == "1")
        {
            if (!c.Login("dbo.Customer"))
            {
                Console.ReadKey();
                Console.Clear();
                goto Login;
            }
            else
            {
                c.GetInfor("dbo.Customer");
                c.Menu();
            }
        }
        else if (choice == "2")
        {
            c.Register("dbo.Customer");
        }
    }


    public override void Menu()
    {
        Console.Title = "Customer";
        Console.BackgroundColor = ConsoleColor.Cyan;
        Console.ForegroundColor = ConsoleColor.Black;
        string choice = "";

        while (true)
        {
            Console.Title = "Customer";
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.WriteLine($"Hello, {fullname}");
            Console.WriteLine("===================================");
            Console.WriteLine("Who are you?");
            Console.WriteLine("1. Show All Vehicle");
            Console.WriteLine("2. Find Vehicle");
            Console.WriteLine("3. Show your contracts");
            Console.WriteLine("4. Find Contract");
            Console.WriteLine("5. Your Information");
            Console.WriteLine("6. Exit");
            Console.WriteLine("===================================");

            Console.Write("Enter your choice: ");

            choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Show Vehicles");
                    // Add your code for Option 1 here
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("Find Vehicles");
                    // Add your code for Option 2 here
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("Show your contracts");
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Find contracts");
                    break;
                case "5":
                    Console.Clear();
                    Display();
                    // Add your code for Option 3 here
                    break;
                case "6":
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
