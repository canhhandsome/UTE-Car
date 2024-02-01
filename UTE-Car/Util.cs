class Util
{
    public static string StandardizeName(string name)
    {
        string[] s = name.Split(' ');
        string box = string.Empty;
        foreach (string word in s)
        {
            box += word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower() + " ";
        }
        return box.Substring(0, box.Length - 1);
    }
}