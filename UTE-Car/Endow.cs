class Endow
{
    private double percent;

    public Endow()
    { }

    public Endow(double percent)
    {
        this.percent = percent;
    }

    public double Percent()
    {
        if (percent < 0)
        {
            percent *= -1;
            return 1 - percent / 100;
        }
        else
            return 1 + percent / 100; 
    }

    ~Endow() { }    
}
