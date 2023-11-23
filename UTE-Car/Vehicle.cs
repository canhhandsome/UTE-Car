﻿using System.Data.SqlClient;
using System.Net;
using System.Numerics;

enum EType
{
    car,
    motobike
}
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
    protected bool isRent;

    public Vehicle() { }

    public Vehicle(string idvehicle, string brand, DateTime daybuy, int traveldistance, bool insurance)
    {
        this.idvehicle = idvehicle;
        this.brand = brand;
        this.daybuy = daybuy;
        this.traveldistance = traveldistance;
        this.insurance = insurance;
    }

    public virtual string Idvehicle
    {
        get { return idvehicle; }
        set { idvehicle = value; }
    }

    public virtual double RentCost()
    {
        if (this.insurance)
        {
            return basecost * level;
        }
        else
        {
            level -= fee;
            return basecost * level;
        }
    }

    public virtual void display()
    {

        Console.WriteLine($"{idvehicle,-10} {brand,-25} {traveldistance,-20} {daybuy.ToString("dd/MM/yyyy"),-15} {insurance, -15} {RentCost()}");
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

    public static bool GetInfor(Owner owner)
    {
        string connectionString = "Server=LAPTOP-Q3MNC1CJ;Database=UTE_Car;Trusted_Connection=true";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand($"SELECT V.idvehicle, brand, daybuy, traveldistance, insurance, V.idowner, typeVehicle\r\nFROM dbo.Vehicle V\r\nWHERE V.idowner = @idowner", connection))
                {
                    command.Parameters.AddWithValue("@idowner", owner.Id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string typeV = new string(reader["typeVehicle"].ToString().Trim());
                            string className = Vehicle.GetType(typeV); // The class name
                                                           // Use reflection to create an instance
                            Type type = Type.GetType(className);
                            object instance = Activator.CreateInstance(type);

                            if (instance is Vehicle v)
                            {
                                v.idvehicle = reader["idvehicle"].ToString();
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
                                owner.VehicleList.Add(v);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during database operations
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return true;
        }
    }
}
