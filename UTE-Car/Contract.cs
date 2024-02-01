using System.Collections.Generic;
using System.Data.SqlClient;

class Contract
{
    private string idcontract;
    private string idcustomer;
    private Vehicle vehicle = new Vehicle();
    private double promotion;
    private double downpayment;
    private DateTime daterent;
    private DateTime datereturn;
    private int numberdayrent;
    private double total;
    public static int numContract = 0;

    public Contract() { }
    public Contract(string customer, Vehicle vehicle, DateTime daterent, int numberdayrent)
    {
        ++numContract;
        this.idcontract = "ct" + numContract.ToString();
        this.idcustomer = customer;
        this.vehicle = vehicle;
        this.daterent = daterent;
        this.downpayment = DownPayment();
        this.numberdayrent = numberdayrent;
        this.promotion = Endow.Percent();
        this.total = MoneyPay();
    }

    public string Idcontract
    { get { return idcontract; } }

    public string Idcustomer
    { get { return idcustomer; } }

    public DateTime DateReturn
    {
        get { return datereturn; }
    }

    public DateTime DateExpire()
    {
        return daterent.AddDays(numberdayrent);
    }

    public Vehicle Vehicle 
    { 
        get { return vehicle; } 
    }


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
        if(NumberDaysRent() > numberdayrent)
            return (numberdayrent - NumberDaysRent()) * vehicle.RentCost() * 1.1;
        return 0;
    }

    public double MoneyPay()
    {
        return vehicle.RentCost() * numberdayrent * promotion - DownPayment()+ Fine();
    }

    public void GetInfor(List<Contract> contracts)
    {
        string connectionString = "Server=LAPTOP-Q3MNC1CJ;Database=UTE_Car;Trusted_Connection=true";
        string table = typeof(Contract).Name;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string idcolumn = "id" + table;
            using (SqlCommand command = new SqlCommand($"SELECT {idcolumn}, idcustomer, idvehicle, promotion, downpayment, dayrent, dayreturn, numberdayrent, total FROM dbo.{table} ", connection))
            {

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Contract contract = new Contract(); 
                        contract.vehicle = new Vehicle();
                        contract.idcontract = reader[idcolumn].ToString().Trim();
                        contract.idcustomer = reader["idcustomer"].ToString().Trim();
                        contract.vehicle.Idvehicle = reader["idvehicle"].ToString().Trim();
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
                        if (double.TryParse(reader["total"].ToString(), out double tol))
                        {
                            contract.total = tol;
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
            using (SqlCommand command = new SqlCommand($"INSERT INTO dbo.Contract (idcontract, idcustomer, idvehicle, promotion, downpayment, dayrent, dayreturn, numberdayrent, total) VALUES (@idcontract, @idcustomer, @idvehicle, @promotion, @downpayment, @dayrent, NULL, @numberdayrent, @total)", connection))
            {
                command.Parameters.AddWithValue("@idcontract", this.idcontract); 
                command.Parameters.AddWithValue("@idcustomer", this.idcustomer); 
                command.Parameters.AddWithValue("@idvehicle", this.vehicle.Idvehicle); 
                command.Parameters.AddWithValue("@promotion", this.promotion); 
                command.Parameters.AddWithValue("@downpayment", this.downpayment);
                command.Parameters.AddWithValue("@dayrent", daterent);
                command.Parameters.AddWithValue("@numberdayrent", this.numberdayrent); 
                command.Parameters.AddWithValue("@total", MoneyPay()); 
                command.ExecuteNonQuery();
            }
        }
    }

    public void SetDateReturn()
    {
        string connectionString = "Server=LAPTOP-Q3MNC1CJ;Database=UTE_Car;Trusted_Connection=true";

        datereturn = DateTime.Now;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string updateQuery = "UPDATE dbo.Contract SET dayreturn = @datereturn WHERE idvehicle = @idvehicle";

            using (SqlCommand command = new SqlCommand(updateQuery, connection))
            {
                command.Parameters.AddWithValue("@datereturn", datereturn);
                command.Parameters.AddWithValue("@idvehicle", vehicle.Idvehicle);

                int rowsAffected = command.ExecuteNonQuery();
            }
        }
    }


    public void Display()
    {
        string dreturn = (datereturn == DateTime.MinValue) ? string.Empty : datereturn.ToString("dd/MM/yyyy");

        Console.WriteLine($"{idcontract,-13} | {idcustomer,-13} | {vehicle.Idvehicle,-13} | {promotion + "%",-15} | {downpayment.ToString("0.00"),-20} | {daterent.ToString("dd/MM/yyyy"),-15} | {dreturn,-15} | {numberdayrent, -15} | {total} ");
    }

    ~Contract() { }
}
