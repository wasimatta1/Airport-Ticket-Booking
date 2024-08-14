namespace Airport_Ticket_Booking.Domin.Base
{
    public abstract record BaseEntity
    {   
        public double Price { get; set; }
        public string DepartureCountry { get; set; }
        public string DestinationCountry { get; set; }
        public DateTime DepartureDate { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public string Class { get; set; }

        public override string ToString()
        {
            return $"Price: {Price:C}\n" +
                   $"From: {DepartureCountry} ({DepartureAirport})\n" +
                   $"To: {DestinationCountry} ({ArrivalAirport})\n" +
                   $"Departure: {DepartureDate:yyyy-MM-dd}\n" +
                   $"Class: {Class}";
        }

    }
}
