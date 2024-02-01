class ManualMotorbike : Motorbike
{
    public ManualMotorbike() 
    {
        level = 1;
    }

    public ManualMotorbike(string idvehicle, string brand, DateTime daybuy, int traveldistance, bool insurance, List<Rating> rating) : base(idvehicle, brand, daybuy, traveldistance, insurance, rating)
    {
        level = 1;
    }
}
