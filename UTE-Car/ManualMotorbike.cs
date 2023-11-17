class ManualMotorbike : Motorbike
{
    public ManualMotorbike() 
    {
        level = 1;
    }

    public ManualMotorbike(string idvehicle, string brand, DateTime daybuy, int traveldistance, bool insurance) : base(idvehicle, brand, daybuy, traveldistance, insurance)
    {
        level = 1;
    }
}
