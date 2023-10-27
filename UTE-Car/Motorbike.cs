class Motorbike : Vehicle
{
    public Motorbike() { }

    public Motorbike(string idvehicle, string brand, DateTime daybuy, int traveldistance, bool insurance) : base(idvehicle, brand, daybuy, traveldistance, insurance)
    {
        this.basecost = 110000;
        this.fee = 0.1;
    }

}
