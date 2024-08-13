using Airport_Ticket_Booking.Domin.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Domin.Users
{
    public record Passenger : User
    {
        public List<Booking> Bookings { get; set; }


    }
}
