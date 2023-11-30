using System.Collections.Generic;
using System.Data.SqlClient;

class Contract
{
    private string idcontract;
    private string idcustomer;
    private Vehicle vehicle;
    private double promotion;
    private double downpayment;
    private DateTime daterent;
    private DateTime datereturn;
    private int numberdayrent;
    public static int numContract = 0;

    public Contract() { }
    public Contract(string customer, Vehicle vehicle, DateTime daterent, int numberdayrent, double promotion)
    {
        ++numContract;
        this.idcontract = "ct" + numContract.ToString();
        this.idcustomer = customer;
        this.vehicle = vehicle;
        this.daterent = daterent;
        this.downpayment = DownPayment();
        this.numberdayrent = numberdayrent;
        this.promotion = promotion;
    }

    public string Idcontract
    { get { return idcontract; } }

    public string Idcustomer
    { get { return idcustomer; } }


    public int NumberDaysRent()
    {
        TimeSpan day = datereturn - daterent;
        return day.Days;
    }

    public double DownPayment()
    {
       return vehicle.RentCost() * 30 / 100;
    }

    public double Fine()
    {
        if(NumberDaysRent() < numberdayrent)
            return (numberdayrent - NumberDaysRent()) * vehicle.RentCost() * 1.1;
        return 0;
    }

    public double MoneyPay()
    {
        return vehicle.RentCost() * NumberDaysRent() * Endow.Percent(promotion) - DownPayment() + Fine();
    }


    public static void GetInfor(string table, List<Contract> contracts)
    {
        string connectionString = "Server=LAPTOP-Q3MNC1CJ;Database=UTE_Car;Trusted_Connection=true";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string idcolumn = "id" + table;
            using (SqlCommand command = new SqlCommand($"SELECT {idcolumn}, idcustomer, idvehicle, promotion, downpayment, dayrent, dayreturn, numberdayrent FROM dbo.{table} ", connection))
            {

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Contract contract = new Contract(); 
                        contract.vehicle = new Vehicle();
                        contract.idcontract = reader[idcolumn].ToString();
                        contract.idcustomer = reader["idcustomer"].ToString();
                        contract.vehicle.Idvehicle = reader["idvehicle"].ToString();
                        if (double.TryParse(reader["promotion"].ToString(), out double pro))
                        {
                            contract.promotion = pro;
                        }
                        if (double.TryParse(reader["downpayment"].ToString(), out double down))
                        {
                            contract.downpayment = down;
                        }
                        if (DateTime.TryParse(reader["dayrent"].ToString(), out DateTime daterent))
                        {
                            contract.daterent = daterent;
                        }
                        if (DateTime.TryParse(reader["dayreturn"].ToString(), out DateTime datereturn))
                        {
                            contract.datereturn = datereturn;
                        }
                        if (int.TryParse(reader["numberdayrent"].ToString(), out int num))
                        {
                            contract.numberdayrent = num;
                        }
                        ++numContract;
                        contracts.Add(contract);
                    }
                }
            }
        }
    }

    public virtual void AddContract()
    {

        string connectionString = "Server=LAPTOP-Q3MNC1CJ;Database=UTE_Car;Trusted_Connection=true";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand($"INSERT INTO dbo.Contract (idcontract, idcustomer, idvehicle, promotion, downpayment, dayrent, dayreturn, numberdayrent) VALUES (@idcontract, @idcustomer, @idvehicle, @promotion, @downpayment, @dayrent, NULL, @numberdayrent)", connection))
            {
                command.Parameters.AddWithValue("@idcontract", this.idcontract); 
                command.Parameters.AddWithValue("@idcustomer", this.idcustomer); 
                command.Parameters.AddWithValue("@idvehicle", this.vehicle.Idvehicle); 
                command.Parameters.AddWithValue("@promotion", this.promotion); 
                command.Parameters.AddWithValue("@downpayment", this.downpayment);
                command.Parameters.AddWithValue("@dayrent", DateTime.Now);
                command.Parameters.AddWithValue("@numberdayrent", this.numberdayrent); 
                command.ExecuteNonQuery();
            }
        }
    }

    public void Display()
    {
        string dreturn;
        if (datereturn == DateTime.MinValue)
            dreturn = string.Empty;
        else dreturn = datereturn.ToString("dd/MM/yyyy");
        Console.WriteLine($"{idcontract,-10} {idcustomer,-25} {vehicle.Idvehicle,-20} {promotion + "%", -15} {downpayment,-15} {daterent.ToString("dd/MM/yyyy"), -15} {dreturn, -15} {numberdayrent} ");
    }

    ~Contract() { }
}
