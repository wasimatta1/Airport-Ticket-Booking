using Airport_Ticket_Booking.CustomAttributes;
using CsvHelper.Configuration.Attributes;

namespace Airport_Ticket_Booking.Domin.Base
{
    public abstract record BaseEntity
    {
       
        [Name("Price")]
        [Range<double>(nameof(Price),0,true)]
        public double Price { get; set; }

        [Name("Departure Country")]
        [TextValidationAttribute(nameof(DepartureCountry),true)]
        public string DepartureCountry { get; set; }

        [Name("Destination Country")]
        [TextValidationAttribute(nameof(DestinationCountry),30, true)]
        public string DestinationCountry { get; set; }

        [Name("Departure Date")]
        [DateTimeRange(nameof(DateTime))]
        public DateTime DepartureDate { get; set; }

        [Name("Departure Airport")]
        [TextValidationAttribute(nameof(DepartureAirport), 30, true)]
        public string DepartureAirport { get; set; }

        [Name("Arrival Airport")]
        [TextValidationAttribute(nameof(ArrivalAirport), 30, true)]
        public string ArrivalAirport { get; set; }

        [Name("Class")]
        [TextValidationAttribute(nameof(Class), true, true)]
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
