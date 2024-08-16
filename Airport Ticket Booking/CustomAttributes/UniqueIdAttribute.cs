
namespace Airport_Ticket_Booking.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class UniqueIdAttribute : Attribute
    {
        public  static HashSet<string> UniqueIds { get;} = new HashSet<string>();
        public string Name { get; set; }
        public UniqueIdAttribute(string name)
        {
            Name = name;
        }
        public bool AddAndCheckIfUnique(string Id)
        {
            return UniqueIds.Add(Id);
        }
        override public string ToString()
        {
            string constraints = $"Required, Unique";
            return $"{Name}:\nType: Free Text(Id)\nConstraint: {constraints}";
        }
    }
}
