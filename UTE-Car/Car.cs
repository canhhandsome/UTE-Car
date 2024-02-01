class Car : Vehicle
{
    public Car() 
    {
        this.fee = 0.2;
        this.basecost = 1000000;
    }

    public Car(string idvehicle, string brand, DateTime daybuy, int traveldistance, bool insurance, List<Rating> rating) : base(idvehicle, brand, daybuy, traveldistance, insurance, rating)
    {
        this.fee = 0.2;
        this.basecost = 1000000;
    }


}

