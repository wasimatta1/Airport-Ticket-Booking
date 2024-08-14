using Airport_Ticket_Booking.Domin.Base;
using Airport_Ticket_Booking.Domin.CSV;
using Airport_Ticket_Booking.Domin.Users;

namespace Airport_Ticket_Booking
{
    internal class Program
    {
        static void Main(string[] args)
        {   // TODO ...
            // all code until now just to test , it will be updated soon 

            string userFilePath = "C:\\Users\\wasim\\OneDrive\\Desktop\\C# project\\Airport Ticket Booking\\Users.csv";
            var Users = CSVRepo.LoadDataFromCSVFile<User>(userFilePath);
            User? user;
            while (true)
            {
                Console.Write("Enter your username: ");
                string inputUsername = Console.ReadLine();

                Console.Write("Enter your password: ");
                string inputPassword = Console.ReadLine();

                user = Users.SingleOrDefault(x=>x.UserName == inputUsername && x.UserPassword == inputPassword);

                if (!(user is null))
                {
                    if (user.Role == "Manager")
                        MangerMenu(new Manager(user));
                    else
                        PassengerMenu(new Passenger(user));
                    user = null;
                    Console.Clear();
                    continue;
                }
                Console.WriteLine("Wrong UserName Or Password");
            }




            Console.ReadKey();
        }
        static void MangerMenu(Manager manager)
        {
            //ToDo....
        }
        static void PassengerMenu(Passenger passenger)
        {
            string BookingsFilePath = "C:\\Users\\wasim\\OneDrive\\Desktop\\C# project\\Airport Ticket Booking\\Bookings.csv";

            var bookings = CSVRepo.LoadDataFromCSVFile<Booking>(BookingsFilePath);

            passenger.Bookings = bookings.Where(x => x.PassengerUserName == passenger.UserName).ToList();

            while (true)
            {
                Console.WriteLine("=== Passenger Menu ===");
                Console.WriteLine("1- Book a Flight");
                Console.WriteLine("2- Search for Available Flights");
                Console.WriteLine("3- Cancel a Booking");
                Console.WriteLine("4- Modify a Booking");
                Console.WriteLine("5- View Personal Bookings");
                Console.WriteLine("6- Logout");
                Console.Write("Please select an option (1-7): ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        //TODo...
                        break;
                    case "2":
                        //TODo...
                        break;
                    case "3":
                        //TODo...
                        break;
                    case "4":
                        //TODo...
                        break;
                    case "5":
                        passenger.Bookings.Print("Your Bookings");
                        break;
                    case "6":
                        return; 
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }

                Console.WriteLine();
            }
        }
    }
}
