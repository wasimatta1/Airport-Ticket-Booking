
namespace Airport_Ticket_Booking.Domin
{
    public static class Enums
    {
        public enum Class
        {
            Economy = 1,
            Business = 2,
            FirstClass = 3
        }
        public enum FilterOperator
        {
            Equals,
            GreaterThan,
            LessThan,
            GreaterThanOrEqual,
            LessThanOrEqual,
            NotEqual
        }
    }
}
