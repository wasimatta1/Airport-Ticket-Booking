
namespace Airport_Ticket_Booking.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DateTimeRangeAttribute : Attribute
    {
        public string Name { get; set; }
        public DateTime MinValue { get; set; }
        public DateTime MaxValue { get; set; }
        public DateTimeRangeAttribute(string name)
        {
            Name = name;
            MinValue = DateTime.Now;
            MaxValue = DateTime.MaxValue;
        }

        public DateTimeRangeAttribute(string name, DateTime minValue)
        {
            Name = name;
            MinValue = minValue;
            MaxValue = DateTime.MaxValue;
        }

        public DateTimeRangeAttribute(string name, DateTime minValue, DateTime maxValue)
        {
            Name = name;
            MinValue = minValue;
            MaxValue = maxValue;
        }
        public bool IsValid(DateTime value)
        {
            if (DateTime.Compare(value, MinValue) < 0)
            {
                return false;
            }
            if (DateTime.Compare(value, MaxValue) > 0)
            {
                return false;
            }
            return true;
        }
        override public string ToString()
        {
            string constraints = $"Required, Allowed Range (today → future)";
            return $"{Name}:\nType: Date Time\nConstraint: {constraints}";
        }
    }
}
