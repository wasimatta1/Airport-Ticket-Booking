using CsvHelper;
using System.Globalization;

namespace Airport_Ticket_Booking.Domin.CSV
{
    public static class CSVRepo
    {
        public static IEnumerable<T> LoadDataFromCSVFile<T>(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader ,CultureInfo.InvariantCulture);
            return csv.GetRecords<T>().ToList();
        }


        // TODo ..
        // Method below will be used S
        public static void Print<T>(this IEnumerable<T> source, string title)
        {
            if (source == null)
                return;
            Console.WriteLine();
            Console.WriteLine("┌───────────────────────────────────────────────────────┐");
            Console.WriteLine($"│   {title.PadRight(52, ' ')}│");
            Console.WriteLine("└───────────────────────────────────────────────────────┘");
            Console.WriteLine();
            foreach (var item in source)
            {
   
                Console.WriteLine(item);
                Console.WriteLine();

            }
        }
    }
}
