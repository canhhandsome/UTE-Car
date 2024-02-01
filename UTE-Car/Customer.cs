using System.Data.SqlClient;

class Customer : Account
{
    public static int numCus = 1;
    public Customer() 
    {
        ++numCus;
        id = "c" + numCus.ToString(); 
    }

    public Customer(string id,  string fullname, string address, string phone) : base(id, fullname, address, phone)
    {
        ++numCus;
        id = "c" + numCus.ToString();
    }

}
