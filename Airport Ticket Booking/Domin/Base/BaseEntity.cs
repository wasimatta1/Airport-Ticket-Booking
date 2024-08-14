using CsvHelper.Configuration.Attributes;

namespace Airport_Ticket_Booking.Domin.Base
{
    public abstract record BaseEntity
    {
       
        [Name("Price")]
        public double Price { get; set; }

        [Name("Departure Country")]
        public string DepartureCountry { get; set; }

        [Name("Destination Country")]
        public string DestinationCountry { get; set; }

        [Name("Departure Date")]
        public DateTime DepartureDate { get; set; }

        [Name("Departure Airport")]
        public string DepartureAirport { get; set; }

        [Name("Arrival Airport")]
        public string ArrivalAirport { get; set; }
        [Name("Class")]
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
