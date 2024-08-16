namespace Airport_Ticket_Booking.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RangeAttribute<T> : Attribute  
    {
        public string Name { get; set; }
        public T MinValue { get; set; }
        public T MaxValue { get; set; }

        public bool Required { get; set; }


        public RangeAttribute(string name,bool required)
        {
            Name = name;
            Required = required;
            MinValue = (T)(object)DateTime.Now;
            MaxValue = (T)(object)DateTime.MaxValue;
        }
        public RangeAttribute(string name, T minValue, T maxValue,bool required)
        {
            Name = name;
            Required = required;
            MinValue = minValue;
            MaxValue = maxValue;
        }
        public RangeAttribute(string name, T minValue,bool required)
        {
            Name = name;
            Required = required;
            MinValue = minValue;
            MaxValue = GetMaxValue();

        }

       public bool IsValid(T value)
       {   if (Required && value is null)
           {
               return false;
           }
           if (Comparer<T>.Default.Compare(value, MinValue) < 0)
           {
               return false;
           }
           if (Comparer<T>.Default.Compare(value, MaxValue) > 0)
           {
               return false;
           }
           return true;
       }
        public T GetMaxValue()
        {
            if (typeof(T) == typeof(int))
            {
                return (T)(object)int.MaxValue;
            }
            else if (typeof(T) == typeof(double))
            {
                return (T)(object)double.MaxValue;
            }
            else if (typeof(T) == typeof(decimal))
            {
                return (T)(object)decimal.MaxValue;
            }
            else if (typeof(T) == typeof(float))
            {
                return (T)(object)float.MaxValue;
            }
            else if (typeof(T) == typeof(long))
            {
                return (T)(object)long.MaxValue;
            }
            else if (typeof(T) == typeof(byte))
            {
                return (T)(object)byte.MaxValue;
            }

            throw new InvalidOperationException("Max value not defined for this type");
        }
        override public string ToString()
        {   var required = Required ? "Required" : "Not Required";
            string constraints = $"{required}, Allowed Range (Postive Value)";
            return $"{Name}:\nType: {typeof(T)}\nConstraint: {constraints}";
        }

    }
}
