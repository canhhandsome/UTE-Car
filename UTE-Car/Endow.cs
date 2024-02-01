class Endow
{
    private double percent;

    public static List<DateTime> specialOccasionsInsPrice = new List<DateTime>
    {
        new DateTime(DateTime.Now.Year, 1, 1),    // New Year's Day
        new DateTime(DateTime.Now.Year, 2, 14),   // Valentine's Day
        new DateTime(DateTime.Now.Year, 3, 8),    // International Women's Day
        new DateTime(DateTime.Now.Year, 4, 1),    // April Fools' Day
        new DateTime(DateTime.Now.Year, 5, 1),    // Labor Day
        new DateTime(DateTime.Now.Year, 6, 21),   // Summer Solstice 
        new DateTime(DateTime.Now.Year, 11, 11),  // Veterans Day
        new DateTime(DateTime.Now.Year, 12, 31),  // New Year's Eve
        new DateTime(DateTime.Now.Year, 9, 2),    // Quoc Khanh
        new DateTime(DateTime.Now.Year, 9, 15),   // Tet Trung
        new DateTime(DateTime.Now.Year, 4, 30),   // Giai Phong Mien Nam Thong Nhat Dat Nuoc
        new DateTime(DateTime.Now.Year, 5, 1),   // Giai Phong Mien Nam Thong Nhat Dat Nuoc
        // Tet am nam 2024
        new DateTime(2024, 2, 8), 
        new DateTime(2024, 2, 9), 
        new DateTime(2024, 2, 10), 
        new DateTime(2024, 2, 11), 
        new DateTime(2024, 2, 12), 
        new DateTime(2024, 2, 13), 
    };

    public static List<DateTime> specialOccasionsDesPrice = new List<DateTime>
    {
        new DateTime(DateTime.Now.Year, 6, 1), // Quoc Te Thieu nhi
        new DateTime(DateTime.Now.Year, 12, 24),  // Christmas Eve
        new DateTime(DateTime.Now.Year, 10, 31),  // Halloween
    };

    public Endow()
    { }

    public Endow(double percent)
    {
        this.percent = percent;
    }

    public static double Percent()
    {
        double percent = 0;
        DateTime dateTime = DateTime.Now;
        if(dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday)
            percent = 20;
        foreach (var list in specialOccasionsInsPrice)
        {
            if(list == dateTime)
            {
                percent += 40;
                break;
            }
        }

        foreach(var list in specialOccasionsDesPrice)
        {
            if(list == dateTime || dateTime.Month == 8 || dateTime.Month == dateTime.Day)
            {
                percent -= 30;
                break;
            }
        }

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
