using System.Data.SqlClient;

class Customer : Account
{ 
    private List<Vehicle> vehiclerent = new List<Vehicle>();
    public Customer() { }

    public Customer(string id,  string fullname, string address, string phone) : base(id, fullname, address, phone) 
    {
        this.id = id;
    }

    public void RentCar()
    {

    }

    

    // Methods
    /* 
    public override void Register(string table);
    public override Boolean Login(string table);
    public override void GetInfor(string table);
    public void RentCar();
    public Boolean CheckAccount(string table);
    public override void Display();
    public override void LoginPage();
    public override void Menu();


     */
    // Methods


    public override bool LoginPage()
    {
        Console.Title = "Login";
        Console.BackgroundColor = ConsoleColor.Cyan;
        Console.ForegroundColor = ConsoleColor.Black;
        string choice = "";
    Login:
        Console.WriteLine("Hello!");
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Register");
        Console.WriteLine("3. Return ");
        Console.Write("Enter your choice: ");
        choice = Console.ReadLine();

        if (choice == "1")
        {
            if (!Login("Customer"))
            {
                Console.ReadKey();
                Console.Clear();
                goto Login;
            }
            else
            {
                GetInfor("Customer");
                return true;
            }
        }
        else if (choice == "2")
        {
            Register("Customer");
            return true;
        }
        else if(choice == "3") 
        { 
            Console.WriteLine("Returning to Menu Login...");
            return false; 
        }
        return false;
    }


    
}
