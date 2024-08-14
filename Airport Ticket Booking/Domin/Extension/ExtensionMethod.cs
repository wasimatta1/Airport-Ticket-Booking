using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Domin.Extension
{
    public static class ExtensionMethod
    {
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> input, Dictionary<string,object> filters)
        {
            if (input == null)
                return null;

            //TODO: ..
            //The Filter method will filter the input list based on the conditions
            // will used Expressions to build the conditions
            // and use reflection to get the property value of the object
            // and compare it with the value in the filter using our custom expression<Func>

            return input;
        }
        public static void Print<T>(this IEnumerable<T> source, string title)
        {
            if (source == null)
                return;
            Console.WriteLine();
            Console.WriteLine("┌────────────────────────────────────┐");
            Console.WriteLine($"│   {title.PadRight(52, ' ')}│");
            Console.WriteLine("└────────────────────────────────────┘");
            Console.WriteLine();
            foreach (var item in source)
            {

                Console.WriteLine(item);
                Console.WriteLine();

            }
        }

    }
}
