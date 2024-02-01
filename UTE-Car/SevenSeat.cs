class SevenSeat : Car
{
    public SevenSeat() 
    {
        level = 1.25;
    }

    public SevenSeat(string idvehicle, string brand, DateTime daybuy, int traveldistance, bool insurance, List<Rating> rating) : base(idvehicle, brand, daybuy, traveldistance, insurance, rating)
    {
        level = 1.25;
    }
}
