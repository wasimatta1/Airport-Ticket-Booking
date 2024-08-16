namespace Airport_Ticket_Booking.Domin.Users
{
    internal record Manager : User
    {
        public Manager(User user)
        {
            this.UserName = user.UserName;
            this.UserPassword = user.UserPassword;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Role = "Manager";
        }
    }
}
