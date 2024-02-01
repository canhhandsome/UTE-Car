using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Security.Principal;

class Account
{
    protected string id;
    protected string fullname;
    protected string address;
    protected string phone;
    protected string username;
    protected string password;
    protected List<Vehicle> vehicle = new List<Vehicle>();

    public Account() { }

    public Account(string id, string fullname, string address, string phone)
    {
        this.id = id;
        this.fullname = fullname;
        this.address = address;
        this.phone = phone;
    }

    public string Username
    {
        get { return this.username; }
        set { this.username = value; }
    }
    public string Password
    {
        get { return this.password; }
        set { this.password = value; }
    }

    public string Id
    {
        get { return id; }
        set { id = value; }
    }

    public string Fullname
    {
        get { return fullname; }
        set { fullname = value; }
    }

    public string Address
    {
        get { return address; }
        set { address = value; }
    }

    public string Phone
    {
        get { return phone; }
        set { phone = value; }
    }

    public List<Vehicle> vehicles { get { return vehicles; } }

    public virtual void DisplayContract(List<Contract> contractlist)
    {
        Console.WriteLine($"{"idcontract",-13} | {"idcustomer",-13} | {"idvehicle",-13} | {"promotion",-15} | {"downpayment",-20} | {"day rent",-15} | {"day return",-15} | {"Number day rent", -15} | {"Total"}");
        Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------------------------");
        foreach (Contract contract in contractlist)
        {
            contract.Display();
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------------------------------");
        }
    }

    public virtual void AddAccount()
    {
        string connectionString = "Server=LAPTOP-Q3MNC1CJ;Database=UTE_Car;Trusted_Connection=true";
        string table = this.GetType().ToString();
        Console.Write("Enter your Username: ");
        this.username = Console.ReadLine();
        Console.Write("Enter your Password: ");
        this.password = Console.ReadLine();
        Console.Write("Enter your fullname: ");
        this.fullname = Util.StandardizeName(Console.ReadLine());
        Console.Write("Enter your address: ");
        this.address = Util.StandardizeName(Console.ReadLine());
        Console.Write("Enter your phone number: ");
        this.phone = Console.ReadLine();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            Customer.numCus++;
            Console.WriteLine(Customer.numCus);
            Console.ReadKey();
            connection.Open();
            string idcolumn = "id" + table;
            using (SqlCommand command = new SqlCommand($"INSERT INTO dbo.{table} ({idcolumn}, fullname, address, phone, username, password) VALUES (@id, @fullname, @address, @phone, @username, @password)", connection))
            {
                command.Parameters.AddWithValue("@id", this.id);
                command.Parameters.AddWithValue("@fullname", this.fullname);
                command.Parameters.AddWithValue("@address", this.address);
                command.Parameters.AddWithValue("@phone", this.phone);
                command.Parameters.AddWithValue("@username", this.username);
                command.Parameters.AddWithValue("@password", this.password);
                command.ExecuteNonQuery();
            }
        }
        Console.WriteLine("Register Success!!!");
    }


    public void Login()
    {
        Console.Write("Enter your Username: ");
        this.username = Console.ReadLine();
        Console.Write("Enter your Password: ");
        this.password = Console.ReadLine();
    }

    public void Display()
    {
        Console.WriteLine("ID: " + id);
        Console.WriteLine("Fullname: " + fullname);
        Console.WriteLine("Address: " + address);
        Console.WriteLine("Phone number: " + phone);
    }



    ~Account() { }
}
