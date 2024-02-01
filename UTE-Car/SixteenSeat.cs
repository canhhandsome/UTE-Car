class SixteenSeat : Car
{
    public SixteenSeat() 
    {
        level = 2;
    }

    public SixteenSeat(string idvehicle, string brand, DateTime daybuy, int traveldistance, bool insurance, List<Rating> rating) : base(idvehicle, brand, daybuy, traveldistance, insurance, rating)
    {
        level = 2;
    }
}
