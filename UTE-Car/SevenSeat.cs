class SevenSeat : Car
{
    public SevenSeat() { }

    public SevenSeat(string idvehicle, string brand, DateTime daybuy, int traveldistance, bool insurance) : base(idvehicle, brand, daybuy, traveldistance, insurance)
    {
        level = 1.25;
    }
}
