using Airport_Ticket_Booking.Domin.CSV;
using Airport_Ticket_Booking.Domin.Users;

namespace Airport_Ticket_Booking
{
    internal class Program
    {
        static void Main(string[] args)
        {   // TODO ...
            // all code until now just to test , it will be updated soon 

            string path = "C:\\Users\\wasim\\OneDrive\\Desktop\\C# project\\Airport Ticket Booking\\Users.csv";
            var Users = CSVRepo.LoadDataFromCSVFile<User>(path);
            User? user;
            while (true)
            {
                Console.Write("Enter your username: ");
                string inputUsername = Console.ReadLine();

                Console.Write("Enter your password: ");
                string inputPassword = Console.ReadLine();

                user = Users.SingleOrDefault(x=>x.UserName == inputUsername && x.UserPassword == inputPassword);

                if(!(user is null)) break;

                Console.WriteLine("Wrong UserName Or Password");
            }

            if (user.Role == "Manager") Console.WriteLine("Welcom Manger");
            else Console.WriteLine("Welcom Passenger");
            Console.WriteLine(user);



            Console.ReadKey();
        }
    }
}
