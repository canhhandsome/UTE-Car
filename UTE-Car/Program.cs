using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;



class Program
{
    static void Main()
    {
        Customer customer = new Customer();
        Owner owner = new Owner();

        while (true)
        {

            Console.Title = "UTE-CAR";
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.WriteLine("\tWelcome to UTE-Car");
            Console.WriteLine("===================================");
            Console.WriteLine("Who are you?");
            Console.WriteLine("1. Customer");
            Console.WriteLine("2. Owner");
            Console.WriteLine("3. Guest");
            Console.WriteLine("4. Exit");
            Console.WriteLine("===================================");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("You are Customer");
                    customer.LoginPage();
                    // Add your code for Option 1 here
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("You are Owner");
                    owner.LoginPage();
                    // Add your code for Option 2 here
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("You are Guest");
                    // Add your code for Option 3 here
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Thank you for visiting\nGoodbye!");
                    Console.ResetColor(); // Reset console colors
                    return; // Exit the program
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
