using Airport_Ticket_Booking.Domin.Base;
using Airport_Ticket_Booking.Domin.CSV;
using Airport_Ticket_Booking.Domin.Extension;
using Airport_Ticket_Booking.Domin.Users;
using static Airport_Ticket_Booking.Domin.Extension.ExtensionMethod;

namespace Airport_Ticket_Booking
{
    internal class Program
    {
        static string FilePath = @"C:\Users\wasim\OneDrive\Desktop\C# project\Airport Ticket Booking\";

        static List<Flight>? flights;
        static void Main(string[] args)
        { 
            var Users = CSVRepo.LoadDataFromCSVFile<User>($"{FilePath}Users.csv");
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
            flights = CSVRepo.LoadDataFromCSVFile<Flight>($"{FilePath}Flights.csv");

            List<Booking> bookings = CSVRepo.LoadDataFromCSVFile<Booking>($"{FilePath}Bookings.csv");

            passenger.Bookings = bookings.Where(x => x.PassengerUserName == passenger.UserName).ToList();

            IEnumerable<Flight>? filteredFlights = flights; // Default value All Flights

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
                        Booking? bookingFlight = BookingNewFlight(filteredFlights,passenger);
                        if(bookingFlight is null)
                            break;
                        passenger.Bookings.Add(bookingFlight);
                        bookings.Add(bookingFlight);
                        CSVRepo.SaveDataToCSVFile($"{FilePath}Bookings.csv", bookings);
                        break;
                    case "2":

                        var filters = GetFilters(false);
                        if (filters.Count == 0)
                            filteredFlights = flights;
                        else 
                            filteredFlights = flights.Filter(filters);

                        filteredFlights?.Print("Available Flights");
                        break;
                    case "3":
                        Booking? removedBooking;
                        if (CancelBooking(passenger, out removedBooking))
                        {
                            bookings.Remove(removedBooking!);
                            CSVRepo.SaveDataToCSVFile($"{FilePath}Bookings.csv", bookings);
                        }
                        break;
                    case "4":
                        Booking? modifiedBooking;
                        if (ModifyBooking(passenger, out modifiedBooking))
                        {
                            var preUpdateBook = bookings.Single(x => x.BookingId == modifiedBooking!.BookingId);
                            preUpdateBook = modifiedBooking!;
                            CSVRepo.SaveDataToCSVFile($"{FilePath}Bookings.csv", bookings);
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

        private static Booking? BookingNewFlight(IEnumerable<Flight>? filteredFlights, Passenger passenger)
        {
            filteredFlights?.Print("Available Flights From Last Search");

            Console.Write("Enter the Flight ID you want to book: ");

            string? flightId = Console.ReadLine();

            Flight? flight = filteredFlights?.SingleOrDefault(x => x.FlightId == flightId);

            if (flight is null)
            {
                Console.WriteLine("Invalid Flight ID");
               return null;
            }
            bool isBooked = passenger.Bookings.Any(x => x.FlightId == flightId);

            if (isBooked)
            {
                Console.WriteLine("You already booked this flight");
                return null;
            }
            Console.Write("Enter the Class {Economy,Business,First Class}: ");
            string? flightClass = Console.ReadLine();


            if (!Enum.IsDefined(typeof(Class), flightClass?.Replace(" ", "")!))
            {
                Console.WriteLine("Invalid Class");
                return null;
            }
            var price = CalculatePrice("Economy", flightClass!, flight.Price);

            var NewBook = new Booking
            {
                BookingId = Guid.NewGuid().ToString(),
                PassengerUserName = passenger.UserName,
                FlightId = flightId!,
                Price = price,
                DepartureCountry = flight.DepartureCountry,
                DestinationCountry = flight.DestinationCountry,
                DepartureDate = flight.DepartureDate,
                DepartureAirport = flight.DepartureAirport,
                ArrivalAirport = flight.ArrivalAirport,
                Class = flightClass!
            };

            return NewBook;
        }

        private static Dictionary<string, (FilterOperator, object)> GetFilters(bool isManger)
        {
            var filters = new Dictionary<string, (FilterOperator, object)>();

            while (true)
            {
                Console.WriteLine("Choose a parameter to filter by:");
                Console.WriteLine("1- Price");
                Console.WriteLine("2- Departure Country");
                Console.WriteLine("3- Destination Country");
                Console.WriteLine("4- Departure Date");
                Console.WriteLine("5- Departure Airport");
                Console.WriteLine("6- Arrival Airport");
                if (isManger)
                {
                    Console.WriteLine("7- Flight ID");
                    Console.WriteLine("8- Passenger UserName");
                    Console.WriteLine("9- Finish");
                }else
                    Console.WriteLine("7- Finish");

                string? choice = Console.ReadLine();

                if(int.TryParse(choice, out int x))
                {
                    if (!isManger && x > 7 || x < 1)
                    {
                        Console.WriteLine("Invalid Choose selected, please try again.");
                        continue;
                    }else if (isManger && x > 9 || x < 1)
                    {
                        Console.WriteLine("Invalid Choose selected, please try again.");
                        continue;
                    }
                }

                if (!isManger && choice == "7" || isManger && choice == "9")
                {
                    break; // Finish
                }

                Console.WriteLine("Select the filter operation:");
                Console.WriteLine("1- Equals");
                Console.WriteLine("2- Not Equal");
                var isVaildOpreator = choice == "4" || choice == "1";
                if (isVaildOpreator)
                {
                    Console.WriteLine("3- Less Than");
                    Console.WriteLine("4- Greater Than or Equal");
                    Console.WriteLine("5- Less Than or Equal");
                    Console.WriteLine("6- Greater Than");
                }


                string? operationChoice = Console.ReadLine();
                if (!isVaildOpreator)
                {
                    switch (operationChoice)
                    {
                        case "2":
                        case "3":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                            Console.WriteLine("Invalid operation selected, please try again.");
                            continue;
                    }
                }

                FilterOperator filterOperator = FilterOperator.Equals; 

                switch (operationChoice)
                {
                    case "1":
                        filterOperator = FilterOperator.Equals;
                        break;
                    case "2":
                        filterOperator = FilterOperator.NotEqual;
                        break;
                    case "3":
                        filterOperator = FilterOperator.LessThan;
                        break;
                    case "4":
                        filterOperator = FilterOperator.GreaterThanOrEqual;
                        break;
                    case "5":
                        filterOperator = FilterOperator.LessThanOrEqual;
                        break;
                    case "6":
                        filterOperator = FilterOperator.GreaterThan;
                        break;
                    default:
                        Console.WriteLine("Invalid operation selected, please try again.");
                        continue;
                }

                Console.WriteLine("Enter the value for the selected parameter:");

                string? filterValueStr = Console.ReadLine();
                if (filterValueStr is null)
                {
                    Console.WriteLine("Invalid value entered, please try again.");
                    continue;
                }
                object filterValue;

                switch (choice)
                {
                    case "1":
                        filterValue = double.Parse(filterValueStr);
                        filters.Add("Price", (filterOperator, filterValue));
                        break;
                    case "2":
                        filterValue = filterValueStr;
                        filters.Add("DepartureCountry", (filterOperator, filterValue));
                        break;
                    case "3":
                        filterValue = filterValueStr;
                        filters.Add("DestinationCountry", (filterOperator, filterValue));
                        break;
                    case "4":
                        filterValue = DateTime.Parse(filterValueStr);
                        filters.Add("DepartureDate", (filterOperator, filterValue));
                        break;
                    case "5":
                        filterValue = filterValueStr;
                        filters.Add("DepartureAirport", (filterOperator, filterValue));
                        break;
                    case "6":
                        filterValue = filterValueStr;
                        filters.Add("ArrivalAirport", (filterOperator, filterValue));
                        break;
                    case "7":
                        filterValue = filterValueStr;
                        filters.Add("FlightId", (filterOperator, filterValue));
                        break;
                    case "8":
                        filterValue = filterValueStr;
                        filters.Add("PassengerUserName", (filterOperator, filterValue));
                        break;
                }
            }

            return filters;
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
