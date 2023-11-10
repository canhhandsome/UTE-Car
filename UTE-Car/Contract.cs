class Contract
{
    private string idcontract;
    private Customer customer;
    private Vehicle vehicle;
    private double promotion;
    private double downpayment;
    private DateTime daterent;
    private DateTime datereturn;
    private int numberdayrent;

    public Contract() { }
    public Contract(string idcontract, Customer customer, Vehicle vehicle, double downpayment, DateTime daterent, DateTime datereturn, int numberdayrent, double promotion)
    {
        this.idcontract = idcontract;
        this.customer = customer;
        this.vehicle = vehicle;
        this.downpayment = downpayment;
        this.daterent = daterent;
        this.datereturn = datereturn;
        this.numberdayrent = numberdayrent;
        this.promotion = promotion;
    }

    public int NumberDaysRent()
    {
        TimeSpan day = datereturn - daterent;
        return day.Days;
    }

    public double Fine()
    {
        if(NumberDaysRent() < numberdayrent)
            return (numberdayrent - NumberDaysRent()) * vehicle.RentCost() * 1.1;
        return 0;
    }

    public double MoneyPay()
    {
        return vehicle.RentCost() * NumberDaysRent() * Endow.Percent(promotion) - downpayment + Fine();
    }
    
    
    public void display()
    {
        Console.WriteLine("ID of Contract: " + idcontract);
        customer.Display();
    }

    ~Contract() { }
}
