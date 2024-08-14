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
            //string flightsFilePath = "C:\\Users\\wasim\\OneDrive\\Desktop\\C# project\\Airport Ticket Booking\\Flights.csv";
            //var flights = CSVRepo.LoadDataFromCSVFile<Flight>(flightsFilePath);
            //flights.Print("Flights");

            string userFilePath = "C:\\Users\\wasim\\OneDrive\\Desktop\\C# project\\Airport Ticket Booking\\Users.csv";
            var Users = CSVRepo.LoadDataFromCSVFile<User>(userFilePath);
            User? user;
            while (true)
            {
                Console.Write("Enter your username: ");
                string? inputUsername = Console.ReadLine();

                Console.Write("Enter your password: ");
                string? inputPassword = Console.ReadLine();

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

        }
        static void MangerMenu(Manager manager)
        {
            //ToDo....
        }
        static void PassengerMenu(Passenger passenger)
        {
            string BookingsFilePath = "C:\\Users\\wasim\\OneDrive\\Desktop\\C# project\\Airport Ticket Booking\\Bookings.csv";

            List<Booking> bookings = CSVRepo.LoadDataFromCSVFile<Booking>(BookingsFilePath);

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

                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        //TODo...
                        break;
                    case "2":
                        //TODo...
                        break;
                    case "3":
                        Booking? removedBooking;
                        if (CancelBooking(passenger,out removedBooking))
                        {   
                            bookings.Remove(removedBooking!);
                            CSVRepo.SaveDataToCSVFile(BookingsFilePath, bookings);
                        }
                        break;
                    case "4":
                        Booking? modifiedBooking;
                        if (ModifyBooking(passenger, out modifiedBooking))
                        {
                            var preUpdateBook = bookings.Single(x => x.BookingId == modifiedBooking!.BookingId);
                            preUpdateBook = modifiedBooking!;
                            CSVRepo.SaveDataToCSVFile(BookingsFilePath, bookings);
                        }
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
        static bool CancelBooking(Passenger passenger,out Booking? removedBooking)
        {
            Console.Write("Enter the Booking ID you want to cancel: ");
            var bookingId = Console.ReadLine();
                
            var booking = passenger.Bookings.SingleOrDefault(x => x.BookingId == bookingId);

            if (booking is null)
            {
                Console.WriteLine("Invalid Booking ID");
                removedBooking = null;
                return false;
            }
            removedBooking = booking!;
            passenger.Bookings.Remove(booking);
            return true;
        }
        static bool ModifyBooking(Passenger passenger,out Booking? modifiedBooking)
        {
            Console.Write("Enter the Booking ID you want to modify: ");
            var bookingId = Console.ReadLine();

            var booking = passenger.Bookings.SingleOrDefault(x => x.BookingId == bookingId);

            if (booking is null)
            {
                Console.WriteLine("Invalid Booking ID");
                modifiedBooking = null;
                return false;
            }

            Console.Write("Enter the new Class {Economy,Business,First Class}: ");
            string? newClass = Console.ReadLine();

            if (!Enum.IsDefined(typeof(Class), newClass?.Replace(" ", "")!))
            {
                Console.WriteLine("Invalid Class");
                modifiedBooking = null;
                return false;
            }

            booking.Price = CalculatePrice(booking.Class, newClass!, booking.Price);
            booking.Class = $"{newClass}";
            modifiedBooking = booking!;

            return true;

        }

        private static double CalculatePrice(string CurrentClass, string newClass, double price)
        {
            price /= (int)Enum.Parse(typeof(Class), CurrentClass.Replace(" ",""));
            price *= (int)Enum.Parse(typeof(Class), newClass.Replace(" ", ""));
            return price;
        }

        enum Class
        {
            Economy = 1,
            Business  = 2,
            FirstClass = 3
        }

    }
}
