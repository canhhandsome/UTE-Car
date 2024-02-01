class Motorbike : Vehicle
{
    public Motorbike() 
    {
        this.basecost = 110000;
        this.fee = 0.1;
    }

    public Motorbike(string idvehicle, string brand, DateTime daybuy, int traveldistance, bool insurance, List<Rating> rating) : base(idvehicle, brand, daybuy, traveldistance, insurance, rating)
    {
        this.basecost = 110000;
        this.fee = 0.1;
    }

}
