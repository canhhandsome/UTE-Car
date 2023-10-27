class ManualClutchMotorbike : Motorbike
{
    public ManualClutchMotorbike() { }

    public ManualClutchMotorbike(string idvehicle, string brand, DateTime daybuy, int traveldistance, bool insurance) : base(idvehicle, brand, daybuy, traveldistance, insurance)
    {
        level = 1.5;
    }
}
