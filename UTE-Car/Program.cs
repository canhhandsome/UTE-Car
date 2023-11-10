Vehicle vehicle1 = new Vehicle("V1", "Porsche", new DateTime(2022, 8, 26), 10000, true);
Vehicle vehicle2 = new Vehicle("V2", "Mercedes", new DateTime(2018, 2, 26), 30000, true);
Vehicle vehicle3 = new Vehicle("V3", "Bmw", new DateTime(2023, 11, 1), 100, false);
Vehicle[] vehicle = {vehicle1,  vehicle2, vehicle3};
Customer customer = new Customer("C1", "Nguyen Nhat An", "Bien Hoa", "0651165061");
Owner owner = new Owner("O1", "BAch DUca CANh", "Bien Hoa", "0651165061", vehicle);

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
            owner.Menu();
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