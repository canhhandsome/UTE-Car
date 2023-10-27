class Customer : Account
{ 
    public Customer() { }

    public Customer(string id,  string fullname, string address, string phone) : base(fullname, address, phone) 
    {
        this.id = id;
    }

    public void RentCar()
    {
        
    }

    public override void Display()
    {
        base.Display();
    }
}
