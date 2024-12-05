using Airport_Ticket_Booking.Domin.Base;

namespace Airport_Ticket_Booking.Domin.Users
{
    public record Passenger : User
    {
        public List<Booking> Bookings { get; set; }

        public Passenger(User user)
        {
            this.UserName = user.UserName;
            this.UserPassword = user.UserPassword;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Role = "Passenger";
        }
        public override string ToString()
        {
            return base.ToString();
        }

    }
}
