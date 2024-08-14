namespace Airport_Ticket_Booking.Domin.Base
{
    public record Booking :BaseEntity
    {
        public string BookingId { get; set; }
        public string PassengerUserName { get; set; }
        public string FlightId { get; set; }

        public override string ToString()
        {
            return $"Booking ID: {BookingId}\n" +
                   $"Passenger User Name: {PassengerUserName}\n" +
                   $"Flight ID: {FlightId}\n" +
                   base.ToString();            
        }
    }
}
