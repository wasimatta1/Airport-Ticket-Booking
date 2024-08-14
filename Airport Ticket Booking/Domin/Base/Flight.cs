namespace Airport_Ticket_Booking.Domin.Base
{
    public record Flight : BaseEntity
    {
        public string FlightId { get; set; }

        public override string ToString()
        {
            return $"Flight ID: {FlightId}\n" +
                   base.ToString();
        }
    }
}
