using Airport_Ticket_Booking.Domin.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Domin.Base
{
    public record Booking :BaseEntity
    {
        public string BookingId { get; set; }
        public Passenger PassengerDetails { get; set; }
        public Flight FlightDetails { get; set; }

    }
}
