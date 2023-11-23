class Owner : Account
{
    private List<Vehicle> vehicle = new List<Vehicle>();
    public Owner() { }

    public Owner(string id, string fullname, string address, string phone, List<Vehicle> vehicle) : base(id, fullname, address, phone)
    {
        this.vehicle = vehicle;
    }

    public string Id
    { 
        get 
        { return id; } 
    }

    public List<Vehicle> VehicleList
    { get { return vehicle; } }

    // Methods
    /* 
    public virtual void GetInfor(string table);
    public virtual void Display();
    public virtual void LoginPage();
    public virtual void Menu();


     */
    // Methods


    public override void Display()
    {
        base.Display();
        Console.WriteLine("Here is your vehicles:");
        Console.WriteLine($"{"ID Car",-10} {"Brand",-25} {"Travel Distance",-20} {"Day buy",-15} {"Insurance", -15} {"Rent Cost"}");
        foreach (var v in vehicle)
        {
            v.display();    
        }
    }

    public override bool LoginPage()
    {
        Console.Title = "Login";
        Console.BackgroundColor = ConsoleColor.Cyan;
        Console.ForegroundColor = ConsoleColor.Black;
        string choice = "";
    Login:
        Console.WriteLine("Hello!");
        Console.WriteLine("PLease Login!!");

        if (!Login("Owner"))
        {
            Console.ReadKey();
            Console.Clear();
            goto Login;
        }
        else
        {
            GetInfor("Owner");
            Console.ReadKey();
            return true;
        }
    }

    public override void GetInfor(string table)
    {
        base.GetInfor(table);
        if(!Vehicle.GetInfor(this))
        {
            Console.WriteLine("Can't Read Data for Vehicles");

        }
    }


    public override void Menu()
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
            Console.WriteLine($"Hello, {fullname}");
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
                    Display();
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
