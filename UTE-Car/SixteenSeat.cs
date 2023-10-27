class SixteenSeat : Car
{
    public SixteenSeat() { }

    public SixteenSeat(string idvehicle, string brand, DateTime daybuy, int traveldistance, bool insurance) : base(idvehicle, brand, daybuy, traveldistance, insurance)
    {
        level = 2;
    }
}
