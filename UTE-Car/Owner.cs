class Owner : Account
{
    private Vehicle[] vehicle = { };
    public Owner() { }

    public Owner(string id, string fullname, string address, string phone, Vehicle[] vehicle) : base(id, fullname, address, phone) 
    {
        this.vehicle = vehicle;
    }


    public override void Register()
    {
        Console.WriteLine("Please register directly at my company!\nThank for your interest in my company");
    }

    public override void Display()
    {
        base.Display();
        foreach (var v in vehicle)
        {
            v.display();
        }
    }
}
