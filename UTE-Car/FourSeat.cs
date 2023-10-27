class FourSeat : Car
{
    public FourSeat() { }

    public FourSeat(string idvehicle, string brand, DateTime daybuy, int traveldistance, bool insurance) : base(idvehicle, brand, daybuy, traveldistance, insurance)
    {
        level = 1;
    }
}

