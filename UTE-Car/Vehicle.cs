class Vehicle
{
    protected string idvehicle;
    protected string brand;
    protected DateTime daybuy;
    protected int traveldistance;
    protected Boolean insurance;
    protected double level;
    protected double fee;
    protected double basecost;
    protected Endow endow;

    public Vehicle() { }

    public Vehicle(string idvehicle, string brand, DateTime daybuy, int traveldistance, bool insurance)
    {
        this.idvehicle = idvehicle;
        this.brand = brand;
        this.daybuy = daybuy;
        this.traveldistance = traveldistance;
        this.insurance = insurance;
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
        Console.WriteLine($"{idvehicle, -10} {brand, -15} {traveldistance, -20} {daybuy.ToString("dd/MM/yyyy"), -15} {insurance}");
    }

    public virtual void GetInfor()
    {

    }
}
