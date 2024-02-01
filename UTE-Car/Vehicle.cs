using System.Data.SqlClient;
using System.Net;
using System.Numerics;

class Vehicle
{
    protected string idvehicle;
    protected string brand;
    protected DateTime daybuy;
    protected int traveldistance;
    protected Boolean insurance;
    protected double level = 1;
    protected double fee;
    protected double basecost = 1;
    protected bool isRent = false;
    protected List<Rating> rating = new List<Rating>();

    public Vehicle() 
    {
        rating = new List<Rating>();    
    }

    public Vehicle(string idvehicle, string brand, DateTime daybuy, int traveldistance, bool insurance, List<Rating> rating)
    {
        this.idvehicle = idvehicle;
        this.brand = brand;
        this.daybuy = daybuy;
        this.traveldistance = traveldistance;
        this.insurance = insurance;
        this.rating = rating;

    }

    public virtual string Idvehicle
    {
        get { return idvehicle; }
        set { idvehicle = value; }
    }

    public virtual string Brand
    {
        get { return brand; }
    }

    public virtual bool IsRented
    {
        get { return isRent; }
    }

    public DateTime DayBuy
    {
        get { return daybuy; }
    }

    public bool Insurance
    {
        get { return insurance; }
    }

    public List<Rating> Ratings { get { return rating; } }
    public double RentCost()
    {
        double tmp = level;
        if (this.insurance)
        {
            return basecost * tmp;
        }
        else
        {
            tmp -= fee;
            return basecost * tmp;
        }
    }

    public void DisplayRating()
    {
        Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
        Console.WriteLine("|  Person  |  Voting  |             Comment  ");
        foreach (Rating r in rating)
        {
            r.DisplayRating();
        }
    }
    public virtual void display()
    {

        Console.WriteLine($"{idvehicle,-5} | {brand,-22} | {traveldistance,-15} | {daybuy.ToString("dd/MM/yyyy"),-12} | {insurance,-12} | {RentCost().ToString("0.00"),-12} | {isRent} |");
    }

    public static string GetType(string type)
    {
        switch (type)
        {
            case "mm":
                return "ManualMotorbike";
            case "mcm":
                return "ManualClutchMotorbike";
            case "am":
                return "AutomaticMotorbike";
            case "c4":
                return "FourSeat";
            case "c7":
                return "SevenSeat";
            case "c16":
                return "SixteenSeat";
            default:
                return "Vehicle";
        }
    }

    public void setIsRent(bool rent)
    {
        string connectionString = "Server=LAPTOP-Q3MNC1CJ;Database=UTE_Car;Trusted_Connection=true";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            int tmp = rent ? 1 : 0;
            string updateQuery = $"UPDATE dbo.Vehicle SET IsRent = {tmp} WHERE idvehicle = @idvehicle";

            using (SqlCommand command = new SqlCommand(updateQuery, connection))
            {
                command.Parameters.AddWithValue("@idvehicle", idvehicle);
                isRent = rent;
                int rowsAffected = command.ExecuteNonQuery();

            }
        }
    }

    public static bool GetInfor(Owner owner)
    {
        string connectionString = "Server=LAPTOP-Q3MNC1CJ;Database=UTE_Car;Trusted_Connection=true";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand($"SELECT distinct V.idvehicle, brand, daybuy, traveldistance, insurance, V.idowner, typeVehicle, isRent \r\nFROM dbo.Vehicle V\r\nWHERE V.idowner = @idowner", connection))
                {
                    command.Parameters.AddWithValue("@idowner", owner.Id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string typeV = new string(reader["typeVehicle"].ToString().Trim());
                            string className = Vehicle.GetType(typeV);
                            Type type = Type.GetType(className);
                            object instance = Activator.CreateInstance(type);

                            if (instance is Vehicle v)
                            {
                                v.idvehicle = reader["idvehicle"].ToString().Trim();
                                v.brand = reader["brand"].ToString().Trim();
                                if (DateTime.TryParse(reader["daybuy"].ToString(), out DateTime date))
                                {
                                    v.daybuy = date;
                                }

                                string traveldistanceString = reader["traveldistance"].ToString();
                                if (int.TryParse(traveldistanceString, out int distance))
                                {
                                    v.traveldistance = distance;
                                }
                                if (bool.TryParse(reader["insurance"].ToString(), out bool ins))
                                {
                                    v.insurance = ins;
                                }
                                if (bool.TryParse(reader["isRent"].ToString(), out bool isR))
                                {
                                    v.isRent = isR;
                                }
                                Rating.ReadData("Rating", v.rating, v.idvehicle);
                                owner.VehicleList.Add(v);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return true;
        }
    }
}
