class Owner : Account
{
    private Vehicle[] vehicle = { };
    public Owner() { }

    public Owner(string id, string fullname, string address, string phone, Vehicle[] vehicle) : base(id, fullname, address, phone) 
    {
        this.vehicle = vehicle;
    }


    public override void Display()
    {
        base.Display();
        Console.WriteLine("Here is your vehicles:");
        Console.WriteLine($"{"ID Car", -10} {"Brand", -15} {"Travel Distance", -20} {"Day buy", -15} {"Insurance"}");
        foreach (var v in vehicle)
        {
            v.display();
        }
    }

    public override void Menu()
    {
        Console.Title = "Owner";
        Console.BackgroundColor = ConsoleColor.Cyan;
        Console.ForegroundColor = ConsoleColor.Black;
        string choice = "";
    Login:
        Console.WriteLine("Hello!\nPlease login!");
        if (!Login("dbo.Information"))
        {
            Console.ReadKey();
            Console.Clear();
            goto Login;
        }


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
