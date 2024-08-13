using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Domin.Base
{
    public record Flight : BaseEntity
    {
        public string FlightId { get; set; }

    }
}
