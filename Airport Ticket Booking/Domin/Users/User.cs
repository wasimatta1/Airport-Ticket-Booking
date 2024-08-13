using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Domin.Users
{
    public record User
    {
        [Name("User Name")]
        public string UserName { get; set; }
        [Name("User Password")]
        public string UserPassword { get; set; }
        [Name("First Name")]
        public string FirstName { get; set; }
        [Name("Last Name")]
        public string LastName { get; set; }
        [Name("Role")]
        public string Role { get; set; }

    }
}
