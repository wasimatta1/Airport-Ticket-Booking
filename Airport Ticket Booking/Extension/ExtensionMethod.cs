﻿using Airport_Ticket_Booking.Domin;
using System.Linq.Expressions;

namespace Airport_Ticket_Booking.Extension
{
    public static class ExtensionMethod
    {
        public static IEnumerable<T>? Filter<T>(this IEnumerable<T> input, Dictionary<string, (Enums.FilterOperator, object)> filters)
        {
            if (input is null || filters is null || filters.Count == 0)
                return null;

            foreach (var filter in filters)
            {
                var filterOperator = filter.Value.Item1;
                var filterValue = filter.Value.Item2;
                ConstantExpression value = Expression.Constant(filterValue);
                ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
                MemberExpression member = Expression.Property(parameter, filter.Key);
                Expression? condition = null;
                switch (filterOperator)
                {
                    case Enums.FilterOperator.Equals:
                        condition = Expression.Equal(member, value);
                        break;
                    case Enums.FilterOperator.GreaterThan:
                        condition = Expression.GreaterThan(member, value);
                        break;
                    case Enums.FilterOperator.LessThan:
                        condition = Expression.LessThan(member, value);
                        break;
                    case Enums.FilterOperator.GreaterThanOrEqual:
                        condition = Expression.GreaterThanOrEqual(member, value);
                        break;
                    case Enums.FilterOperator.LessThanOrEqual:
                        condition = Expression.LessThanOrEqual(member, value);
                        break;
                    case Enums.FilterOperator.NotEqual:
                        condition = Expression.NotEqual(member, value);
                        break;
                }

                var lambda = Expression.Lambda<Func<T, bool>>(condition!, new ParameterExpression[] { parameter });
                input = input.Where(lambda.Compile());
            }
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
        public static void SaveInFile<T>(this IEnumerable<T> source , string fileName)
        {
            string path = $"C:\\Users\\wasim\\OneDrive\\Desktop\\C# project\\Airport Ticket Booking\\{fileName}";

            var lines = source.Select(x => x?.ToString()).ToArray();

            File.WriteAllLines(path, lines); 
          

        }
    }
}
