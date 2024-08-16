namespace Airport_Ticket_Booking.CustomAttributes
{
    public class Error
    {
        private string field;
        private string id;
        private string details;

        public Error(string field,string id, string details)
        {
            this.field = field;
            this.id = id;
            this.details = details;
        }

        public override string ToString()
        {
            return $"{field} in Flight: {{{id}}}  : \"{details}\"";
        }
    }
}
