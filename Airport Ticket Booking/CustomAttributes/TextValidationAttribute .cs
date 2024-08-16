
using Airport_Ticket_Booking.Domin;

namespace Airport_Ticket_Booking.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class TextValidationAttribute : Attribute
    {
        public string Name { get; set; }
        public bool Required { get; set; }
        public int MaxLength { get; set; }
        public bool IsClass { get; set; } = false;

        public TextValidationAttribute(string name, bool required)
        {
            Name = name;
            Required = required;
            MaxLength = 0;
        }
        public TextValidationAttribute(string name, bool isClass, bool required)
        {
            Name = name;
            IsClass = isClass;
            Required = required;
            MaxLength = 0;
        }

        public TextValidationAttribute(string name, int maxLength, bool required)
        {
            Name = name;
            MaxLength = maxLength;
            Required = required;
        }
        public bool IsValid(string value)
        {
            if (Required && string.IsNullOrWhiteSpace(value))
            {
                return false;
            }
            if (IsClass)
            {
                if (!Enum.IsDefined(typeof(Enums.Class), value?.Replace(" ", "")!))
                {
                    Console.WriteLine("Invalid Class");
                    return false;
                }
            }
            if (string.IsNullOrWhiteSpace(value)) return true;

            if (MaxLength != 0 && value.Length > MaxLength)
            {
                return false;
            }
            return true;
        }
        override public string ToString()
        {
            var required = Required ? "Required" : "Not Required";
            var condation = IsClass ? "Allowed Values (Economy, Business, First)" : "";
            string constraints = $"{required}, {condation}";
            return $"{Name}:\nType: Free Text\nConstraint: {constraints}";
        }


    }
}
