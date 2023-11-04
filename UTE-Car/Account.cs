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

    public Account() { }

    public Account(string id, string fullname, string address, string phone)
    {
        this.id = id;
        this.fullname = fullname;
        this.address = address;
        this.phone = phone;
    }

    public virtual void Register(string table)
    {

        string connectionString = "Server=LAPTOP-Q3MNC1CJ;Database=TestDatabase;Trusted_Connection=true";

        Console.Write("Enter your Username: ");
        this.username = Console.ReadLine();
        Console.Write("Enter your Password: ");
        this.password = Console.ReadLine();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand($"INSERT INTO dbo.{table} (UsernameL, PasswordL) VALUES (@username, @password)", connection))
            {
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
        if(CheckAccount(table))
        {
            Console.WriteLine("Login Success!!!");
            return true;
        }
        Console.WriteLine("Wrong Username or Password!!");
        return false;
    }



    public Boolean CheckAccount(string table)
    {
        string connectionString = "Server=LAPTOP-Q3MNC1CJ;Database=TestDatabase;Trusted_Connection=true";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand($"SELECT * FROM {table} WHERE UsernameL = @username AND PasswordL = @password", connection))
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


    public virtual void Menu()
    {
        
    }


    ~Account() {  }
}
