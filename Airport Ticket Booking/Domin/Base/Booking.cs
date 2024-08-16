using CsvHelper.Configuration.Attributes;

namespace Airport_Ticket_Booking.Domin.Base
{
    public record Booking :BaseEntity
    {
        [Name("Booking ID")]
        public string BookingId { get; set; }

        [Name("Passenger UserName")]
        public string PassengerUserName { get; set; }

        [Name("Flight ID")]
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
