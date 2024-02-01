using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Text;

class Rating
{
    private string comment;
    private int vote;
    private string idcustomer;


    public Rating() { }
    public Rating(string comment, int vote, string idcustomer)
    {
        this.comment = comment;
        this.vote = vote;
        this.idcustomer = idcustomer;
        
    }

    public void EnterYourRating(string customer)
    {
        Console.Write("Enter your comment about this vehicle (press 'Enter' to finish): ");
        StringBuilder inputStringBuilder = new StringBuilder();
        int stringLength = 0;
        while (true)
        {
            bool checkLimit = false;
            ConsoleKeyInfo keyInfo = Console.ReadKey(true); // Read key without displaying it
            if (keyInfo.Key == ConsoleKey.Enter)
            {
                break;
            }
            else if (keyInfo.Key == ConsoleKey.Backspace)
            {
                // Handle backspace to decrement the length
                if (inputStringBuilder.Length > 0)
                {
                    inputStringBuilder.Remove(inputStringBuilder.Length - 1, 1);
                    stringLength = inputStringBuilder.Length;
                }
            }
            else
            {
                if (stringLength < 150)
                {
                    inputStringBuilder.Append(keyInfo.KeyChar);
                    stringLength = inputStringBuilder.Length;
                }
                else checkLimit = true;
            }
            Console.Clear();
            Console.Write("Enter your comment about this vehicle (press 'Enter' to finish): ");
            Console.WriteLine($"String: {inputStringBuilder.ToString()}");
            if (checkLimit) 
            {
                Console.WriteLine("You reach the limit!"); 
            }
            Console.WriteLine($"Length: {stringLength}");
        }
        comment = inputStringBuilder.ToString();
        Console.Write("Enter your vote (1 to 5): ");
        vote = int.Parse(Console.ReadLine());
        idcustomer = customer;
    }

    public static void ReadData(string table, List<Rating> ratings, string idvehicle)
    {
        string connectionString = "Server=LAPTOP-Q3MNC1CJ;Database=UTE_Car;Trusted_Connection=true";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand($"SELECT idcustomer, rate, comment FROM dbo.{table} Where idvehicle = @idvehicle ", connection))
            {
                command.Parameters.AddWithValue("@idvehicle", idvehicle);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Rating rating = new Rating();
                        rating.idcustomer = reader["idcustomer"].ToString().Trim();
                        string rated = reader["rate"].ToString().Trim();
                        if (int.TryParse(rated, out int rate))
                        {
                            rating.vote = rate;
                        }
                        rating.comment = reader["comment"].ToString().Trim();
                        ratings.Add(rating);
                    }
                }
            }
        }
    }

    public void AddComment(Vehicle vehicle)
    {
        string connectionString = "Server=LAPTOP-Q3MNC1CJ;Database=UTE_Car;Trusted_Connection=true";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand($"INSERT INTO dbo.Rating (idvehicle, idcustomer, rate, comment) VALUES (@idvehicle, @idcustomer, @rate, @comment)", connection))
            {
                command.Parameters.AddWithValue("@idcustomer", idcustomer);
                command.Parameters.AddWithValue("@idvehicle", vehicle.Idvehicle);
                command.Parameters.AddWithValue("@rate", vote);
                command.Parameters.AddWithValue("@comment", comment);
                command.ExecuteNonQuery();
            }
        }
    }

    public void DisplayRating()
    {
        Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
        Console.WriteLine($"|  {idcustomer, -7}  |  {vote + "/5", -5}  |  {comment}  |");
        Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
    }

    ~Rating() { }
}