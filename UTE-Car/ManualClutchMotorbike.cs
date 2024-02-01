class ManualClutchMotorbike : Motorbike
{
    public ManualClutchMotorbike() 
    {
        level = 1.5;
    }

    public ManualClutchMotorbike(string idvehicle, string brand, DateTime daybuy, int traveldistance, bool insurance, List<Rating> rating) : base(idvehicle, brand, daybuy, traveldistance, insurance, rating)
    {
        level = 1.5;
    }
}
