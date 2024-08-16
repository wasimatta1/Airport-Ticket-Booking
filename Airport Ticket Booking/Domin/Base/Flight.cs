using Airport_Ticket_Booking.CustomAttributes;
using CsvHelper.Configuration.Attributes;

namespace Airport_Ticket_Booking.Domin.Base
{
    public record Flight : BaseEntity
    {
        [Name("Flight ID")]
        [UniqueId(nameof(FlightId))]
        public string FlightId { get; set; }

        public override string ToString()
        {
            return $"Flight ID: {FlightId}\n" +
                   base.ToString();
        }
    }
}
