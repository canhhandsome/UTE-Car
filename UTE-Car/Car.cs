class Car : Vehicle
{
    public Car() { }

    public Car(string idvehicle, string brand, DateTime daybuy, int traveldistance, bool insurance) : base(idvehicle, brand, daybuy, traveldistance, insurance)
    {
        this.fee = 0.2;
        this.basecost = 1000000;
    }

}

