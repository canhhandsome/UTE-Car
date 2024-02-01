using System.Data.SqlClient;

class Owner : Account
{
    public Owner() { }

    public Owner(string id, string fullname, string address, string phone, List<Vehicle> vehicle) : base(id, fullname, address, phone)
    {
        this.vehicle = vehicle;
    }

    public List<Vehicle> VehicleList
    { get { return vehicle; } }

    public override void AddAccount()
    {
        Console.WriteLine("You can Register online!!\nPlease go to UTE_Car at 1st VVN street to Register!\nThank you pls");
    }
    

    public override void DisplayContract(List<Contract> contractlist)
    {
        List<Contract> contract = new List<Contract>();
        foreach(Vehicle v in vehicle) 
        {
            List<Contract> c = contractlist.Where(c => c.Vehicle.Idvehicle == v.Idvehicle).ToList();
            foreach(Contract contract1 in c)
                contract.Add(contract1);
        }
        base.DisplayContract(contract);
    }

    public void Display()
    {
        base.Display();
        Console.WriteLine("Here is vehicles:");
        Console.WriteLine($"{"ID",-5} | {"Brand",-22} | {"TravelDistance",-15} | {"DayBuy",-12} | {"Insurance",-12} | {"RentCost",-12} | {"IsRent"} |");
        foreach (var v in vehicle)
        {
            v.display();    
        }
    }

}
