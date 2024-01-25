using System.Data.SqlClient;
using System.Runtime.CompilerServices;

class Account
{
    protected string id;
    protected string fullname;
    protected string address;
    protected string phone;
    protected string username;
    protected string password;
    protected string email;
    protected string phone2;
    protected string username1;
    protected string password1;
    protected string password2;
    protected string email2;
    protected  string email3;


    public Account() { }

    public Account(string id, string fullname, string address, string phone)
    {
        this.id = id;
        this.fullname = fullname;
        this.address = address;
        this.phone = phone;
    }


    public virtual string Id
    {
        get { return id; }
        set { id = value; }
    }

    public virtual string Fullname
    {
        get { return fullname; }
        set { fullname = value; }
    }

    public virtual string Address
    {
        get { return address; }
        set { address = value; }
    }

    public virtual string Phone
    {
        get { return phone; }
        set { phone = value; }
    }

    // Methods
    /* 
    public virtual void Register(string table);
    public virtual Boolean Login(string table);
    public virtual void GetInfor(string table)
    public Boolean CheckAccount(string table)
    public virtual void Display()
    public virtual void LoginPage()
    public virtual void Menu()


     */
    // Methods
    public virtual void Register(string table)
    {

        string connectionString = "Server=LAPTOP-Q3MNC1CJ;Database=UTE_Car;Trusted_Connection=true";

        Console.Write("Enter your Username: ");
        this.username = Console.ReadLine();
        Console.Write("Enter your Password: ");
        this.password = Console.ReadLine();
        Console.Write("Enter your id: ");
        this.id = Console.ReadLine();
        Console.Write("Enter your fullname: ");
        this.fullname = Console.ReadLine();
        Console.Write("Enter your address: ");
        this.address = Console.ReadLine();
        Console.Write("Enter your phone number");
        this.phone = Console.ReadLine();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
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

    public virtual Boolean Login(string table)
    {
        Console.Write("Enter your Username: ");
        this.username = Console.ReadLine();
        Console.Write("Enter your Password: ");
        this.password = Console.ReadLine();
        if (CheckAccount(table))
        {
            Console.WriteLine("Login Success!!!");
            return true;
        }
        Console.WriteLine("Wrong Username or Password!!");
        return false;
    }

    public virtual void GetInfor(string table)
    {
        string connectionString = "Server=LAPTOP-Q3MNC1CJ;Database=UTE_Car;Trusted_Connection=true";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string idcolumn = "id" + table;
            using (SqlCommand command = new SqlCommand($"SELECT {idcolumn}, fullname, address, phone FROM dbo.{table} WHERE username = @username", connection))
            {
                command.Parameters.AddWithValue("@username", this.username);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = reader[idcolumn].ToString();
                        fullname = reader["fullname"].ToString();
                        address = reader["address"].ToString();
                        phone = reader["phone"].ToString();
                    }
                }
            }
        }
    }




    public Boolean CheckAccount(string table)
    {
        string connectionString = "Server=LAPTOP-Q3MNC1CJ;Database=UTE_Car;Trusted_Connection=true";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand($"SELECT username, password FROM dbo.{table} WHERE username = @username AND password = @password", connection))
            {
                command.Parameters.AddWithValue("@username", this.username);
                command.Parameters.AddWithValue("@password", this.password);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return true; // Matching username and password found
                    }
                }
            }
        }

        return false; // No matching account found
    }


    public virtual void Display()
    {
        Console.WriteLine("ID: " + id);
        Console.WriteLine("Fullname: " + fullname);
        Console.WriteLine("Address: " + address);
        Console.WriteLine("Phone number: " + phone);
    }

    public virtual bool LoginPage()
    {

        return true;
    }


    public virtual void Menu()
    {

    }


    ~Account() { }
}
